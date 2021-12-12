using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.EatableResources;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Mating
{
    public class WomanComponent : FemaleComponent
    {
        private InventoryComponent<Resource> _inventory;
        private WarehousesOwnerComponent _warehousesOwnerComponent;
        private ManComponent _man;
        
        public WomanComponent(WorldObject owner, int pregnantPeriod) 
            : base(owner, false, pregnantPeriod)
        {
        }

        public override void Start()
        {
            base.Start();
            _inventory = GetComponent<InventoryComponent<Resource>>();
            _warehousesOwnerComponent = GetComponent<WarehousesOwnerComponent>();
        }

        public override void Update()
        {
            base.Update();
            if (Partner != null && _man == null)
                _man = (ManComponent) Partner;
        }

        public void AverageEatWith(InventoryComponent<Resource> otherInventory)
        {
            _inventory.AverageReserveWith<EatableResource, Resource>(otherInventory);
        }

        public override int GetPriorityInBehaviour()
        {
            var man = (ManComponent) Partner;
            return CheckWereDestroyed(man) ? Defaults.BehaviourHaveNotPriority 
                : man.IsVeryHungry() ? Defaults.BehaviourPartnerIsVeryHungry 
                : man.IsHungry() ? Defaults.BehaviourPartnerIsHungry 
                : man.IsReady() && IsReady() ? Defaults.BehaviourItIsTimeToMating 
                : Defaults.BehaviourHaveNotPriority;
        }

        public override WorldObject GetTarget()
        {
            return CheckWereDestroyed(_man) 
                ? null 
                : IsReady() && _man.IsReady() 
                    ? _warehousesOwnerComponent.House 
                    : _man.WorldObject;
        }

        public bool IsHungry()
        {
            return eaterComponent.IsHungry();
        }

        public bool IsVeryHungry()
        {
            return eaterComponent.IsVeryHungry();
        }

        public void SetHouse(House house)
        {
            _warehousesOwnerComponent.House = house;
        }
    }
}