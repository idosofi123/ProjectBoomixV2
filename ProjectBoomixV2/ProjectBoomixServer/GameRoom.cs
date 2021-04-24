using System;
using System.Collections.Generic;
using LiteNetLib;
using LiteNetLib.Utils;
using ProjectBoomixCore.Game;
using ProjectBoomixCore.Networking;
using ProjectBoomixCore.Networking.Packets;

namespace ProjectBoomixServer {

    /// <summary>
    /// The LiteNetLib implementation of the abstract game room.
    /// </summary>
    public sealed class GameRoom : GameRoomAbstraction {

        private NetManager                  server;
        private Dictionary<string, NetPeer> peers;
        private Dictionary<int, string>     peerIDToClientID;
        private EventBasedNetListener       eventListener;
        private ushort                      port;

        public GameRoom(ushort port, List<string> playerWhitelist) : base(playerWhitelist) {

            this.port = port;

            this.eventListener = new EventBasedNetListener();
            this.eventListener.ConnectionRequestEvent += this.HandleConnectionRequest;
            this.eventListener.NetworkReceiveEvent    += this.HandleNetworkReceive;

            this.server = new NetManager(this.eventListener);
            this.peers = new Dictionary<string, NetPeer>();
            this.peerIDToClientID = new Dictionary<int, string>();
        }

        // LiteNetLib Event
        private async void HandleConnectionRequest(ConnectionRequest request) {

            Program.Logger.Info($"New connection request from: {request.RemoteEndPoint.Address}");

            string[] requestCredentials = request.Data.GetString().Split(':');
            string clientID = requestCredentials[0];
            string clientPassword = requestCredentials[1];

            Program.Logger.Info($"Login details - ID: {clientID}, Password: {clientPassword}");

            if (this.playersToBeApproved.Contains(clientID)) {
                NetPeer newPeer = request.Accept();
                this.peers.Add(clientID, newPeer);
                this.peerIDToClientID[newPeer.Id] = clientID;
                playersToBeApproved.Remove(clientID);
                if (playersToBeApproved.Count == 0) {
                    this.EndMatchmakingAndStartGame();
                }
            } else {
                request.Reject();
                Program.Logger.Warn($"Unwanted user attempted to connect, named: {requestCredentials[0]}");
            }

        }

        // LiteNetLib Event
        private void HandleNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod) {

            try {
                byte[] packetBytes = new byte[reader.AvailableBytes];
                reader.GetBytes(packetBytes, reader.AvailableBytes);
                base.packetsToHandle.Enqueue(
                    new SentClientPacket((ClientPacket)Packet.Deserialize(packetBytes), this.peerIDToClientID[peer.Id])
                );
            } catch (InvalidCastException e) {
                Program.Logger.Error($"Garbage packet received from peer: {peer.Id}");
            }
        }

        private void EndMatchmakingAndStartGame() {
            GameStartedPacket packet = new GameStartedPacket();
            NetDataWriter packetWrapper = new NetDataWriter();
            packetWrapper.Put(packet.Serialize());
            this.server.SendToAll(packetWrapper, DeliveryMethod.ReliableOrdered);
            Program.Logger.Info($"Game started on port {this.port}!");
        }

        public override void Start() {
            base.Start();
            this.server.Start(port);
            Program.Logger.Info($"New game room started on port: {this.port}");
        }

        protected override void PoolClientEvents() {
            this.server.PollEvents();
        }

        protected override void SendPacketToClients(ServerPacket packet) {
            NetDataWriter wrapper = new NetDataWriter();
            wrapper.Put(packet.Serialize());
            this.server.SendToAll(wrapper, packet.CanBeDropped() ? DeliveryMethod.ReliableSequenced : DeliveryMethod.ReliableOrdered);
        }

        protected override void BroadcastNewGameState(float updateTickLag) {
            ComponentChange[] changes = base.Game.GetChangesSnapshot();
            if (changes.Length > 0) {
                this.SendPacketToClients(new GameStatePacket(changes, updateTickLag));
            }
        }
    }
}
