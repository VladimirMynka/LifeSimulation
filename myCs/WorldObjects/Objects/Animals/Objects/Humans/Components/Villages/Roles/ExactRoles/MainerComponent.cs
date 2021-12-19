using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles.ExactRoles
{
    public class MainerComponent : ProfessionalComponent
    {
        private InventoryComponent<Resource> _inventoryComponent;

        public MainerComponent(WorldObject owner, int period) 
            : base(owner, period)
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

        public override void OnDestroy()
        {
            base.OnDestroy();
            instrumentsOwnerComponent.AlwaysSearch = false;
            instrumentsOwnerComponent.MaxInstrumentsCount = Defaults.InstrumentsMax;
        }
    }
}