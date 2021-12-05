using System.Collections.Generic;
using System.Linq;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.Instruments;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents
{
    public class ResourceKeeperComponent<T> : WorldObjectComponent, IResourceKeeper<T> where T: Resource
    {
        private readonly T _resource;
        private readonly InstrumentType[] _instrumentTypes;
        
        public ResourceKeeperComponent(WorldObject owner, T resource, InstrumentType[] instrumentTypes) 
            : base(owner)
        {
            _resource = resource;
            _instrumentTypes = instrumentTypes;
        }

        public bool CanBeExtractUsing(InstrumentType instrumentType)
        {
            return _instrumentTypes.Contains(instrumentType);
        }

        public T Extract(InstrumentType instrumentType)
        {
            if (!CanBeExtractUsing(instrumentType))
                return null;
            WorldObject.Destroy();
            return _resource;
        }

        public int[] GetCoords()
        {
            return WorldObject.Cell.Coords;
        }

        public WorldObject GetWorldObject()
        {
            return WorldObject;
        }

        public bool CheckWereDestroyed()
        {
            return CheckWereDestroyed(this);
        }
    }
}