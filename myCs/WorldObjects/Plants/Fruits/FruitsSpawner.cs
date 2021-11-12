using LifeSimulation.myCs.Drawer;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Plants.Fruits
{
    public static class FruitsSpawner
    {
        public static Fruit SpawnFruitByType(Cell cell, CreatureType creatureType)
        {
            switch (creatureType)
            {
                case CreatureType.EatableGreenPlant:
                    return new Fruit(cell, Pictures.Fruit, 3, creatureType, Effect.None);
                case CreatureType.PoisonousPurplePlant:
                    return new Fruit(cell, Pictures.Fruit, 3, creatureType, Effect.Heart);
                case CreatureType.UneatableBrownPlant:
                    return new Fruit(cell, Pictures.Fruit, 3, creatureType, Effect.Uneatable);
                default:
                    return null;
            }
        }
    }
}