
using LifeSimulation.myCs.WorldObjects.Plants;

namespace LifeSimulation.myCs.WorldObjects
{
    public abstract class WorldObject
    {
        protected int color;
        protected Cell cell;
        protected World world;

        public bool evenCycle;

        protected WorldObject(Cell keeper, int newColor = 0)
        {
            color = newColor;
            cell = keeper;
            world = keeper.World;
            evenCycle = false;

            if (this.GetType() == typeof(Plant)) cell.CurrentObjects[0] = this;
            else cell.CurrentObjects[1] = this;
        }

        public virtual void Update()
        {
            evenCycle = !evenCycle;
        }

        public int GetColor()
        {
            return color;
        }
    }
}