using System.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Animals
{
    public class Animal : WorldObject
    {
        public Animal(
            Cell keeper,
            Image image,
            Image afterDiedImage,
            CreatureType creatureType,
            int layer = 0,
            MealType mealType = MealType.AllTypes,
            bool isMale = true,
            int pregnantPeriod = 0,
            bool byEggs = false,
            int maxHealth = Defaults.AnimalHealth,
            int maxSatiety = Defaults.AnimalSatiety,
            WalkingState walkingState = WalkingState.UsualWalking,
            MovingToTargetState movingToTargetState = MovingToTargetState.UsualMoving,
            int pace = Defaults.TicksToStep
        ) : base(keeper)
        {
            components.Add(new DrawableComponent(this, image, layer));
            components.Add(new AnimalAgeComponent(this, creatureType, Effect.None, 
                isMale, afterDiedImage, layer, pregnantPeriod, byEggs));
            components.Add(new MovingComponent(this, walkingState, movingToTargetState, pace));
            components.Add(new HealthComponent(this, maxHealth));
            components.Add(new EaterComponent(this, mealType, maxSatiety));
            components.Add(new EatableComponent(this, creatureType, MealType.FreshMeat, Effect.None));

            if (mealType == MealType.Plant)
                components.Add(new PlanterComponent(this));

            components.Add(new AnimalInformationComponent(this));
            Start();

        }
    }
}