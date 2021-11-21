using System.Collections.Generic;
using LifeSimulation.myCs.WorldStructure.Weather;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents
{
    public class DependingOnWeatherComponent : WorldObjectComponent
    {
        private List<IDependingOnWeather> _dependingComponents;
        private Weather _weather;

        public DependingOnWeatherComponent(WorldObject owner) : base(owner)
        {
        }

        public override void Start()
        {
            base.Start();
            _dependingComponents = GetComponents<IDependingOnWeather>();
            _weather = world.Weather;
        }

        public override void Update()
        {
            base.Update();
            foreach (var component in _dependingComponents)
            {
                component.ConfigureByWeather(_weather);
            }
        }

        public void AddComponent(IDependingOnWeather component)
        {
            _dependingComponents.Add(component);
        }
    }
}