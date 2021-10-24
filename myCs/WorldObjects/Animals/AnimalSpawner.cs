using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public static class AnimalsSpawner
    {
        public static Animal SpawnNormalAnimal(Cell cell)
        {
            return new Animal(cell);
        }
    }
}