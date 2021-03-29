using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectBoomixClient.Network;

namespace ProjectBoomixClient.Screening.Screens {

    public sealed class PregameScreen : Screen {

        public override void Init(ContentManager contentManager) {
            GameClient.Instance.GameStartedEvent += this.HandleGameStarted;
        }

        public override void HandleInput(GameTime gameTime, InputState inputState) {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime) {
            GameClient.Instance.PollServerEvents();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            throw new NotImplementedException();
        }

        public override void Dispose() {
            throw new NotImplementedException();
        }

        private void HandleGameStarted() {
            RaiseScreenExitedEvent(new InGameScreen());
        }
    }
}
