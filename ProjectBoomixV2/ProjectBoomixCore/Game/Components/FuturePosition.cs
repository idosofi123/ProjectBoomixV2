using System;

namespace ProjectBoomixCore.Game.Components {

    public class FuturePosition : Position {

        public DateTime Timestamp { get; set; }

        public FuturePosition(float x, float y, DateTime timestamp) : base(x, y) {
            this.Timestamp = timestamp;
        }
    }
}
