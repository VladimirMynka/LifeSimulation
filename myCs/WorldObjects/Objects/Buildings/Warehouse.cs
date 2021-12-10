using System.Drawing;
using LifeSimulation.myCs.Drawing;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Buildings
{
    public class Warehouse<T> : Building where T : Resource
    {
        private Warehouse(
            Cell keeper,
            Image[] images,
            int layer = 0,
            int buildingTypeNumber = 0
        ) : base(keeper, images[0], layer)
        {
            components.Add(new BuildingComponent<T>(this, buildingTypeNumber, images));
            Start();
        }

        public Warehouse<TSpecific> Create<TSpecific>(Cell cell, int type) where TSpecific : T
        {
            switch (type)
            {
                case 0:
                    return new Warehouse<TSpecific>(cell, Pictures.FirstBuilding, 1, type);
                case 1:
                    return new Warehouse<TSpecific>(cell, Pictures.SecondBuilding, 1, type);
            }

            return null;
        }
    }
}