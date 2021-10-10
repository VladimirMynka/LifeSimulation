﻿using System;
using LifeSimulation.myCs.WorldObjects.Animals;
using LifeSimulation.myCs.WorldObjects.Plants;

namespace LifeSimulation.myCs
{
    public class World
    {
        private const int AnimalsNormalCount = 20000;
        private const int PlantsNormalCount = 1000;

        private Cell[][] _cells;
        public readonly int Length;
        public readonly int Width;
        public static Random Random = new Random();

        private Drawer _drawer;

        public World(int length, int width, Drawer drawer)
        {
            Length = length;
            Width = width;
            _cells = new Cell[length][];
            _drawer = drawer;

            for (int i = 0; i < length; i++)
            {
                _cells[i] = new Cell[width];
                for (int j = 0; j < width; j++)
                {
                    _cells[i][j] = new Cell(this,
                        drawer,
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

        public void Update(bool updateAll)
        {
            foreach (var cellLine in _cells)
                foreach (var cell in cellLine)
                    cell.Update(updateAll);
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
            AddPlant(GetCell(x, y));
        }
        
        public void AddPlant(Cell cell)
        {
            var color = Colors.Plant1Const;
            var isEatable = true;
            var effect = Effect.None;
            
            if (Random.Next(0, 2) == 1)
            {
                isEatable = false;
                color = Colors.Tree1Const;
            }
            else if (Random.Next(0, 2) == 1)
            {
                effect = Effect.Heart;
                color = Colors.Poisonous1Const;
            }
            
            new Plant(cell, color, isEatable, effect);
        }

    }
}