using System;
using System.Collections.Generic;
using MonoGame.Extended.Entities;
using ProjectBoomixCore.Networking.Packets;

namespace ProjectBoomixCore.Networking {

    public abstract class GameClientAbstraction {

        private readonly Dictionary<int, int> serverToClientEntityID;

        public GameClientAbstraction() {
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
    }
}
