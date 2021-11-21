using System.Drawing;
using LifeSimulation.myCs.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Moving;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals.Components;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.PetsOwner;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals
{
    public class Animal : WorldObject
    {
        private Animal(
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
            int regeneration = Defaults.AnimalHealthRegeneration,
            int satietyDestruction = Defaults.AnimalSatietyDestruction,
            int visibility = Defaults.AnimalVisibleArea,
            WalkingState walkingState = WalkingState.UsualWalking,
            MovingToTargetState movingToTargetState = MovingToTargetState.UsualMoving,
            int pace = Defaults.TicksToStep,
            bool winterSleeper = false,
            bool isTamable = true,
            int presentsTimer = 15,
            PetEffect petEffect = PetEffect.AddMeal,
            int presentValue = 20
        ) : base(keeper)
        {
            components.Add(new DrawableComponent(this, image, layer));
            components.Add(new AnimalAgeComponent(this, creatureType, Effect.None, 
                isMale, afterDiedImage, layer, pregnantPeriod, byEggs));
            components.Add(new HealthComponent(this, maxHealth, regeneration));
            components.Add(new MovingComponent(this, walkingState, movingToTargetState, pace));
            components.Add(new VisibilityComponent(this, visibility));
            components.Add(new AnimalEaterComponent(this, mealType, maxSatiety, satietyDestruction));
            components.Add(new EatableComponent(this, creatureType, MealType.FreshMeat, Effect.None));

            if (mealType == MealType.Plant)
                components.Add(new PlanterComponent(this));
            if (winterSleeper)
                components.Add(new SleeperComponent(this));
            if (isTamable)
                components.Add(new PetComponent(this, presentsTimer, petEffect, presentValue));

            components.Add(new AnimalInformationComponent(this));
            components.Add(new DependingOnWeatherComponent(this));
            components.Add(new BehaviourChangerComponent(this));
            Start();

        }
        
        public static void SpawnRandomAnimal(Cell cell)
        {
            int randomTypeNumber = World.Random.Next((int) CreatureType.Herbivore1,
                (int) CreatureType.Scavenger3 + 1);
            SpawnWithRandomGender(cell, (CreatureType) randomTypeNumber);
        }
        
        public static void SpawnWithRandomGender(Cell cell, CreatureType creatureType)
        {
            var isMale = (World.Random.Next(2) == 1);
            SpawnWithGender(cell, creatureType, isMale);
        }

        public static void SpawnWithGender(Cell cell, CreatureType creatureType, bool isMale)
        {
            switch (creatureType)
            {
                case CreatureType.Herbivore1:
                    new Animal(cell, Pictures.Herbivore, Pictures.Meat, creatureType,
                        4, MealType.Plant, isMale, 0, false, 200, 200, 
                        2, 1, 6, WalkingState.UsualWalking, 
                        MovingToTargetState.OrthogonalMoving, 4, false,
                        true, 30, PetEffect.WarmClothes, 15);
					break;
                case CreatureType.Herbivore2:
                    new Animal(cell, Pictures.Herbivore2, Pictures.Meat3, creatureType,
                        4, MealType.Plant, isMale, 18, false, 150, 300, 
                        2, 1, 10,
                        WalkingState.LeftTopWalking, MovingToTargetState.SnakeMoving, 6);
					break;
                case CreatureType.Herbivore3:
                     new Animal(cell, Pictures.Herbivore3, Pictures.Meat2, creatureType,
                        4, MealType.Plant, isMale, 20, false, 300, 150, 
                        2, 1, 10,
                        WalkingState.NoSharpTurns, MovingToTargetState.UsualMoving, 5, true,
                        true, 20, PetEffect.AddMeal, 50);
					break;
                
                case CreatureType.Predator1:
                     new Animal(cell, Pictures.Predator, Pictures.Meat3, creatureType,
                        4, MealType.FreshMeat, isMale, 12, false, 200, 200, 
                        2, 1, 12,
                        WalkingState.NoSharpTurns, MovingToTargetState.UsualMoving, 3, false,
                        true, 20, PetEffect.Protection, 20);
					break;
                case CreatureType.Predator2:
                     new Animal(cell, Pictures.Predator2, Pictures.Meat3, creatureType,
                        4, MealType.FreshMeat, isMale, 16, false, 400, 80, 
                        2, 1, 10,
                        WalkingState.RightBottomWalking, MovingToTargetState.SnakeMoving, 2, false, 
                        false);
					break;
                case CreatureType.Predator3:
                     new Animal(cell, Pictures.Predator3, Pictures.Meat, creatureType,
                        4, MealType.FreshMeat, isMale, 12, false, 80, 400, 
                        2, 1, 6,
                        WalkingState.UsualWalking, MovingToTargetState.OrthogonalMoving, 4, true,
                        false);
					break;
                
                case CreatureType.Omnivore1:
                     new Animal(cell, Pictures.Omnivore, Pictures.Meat2, creatureType,
                        4, MealType.AllTypes, isMale, 20, false, 300, 100, 
                        2, 1, 8,
                        WalkingState.LeftTopWalking, MovingToTargetState.UsualMoving, 3, true,
                        true, 40, PetEffect.AddMeal, 100);
					break;
                case CreatureType.Omnivore2:
                     new Animal(cell, Pictures.Omnivore2, Pictures.Meat, creatureType,
                        4, MealType.AllTypes, isMale, 12, true, 150, 100, 
                        2, 1, 8,
                        WalkingState.UsualWalking, MovingToTargetState.SnakeMoving, 4, false,
                        false);
					break;
                case CreatureType.Omnivore3:
                     new Animal(cell, Pictures.Omnivore3, Pictures.Meat4, creatureType,
                        4, MealType.AllTypes, isMale, 12, true, 150, 160, 
                        2, 1, 10,
                        WalkingState.NoSharpTurns, MovingToTargetState.OrthogonalMoving, 2, false,
                        true, 30, PetEffect.WarmClothes, 20);
					break;
                
                case CreatureType.Scavenger1:
                     new Animal(cell, Pictures.Scavenger, Pictures.Meat4, creatureType,
                        4, MealType.DeadMeat, isMale, 12, true, 80, 100, 
                        2, 1, 12,
                        WalkingState.NoSharpTurns, MovingToTargetState.UsualMoving, 2, false,
                        true, 50, PetEffect.AddMeal, 20);
					break;
                case CreatureType.Scavenger2:
                     new Animal(cell, Pictures.Scavenger2, Pictures.Meat3, creatureType,
                        4, MealType.DeadMeat, isMale, 12, true, 150, 100, 
                        2, 1, 6,
                        WalkingState.LeftTopWalking, MovingToTargetState.SnakeMoving, 6, false,
                        false);
					break;
                case CreatureType.Scavenger3:
                     new Animal(cell, Pictures.Scavenger3, Pictures.Meat3, creatureType,
                        4, MealType.DeadMeat, isMale, 12, true, 300, 80, 
                        2, 1, 6,
                        WalkingState.UsualWalking, MovingToTargetState.OrthogonalMoving, 6, true,
                        false);
                     break;
                case CreatureType.Human:
                    Human.SpawnHumanWithGender(cell, isMale);
                    break;
            }
        }
    }
}