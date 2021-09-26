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

        public Cell(World world, int[] coords, int color = 0, bool isLocked = false)
        {
            World = world;
            Coords = coords;
            DefaultColor = color;
            _color = color;
            _isLocked = isLocked;
            CurrentObjects = new WorldObject[2];
            
            World.Graphics.FillRectangle(Colors.GetBrush(color),
                new Rectangle(
                    World.PixelSize * Coords[0], 
                    World.PixelSize * Coords[1],
                    World.PixelSize,
                    World.PixelSize));
        }

        public void Update()
        {
            if (CurrentObjects[0] != null) CurrentObjects[0].Update();
            if (CurrentObjects[1] != null) CurrentObjects[1].Update();
            
        }

        public void SetColor(int color)
        {
            _color = color;
            World.Graphics.FillRectangle(Colors.GetBrush(color),
                new Rectangle(
                    World.PixelSize * Coords[0], 
                    World.PixelSize * Coords[1],
                    World.PixelSize,
                    World.PixelSize));
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