using System.Collections.Generic;
using System.Linq;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents
{
    public class InventoryComponent<T> : WorldObjectComponent, IHaveInformation where T : Resource
    {
        private readonly List<T> _reserves;
        private int _currentCount;
        private readonly int _maxCount;
        public InventoryComponent(WorldObject owner, int maxCount) : base(owner)
        {
            _maxCount = maxCount;
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

        public TExact Remove<TExact>() where TExact : T
        {
            var reserve = GetReserveWithType<TExact>();
            _reserves.Remove(reserve);
            return reserve;
        }
        
        public int Add<TExact>(TExact resource) where TExact : T
        {
            if (resource == null)
                return 0;
            var addingCount = resource.GetCount();
            var excess = addingCount + _currentCount - _maxCount;
            if (excess == addingCount)
                return 0;
            if (excess > 0)
                addingCount -= excess;

            var reserve = GetReserveWithType<TExact>();
            if (reserve == null)
            {
                reserve = resource;
                _reserves.Add(reserve);
            }
            else
            {
                reserve.Add(addingCount);
            }

            _currentCount += addingCount;
            return addingCount;
        }
        
        public int Add<TExact>(int addingCount) where TExact : T, new()
        {
            var excess = addingCount + _currentCount - _maxCount;
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

        public int RemoveAll<TExact>() where TExact : T
        {
            var exactReserves = GetReservesWithType<TExact>();
            var removingCount = 0;
            foreach (var reserve in exactReserves)
            {
                removingCount += reserve.TakeAll();
                _reserves.Remove(reserve);
            }

            return removingCount;
        }

        public int RemoveAllWithTypeAs<TExact>(TExact reserve) where TExact : T
        {
            return RemoveAll<TExact>();
        }

        public bool IsFilled()
        {
            return _currentCount == _maxCount;
        }

        public void AverageReserveWith<TCommon, TOther>(InventoryComponent<TOther> other) 
            where TCommon : T, TOther, new()
            where TOther : Resource
        {
            var commonReserves = GetComponents<TCommon>();
            foreach (var reserve in commonReserves)
            {
                var type = reserve.GetType();
                var taken = reserve.TakeAll();
                _currentCount -= taken;
                var commonCount = reserve.TakeAll() + other.RemoveAllWithTypeAs(reserve);
                
                var part1 = commonCount / 2;
                var excees1 = reserve.AddWithExcess(part1, _maxCount - _currentCount);
                
                var forAdding = Resource.Clone(reserve);
                forAdding.Set(commonCount - part1);
                var excees2 = commonCount - part1 - Add(forAdding);
                
                if (excees1 > excees2)
                {
                    forAdding = Resource.Clone(reserve);
                    forAdding.Set(excees1);
                    
                }
                    
            }
            /*var commonReserve = other.RemoveAll<TCommon>();
            commonReserve += RemoveAll<TCommon>();
            var part1 = commonReserve / 2;
            var excess1 = part1 - other.Add<TCommon>(part1);
            var excess2 = commonReserve - part1 - Add<TCommon>(commonReserve - part1);
            if (excess1 > excess2)
                Add<TCommon>(excess1);
            else
                other.Add<TCommon>(excess2);*/
        }

        public bool CheckHave<TExact>(int quantity) where TExact : T
        {
            var exactReserves = GetComponents<TExact>();
            foreach (var reserve in exactReserves)
                quantity -= reserve.GetCount();

            return quantity <= 0;
        }

        public override string ToString()
        {
            var info = "Inventory: \n";
            foreach (var reserve in _reserves)
                info += reserve +  '/' + _maxCount + '\n';
            return info;
        }

        public int GetInformationPriority()
        {
            return 15;
        }
    }
}