using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixCore.Game.Systems {

    public class InterpolationSystem : EntityProcessingSystem {

        private ComponentMapper<Position> positionMapper;
        private ComponentMapper<Position> futurePositionMapper;

        public InterpolationSystem() : base(Aspect.All(new[] { typeof(Position), typeof(FuturePosition) })) { }

        public override void Initialize(IComponentMapperService mapperService) {
            this.positionMapper = mapperService.GetMapper<Position>();
            this.futurePositionMapper = mapperService.GetMapper<FuturePosition>();
        }

        public override void Process(GameTime gameTime, int entityId) {
            throw new NotImplementedException();
        }
    }
}
