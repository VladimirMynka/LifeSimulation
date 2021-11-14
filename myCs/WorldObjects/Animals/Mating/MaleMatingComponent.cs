using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Animals.Animals;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;

namespace LifeSimulation.myCs.WorldObjects.Animals.Mating
{
    public abstract class MaleMatingComponent : MatingComponent
    {
        protected FemaleMatingComponent partner;
        
        public MaleMatingComponent(WorldObject owner, int ticksToMating = Defaults.AnimalNormalTicksToMating) 
            : base(owner, ticksToMating)
        {
        }

        public override void Update()
        {
            base.Update();
            if (eaterComponent.IsHungry())
                return;
            if (partner == null || 
                partner.WorldObject == null || 
                partner.WorldObject.Cell == null) 
                SearchPartner();
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
                var female = GetComponentFrom(worldObject);
                if (female == null) 
                    continue;
                if (!CanMateWith(female)) 
                    break;
                SetPartner(female);
                
                return true;
            }
            return false;
        }

        protected abstract FemaleMatingComponent GetComponentFrom(WorldObject worldObject);

        private void SetPartner(FemaleMatingComponent female)
        {
            female.Partner = this;
            partner = female;
        }

        private bool CanMateWith(FemaleMatingComponent female)
        {
            return female.IsReady() && female.IsOfType(creatureType);
        }

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
            
            if (partner == null || partner.WorldObject == null || partner.WorldObject.Cell == null)
                info += "none";
            else
                info += "on " + partner.WorldObject.Cell.Coords[0] + ',' + partner.WorldObject.Cell.Coords[1];

            return info;
        }
    }
}