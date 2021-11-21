using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Mating
{
    public class ManComponent : MaleComponent, IHaveTarget
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
            
            if (!CheckWereDestroyed(_woman) && CheckPartnerHere() 
                                            && (_woman.IsHungry() || IsHungry()))
                _woman.AverageEatWith(_inventory);
        }

        
        /// <summary></summary>
        /// <returns>
        /// 7 if partner is very hungry,
        /// 5 if partner is hungry,
        /// 3 if it's time to mating,
        /// 0 in others
        /// </returns>
        public override int GetPriority()
        {
            return CheckWereDestroyed(_woman) ? 0 
                : _woman.IsVeryHungry() ? 7 
                : _woman.IsHungry() ? 5 
                : _woman.IsReady() && IsReady() ? 3 
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

        protected override bool CanMateWith(FemaleComponent female)
        {
            return female != null && female.IsReady() && female is WomanComponent;
        }
    }
}