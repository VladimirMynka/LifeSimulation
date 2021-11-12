using System.Drawing;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Plants.Plants
{
    public class Plant : WorldObject
    {
        public Plant(
            Cell keeper, 
            Image image,
            CreatureType creatureType,
            int layer = 0,
            Effect effect = Effect.None, 
            int[] transAges = null) 
            : base(keeper)
        {
            components.Add(new PlantAgeComponent(this, creatureType, effect, image, layer, transAges));
            Start();
        }
    }
}