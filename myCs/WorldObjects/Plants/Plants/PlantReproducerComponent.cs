using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Eatable;
using LifeSimulation.myCs.WorldObjects.Plants.Fruits;

namespace LifeSimulation.myCs.WorldObjects.Plants.Plants
{
    public class PlantReproducerComponent : WorldObjectComponent
    {
        private EatableComponent _eatableComponent;
        
        public PlantReproducerComponent(WorldObject owner) : base(owner)
        {
            
        }

        public override void Start()
        {
            base.Start();
            _eatableComponent = WorldObject.GetComponent<EatableComponent>();
        }

        public override void Update()
        {
            Reproduce();
        }

        private void Reproduce()
        {
            if (World.World.Random.Next(100) > Defaults.ReproduceChance) return;

            var neighCell = WorldObject.Cell.GetRandomNeighbour();
            foreach (var neighObject in neighCell.CurrentObjects)
            {
                if (neighObject is Plant || neighObject is Fruit)
                {
                    return;
                }
            }

            if (World.World.Random.Next(2) == 0)
                SpawnPlant(neighCell);
            else
                SpawnFruit(neighCell);
        }

        private void SpawnPlant(Cell cell)
        {
            if (_eatableComponent == null)
                PlantsSpawner.SpawnUneatablePlant(cell);
            else if (_eatableComponent.IsPoisonous())
                PlantsSpawner.SpawnPoisonousPlant(cell);
            else
                PlantsSpawner.SpawnNormalPlant(cell);
        }

        private void SpawnFruit(Cell cell)
        {
            if (_eatableComponent == null)
                FruitsSpawner.SpawnUneatableFruit(cell);
            else if (_eatableComponent.IsPoisonous())
                FruitsSpawner.SpawnPoisonousFruit(cell);
            else
                FruitsSpawner.SpawnNormalFruit(cell);
        }
    }
}