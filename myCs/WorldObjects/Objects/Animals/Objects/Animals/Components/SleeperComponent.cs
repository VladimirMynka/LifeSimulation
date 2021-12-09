using LifeSimulation.myCs.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.DependingOnWeather;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Moving;
using LifeSimulation.myCs.WorldStructure.Weather;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals.Components
{
    public class SleeperComponent : WorldObjectComponent, IHaveInformation, IDependingOnWeather
    {
        private HealthComponent _healthComponent;
        private MovingComponent _movingComponent;
        private DrawableComponent _drawableComponent;
        private bool _active;
        
        public SleeperComponent(WorldObject owner) : base(owner)
        {
        }

        public override void Start()
        {
            base.Start();
            _healthComponent = GetComponent<HealthComponent>();
            _movingComponent = GetComponent<MovingComponent>();
            _drawableComponent = GetComponent<DrawableComponent>();
        }

        public override void Update()
        {
            base.Update();
            if (!_active)
            {
                _drawableComponent.Image = _drawableComponent.DefaultImage;
                return;
            }

            _movingComponent.WaitFor(1);
            _healthComponent.AddHealth(5);
            _drawableComponent.SetImage(Pictures.Sleeper);
        }

        public void ConfigureByWeather(Weather weather)
        {
            _active = weather.GetSeason() == Season.Winter;
        }

        public override string ToString()
        {
            return "IsSleeping: " + _active;
        }

        public int GetInformationPriority()
        {
            return Defaults.InfoPrioritySleeper;
        }
    }
}