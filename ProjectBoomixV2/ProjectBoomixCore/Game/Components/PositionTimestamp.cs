using System;

namespace ProjectBoomixCore.Game.Components {
    
    public class PositionTimestamp : Component {

        public DateTime Timestamp { get; set; }

        public PositionTimestamp(DateTime timestamp) {
            this.Timestamp = timestamp;
        }
    }
}
