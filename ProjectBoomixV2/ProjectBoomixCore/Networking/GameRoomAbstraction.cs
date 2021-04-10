using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using MonoGame.Extended.Entities;
using ProjectBoomixCore.Game;
using ProjectBoomixCore.Networking.Packets;

namespace ProjectBoomixCore.Networking {

    /// <summary>
    /// The logical skeleton of a game room.
    /// This class should be inherited by game room implementations that depend on external tools and frameworks.
    /// </summary>
    public abstract class GameRoomAbstraction {

        private static readonly long TICKS_PER_FRAME = 1000 / GameInstance.FPS;

        protected GameInstance Game;

        private Dictionary<string, int> clientIDtoEntityID;

        protected Thread                  gameRoomThread;
        protected List<string>            playersToBeApproved;
        protected Queue<SentClientPacket> packetsToHandle;

        public bool IsRunning { get; protected set; }

        public GameRoomAbstraction(List<string> playerWhitelist) {
            this.gameRoomThread = new Thread(this.RunHostLoop);
            this.packetsToHandle = new Queue<SentClientPacket>();
            this.playersToBeApproved = playerWhitelist;
            this.clientIDtoEntityID = new Dictionary<string, int>();
            this.Game = new GameInstance(playerWhitelist, this.clientIDtoEntityID);
        }

        protected abstract void PoolClientEvents();

        protected abstract void BroadcastNewGameState(float updateTickLag);

        protected abstract void SendPacketToClients(ServerPacket packet);

        public virtual void Start() {
            this.IsRunning = true;
            this.gameRoomThread.Start();
        }

        public virtual void Stop() {
            this.IsRunning = false;
        }

        private void RunHostLoop() {

            SentClientPacket receivedPacket;
            Stopwatch stopwatch = new Stopwatch();
            Stopwatch fpsWatch = new Stopwatch();
            long tickLag = 0;
            int fps = 0;

            stopwatch.Start();
            fpsWatch.Start();

            while (this.IsRunning) {

                // Process user input of last frame.
                this.PoolClientEvents();
                while (this.packetsToHandle.Count > 0) {
                    receivedPacket = this.packetsToHandle.Dequeue();
                    receivedPacket.Packet.ApplyPacket(this, receivedPacket.ClientID);
                }

                // Playing catch-up and updating the game state in a fixed timestep.
                if (stopwatch.ElapsedMilliseconds >= TICKS_PER_FRAME) {
                    this.Game.Update();
                    fps++;
                    tickLag -= TICKS_PER_FRAME;
                    stopwatch.Restart();
                }

                // Broadcast new game state and include remaining lag for rendering extrapolation.
                this.BroadcastNewGameState(tickLag);

                // temp fps checking
                if (fpsWatch.ElapsedMilliseconds >= 1000) {
                    System.Console.WriteLine("FPS: " + fps);
                    fps = 0;
                    fpsWatch.Restart();
                }
            }
        }

        // API exposed to incoming packets -
        public Entity GetPlayerEntity(string clientID) {
            return Game.GetEntity(this.clientIDtoEntityID[clientID]);
        }
    }
}
