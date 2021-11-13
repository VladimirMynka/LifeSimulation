namespace LifeSimulation.myCs.WorldObjects.Animals.RotMeat
{
    public class RotMeatComponent : WorldObjectComponent
    {
        private int _ticksToRot;
        private CreatureType _creatureType;

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
        
        public string GetInformation()
        {
            var info = "Type: " + _creatureType + '\n';
            info = "Ticks to rot at all: " + _ticksToRot;
            return info;
        }
    }
}