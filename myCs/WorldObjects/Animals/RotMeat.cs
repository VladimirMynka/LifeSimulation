using System.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public class RotMeat : WorldObject
    {
        public RotMeat(
            Cell keeper,
            Image image,
            int layer = 0
        ) : base(keeper)
        {
            components.Add(new DrawableComponent(this, image, layer));
            components.Add(new RotMeatComponent(this, Defaults.AnimalRotAge));
            components.Add(new EatableComponent(this, MealType.DeadMeat, Effect.None));
            
            Start();

        }
    }
}