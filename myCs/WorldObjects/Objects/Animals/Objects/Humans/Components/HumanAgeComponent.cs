using System.Drawing;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Mating;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components
{
    public class HumanAgeComponent : AbstractAnimalAgeComponent
    {
        public HumanAgeComponent(WorldObject owner, bool isMale, Image image, 
            int layer, int pregnantPeriod, int[] transAges = null) 
            : base(owner, CreatureType.Human, Effect.None, isMale, image, layer, 
                pregnantPeriod, false, transAges)
        {
        }

        protected override void AddMaleComponent()
        {
            WorldObject.AddComponent(new ManComponent(WorldObject));
        }

        protected override void AddFemaleComponent(bool byEggs, int pregnantPeriod)
        {
            WorldObject.AddComponent(new WomanComponent(WorldObject, pregnantPeriod));
        }
    }
}