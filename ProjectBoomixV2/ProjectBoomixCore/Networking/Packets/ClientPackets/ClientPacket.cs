using ProtoBuf;

namespace ProjectBoomixCore.Networking.Packets {

    [ProtoContract]
    [ProtoInclude(1, typeof(MovePacket))]
    public abstract class ClientPacket : Packet {

        public abstract void ApplyPacket(GameRoomAbstraction host, string clientID);
    }
}
