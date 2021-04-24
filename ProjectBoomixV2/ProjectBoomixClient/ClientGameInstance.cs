using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities.Systems;
using ProjectBoomixCore.Game;
using ProjectBoomixCore.Game.Systems;

namespace ProjectBoomixClient {

    public class ClientGameInstance : GameInstance, IDrawable {

        public ClientGameInstance() : base(new EntitySystem[] { new MovementSystem() }) { }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            world.Draw(gameTime);
        }
    }
}
