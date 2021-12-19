using System.Drawing;
using LifeSimulation.myCs.Drawing;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.EatableResources;
using LifeSimulation.myCs.Resources.UneatableResources;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings.Components;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Buildings
{
    public class Warehouse<T> : Building where T : Resource
    {
        private Warehouse(
            Cell keeper,
            Image[] images,
            int layer = 0,
            int buildingTypeNumber = 0,
            int size = 250
        ) : base(keeper, images[0], layer)
        {
            components.Add(new BuildingComponent<T>(this, buildingTypeNumber, images, size));
            Start();
        }

        public static Building Create<TSpecific>(Cell cell) where TSpecific : Resource
        {
            return new Warehouse<TSpecific>(cell, Pictures.SecondBuilding, 1, 1);
        }

        public static Building Create(Cell cell, Resource resource)
        {
            return resource is EatableResource ? Create<EatableResource>(cell) :
                resource is GoldResource ? Create<GoldResource>(cell) :
                resource is WoodResource ? Create<WoodResource>(cell) :
                resource is IronResource ? Create<IronResource>(cell) :
                resource is CompostResource ? Create<CompostResource>(cell) : null;
        }
    }
}