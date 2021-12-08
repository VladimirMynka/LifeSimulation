using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.EatableResources;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Mating
{
    public class WomanComponent : FemaleComponent
    {
        private InventoryComponent<Resource> _inventory;
        private ManComponent _man;
        
        public WomanComponent(WorldObject owner, int pregnantPeriod) 
            : base(owner, false, pregnantPeriod)
        {
        }

        public override void Start()
        {
            base.Start();
            _inventory = GetComponent<InventoryComponent<Resource>>();
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

        public bool IsHungry()
        {
            return eaterComponent.IsHungry();
        }

        public bool IsVeryHungry()
        {
            return eaterComponent.IsVeryHungry();
        }
    }
}