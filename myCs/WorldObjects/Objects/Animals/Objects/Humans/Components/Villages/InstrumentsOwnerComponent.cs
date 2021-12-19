using System.Collections.Generic;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.Instruments;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Behaviour;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages
{
    public class InstrumentsOwnerComponent : WorldObjectComponent, IHaveInformation, IHaveTarget
    {
        private InventoryComponent<Resource> _inventoryComponent;
        private WarehousesOwnerComponent _warehousesOwnerComponent;
        private readonly List<Instrument> _instruments;
        private VisibilityComponent _visibilityComponent;
        public bool AlwaysSearch;
        public int MaxInstrumentsCount = Defaults.InstrumentsMax;

        private IResourceKeeper<Resource> _target;

        public InstrumentsOwnerComponent(WorldObject owner) : base(owner)
        {
            _instruments = new List<Instrument>();
        }

        public override void Start()
        {
            base.Start();
            _inventoryComponent = GetComponent<InventoryComponent<Resource>>();
            _warehousesOwnerComponent = GetComponent<WarehousesOwnerComponent>();
            _visibilityComponent = GetComponent<VisibilityComponent>();
        }

        public override void Update()
        {
            base.Update();
            if (_target != null && _target.CheckWereDestroyed())
                _target = null;
            if (_target == null && AlwaysSearch)
                SearchResource();
            if (ResourceHere())
                Extract();
            CheckAndRemoveInstruments();
            CreateNewInstrument();
            
        }

        private void SearchResource()
        {
            _target = _visibilityComponent.Search<IResourceKeeper<Resource>>(CanExtractFrom);
        }

        public void SearchResource(Resource resource)
        {
            if (_target != null)
                return;
            _target = _visibilityComponent.Search<IResourceKeeper<Resource>>(
                keeper => keeper.KeepingType() == resource.GetType() && CanExtractFrom(keeper));
        }

        private bool CanExtractFrom(IResourceKeeper<Resource> component)
        {
            if (component.CanBeExtractUsing(InstrumentType.None))
                return true;
            foreach (var instrument in _instruments)
            {
                if (component.CanBeExtractUsing(instrument.GetInstrumentType()))
                    return true;
            }

            return false;
        }

        private void CheckAndRemoveInstruments()
        {
            _instruments.RemoveAll((instrument) => instrument.IsDestroyed());
        }

        private bool ResourceHere()
        {
            if (_target != null && !_target.CheckWereDestroyed())
                return OnOneCellWith(_target.GetWorldObject());
            return false;
        }

        private void Extract()
        {
            if (_target.CanBeExtractUsing(InstrumentType.None))
            {
                _inventoryComponent.Add(_target.Extract(InstrumentType.None));
                return;
            }

            foreach (var instrument in _instruments)
            {
                if (_target.CanBeExtractUsing(instrument.GetInstrumentType()))
                {
                    _inventoryComponent.Add(instrument.ExtractFrom(_target));
                    return;
                }
            }
        }

        private void CreateNewInstrument()
        {
            if (_instruments.Count >= MaxInstrumentsCount) return;
            int random = World.Random.Next(1, (int) InstrumentType.Shovel + 1);
            var type = (InstrumentType) random;
            var needed = Instrument.CheckCanCreate(type, _inventoryComponent);
            if (needed != null && !_warehousesOwnerComponent.SetTakingOrPuttingResource(needed, true))
            {
                SearchResource(needed);
                return;
            }

            var instrument = Instrument.Create(type, _inventoryComponent);
            if (instrument != null)
                _instruments.Add(instrument);
        }

        public int GetInformationPriority()
        {
            return Defaults.InfoPriorityInstrumentsOwner;
        }

        public int ResourcesSearchingTriggered = Defaults.BehaviourResourcesSearchingTriggered;
        public int UneatableSearching = Defaults.BehaviourUneatableSearching;
        public int GetPriorityInBehaviour()
        {
            return _target != null
                ? _inventoryComponent.IsHalfFull()
                    ? ResourcesSearchingTriggered
                    : !_inventoryComponent.IsFilled()
                        ? UneatableSearching
                        : Defaults.BehaviourHaveNotPriority
                : Defaults.BehaviourHaveNotPriority;

        }

        public WorldObject GetTarget()
        {
            return _target.GetWorldObject();
        }

        public int GetInstrumentsCount()
        {
            return _instruments.Count;
        }

        public override string ToString()
        {
            var info = "Target resource: " + (_target == null
                ? "none"
                : _target.ToResourceString());

            info += "\nInstruments: ";
            foreach (var instrument in _instruments)
                info += '\n' + instrument.GetInstrumentType().ToString() + ": " + instrument;

            return info;
        }
    }
}