using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace ProjectBoomixClient.UI.Components {

    public sealed class SpinnerComponent : UIComponent, IDisposable {

        private const double SPINNER_ROTATION_SPEED = 8f;

        private Texture2D texture;
        private float spinnerRotation;

        public SpinnerComponent(float x, float y, ContentManager contentManager) : base(x, y) {
            texture = contentManager.Load<Texture2D>(AssetsPaths.Spinner);
        }

        public override void Update(GameTime gameTime) {
            spinnerRotation += (float)(SPINNER_ROTATION_SPEED * gameTime.ElapsedGameTime.TotalSeconds);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(
                 this.texture,
                 this.position,
                 null,
                 Color.White,
                 this.spinnerRotation,
                 new Vector2(texture.Width / 2f, texture.Height / 2f),
                 1f,
                 SpriteEffects.None,
                 1f);
        }

        public void Dispose() {
            this.texture.Dispose();
        }
    }
}
