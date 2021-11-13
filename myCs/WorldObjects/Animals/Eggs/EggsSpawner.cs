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
                    return SpawnHerbivoreEgg(cell);
                case CreatureType.Omnivore1:
                    return SpawnOmnivoreEgg(cell);
                case CreatureType.Predator1:
                    return SpawnPredatorEgg(cell);
                case CreatureType.Scavenger1:
                    return SpawnScavengerEgg(cell);
                default:
                    return null;
            }
        }
        
        public static Egg SpawnOmnivoreEgg(Cell cell)
        {
            return new Egg(cell, Pictures.Egg, CreatureType.Omnivore1, 3, 30);
        }

        public static Egg SpawnPredatorEgg(Cell cell)
        {
            return new Egg(cell, Pictures.Egg, CreatureType.Predator1, 3, 50);
        }

        public static Egg SpawnHerbivoreEgg(Cell cell)
        {
            return new Egg(cell, Pictures.Egg, CreatureType.Herbivore1, 3, 10);
        }

        public static Egg SpawnScavengerEgg(Cell cell)
        {
            return new Egg(cell, Pictures.Egg, CreatureType.Scavenger1, 3, 20);
        }
    }
}