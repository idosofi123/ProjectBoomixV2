using System;
using System.Collections.Generic;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using ProjectBoomixCore.Game.Prefabs;

namespace ProjectBoomixCore.Game {

    public abstract class GameInstance : IUpdateable {

        public static readonly int FPS = 60;

        protected World world;
        private Dictionary<Type, EntitySystem> systems;
        private Dictionary<string, int> playerIDToEntityID;

        public GameInstance(EntitySystem[] gameSystems) {

            this.systems = new Dictionary<Type, EntitySystem>();

            WorldBuilder builder = new WorldBuilder();
            foreach (EntityUpdateSystem system in gameSystems) {
                this.systems[system.GetType()] = system;
                builder.AddSystem(system);
            }

            this.world = builder.Build();

            this.playerIDToEntityID = new Dictionary<string, int>();
        }

        public Entity AddPlayer(string playerID) {
            return Player.AddPlayerEntity(world);
        }

        public void Update() {
            this.world.Update(null);
        }

        public Entity GetEntity(string playerID) {
            return world.GetEntity(this.playerIDToEntityID[playerID]);
        }

        public T GetSystem<T>() where T : EntitySystem {
            T temp = default;
            return (T)systems[temp.GetType()];
        }
    }
}
