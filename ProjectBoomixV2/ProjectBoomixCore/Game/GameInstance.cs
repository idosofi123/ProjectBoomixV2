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

        public void AddPlayer(string playerID, float x = 0, float y = 0) {
            this.playerIDToEntityID[playerID] = Player.AddPlayerEntity(world, x, y).Id;
        }

        public void Update() {
            this.world.Update(null);
        }

        public Entity GetPlayerEntity(string playerID) {
            return world.GetEntity(this.playerIDToEntityID[playerID]);
        }

        public T GetSystem<T>() where T : EntitySystem {
            T temp = default;
            return (T)systems[temp.GetType()];
        }

        public delegate void EntityAction(Entity entity);

        public void PerformOverEntities(EntityAction action) {
            foreach (int id in playerIDToEntityID.Values) {
                action(this.world.GetEntity(id));
            }
        }
    }
}
