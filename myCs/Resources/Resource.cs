using LifeSimulation.myCs.WorldObjects.CommonComponents;

namespace LifeSimulation.myCs.Resources
{
    public abstract class Resource
    {
        private int _count;

        public Resource(int count = 0)
        {
            _count = count < 0 ? 0 : count;
        }

        public static T Clone<T>(T resource) where T : Resource, new()
        {
            var clone = new T();
            clone.Set(resource._count);
            return clone;
        }

        public int Take(int count = -1)
        {
            if (_count <= count || count < 0) 
                return TakeAll();
            _count -= count;
            return count;
        }

        public int TakeAll()
        {
            var forReturn = _count;
            _count = 0;
            return forReturn;
        }

        public int GetCount()
        {
            return _count;
        }

        public void Set(int count)
        {
            _count = count;
        }

        public void Add(int count)
        {
            _count += count;
        }

        public int AddWithExcess(int count, int max)
        {
            var excess = max - count - _count;
            if (excess > 0)
            {
                Add(count - excess);
                return excess;
            }
            Add(count);
            return 0;
        }

        public void Add(Resource other)
        {
            Add(other.TakeAll());
        }

        public void ShareWith(Resource other, int count)
        {
            var realCount = Take(count);
            other.Add(realCount);
        }

        public bool IsEmpty()
        {
            return _count == 0;
        }

        public override string ToString()
        {
            return GetType().Name + _count;
        }

        public static implicit operator int(Resource resource)
        {
            return resource._count;
        }
    }
}