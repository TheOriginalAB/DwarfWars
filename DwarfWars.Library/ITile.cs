using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfWars.Library
{
    public abstract class ITile
    {
        public TileType TileType;

        public abstract void OnDestroy();
        public abstract void OnBuild();
        public abstract void OnUpdate();
    }
}
