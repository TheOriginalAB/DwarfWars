using System;
using System.Collections.Generic;
using System.Linq;
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

        public PlayerAvatar Avatar;
        public byte ID;
        
        public Player(int x, int y, float rotation)
        {
            Avatar = new PlayerAvatar(x, y, rotation);
        }

        public void SetID(byte id)
        {
            ID = id;
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

    public class Team<T> where T : Player
    {
        public T[] players;

        public Team(List<T> lobby, int amount)
        {
            players = new T[amount];
        }
    }

    public class PlayerAvatar
    {
        private readonly object Lock = new object();
        private int _XPos;
        public int XPos { get { lock (Lock) { return _XPos; } } set { lock (Lock) { _XPos = value; } } }

        private int _YPos;
        public int YPos { get { lock (Lock) { return _YPos; } } set { lock (Lock) { _YPos = value; } } }

        private float _Rotation;
        public float Rotation { get { lock (Lock) { return _Rotation; } } set { lock (Lock) { _Rotation = value; } } }
    
        public void SetPos(int X, int Y)
        {
            XPos = X;
            YPos = Y;
        }

        public PlayerAvatar(int x, int y, float rotation)
        {
            XPos = x;
            YPos = y;
            Rotation = rotation;
        }
    }

    public class Data
    {
        private static readonly Data _instance = new Data();


        public Data Information { get { return _instance; } }

        private Data()
        {
            
        }
    }
}
