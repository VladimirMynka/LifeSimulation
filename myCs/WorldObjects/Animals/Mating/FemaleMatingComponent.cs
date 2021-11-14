using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Mating
{
    public class FemaleMatingComponent : MatingComponent
    {
        public MaleMatingComponent Partner;
        private PregnantComponent _pregnantComponent;
        private MovingComponent _moving;
        private readonly bool _byEggs;
        private readonly int _pregnantPeriod;
        
        public FemaleMatingComponent(
            WorldObject owner, 
            bool byEggs = true,
            int pregnantPeriod = Defaults.PregnantPeriod,
            int ticksToMating = Defaults.AnimalNormalTicksToMating
            )
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
            if (_pregnantComponent != null && _pregnantComponent.WorldObject == null)
                _pregnantComponent = null;

            if (Partner == null)
                return;
            
            if (Partner.WorldObject == null || Partner.WorldObject.Cell == null)
            {
                Partner = null;
                return;
            }
            
            _moving.SetTarget(Partner.WorldObject);
            var sqrLength = _moving.SqrLengthToTarget();
            if (sqrLength >= 0 && sqrLength < 2)
                _moving.WaitFor(1);
        }

        public bool IsOfType(CreatureType type)
        {
            return (type == creatureType);
        }

        public override bool IsReady()
        {
            return (base.IsReady() && Partner == null);
        }

        public void Mate(MaleMatingComponent partner)
        {
            BecomePregnant();
            ToWaitingStage();
            Partner = null;
        }

        private void BecomePregnant()
        {
            _pregnantComponent = new PregnantComponent(WorldObject, _pregnantPeriod, _byEggs);
            WorldObject.AddComponent(_pregnantComponent);
        }
        
        public override string GetInformation()
        {
            var info = base.GetInformation() + '\n';
            info += "Gender: female \n";
            info += "Partner: ";
            
            if (Partner == null)
                info += "none";
            else
                info += "on " + Partner.WorldObject.Cell.Coords[0] + ',' + Partner.WorldObject.Cell.Coords[1];

            if (_pregnantComponent != null)
                info += '\n' + _pregnantComponent.GetInformation();

            return info;
        }
    }
}