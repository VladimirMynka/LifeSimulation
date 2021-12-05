using System;
using System.Collections.Generic;
using LifeSimulation.myCs.Resources;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents
{
    public interface IInventory<in T> where T : Resource
    {
        public int Remove<TExact>(int quantity) where TExact : T;

        public int Remove<TExact>(TExact resource) where TExact : T;

        public TExact Remove<TExact>() where TExact : T;

        public TExact RemoveWithTypeAs<TExact>(TExact resource) where TExact : T;

        public int Remove(T resource);

        public bool Remove(T[] resources);

        public int Add(T resource);

        public int Add<TExact>(int addingCount) where TExact : T;

        int SetTo(T resource, int count);

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