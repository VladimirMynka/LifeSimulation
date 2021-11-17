using LifeSimulation.myCs.WorldObjects.Plants.Plants;

namespace LifeSimulation.myCs.WorldObjects.Plants.Fruits
{
    public class RotComponent : WorldObjectComponent
    {
        private readonly CreatureType _creatureType;
        public RotComponent(WorldObject owner, CreatureType creatureType) : base(owner)
        {
            _creatureType = creatureType;
        }

        protected override void OnDestroy()
        {
            SpawnPlant();
        }

        private void SpawnPlant()
        {
            Plant.SpawnPlantByType(WorldObject.Cell, _creatureType);
        }
    }
}