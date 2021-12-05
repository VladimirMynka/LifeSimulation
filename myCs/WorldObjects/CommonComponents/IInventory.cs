using LifeSimulation.myCs.Resources;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents
{
    public interface IInventory<in T> where T : Resource
    {
        int Remove<TExact>(int quantity) where TExact : T;

        int Remove<TExact>(TExact resource) where TExact : T;

        TExact Remove<TExact>() where TExact : T;

        TExact RemoveWithTypeAs<TExact>(TExact resource) where TExact : T;

        int Remove(T resource);

        bool Remove(T[] resources);

        int Add(T resource);

        int Add<TExact>(int addingCount) where TExact : T, new();
        
        int RemoveAll<TExact>() where TExact : T;

        bool IsFilled();

        void AverageReserveWith<TCommon, TOther>(InventoryComponent<TOther> other)
            where TCommon : T, TOther
            where TOther : Resource;

        bool CheckHave<TExact>(int quantity) where TExact : T;

        bool CheckHave(T resource);

        bool CheckHave(T[] resources);

        bool RemoveIfHave(T resource);

        bool RemoveIfHave(T[] resources);
    }
}