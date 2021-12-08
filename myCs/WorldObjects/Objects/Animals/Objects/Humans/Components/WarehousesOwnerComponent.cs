using System.Collections.Generic;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components
{
    public class WarehousesOwnerComponent : WorldObjectComponent, IHaveTarget, IHaveInformation
    {
        private List<IInventory<Resource>> _endedBuildings;

        private IInventory<Resource> _targetBuilding;
        public WarehousesOwnerComponent(WorldObject owner) : base(owner)
        {
            _endedBuildings = new List<IInventory<Resource>>();
        }

        public int GetPriorityInBehaviour()
        {
            throw new System.NotImplementedException();
        }

        public WorldObject GetTarget()
        {
            throw new System.NotImplementedException();
        }

        public int GetInformationPriority()
        {
            throw new System.NotImplementedException();
        }
    }
}