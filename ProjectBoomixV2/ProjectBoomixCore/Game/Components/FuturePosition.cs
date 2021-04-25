using System;

namespace ProjectBoomixCore.Game.Components {

    public class FuturePosition : Position {

        public DateTime Timestamp { get; set; }

        public FuturePosition(DateTime timestamp) {
            this.Timestamp = timestamp;
        }
    }
}
