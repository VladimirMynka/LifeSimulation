using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public abstract class MatingComponent : WorldObjectComponent
    {
        protected EaterComponent eaterComponent;
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
            eaterComponent = worldObject.GetComponent<EaterComponent>();
            cell = worldObject.Cell;
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

        public bool IsReady()
        {
            return (_ticksToMating <= 0 && !eaterComponent.IsHungry());
        }
        
        protected void ToWaitingStage()
        {
            _ticksToMating = _normalTicksToMating;
        }
    }
}