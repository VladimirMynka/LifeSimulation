using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Mating
{
    public class FemaleMatingComponent : MatingComponent
    {
        public MaleMatingComponent Partner;
        private MovingComponent _moving;
        private readonly bool _byEggs;
        private readonly int _pregnantPeriod;
        
        public FemaleMatingComponent(
            WorldObject owner, 
            int ticksToMating = Defaults.AnimalNormalTicksToMating,
            bool byEggs = true,
            int pregnantPeriod = Defaults.PregnantPeriod) 
            : base(owner, ticksToMating)
        {
            _byEggs = byEggs;
            _pregnantPeriod = pregnantPeriod;
        }

        public override void Start()
        {
            base.Start();
            _moving = GetComponent<MovingComponent>();
        }

        public override void Update()
        {
            base.Update();
            if (Partner != null) _moving.SetTarget(Partner.WorldObject, false);
        }

        public bool IsEaterOfType(MealType type)
        {
            return (type == eaterComponent.MealType);
        }

        public void Mate(MaleMatingComponent partner)
        {
            BecomePregnant();
            ToWaitingStage();
            Partner = null;
        }

        private void BecomePregnant()
        {
            WorldObject.AddComponent(new PregnantComponent(WorldObject, _pregnantPeriod, _byEggs));
        }
    }
}