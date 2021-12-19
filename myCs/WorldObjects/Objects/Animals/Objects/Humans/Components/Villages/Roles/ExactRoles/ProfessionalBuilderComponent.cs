using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles.ExactRoles
{
    public class ProfessionalBuilderComponent : ProfessionalComponent
    {
        private InventoryComponent<Resource> _inventoryComponent;
        public ProfessionalBuilderComponent(WorldObject owner, int period) 
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
            ConfigureInstrumentsOwnerBehaviour(25, 0);
            ConfigurePetsOwnerBehaviour(0, 0, 0, 0);
            ConfigureWarehousesOwnerBehaviour(50, 45);
            ConfigureBuilderBehaviour(40);
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