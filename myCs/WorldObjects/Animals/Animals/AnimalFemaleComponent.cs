using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Animals.Mating;

namespace LifeSimulation.myCs.WorldObjects.Animals.Animals
{
    public class AnimalFemaleComponent : FemaleMatingComponent
    {
        public AnimalFemaleComponent(WorldObject owner, 
            bool byEggs = true, 
            int pregnantPeriod = Defaults.PregnantPeriod, 
            int ticksToMating = Defaults.AnimalNormalTicksToMating) 
            : base(owner, byEggs, pregnantPeriod, ticksToMating)
        {
        }
    }
}