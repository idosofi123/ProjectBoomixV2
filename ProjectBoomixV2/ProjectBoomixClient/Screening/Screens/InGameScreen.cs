using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectBoomixClient.Network;

namespace ProjectBoomixClient.Screening.Screens {

    public sealed class InGameScreen : Screen {

        public override void Init(ContentManager contentManager) {
            //throw new NotImplementedException();
        }

        public override void HandleInput(InputState inputState) {
            //throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime) {
            //throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, VirtualResolution.GetVirtualResolutionScale());
            spriteBatch.DrawString(GlobalResources.RegularFont, "In Game", new Vector2(40, 30), Color.White);
            spriteBatch.DrawString(GlobalResources.RegularFont, $"FPS: {(int)Math.Round(1 / gameTime.ElapsedGameTime.TotalSeconds)}", new Vector2(1740, 30), Color.White);
            spriteBatch.End();
        }

        public override void Dispose() {
            //throw new NotImplementedException();
        }
    }
}
