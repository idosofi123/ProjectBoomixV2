using System.Collections.Generic;
using MonoGame.Extended.Entities;
using ProjectBoomixCore.Networking.Packets;

namespace ProjectBoomixCore.Networking {

    public abstract class GameClientAbstraction {

        private readonly World world;
        private readonly Dictionary<int, int> serverToClientEntityID;

        public GameClientAbstraction() {
            this.world = new WorldBuilder().Build();
            this.serverToClientEntityID = new Dictionary<int, int>();
        }

        public delegate void GameStarted();

        public event GameStarted GameStartedEvent;

        public abstract void PollServerEvents();

        public abstract void SendPacketToServer(ClientPacket packet);

        // Interface exposed to packets -
        public void HandleGameStarted() {
            this.GameStartedEvent();
        }

        public int GetClientIDByServerID(int serverID) {
            return serverToClientEntityID[serverID];
        }

        public Entity GetEntity(int serverID) {
            return this.world.GetEntity(serverToClientEntityID[serverID]);
        }

        public Entity AddNewEntity(int serverID) {
            Entity newEntity = this.world.CreateEntity();
            serverToClientEntityID[serverID] = newEntity.Id;
            return newEntity;
        }
    }
}
