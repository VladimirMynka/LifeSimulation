using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects
{
    public class HealthComponent : WorldObjectComponent
    {
        private int _health;
        private readonly int _maxHealth;

        public HealthComponent(WorldObject owner, int health) : base(owner)
        {
            _health = health;
            _maxHealth = health;
        }

        public override void Update()
        {
            if (CantLive())
                WorldObject.Destroy();
        }

        private bool CantLive()
        {
            return (_health <= 0);
        }

        public void AddHealth(int delta)
        {
            _health += delta;
            if (_health > _maxHealth) 
                _health = _maxHealth;
        }

    }
}