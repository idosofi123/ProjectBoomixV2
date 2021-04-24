using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectBoomixCore.Game;
using ProjectBoomixCore.Game.Commands;
using ProjectBoomixCore.Networking.Packets;
using ProjectBoomixClient.Network;

namespace ProjectBoomixClient.Screening.Screens {

    public sealed class InGameScreen : Screen {

        // Logical members
        private GameInstance game;
        private int gameCommandPacketSequencer;

        // UI-related members
        private Texture2D sword;

        public override void Init(ContentManager contentManager) {
            this.gameCommandPacketSequencer = 0;
            this.sword = contentManager.Load<Texture2D>(AssetsPaths.Sword);
        }

        public override void HandleInput(InputState inputState) {

            // Movement handling
            if (inputState.isKeyDown(Keys.Right)
             || inputState.isKeyDown(Keys.Left)
             || inputState.isKeyDown(Keys.Up)
             || inputState.isKeyDown(Keys.Down)) {

                MoveDirection direction = MoveDirection.Right;
                long minPressDuration = inputState.GetPressDuration(Keys.Right);


                // Determine movement direction.
                if (inputState.GetPressDuration(Keys.Left) < minPressDuration && inputState.GetPressDuration(Keys.Left) > 0) {
                    direction = MoveDirection.Left;
                    minPressDuration = inputState.GetPressDuration(Keys.Left);

                } else if (inputState.GetPressDuration(Keys.Up) < minPressDuration && inputState.GetPressDuration(Keys.Up) > 0) {
                    direction = MoveDirection.Up;
                    minPressDuration = inputState.GetPressDuration(Keys.Up);

                } else if (inputState.GetPressDuration(Keys.Down) < minPressDuration && inputState.GetPressDuration(Keys.Down) > 0) {
                    direction = MoveDirection.Down;
                    minPressDuration = inputState.GetPressDuration(Keys.Down);
                }

                MoveCommand command = new MoveCommand(direction);

                // Apply command locally (client-side prediction), and send it to the server.
                command.ApplyCommand(this.game, GameClient.Instance.ID);
                GameCommandPacket packet = new GameCommandPacket(command, gameCommandPacketSequencer++);
                GameClient.Instance.SendPacketToServer(packet);
            }
        }

        public override void Update(GameTime gameTime) {
            GameClient.Instance.PollServerEvents();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, VirtualResolution.GetVirtualResolutionScale());

            spriteBatch.DrawString(GlobalResources.RegularFont, "In Game", new Vector2(40, 30), Color.White);
            spriteBatch.DrawString(GlobalResources.RegularFont, $"FPS: {(int)Math.Round(1 / gameTime.ElapsedGameTime.TotalSeconds)}", new Vector2(1740, 30), Color.LightGreen);

            // Draw game world.
            //GameClient.Instance.PerformOverEntities((Entity entity) => {
            //    Position entityPosition = entity.Get<Position>();
            //    spriteBatch.Draw(sword, new Vector2(entityPosition.X, entityPosition.Y), Color.White);
            //});

            spriteBatch.End();
        }

        public override void Dispose() {
            this.sword.Dispose();
        }
    }
}
