using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public class PregnantComponent : WorldObjectComponent
    {
        protected EaterComponent eaterComponent;
        private int _ticksToBirthday;

        public PregnantComponent(WorldObject owner, int ticksToBirthday = Defaults.AnimalNormalTicksToMating) 
            : base(owner)
        {
            _ticksToBirthday = ticksToBirthday;
        }

        public override void Start()
        {
            base.Start();
            eaterComponent = worldObject.GetComponent<EaterComponent>();
        }

        public override void Update()
        {
            base.Update();
            if (_ticksToBirthday > 0)
                _ticksToBirthday--;
            else
                Destroy();
        }

        protected override void OnDestroy()
        {
            
        }
    }
}