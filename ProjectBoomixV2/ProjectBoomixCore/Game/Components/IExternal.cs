using System;
using System.Reflection;
using System.Linq;
using ProtoBuf;

namespace ProjectBoomixCore.Game.Components {

    /// <summary>
    /// This may be an anti-pattern, but couldn't find a better solution for generalizing an external component.
    /// Do NOT implement this interface directly.
    /// </summary>
    public interface IExternal {

        public bool HasChanged { get; set; }

        public static Type[] GetAllExterntalComponents() {
            Type externalType = typeof(IExternal);
            return Assembly.GetExecutingAssembly().GetTypes().Where((type) => externalType.IsAssignableFrom(type) && type.IsClass).ToArray();
        }
    }

    /// <summary>
    /// Marks a components as external - meaning that it should be sent to clients,
    /// and therefore changes within it should be marked.
    /// Moreover, they should be able to sync their state with the corresponding server component.
    /// </summary>
    public interface IExternal<in T> : IExternal {
        public void SyncWithServerComponent(T serverComponent);
    }
}
