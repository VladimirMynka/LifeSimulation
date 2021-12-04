using System.Drawing;
using LifeSimulation.myCs.Drawing;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.Objects.Plants.Plants;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Buildings
{
    public abstract class Building<T> : WorldObject where T : Resource
    {
        protected Building(
            Cell keeper, 
            Image[] images,
            int layer = 0
            )            
            : base(keeper)
        {
            components.Add(new DrawableComponent(this, images[0], layer));
            components.Add(new BuildingComponent<T>(this));
            Start();
        }
    }
}