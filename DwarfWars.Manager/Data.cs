using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwarfWars.Manager
{
    public class Data
    {
        public string version;
        public Tile[] tileTypes;
        public Item[] itemTypes;
        public Data(string v)
        {
            version = v;
            itemTypes = new Item[2] { new Item(0, "Sword", "", ItemType.Weapon), new Item(1, "Rock", "", ItemType.Material) };
            tileTypes = new Tile[2] { new Ore(0, "Air", "", 1), new Ore(1, "Stone", "stone", 1) };
        }

        public List<Ore> GetOre()
        {
            List<Ore> output = new List<Ore>();
            foreach (Tile t in tileTypes)
            {
                if (t.TileType == TileType.Ore) output.Add((Ore)t);
            }
            return output;
        }
    }

    public class Entity
    {
        public int ID { get; private set; }
        public string Filename { get; private set; }

        public Entity(int id, string filename)
        {
            ID = id;
        }
    }

    public class DropTable
    {
        private List<int> Amounts;
        private List<int> EntityIDs;
        private List<int> Chances;

        public DropTable(List<int> amounts, List<int> entityids, List<int> chances)
        {
            Amounts = amounts ?? new List<int>();
            EntityIDs = entityids ?? new List<int>();
            Chances = chances ?? new List<int>();
        }

        public void Add(int amount, int entityid, int chance)
        {
            Amounts.Add(amount);
            EntityIDs.Add(entityid);
            Chances.Add(chance);
        }

        public void Change(int index, int amount, int entityid, int chance)
        {
            Amounts[index] = amount;
            EntityIDs[index] = entityid;
            Chances[index] = chance;
        }

        public int[] Drop()
        {
            List<int[]> output = new List<int[]>();
            Random r = new Random();
            for (int i = 0; i < EntityIDs.Count; i++)
            {
                for (int chance = 0; chance < Chances[i]; chance++)
                {
                    output.Add(new int[] { Amounts[i], EntityIDs[i] });
                }
            }
            return output[r.Next(output.Count)];
        }
    }

    public abstract class Interaction
    {
        public abstract void Run();
    }
}
