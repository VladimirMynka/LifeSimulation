using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Buildings
{
    public abstract class Building : WorldObject
    {
        protected Building(Cell keeper, Image image, int layer = 0): base(keeper)
        {
            components.Add(new DrawableComponent(this, image, layer));
            components.Add(new InformationComponent(this));
        }
        
        
    }
}