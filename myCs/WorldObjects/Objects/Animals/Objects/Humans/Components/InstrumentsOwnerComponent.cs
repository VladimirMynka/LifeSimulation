﻿using System.Collections.Generic;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.Instruments;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components
{
    public class InstrumentsOwnerComponent : WorldObjectComponent, IHaveInformation, IHaveTarget
    {
        private InventoryComponent<Resource> _inventoryComponent;
        private readonly List<Instrument> _instruments;
        private VisibilityComponent _visibilityComponent;

        private IResourceKeeper<Resource> _target;

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
            if (_target != null && _target.CheckWereDestroyed())
                _target = null;
            if (_target == null)
                SearchSource();
            if (ResourceHere())
                Extract();
            CheckAndRemoveInstruments();
        }

        private void SearchSource()
        {
            _target = _visibilityComponent.SearchOf<IResourceKeeper<Resource>>(CanExtractFrom);
        }

        private bool CanExtractFrom(IResourceKeeper<Resource> component)
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
            _instruments.RemoveAll((instrument) => instrument.IsDestroyed());
        }

        private bool ResourceHere()
        {
            if (_target != null && !_target.CheckWereDestroyed())
                return Direction.CheckEqual(WorldObject.Cell.Coords, _target.GetCoords());
            return false;
        }

        private void Extract()
        {
            if (_target.CanBeExtractUsing(InstrumentType.None))
                _inventoryComponent.Add(_target.Extract(InstrumentType.None));
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
            int random = World.Random.Next((int) InstrumentType.Shovel + 1);
            var instrument = Instrument.Create((InstrumentType) random, _inventoryComponent);
            if (instrument != null)
                _instruments.Add(instrument);
        }

        public int GetInformationPriority()
        {
            return Defaults.InfoPriorityInstrumentsOwner;
        }

        public int GetPriorityInBehaviour()
        {
            return _target != null
                ? _inventoryComponent.IsFilled()
                    ? Defaults.BehaviourInstrumentsTriggered
                    : Defaults.BehaviourSearching
                : Defaults.BehaviourHaveNotPriority;
        }

        public WorldObject GetTarget()
        {
            return _target.GetWorldObject();
        }

        public override string ToString()
        {
            return _instruments.Count.ToString();
        }
    }
}