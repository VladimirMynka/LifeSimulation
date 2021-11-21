using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldStructure.Weather;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents
{
    public class HealthComponent : WorldObjectComponent, IHaveInformation, IDependingOnWeather
    {
        private int _health;
        private readonly int _maxHealth;
        private int _regeneration;
        private readonly int _normalRegeneration;

        public HealthComponent(WorldObject owner, int health, int regeneration) : base(owner)
        {
            _health = health;
            _maxHealth = health;
            _regeneration = regeneration;
            _normalRegeneration = regeneration;
        }

        public override void Update()
        {
            if (!IsFull())
                Regenerate();
            if (CantLive())
                WorldObject.Destroy();
        }
        
        private bool IsFull()
        {
            return _health == _maxHealth;
        }
        
        private bool CantLive()
        {
            return (_health <= 0);
        }
        
        private void Regenerate()
        {
            _health += _regeneration;
            if (_health > _maxHealth)
                _health = _maxHealth;
        }

        public void AddHealth(int delta)
        {
            _health += delta;
            if (_health > _maxHealth) 
                _health = _maxHealth;
        }
        
        public void ConfigureByWeather(Weather weather)
        {
            var t = weather.GetTemperature();
            if (t <= -30 || t >= 50)
                _regeneration = _normalRegeneration / -2;
            else if (t <= -15 || t >= 40)
                _regeneration = 0;
            else if (t <= 0 || t >= 30)
                _regeneration = _normalRegeneration / 2;
            else
                _regeneration = _normalRegeneration;
        }

        public string GetInformation()
        {
            var info = "Health: " + _health + '/' + _maxHealth;
            return info;
        }

        
    }
}