using System.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public class Animal : WorldObject
    {
        public Animal(
            Cell keeper,
            Image image,
            Image afterDiedImage,
            int layer = 0,
            MealType mealType = MealType.AllTypes,
            bool isMale = true,
            int maxHealth = Defaults.AnimalHealth,
            int maxSatiety = Defaults.AnimalSatiety
            
        ) : base(keeper)
        {
            components.Add(new DrawableComponent(this, image, layer));
            components.Add(new AnimalAgeComponent(this, Effect.None, isMale, afterDiedImage, layer));
            components.Add(new MovingComponent(this));
            components.Add(new HealthComponent(this, maxHealth));
            components.Add(new EaterComponent(this, mealType, maxSatiety));
            components.Add(new EatableComponent(this, MealType.FreshMeat, Effect.None));

            if (mealType == MealType.Plant)
                components.Add(new PlanterComponent(this));
            
            Start();

        }
    }
}