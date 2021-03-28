
namespace ProjectBoomixCore.Networking.Packets {

    public struct SentClientPacket {

        public ClientPacket Packet { get; }
        public string ClientID { get; }

        public SentClientPacket(ClientPacket packet, string sender) {
            this.Packet = packet;
            this.ClientID = sender;
        }
    }
}