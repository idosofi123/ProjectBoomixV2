using ProjectBoomixCore.Networking.Packets;

namespace ProjectBoomixCore.Networking {

    public abstract class GameClientAbstraction {

        public delegate void GameStarted();

        public abstract void SendPacketToServer(ClientPacket packet);

        public event GameStarted GameStartedEvent;

        // Interface exposed to packets -
        public void HandleGameStarted() {
            this.GameStartedEvent();
        }
    }
}
