
using LifeSimulation.myCs.WorldObjects.Plants;

namespace LifeSimulation.myCs.WorldObjects
{
    public abstract class WorldObject
    {
        public readonly int color;
        protected Cell cell;
        protected World world;

        protected WorldObject(Cell keeper, int newColor = 0)
        {
            color = newColor;
            cell = keeper;
            world = keeper.World;

            if (this.GetType() == typeof(Plant)) cell.CurrentObjects[0] = this;
            else cell.CurrentObjects[1] = this;
        }

        public virtual void Update() { }

        protected void SetColor(int newColor)
        {
            cell.SetColor(newColor);
            color = newColor;
        }
    }
}