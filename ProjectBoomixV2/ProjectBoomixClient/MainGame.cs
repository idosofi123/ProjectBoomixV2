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
            this.Content.RootDirectory = "Content";

            // Screening and input
            this.screenManager = new ScreenManager(this.Content);
            this.inputState = new InputState();
        }

        protected override void Initialize() {
            this.SetScreenSize(1280, 720, false);
            VirtualResolution.InitResolution(this);
            GameClient.Instance.ConnectToServer();
            base.Initialize();
        }

        protected override void LoadContent() {

            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load assets that are all across the application.
            GlobalResources.LoadGlobalResources(this.Content);

            // Set the initial screen and load its content.
            this.screenManager.SwitchScreen(new PregameScreen());
        }

        protected override void Update(GameTime gameTime) {
            this.inputState.Update(gameTime);
            this.screenManager.HandleInput(this.inputState);
            this.screenManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            this.GraphicsDevice.Clear(Color.Black);
            this.screenManager.Draw(gameTime, this.spriteBatch);
            base.Draw(gameTime);
        }

        private void SetScreenSize(int width, int height, bool isFullScreen) {
            this.graphics.PreferredBackBufferWidth = width;
            this.graphics.PreferredBackBufferHeight = height;
            this.graphics.IsFullScreen = isFullScreen;
            this.graphics.ApplyChanges();
        }
    }
}
