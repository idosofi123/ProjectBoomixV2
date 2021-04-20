using System.Collections.Generic;
using MonoGame.Extended.Entities;
using ProjectBoomixCore.Game.Systems;
using ProjectBoomixCore.Game.Prefabs;

namespace ProjectBoomixCore.Game {

    public class GameInstance : IUpdateable {

        public static readonly int FPS = 60;

        private World world;
        private ExternalStateSystem externalStateSystem;

        public GameInstance() {
            this.externalStateSystem = new ExternalStateSystem();
            this.world = new WorldBuilder().AddSystem(new MovementSystem()).AddSystem(this.externalStateSystem).Build();
        }

        public Entity AddPlayer() {
            return Player.AddPlayerEntity(world); ;
        }

        public void Update() {
            this.world.Update(null);
        }

        public ExternalComponentChange[] GetExternalChanges() {
            return this.externalStateSystem.GetAndClearAllChanges();
        }

        public Entity GetEntity(int id) {
            return world.GetEntity(id);
        }
    }
}
