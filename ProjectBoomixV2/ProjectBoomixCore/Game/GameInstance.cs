using System;
using System.Collections.Generic;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using ProjectBoomixCore.Game.Prefabs;

namespace ProjectBoomixCore.Game {

    public abstract class GameInstance : IUpdateable {

        public static readonly int FPS = 60;

        protected World world;
        protected Dictionary<string, int> playerIDToEntityID;
        protected Dictionary<int, string> entityIDToPlayerID;

        private Dictionary<Type, EntitySystem> systems;

        public GameInstance(EntitySystem[] gameSystems) {

            this.systems = new Dictionary<Type, EntitySystem>();

            WorldBuilder builder = new WorldBuilder();
            foreach (EntityUpdateSystem system in gameSystems) {
                this.systems[system.GetType()] = system;
                builder.AddSystem(system);
            }

            this.world = builder.Build();

            this.playerIDToEntityID = new Dictionary<string, int>();
            this.entityIDToPlayerID = new Dictionary<int, string>();
        }

        public Entity AddPlayer(string playerID, float x = 0, float y = 0) {
            Entity newEntity = Player.AddPlayerEntity(world, x, y);
            this.playerIDToEntityID[playerID] = newEntity.Id;
            this.entityIDToPlayerID[newEntity.Id] = playerID;
            return newEntity;
        }

        public void Update() {
            this.world.Update(null);
        }

        public Entity GetPlayerEntity(string playerID) {
            try {
                return world.GetEntity(this.playerIDToEntityID[playerID]);
            } catch (KeyNotFoundException ex) {
                throw ex;
            }
        }

        public T GetSystem<T>() where T : EntitySystem {
            return (T)systems[typeof(T)];
        }

        public delegate void EntityAction(Entity entity);

        public void PerformOverEntities(EntityAction action) {
            foreach (int id in playerIDToEntityID.Values) {
                action(this.world.GetEntity(id));
            }
        }
    }
}
