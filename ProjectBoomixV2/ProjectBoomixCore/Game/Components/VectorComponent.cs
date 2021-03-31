using ProtoBuf;

namespace ProjectBoomixCore.Game.Components {

    [ProtoContract]
    [ProtoInclude(1, typeof(Position))]
    public abstract class VectorComponent {

        [ProtoMember(1)]
        public float X { get; set; }

        [ProtoMember(2)]
        public float Y { get; set; }

        public VectorComponent(float x, float y) {
            this.X = x;
            this.Y = y;
        }

        public VectorComponent() : this(0, 0) { }
    }
}
