using System.Collections.Generic;
using MonoGame.Extended.Entities;
using ProjectBoomixCore.Game.Systems;
using ProjectBoomixCore.Game.Prefabs;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixCore.Game {

    public class GameInstance : IUpdateable {

        public static readonly int FPS = 60;

        private World world;
        private ExternalStateSystem externalStateSystem;

        public GameInstance(List<string> players, Dictionary<string, int> playerToEntityID) {

            this.externalStateSystem = new ExternalStateSystem();
            this.world = new WorldBuilder().AddSystem(new MovementSystem()).AddSystem(this.externalStateSystem).Build();

            foreach (string player in players) {
                Entity newEntity = Player.AddPlayerEntity(world);
                playerToEntityID.Add(player, newEntity.Id);
            }
        }

        public void Update() {
            this.world.Update(null);
        }

        public ExternalComponentChange[] GetExternalChanges() {
            return this.externalStateSystem.GetAndClearAllChanges();
        }

    }
}
