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

            foreach (ExternalComponentChange change in Changes) {

                Entity entity;
                try {
                    entity = client.GetEntity(change.EntityID);
                } catch (KeyNotFoundException e) {
                    entity = client.AddNewEntity(change.EntityID);
                    entity.GetType().GetMethod("Attach").MakeGenericMethod(change.NewComponent.GetType()).Invoke(entity, new[] { change.NewComponent });
                }

                IExternal<Position> currentComponent =
                    (IExternal<Position>)entity.GetType().GetMethod("Get").MakeGenericMethod(change.NewComponent.GetType()).Invoke(entity, null);

                currentComponent.SyncWithServerComponent((Position)(change.NewComponent));
            }
        }

        public override bool CanBeDropped() {
            return true;
        }
    }
}
