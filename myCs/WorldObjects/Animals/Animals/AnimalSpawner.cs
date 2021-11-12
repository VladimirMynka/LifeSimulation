using LifeSimulation.myCs.Drawer;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Animals
{
    public static class AnimalsSpawner
    {
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
                case CreatureType.Herbivore2:
                case CreatureType.Herbivore3:
                    return new Animal(cell, Pictures.Herbivore, Pictures.Meat, creatureType,
                        4, MealType.Plant, isMale, 100, 100);
                case CreatureType.Predator1:
                case CreatureType.Predator2:
                case CreatureType.Predator3:
                    return new Animal(cell, Pictures.Predator, Pictures.Meat, creatureType,
                        4, MealType.FreshMeat, isMale, 150, 80);
                case CreatureType.Omnivore1:
                case CreatureType.Omnivore2:
                case CreatureType.Omnivore3:
                    return new Animal(cell, Pictures.Omnivore, Pictures.Meat, creatureType,
                        4, MealType.AllTypes, isMale, 80, 150);
                case CreatureType.Scavenger1:
                case CreatureType.Scavenger2:
                case CreatureType.Scavenger3:
                    return new Animal(cell, Pictures.Scavenger, Pictures.Meat, creatureType,
                        4, MealType.DeadMeat, isMale, 50, 200);
                default:
                    return null;
            }
        }
        
        
    }
}