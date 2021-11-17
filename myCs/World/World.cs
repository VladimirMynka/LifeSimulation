using System;
using LifeSimulation.myCs.WorldObjects.Animals.Animals;
using LifeSimulation.myCs.WorldObjects.Animals.Humans;
using LifeSimulation.myCs.WorldObjects.Plants.Plants;

namespace LifeSimulation.myCs.World
{
    public class World
    {
        private const int AnimalsNormalCount = 10000;
        private const int HumanNormalCount = 2000;
        private const int PlantsNormalCount = 5000;

        private readonly Cell[][] _cells;
        private readonly Drawer.Drawer _drawer;
        public readonly Weather.Weather Weather;
        public readonly int Length;
        public readonly int Width;
        public static Random Random = new Random();

        public World(int length, int width, Drawer.Drawer drawer)
        {
            Length = length;
            Width = width;
            _cells = new Cell[length][];
            _drawer = drawer;
            Weather = new Weather.Weather(drawer);
            

            for (int i = 0; i < length; i++)
            {
                _cells[i] = new Cell[width];
                for (int j = 0; j < width; j++)
                {
                    _cells[i][j] = new Cell(
                        this,
                        drawer,
                        new[]{i, j}
                    );

                    int rand = Random.Next(length * width);
                    if (rand < AnimalsNormalCount) 
                        AddAnimal(i, j);
                    
                    rand = Random.Next(length * width);
                    if (rand < PlantsNormalCount) AddPlant(i, j);
                    
                    rand = Random.Next(length * width);
                    if (rand < HumanNormalCount) AddHuman(i, j);
                }
            }
        }

        public Cell GetCell(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Length || y >= Width) return null;
            return _cells[x][y];
        }

        public void Update()
        {
            Weather.Update();
            foreach (var cellLine in _cells)
                foreach (var cell in cellLine)
                    cell.Update();

            var x = _drawer.GetX();
            var y = _drawer.GetY();
            var length = _drawer.GetLength();
            foreach (var cellLine in _cells)
                foreach (var cell in cellLine)
                    cell.AfterUpdate(_drawer.UpdateAll && 
                                     Direction.CheckInSquare(cell.Coords, x, y, length));
        }

        private void AddAnimal(int x, int y)
        {
            Animal.SpawnRandomAnimal(GetCell(x, y));
        }
        
        private void AddPlant(int x, int y)
        {
            Plant.SpawnRandomPlant(GetCell(x, y));
        }

        private void AddHuman(int x, int y)
        {
            var cell = GetCell(x, y);
            foreach (var worldObject in cell.CurrentObjects)
            {
                if (worldObject is Animal) return;
            }
            Human.SpawnHumanWithRandomGender(cell);
        }

        public static int SmoothRandom(int current, int delta1, int delta2, int min, int max)
        {
            var left = Math.Max(current - delta1, min);
            var right = Math.Min(current + delta2, max);
            return Random.Next(Math.Min(left, right), Math.Max(left, right));
        }
    }
}