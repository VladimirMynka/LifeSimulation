using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Animals.Mating;

namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
{
    public class ManComponent : MaleMatingComponent, IHumanMating
    {
        private InventoryComponent _inventory;
        private WomanComponent _woman;
        
        public ManComponent(WorldObject owner) : base(owner)
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
            
            if (partner != null && _woman == null)
                _woman = (WomanComponent) partner;
            
            if (!CheckWereDestroyed(_woman) && _woman.IsHungry() && CheckPartnerHere())
                _woman.AverageEatWith(_inventory);
        }

        public int GetPriority()
        {
            if (partner == null)
                return 0;
            var woman = (IHumanMating) partner;
            if (woman.IsVeryHungry())
                return 7;
            if (woman.IsHungry())
                return 5;
            if (partner.IsReady() && IsReady())
                return 3;
            return 0;
        }

        public WorldObject GetTarget()
        {
            return partner != null ? partner.WorldObject : null;
        }

        public bool IsHungry()
        {
            return eaterComponent.IsHungry();
        }

        public bool IsVeryHungry()
        {
            return eaterComponent.IsVeryHungry();
        }

        protected override FemaleMatingComponent GetFemaleComponent(WorldObject worldObject)
        {
            return worldObject is Human 
                ? worldObject.GetComponent<WomanComponent>()
                : null;
        }

        protected override bool CanMateWith(FemaleMatingComponent female)
        {
            return female != null && female.IsReady() && female is WomanComponent;
        }
    }
}