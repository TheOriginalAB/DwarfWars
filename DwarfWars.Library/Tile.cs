using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfWars.Library
{
    public abstract class Tile
    {
        public TileType TileType;
        public bool Opaque;
        public bool Solid;
        public bool IsAir;
        
        public abstract void OnDestroy(Player target);
        public abstract void OnBuild(Player target);
        public abstract void OnUpdate(Tile[] area);
    }
}
