using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public static class AnimalsSpawner
    {
        public static Animal SpawnOmnivoreAnimalMale(Cell cell)
        {
            return new Animal(cell, Colors.Animal1Const, MealType.AllTypes, true);
        }
        
        public static Animal SpawnOmnivoreAnimalFemale(Cell cell)
        {
            return new Animal(cell, Colors.Animal1Const, MealType.AllTypes, false);
        }
        
        public static Animal SpawnPredatorAnimalMale(Cell cell)
        {
            return new Animal(cell, Colors.Animal2Const, MealType.AllTypes, true);
        }
        
        public static Animal SpawnPredatorAnimalFemale(Cell cell)
        {
            return new Animal(cell, Colors.Animal2Const, MealType.FreshMeat, false);
        }
        
        public static Animal SpawnHerbivoreAnimalMale(Cell cell)
        {
            return new Animal(cell, Colors.Animal3Const, MealType.FreshMeat, true);
        }
        
        public static Animal SpawnHerbivoreAnimalFemale(Cell cell)
        {
            return new Animal(cell, Colors.Animal1Const, MealType.AllTypes, false);
        }
        
        public static Animal SpawnScavengerAnimalMale(Cell cell)
        {
            return new Animal(cell, Colors.Animal4Const, MealType.AllTypes, false);
        }
        
        public static Animal SpawnScavengerAnimalFemale(Cell cell)
        {
            return new Animal(cell, Colors.Animal4Const, MealType.AllTypes, true);
        }
        
        public static Animal SpawnOmnivoreAnimal(Cell cell, bool isMale)
        {
            return new Animal(cell, Colors.Animal1Const, MealType.AllTypes, isMale);
        }
        
        public static Animal SpawnPredatorAnimal(Cell cell, bool isMale)
        {
            return new Animal(cell, Colors.Animal2Const, MealType.AllTypes, isMale);
        }
        
        public static Animal SpawnHerbivoreAnimal(Cell cell, bool isMale)
        {
            return new Animal(cell, Colors.Animal3Const, MealType.FreshMeat, isMale);
        }

        public static Animal SpawnScavengerAnimal(Cell cell, bool isMale)
        {
            return new Animal(cell, Colors.Animal4Const, MealType.AllTypes, isMale);
        }
    }
}