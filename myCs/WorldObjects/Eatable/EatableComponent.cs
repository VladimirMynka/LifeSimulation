using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Animals;

namespace LifeSimulation.myCs.WorldObjects.Eatable
{
    public class EatableComponent : WorldObjectComponent
    {
        public MealType MealType;
        public readonly CreatureType CreatureType;
        private readonly Effect _effect;

        public EatableComponent(WorldObject owner, CreatureType creatureType, MealType mealType,  Effect effect) : base(owner)
        {
            CreatureType = creatureType;
            MealType = mealType;
            _effect = effect;
        }

        public void BeEatenBy(EaterComponent eater)
        {
            ApplyEffect(eater);
            WorldObject.Destroy();
        }

        private void ApplyEffect(EaterComponent target)
        {
            switch (_effect)
            {
                case Effect.None:
                    AddSatiety(target);
                    return; 
                case Effect.Heart:
                    Damage(target);
                    return;
            }
        }

        private void AddSatiety(EaterComponent eater)
        {
            eater.AddSatiety(Defaults.NutritionalValue);
        }

        private void Damage(EaterComponent eater)
        {
            if (eater == null)
                return;
            eater.GetComponent<HealthComponent>().AddHealth(-Defaults.PoisonDamage);
        }

        public bool IsPoisonous()
        {
            return _effect != Effect.None;
        }
    }
}