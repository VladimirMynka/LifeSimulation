using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using LifeSimulation.myCs.WorldObjects;

namespace LifeSimulation.myCs
{
    public class Cell
    {
        public readonly World World;
        public readonly int[] Coords;
        public WorldObject[] CurrentObjects;
        private int _color;
        private bool _isLocked;
        public int DefaultColor;

        private readonly Drawer _drawer;
        public bool evenCycle;
        private bool checkUpdated;

        public Cell(World world, Drawer drawer, int[] coords, int color = 0, bool isLocked = false)
        {
            World = world;
            Coords = coords;
            DefaultColor = color;
            _color = color;
            _isLocked = isLocked;
            CurrentObjects = new WorldObject[2];
            _drawer = drawer;
            evenCycle = false;
            
            drawer.AddCell(new CellDrawer(Coords[0], Coords[1], color));
        }

        public void Update(bool updateInAnyKeys)
        {
            evenCycle = !evenCycle;
            if (CurrentObjects[0] != null && evenCycle == CurrentObjects[0].evenCycle)
            {
                CurrentObjects[0].Update();
            }
            if (CurrentObjects[1] != null && evenCycle == CurrentObjects[1].evenCycle)
            {
                CurrentObjects[1].Update();
            }

            if (updateInAnyKeys || checkUpdated)
            {
                _drawer.AddCell(new CellDrawer(Coords[0], Coords[1], _color));
                checkUpdated = false;
            }
        }

        public void SetColor(int color)
        {
            _color = color;
            checkUpdated = true;
        }
        
        public void ThrowOffColor()
        {
            SetColor(DefaultColor);
        }

        public void Lock()
        {
            _isLocked = true;
        }

        public void Unlock()
        {
            _isLocked = false;
        }

        public bool CheckLocked()
        {
            return _isLocked;
        }

    }
}