using System.Drawing;
using LifeSimulation.myCs.Drawer;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Animals
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
            bool winterSleeper = false
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

            components.Add(new AnimalInformationComponent(this));
            components.Add(new DependingOnWeatherComponent(this));
            Start();

        }
        
        public static Animal SpawnRandomAnimal(Cell cell)
        {
            int randomTypeNumber = World.World.Random.Next((int) CreatureType.Herbivore1,
                (int) CreatureType.Scavenger3 + 1);
            return SpawnWithRandomGender(cell, (CreatureType) randomTypeNumber);
        }
        
        public static Animal SpawnWithRandomGender(Cell cell, CreatureType creatureType)
        {
            var isMale = (World.World.Random.Next(2) == 1);
            return SpawnWithGender(cell, creatureType, isMale);
        }

        public static Animal SpawnWithGender(Cell cell, CreatureType creatureType, bool isMale)
        {
            switch (creatureType)
            {
                case CreatureType.Herbivore1:
                    return new Animal(cell, Pictures.Herbivore, Pictures.Meat, creatureType,
                        4, MealType.Plant, isMale, 0, false, 200, 200, 
                        2, 1, 6, WalkingState.UsualWalking, 
                        MovingToTargetState.OrthogonalMoving, 4);
                case CreatureType.Herbivore2:
                    return new Animal(cell, Pictures.Herbivore2, Pictures.Meat3, creatureType,
                        4, MealType.Plant, isMale, 18, false, 150, 300, 
                        2, 1, 10,
                        WalkingState.LeftTopWalking, MovingToTargetState.SnakeMoving, 6);
                case CreatureType.Herbivore3:
                    return new Animal(cell, Pictures.Herbivore3, Pictures.Meat2, creatureType,
                        4, MealType.Plant, isMale, 20, false, 300, 150, 
                        2, 1, 10,
                        WalkingState.NoSharpTurns, MovingToTargetState.UsualMoving, 5, true);
                
                case CreatureType.Predator1:
                    return new Animal(cell, Pictures.Predator, Pictures.Meat3, creatureType,
                        4, MealType.FreshMeat, isMale, 12, false, 200, 200, 
                        2, 1, 12,
                        WalkingState.NoSharpTurns, MovingToTargetState.UsualMoving, 3);
                case CreatureType.Predator2:
                    return new Animal(cell, Pictures.Predator2, Pictures.Meat3, creatureType,
                        4, MealType.FreshMeat, isMale, 16, false, 400, 80, 
                        2, 1, 10,
                        WalkingState.RightBottomWalking, MovingToTargetState.SnakeMoving, 2);
                case CreatureType.Predator3:
                    return new Animal(cell, Pictures.Predator3, Pictures.Meat, creatureType,
                        4, MealType.FreshMeat, isMale, 12, false, 80, 400, 
                        2, 1, 6,
                        WalkingState.UsualWalking, MovingToTargetState.OrthogonalMoving, 4, true);
                
                case CreatureType.Omnivore1:
                    return new Animal(cell, Pictures.Omnivore, Pictures.Meat2, creatureType,
                        4, MealType.AllTypes, isMale, 20, false, 300, 100, 
                        2, 1, 8,
                        WalkingState.LeftTopWalking, MovingToTargetState.UsualMoving, 3, true);
                case CreatureType.Omnivore2:
                    return new Animal(cell, Pictures.Omnivore2, Pictures.Meat, creatureType,
                        4, MealType.AllTypes, isMale, 12, true, 150, 100, 
                        2, 1, 8,
                        WalkingState.UsualWalking, MovingToTargetState.SnakeMoving, 4);
                case CreatureType.Omnivore3:
                    return new Animal(cell, Pictures.Omnivore3, Pictures.Meat4, creatureType,
                        4, MealType.AllTypes, isMale, 12, true, 150, 160, 
                        2, 1, 10,
                        WalkingState.NoSharpTurns, MovingToTargetState.OrthogonalMoving, 2);
                
                case CreatureType.Scavenger1:
                    return new Animal(cell, Pictures.Scavenger, Pictures.Meat4, creatureType,
                        4, MealType.DeadMeat, isMale, 12, true, 80, 100, 
                        2, 1, 12,
                        WalkingState.NoSharpTurns, MovingToTargetState.UsualMoving, 2);
                case CreatureType.Scavenger2:
                    return new Animal(cell, Pictures.Scavenger2, Pictures.Meat3, creatureType,
                        4, MealType.DeadMeat, isMale, 12, true, 150, 100, 
                        2, 1, 6,
                        WalkingState.LeftTopWalking, MovingToTargetState.SnakeMoving, 6);
                case CreatureType.Scavenger3:
                    return new Animal(cell, Pictures.Scavenger3, Pictures.Meat3, creatureType,
                        4, MealType.DeadMeat, isMale, 12, true, 300, 80, 
                        2, 1, 6,
                        WalkingState.UsualWalking, MovingToTargetState.OrthogonalMoving, 6, true);
                default:
                    return null;
            }
        }
    }
}