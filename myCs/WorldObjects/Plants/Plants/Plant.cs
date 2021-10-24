using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects.Plants
{
    public class Plant : WorldObject
    {
        public Plant(Cell keeper, Effect effect = Effect.None, int color = Colors.Plant1Const, int[] transAges = null) 
            : base(keeper, color)
        {
            components.Add(new PlantAgeComponent(this, effect, transAges));
            Start();
        }
    }
}