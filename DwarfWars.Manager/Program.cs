using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DwarfWars.Library;

namespace DwarfWars.Manager
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Data
    {
        public string version;
        public Tile[] tileTypes;
        public Item[] itemTypes;
        public Data(string v)
        {
            version = v;
            itemTypes = new Item[2] { new Item(0, "Sword", ItemType.Weapon), new Item(1, "Rock", ItemType.Material) };
            tileTypes = new Tile[2] { new Tile(0, "Air", "", TileType.Air, new int[0]), new Tile(1, "Stone", "stone", TileType.General, new int[] { 1 }) };
        }
    }

    class Tile
    {
        public int id;
        public string name;
        public string filename;
        public TileType tileType;
        public int[] droptable;

        public Tile(int id, string n, string fn, TileType tt, int[] items)
        {
            this.id = id;
            name = n;
            filename = fn;
            tileType = tt;
            droptable = items;
        }
    }

    enum TileType
    {
        Air, General, Ore, Patch, Barrier, Interactive
    }

    enum ItemType
    {
        Material,
        Weapon,
        Tool,
        Armor,
        Ammo
    }

    class Item
    {
        public int id;
        public string name;
        public ItemType itemType;

        public Item(int id, string n, ItemType item)
        {
            this.id = id;
            name = n;
            itemType = item;
        }
    }
}
