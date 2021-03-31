using ProtoBuf;
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

        public GameStatePacket() { }

        public GameStatePacket(ExternalComponentChange[] changes, float updateTickLag) {
            this.Changes = changes;
            this.UpdateTickLag = updateTickLag;
        }

        public override void ApplyPacket(GameClientAbstraction client) {

            foreach (ExternalComponentChange change in Changes) {

                Entity entity = client.GetEntity(change.EntityID);

                // If the entity has not been created in the client yet, do so.
                // TODO: Handle with exceptions and not null-checking
                if (entity == null) {
                    entity = client.AddNewEntity(change.EntityID);
                }

                IExternal<object> currentComponent =
                    (IExternal<object>)entity.GetType().GetMethod("Get").MakeGenericMethod(change.NewComponent.GetType()).Invoke(entity, null);

                currentComponent.SyncWithServerComponent(change.NewComponent);
            }
        }

        public override bool CanBeDropped() {
            return true;
        }
    }
}
