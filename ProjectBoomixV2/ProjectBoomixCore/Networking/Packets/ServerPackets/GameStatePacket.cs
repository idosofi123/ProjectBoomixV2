
namespace ProjectBoomixCore.Networking.Packets.ServerPackets {

    public class GameStatePacket : ServerPacket {

        public override void ApplyPacket(GameClientAbstraction client) {
            
        }

        public override bool CanBeDropped() {
            return true;
        }
    }
}
