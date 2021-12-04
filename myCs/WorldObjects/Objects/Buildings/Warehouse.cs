using System.Drawing;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Buildings
{
    public class Warehouse<T> : Building<T> where T : Resource
    {
        private Warehouse(
            Cell keeper,
            Image[] images,
            int layer = 0
        ) : base(keeper, images, layer)
        {}
    }
}