using System;
using System.Drawing;
using LifeSimulation.myCs.WorldObjects.Animals;
using LifeSimulation.myCs.WorldObjects.Plants;

namespace LifeSimulation.myCs
{
    public class World
    {
        public const int PixelSize = 1;
        private const int AnimalsNormalCount = 2000;
        private const int PlantsNormalCount = 1000;

        private Cell[][] _cells;
        public readonly int Length;
        public readonly int Width;
        public static Random Random = new Random();

        public Graphics Graphics;

        public World(int length, int width, Graphics g)
        {
            Length = length;
            Width = width;
            _cells = new Cell[length][];
            Graphics = g;

            for (int i = 0; i < length; i++)
            {
                _cells[i] = new Cell[width];
                for (int j = 0; j < width; j++)
                {
                    _cells[i][j] = new Cell(this,
                        new[]{i, j}, 
                        Random.Next(2), 
                        false);

                    int rand = Random.Next(length * width);
                    if (rand < AnimalsNormalCount) AddAnimal(i, j);
                    
                    rand = Random.Next(length * width);
                    if (rand < PlantsNormalCount) AddPlant(i, j);
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
            foreach (var cellLine in _cells)
                foreach (var cell in cellLine)
                    cell.Update();
        }

        private Cell GetRandomNotLockedCell()
        {
            var random = new Random();
            int x = random.Next(_cells.Length);
            int y = random.Next(_cells[1].Length);
            bool isLocked = _cells[x][y].CheckLocked();
            int i = 0;

            while (isLocked && i < _cells.Length)
            {
                x = random.Next(_cells.Length);
                y = random.Next(_cells[1].Length);
                isLocked = _cells[x][y].CheckLocked();
                i++;
            }

            return _cells[x][y];
        }

        private void AddAnimal(int x, int y)
        {
            var randColor = Random.Next(Colors.Animal1Const, Colors.Animal3Const + 1);
            new Animal(GetCell(x, y), randColor);
        }

        private void AddPlant(int x, int y)
        {
            var randColor = Random.Next(Colors.Plant1Const, Colors.Plant3Const + 1);
            new Plant(GetCell(x, y), randColor);
        }

    }
}