using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;

namespace LifeSimulation.myCs.WorldObjects.Animals.Mating
{
    public class MaleMatingComponent : MatingComponent
    {
        private FemaleMatingComponent _partner;
        private MovingComponent _moving;
        
        public MaleMatingComponent(WorldObject owner, int ticksToMating = Defaults.AnimalNormalTicksToMating) 
            : base(owner, ticksToMating)
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
            {
                _partner = null;
                return;
            }
            if (_partner == null) SearchPartner();
            if (_partner != null) _moving.SetTarget(_partner.WorldObject);
        }

        private void SearchPartner()
        {
            var x = cell.Coords[0];
            var y = cell.Coords[1];
            for (var i = 0; i < visibility; i++)
            {
                for (var j = 0; j < visibility; j++)
                {
                    var currentCell = world.GetCell(x + i, y + j);
                    if (TakePartnerFrom(currentCell)) 
                        return;
                    if (j != 0)
                    {
                        currentCell = world.GetCell(x + i, y - j);
                        if (TakePartnerFrom(currentCell))
                            return;
                    }
                    if (i == 0) continue;
                    currentCell = world.GetCell(x - i, y + j);
                    if (TakePartnerFrom(currentCell)) 
                        return;
                    if (j == 0) continue;
                    currentCell = world.GetCell(x - i, y - j);
                    if (TakePartnerFrom(currentCell))
                        return;
                }
            }
        }

        private bool TakePartnerFrom(Cell checkingCell)
        {
            if (checkingCell == null) return false;
            foreach (var wo in checkingCell.CurrentObjects)
            {
                var partner = wo.GetComponent<FemaleMatingComponent>();
                if (partner == null) continue;
                if (!CanMateWith(partner)) break;
                SetPartner(partner);
                
                return true;
            }
            return false;
        }

        private void SetPartner(FemaleMatingComponent partner)
        {
            partner.Partner = this;
            _partner = partner;
        }

        private bool CanMateWith(FemaleMatingComponent partner)
        {
            if (!partner.IsReady()) return false;
            return partner.IsEaterOfType(eaterComponent.MealType);
        }

        private void Mate(FemaleMatingComponent partner)
        {
            partner.Mate(this);
            ToWaitingStage();
            _partner = null;
        }

        public override string GetInformation()
        {
            var info = base.GetInformation() + '\n';
            info += "Gender: male \n";
            info += "Partner: ";
            
            if (_partner == null)
                info += "none";
            else
                info += "on " + _partner.WorldObject.Cell.Coords[0] + ',' + _partner.WorldObject.Cell.Coords[1];

            return info;
        }
    }
}