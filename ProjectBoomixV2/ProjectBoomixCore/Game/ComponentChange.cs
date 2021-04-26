
using ProtoBuf;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixCore.Game {

    public struct ComponentChange {

        public int EntityID { get; set; }

        public Component NewComponent { get; set; }
    }

    [ProtoContract]
    public struct ExternalComponentChange {

        [ProtoMember(1)]
        public string PlayerID { get; set; }

        [ProtoMember(2)]
        public Component NewComponent { get; set; }
    }
}
