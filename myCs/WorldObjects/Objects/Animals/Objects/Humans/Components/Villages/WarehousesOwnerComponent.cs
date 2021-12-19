using System.Collections.Generic;
using System.Linq;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.EatableResources;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Behaviour;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages
{
    public class WarehousesOwnerComponent : WorldObjectComponent, IHaveTarget, IHaveInformation
    {
        private List<IInventory<Resource>> _warehouses;
        public IInventory<Resource> House;
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

        public delegate bool CheckWarehouse<in T>(IInventory<Resource> warehouse, T resource);
        
        private IInventory<Resource> GetNearestWarehouse<T>(
            T resource, 
            CheckWarehouse<T> checker
        ) where T : Resource
        {
            IInventory<Resource> goodVariant = null;

            var sqrDistance = -1;
            int currentDistance;
            foreach (var warehouse in _warehouses)
            {
                if (!checker(warehouse, resource))
                    continue;
                currentDistance = GetSqrLengthWith(warehouse.GetWorldObject());
                if (sqrDistance != -1 && currentDistance >= sqrDistance)
                    continue;
                goodVariant = warehouse;
                sqrDistance = currentDistance;
            }

            if (House == null)
                return goodVariant;
            if (!checker(House, resource))
                return goodVariant;
            currentDistance = GetSqrLengthWith(House.GetWorldObject());
            if (sqrDistance != -1 && currentDistance >= sqrDistance)
                return goodVariant;

            return House;
        }

        private IInventory<Resource> GetNearestWarehouseForTake<T>(T resource) where T : Resource
        {
            return GetNearestWarehouse(resource,
                (warehouse, res) => warehouse.HasMoreThanNothing(res));
        }

        private IInventory<Resource> GetNearestWarehouseForPut<T>(T resource) where T : Resource
        {
            return GetNearestWarehouse(resource,
                (warehouse, res) => 
                    warehouse.CanKeep(res) && !warehouse.IsFilled());
        }

        public bool SetTakingOrPuttingResource<T>(T resource, bool takeMod) where T : Resource
        {
            if (_neededResource == resource && _takeMod == takeMod)
                return true;
            if (_targetWarehouse != null && _neededResource != null)
                return false;
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
            if (warehouse.GetWorldObject() is House)
                House = warehouse;
            else
                _warehouses.Add(warehouse);
        }

        public int TakeEatPriority = Defaults.BehaviourWarehouseTakeMeal;
        public int PutOrTakeResourcePriority = Defaults.BehaviourWarehouseTakeOrPut;

        public int GetPriorityInBehaviour()
        {
            return _neededResource == null ? Defaults.BehaviourHaveNotPriority
                : _neededResource is EatableResource && _takeMod ? TakeEatPriority
                : PutOrTakeResourcePriority;
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
            return Defaults.InfoPriorityWarehouseOwner;
        }

        public override string ToString()
        {
            var info = _warehouses
                .Aggregate("Warehouses:",
                    (current, warehouse) =>
                        current + ('\n' + warehouse.GetResourceType().Name
                                        + " on " + InformationComponent
                                            .GetInfoAboutCoords(warehouse.GetWorldObject())));
            info += "\n\nHouse: " + (House == null ? "none" : "on " + InformationComponent.GetInfoAboutCoords(House.GetWorldObject()));

            if (_targetWarehouse != null && _neededResource != null)
            {
                info += "\n\nWant " + (_takeMod ? "take " : "put ") +
                        _neededResource.ToString() + (_takeMod ? "from " : "to ") +
                        InformationComponent.GetInfoAboutCoords(_targetWarehouse.GetWorldObject());
            }

            return info;
        }

        public void SetWarehouses(List<IInventory<Resource>> warehouses)
        {
            _warehouses = warehouses;
        }

        public List<IInventory<Resource>> GetWarehouses()
        {
            return _warehouses;
        }
    }
}