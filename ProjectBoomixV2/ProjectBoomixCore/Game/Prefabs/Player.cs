using System;
using MonoGame.Extended.Entities;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixCore.Game.Prefabs {

    public static class Player {
        
        public static Entity AddPlayerEntity(World world, float initPosX = 0, float initPosY = 0) {
            Entity newPlayerEntity = world.CreateEntity();
            newPlayerEntity.Attach(new Position(initPosX, initPosY));
            newPlayerEntity.Attach(new PositionTimestamp(DateTime.Now));
            newPlayerEntity.Attach(new Velocity());
            return newPlayerEntity;
        }
    }
}
