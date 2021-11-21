using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Plants.Plants;

namespace LifeSimulation.myCs.WorldObjects.Objects.Plants.Fruits
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