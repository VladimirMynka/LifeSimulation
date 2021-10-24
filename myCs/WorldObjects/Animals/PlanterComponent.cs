using System.Collections.Generic;
using System.Linq;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Plants;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public class PlanterComponent : WorldObjectComponent
    {
        private EaterComponent _eaterComponent;
        public PlanterComponent(WorldObject owner) : base(owner)
        {
        }

        public override void Start()
        {
            base.Start();
            _eaterComponent = worldObject.GetComponent<EaterComponent>();
        }

        public override void Update()
        {
            base.Update();
            if (!_eaterComponent.IsHungry()) PlantSeed();
        }

        private void PlantSeed()
        {
            if (worldObject.Cell.CurrentObjects.Any(neighObject => neighObject is Plant || neighObject is Fruit))
            {
                return;
            }
            var chance = _eaterComponent.Satiety * Defaults.AnimalPlantProbability / 100;
            var random = World.World.Random.Next(0, _eaterComponent.MaxSatiety);
            if (random < chance)
            {
                world.AddPlant(worldObject.Cell);
            }
        }
    }
}