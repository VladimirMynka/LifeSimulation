using System.Drawing;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals.Components.Mating;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals.Components
{
    public class AnimalAgeComponent : AbstractAnimalAgeComponent
    {
        public AnimalAgeComponent(WorldObject owner, CreatureType creatureType, 
            Effect effect, bool isMale, Image image, int layer, int pregnantPeriod, 
            bool byEggs, int[] transAges = null) 
            : base(owner, creatureType, effect, isMale, image, layer, pregnantPeriod, byEggs, transAges)
        {
        }

        protected override MatingComponent NewMaleComponent()
        {
            return new AnimalMaleComponent(WorldObject);
        }

        protected override MatingComponent NewFemaleComponent(bool byEggs, int pregnantPeriod)
        {
            return new AnimalFemaleComponent(WorldObject, byEggs, pregnantPeriod);
        }
    }
}