using System.Collections.Generic;
using LifeSimulation.myCs.Resources;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents.Resources
{
    public interface IInventory<out T> where T : Resource
    {
        int Remove(Resource resource);
        
        int Add(Resource resource);

        bool IsFilled();

        bool CheckHave(Resource resource);
        
        bool HasMoreThanNothing(Resource resource);

        bool CheckHave(Resource[] resources);

        bool RemoveIfHave(Resource resource);

        bool RemoveIfHave(Resource[] resources);

        int GetCountOf(Resource resource);

        int GetLackCount(Resource resource);

        List<Resource> GetLackCounts(Resource[] resources);

        Resource FirstOrDefaultLackCounts(Resource[] resources);
        
        Resource RemoveOrGetFirstLack(Resource[] resources);

        bool CanKeep<T2>(T2 resource) where T2 : Resource;

        WorldObject GetWorldObject();
    }
}