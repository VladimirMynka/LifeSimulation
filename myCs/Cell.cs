using System.Drawing;
using LifeSimulation.myCs.WorldObjects;

namespace LifeSimulation.myCs
{
    public class Cell
    {
        public World World;
        public readonly int[] Coords;
        public WorldObject[] CurrentObjects;
        private int _color;
        private bool _isLocked;
        public int DefaultColor;
        private Graphics _graphics;

        public Cell(World world, Graphics graphics, int[] coords, int color = 0, bool isLocked = false)
        {
            World = world;
            Coords = coords;
            DefaultColor = color;
            _color = color;
            _isLocked = isLocked;
            CurrentObjects = new WorldObject[2];
            _graphics = graphics;
            
            _graphics.FillRectangle(Colors.GetBrush(color),
                new Rectangle(
                    World.PixelSize * Coords[0], 
                    World.PixelSize * Coords[1],
                    World.PixelSize,
                    World.PixelSize));
        }

        public void Update(Graphics g)
        {
            _graphics = g;
            if (CurrentObjects[0] != null) CurrentObjects[0].Update();
            if (CurrentObjects[1] != null) CurrentObjects[1].Update();
        }

        public void SetColor(int color)
        {
            _color = color;
            _graphics.FillRectangle(Colors.GetBrush(color),
                new Rectangle(
                    World.PixelSize * Coords[0], 
                    World.PixelSize * Coords[1],
                    World.PixelSize,
                    World.PixelSize));
        }
        
        public void ThrowOffColor()
        {
            _color = DefaultColor;
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