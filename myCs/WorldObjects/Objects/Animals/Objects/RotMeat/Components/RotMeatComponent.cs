using LifeSimulation.myCs.WorldObjects.CommonComponents;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.RotMeat.Components
{
    public class RotMeatComponent : WorldObjectComponent, IHaveInformation
    {
        private int _ticksToRot;
        private readonly CreatureType _creatureType;

        public RotMeatComponent(WorldObject owner, int ticksToRot, CreatureType creatureType) : base(owner)
        {
            _ticksToRot = ticksToRot;
            _creatureType = creatureType;
        }

        public override void Update()
        {
            _ticksToRot--;
            if (_ticksToRot <= 0)
                WorldObject.Destroy();
        } 
        
        public override string ToString()
        {
            var info = "Type: " + _creatureType + '\n';
            info += "Ticks to rot at all: " + _ticksToRot;
            return info;
        }

        public int GetInformationPriority()
        {
            return 1;
        }
    }
}