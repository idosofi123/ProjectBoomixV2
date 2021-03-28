using ProtoBuf;

namespace ProjectBoomixCore.Networking.Packets {

    [ProtoContract]
    [ProtoInclude(1, typeof(GameStartedPacket))]
    public abstract class ServerPacket : Packet {

        public abstract void ApplyPacket(GameClientAbstraction client);
    }
}
