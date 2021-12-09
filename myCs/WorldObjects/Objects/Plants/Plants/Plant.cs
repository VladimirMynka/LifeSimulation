using System.Drawing;
using LifeSimulation.myCs.Drawing;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.DependingOnWeather;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Plants.Plants
{
    public class Plant : WorldObject
    {
        private Plant(
            Cell keeper, 
            Image image,
            CreatureType creatureType,
            int layer = 0,
            Effect effect = Effect.None, 
            bool isAnnual = false,
            int[] transAges = null) 
            : base(keeper)
        {
            components.Add(new PlantAgeComponent(this, creatureType, effect, image, layer, transAges));
            components.Add(new PlantOnWeatherComponent(this, isAnnual));
            components.Add(new DependingOnWeatherComponent(this));
            components.Add(new InformationComponent(this));
            Start();
        }
        
        public static Plant SpawnRandomPlant(Cell cell)
        {
            int randomTypeNumber = World.Random.Next((int) CreatureType.EatableGreenPlant,
                (int) CreatureType.UneatableBrownPlant + 1);
            return SpawnPlantByType(cell, (CreatureType) randomTypeNumber);
        }
        public static Plant SpawnPlantByType(Cell cell, CreatureType creatureType)
        {
            switch (creatureType)
            {
                case CreatureType.EatableGreenPlant:
                    return new Plant(cell, Pictures.Plant, creatureType, 2, Effect.None, true);
                case CreatureType.PoisonousPurplePlant:
                    return new Plant(cell, Pictures.PoisonousPlant, creatureType, 2, Effect.Heart, true);
                case CreatureType.UneatableBrownPlant:
                    return new Plant(cell, Pictures.UneatablePlant, creatureType, 2, Effect.Uneatable, false);
                default:
                    return null;
            }
        }
    }
}