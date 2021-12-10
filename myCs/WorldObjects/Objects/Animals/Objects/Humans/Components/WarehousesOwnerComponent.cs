using System.Collections.Generic;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.EatableResources;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Behaviour;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components
{
    public class WarehousesOwnerComponent : WorldObjectComponent, IHaveTarget, IHaveInformation
    {
        private readonly List<IInventory<Resource>> _warehouses;
        private InventoryComponent<Resource> _ownerInventory;

        private IInventory<Resource> _targetWarehouse;

        private Resource _neededResource;
        private bool _takeMod;

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
            TryToTakeOrPutResource();
        }

        private void TryToTakeOrPutResource()
        {
            if (_neededResource == null || !CheckWarehouseHere())
                return;

            if (_takeMod)
                TakeResource();
            else
                PutResource();

            _neededResource = null;
            _targetWarehouse = null;
        }

        private void TakeResource()
        {
            _neededResource.Set(_targetWarehouse.Remove(_neededResource));
            _ownerInventory.Add(_neededResource);
        }

        private void PutResource()
        {
            _neededResource.Set(_ownerInventory.Remove(_neededResource));
            _targetWarehouse.Add(_neededResource);
        }

        private IInventory<Resource> GetNearestWarehouseForTake<T>(T resource) where T : Resource
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
        
        private IInventory<Resource> GetNearestWarehouseForPut<T>(T resource) where T : Resource
        {
            IInventory<Resource> goodVariant = null;

            var sqrDistance = -1;
            foreach (var warehouse in _warehouses)
            {
                if (!warehouse.CanKeep(resource) || warehouse.IsFilled())
                    continue;
                var currentDistance = GetSqrLengthWith(warehouse.GetWorldObject());
                if (sqrDistance != -1 && currentDistance >= sqrDistance)
                    continue;
                goodVariant = warehouse;
                sqrDistance = currentDistance;
            }

            return goodVariant;
        }

        public bool SetTakingOrPuttingResource<T>(T resource, bool takeMod) where T : Resource
        {
            if (_targetWarehouse != null && _neededResource != null)
                return true;
            var warehouse = takeMod
                ? GetNearestWarehouseForTake(resource)
                : GetNearestWarehouseForPut(resource);
            if (warehouse == null)
                return false;
            _neededResource = resource;
            _targetWarehouse = warehouse;
            _takeMod = takeMod;
            return true;
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