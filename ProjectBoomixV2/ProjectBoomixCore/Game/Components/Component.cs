﻿using ProtoBuf;

namespace ProjectBoomixCore.Game.Components {

    [ProtoContract]
    [ProtoInclude(1, typeof(VectorComponent))]
    public abstract class Component {

        [ProtoMember(1)]
        public bool HasChanged { get; set; }

        public Component() {
            HasChanged = true;
        }
    }
}
