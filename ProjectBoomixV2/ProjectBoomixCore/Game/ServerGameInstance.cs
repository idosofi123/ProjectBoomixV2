using MonoGame.Extended.Entities.Systems;
using ProjectBoomixCore.Game.Systems;

namespace ProjectBoomixCore.Game {

    public class ServerGameInstance : GameInstance {

        public ServerGameInstance() : base(new EntityUpdateSystem[] { new MovementSystem(), new ChangesSystem() }) { }

        public ComponentChange[] GetChangesSnapshot() {
            return base.GetSystem<ChangesSystem>().GetAndClearAllChanges();
        }
    }
}
