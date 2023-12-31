﻿using LifeSimulation.myCs.Settings;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating
{
    public abstract class MaleComponent : MatingComponent
    {
        protected FemaleComponent partner;

        protected MaleComponent(WorldObject owner, int ticksToMating = Defaults.AnimalNormalTicksToMating) 
            : base(owner, ticksToMating)
        {
        }

        public override void Update()
        {
            base.Update();
            if (eaterComponent.IsHungry())
                return;
            if (CheckWereDestroyed(partner)) 
                SearchPartner();
            if (CheckPartnerHere())
                Mate(partner);
        }

        protected virtual bool CheckPartnerHere()
        {
            return !CheckWereDestroyed(partner) &&
                   OnOneCellWith(partner);
        }

        private void SearchPartner()
        {
            SetPartner(visibilityComponent.Search<FemaleComponent>(FemaleCheckInSearch));
        }

        protected virtual bool FemaleCheckInSearch(FemaleComponent component)
        {
            return CanMateWith(component) && component.Partner == null;
        }

        private void SetPartner(FemaleComponent female)
        {
            if (CheckWereDestroyed(female))
                return;
            female.Partner = this;
            partner = female;
        }

        public void DeletePartner()
        {
            if (partner == null)
                return;
            partner.Partner = null;
            partner = null;
        }

        protected abstract bool CanMateWith(FemaleComponent female);

        protected virtual void Mate(FemaleComponent female)
        {
            if (!CanMateWith(female))
                return;
            female.Mate(this);
            ToWaitingStage();
        }

        public override string ToString()
        {
            var info = base.ToString() + '\n';
            info += "Gender: male \n";
            info += "Partner: ";
            
            if (CheckWereDestroyed(partner))
                info += "none";
            else
                info += "on " + partner.WorldObject.Cell.Coords[0] + 
                        ',' + partner.WorldObject.Cell.Coords[1];

            return info;
        }

        public override WorldObject GetTarget()
        {
            return CheckWereDestroyed(partner) ? null
                : partner.WorldObject;
        }

        public override MatingComponent GetPartner()
        {
            return partner;
        }
    }
}