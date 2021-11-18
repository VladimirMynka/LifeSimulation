using System.Drawing;
using LifeSimulation.myCs.Drawer;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
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
            components.Add(new InventoryComponent(this, maxReserve));
            components.Add(new HumanEaterComponent(this, MealType.AllTypes, maxSatiety, satietyDestruction));
            components.Add(new HealthComponent(this, maxHealth, regeneration));
            components.Add(new VisibilityComponent(this, visibility));
            components.Add(new HumanAgeComponent(this, isMale, afterDiedImage, layer, 20));
            components.Add(new EatableComponent(this, CreatureType.Human, MealType.FreshMeat, Effect.None));
            components.Add(new MovingComponent(this));
            components.Add(new HumanInformationComponent(this));
            Start();
        }
        
        public static Human SpawnHumanWithRandomGender(Cell cell)
        {
            var isMale = (World.World.Random.Next(2) == 1);
            return SpawnHumanWithGender(cell, isMale);

        }
        public static Human SpawnHumanWithGender(Cell cell, bool isMale)
        {
            return new Human(cell, Pictures.Human, Pictures.Meat3,
                4, isMale, 500, 2, 1, 100, 300, 10);
        }
    }
}