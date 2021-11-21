using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Moving;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals.Components.Mating
{
    public class AnimalMaleComponent : MaleMatingComponent
    {
        private MovingComponent _moving;
        public AnimalMaleComponent(WorldObject owner, int ticksToMating = Defaults.AnimalNormalTicksToMating) : base(owner, ticksToMating)
        {
        }
        
        public override void Start()
        {
            base.Start();
            _moving = GetComponent<MovingComponent>();
        }

        public override void Update()
        {
            base.Update();
            
            if (eaterComponent.IsHungry())
            {
                DeletePartner();
                return;
            }
            
            if (!CheckWereDestroyed(partner)) 
                _moving.SetTarget(partner.WorldObject);
        }

        protected override bool CanMateWith(FemaleMatingComponent female)
        {
            return female.IsReady() && female.IsOfType(creatureType);
        }

        protected override void Mate(FemaleMatingComponent female)
        {
            base.Mate(female);
            DeletePartner();
        }
    }
}