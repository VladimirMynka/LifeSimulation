using System;
using System.Linq;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.Instruments;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents.Resources
{
    public class ResourceKeeperComponent<T> : WorldObjectComponent, IHaveInformation, IResourceKeeper<T> where T: Resource
    {
        private readonly T _resource;
        private readonly InstrumentType[] _instrumentTypes;
        
        public ResourceKeeperComponent(WorldObject owner, T resource, int count, InstrumentType[] instrumentTypes) 
            : base(owner)
        {
            _resource = resource;
            _resource.Set(count);
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

        public Type KeepingType()
        {
            return _resource.GetType();
        }

        public int GetInformationPriority()
        {
            return Defaults.InfoPriorityResourceKeeper;
        }

        public override string ToString()
        {
            var info = _resource.ToString();
            info += "\nCan be extract using: ";
            foreach (var instrumentType in _instrumentTypes)
                info += '\n' + instrumentType.ToString();
            return info;
        }

        public string ToResourceString()
        {
            return _resource.ToString() + " on " + InformationComponent.GetInfoAboutCoords(this);
        }
    }
}