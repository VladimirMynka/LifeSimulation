﻿using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Moving;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals.Components.Mating
{
    public class AnimalFemaleComponent : FemaleComponent
    {
        private MovingComponent _moving;
        public AnimalFemaleComponent(WorldObject owner, 
            bool byEggs = true, 
            int pregnantPeriod = Defaults.PregnantPeriod, 
            int ticksToMating = Defaults.AnimalNormalTicksToMating) 
            : base(owner, byEggs, pregnantPeriod, ticksToMating)
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
                DeletePartner();
        }

        public override int GetPriorityInBehaviour()
        {
            return CheckWereDestroyed(Partner) ? 0 
                : 5;
        }
    }
}