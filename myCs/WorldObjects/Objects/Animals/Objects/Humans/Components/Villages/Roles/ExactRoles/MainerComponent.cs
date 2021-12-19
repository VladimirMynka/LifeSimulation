using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles.ExactRoles
{
    public class MainerComponent : ProfessionalComponent
    {
        private InventoryComponent<Resource> _inventoryComponent;

        public MainerComponent(WorldObject owner, CitizenComponent citizenComponent, int period) 
            : base(owner, citizenComponent, period)
        {
        }

        public override void Start()
        {
            base.Start();
            _inventoryComponent = GetComponent<InventoryComponent<Resource>>();
        }

        protected override void ConfigureBehaviour()
        {
            ConfigureEaterBehaviour(20, 10, 0);
            ConfigureMatingBehaviour(5, 3, 15);
            ConfigureInstrumentsOwnerBehaviour(35, 35);
            ConfigurePetsOwnerBehaviour(0, 0, 0, 0);
            ConfigureWarehousesOwnerBehaviour(50, 40);
            ConfigureBuilderBehaviour(0);
            instrumentsOwnerComponent.AlwaysSearch = true;
            instrumentsOwnerComponent.MaxInstrumentsCount = 3 * Defaults.InstrumentsMax;
        }

        public override void Update()
        {
            base.Update();
            if (_inventoryComponent.IsHalfFull())
                AskBuildersWarehouse(_inventoryComponent.GetTheLargestResource());
        }

        public bool SetMission(Resource resource, ProfessionalBuilderComponent requester)
        {
            if (!_inventoryComponent.CheckHave(resource)) 
                return false;
            
            if (warehousesOwnerComponent.SetTakingOrPuttingResource(resource, false)) 
                return true;

            var otherInventory = requester.GetHouseInventory();
            otherInventory = otherInventory == null 
                ? requester.GetInventory() 
                : otherInventory;
                
            return warehousesOwnerComponent
                .SetPuttingResourceAndWarehouse(resource, otherInventory);
        }

        private void AskBuildersWarehouse(Resource resource)
        {
            var builders = citizenComponent.GetPresident()
                .GetRolesOfType(typeof(ProfessionalBuilderComponent));
            foreach (var builder in builders)
            {
                if (((ProfessionalBuilderComponent) builder).SetMission(resource))
                    return;
            }
        }

        protected override void ConfigureWithDefaults()
        {
            base.ConfigureWithDefaults();
            instrumentsOwnerComponent.AlwaysSearch = false;
            instrumentsOwnerComponent.MaxInstrumentsCount = Defaults.InstrumentsMax;

        }
    }
}