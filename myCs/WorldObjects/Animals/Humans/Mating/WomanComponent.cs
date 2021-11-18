using LifeSimulation.myCs.WorldObjects.Animals.Mating;

namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
{
    public class WomanComponent : FemaleMatingComponent, IHumanMating
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