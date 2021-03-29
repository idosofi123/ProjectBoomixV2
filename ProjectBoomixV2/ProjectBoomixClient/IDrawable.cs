using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectBoomixClient {

    interface IDrawable {
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
