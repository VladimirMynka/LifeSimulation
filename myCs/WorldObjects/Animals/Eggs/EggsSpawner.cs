using LifeSimulation.myCs.Drawer;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects.Animals.Eggs
{
    public static class EggsSpawner
    {
        public static Egg SpawnEggByType(Cell cell, CreatureType creatureType)
        {
            switch (creatureType)
            {
                case CreatureType.Herbivore1:
                case CreatureType.Herbivore2:
                case CreatureType.Herbivore3:
                    return new Egg(cell, Pictures.Egg, creatureType, 3, 20);
                case CreatureType.Omnivore1:
                case CreatureType.Omnivore2:
                case CreatureType.Omnivore3:
                    return new Egg(cell, Pictures.Egg, creatureType, 3, 10);
                case CreatureType.Predator1:
                case CreatureType.Predator2:
                case CreatureType.Predator3:
                    return new Egg(cell, Pictures.Egg, creatureType, 3, 20);
                case CreatureType.Scavenger1:
                case CreatureType.Scavenger2:
                case CreatureType.Scavenger3:
                    return new Egg(cell, Pictures.Egg, creatureType, 3, 15);

                default:
                    return null;
            }
        }
    }
}