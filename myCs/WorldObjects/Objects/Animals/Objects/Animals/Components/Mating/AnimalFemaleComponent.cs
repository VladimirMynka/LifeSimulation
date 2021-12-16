using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Moving;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals.Components.Mating
{
    public class AnimalFemaleComponent : FemaleComponent
    {
        private MovingComponent _moving;
        private readonly bool _byEggs;
        public AnimalFemaleComponent(WorldObject owner, 
            bool byEggs = true, 
            int pregnantPeriod = Defaults.PregnantPeriod, 
            int ticksToMating = Defaults.AnimalNormalTicksToMating) 
            : base(owner, pregnantPeriod, ticksToMating)
        {
            _byEggs = byEggs;
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
        }

        protected override PregnantComponent CreatePregnantComponent(int pregnantPeriod)
        {
            return new AnimalPregnantComponent(WorldObject, pregnantPeriod, _byEggs);
        }

        public override int GetPriorityInBehaviour()
        {
            return CheckWereDestroyed(Partner) ? Defaults.BehaviourHaveNotPriority 
                : Defaults.BehaviourAnimalMating;
        }
    }
}