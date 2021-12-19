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
        public IBuilding<Resource> TargetBuilding;
        private InventoryComponent<Resource> _inventory;
        private WarehousesOwnerComponent _warehousesOwnerComponent;
        private InstrumentsOwnerComponent _instrumentsOwnerComponent;
        private VisibilityComponent _visibilityComponent;
        private CitizenComponent _citizenComponent;

        public Resource NeededResource;

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
            if (TargetBuilding != null && CheckWereDestroyed(TargetBuilding.GetWorldObject()))
                TargetBuilding = null;

            if (TargetBuilding != null)
                TryToBuild();

            else if (_inventory.IsHalfFull() 
                     && (_citizenComponent == null || !_citizenComponent.IsActive()))
            {
                var largestResource = _inventory.GetTheLargestResource();
                if (!_warehousesOwnerComponent.SetTakingOrPuttingResource(largestResource, false))
                    StartBuildFor(_inventory.GetTheLargestResource());
            }
        }

        public void StartBuildFor(Resource resource)
        {
            if (TargetBuilding != null)
                return;
            if (WorldObject.Cell.Contains<Building>())
                return;
            var building = Warehouse<Resource>.Create(WorldObject.Cell, resource);
            TargetBuilding = building.GetComponent<IBuilding<Resource>>();
            if (_citizenComponent != null)
                TargetBuilding.SetVillage(_citizenComponent.Village);
            TryToBuild();
        }
        
        public void StartBuildBarn()
        {
            if (TargetBuilding != null && TargetBuilding.GetWorldObject() is Barn)
                return;
            var cell = WorldObject.Cell.GetNearestWithCheck(cell1 => !cell1.Contains<Building>());
            if (WorldObject.Cell.Contains<Building>())
                return;
            var building = Barn.Create(cell);
            TargetBuilding = building.GetComponent<IBuilding<Resource>>();
            TargetBuilding.SetVillage(_citizenComponent.Village);
            TryToBuild();
        }

        public void StartBuildHouse()
        {
            if (TargetBuilding != null)
                return;
            
            var parentHouseComponent = _visibilityComponent.Search<BuildingComponent<Resource>>(building1 =>
                building1.GetWorldObject() is House);
            var parentHouse = parentHouseComponent == null
                ? null
                : parentHouseComponent.GetWorldObject() as House;
            
            var cell = parentHouse == null ? WorldObject.Cell : parentHouse.Cell;
            cell = cell.GetNearestWithCheck(cell1 => !cell1.Contains<Building>());
            if (cell == null)
                return;
            
            var village = parentHouse == null
                ? new Village()
                : parentHouseComponent.Village;
            
            var citizenComponent = new CitizenComponent(WorldObject, village);
            WorldObject.AddComponent(citizenComponent);
            _citizenComponent = citizenComponent;
            
            var building = House.Create(cell).GetComponent<BuildingComponent<Resource>>();
            building.Village = village;
            TargetBuilding = building;
            village.AddNewBuilding(building);
            
            TryToBuild();
        }

        private void TryToBuild()
        {
            if (TargetBuilding == null || !OnOneCellWith(TargetBuilding.GetWorldObject()))
                return;

            var resource = TargetBuilding.GetNeedSource(_inventory);
            if (resource != null
                && !_warehousesOwnerComponent.SetTakingOrPuttingResource(resource, true))
            {
                if (_citizenComponent == null)
                    _instrumentsOwnerComponent.SearchResource(resource);
                else
                    NeededResource = resource;
                return;
            }

            NeededResource = null;

            var resultInventory = TargetBuilding.TryBuildNextStage(_inventory);

            if (resultInventory == null)
                return;
            TargetBuilding = null;
            _warehousesOwnerComponent.AddWarehouse(resultInventory);
        }

        public int GetInformationPriority()
        {
            return Defaults.InfoPriorityBuilder;
        }

        public override string ToString()
        {
            return "Build: " + (TargetBuilding == null
                ? "none"
                : TargetBuilding.KeepResourceOfType().Name + " on " +
                  InformationComponent.GetInfoAboutCoords(TargetBuilding.GetWorldObject()));
        }

        public int BuildingProcessPriority = Defaults.BehaviourBuilder;

        public int GetPriorityInBehaviour()
        {
            return TargetBuilding == null || TargetBuilding.GetNeedSource(_inventory) != null
                ? 0
                : BuildingProcessPriority;
        }

        public WorldObject GetTarget()
        {
            if (TargetBuilding == null || CheckWereDestroyed(TargetBuilding.GetWorldObject()))
                return null;
            return TargetBuilding.GetWorldObject();
        }
    }
}