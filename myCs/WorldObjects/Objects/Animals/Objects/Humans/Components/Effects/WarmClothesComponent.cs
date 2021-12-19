using LifeSimulation.myCs.WorldObjects.CommonComponents.DependingOnWeather;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldStructure.Weather;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Effects
{
    public class WarmClothesComponent : EffectComponent, IDependingOnWeather
    {
        private HealthComponent _healthComponent;
        private EaterComponent _eaterComponent;
        private int _savingHealth;
        private int _savingSatiety;
        public WarmClothesComponent(WorldObject owner, int period) : base(owner, period)
        {
        }

        public override void Start()
        {
            base.Start();
            _healthComponent = GetComponent<HealthComponent>();
            _eaterComponent = GetComponent<EaterComponent>();
        }

        public override void Update()
        {
            base.Update();
            _healthComponent.AddHealth(_savingHealth);
            _eaterComponent.AddSatiety(_savingSatiety);
        }

        public void ConfigureByWeather(Weather weather)
        {
            if (weather.GetTemperature() < 0)
            {
                _savingHealth = 1;
                _savingSatiety = 1;
                return;
            }
            _savingHealth = 0; 
            _savingSatiety = 0;
        }
    }
}