using ProtoBuf;
using MonoGame.Extended.Entities;
using ProjectBoomixCore.Game;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixCore.Game.Commands {

    [ProtoContract]
    [ProtoInclude(1, typeof(MoveCommand))]
    public abstract class GameCommand {

        public void ApplyCommand(GameInstance game, string playerID) {
            ApplyCommand(game.GetEntity(playerID));
        }

        protected abstract void ApplyCommand(Entity playerEntity);
    }
}
