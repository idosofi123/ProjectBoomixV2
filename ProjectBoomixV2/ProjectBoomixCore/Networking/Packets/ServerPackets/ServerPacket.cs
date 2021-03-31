using ProtoBuf;

namespace ProjectBoomixCore.Networking.Packets {

    [ProtoContract]
    [ProtoInclude(1, typeof(GameStartedPacket))]
    [ProtoInclude(2, typeof(GameStatePacket))]
    public abstract class ServerPacket : Packet {

        public abstract void ApplyPacket(GameClientAbstraction client);
    }
}
