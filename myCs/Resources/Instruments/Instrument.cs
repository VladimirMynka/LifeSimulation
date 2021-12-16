using LifeSimulation.myCs.Resources.UneatableResources;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;

namespace LifeSimulation.myCs.Resources.Instruments
{
    public class Instrument
    {
        private static readonly Resource[][] CreatingLists = new Resource[][]{
            new Resource[]{new WoodResource(10)},
            new Resource[]{new WoodResource(5), new IronResource(2), new GoldResource(2)},
            new Resource[]{new WoodResource(7), new CompostResource(3)},
            new Resource[]{new IronResource(10)}
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

        public bool IsDestroyed()
        {
            return _stability <= 0;
        }

        public TKeeper ExtractFrom<TKeeper>(IResourceKeeper<TKeeper> keeper) 
            where TKeeper : Resource
        {
            if (_stability <= 0)
                return null;
            var resource = keeper.Extract(GetInstrumentType());
            if (resource != null)
                _stability--;
            return resource;
        }

        private static int GetListNumber(InstrumentType type)
        {
            switch (type)
            {
                case InstrumentType.Axe:
                    return 0;
                case InstrumentType.Pickaxe:
                    return 2;
                case InstrumentType.Shovel:
                    return 3;
            }

            return -1;
        }

        public static Resource CheckCanCreate(InstrumentType type, InventoryComponent<Resource> inventory)
        {
            var array = CreatingLists[GetListNumber(type)];
            return inventory.FirstOrDefaultLackCounts(array);
        }
        
        public static Instrument Create(InstrumentType type, InventoryComponent<Resource> inventory)
        {
            var listNumber = GetListNumber(type);

            return CreateByList(listNumber, type, inventory);
        }

        private static Instrument CreateByList(int listNumber,
            InstrumentType type,
            InventoryComponent<Resource> inventory)
        {
            if (inventory.RemoveIfHave(CreatingLists[listNumber]))
                return new Instrument(40, type);
            return null;
        }

        public override string ToString()
        {
            return _stability.ToString();
        }
    }
}