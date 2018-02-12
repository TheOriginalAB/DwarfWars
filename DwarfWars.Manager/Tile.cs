using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwarfWars.Manager
{
    public enum TileType
    {
        Air, General, Ore, Patch, Barrier, Interactive
    }

    public abstract class Tile : Entity
    {
        public string Name { get; private set; }
        public TileType TileType { get; private set; }
        public bool Tangible { get; private set; }

        public Tile(int id, string n, string fn, TileType tt, bool tang) : base(id, fn)
        {
            Name = n;
            TileType = tt;
        }
    }

    public class Air : Tile
    {
        public Air(int id, string name) : base(id, name, "", TileType.Air, false)
        {

        }
    }

    public abstract class Tangible : Tile
    {
        public Tangible(int id, string n, string fn, TileType tt) : base(id, n, fn, tt, true)
        {

        }
    }

    public abstract class Breakable : Tangible
    {
        public DropTable DropTable { get; private set; }

        public Breakable(int id, string n, string fn, TileType tt, DropTable dt) : base(id, n, fn, tt)
        {
            DropTable = dt;
        }
    }


    public class Ore : Breakable
    {
        public double Abundance { get; private set; }
        public Ore(int id, string n, string fn, double abundance, DropTable dt) : base(id, n, fn, TileType.Ore, dt)
        {
            Abundance = abundance;
        }
    }

    public class Patch : Breakable
    {
        public double abundance;
        public Patch(int id, string n, string fn, double abundance, DropTable dt) : base(id, n, fn, TileType.Patch, dt)
        {
            this.abundance = abundance;
        }
    }

    public class Interactive : Breakable
    {
        public 
    }
}
