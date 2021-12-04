using System.Collections.Generic;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.Instruments;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components
{
    public class InstrumentsOwnerComponent : WorldObjectComponent
    {
        private InventoryComponent<Resource> _inventoryComponent;
        private List<Instrument> _instruments;
        private VisibilityComponent _visibilityComponent;

        private WorldObject target;

        public InstrumentsOwnerComponent(WorldObject owner) : base(owner)
        {
            _instruments = new List<Instrument>();
        }
        
        
    }
}