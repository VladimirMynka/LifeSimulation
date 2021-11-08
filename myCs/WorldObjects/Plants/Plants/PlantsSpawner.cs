using LifeSimulation.myCs.Drawer;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Plants.Plants
{
    public static class PlantsSpawner
    {
        public static Plant SpawnNormalPlant(Cell cell)
        {
            return new Plant(cell, Pictures.Plant, 1,Effect.None);
        }

        public static Plant SpawnUneatablePlant(Cell cell)
        {
            return new Plant(cell, Pictures.UneatablePlant, 1, Effect.Uneatable);
        }

        public static Plant SpawnPoisonousPlant(Cell cell)
        {
            return new Plant(cell, Pictures.PoisonousPlant, 1, Effect.Heart);
        }
    }
}