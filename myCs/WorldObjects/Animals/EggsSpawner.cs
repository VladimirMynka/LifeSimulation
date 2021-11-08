using LifeSimulation.myCs.Drawer;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public static class EggsSpawner
    {
        public static Egg SpawnOmnivoreEgg(Cell cell)
        {
            return new Egg(cell, MealType.AllTypes, Pictures.Egg, 3);
        }

        public static Egg SpawnPredatorEgg(Cell cell)
        {
            return new Egg(cell, MealType.FreshMeat, Pictures.Egg, 3);
        }

        public static Egg SpawnHerbivoreEgg(Cell cell)
        {
            return new Egg(cell, MealType.Plant, Pictures.Egg, 3);
        }

        public static Egg SpawnScavengerEgg(Cell cell)
        {
            return new Egg(cell, MealType.DeadMeat, Pictures.Egg, 3);
        }
    }
}