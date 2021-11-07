using LifeSimulation.myCs.Drawer;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public static class AnimalsSpawner
    {
        public static Animal SpawnOmnivoreAnimalMale(Cell cell)
        {
            return new Animal(cell, Pictures.Animal, 3, MealType.AllTypes, true);
        }
        
        public static Animal SpawnOmnivoreAnimalFemale(Cell cell)
        {
            return new Animal(cell, Pictures.Animal, 3, MealType.AllTypes, false);
        }
        
        public static Animal SpawnPredatorAnimalMale(Cell cell)
        {
            return new Animal(cell, Pictures.Animal, 3, MealType.AllTypes, true);
        }
        
        public static Animal SpawnPredatorAnimalFemale(Cell cell)
        {
            return new Animal(cell, Pictures.Animal, 3, MealType.FreshMeat, false);
        }
        
        public static Animal SpawnHerbivoreAnimalMale(Cell cell)
        {
            return new Animal(cell, Pictures.Animal, 3, MealType.FreshMeat, true);
        }
        
        public static Animal SpawnHerbivoreAnimalFemale(Cell cell)
        {
            return new Animal(cell, Pictures.Animal, 3, MealType.AllTypes, false);
        }
        
        public static Animal SpawnScavengerAnimalMale(Cell cell)
        {
            return new Animal(cell, Pictures.Animal, 3, MealType.AllTypes, false);
        }
        
        public static Animal SpawnScavengerAnimalFemale(Cell cell)
        {
            return new Animal(cell, Pictures.Animal, 3, MealType.AllTypes, true);
        }
        
        public static Animal SpawnOmnivoreAnimal(Cell cell, bool isMale)
        {
            return new Animal(cell, Pictures.Animal, 3, MealType.AllTypes, isMale);
        }
        
        public static Animal SpawnPredatorAnimal(Cell cell, bool isMale)
        {
            return new Animal(cell, Pictures.Animal, 3, MealType.AllTypes, isMale);
        }
        
        public static Animal SpawnHerbivoreAnimal(Cell cell, bool isMale)
        {
            return new Animal(cell, Pictures.Animal, 3, MealType.FreshMeat, isMale);
        }

        public static Animal SpawnScavengerAnimal(Cell cell, bool isMale)
        {
            return new Animal(cell, Pictures.Animal, 3, MealType.AllTypes, isMale);
        }
    }
}