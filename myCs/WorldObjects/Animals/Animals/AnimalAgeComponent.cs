using System.Drawing;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Animals
{
    public class AnimalAgeComponent : AbstractAnimalAgeComponent
    {
        public AnimalAgeComponent(WorldObject owner, CreatureType creatureType, 
            Effect effect, bool isMale, Image image, int layer, int pregnantPeriod, 
            bool byEggs, int[] transAges = null) 
            : base(owner, creatureType, effect, isMale, image, layer, pregnantPeriod, byEggs, transAges)
        {
        }

        protected override void AddMaleComponent()
        {
            WorldObject.AddComponent(new AnimalMaleComponent(WorldObject));
        }

        protected override void AddFemaleComponent(bool byEggs, int pregnantPeriod)
        {
            WorldObject.AddComponent(new AnimalFemaleComponent(WorldObject, byEggs, pregnantPeriod));
        }
    }
}