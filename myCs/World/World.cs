﻿using System;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Animals;
using LifeSimulation.myCs.WorldObjects.Plants;

namespace LifeSimulation.myCs.World
{
    public class World
    {
        private const int AnimalsNormalCount = 2000;
        private const int PlantsNormalCount = 500;

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

        public void Update(bool updateAll)
        {
            foreach (var cellLine in _cells)
            foreach (var cell in cellLine)
                cell.Update(updateAll);
        }

        private void AddAnimal(int x, int y)
        {
            AnimalsSpawner.SpawnNormalAnimal(GetCell(x, y));
        }

        private void AddPlant(int x, int y)
        {
            AddPlant(GetCell(x, y));
        }
        
        public void AddPlant(Cell cell)
        {
            switch (Random.Next(3))
            {
                case 0:
                    PlantsSpawner.SpawnNormalPlant(cell);
                    return;
                case 1:
                    PlantsSpawner.SpawnPoisonousPlant(cell);
                    return;
                case 2:
                    PlantsSpawner.SpawnUneatablePlant(cell);
                    return;
                default:
                    return;
            }
        }

    }
}