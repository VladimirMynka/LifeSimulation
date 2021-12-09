using LifeSimulation.myCs.Resources;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents.Resources
{
    public interface IInventory<out T> where T : Resource
    {
        int Remove(Resource resource);

        bool Remove(Resource[] resources);

        int Add(Resource resource);

        bool IsFilled();

        bool CheckHave(Resource resource);
        
        bool HasMoreThanNothing(Resource resource);

        bool CheckHave(Resource[] resources);

        bool RemoveIfHave(Resource resource);

        bool RemoveIfHave(Resource[] resources);

        WorldObject GetWorldObject();

        int[] GetCoords();
    }
}