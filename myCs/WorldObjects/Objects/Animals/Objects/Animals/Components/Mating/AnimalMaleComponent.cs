using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Moving;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals.Components.Mating
{
    public class AnimalMaleComponent : MaleComponent
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
                DeletePartner();
        }

        protected override bool CanMateWith(FemaleComponent female)
        {
            return female.IsReady() && female.IsOfType(creatureType);
        }
        
        protected override void Mate(FemaleComponent female)
        {
            base.Mate(female);
            DeletePartner();
        }
        
        public override int GetPriorityInBehaviour()
        {
            return CheckWereDestroyed(partner) ? Defaults.BehaviourHaveNotPriority 
                : Defaults.BehaviourAnimalMating;
        }
    }
}