﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SharpNoise;
using SharpNoise.Modules;
using SharpNoise.Builders;

namespace DwarfWars.Library
{
    
    public abstract class IWorld<T> where T : Player
    {
        public GameState GameState;
        public Player Creator;

        public IWorld(GameState gameState, Player creator)
        {
            GameState = gameState;
            Creator = creator;
        }

        public abstract void Transition(ref IWorld<T> world);

        
    }

    public class Lobby<T> : IWorld<T> where T : Player
    {
        public List<T> Players;

        public Lobby(List<T> players, Player creator) : base(GameState.Lobby, creator)
        {
            Players = players;
        }

        public override void Transition(ref IWorld<T> world)
        {
            world = new InGame<T>((Lobby<T>)world, CreateTeams(Players));
        }

        public Team<T>[] CreateTeams(List<T> players)
        {
            Team<T>[] output = new Team<T>[2];

            return output;
        }
    }

    public class InGame<T> : IWorld<T> where T : Player
    {
        public ITile[,] Map;
        public Team<T>[] Teams;

        private readonly int width = 150;
        private readonly int height = 150;
        private bool useRandomSeed;
        private string seed;
        private Random pseudoRandom;
        private readonly int randomFillpercent = 60;
        private int[,] map;

        public InGame(Lobby<T> lobby, Team<T>[] teams) : base(GameState.Game, lobby.Creator)
        {
            Map = new ITile[width, height];
            Teams = teams;
            GenerateMap();
        }


#region Generation logic 
        public void GenerateMap()
        {
            map = new int[width, height];
            RandomFillMap();

            for (int i = 0; i < 5; i++)
            {
                SmoothMap();
            }

        }

        private void GeneratePatch(int abundance, int tileNum)
        {
            NoiseMap output = new NoiseMap();
            Perlin per = new Perlin() { Seed = seed.GetHashCode(), Frequency = 1, OctaveCount = Perlin.DefaultOctaveCount, Lacunarity = Perlin.DefaultLacunarity, Persistence = Perlin.DefaultPersistence, Quality = NoiseQuality.Best };
            var noiseMapBuilder = new PlaneNoiseMapBuilder() { DestNoiseMap = output, SourceModule = per };

            noiseMapBuilder.SetDestSize(width, height);
            noiseMapBuilder.SetBounds(-2, 3, -2, 3);
            noiseMapBuilder.Build();

            for (int r = 0; r < width; r++)
            {
                for (int c = 0; c < height; c++)
                {
                    if (map[r, c] != 0)
                    {
                        map[r, c] = (Math.Abs(output[r, c]) * 100) > 80 ? 2 : map[r, c];
                    }
                }
            }
        }

        private void GenerateVeins(float abundance, int tileNum)
        {

            Vector2 veinPos = new Vector2(pseudoRandom.Next(0, width - 1), pseudoRandom.Next(0, height - 1));
            int veinLength = (int)(pseudoRandom.NextDouble() * pseudoRandom.NextDouble() * 75 * abundance);

            for (int i = 0; i < veinLength; i++)
            {
                veinPos.X += pseudoRandom.Next(-1, 2);
                veinPos.Y += pseudoRandom.Next(-1, 2);

                if (IsInMapRange((int)veinPos.X, (int)veinPos.Y) && map[(int)veinPos.X, (int)veinPos.Y] != 0)
                {
                    map[(int)veinPos.X, (int)veinPos.Y] = tileNum;
                }
            }
        }

        private void RandomFillMap()
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

        private void SmoothMap()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int neighbourWallTiles = GetSurroundingWallCount(x, y);
                    if (neighbourWallTiles > 4)
                        map[x, y] = 1;
                    if (neighbourWallTiles < 4)
                        map[x, y] = 0;


                }
            }
        }

        private int GetSurroundingWallCount(int gridX, int gridY)
        {
            int wallCount = 0;
            for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
            {
                for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
                {
                    if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                    {
                        if (neighbourX != gridX || neighbourY != gridY)
                        {
                            wallCount += map[neighbourX, neighbourY];
                        }
                    }
                    else
                    {
                        wallCount++;
                    }
                }
            }

            return wallCount;
        }

        private bool IsInMapRange(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }
#endregion

        public override void Transition(ref IWorld<T> ingame)
        {
            throw new NotImplementedException();
        }
    }

    public class PostGame<T> : IWorld<T> where T : Player
    {
        Team<T> Winner;

        public PostGame(Team<T> winner, InGame<T> game) : base(GameState.PostGame, game.Creator)
        {
            Winner = winner;
        }

        public override void Transition(ref IWorld<T> lobby)
        {
            throw new NotImplementedException();
        }
    }

    public enum GameState
    {
        Lobby, Game, PostGame
    }
}
