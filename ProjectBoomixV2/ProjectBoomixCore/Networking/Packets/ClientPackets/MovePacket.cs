using ProtoBuf;
using MonoGame.Extended.Entities;
using ProjectBoomixCore.Game;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixCore.Networking.Packets {

    [ProtoContract]
    public class MovePacket : ClientPacket {

        [ProtoMember(1)]
        public MoveDirection Direction { get; set; }

        public MovePacket() { }

        public MovePacket(MoveDirection direction) {
            this.Direction = direction;
        }

        public override void ApplyPacket(GameRoomAbstraction host, string clientID) {
            Entity playerEntity = host.GetPlayerEntity(clientID);
            playerEntity.Get<Velocity>().X = (Direction == MoveDirection.Right) ? 5 : -5;
        }

        public override bool CanBeDropped() {
            return true;
        }
    }
}
