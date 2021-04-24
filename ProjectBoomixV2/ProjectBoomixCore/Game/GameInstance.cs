using System.Collections.Generic;
using MonoGame.Extended.Entities;
using ProjectBoomixCore.Game.Systems;
using ProjectBoomixCore.Game.Prefabs;

namespace ProjectBoomixCore.Game {

    public class GameInstance : IUpdateable {

        public static readonly int FPS = 60;

        private World world;
        private ExternalStateSystem externalStateSystem;
        private Dictionary<string, int> playerIDToEntityID;

        public GameInstance() {
            this.externalStateSystem = new ExternalStateSystem();
            this.world = new WorldBuilder().AddSystem(new MovementSystem()).AddSystem(this.externalStateSystem).Build();
            this.playerIDToEntityID = new Dictionary<string, int>();
        }

        public Entity AddPlayer(string playerID) {
            return Player.AddPlayerEntity(world);
        }

        public void Update() {
            this.world.Update(null);
        }

        public ComponentChange[] GetChangesSnapshot() {
            return this.externalStateSystem.GetAndClearAllChanges();
        }

        public Entity GetEntity(string playerID) {
            return world.GetEntity(this.playerIDToEntityID[playerID]);
        }
    }
}
