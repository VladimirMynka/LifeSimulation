using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects.Plants
{
    public class Fruit : WorldObject
    {
        public Fruit(Cell keeper, Effect effect = Effect.None, int color = Colors.NormalFruit1Const, int[] transAges = null) 
            : base(keeper, color)
        {
            components.Add(new FruitAgeComponent(this, effect, transAges));
            components.Add(new RotComponent(this));
            Start();
        }
    }
}