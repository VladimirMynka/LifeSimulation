using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components
{
    public class InventoryComponent : WorldObjectComponent, IHaveInformation
    {
        private class Wrapper<T>
        {
            public T inner;

            public override string ToString()
            {
                return inner.ToString();
            }
        }
        private readonly Wrapper<int> _plantReserve = new Wrapper<int>();
        private readonly Wrapper<int> _meatReserve = new Wrapper<int>();
        private readonly Wrapper<int> _rotMeatReserve = new Wrapper<int>();
        private readonly int _maxReserve;
        public InventoryComponent(WorldObject owner, int reserve) : base(owner)
        {
            _maxReserve = reserve;
        }

        private Wrapper<int> ReserveByMealType(MealType mealType)
        {
            switch (mealType)
            {
                case MealType.Plant:
                    return _plantReserve;
                case MealType.FreshMeat:
                    return _meatReserve;
                case MealType.DeadMeat:
                    return _rotMeatReserve;
                default:
                    return _plantReserve;
            }
        }

        /// <summary>
        /// Returns quantity or removed quantity. 
        /// For negative quantity it works as Remove.
        /// </summary>
        public int Remove(int quantity, MealType mealType = MealType.AllTypes)
        {
            if (quantity < 0)
            {
                return Add(-quantity, mealType);
            }
            
            if (mealType == MealType.AllTypes)
            {
                int removed = RemoveFrom(_plantReserve, quantity / 3);
                removed += RemoveFrom(_meatReserve, quantity / 3 * 2 - removed);
                removed += RemoveFrom(_rotMeatReserve, quantity - removed);
                removed += RemoveFrom(_plantReserve, quantity - removed);
                removed += RemoveFrom(_meatReserve, quantity - removed);
                return removed;
            }
            
            var reserve = ReserveByMealType(mealType);
            return RemoveFrom(reserve, quantity);
        }

        private static int RemoveFrom(Wrapper<int> reserve, int quantity)
        {
            if (quantity < 0)
                return 0;
            if (reserve.inner > quantity)
            {
                reserve.inner -= quantity;
                return quantity;
            }
            var forReturn = reserve.inner;
            reserve.inner = 0;
            return forReturn;
        }

        /// <summary>
        /// Returns 0 or excess.
        /// For negative quantity it works as Add.
        /// </summary>
        public int Add(int quantity, MealType mealType = MealType.AllTypes)
        {
            if (mealType == MealType.AllTypes)
            {
                int excess = AddTo(_plantReserve, quantity / 3);
                excess = AddTo(_meatReserve, quantity / 3 + excess);
                excess = AddTo(_rotMeatReserve, quantity - quantity / 3 * 2 + excess);
                excess = AddTo(_plantReserve, excess);
                excess = AddTo(_meatReserve, excess);
                return excess;
            }
            
            var reserve = ReserveByMealType(mealType);
            return AddTo(reserve, quantity);
        }

        private int AddTo(Wrapper<int> reserve, int quantity)
        {
            if (quantity < 0)
                return 0;
            if (quantity + _plantReserve.inner + _meatReserve.inner + _rotMeatReserve.inner <= _maxReserve)
            {
                reserve.inner += quantity;
                return 0;
            }
            var freeSpace = _maxReserve - _meatReserve.inner - _plantReserve.inner - _rotMeatReserve.inner;
            reserve.inner += freeSpace;
            return quantity - freeSpace;
        }

        public int RemoveAll()
        {
            var removedQuantity = _plantReserve.inner + _meatReserve.inner + _rotMeatReserve.inner;
            _plantReserve.inner = 0;
            _meatReserve.inner = 0;
            _rotMeatReserve.inner = 0;
            return removedQuantity;
        }

        public bool IsFilled()
        {
            return _plantReserve.inner + _meatReserve.inner + _rotMeatReserve.inner == _maxReserve;
        }

        public void AverageReserveWith(InventoryComponent other)
        {
            int commonReserve = other.RemoveAll();
            commonReserve += this.RemoveAll();
            int part1 = commonReserve / 2;
            int excess1 = other.Add(part1);
            int excess2 = this.Add(commonReserve - part1);
            if (excess1 > excess2)
                this.Add(excess1);
            else
                other.Add(excess2);
        }

        public int AllReserve()
        {
            return _meatReserve.inner + _plantReserve.inner + _rotMeatReserve.inner;
        }

        public bool CheckHave(int quantity, MealType mealType)
        {
            if (mealType == MealType.AllTypes)
                return AllReserve() > quantity;
            return CheckHaveIn(ReserveByMealType(mealType).inner, quantity);
        }

        private static bool CheckHaveIn(int reserve, int quantity)
        {
            return reserve >= quantity;
        }

        public override string ToString()
        {
            var info = "Inventory: \n";
            info += "plants: " + _plantReserve + '/' + _maxReserve + '\n';
            info += "meat: " + _meatReserve + '/' + _maxReserve + '\n';
            info += "rot meat: " + _rotMeatReserve + '/' + _maxReserve;
            return info;
        }

        public int GetInformationPriority()
        {
            return 15;
        }
    }
}