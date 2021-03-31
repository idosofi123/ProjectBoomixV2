﻿using System.Collections.Generic;
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

        private readonly long TICKS_PER_FRAME = Stopwatch.Frequency / GameInstance.FPS;

        private GameInstance Game;

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
            long tickLag = 0;

            stopwatch.Start();
            while (this.IsRunning) {

                // Measure elapsed time.
                stopwatch.Stop();
                tickLag += stopwatch.Elapsed.Ticks;
                stopwatch.Start();

                // Process user input of last frame.
                this.PoolClientEvents();
                while (this.packetsToHandle.Count > 0) {
                    receivedPacket = this.packetsToHandle.Dequeue();
                    receivedPacket.Packet.ApplyPacket(this, receivedPacket.ClientID);
                }

                // Playing catch-up and updating the game state in a fixed timestep.
                while (tickLag >= TICKS_PER_FRAME) {
                    this.Game.Update();
                    tickLag -= TICKS_PER_FRAME;
                }

                // Broadcast new game state and include remaining lag for rendering extrapolation.
                this.BroadcastNewGameState(tickLag);
            }
        }

        // API exposed to incoming packets -
        public Entity GetPlayerEntity(string clientID) {
            return Game.GetEntity(this.clientIDtoEntityID[clientID]);
        }
    }
}
