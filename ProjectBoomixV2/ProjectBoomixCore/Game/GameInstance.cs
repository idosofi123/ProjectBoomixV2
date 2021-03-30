using System.Collections.Generic;
using MonoGame.Extended.Entities;
using ProjectBoomixCore.Game.Systems;
using ProjectBoomixCore.Game.Prefabs;

namespace ProjectBoomixCore.Game {

    public class GameInstance : IUpdateable {

        public static readonly int FPS = 60;

        private World world = new WorldBuilder().AddSystem(new MovementSystem()).Build();

        public GameInstance(List<string> players, Dictionary<string, int> playerToEntityID) {
            world.Initialize();
            foreach (string player in players) {
                Entity newEntity = Player.AddPlayerEntity(world);
                playerToEntityID.Add(player, newEntity.Id);
            }
        }

        public void Update() {
            this.world.Update(null);
        }

    }
}
