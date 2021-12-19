using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Objects.Plants.Fruits;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Plants.Plants
{
    public class PlantReproducerComponent : WorldObjectComponent
    {
        private PlantAgeComponent _plantAgeComponent;
        private SeedKeeperComponent _seedKeeperComponent;

        public PlantReproducerComponent(WorldObject owner) : base(owner)
        {
            
        }

        public override void Start()
        {
            base.Start();
            _plantAgeComponent = WorldObject.GetComponent<PlantAgeComponent>();
            _seedKeeperComponent = WorldObject.GetComponent<SeedKeeperComponent>();
        }

        public override void Update()
        {
            Reproduce();
        }

        private void Reproduce()
        {
            if (World.Random.Next(100) > Defaults.ReproduceChance 
                * _seedKeeperComponent.GetCount() / SeedKeeperComponent.MaxSeedCount) 
                return;

            var neighCell = WorldObject.Cell.GetRandomNeighbour();
            
            if (neighCell.Contains<Plant>())
                return;
            if (neighCell.Contains<Fruit>())
                return;

            if (World.Random.Next(2) == 0)
                SpawnPlant(neighCell);
            else
                SpawnFruit(neighCell);
        }

        private void SpawnPlant(Cell cell)
        {
            Plant.SpawnPlantByType(cell, _plantAgeComponent.CreatureType);
        }

        private void SpawnFruit(Cell cell)
        {
            Fruit.SpawnFruitByType(cell, _plantAgeComponent.CreatureType);
        }
    }
}