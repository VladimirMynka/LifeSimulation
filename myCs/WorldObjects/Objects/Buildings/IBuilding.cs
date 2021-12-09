using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;

namespace LifeSimulation.myCs.WorldObjects.Objects.Buildings
{
    public interface IBuilding<out T> where T : Resource
    {
        void TryBuildNextStage(InventoryComponent<Resource> builderInventory);

        WorldObject GetWorldObject();

    }
}