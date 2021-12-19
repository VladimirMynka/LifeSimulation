using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.EatableResources;
using LifeSimulation.myCs.Resources.UneatableResources;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Behaviour;
using LifeSimulation.myCs.WorldObjects.Objects.Plants;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles.ExactRoles
{
    public class GardenerComponent : ProfessionalComponent, IHaveTarget
    {
        private SeedKeeperComponent _targetSeedKeeper;
        private IInventory<Resource> _targetWarehouseForPlanting;
        private InventoryComponent<Resource> _inventoryComponent;
        private VisibilityComponent _visibilityComponent;
        private WarehousesOwnerComponent _warehousesOwnerComponent;

        private static readonly SeedResource SampleResource = new SeedResource(10);
        private static readonly PlantResource SamplePlantResource = new PlantResource(10);
        private static readonly WoodResource SampleWoodResource = new WoodResource(10);

        
        public GardenerComponent(WorldObject owner, int period) 
            : base(owner, period)
        {
        }

        public override void Start()
        {
            base.Start();
            _inventoryComponent = GetComponent<InventoryComponent<Resource>>();
            _visibilityComponent = GetComponent<VisibilityComponent>();
            _warehousesOwnerComponent = GetComponent<WarehousesOwnerComponent>();
        }

        public override void Update()
        {
            base.Update();
            if (_targetSeedKeeper == null)
                _targetSeedKeeper = _visibilityComponent.Search<SeedKeeperComponent>(component
                    => component.IsRipe());
            if (!_inventoryComponent.CheckHave(SampleResource))
                return;

            var fromInventory = _inventoryComponent.Remove<SeedResource>();
            
            if (fromInventory.GetCreatureType() == CreatureType.EatableGreenPlant)
                _targetWarehouseForPlanting = _warehousesOwnerComponent
                    .GetNearestWarehouseOfType(SamplePlantResource);
            
            else if (fromInventory.GetCreatureType() == CreatureType.UneatableBrownPlant)
                _targetWarehouseForPlanting = _warehousesOwnerComponent
                    .GetNearestWarehouseOfType(SampleWoodResource);
            
            else 
                return;

            _inventoryComponent.Add(fromInventory);
        }

        protected override void ConfigureBehaviour()
        {
            ConfigureEaterBehaviour(10, 5, 5);
            ConfigureMatingBehaviour(5, 3, 15);
            ConfigurePetsOwnerBehaviour(8, 2, 6, 0);
            ConfigureWarehousesOwnerBehaviour(50, 35);
            
            humanEaterComponent.CollectingTypes.Clear();
            humanEaterComponent.CollectingTypes.Add(MealType.Plant);
            
            WorldObject.RemoveComponent(builderComponent);
            WorldObject.RemoveComponent(instrumentsOwnerComponent);
            builderComponent = null;
            instrumentsOwnerComponent = null;
        }

        public int GetPriorityInBehaviour()
        {
            return 40;
        }

        public WorldObject GetTarget()
        {
            return _inventoryComponent.CheckHave(new SeedResource(10)) 
                ? _targetWarehouseForPlanting.GetWorldObject() 
                : _targetSeedKeeper.WorldObject;
        }

        public override string ToString()
        {
            return base.ToString() + '\n' + "target on " + (
                _inventoryComponent.CheckHave(new SeedResource(10))
                    ? InformationComponent.GetInfoAboutCoords(_targetSeedKeeper)
                    : InformationComponent.GetInfoAboutCoords(_targetWarehouseForPlanting.GetWorldObject()));
        }
    }
}