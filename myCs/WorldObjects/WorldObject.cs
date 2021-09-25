
namespace LifeSimulation.myCs.WorldObjects
{
    public abstract class WorldObject
    {
        protected int color;
        protected Cell cell;
        protected World world;

        protected WorldObject(Cell keeper, int newColor = 0)
        {
            color = newColor;
            cell = keeper;
            world = keeper.World;
        }

        public virtual void Update()
        {
            
        }

        protected virtual void SetColor(int newColor)
        {
            cell.SetColor(newColor);
            color = newColor;
        }
    }
}