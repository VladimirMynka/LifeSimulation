namespace LifeSimulation.myCs
{
    public class CellDrawer
    {
        public readonly int PositionX;
        public readonly int PositionY;
        public readonly int Color;

        public CellDrawer(int x, int y, int color)
        {
            PositionX = x;
            PositionY = y;
            Color = color;
        }
    }
}