using System;
using log4net;

namespace ProjectBoomixServer {

    class Program {

        public static ILog Logger { get; } = LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args) {

            Logger.Info("Attempting to initialize server...");
            if (GameServer.Instance.Initialize()) {
                Logger.Info("Server has successfully initialized! Running...");
                GameServer.Instance.Start();
            } else {
                Logger.Error("Could not initialize server. Terminating...");
            }

        }

    }

}
