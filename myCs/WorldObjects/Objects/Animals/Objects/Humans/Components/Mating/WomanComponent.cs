using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.EatableResources;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Mating
{
    public class WomanComponent : FemaleComponent, IHaveTarget
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

        /// <summary></summary>
        /// <returns>
        /// 7 if partner is very hungry,
        /// 5 if partner is hungry,
        /// 3 if it's time to mating,
        /// 0 in others
        /// </returns>
        public override int GetPriorityInBehaviour()
        {
            var man = (ManComponent) Partner;
            return CheckWereDestroyed(man) ? 0 
                : man.IsVeryHungry() ? 7 
                : man.IsHungry() ? 5 
                : man.IsReady() && IsReady() ? 3 
                : 0;
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