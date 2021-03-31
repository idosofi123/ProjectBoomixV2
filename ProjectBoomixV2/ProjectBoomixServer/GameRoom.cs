using System;
using System.Collections.Generic;
using LiteNetLib;
using LiteNetLib.Utils;
using ProjectBoomixCore.Networking;
using ProjectBoomixCore.Networking.Packets;

namespace ProjectBoomixServer {

    /// <summary>
    /// The LiteNetLib implementation of the abstract game room.
    /// </summary>
    public sealed class GameRoom : GameRoomAbstraction {

        private NetManager                  server;
        private Dictionary<string, NetPeer> peers;
        private EventBasedNetListener       eventListener;
        private ushort                      port;

        public GameRoom(ushort port, List<string> playerWhitelist) : base(playerWhitelist) {

            this.port = port;

            this.eventListener = new EventBasedNetListener();
            this.eventListener.ConnectionRequestEvent += this.HandleConnectionRequest;
            this.eventListener.NetworkReceiveEvent    += this.HandleNetworkReceive;

            this.server = new NetManager(this.eventListener);
            this.peers = new Dictionary<string, NetPeer>();
        }

        // LiteNetLib Event Handling ~

        private async void HandleConnectionRequest(ConnectionRequest request) {

            Program.Logger.Info($"New connection request from: {request.RemoteEndPoint.Address}");

            string[] requestCredentials = request.Data.GetString().Split(':');
            string clientID = requestCredentials[0];
            string clientPassword = requestCredentials[1];

            Program.Logger.Info($"Login details - ID: {clientID}, Password: {clientPassword}");

            if (this.playersToBeApproved.Contains(clientID)) {
                this.peers.Add(clientID, request.Accept());
                playersToBeApproved.Remove(clientID);
                if (playersToBeApproved.Count == 0) {
                    this.EndMatchmakingAndStartGame();
                }
            } else {
                request.Reject();
                Program.Logger.Warn($"Unwanted user attempted to connect, named: {requestCredentials[0]}");
            }

            // * VALIDATE AGAINST WEB SERVER - CURRENTLY IRRELEVANT *
            //if (this.playersToBeApproved.Contains(requestCredentials[0])) {
            //    HttpResponseMessage isValidResponse = await ValidateConnectionAttempt(requestCredentials[0], requestCredentials[1]);
            //    if (isValidResponse.StatusCode == HttpStatusCode.OK) {
            //        request.Accept();
            //        playersToBeApproved.Remove(requestCredentials[0]);
            //        if (playersToBeApproved.Count == 0) {
            //            this.EndMatchmakingAndStartGame();
            //        }
            //    } else {
            //        request.Reject();
            //    }
            //}

        }

        private void HandleNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod) {

            try {
                byte[] packetBytes = new byte[reader.AvailableBytes];
                reader.GetBytes(packetBytes, reader.AvailableBytes);
                base.packetsToHandle.Enqueue(
                    new SentClientPacket((ClientPacket)Packet.Deserialize(packetBytes), peer.Id.ToString())
                );
            } catch (InvalidCastException e) {
                Program.Logger.Error($"Garbage packet received from peer ID: {peer.Id}");
            }
        }

        // ~

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
            this.SendPacketToClients(new GameStatePacket(base.Game.GetExternalChanges(), updateTickLag));
        }
    }
}
