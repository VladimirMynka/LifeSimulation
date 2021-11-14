using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Animals.Animals;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;

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
            var x = cell.Coords[0];
            var y = cell.Coords[1];
            for (var radius = 0; radius < visibility; radius++)
            {
                for (var j = 0; j <= radius; j++)
                {
                    var i = radius - j;
                    var currentCell = world.GetCell(x + i, y + j);
                    if (TakePartnerFrom(currentCell)) 
                        return;
                    if (j != 0)
                    {
                        currentCell = world.GetCell(x + i, y - j);
                        if (TakePartnerFrom(currentCell))
                            return;
                    }
                    if (i == 0) 
                        continue;
                    currentCell = world.GetCell(x - i, y + j);
                    if (TakePartnerFrom(currentCell)) 
                        return;
                    if (j == 0) 
                        continue;
                    currentCell = world.GetCell(x - i, y - j);
                    if (TakePartnerFrom(currentCell))
                        return;
                }
            }
        }

        private bool TakePartnerFrom(Cell checkingCell)
        {
            if (checkingCell == null) 
                return false;
            foreach (var worldObject in checkingCell.CurrentObjects)
            {
                var female = GetFemaleComponent(worldObject);
                if (female == null) 
                    continue;
                if (!CanMateWith(female)) 
                    break;
                SetPartner(female);
                
                return true;
            }
            return false;
        }

        protected abstract FemaleMatingComponent GetFemaleComponent(WorldObject worldObject);

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