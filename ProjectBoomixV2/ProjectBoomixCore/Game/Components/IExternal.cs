using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBoomixCore.Game.Components {

    /// <summary>
    /// Marks a components as external - meaning that it should be sent to clients,
    /// and therefore changes within it should be marked.
    /// Moreover, they should be able to sync their state with the corresponding server component.
    /// </summary>
    public interface IExternal<T> {

        public bool HasChanged { get; set; }
        public void SyncWithServerComponent(T serverComponent);
    }
}
