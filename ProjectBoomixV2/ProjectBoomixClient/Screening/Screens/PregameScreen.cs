using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectBoomixClient.Network;
using ProjectBoomixClient.UI.Components;

namespace ProjectBoomixClient.Screening.Screens {

    public sealed class PregameScreen : Screen {

        private const string LOADING_MESSAGE = "Match is about to begin...";
        private const int SPINNER_POS_X = 50;
        private const int SPINNER_POS_Y = 50;
        private const int TITLE_X = 93;
        private const int TITLE_Y = 24;

        private Texture2D background;
        private SpinnerComponent spinner;

        public override void Init(ContentManager contentManager) {
            background = contentManager.Load<Texture2D>(AssetsPaths.PregameBackground);
            spinner = new SpinnerComponent(SPINNER_POS_X, SPINNER_POS_Y, contentManager);

            GameClient.Instance.GameStartedEvent += this.HandleGameStarted;
        }

        public override void HandleInput(InputState inputState) {
            //throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime) {
            GameClient.Instance.PollServerEvents();
            this.spinner.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, VirtualResolution.GetVirtualResolutionScale());

            // Draw background image.
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(GlobalResources.RegularFont, $"FPS: {(int)Math.Round(1 / gameTime.ElapsedGameTime.TotalSeconds)}", new Vector2(1740, 30), Color.LightGreen);

            spinner.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(GlobalResources.RegularFont, LOADING_MESSAGE, new Vector2(TITLE_X, TITLE_Y), Color.White);

            spriteBatch.End();
        }

        public override void Dispose() {
            spinner.Dispose();
            background.Dispose();
        }

        private void HandleGameStarted() {
            RaiseScreenExitedEvent(new InGameScreen());
        }
    }
}
