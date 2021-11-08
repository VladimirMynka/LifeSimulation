using System.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public class Egg : WorldObject
    {
        public Egg(
            Cell keeper,
            MealType afterBirthdayType,
            Image image,
            int layer = 0,
            int ticksToBirthday = Defaults.AnimalEggPeriod
            ) : base(keeper)
        {
            components.Add(new DrawableComponent(this, image, layer));
            components.Add(new EggComponent(this, ticksToBirthday, afterBirthdayType));
            components.Add(new EatableComponent(this, MealType.FreshMeat, Effect.None));
            
            Start();

        }
    }
}