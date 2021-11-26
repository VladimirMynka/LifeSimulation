using System.Drawing;
using LifeSimulation.myCs.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Eggs.Components;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Eggs
{
    public class Egg : WorldObject
    {
        private Egg(
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
            components.Add(new InformationComponent(this));
            
            Start();
        }
        
        public static Egg SpawnEggByType(Cell cell, CreatureType creatureType)
        {
            switch (creatureType)
            {
                case CreatureType.Herbivore1:
                case CreatureType.Herbivore2:
                case CreatureType.Herbivore3:
                    return new Egg(cell, Pictures.Egg, creatureType, 3, 20);
                case CreatureType.Omnivore1:
                case CreatureType.Omnivore2:
                case CreatureType.Omnivore3:
                    return new Egg(cell, Pictures.Egg, creatureType, 3, 10);
                case CreatureType.Predator1:
                case CreatureType.Predator2:
                case CreatureType.Predator3:
                    return new Egg(cell, Pictures.Egg, creatureType, 3, 20);
                case CreatureType.Scavenger1:
                case CreatureType.Scavenger2:
                case CreatureType.Scavenger3:
                    return new Egg(cell, Pictures.Egg, creatureType, 3, 15);

                default:
                    return null;
            }
        }

    }
}