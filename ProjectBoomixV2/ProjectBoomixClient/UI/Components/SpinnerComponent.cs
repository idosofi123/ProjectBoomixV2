using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace ProjectBoomixClient.UI.Components {

    public sealed class SpinnerComponent : UIComponent, IDisposable {

        private const float MAX_ROTATION_SPEED     = 15f;
        private const float ROTATION_SPEED_DECLINE = 8f;

        private Texture2D texture;
        private float rotation;
        private float rotationSpeed;

        public SpinnerComponent(float x, float y, ContentManager contentManager) : base(x, y) {
            texture = contentManager.Load<Texture2D>(AssetsPaths.Spinner);
            this.rotationSpeed = MAX_ROTATION_SPEED;
        }

        public override void Update(GameTime gameTime) {
            rotation += (float)(this.rotationSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            rotationSpeed -= (float)(ROTATION_SPEED_DECLINE * gameTime.ElapsedGameTime.TotalSeconds);
            rotationSpeed = (rotationSpeed <= 5f) ? MAX_ROTATION_SPEED : rotationSpeed;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(
                 this.texture,
                 this.position,
                 null,
                 Color.White,
                 this.rotation,
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
