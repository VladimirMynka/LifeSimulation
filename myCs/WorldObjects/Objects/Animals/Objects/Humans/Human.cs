using System.Drawing;
using LifeSimulation.myCs.Drawing;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.DependingOnWeather;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Behaviour;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Moving;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.PetsOwner;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans
{
    public class Human : WorldObject
    {
        private Human(
            Cell keeper,
            Image image,
            Image afterDiedImage,
            int layer = 0,
            bool isMale = true,
            int maxHealth = Defaults.AnimalHealth,
            int regeneration = Defaults.AnimalHealthRegeneration,
            int satietyDestruction = Defaults.AnimalSatietyDestruction,
            int maxSatiety = Defaults.AnimalSatiety,
            int maxReserve = Defaults.AnimalSatiety,
            int visibility = Defaults.AnimalVisibleArea
            
        ) : base(keeper)
        {
            components.Add(new DrawableComponent(this, image, layer));
            components.Add(new InventoryComponent<Resource>(this, maxReserve));
            components.Add(new HumanEaterComponent(this, MealType.AllTypes, maxSatiety, satietyDestruction));
            components.Add(new HealthComponent(this, maxHealth, regeneration));
            components.Add(new VisibilityComponent(this, visibility));
            components.Add(new HumanAgeComponent(this, isMale, afterDiedImage, layer, 20));
            components.Add(new EatableComponent(this, CreatureType.Human, MealType.FreshMeat, Effect.None));
            components.Add(new MovingComponent(this));
            components.Add(new PetsOwnerComponent(this));
            
            components.Add(new BehaviourChangerComponent(this));
            components.Add(new InformationComponent(this));
            components.Add(new DependingOnWeatherComponent(this));

            components.Add(new InstrumentsOwnerComponent(this));
            components.Add(new WarehousesOwnerComponent(this));
            components.Add(new BuilderComponent(this));

            Start();
        }
        
        public static Human SpawnHumanWithRandomGender(Cell cell)
        {
            var isMale = (World.Random.Next(2) == 1);
            return SpawnHumanWithGender(cell, isMale);

        }
        public static Human SpawnHumanWithGender(Cell cell, bool isMale)
        {
            if (isMale)
                return new Human(cell, Pictures.Human, Pictures.Meat3,
                4, true, 500, 2, 1, 100, 300, 15);
            return new Human(cell, Pictures.Human2, Pictures.Meat3,
                4, false, 500, 2, 1, 100, 200, 15);
        }
    }
}