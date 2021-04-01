using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using ProjectBoomixCore.Game;
using ProjectBoomixCore.Game.Components;
using ProjectBoomixClient.Network;
using ProjectBoomixCore.Networking.Packets;

namespace ProjectBoomixClient.Screening.Screens {

    public sealed class InGameScreen : Screen {

        private Texture2D sword;

        public override void Init(ContentManager contentManager) {
            this.sword = contentManager.Load<Texture2D>(AssetsPaths.Sword);
        }

        public override void HandleInput(InputState inputState) {

            // Movement handling
            if (inputState.isKeyDown(Keys.Right) || inputState.isKeyDown(Keys.Left)) {
                MoveDirection direction;
                if (inputState.isKeyDown(Keys.Right) && inputState.isKeyDown(Keys.Left)) {
                    direction = inputState.GetPressDuration(Keys.Right) < inputState.GetPressDuration(Keys.Left) ? MoveDirection.Right : MoveDirection.Left;
                } else {
                    direction = inputState.isKeyDown(Keys.Right) ? MoveDirection.Right : MoveDirection.Left;
                }
                GameClient.Instance.SendPacketToServer(new MovePacket(direction));
            }


        }

        public override void Update(GameTime gameTime) {
            GameClient.Instance.PollServerEvents();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, VirtualResolution.GetVirtualResolutionScale());

            spriteBatch.DrawString(GlobalResources.RegularFont, "In Game", new Vector2(40, 30), Color.White);
            spriteBatch.DrawString(GlobalResources.RegularFont, $"FPS: {(int)Math.Round(1 / gameTime.ElapsedGameTime.TotalSeconds)}", new Vector2(1740, 30), Color.White);

            // Draw game world.
            GameClient.Instance.PerformOverEntities((Entity entity) => {
                Position entityPosition = entity.Get<Position>();
                spriteBatch.Draw(sword, new Vector2(entityPosition.X, entityPosition.Y), Color.White);
            });

            spriteBatch.End();
        }

        public override void Dispose() {
            this.sword.Dispose();
        }
    }
}
