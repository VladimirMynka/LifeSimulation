using System.Drawing;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Buildings
{
    public class House : Building
    {
        private House(
            Cell keeper, 
            Image[] images,
            int layer = 0) 
            : base(keeper, images[0], layer)
        {
            components.Add(new BuildingComponent<Resource>(this, 0, images));
            Start();
        }
    }
}