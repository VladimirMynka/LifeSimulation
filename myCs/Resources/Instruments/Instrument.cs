using System.Collections.Generic;
using LifeSimulation.myCs.Resources.UneatableResources;
using LifeSimulation.myCs.WorldObjects.CommonComponents;

namespace LifeSimulation.myCs.Resources.Instruments
{
    public class Instrument
    {
        private static readonly Resource[][] CreatingLists = new Resource[][]{
            new Resource[]{new WoodResource(50), new IronResource(10)},
            new Resource[]{new WoodResource(20), new IronResource(10), new GoldResource(5)},
            new Resource[]{new WoodResource(20), new CompostResource(10)},
            new Resource[]{new IronResource(25)}
        };

        private int _stability;
        private readonly InstrumentType _instrumentType;


        private Instrument(int stability, InstrumentType instrumentType)
        {
            _stability = stability;
            _instrumentType = instrumentType;
        }

        public InstrumentType GetInstrumentType()
        {
            return _instrumentType;
        }

        public bool isDestroyed()
        {
            return _stability <= 0;
        }

        public void ExtractFromTo<TKeeper, TInventory>(
            ResourceKeeperComponent<TKeeper> keeper,
            InventoryComponent<TInventory> inventory
        ) where TKeeper : TInventory where TInventory : Resource
        {
            if (_stability <= 0)
                return;
            var resource = keeper.Extract(this);
            if (resource == null)
                return;
            inventory.Add(resource);
            _stability--;
        }

        public static Instrument Create(int level, InstrumentType type, List<Resource> list)
        {
            var listNumber = 0;
            switch (type)
            {
                case InstrumentType.Axe:
                    listNumber = 0;
                    break;
                case InstrumentType.Pickaxe:
                    listNumber = level > 1 ? 1 : 2;
                    break;
                case InstrumentType.Shovel:
                    listNumber = 3;
                    break;
            }

            return CreateByList(listNumber, 25 * (3 + level), 20 + 20 * level, type, list);
        }

        private static Instrument CreateByList(int listNumber,
            int percents,
            int stability,
            InstrumentType type,
            List<Resource> list)
        {
            return CanBeCreated(listNumber, percents, list)
                ? new Instrument(stability, type)
                : null;
        }

        private static bool CanBeCreated(int listNumber, int percents, List<Resource> list)
        {
            foreach (var neededResource in CreatingLists[listNumber])
            {
                var allCheckedResourceEnough = false;
                foreach (var resource in list)
                {
                    if (resource.GetType() != neededResource.GetType())
                        continue;
                    if (resource.GetCount() < neededResource.GetCount() * percents / 100)
                        return false;
                    allCheckedResourceEnough = true;
                    break;
                }

                if (!allCheckedResourceEnough)
                    return false;
            }

            return true;
        }
    }
}