using ProtoBuf;

namespace ProjectBoomixCore.Game.Components {

    [ProtoContract]
    public class Position : VectorComponent, IExternal {

        [ProtoMember(1)]
        private bool hasChanged;

        public bool HasChanged { get => hasChanged; set => hasChanged = value; }

        public Position(float x, float y) : base(x, y) {
            hasChanged = true;
        }

        public Position() : this(0, 0) { }
    }
}
