using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ProjectBoomixCore.Game;
using ProjectBoomixCore.Networking.Packets;

namespace ProjectBoomixCore.Networking {

    /// <summary>
    /// The logical skeleton of a game room.
    /// This class should be inherited by game room implementations that depend on external tools and frameworks.
    /// </summary>
    public abstract class GameRoomAbstraction {

        private static readonly long TICKS_PER_FRAME = Stopwatch.Frequency / GameInstance.FPS;

        public ServerGameInstance Game;

        protected Thread                  gameRoomThread;
        protected List<string>            playersToBeApproved;
        protected Queue<SentClientPacket> packetsToHandle;

        public bool IsRunning { get; protected set; }

        public GameRoomAbstraction(List<string> playerWhitelist) {
            this.gameRoomThread = new Thread(this.RunHostLoop);
            this.packetsToHandle = new Queue<SentClientPacket>();
            this.playersToBeApproved = playerWhitelist;

            this.Game = new ServerGameInstance();
            foreach (string playerID in this.playersToBeApproved) {
                this.Game.AddPlayer(playerID);
            }
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
            long tickCounter = 0, tickRemainder = 0, frame = 0;

            stopwatch.Start();
            fpsWatch.Start();

            while (this.IsRunning) { 

                // Process user input of last frame.
                this.PoolClientEvents();
                while (this.packetsToHandle.Count > 0) {
                    receivedPacket = this.packetsToHandle.Dequeue();
                    receivedPacket.Packet.ApplyPacket(this, receivedPacket.ClientID);
                }

                tickCounter = stopwatch.ElapsedTicks;
                
                if (tickRemainder > 0) {
                    tickCounter += tickRemainder;
                    tickRemainder = 0;
                }

                // Playing catch-up and updating the game state in a fixed timestep.
                if (tickCounter >= TICKS_PER_FRAME) {
                    stopwatch.Restart();
                    while (tickCounter >= TICKS_PER_FRAME) {

                        this.Game.Update();
                        tickCounter -= TICKS_PER_FRAME;
                        frame++;

                        // Broadcast new game state and include remaining lag for potential rendering extrapolation.
                        if (tickCounter < TICKS_PER_FRAME) {
                            this.BroadcastNewGameState(tickCounter);
                            tickRemainder = tickCounter;
                        }
                    }
                }

                if (fpsWatch.ElapsedMilliseconds >= 1000) {
                    System.Console.WriteLine($"FPS: {frame}");
                    frame = 0;
                    fpsWatch.Restart();
                }
            }
        }
    }
}
