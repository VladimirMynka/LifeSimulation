using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Behaviour;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings.Components;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages
{
    public class BuilderComponent : WorldObjectComponent, IHaveInformation, IHaveTarget
    {
        private IBuilding<Resource> _targetBuilding;
        private InventoryComponent<Resource> _inventory;
        private WarehousesOwnerComponent _warehousesOwnerComponent;
        private InstrumentsOwnerComponent _instrumentsOwnerComponent;
        private VisibilityComponent _visibilityComponent;

        public BuilderComponent(WorldObject owner) : base(owner)
        {
        }

        public override void Start()
        {
            base.Start();
            _inventory = GetComponent<InventoryComponent<Resource>>();
            _warehousesOwnerComponent = GetComponent<WarehousesOwnerComponent>();
            _instrumentsOwnerComponent = GetComponent<InstrumentsOwnerComponent>();
            _visibilityComponent = GetComponent<VisibilityComponent>();
        }

        public override void Update()
        {
            base.Update();
            if (_targetBuilding != null && CheckWereDestroyed(_targetBuilding.GetWorldObject()))
                _targetBuilding = null;

            if (_targetBuilding != null)
                TryToBuild();

            else if (_inventory.IsHalfFull())
            {
                var largestResource = _inventory.GetTheLargestResource();
                if (!_warehousesOwnerComponent.SetTakingOrPuttingResource(largestResource, false))
                    StartBuildFor(_inventory.GetTheLargestResource());
            }
        }

        public void StartBuildFor(Resource resource)
        {
            if (_targetBuilding != null)
                return;
            if (WorldObject.Cell.Contains<Building>())
                return;
            var building = Warehouse<Resource>.Create(WorldObject.Cell, resource);
            _targetBuilding = building.GetComponent<IBuilding<Resource>>();
            TryToBuild();
        }

        public void StartBuildHouse()
        {
            if (_targetBuilding != null)
                return;

            var citizenComponent = new CitizenComponent(WorldObject);

            var parentHouseComponent = _visibilityComponent.Search<BuildingComponent<Resource>>(building1 =>
                building1.GetWorldObject() is House);
            var parentHouse = parentHouseComponent == null
                ? null
                : parentHouseComponent.GetWorldObject() as House;
            
            var cell = parentHouse == null ? WorldObject.Cell : parentHouse.Cell;
            cell = cell.GetNearestWithCheck(cell1 => !cell1.Contains<Building>());
            if (cell == null)
                return;

            WorldObject.AddComponent(citizenComponent);

            var village = parentHouse == null
                ? new Village(citizenComponent)
                : parentHouseComponent.Village;
            
            citizenComponent.Village = village;

            var building = House.Create(cell).GetComponent<BuildingComponent<Resource>>();
            building.Village = village;
            _targetBuilding = building;
            TryToBuild();
        }

        private void TryToBuild()
        {
            if (_targetBuilding == null || !OnOneCellWith(_targetBuilding.GetWorldObject()))
                return;

            var resource = _targetBuilding.GetNeedSource(_inventory);
            if (resource != null
                && !_warehousesOwnerComponent.SetTakingOrPuttingResource(resource, true))
            {
                _instrumentsOwnerComponent.SearchResource(resource);
                return;
            }

            var resultInventory = _targetBuilding.TryBuildNextStage(_inventory);

            if (resultInventory == null)
                return;
            _targetBuilding = null;
            _warehousesOwnerComponent.AddWarehouse(resultInventory);
            if (resultInventory.GetWorldObject() is House)
                _warehousesOwnerComponent.House = resultInventory.GetWorldObject() as House;
        }

        public int GetInformationPriority()
        {
            return Defaults.InfoPriorityBuilder;
        }

        public override string ToString()
        {
            return "Build: " + (_targetBuilding == null
                ? "none"
                : _targetBuilding.KeepResourceOfType().Name + " on " +
                  InformationComponent.GetInfoAboutCoords(_targetBuilding.GetWorldObject()));
        }

        public int BuildingProcessPriority = Defaults.BehaviourBuilder;

        public int GetPriorityInBehaviour()
        {
            return _targetBuilding == null || _targetBuilding.GetNeedSource(_inventory) != null
                ? 0
                : BuildingProcessPriority;
        }

        public WorldObject GetTarget()
        {
            if (_targetBuilding == null || CheckWereDestroyed(_targetBuilding.GetWorldObject()))
                return null;
            return _targetBuilding.GetWorldObject();
        }
    }
}