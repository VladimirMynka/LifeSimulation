using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components
{
    public class WarehousesOwnerComponent : WorldObjectComponent
    {
        InventoryComponent<>
        public WarehousesOwnerComponent(WorldObject owner) : base(owner)
        {
        }
    }
}