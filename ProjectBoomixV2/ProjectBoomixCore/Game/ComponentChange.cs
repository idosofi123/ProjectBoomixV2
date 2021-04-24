using ProtoBuf;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixCore.Game {

    [ProtoContract]
    public class ComponentChange {

        [ProtoMember(1)]
        public readonly int EntityID;

        [ProtoMember(2)]
        public readonly Component NewComponent;

        public ComponentChange() { }

        public ComponentChange(int entityID, Component newComponent) {
            this.EntityID = entityID;
            this.NewComponent = newComponent;
        }
    }
}
