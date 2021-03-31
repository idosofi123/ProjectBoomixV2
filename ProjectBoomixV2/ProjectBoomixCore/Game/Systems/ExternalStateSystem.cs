using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixCore.Game.Systems {

    public sealed class ExternalStateSystem : EntityProcessingSystem {

        private ComponentMapper<IExternal> componentMapper;
        private Queue<ExternalComponentChange> changesQueue;

        public ExternalStateSystem() : base(Aspect.All(new[] { typeof(IExternal) })) { }

        public override void Initialize(IComponentMapperService mapperService) {
            this.componentMapper = mapperService.GetMapper<IExternal>();
            this.changesQueue = new Queue<ExternalComponentChange>();
        }

        public override void Process(GameTime gameTime, int entityId) {

            IExternal component = componentMapper.Get(entityId);

            if (component.HasChanged) {
                this.changesQueue.Enqueue(new ExternalComponentChange(entityId, component));
                component.HasChanged = false;
            }
        }

        public ExternalComponentChange[] GetAndClearAllChanges() {
            ExternalComponentChange[] values = changesQueue.ToArray();
            changesQueue.Clear();
            return values;
        }
    }
}
