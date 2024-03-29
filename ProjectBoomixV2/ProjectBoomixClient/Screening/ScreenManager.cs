﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ProjectBoomixClient.Screening {

    public sealed class ScreenManager : IUpdateable, IInputResponsive, IDrawable, IDisposable {

        private readonly ContentManager contentManager;
        private Screen activeScreen;

        public ScreenManager(ContentManager contentManager) {
            this.contentManager = contentManager;
        }

        public void SwitchScreen(Screen newScreen) {
            if (this.activeScreen != null) {
                this.activeScreen.ScreenExitedEvent -= this.SwitchScreen;
            }
            this.activeScreen?.Dispose();
            this.activeScreen = newScreen;
            this.activeScreen.ScreenExitedEvent += this.SwitchScreen;
            this.activeScreen.Init(this.contentManager);
        }

        public void HandleInput(InputState inputState) {
            this.activeScreen.HandleInput(inputState);
        }

        public void Update(GameTime gameTime) {
            this.activeScreen.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            this.activeScreen.Draw(gameTime, spriteBatch);
        }

        public void Dispose() {
            this.activeScreen?.Dispose();
        }
    }
}
