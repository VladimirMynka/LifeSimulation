using LifeSimulation.myCs.Drawer;
using LifeSimulation.myCs.World.Weather;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;

namespace LifeSimulation.myCs.WorldObjects.Animals.Animals
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

        public string GetInformation()
        {
            return "IsSleeping: " + _active;
        }
    }
}