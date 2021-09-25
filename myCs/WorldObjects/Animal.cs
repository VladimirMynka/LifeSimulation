using System.IO;

namespace LifeSimulation.myCs.WorldObjects
{
    public class Animal : WorldObject
    {
        private int _health;
        private int _maxHealth;
        private int _satiety;
        private int _maxSatiety;
        
        
        public Animal(Cell keeper, int newColor = 0) : base(keeper, newColor)
        {
            
        }

        public override void Update()
        {
            
        }

        private bool IsHungry()
        {
            return (_satiety == 0);
        }

        private void AddHealth(int delta)
        {
            _health += delta;
            if (_health > _maxHealth) _health = _maxHealth;
        }

        private void AddSatiety(int delta)
        {
            _satiety += delta;
            if (_satiety > _maxSatiety) _satiety = _maxSatiety;
        }

        private void Walk()
        {
            
        }

        private void SearchMeal()
        {
            
        }

        private void Step(Direction direction)
        {
            
        }

        private void Eat(Plant meal)
        {
            AddSatiety(meal.NutritionalValue);
            meal.Die();
        }

        private void Plant()
        {
            if (cell.CurrentObjects[0] != null) return;
            var chance = _satiety / 2;
            var random = World.Random.Next(0, _maxSatiety);
            if (random < chance)
                new Plant(cell);
        }

        private void Die()
        {
            
        }
    }
}