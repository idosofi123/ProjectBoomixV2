using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace ProjectBoomixClient {

    public class ClientSystem : EntityProcessingSystem {

        public ClientSystem() : base(Aspect.All()) { }

        public override void Initialize(IComponentMapperService mapperService) {
            throw new System.NotImplementedException();
        }

        public override void Process(GameTime gameTime, int entityId) {
            throw new System.NotImplementedException();
        }
    }
}
