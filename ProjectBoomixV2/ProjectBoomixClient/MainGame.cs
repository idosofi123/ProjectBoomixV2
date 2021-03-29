using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectBoomixClient.Network;

namespace ProjectBoomixClient {

    public class MainGame : Game {

        private GraphicsDeviceManager graphics;
        private SpriteBatch           spriteBatch;
        private readonly InputState   inputState;

        public MainGame() {

            // Graphics and Assets
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";

            this.inputState = new InputState();
        }

        protected override void Initialize() {
            base.Initialize();
            GameClient.Instance.ConnectToServer();
        }

        protected override void LoadContent() {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            this.inputState.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
