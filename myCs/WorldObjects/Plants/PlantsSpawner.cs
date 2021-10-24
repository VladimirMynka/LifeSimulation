using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects.Plants
{
    public static class PlantsSpawner
    {
        public static Plant SpawnNormalPlant(Cell cell)
        {
            return new Plant(cell, Effect.None, Colors.Plant1Const);
        }

        public static Plant SpawnUneatablePlant(Cell cell)
        {
            return new Plant(cell, Effect.Uneatable, Colors.UneatablePlant1Const);
        }

        public static Plant SpawnPoisonousPlant(Cell cell)
        {
            return new Plant(cell, Effect.Heart, Colors.PoisonousPlant1Const);
        }
    }
}