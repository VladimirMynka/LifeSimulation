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
            bool checkSmth = false;
            evenCycle = !evenCycle;
            if (CurrentObjects[0] != null && evenCycle == CurrentObjects[0].evenCycle)
            {
                CurrentObjects[0].Update();
                checkSmth = true;
            }
            if (CurrentObjects[1] != null && evenCycle == CurrentObjects[1].evenCycle)
            {
                CurrentObjects[1].Update();
                checkSmth = true;
            }
            if (updateInAnyKeys || checkSmth) 
                _drawer.AddCell(new CellDrawer(Coords[0], Coords[1], _color));
        }

        public void SetColor(int color)
        {
            _color = color;
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