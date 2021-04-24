using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using ProjectBoomixCore.Game;
using ProjectBoomixCore.Game.Systems;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixClient {

    public class ClientGameInstance : GameInstance {

        public ClientGameInstance() : base(new EntitySystem[] { new MovementSystem() }) { }
    }
}
