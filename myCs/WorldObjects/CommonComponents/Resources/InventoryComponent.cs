﻿using System;
using System.Collections.Generic;
using System.Linq;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents.Resources
{
    public class InventoryComponent<T> : WorldObjectComponent, IHaveInformation, IInventory<T> where T : Resource
    {
        private readonly List<T> _reserves;
        private int _currentCount;
        public int MaxCount;
        public InventoryComponent(WorldObject owner, int maxCount) : base(owner)
        {
            MaxCount = maxCount;
            _currentCount = 0;
            _reserves = new List<T>();
        }

        private List<TExact> GetReservesWithType<TExact>() where TExact : T
        {
            return _reserves.OfType<TExact>().ToList();
        }

        private TExact GetReserveWithType<TExact>() where TExact : T
        {
            foreach (var reserve in _reserves)
            {
                var resource = reserve as TExact;
                if (resource != null)
                    return resource;
            }
            return null;
        }
        
        private T GetReserveWithType(Type type)
        {
            foreach (var reserve in _reserves)
            {
                if (reserve.GetType() == type)
                    return reserve;
            }
            return null;
        }
        
        public int Remove<TExact>(int quantity) where TExact : T
        {
            var reservesOfType = GetReservesWithType<TExact>();
            var realRemoved = 0;
            var count = reservesOfType.Count - 1; 
            foreach (var reserve in reservesOfType)
            {
                var mustBeRemovedNow = quantity - realRemoved - quantity * count / reservesOfType.Count;
                realRemoved += reserve.Take(mustBeRemovedNow);
                count--;
            }
            foreach (var reserve in reservesOfType)
            {
                realRemoved += reserve.Take(quantity - realRemoved);
                if (reserve.IsEmpty())
                    _reserves.Remove(reserve);
            }
            _currentCount -= realRemoved;
            return realRemoved;
        }
        
        public int Remove<TExact>(TExact resource) where TExact : T
        {
            var quantity = resource.GetCount();
            return Remove<TExact>(quantity);
        }

        public TExact Remove<TExact>() where TExact : T
        {
            var reserve = GetReserveWithType<TExact>();
            _reserves.Remove(reserve);
            _currentCount -= reserve.GetCount();
            return reserve;
        }

        public TExact RemoveWithTypeAs<TExact>(TExact resource) where TExact : T
        {
            return Remove<TExact>();
        }

        public int Remove(Resource resource)
        {
            var reserve = GetReserveWithType(resource.GetType());
            if (reserve == null)
                return 0;
            int count = reserve.Take(resource.GetCount());
            if (reserve.GetCount() == 0)
                _reserves.Remove(reserve);
            _currentCount -= count;
            return count;
        }

        public bool Remove(Resource[] resources)
        {
            throw new NotImplementedException();
        }

        public int Add(Resource resource)
        {
            var reserve = GetReserveWithType(resource.GetType());
            var addingCount = Math.Min(MaxCount - _currentCount, resource.GetCount());
            if (reserve == null)
            {
                reserve = (T)resource;
                resource.Set(addingCount);
                _reserves.Add(reserve);
            }
            else 
                reserve.Add(addingCount);
            _currentCount += addingCount;
            return addingCount;
        }
        
        public int Add<TExact>(int addingCount) where TExact : T, new()
        {
            var excess = addingCount + _currentCount - MaxCount;
            if (excess == addingCount)
                return 0;
            if (excess > 0)
                addingCount -= excess;

            var reserve = GetReserveWithType<TExact>();
            if (reserve == null)
            {
                reserve = new TExact();
                reserve.Set(addingCount);
                _reserves.Add(reserve);
            }
            else
            {
                reserve.Add(addingCount);
            }

            _currentCount += addingCount;
            return addingCount;
        }
        
        private int SetTo(T resource, int count)
        {
            _currentCount -= resource.GetCount();
            resource.Set(Math.Min(count, MaxCount - _currentCount));
            _currentCount += resource.GetCount();
            return count - resource.GetCount();
        }

        public int RemoveAll<TExact>() where TExact : T
        {
            var exactReserves = GetReservesWithType<TExact>();
            var removingCount = 0;
            foreach (var reserve in exactReserves)
            {
                removingCount += reserve.TakeAll();
                _reserves.Remove(reserve);
            }

            _currentCount -= removingCount;
            return removingCount;
        }

        public bool IsFilled()
        {
            return _currentCount == MaxCount;
        }

        public bool IsHalfFull()
        {
            return _currentCount >= MaxCount / 2;
        }

        public Resource GetTheLargestResource()
        {
            Resource returningResource = null;
            foreach (var resource in _reserves)
            {
                if (returningResource == null || resource.GetCount() > returningResource.GetCount())
                    returningResource = resource;
            }

            return returningResource;
        }

        public void AverageReserveWith<TCommon, TOther>(InventoryComponent<TOther> other, bool secondTime = false) 
            where TCommon : T, TOther
            where TOther : Resource
        {
            var commonReserves = GetComponents<TCommon>();
            foreach (var reserve in commonReserves)
            {
                var otherReserve = other.GetReserveWithType(reserve.GetType()); 
                var taken1 = reserve.TakeAll();
                var taken2 = otherReserve.TakeAll();
                
                _currentCount -= taken1;
                other._currentCount -= taken2;
                int commonCount = taken1 + taken2;
                
                var part1 = commonCount / 2;
                var excess1 = reserve.AddWithExcess(part1, MaxCount - _currentCount);

                var excess2 = other.SetTo(otherReserve, commonCount - part1);

                if (excess1 == 0)
                {
                    reserve.Add(excess2);
                    _currentCount += reserve.GetCount();
                    continue;
                }

                other.SetTo(otherReserve, excess1 + otherReserve.GetCount());
            }

            if (!secondTime)
            {
                other.AverageReserveWith<TCommon, T>(this, true);
            }
        }

        public bool CheckHave<TExact>(int quantity) where TExact : T
        {
            var exactReserves = GetComponents<TExact>();
            foreach (var reserve in exactReserves)
                quantity -= reserve.GetCount();

            return quantity <= 0;
        }
        
        public bool CheckHave(Resource resource)
        {
            var reserve = GetReserveWithType(resource.GetType());
            return reserve != null && reserve.GetCount() >= resource.GetCount();
        }
        
        public bool HasMoreThanNothing(Resource resource)
        {
            var reserve = GetReserveWithType(resource.GetType());
            return reserve != null && reserve.GetCount() > 0;
        }

        public bool CheckHave(Resource[] resources)
        {
            foreach (var resource in resources)
            {
                if (!CheckHave(resource))
                    return false;
            }
            return true;
        }

        public int GetCountOf(Resource resource)
        {
            var reserve = GetReserveWithType(resource.GetType());
            return reserve == null 
                ? 0 
                : reserve.GetCount();
        }

        public int GetLackCount(Resource resource)
        {
            int forReturn = resource.GetCount() - GetCountOf(resource);
            return forReturn < 0 ? 0 : forReturn;
        }

        public List<Resource> GetLackCounts(Resource[] resources)
        {
            var list = new List<Resource>();
            foreach (var resource in resources)
            {
                var count = GetLackCount(resource);
                if (count <= 0)
                    continue;
                var clone = resource.Clone();
                clone.Set(count);
                    list.Add(clone);
            }

            return list;
        }
        
        public Resource FirstOrDefaultLackCounts(Resource[] resources)
        {
            foreach (var resource in resources)
            {
                var count = GetLackCount(resource);
                if (count <= 0)
                    continue;
                var clone = resource.Clone();
                clone.Set(count);
                return clone;
            }

            return null;
        }

        public override string ToString()
        {
            var info = "Inventory: ";
            foreach (var reserve in _reserves)
                info += '\n' + reserve.ToString() +  '/' + MaxCount;
            return info;
        }

        public int GetInformationPriority()
        {
            return Defaults.InfoPriorityInventory;
        }

        public bool RemoveIfHave(Resource resource)
        {
            return CheckHave(resource) && Remove(resource) != -1;
        }
        
        public bool RemoveIfHave(Resource[] resources)
        {
            if (!CheckHave(resources))
                return false;
            foreach (var resource in resources)
            {
                Remove(resource);
            }

            return true;
        }

        public Resource RemoveOrGetFirstLack(Resource[] resources)
        {
            var lack = FirstOrDefaultLackCounts(resources);
            if (lack != null)
                RemoveIfHave(resources);
            return lack;
        }

        public bool CanKeep<T2>(T2 resource) where T2 : Resource
        {
            return resource is T;
        }

        public WorldObject GetWorldObject()
        {
            return WorldObject;
        }

        public Type GetResourceType()
        {
            return typeof(T);
        }

        public int GetAllCount()
        {
            return _currentCount;
        }
    }
}