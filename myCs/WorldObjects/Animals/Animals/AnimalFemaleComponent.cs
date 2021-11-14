using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Animals.Mating;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;

namespace LifeSimulation.myCs.WorldObjects.Animals.Animals
{
    public class AnimalFemaleComponent : FemaleMatingComponent
    {
        private MovingComponent _moving;
        public AnimalFemaleComponent(WorldObject owner, 
            bool byEggs = true, 
            int pregnantPeriod = Defaults.PregnantPeriod, 
            int ticksToMating = Defaults.AnimalNormalTicksToMating) 
            : base(owner, byEggs, pregnantPeriod, ticksToMating)
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
                DeletePartner();

            if (CheckPartnerNearly())
            {
                _moving.SetTarget(null);
                return;
            }

            if (!CheckWereDestroyed(Partner))
                _moving.SetTarget(Partner.WorldObject);
        }
    }
}