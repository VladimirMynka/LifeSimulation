using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects.Animals.Mating
{
    public abstract class MaleMatingComponent : MatingComponent
    {
        protected FemaleMatingComponent partner;

        protected MaleMatingComponent(WorldObject owner, int ticksToMating = Defaults.AnimalNormalTicksToMating) 
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

        protected bool CheckPartnerHere()
        {
            return !CheckWereDestroyed(partner) &&
                   Direction.CheckEqual(WorldObject.Cell.Coords, partner.WorldObject.Cell.Coords);
        }

        private void SearchPartner()
        {
            SetPartner(visibilityComponent.Search<FemaleMatingComponent>(CanMateWith));
        }

        private void SetPartner(FemaleMatingComponent female)
        {
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

        protected abstract bool CanMateWith(FemaleMatingComponent female);

        protected virtual void Mate(FemaleMatingComponent female)
        {
            female.Mate(this);
            ToWaitingStage();
        }

        public override string GetInformation()
        {
            var info = base.GetInformation() + '\n';
            info += "Gender: male \n";
            info += "Partner: ";
            
            if (CheckWereDestroyed(partner))
                info += "none";
            else
                info += "on " + partner.WorldObject.Cell.Coords[0] + 
                        ',' + partner.WorldObject.Cell.Coords[1];

            return info;
        }
    }
}