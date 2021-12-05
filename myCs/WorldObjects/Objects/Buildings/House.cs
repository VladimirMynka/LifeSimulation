using System.Drawing;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.Objects.Plants.Plants;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Buildings
{
    public class House : Building<Resource>
    {
        private House(
            Cell keeper, 
            Image[] images,
            int layer = 0) 
            : base(keeper, images, layer)
        {
            Start();
        }
    }
}