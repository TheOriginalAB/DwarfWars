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
    
    public enum ItemType
    {
        Material,
        Weapon,
        Tool,
        Armor,
        Ammo
    }

    public class Item : Entity
    {
        public string name;
        public ItemType itemType;

        public Item(int id, string n, string fn, ItemType item) : base(id, fn)
        {
            name = n;
            itemType = item;
        }
    }
}
