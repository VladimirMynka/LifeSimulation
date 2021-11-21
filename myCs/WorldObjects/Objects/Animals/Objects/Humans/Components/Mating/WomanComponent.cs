using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Mating
{
    public class WomanComponent : FemaleMatingComponent, IHaveTarget
    {
        private InventoryComponent _inventory;
        public WomanComponent(WorldObject owner, int pregnantPeriod) 
            : base(owner, false, pregnantPeriod)
        {
        }

        public override void Start()
        {
            base.Start();
            _inventory = GetComponent<InventoryComponent>();
        }

        public override void Update()
        {
            base.Update();
            
        }

        public void AverageEatWith(InventoryComponent otherInventory)
        {
            _inventory.AverageReserveWith(otherInventory);
        }

        public int GetPriority()
        {
            throw new System.NotImplementedException();
        }

        public WorldObject GetTarget()
        {
            throw new System.NotImplementedException();
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