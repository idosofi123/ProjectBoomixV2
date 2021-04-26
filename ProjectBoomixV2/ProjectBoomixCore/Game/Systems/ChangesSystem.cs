using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixCore.Game.Systems {

    public sealed class ChangesSystem : EntityProcessingSystem {

        private ComponentMapper<Position> positionMapper;
        private Queue<ComponentChange> changesQueue;

        public ChangesSystem() : base(Aspect.All()) { }

        public override void Initialize(IComponentMapperService mapperService) {
            this.positionMapper = mapperService.GetMapper<Position>();
            this.changesQueue = new Queue<ComponentChange>();
        }

        public override void Process(GameTime gameTime, int entityId) {

            Position position = positionMapper.Get(entityId);

            if (position.HasChanged) {
                this.changesQueue.Enqueue(new ComponentChange { EntityID = entityId, NewComponent = position });
                position.HasChanged = false;
            }
        }

        public ComponentChange[] GetAndClearAllChanges() {
            ComponentChange[] values = changesQueue.ToArray();
            changesQueue.Clear();
            return values;
        }
    }
}
