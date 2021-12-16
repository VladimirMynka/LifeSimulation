using System.Linq;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.DependingOnWeather;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Plants.Fruits;
using LifeSimulation.myCs.WorldObjects.Objects.Plants.Plants;
using LifeSimulation.myCs.WorldStructure;
using LifeSimulation.myCs.WorldStructure.Weather;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals.Components
{
    public class PlanterComponent : WorldObjectComponent, IDependingOnWeather
    {
        private EaterComponent _eaterComponent;
        private bool _tooCold;
        public PlanterComponent(WorldObject owner) : base(owner)
        {
        }

        public override void Start()
        {
            base.Start();
            _eaterComponent = WorldObject.GetComponent<EaterComponent>();
        }

        public override void Update()
        {
            base.Update();
            if (!_eaterComponent.IsHungry()) PlantSeed();
        }

        private void PlantSeed()
        {
            if (WorldObject.Cell.CurrentObjects.Any(neighObject => neighObject is Plant || neighObject is Fruit))
                return;
            var chance = _eaterComponent.GetSatiety() * Defaults.AnimalPlantProbability / 100;
            var random = World.Random.Next(0, _eaterComponent.GetMaxSatiety());
            if (random < chance)
            {
                Plant.SpawnRandomPlant(WorldObject.Cell);
            }
        }

        public void ConfigureByWeather(Weather weather)
        {
            _tooCold = weather.GetTemperature() < 0;
        }
    }
}