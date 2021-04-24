using System;
using LiteNetLib;
using LiteNetLib.Utils;
using ProjectBoomixCore.Networking;
using ProjectBoomixCore.Networking.Packets;

namespace ProjectBoomixClient.Network {

    public sealed class GameClient : GameClientAbstraction {

        public static GameClient Instance { get; } = new GameClient();

        public readonly string ID;

        private NetManager            client;
        private EventBasedNetListener eventListener;
        private NetPeer               server;

        private GameClient() {
            this.eventListener = new EventBasedNetListener();
            this.eventListener.NetworkReceiveEvent += this.HandleNetworkReceive;
            this.client = new NetManager(this.eventListener);
            this.ID = Program.LaunchArgs.Username;
        }

        public void ConnectToServer() {
            this.client.Start();
            this.server = this.client.Connect(
                Program.LaunchArgs.Host,
                Program.LaunchArgs.Port,
                $"{Program.LaunchArgs.Username}:{Program.LaunchArgs.Password}"
            );

            // TODO: Handle connection failing.
        }

        public override void SendPacketToServer(ClientPacket packet) {
            NetDataWriter wrapper = new NetDataWriter();
            wrapper.Put(packet.Serialize());
            this.server.Send(wrapper, packet.CanBeDropped() ? DeliveryMethod.ReliableSequenced : DeliveryMethod.ReliableOrdered);
        }

        private void HandleNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod) {
            try {
                byte[] packetBytes = new byte[reader.AvailableBytes];
                reader.GetBytes(packetBytes, reader.AvailableBytes);
                ServerPacket receivedPacket = (ServerPacket)Packet.Deserialize(packetBytes);
                receivedPacket.ApplyPacket(this);
            } catch (InvalidCastException e) {
                // TODO: Handle, or maybe just try idk tbh
            }
        }

        public override void PollServerEvents() {
            this.client.PollEvents();
        }
    }
}
