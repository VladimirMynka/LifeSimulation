﻿using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Mating
{
    public abstract class MatingComponent : WorldObjectComponent, IHaveInformation
    {
        protected EaterComponent eaterComponent;
        protected CreatureType creatureType;
        private int _ticksToMating;
        private readonly int _normalTicksToMating;

        protected Cell cell;
        protected int visibility;

        protected MatingComponent(WorldObject owner, int ticksToMating = Defaults.AnimalNormalTicksToMating) 
            : base(owner)
        {
            _ticksToMating = ticksToMating;
            _normalTicksToMating = ticksToMating;
        }

        public override void Start()
        {
            base.Start();
            eaterComponent = WorldObject.GetComponent<EaterComponent>();
            creatureType = WorldObject.GetComponent<EatableComponent>().CreatureType;
            cell = WorldObject.Cell;
            visibility = Defaults.AnimalVisibleArea;
        }

        public override void Update()
        {
            base.Update();
            Tick();
        }

        private void Tick()
        {
            if (_ticksToMating > 0)
                _ticksToMating--;
        }

        public virtual bool IsReady()
        {
            return (_ticksToMating <= 0 && !eaterComponent.IsHungry());
        }
        
        protected void ToWaitingStage()
        {
            _ticksToMating = _normalTicksToMating;
        }

        public virtual string GetInformation()
        {
            var info = "TicksToMating: " + _ticksToMating;
            return info;
        }
    }
}