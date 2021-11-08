using System.Drawing;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Plants.Fruits
{
    public class Fruit : WorldObject
    {
        public Fruit(
            Cell keeper, 
            Image image,
            int layer,
            Effect effect = Effect.None,
            int[] transAges = null) 
            : base(keeper)
        {
            components.Add(new FruitAgeComponent(this, effect, image, layer, transAges));
            components.Add(new RotComponent(this));
            Start();
        }
    }
}