using System.Collections.Generic;
using ProjectBoomixCore.Game;
using ProjectBoomixCore.Game.Commands;
using ProjectBoomixCore.Networking.Packets;

namespace ProjectBoomixCore.Networking {

    public abstract class GameClientAbstraction {

        public readonly ClientGameInstance Game;
        public string ID { get; protected set; }

        private int gameCommandPacketSequencer;

        public GameClientAbstraction() {
            this.Game = new ClientGameInstance();
            this.Game.AddPlayer(this.ID, 100, 100); // temp
            this.gameCommandPacketSequencer = 0;
        }

        public delegate void GameStarted();

        public event GameStarted GameStartedEvent;

        public abstract void PollServerEvents();

        public abstract void SendPacketToServer(ClientPacket packet);

        public virtual void SendGameCommandToServer(GameCommand command) {
            GameCommandPacket packet = new GameCommandPacket(command, this.gameCommandPacketSequencer++);
            this.SendPacketToServer(packet);
        }

        // Interface exposed to packets -
        public void HandleGameStarted() {
            this.GameStartedEvent();
        }
    }
}
