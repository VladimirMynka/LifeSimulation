using System.Collections.Generic;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.Instruments;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components
{
    public class InstrumentsOwnerComponent : WorldObjectComponent, IHaveInformation, IHaveTarget
    {
        private InventoryComponent<Resource> _inventoryComponent;
        private readonly List<Instrument> _instruments;
        private VisibilityComponent _visibilityComponent;

        private WorldObject _target;

        public InstrumentsOwnerComponent(WorldObject owner) : base(owner)
        {
            _instruments = new List<Instrument>();
        }

        public override void Start()
        {
            base.Start();
            _inventoryComponent = GetComponent<InventoryComponent<Resource>>();
            _visibilityComponent = GetComponent<VisibilityComponent>();
        }

        public override void Update()
        {
            base.Update();
            CheckAndRemoveInstruments();
        }

        private void SearchSource<T>() where T : Resource
        {
            _target = _visibilityComponent.Search<ResourceKeeperComponent<T>>(CanExtractFrom).WorldObject;
        }

        private bool CanExtractFrom<T>(ResourceKeeperComponent<T> component) where T : Resource
        {
            foreach (var instrument in _instruments)
            {
                if (component.CanBeExtractUsing(instrument.GetInstrumentType()))
                    return true;
            }

            return false;
        }

        private void CheckAndRemoveInstruments()
        {
            _instruments.RemoveAll((instrument) => instrument.isDestroyed());
        }

        public int GetInformationPriority()
        {
            return 110;
        }

        public int GetPriorityInBehaviour()
        {
            return _target != null
                ? _inventoryComponent.IsFilled()
                    ? 2
                    : 1
                : 0;
        }

        public WorldObject GetTarget()
        {
            return _target;
        }
    }
}