using ProtoBuf;
using ProjectBoomixCore.Game.Commands;

namespace ProjectBoomixCore.Networking.Packets {

    [ProtoContract]
    public class GameCommandPacket : ClientPacket {

        [ProtoMember(1)]
        public readonly int ID;

        [ProtoMember(2)]
        public readonly GameCommand command;

        public GameCommandPacket(GameCommand gameCommand, int id) {
            this.command = gameCommand;
            this.ID = id;
        }

        public override void ApplyPacket(GameRoomAbstraction host, string clientID) {
            this.command.ApplyCommand(host.Game, clientID);
        }

        public override bool CanBeDropped() {
            return true;
        }
    }
}
