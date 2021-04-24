using ProtoBuf;
using MonoGame.Extended.Entities;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixCore.Game.Commands {

    [ProtoContract]
    public class MoveCommand : GameCommand {

        private const float MOVEMENT_SPEED = 5f;

        [ProtoMember(1)]
        public MoveDirection Direction { get; set; }

        protected override void ApplyCommand(Entity playerEntity) {
            playerEntity.Get<Velocity>().SetFromVector(DirectionUtil.Directions[this.Direction] * MOVEMENT_SPEED);
        }
    }
}
