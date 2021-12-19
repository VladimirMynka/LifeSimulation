using LifeSimulation.myCs.WorldObjects.CommonComponents;

namespace LifeSimulation.myCs.Resources.UneatableResources
{
    public class SeedResource : UneatableResource
    {
        private readonly CreatureType _plantType;

        public SeedResource(int count = 0, CreatureType plantType = CreatureType.EatableGreenPlant) 
            : base(count)
        {
            _plantType = plantType;
        }

        public CreatureType GetCreatureType()
        {
            return _plantType;
        }
        
        public SeedResource() : base(0)
        { }

        public override Resource Clone()
        {
            return new SeedResource(GetCount());
        }
    }
}