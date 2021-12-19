using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating
{
    public abstract class FemaleComponent : MatingComponent
    {
        public MaleComponent Partner;
        private PregnantComponent _pregnantComponent;
        private readonly int _pregnantPeriod;

        protected FemaleComponent(
            WorldObject owner, 
            int pregnantPeriod = Defaults.PregnantPeriod,
            int ticksToMating = Defaults.AnimalNormalTicksToMating
            )
                : base(owner, ticksToMating)
        {
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
                GetSqrLengthWith(Partner);
            return 0 < sqrLength && sqrLength < 2;
        }

        public bool IsOfType(CreatureType type)
        {
            return (type == creatureType);
        }

        public override bool IsReady()
        {
            return base.IsReady() && _pregnantComponent == null;
        }

        public void Mate(MaleComponent partner)
        {
            BecomePregnant();
            ToWaitingStage();
        }

        private void BecomePregnant()
        {
            _pregnantComponent = CreatePregnantComponent(_pregnantPeriod);
            WorldObject.AddComponent(_pregnantComponent);
        }

        protected abstract PregnantComponent CreatePregnantComponent(int pregnantPeriod);
        
        public override string ToString()
        {
            var info = base.ToString() + '\n';
            info += "Gender: female \n";
            info += "Partner: ";
            
            if (CheckWereDestroyed(Partner))
                info += "none";
            else
                info += "on " + Partner.WorldObject.Cell.Coords[0] + ',' + Partner.WorldObject.Cell.Coords[1];

            return info;
        }

        public override WorldObject GetTarget()
        {
            return CheckWereDestroyed(Partner) ? null 
                : Partner.WorldObject;
        }

        public override MatingComponent GetPartner()
        {
            return Partner;
        }
    }
}