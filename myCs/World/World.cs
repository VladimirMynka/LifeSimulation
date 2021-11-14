using System;
using LifeSimulation.myCs.WorldObjects.Animals;
using LifeSimulation.myCs.WorldObjects.Animals.Animals;
using LifeSimulation.myCs.WorldObjects.Plants.Fruits;
using LifeSimulation.myCs.WorldObjects.Plants.Plants;

namespace LifeSimulation.myCs.World
{
    public class World
    {
        private const int AnimalsNormalCount = 10000;
        private const int HumanNormalCount = 2000;
        private const int PlantsNormalCount = 5000;

        private readonly Cell[][] _cells;
        public readonly int Length;
        public readonly int Width;
        public static Random Random = new Random();

        public World(int length, int width, Drawer.Drawer drawer)
        {
            Length = length;
            Width = width;
            _cells = new Cell[length][];

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

        public void Update(bool updateAll = false)
        {
            foreach (var cellLine in _cells)
                foreach (var cell in cellLine)
                    cell.Update();
            
            foreach (var cellLine in _cells)
                foreach (var cell in cellLine)
                    cell.AfterUpdate(updateAll);
        }

        private void AddAnimal(int x, int y)
        {
            AnimalsSpawner.SpawnRandomAnimal(GetCell(x, y));
        }
        
        private void AddPlant(int x, int y)
        {
            PlantsSpawner.SpawnRandomPlant(GetCell(x, y));
        }

        private void AddHuman(int x, int y)
        {
            var cell = GetCell(x, y);
            foreach (var worldObject in cell.CurrentObjects)
            {
                if (worldObject is Animal) return;
            }
            AnimalsSpawner.SpawnHumanWithRandomGender(cell);
        }

    }
}