using System.Collections.Generic;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.EatableResources;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Behaviour;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components
{
    public class WarehousesOwnerComponent : WorldObjectComponent, IHaveTarget, IHaveInformation
    {
        private readonly List<IInventory<Resource>> _warehouses;
        private InventoryComponent<Resource> _ownerInventory;

        private IInventory<Resource> _targetWarehouse;

        private Resource _neededResource;

        public WarehousesOwnerComponent(WorldObject owner) : base(owner)
        {
            _warehouses = new List<IInventory<Resource>>();
        }

        public override void Start()
        {
            base.Start();
            _ownerInventory = GetComponent<InventoryComponent<Resource>>();
        }

        public override void Update()
        {
            base.Update();
            TryToTakeResource();
        }

        private void TryToTakeResource()
        {
            if (_neededResource != null && CheckWarehouseHere())
            {
                _neededResource.Set(_targetWarehouse.Remove(_neededResource));
                _ownerInventory.Add(_neededResource);
                _neededResource = null;
            }
        }

        private IInventory<Resource> GetNearestWarehouseFor<T>(T resource) where T : Resource
        {
            IInventory<Resource> goodVariant = null;
            var sqrDistance = -1;
            foreach (var warehouse in _warehouses)
            {
                if (!warehouse.HasMoreThanNothing(resource))
                    continue;
                var currentDistance = GetSqrLengthWith(warehouse.GetWorldObject());
                if (sqrDistance != -1 && currentDistance >= sqrDistance) 
                    continue;
                goodVariant = warehouse;
                sqrDistance = currentDistance;
            }

            return goodVariant;
        }
        
        public void SetTarget<T>(T needed) where T : Resource
        {
            var warehouse = GetNearestWarehouseFor(needed);
            if (warehouse == null)
                return;
            _neededResource = needed;
            _targetWarehouse = warehouse;
        }

        public void AddWarehouse(IInventory<Resource> warehouse)
        {
            _warehouses.Add(warehouse);
        } 

        public int GetPriorityInBehaviour()
        {
            return _neededResource == null ? -1
                : _neededResource is EatableResource ? -2
                : -3;
        }

        public WorldObject GetTarget()
        {
            return _targetWarehouse.GetWorldObject();
        }

        private bool CheckWarehouseHere()
        {
            return _targetWarehouse != null 
                   && OnOneCellWith(_targetWarehouse.GetWorldObject());
        }

        public int GetInformationPriority()
        {
            return 10000;
        }
    }
}