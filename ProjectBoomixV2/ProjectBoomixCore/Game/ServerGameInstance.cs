using System.Linq;
using MonoGame.Extended.Entities.Systems;
using ProjectBoomixCore.Game.Systems;

namespace ProjectBoomixCore.Game {

    public class ServerGameInstance : GameInstance {

        public ServerGameInstance() : base(new EntityUpdateSystem[] { new MovementSystem(), new ChangesSystem() }) { }

        public ExternalComponentChange[] GetChangesSnapshot() {

            return base.GetSystem<ChangesSystem>().GetAndClearAllChanges().Select(
                change => new ExternalComponentChange {
                    PlayerID     = base.entityIDToPlayerID[change.EntityID],
                    NewComponent = change.NewComponent
                }
            ).ToArray();
        }
    }
}
