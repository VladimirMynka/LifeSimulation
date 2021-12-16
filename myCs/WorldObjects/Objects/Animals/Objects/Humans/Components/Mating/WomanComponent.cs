using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.EatableResources;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings.Components;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Mating
{
    public class WomanComponent : FemaleComponent
    {
        private InventoryComponent<Resource> _inventory;
        private WarehousesOwnerComponent _warehousesOwnerComponent;
        private CitizenComponent _citizenComponent;
        private ManComponent _man;
        
        public WomanComponent(WorldObject owner, int pregnantPeriod) 
            : base(owner, pregnantPeriod)
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

        public int PriorityWhenPartnerIsVeryHungry = Defaults.BehaviourPartnerIsVeryHungry;
        public int PriorityWhenPartnerIsHungry = Defaults.BehaviourPartnerIsHungry;
        public int PriorityWhenItIsTimeToMating = Defaults.BehaviourItIsTimeToMating;
        public override int GetPriorityInBehaviour()
        {
            var man = (ManComponent) Partner;
            return CheckWereDestroyed(man) ? Defaults.BehaviourHaveNotPriority 
                : man.IsVeryHungry() ? PriorityWhenPartnerIsVeryHungry
                : man.IsHungry() ? PriorityWhenPartnerIsHungry
                : man.IsReady() && IsReady() ? PriorityWhenItIsTimeToMating
                : Defaults.BehaviourHaveNotPriority;
        }

        protected override PregnantComponent CreatePregnantComponent(int pregnantPeriod)
        {
            return new WomanPregnantComponent(WorldObject, pregnantPeriod);
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
            if (house == _warehousesOwnerComponent.House)
                return;
            _warehousesOwnerComponent.House = house;
            WorldObject.RemoveComponent(_citizenComponent);
            _citizenComponent = new CitizenComponent(WorldObject,
                house.GetComponent<BuildingComponent<Resource>>().Village);
            WorldObject.AddComponent(_citizenComponent);
        }

        public House GetHouse()
        {
            return _warehousesOwnerComponent.House;
        }
    }
}