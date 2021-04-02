using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ProjectBoomixServer {

    public sealed class GameServer {

        public static GameServer Instance { get; } = new GameServer();

        private readonly Dictionary<ushort, GameRoom> gameRooms;

        public readonly HttpClient WebServerClient;

        private GameServer() {
            this.gameRooms = new Dictionary<ushort, GameRoom>();
            this.WebServerClient = new HttpClient();
        }

        public void Start() {
            // TODO: Main server loop - creates game rooms for users to connect to and such, communicate with rest API server

            // Temporarily - add a single game room for the testing
            GameRoom tempGameRoom = new GameRoom(17420, new List<string>(new[] { "idoplay", "tay2pie" }));
            gameRooms.Add(17420, tempGameRoom);

            tempGameRoom.Start();
        }

        public bool Initialize() {
            return true;
        }

        public void Deinitialize() {

        }

    }

}
