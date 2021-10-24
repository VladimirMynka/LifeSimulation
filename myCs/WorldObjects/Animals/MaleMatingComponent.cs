﻿using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public class MaleMatingComponent : MatingComponent
    {
        private FemaleMatingComponent _partner;
        
        public MaleMatingComponent(WorldObject owner, int ticksToMating = Defaults.AnimalNormalTicksToMating) 
            : base(owner, ticksToMating)
        {
            
        }

        public override void Update()
        {
            base.Update();
            if (eaterComponent.IsHungry()) return;
            if (_partner == null) SearchPartner();
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
            foreach (var wo in checkingCell.CurrentObjects)
            {
                var partner = wo.GetComponent<FemaleMatingComponent>();
                if (partner == null) continue;
                if (CanMateWith(partner)) break;
                _partner = partner;
                
                return true;
            }
            return false;
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
    }
}