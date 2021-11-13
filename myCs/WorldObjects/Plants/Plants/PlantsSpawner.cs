using LifeSimulation.myCs.Drawer;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Plants.Plants
{
    public static class PlantsSpawner
    {
        public static Plant SpawnRandomPlant(Cell cell)
        {
            int randomTypeNumber = World.World.Random.Next((int) CreatureType.EatableGreenPlant,
                (int) CreatureType.UneatableBrownPlant + 1);
            return SpawnPlantByType(cell, (CreatureType) randomTypeNumber);
        }
        public static Plant SpawnPlantByType(Cell cell, CreatureType creatureType)
        {
            switch (creatureType)
            {
                case CreatureType.EatableGreenPlant:
                    return new Plant(cell, Pictures.Plant, creatureType, 2, Effect.None);
                case CreatureType.PoisonousPurplePlant:
                    return new Plant(cell, Pictures.PoisonousPlant, creatureType, 2, Effect.Heart);
                case CreatureType.UneatableBrownPlant:
                    return new Plant(cell, Pictures.UneatablePlant, creatureType, 2, Effect.Uneatable);
                default:
                    return null;
            }
        }
    }
}