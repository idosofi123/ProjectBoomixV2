using Microsoft.Xna.Framework;

namespace ProjectBoomixClient {

    public static class VirtualResolution {

        public const float VirtualResWidth = 1920;
        public const float VirtualResHeight = 1080;

        private static Game game;

        public static void InitResolution(Game gameToFocus) {
            game = gameToFocus;
        }

        public static Matrix GetVirtualResolutionScale() {
            return Matrix.CreateScale(
                new Vector3(
                    game.GraphicsDevice.Viewport.Width / VirtualResWidth,
                    game.GraphicsDevice.Viewport.Height / VirtualResHeight,
                    1
                )
            );
        }

    }

}