using System.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.RotMeat.Components;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.RotMeat
{
    public class RotMeat : WorldObject
    {
        public RotMeat(
            Cell keeper,
            Image image,
            CreatureType creatureType,
            int layer = 0
        ) : base(keeper)
        {
            components.Add(new DrawableComponent(this, image, layer));
            components.Add(new RotMeatComponent(this, Defaults.AnimalRotAge - Defaults.AnimalDiedAge, creatureType));
            components.Add(new EatableComponent(this, creatureType, MealType.DeadMeat, Effect.None));
            components.Add(new InformationComponent(this));
            Start();
        }
    }
}