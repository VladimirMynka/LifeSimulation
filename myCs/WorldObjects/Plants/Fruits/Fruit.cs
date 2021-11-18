using System.Drawing;
using LifeSimulation.myCs.Drawer;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Eatable;
using LifeSimulation.myCs.WorldObjects.Plants.Plants;

namespace LifeSimulation.myCs.WorldObjects.Plants.Fruits
{
    public class Fruit : WorldObject
    {
        private Fruit(
            Cell keeper, 
            Image image,
            int layer,
            CreatureType creatureType,
            Effect effect = Effect.None,
            int[] transAges = null) 
            : base(keeper)
        {
            components.Add(new FruitAgeComponent(this, creatureType, effect, image, layer, transAges));
            components.Add(new RotComponent(this, creatureType));
            components.Add(new PlantOnWeatherComponent(this, false));
            components.Add(new DependingOnWeatherComponent(this));
            components.Add(new FruitInformationComponent(this));
            Start();
        }
        
        public static Fruit SpawnFruitByType(Cell cell, CreatureType creatureType)
        {
            switch (creatureType)
            {
                case CreatureType.EatableGreenPlant:
                    return new Fruit(cell, Pictures.Fruit, 3, creatureType, Effect.None);
                case CreatureType.PoisonousPurplePlant:
                    return new Fruit(cell, Pictures.Fruit, 3, creatureType, Effect.Heart);
                case CreatureType.UneatableBrownPlant:
                    return new Fruit(cell, Pictures.Fruit, 3, creatureType, Effect.Uneatable);
                default:
                    return null;
            }
        }
    }
}