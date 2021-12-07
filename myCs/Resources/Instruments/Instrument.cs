using LifeSimulation.myCs.Resources.UneatableResources;
using LifeSimulation.myCs.WorldObjects.CommonComponents;

namespace LifeSimulation.myCs.Resources.Instruments
{
    public class Instrument
    {
        private static readonly Resource[][] CreatingLists = new Resource[][]{
            new Resource[]{new WoodResource(50)},
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

        public static Instrument Create(InstrumentType type, InventoryComponent<Resource> inventory)
        {
            var listNumber = 0;
            switch (type)
            {
                case InstrumentType.Axe:
                    listNumber = 0;
                    break;
                case InstrumentType.Pickaxe:
                    listNumber = 2;
                    break;
                case InstrumentType.Shovel:
                    listNumber = 3;
                    break;
            }

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