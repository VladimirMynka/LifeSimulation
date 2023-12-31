﻿using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Behaviour;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating
{
    public abstract class MatingComponent : WorldObjectComponent, IHaveInformation, IHaveTarget
    {
        protected EaterComponent eaterComponent;
        protected CreatureType creatureType;
        private int _ticksToMating;
        private readonly int _normalTicksToMating;

        protected Cell cell;
        protected VisibilityComponent visibilityComponent;

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
            visibilityComponent = GetComponent<VisibilityComponent>();
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

        public override string ToString()
        {
            var info = "TicksToMating: " + _ticksToMating;
            return info;
        }

        public int GetInformationPriority()
        {
            return Defaults.InfoPriorityMating;
        }

        public virtual int GetPriorityInBehaviour()
        {
            return 0;
        }

        public virtual WorldObject GetTarget()
        {
            return null;
        }

        public abstract MatingComponent GetPartner();
    }
}