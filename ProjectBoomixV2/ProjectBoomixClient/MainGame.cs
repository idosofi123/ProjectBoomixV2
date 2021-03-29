using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectBoomixClient.Screening;
using ProjectBoomixClient.Screening.Screens;
using ProjectBoomixClient.Network;

namespace ProjectBoomixClient {

    public class MainGame : Game {

        private GraphicsDeviceManager graphics;
        private SpriteBatch           spriteBatch;
        private ScreenManager         screenManager;
        private readonly InputState   inputState;

        public MainGame() {

            // Graphics and assets
            this.graphics = new GraphicsDeviceManager(this);
            this.SetScreenSize(1024, 596, false);
            this.Content.RootDirectory = "Content";
            VirtualResolution.InitResolution(this);
            GlobalResources.LoadGlobalResources(this.Content);

            // Screening and input
            this.screenManager = new ScreenManager(this.Content);
            this.inputState = new InputState();
        }

        protected override void Initialize() {
            base.Initialize();
            GameClient.Instance.ConnectToServer();
        }

        protected override void LoadContent() {

            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            // Set the initial screen and load its content.
            this.screenManager.SwitchScreen(new PregameScreen());
        }

        protected override void Update(GameTime gameTime) {
            this.inputState.Update(gameTime);
            this.screenManager.HandleInput(gameTime, this.inputState);
            this.screenManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            this.GraphicsDevice.Clear(Color.Black);
            this.screenManager.Draw(gameTime, this.spriteBatch);
            base.Draw(gameTime);
        }

        private void SetScreenSize(int width, int height, bool isFullScreen) {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.IsFullScreen = isFullScreen;
            graphics.ApplyChanges();
        }
    }
}
