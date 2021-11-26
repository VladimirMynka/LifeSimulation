using System.Drawing;
using LifeSimulation.myCs.Drawing;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.Objects.Plants.Plants;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Plants.Fruits
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
            components.Add(new InformationComponent(this));
            Start();
        }
        
        public static Fruit SpawnFruitByType(Cell cell, CreatureType creatureType)
        {
            switch (creatureType)
            {
                case CreatureType.EatableGreenPlant:
                    return new Fruit(cell, Pictures.Fruit, 3, creatureType, Effect.None);
                case CreatureType.PoisonousPurplePlant:
                    return new Fruit(cell, Pictures.PoisonousFruit, 3, creatureType, Effect.Heart);
                case CreatureType.UneatableBrownPlant:
                    return new Fruit(cell, Pictures.UneatableFruit, 3, creatureType, Effect.Uneatable);
                default:
                    return null;
            }
        }
    }
}