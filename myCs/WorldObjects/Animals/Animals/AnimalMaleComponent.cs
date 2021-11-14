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
                partner = null;
                return;
            }
            
            if (partner != null) 
                _moving.SetTarget(partner.WorldObject);
            if (partner != null && _moving.SqrLengthToTarget() == 0)
                Mate(partner);
        }

        protected override FemaleMatingComponent GetComponentFrom(WorldObject worldObject)
        {
            return worldObject is Animal 
                ? worldObject.GetComponent<AnimalFemaleComponent>() 
                : null;
        }

        protected override void Mate(FemaleMatingComponent female)
        {
            base.Mate(female);
            partner = null;
        }
    }
}