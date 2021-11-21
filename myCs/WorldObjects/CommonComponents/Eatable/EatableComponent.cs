using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable
{
    public class EatableComponent : WorldObjectComponent
    {
        public MealType MealType;
        public readonly CreatureType CreatureType;
        private readonly Effect _effect;
        public readonly int NutritionalValue;

        public EatableComponent(WorldObject owner, 
            CreatureType creatureType, 
            MealType mealType,  
            Effect effect,
            int nutritionalValue = Defaults.NutritionalValue) : base(owner)
        {
            CreatureType = creatureType;
            MealType = mealType;
            _effect = effect;
            NutritionalValue = nutritionalValue;
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
            eater.AddSatiety(NutritionalValue);
        }
        
        private void Damage(EaterComponent eater)
        {
            if (CheckWereDestroyed(eater))
                return;
            eater.GetComponent<HealthComponent>().AddHealth(-NutritionalValue / 2);
        }

        public bool IsPoisonous()
        {
            return _effect != Effect.None;
        }
    }
}