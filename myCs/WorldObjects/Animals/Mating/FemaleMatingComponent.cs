using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects.Animals.Mating
{
    public class FemaleMatingComponent : MatingComponent
    {
        public MaleMatingComponent Partner;
        private PregnantComponent _pregnantComponent;
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
        
        public override void Update()
        {
            base.Update();
            if (CheckWereDestroyed(_pregnantComponent))
                _pregnantComponent = null;
            
            if (CheckWereDestroyed(Partner))
                Partner = null;
        }

        protected void DeletePartner()
        {
            if (Partner != null)
                Partner.DeletePartner();
        }

        public bool CheckPartnerNearly()
        {
            if (CheckWereDestroyed(Partner)) 
                return false;
            var sqrLength = 
                Direction.SqrLength(WorldObject.Cell.Coords, Partner.WorldObject.Cell.Coords);
            return 0 < sqrLength && sqrLength < 2;
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
            
            if (CheckWereDestroyed(Partner))
                info += "none";
            else
                info += "on " + Partner.WorldObject.Cell.Coords[0] + ',' + Partner.WorldObject.Cell.Coords[1];

            if (_pregnantComponent != null)
                info += '\n' + _pregnantComponent.GetInformation();

            return info;
        }
    }
}