using ProtoBuf;

namespace ProjectBoomixCore.Game.Components {

    [ProtoContract]
    public class Position : VectorComponent {

        public Position(float x, float y) : base(x, y) { }

        public Position() : base(0, 0) { }
    }
}
