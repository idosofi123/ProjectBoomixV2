using System;
using ProtoBuf;
using ProjectBoomixCore.Game;

namespace ProjectBoomixCore.Networking.Packets {

    [ProtoContract]
    public class MovePacket : ClientPacket {

        [ProtoMember(1)]
        public MoveDirection Direction { get; set; }

        public MovePacket() { }

        public MovePacket(MoveDirection direction) {
            this.Direction = direction;
        }

        public override void ApplyPacket(GameRoomAbstraction host, string clientID) {
            // VERY TEMP - Need to implement ecs in the server, so this packet will pick up the relevant entity and move it ~.~
            Console.WriteLine($"{clientID} MOVED TO THE {Direction}");
        }

        public override bool CanBeDropped() {
            return true;
        }
    }
}
