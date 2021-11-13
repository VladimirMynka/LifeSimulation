﻿using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Mating
{
    public class FemaleMatingComponent : MatingComponent
    {
        public MaleMatingComponent Partner;
        private MovingComponent _moving;
        private readonly bool _byEggs;
        private readonly int _pregnantPeriod;
        
        public FemaleMatingComponent(
            WorldObject owner, 
            int ticksToMating = Defaults.AnimalNormalTicksToMating,
            bool byEggs = true,
            int pregnantPeriod = Defaults.PregnantPeriod) 
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
            if (Partner == null)
                return;
            _moving.SetTarget(Partner.WorldObject);
            var sqrLength = _moving.SqrLengthToTarget();
            if (sqrLength >= 0 && sqrLength < 2)
                _moving.WaitFor(5);
        }

        public bool IsEaterOfType(MealType type)
        {
            return (type == eaterComponent.MealType);
        }

        public void Mate(MaleMatingComponent partner)
        {
            BecomePregnant();
            ToWaitingStage();
            Partner = null;
        }

        private void BecomePregnant()
        {
            WorldObject.AddComponent(new PregnantComponent(WorldObject, _pregnantPeriod, _byEggs));
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

            return info;
        }
    }
}