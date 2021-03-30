﻿using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixCore.Game.Systems {

    public sealed class MovementSystem : EntityProcessingSystem {

        private ComponentMapper<Position> positionMapper;
        private ComponentMapper<Velocity> velocityMapper;

        public MovementSystem() : base(Aspect.All(new[] { typeof(Position), typeof(Velocity) })) { }


        public override void Initialize(IComponentMapperService mapperService) {
            this.positionMapper = mapperService.GetMapper<Position>();
            this.velocityMapper = mapperService.GetMapper<Velocity>();
        }

        public override void Process(GameTime gameTime, int entityId) {
            Position entityPosition = positionMapper.Get(entityId);
            Velocity entityVelocity = velocityMapper.Get(entityId);
            entityPosition.Vector += entityVelocity.Vector;
            entityVelocity.X = 0;
            entityVelocity.Y = 0;
        }
    }
}
