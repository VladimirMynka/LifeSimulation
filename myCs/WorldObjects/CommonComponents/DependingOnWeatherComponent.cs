using System.Collections.Generic;
using LifeSimulation.myCs.WorldStructure.Weather;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents
{
    public class DependingOnWeatherComponent : WorldObjectComponent
    {
        private readonly List<IDependingOnWeather> _dependingComponents;
        private Weather _weather;

        public DependingOnWeatherComponent(WorldObject owner) : base(owner)
        {
            _dependingComponents = new List<IDependingOnWeather>();
        }

        public override void Start()
        {
            base.Start();
            var components = GetComponents<IDependingOnWeather>();
            foreach (var component in components)
            {
                _dependingComponents.Add(component as IDependingOnWeather);
            }
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