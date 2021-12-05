using System.Drawing;
using LifeSimulation.myCs.Resources;
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