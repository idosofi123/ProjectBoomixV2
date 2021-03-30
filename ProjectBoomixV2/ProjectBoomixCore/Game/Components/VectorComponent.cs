using Microsoft.Xna.Framework;

namespace ProjectBoomixCore.Game.Components {

    public abstract class VectorComponent {

        public Vector2 Vector;

        public VectorComponent(float x, float y) {
            this.Vector = new Vector2(x, y);
        }

        public VectorComponent() : this(0, 0) { }

        public float X { get => Vector.X; set => Vector.X = value; }
        public float Y { get => Vector.Y; set => Vector.Y = value; }
    }
}
