using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.WorldGeneration
{
    public interface IWorldFactory
    {
        IWorldFactory Seed(int seed);
        IWorldFactory GridSize(int width, int height);
        IWorldFactory CreateTiles(int id, int chance);
        List<IWorldTile> Build();
    }

    public class WorldFactory
    {
        public enum WorldFactoryGeneratorType
        {
            Basic
        }
        public static IWorldFactory Get(WorldFactoryGeneratorType generatorType)
        {
            switch (generatorType)
            {
                case WorldFactoryGeneratorType.Basic:
                    return new BasicWorldFactory();
            }
            return null;
        }
    }

    internal class BasicWorldFactory : IWorldFactory
    {
        private class Tiles
        {
            public int Chance { get; set; }
            public int ID { get; set; }
        }

        private List<Tiles> tiles = new List<Tiles>();
        private int MaxChance;

        private Random _RNG = new Random();
        private int _Width = 10;
        private int _Height = 10;

        public IWorldFactory GridSize(int width, int height)
        {
            _Width = width;
            _Height = height;
            return this;
        }

        public IWorldFactory CreateTiles(int id, int chance)
        {
            tiles.Add(new Tiles()
            {
                ID = id,
                Chance = chance
            });
            return this;
        }

        private int GetNextID()
        {
            var roll = _RNG.Next(0, MaxChance);
            var originalRoll = roll;
            foreach (var tileTemplate in tiles)
            {
                roll -= tileTemplate.Chance;
                if (roll < 0) return tileTemplate.ID;
            }
            throw new Exception($"Invalid roll '{originalRoll}'. Maximum chance = '{MaxChance}'. Actual Maximum Chance = '{tiles.Sum(tt => tt.Chance)}'");
        }

        public IWorldFactory Seed(int seed)
        {
            _RNG = new Random(seed);
            return this;
        }

        public List<IWorldTile> Build()
        {
            MaxChance = tiles.Sum(tt => tt.Chance);
            List<IWorldTile> world = new List<IWorldTile>();
            for (int x = 0; x < _Width; x++)
            {
                for (int y = 0; y < _Height; y++)
                {
                    var tile = new WorldTile()
                    {
                        X = x,
                        Y = y,
                        TypeId = GetNextID()
                    };

                    world.Add(tile);
                }
            }
            return world;
        }
    }
}