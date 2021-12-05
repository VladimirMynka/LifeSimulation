using System.Drawing;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Buildings
{
    public abstract class Building<T> : WorldObject where T : Resource
    {
        protected Building(
            Cell keeper, 
            Image[] images,
            int layer = 0,
            int buildingTypeNumber = 0
        )            
            : base(keeper)
        {
            components.Add(new DrawableComponent(this, images[0], layer));
            components.Add(new BuildingComponent<T>(this, buildingTypeNumber, images));
        }
    }
}