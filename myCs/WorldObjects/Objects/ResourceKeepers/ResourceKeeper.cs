using System.Drawing;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.Instruments;
using LifeSimulation.myCs.Resources.UneatableResources;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.ResourceKeepers
{
    public abstract class ResourceKeeper<T> : WorldObject where T : Resource, new()
    {
        protected ResourceKeeper(
            Cell keeper, 
            Image[] images,
            int layer,
            int count,
            InstrumentType[] instrumentTypes
        )            
            : base(keeper)
        {
            components.Add(new DrawableComponent(this, images[0], layer));
            components.Add(new ResourceKeeperComponent<T>(this, new T(), count, instrumentTypes));
        }

        /*public static void Create(int count)
        {
            var random = World.Random.Next(6);
            switch (random)
            {
                case 0:
                    new ResourceKeeperComponent<GoldResource>();
            }
        }*/
    }
}