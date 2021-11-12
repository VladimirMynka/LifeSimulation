namespace LifeSimulation.myCs.WorldObjects.Animals.RotMeat
{
    public class RotMeatComponent : WorldObjectComponent
    {
        private int _ticksToRot;

        public RotMeatComponent(WorldObject owner, int ticksToRot) : base(owner)
        {
            _ticksToRot = ticksToRot;
        }

        public override void Update()
        {
            _ticksToRot--;
            if (_ticksToRot <= 0)
                WorldObject.Destroy();
        } 
    }
}