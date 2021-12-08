using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating
{
    public class FemaleComponent : MatingComponent
    {
        public MaleComponent Partner;
        private PregnantComponent _pregnantComponent;
        private readonly bool _byEggs;
        private readonly int _pregnantPeriod;
        
        public FemaleComponent(
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
            return (base.IsReady() && Partner == null && _pregnantComponent == null);
        }

        public void Mate(MaleComponent partner)
        {
            BecomePregnant();
            ToWaitingStage();
        }

        private void BecomePregnant()
        {
            _pregnantComponent = new PregnantComponent(WorldObject, _pregnantPeriod, _byEggs);
            WorldObject.AddComponent(_pregnantComponent);
        }
        
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
    }
}