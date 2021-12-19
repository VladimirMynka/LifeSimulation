using System.Drawing;
using LifeSimulation.myCs.Drawing;
using LifeSimulation.myCs.Resources.EatableResources;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings.Components;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Buildings
{
    public class Barn : Building
    {
        private Barn(Cell keeper, Image[] images, int layer = 0) 
            : base(keeper, images[0], layer)
        {
            components.Add(new BuildingComponent<EatableResource>(this, 2, images, 1250));
            Start();
        }

        public static Barn Create(Cell cell)
        {
            return new Barn(cell, Pictures.ThirdBuilding, 1);
        }
    }
}