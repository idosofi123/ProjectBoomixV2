﻿using ProtoBuf;
using MonoGame.Extended.Entities;

namespace ProjectBoomixCore.Game.Commands {

    [ProtoContract]
    [ProtoInclude(1, typeof(MoveCommand))]
    public abstract class GameCommand {

        public void ApplyCommand(GameInstance game, string playerID) {
            ApplyCommand(game.GetPlayerEntity(playerID));
        }

        protected abstract void ApplyCommand(Entity playerEntity);
    }
}