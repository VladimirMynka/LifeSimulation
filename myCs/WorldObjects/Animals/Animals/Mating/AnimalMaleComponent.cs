using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Animals.Mating;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;

namespace LifeSimulation.myCs.WorldObjects.Animals.Animals
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

        protected override FemaleMatingComponent GetFemaleComponent(WorldObject worldObject)
        {
            return worldObject is Animal 
                ? worldObject.GetComponent<AnimalFemaleComponent>() 
                : null;
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