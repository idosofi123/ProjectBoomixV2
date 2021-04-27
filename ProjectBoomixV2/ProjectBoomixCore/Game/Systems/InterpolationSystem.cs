using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using ProjectBoomixCore.Game;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixCore.Game.Systems {

    public class InterpolationSystem : EntityProcessingSystem {

        public int LocalPlayerEntityID { get; set; }
        private ComponentMapper<Position> positionMapper;
        private ComponentMapper<PositionTimestamp> positionTsMapper;
        private ComponentMapper<FuturePosition> futurePositionMapper;

        public InterpolationSystem() : base(Aspect.All(new[] { typeof(Position), typeof(PositionTimestamp), typeof(FuturePosition) })) { }

        public override void Initialize(IComponentMapperService mapperService) {
            this.positionMapper = mapperService.GetMapper<Position>();
            this.positionTsMapper = mapperService.GetMapper<PositionTimestamp>();
            this.futurePositionMapper = mapperService.GetMapper<FuturePosition>();
        }

        public override void Process(GameTime gameTime, int entityId) {

            if (entityId != this.LocalPlayerEntityID) {

                Position currentPosition = positionMapper.Get(entityId);
                PositionTimestamp currentPosTs = positionTsMapper.Get(entityId);
                FuturePosition futurePosition = futurePositionMapper.Get(entityId);

                DateTime renderTimestamp = DateTime.Now.Subtract(TimeSpan.FromTicks(Stopwatch.Frequency / GameInstance.FPS));

                // Interpolate -
                //currentPosition.X += (futurePosition.X - currentPosition.X) *
                //    Math.Min((renderTimestamp.Ticks - currentPosTs.Timestamp.Ticks) / (futurePosition.Timestamp.Ticks - currentPosTs.Timestamp.Ticks), 1);

                //currentPosition.Y += (futurePosition.Y - currentPosition.Y) *
                //    Math.Min((renderTimestamp.Ticks - currentPosTs.Timestamp.Ticks) / (futurePosition.Timestamp.Ticks - currentPosTs.Timestamp.Ticks), 1);

                currentPosition.X = futurePosition.X;
                currentPosition.Y = futurePosition.Y;

                // In case of interpolation ending -
                if (currentPosition.Equals(futurePosition)) {
                    currentPosTs.Timestamp = futurePosition.Timestamp;
                    futurePositionMapper.Delete(entityId);
                }
            }
        }
    }
}
