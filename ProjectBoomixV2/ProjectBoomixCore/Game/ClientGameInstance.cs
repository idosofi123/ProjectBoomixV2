using MonoGame.Extended.Entities.Systems;
using ProjectBoomixCore.Game.Systems;

namespace ProjectBoomixCore.Game {

    public class ClientGameInstance : GameInstance {

        public ClientGameInstance() : base(new EntitySystem[] { new MovementSystem(), new InterpolationSystem() }) { }
    }
}
