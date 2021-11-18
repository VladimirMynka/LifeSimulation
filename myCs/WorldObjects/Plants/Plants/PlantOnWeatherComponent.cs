using LifeSimulation.myCs.World.Weather;

namespace LifeSimulation.myCs.WorldObjects.Plants.Plants
{
    public class PlantOnWeatherComponent: WorldObjectComponent, IDependingOnWeather
    {
        private readonly bool _isAnnual;
        private bool _active;
        private AgeComponent _ageComponent;
        private PlantReproducerComponent _reproducerComponent;
        private bool _wereReproducerComponent;
        public PlantOnWeatherComponent(WorldObject owner, bool isAnnual) : base(owner)
        {
            _isAnnual = isAnnual;
            _active = false;
        }

        public override void Start()
        {
            base.Start();
            _ageComponent = GetComponent<AgeComponent>();
        }

        public void ConfigureByWeather(Weather weather)
        {
            if (!_active && weather.GetSeason() == Season.Winter)
                OnWinter();
            else if (_active && weather.GetSeason() == Season.Spring)
                OnSpring();
        }

        private void OnWinter()
        {
            if (_isAnnual && 
                _ageComponent.AgeStage != AgeStage.Child && 
                _ageComponent.AgeStage != AgeStage.Died)
            {
                WorldObject.Destroy();
                return;
            }
            _active = true;
            _reproducerComponent = GetComponent<PlantReproducerComponent>();
            _wereReproducerComponent = _reproducerComponent != null;
            WorldObject.RemoveComponent(_reproducerComponent);
            _ageComponent.Wait();
        }

        private void OnSpring()
        {
            _active = false;
            if (_wereReproducerComponent)
            {
                _reproducerComponent = new PlantReproducerComponent(WorldObject);
                WorldObject.AddComponent(_reproducerComponent);
            }
            _ageComponent.StopWait();
        }
    }
}