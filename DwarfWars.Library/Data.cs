using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfWars.Library
{
    public enum CommandType : sbyte
    {
        Welcome = -1, Connect = 1,
        Goodbye = -2, Disconnect = 2,

        Movement = -3, Location = 3,
        Interact = -4
    }

    public enum TileType : byte
    {
        Air = 0,
        Stone = 1,
        Barrier = 2,
        Dirt = 3,

        CoalOre = 4,
        TinOre = 5,
        CopperOre = 6,
        IronOre = 7,
        GoldOre = 8,
        DiamondOre = 9,

        WorkBench = 10,
        Refinery = 11

    }
    
    public enum ResourceType : byte
    {
        Coal = 0,
        TinBar = 1,
        CopperBar = 2,
        IronBar = 3,
        GoldBar = 4,
        Diamond = 5,
        
        Root = 6,
        Stick = 7,
        
    }
    
    public class Player
    {
        private readonly object Lock = new object();
        private int _XPos;
        public int XPos { get { lock (Lock) { return _XPos; } } set { lock (Lock) { _XPos = value; } } }
        
        private int _YPos;
        public int YPos { get { lock (Lock) { return _YPos; } } set { lock (Lock) { _YPos = value; } } }

        public float Rotation;
        public byte ID;
        
        public Player(int x, int y, float rotation)
        {
            XPos = x;
            YPos = y;
            Rotation = rotation;
        }

        public void SetID(byte id)
        {
            ID = id;
        }

        public void SetPos(int X, int Y)
        {
            XPos = X;
            YPos = Y;
        }

        public static Player GetPlayer(Player[] players, byte ID)
        {
            Player output = null;
            foreach(Player player in players)
            {
                if(player.ID == ID)
                {
                    output = player;
                    break;
                }
            }
            return output;
        }
    }

    public class World
    {
        public ITile[,] Map;
        public Player Creator { get; private set; }
        private int[,] map;
        private int width, height;
        private bool useRandomSeed;
        private string seed;
        private Random pseudoRandom;
        private int randomFillpercent;

        public World()
        {
            GenerateMap();
        }

        public void GenerateMap()
        {
            map = new int[width, height];
            RandomFillMap();

            for (int i = 0; i < 5; i++)
            {
                SmoothMap();
            }

            GenerateDirt();
            double abundance = 1.5;

            for (int o = 0; o < 5; o++)
            {
                for (int i = 0; i < height * width * abundance / 1024; i++)
                {
                    GenerateVeins((float)abundance, o + 3);
                }
                abundance -= .2;
            }
        }

        public void RandomFillMap()
        {
            if (useRandomSeed)
            {
                seed = DateTime.Now.ToString();
                useRandomSeed = false;
            }

            pseudoRandom = new Random(seed.GetHashCode());

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    {
                        map[x, y] = 1;
                    }
                    else
                    {
                        int temp = pseudoRandom.Next(0, 100);
                        map[x, y] = (temp < randomFillpercent) ? 1 : 0;
                    }
                }
            }
        }


    }
}
