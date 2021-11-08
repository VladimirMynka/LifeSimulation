using LifeSimulation.myCs.Drawer;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Plants.Fruits
{
    public static class FruitsSpawner
    {
        public static Fruit SpawnNormalFruit(Cell cell)
        {
            return new Fruit(cell, Pictures.Fruit, 2, Effect.None);
        }

        public static Fruit SpawnUneatableFruit(Cell cell)
        {
            return new Fruit(cell, Pictures.Fruit, 2, Effect.Uneatable);
        }

        public static Fruit SpawnPoisonousFruit(Cell cell)
        {
            return new Fruit(cell, Pictures.Fruit, 2, Effect.Heart);
        }
    }
}