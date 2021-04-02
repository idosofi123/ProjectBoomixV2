using ProtoBuf;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixCore.Game {

    [ProtoContract]
    public class ExternalComponentChange {

        [ProtoMember(1)]
        public readonly int EntityID;

        [ProtoMember(2)]
        public readonly Component NewComponent;

        public ExternalComponentChange() { }

        public ExternalComponentChange(int entityID, Component newComponent) {
            this.EntityID = entityID;
            this.NewComponent = newComponent;
        }
    }
}
