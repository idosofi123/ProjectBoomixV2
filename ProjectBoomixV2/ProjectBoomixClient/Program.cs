using System;

namespace ProjectBoomixClient {

    public struct LaunchArguments {
        public string Host     { get; set; }
        public ushort Port     { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public static class Program {

        public static LaunchArguments LaunchArgs { get; private set; }

        [STAThread]
        public static void Main(string[] args) {

            using (var game = new MainGame()) {

                // TODO: Handle case of argument dismantling failing (illegal launch)

                string[] hostDestination = args[0].Split(new[] { ':' });
                string[] userCredentials = args[1].Split(new[] { ':' });
                LaunchArgs = new LaunchArguments {
                    Host     = hostDestination[0],
                    Port     = ushort.Parse(hostDestination[1]),
                    Username = userCredentials[0],
                    Password = userCredentials[1]
                };

                game.Run();
            }

        }

    }
}