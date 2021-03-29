using ProjectBoomixCore.Networking.Packets;

namespace ProjectBoomixCore.Networking {

    public abstract class GameClientAbstraction {

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
