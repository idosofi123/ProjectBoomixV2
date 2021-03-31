using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBoomixCore.Game.Components {

    /// <summary>
    /// Marks a components as external - meaning that it should be sent to clients,
    /// and therefore changes within it should be marked.
    /// </summary>
    public interface IExternal {
        public bool HasChanged { get; set; }
    }
}
