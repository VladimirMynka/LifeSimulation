using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects.Plants
{
    public static class FruitsSpawner
    {
        public static Fruit SpawnNormalFruit(Cell cell)
        {
            return new Fruit(cell, Effect.None, Colors.NormalFruit1Const);
        }

        public static Fruit SpawnUneatableFruit(Cell cell)
        {
            return new Fruit(cell, Effect.Uneatable, Colors.UneatableFruit1Const);
        }

        public static Fruit SpawnPoisonousFruit(Cell cell)
        {
            return new Fruit(cell, Effect.Heart, Colors.PoisonousFruit1Const);
        }
    }
}