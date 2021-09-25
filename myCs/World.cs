using System;

namespace LifeSimulation.myCs
{
    public class World
    {
        private Cell[][] _cells;
        public static Random Random = new Random();

        public World()
        {
            
        }

        public Cell GetCell(int x, int y)
        {
            return _cells[x][y];
        }

        private void Update()
        {
            for (int i = 0; i < _cells.Length; i++)
                for (int j = 0; j < _cells[i].Length; j++) 
                    _cells[i][j].Update();
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
                int x = random.Next(_cells.Length);
                int y = random.Next(_cells[1].Length);
                bool isLocked = _cells[x][y].CheckLocked();
                i++;
            }

            return _cells[x][y];
        }

    }
}