using System;
using ProtoBuf;
using System.Collections.Generic;
using MonoGame.Extended.Entities;
using ProjectBoomixCore.Game;
using ProjectBoomixCore.Game.Components;

namespace ProjectBoomixCore.Networking.Packets {

    [ProtoContract]
    public class GameStatePacket : ServerPacket {

        [ProtoMember(1)]
        public ExternalComponentChange[] Changes { get; set; }

        [ProtoMember(2)]
        public float UpdateTickLag { get; set; }

        public GameStatePacket() {
            Changes = new ExternalComponentChange[0];
        }

        public GameStatePacket(ExternalComponentChange[] changes, float updateTickLag) {
            this.Changes = changes;
            this.UpdateTickLag = updateTickLag;
        }

        public override void ApplyPacket(GameClientAbstraction client) {
            
            // TODO: Currently assuming all new packets are Position packets, need to handle other types too in the future.

            foreach (ExternalComponentChange change in Changes) {

                Entity entity;

                try {
                    entity = client.Game.GetPlayerEntity(change.PlayerID);
                } catch (KeyNotFoundException e) {
                    entity = client.Game.AddPlayer(change.PlayerID);
                    entity.Attach(change.NewComponent);
                }

                Position newPosition = (Position)(change.NewComponent);
                entity.Attach<FuturePosition>(new FuturePosition(newPosition.X, newPosition.Y, DateTime.Now));
            }
        }

        public override bool CanBeDropped() {
            return true;
        }
    }
}
