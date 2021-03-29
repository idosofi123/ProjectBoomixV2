using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectBoomixClient;

namespace ProjectBoomixClient {

    public static class GlobalResources {

        public static SpriteFont RegularFont { get; private set; }

        public static void LoadGlobalResources(ContentManager contentManager) {
            RegularFont = contentManager.Load<SpriteFont>(AssetsPaths.RegularFont);
        }

    }

}