using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixCore.Game.Systems {

    public sealed class MovementSystem : EntityProcessingSystem {

        private const float MOVEMENT_EASE = 0.9f;
        private const float MOVEMENT_FLOOR = 0.1f;

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

            if (entityVelocity.X != 0 || entityVelocity.Y != 0) {

                entityPosition.X += entityVelocity.X;
                entityPosition.Y += entityVelocity.Y;

                entityVelocity.X *= MOVEMENT_EASE;
                if (Math.Abs(entityVelocity.X) <= MOVEMENT_FLOOR) {
                    entityVelocity.X = 0;
                }

                entityVelocity.Y *= MOVEMENT_EASE;
                if (Math.Abs(entityVelocity.Y) <= MOVEMENT_FLOOR) {
                    entityVelocity.Y = 0;
                }

                entityPosition.HasChanged = true;
                entityVelocity.HasChanged = true;
            }
        }
    }
}
