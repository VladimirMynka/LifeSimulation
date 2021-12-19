using System.Linq;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.EatableResources;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings.Components;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles.ExactRoles
{
    public class ProfessionalBuilderComponent : ProfessionalComponent
    {
        private InventoryComponent<Resource> _inventoryComponent;
        private VisibilityComponent _visibilityComponent;
        
        public ProfessionalBuilderComponent(WorldObject owner, CitizenComponent citizenComponent, int period) 
            : base(owner, citizenComponent, period)
        {
        }

        public override void Start()
        {
            base.Start();
            _inventoryComponent = GetComponent<InventoryComponent<Resource>>();
            _visibilityComponent = GetComponent<VisibilityComponent>();
        }

        public override void Update()
        {
            base.Update();
            if (builderComponent.TargetBuilding == null)
            {
                if (!(warehousesOwnerComponent.GetWarehouses().First().GetWorldObject() is Barn))
                {
                    builderComponent.TargetBuilding = _visibilityComponent
                        .Search<BuildingComponent<EatableResource>>(building => 
                            building.WorldObject is Barn 
                            && building.Village == citizenComponent.Village
                            && !building.IsEnded());
                    if (builderComponent.TargetBuilding == null)
                        builderComponent.StartBuildBarn();
                }

                builderComponent.TargetBuilding = _visibilityComponent
                    .Search<IBuilding<Resource>>(building => 
                        building.GetVillage() == citizenComponent.Village
                        && !building.IsEnded());
            }
            if (builderComponent.NeededResource != null)
                AskMainersResource(builderComponent.NeededResource);
        }

        private void AskMainersResource(Resource resource)
        {
            var mainers = citizenComponent.GetPresident()
                .GetRolesOfType(typeof(MainerComponent));
            foreach (var mainer in mainers)
            {
                if (((MainerComponent) mainer).SetMission(resource, this))
                    return;
            }
        }

        public bool SetMission(Resource resource)
        {
            if (builderComponent == null || builderComponent.TargetBuilding != null) 
                return false;
            
            builderComponent.TargetBuilding = _visibilityComponent
                .Search<IBuilding<Resource>>(building =>
                    building.KeepResourceOfType().IsInstanceOfType(resource)
                    && !building.IsEnded());
            
            if (builderComponent.TargetBuilding == null)
                builderComponent.StartBuildFor(resource);
            return true;

        }

        protected override void ConfigureBehaviour()
        {
            ConfigureEaterBehaviour(20, 10, 0);
            ConfigureMatingBehaviour(5, 3, 15);
            ConfigureInstrumentsOwnerBehaviour(25, 0);
            ConfigurePetsOwnerBehaviour(0, 0, 0, 0);
            ConfigureWarehousesOwnerBehaviour(70, 45);
            ConfigureBuilderBehaviour(60);
        }

        public IInventory<Resource> GetHouseInventory()
        {
            return warehousesOwnerComponent.House == null 
                ? null 
                : warehousesOwnerComponent.House;
        }

        public IInventory<Resource> GetInventory()
        {
            return _inventoryComponent;
        }
    }
}