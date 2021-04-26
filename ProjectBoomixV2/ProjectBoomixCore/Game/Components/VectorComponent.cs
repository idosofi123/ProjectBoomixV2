using ProtoBuf;
using Microsoft.Xna.Framework;

namespace ProjectBoomixCore.Game.Components {

    [ProtoContract]
    [ProtoInclude(1, typeof(Position))]
    public abstract class VectorComponent : Component {

        [ProtoMember(2)]
        public float X { get; set; }

        [ProtoMember(3)]
        public float Y { get; set; }

        public VectorComponent(float x, float y) {
            this.X = x;
            this.Y = y;
        }

        public VectorComponent() : this(0, 0) { }

        public void SetFromVector(Vector2 vector) {
            this.X = vector.X;
            this.Y = vector.Y;
        }

        public override bool Equals(object obj) {
            return obj is VectorComponent && ((VectorComponent)obj).X == this.X && ((VectorComponent)obj).Y == this.Y;
        }
    } 
}
