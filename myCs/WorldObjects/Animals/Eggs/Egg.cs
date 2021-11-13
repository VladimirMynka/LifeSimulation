using System.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Eggs
{
    public class Egg : WorldObject
    {
        public Egg(
            Cell keeper,
            Image image,
            CreatureType creatureType,
            int layer = 0,
            int ticksToBirthday = Defaults.AnimalEggPeriod
            ) : base(keeper)
        {
            components.Add(new DrawableComponent(this, image, layer));
            components.Add(new EggComponent(this, creatureType, ticksToBirthday));
            components.Add(new EatableComponent(this, creatureType, MealType.FreshMeat, Effect.None));
            components.Add(new EggInformationComponent(this));
            
            Start();
        }
    }
}