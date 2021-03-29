using ProtoBuf;

namespace ProjectBoomixCore.Networking.Packets {

    [ProtoContract]
    public class GameStartedPacket : ServerPacket {

        public override void ApplyPacket(GameClientAbstraction client) {
            client.HandleGameStarted();
        }

        public override bool CanBeDropped() {
            return false;
        }
    }
}
