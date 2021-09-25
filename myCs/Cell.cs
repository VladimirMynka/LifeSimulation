using LifeSimulation.myCs.WorldObjects;

namespace LifeSimulation.myCs
{
    public class Cell
    {
        public World World;
        public WorldObject[] CurrentObjects;
        private int _color;
        private bool _isLocked;
        public int DefaultColor;

        public Cell(World world, int color = 0, bool isLocked = false)
        {
            World = world;
            DefaultColor = color;
            _color = color;
            _isLocked = isLocked;
            CurrentObjects = new WorldObject[2];
                aaaaaaaaaa
        }

        public void Update()
        {
            if (CurrentObjects[0] != null) CurrentObjects[0].Update();
            if (CurrentObjects[1] != null) CurrentObjects[1].Update();
        }

        public void SetColor(int color)
        {
            _color = color;
                aaaaaaaaaaa
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