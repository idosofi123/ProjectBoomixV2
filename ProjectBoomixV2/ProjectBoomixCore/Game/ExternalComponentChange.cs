using MonoGame.Extended.Entities;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixCore.Game {

    public class ExternalComponentChange {

        public readonly int EntityID;
        public readonly IExternal NewComponent;

        public ExternalComponentChange(int entityID, IExternal newComponent) {
            this.EntityID = entityID;
            this.NewComponent = newComponent;
        }
    }
}
