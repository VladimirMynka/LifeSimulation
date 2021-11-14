using System.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
{
    public class Human : WorldObject
    {
        public Human(
            Cell keeper,
            Image image,
            Image afterDiedImage,
            int layer = 0,
            MealType mealType = MealType.AllTypes,
            bool isMale = true,
            int maxHealth = Defaults.AnimalHealth,
            int maxSatiety = Defaults.AnimalSatiety
            
        ) : base(keeper)
        {

            Start();
        }
    }
}