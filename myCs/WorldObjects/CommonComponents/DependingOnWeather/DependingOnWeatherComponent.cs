using System.Collections.Generic;
using LifeSimulation.myCs.WorldStructure.Weather;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents.DependingOnWeather
{
    public class DependingOnWeatherComponent : WorldObjectComponent
    {
        private List<IDependingOnWeather> _dependingComponents;
        private readonly Weather _weather;

        public DependingOnWeatherComponent(WorldObject owner) : base(owner)
        {
            _weather = world.Weather;
        }

        public override void Start()
        {
            base.Start();
            _dependingComponents = GetComponents<IDependingOnWeather>();
        }

        public override void Update()
        {
            base.Update();
            ConfigureAll();
            CheckAndRemove();
        }

        private void ConfigureAll()
        {
            foreach (var component in _dependingComponents)
            {
                component.ConfigureByWeather(_weather);
            }
        }

        public void AddComponent(IDependingOnWeather component)
        {
            _dependingComponents.Add(component);
        }

        private void CheckAndRemove()
        {
            var clone = new IDependingOnWeather[_dependingComponents.Count];
            _dependingComponents.CopyTo(clone);

            foreach (var component in clone)
            {
                if (CheckWereDestroyed(component))
                    _dependingComponents.Remove(component);
            }
        }

        private static bool CheckWereDestroyed(IDependingOnWeather component)
        {
            return CheckWereDestroyed(component as WorldObjectComponent);
        }
    }
}