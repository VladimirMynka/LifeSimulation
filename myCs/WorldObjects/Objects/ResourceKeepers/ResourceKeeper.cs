using System.Drawing;
using LifeSimulation.myCs.Drawing;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.Instruments;
using LifeSimulation.myCs.Resources.UneatableResources;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.ResourceKeepers
{
    public class ResourceKeeper<T> : WorldObject where T : Resource, new()
    {
        protected ResourceKeeper(
            Cell keeper, 
            Image image,
            int layer,
            int count,
            InstrumentType[] instrumentTypes
        )            
            : base(keeper)
        {
            components.Add(new DrawableComponent(this, image, layer));
            components.Add(new ResourceKeeperComponent<T>(this, new T(), count, instrumentTypes));
        }

        public static void Spawn(Cell cell)
        {
            Spawn((ResourceKeeperType) World.Random.Next((int) ResourceKeeperType.EasyCompostResource), 
                cell);
        }

        public static void Spawn(ResourceKeeperType keeperType, Cell cell)
        {
            switch (keeperType)
            {
                case ResourceKeeperType.GoldResource:
                    new ResourceKeeper<GoldResource>(cell, Pictures.Gold, 1, 20, 
                        new InstrumentType[]{InstrumentType.Pickaxe});
                    return;
                case ResourceKeeperType.IronResource:
                    new ResourceKeeper<IronResource>(cell, Pictures.Iron, 1, 50, 
                        new InstrumentType[]{InstrumentType.Pickaxe});
                    return;
                case ResourceKeeperType.EasyIronResource:
                    new ResourceKeeper<IronResource>(cell, Pictures.IronEasy, 1, 20, 
                        new InstrumentType[]{InstrumentType.None});
                    return;
                case ResourceKeeperType.CompostResource:
                    new ResourceKeeper<CompostResource>(cell, Pictures.Compost, 1, 50, 
                        new InstrumentType[]{InstrumentType.Shovel, InstrumentType.Pickaxe});
                    return;
                case ResourceKeeperType.EasyCompostResource:
                    new ResourceKeeper<CompostResource>(cell, Pictures.CompostEasy, 1, 30, 
                        new InstrumentType[]{InstrumentType.None});
                    return;
                default:
                    return;
            }
        }
    }
}