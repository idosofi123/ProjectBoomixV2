using MonoGame.Extended.Entities.Systems;
using ProjectBoomixCore.Game.Systems;

namespace ProjectBoomixCore.Game {

    public class ClientGameInstance : GameInstance {

        public ClientGameInstance(string localPlayerID) : base(new EntitySystem[] { new MovementSystem(), new InterpolationSystem() }) {

            // Add the entity of the local player, and set it within the interpolation system so it'll know which entity not to handle.
            this.AddPlayer(localPlayerID, 0, 0);
            this.GetSystem<InterpolationSystem>().LocalPlayerEntityID = base.playerIDToEntityID[localPlayerID];
        }
    }
}
