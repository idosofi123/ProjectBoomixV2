using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixCore.Game.Systems {

    public sealed class ExternalStateSystem : EntityProcessingSystem {

        private ComponentMapper<Position> positionMapper;
        private Queue<ExternalComponentChange> changesQueue;

        public ExternalStateSystem() : base(Aspect.All(IExternal.GetAllExterntalComponents())) { }

        public override void Initialize(IComponentMapperService mapperService) {
            this.positionMapper = mapperService.GetMapper<Position>();
            this.changesQueue = new Queue<ExternalComponentChange>();
        }

        public override void Process(GameTime gameTime, int entityId) {

            Position position = positionMapper.Get(entityId);

            if (position.HasChanged) {
                //System.Console.WriteLine("PROCESS CHANGE");
                this.changesQueue.Enqueue(new ExternalComponentChange(entityId, position));
                position.HasChanged = false;
            }
        }

        public ExternalComponentChange[] GetAndClearAllChanges() {
            ExternalComponentChange[] values = changesQueue.ToArray();
            changesQueue.Clear();
            return values;
        }
    }
}
