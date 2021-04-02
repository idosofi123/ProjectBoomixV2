using ProtoBuf;

namespace ProjectBoomixCore.Game.Components {

    /// <summary>
    /// A title class.
    /// </summary>
    [ProtoContract]
    [ProtoInclude(1, typeof(VectorComponent))]
    public abstract class Component { }
}
