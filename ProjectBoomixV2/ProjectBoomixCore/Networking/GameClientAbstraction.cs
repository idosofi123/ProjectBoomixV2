using ProjectBoomixCore.Networking.Packets;

namespace ProjectBoomixCore.Networking {

    public abstract class GameClientAbstraction {

        public delegate void GameStarted();

        public event GameStarted GameStartedEvent;

        protected abstract void PoolServerEvents();

        public abstract void SendPacketToServer(ClientPacket packet);

        // Interface exposed to packets -
        public void HandleGameStarted() {
            this.GameStartedEvent();
        }
    }
}
