using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ProjectBoomixClient.Screening {

    public abstract class Screen : IUpdateable, IDrawable, IDisposable {

        public delegate void ScreenExited(Screen nextScreen);

        public event ScreenExited ScreenExitedEvent;

        protected void RaiseScreenExitedEvent(Screen nextScreen) {
            ScreenExitedEvent?.Invoke(nextScreen);
        }

        public abstract void Init(ContentManager contentManager);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void HandleInput(GameTime gameTime, InputState inputState);

        public abstract void Dispose();

    }

}
