using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectBoomixClient.UI {

    public abstract class UIComponent : IUpdateable, IDrawable {

        protected Vector2 position;

        public UIComponent(float x, float y) {
            this.position = new Vector2(x, y);
        }

        public UIComponent() : this(0, 0) { }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    }
}
