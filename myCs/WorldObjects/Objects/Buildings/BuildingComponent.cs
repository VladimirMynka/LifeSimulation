using System.Collections.Generic;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components;

namespace LifeSimulation.myCs.WorldObjects.Objects.Buildings
{
    public class BuildingComponent<T> : WorldObjectComponent where T : Resource
    {
        private List<WarehousesOwnerComponent> _owners;
        public BuildingComponent(WorldObject owner) : base(owner)
        {
            _owners = new List<WarehousesOwnerComponent>();
        }
    }
}