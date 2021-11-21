using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components
{
    public class InventoryComponent : WorldObjectComponent, IHaveInformation
    {
        private int _plantReserve;
        private int _meatReserve;
        private int _rotMeatReserve;
        private readonly int _maxReserve;
        public InventoryComponent(WorldObject owner, int reserve) : base(owner)
        {
            _plantReserve = 0;
            _meatReserve = 0;
            _rotMeatReserve = 0;
            _maxReserve = reserve;
        }

        private ref int ReserveByMealType(MealType mealType)
        {
            switch (mealType)
            {
                case MealType.Plant:
                    return ref _plantReserve;
                case MealType.FreshMeat:
                    return ref _meatReserve;
                case MealType.DeadMeat:
                    return ref _rotMeatReserve;
                default:
                    return ref _plantReserve;
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
                int removed = RemoveFrom(ref _plantReserve, quantity / 3);
                removed += RemoveFrom(ref _meatReserve, quantity / 3 * 2 - removed);
                removed += RemoveFrom(ref _rotMeatReserve, quantity - removed);
                removed += RemoveFrom(ref _plantReserve, quantity - removed);
                removed += RemoveFrom(ref _meatReserve, quantity - removed);
                return removed;
            }
            
            ref var reserve = ref ReserveByMealType(mealType);
            return RemoveFrom(ref reserve, quantity);
        }

        private static int RemoveFrom(ref int reserve, int quantity)
        {
            if (quantity < 0)
                return 0;
            if (reserve > quantity)
            {
                reserve -= quantity;
                return quantity;
            }
            var forReturn = reserve;
            reserve = 0;
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
                int excess = RemoveFrom(ref _plantReserve, quantity / 3);
                excess = RemoveFrom(ref _meatReserve, quantity / 3 + excess);
                excess = RemoveFrom(ref _rotMeatReserve, quantity - quantity / 3 * 2 + excess);
                excess = RemoveFrom(ref _plantReserve, excess);
                excess = RemoveFrom(ref _meatReserve, excess);
                return excess;
            }
            
            ref var reserve = ref ReserveByMealType(mealType);
            return AddTo(ref reserve, quantity);
        }

        private int AddTo(ref int reserve, int quantity)
        {
            if (quantity < 0)
                return 0;
            if (quantity + _plantReserve + _meatReserve + _rotMeatReserve <= _maxReserve)
            {
                reserve += quantity;
                return 0;
            }
            var empty = _maxReserve - _meatReserve - _plantReserve - _rotMeatReserve;
            reserve += empty;
            return quantity - empty;
        }

        public int RemoveAll()
        {
            var removedQuantity = _plantReserve + _meatReserve + _rotMeatReserve;
            _plantReserve = 0;
            _meatReserve = 0;
            _rotMeatReserve = 0;
            return removedQuantity;
        }

        public bool IsFilled()
        {
            return _plantReserve + _meatReserve + _rotMeatReserve == _maxReserve;
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
            return _meatReserve + _plantReserve + _rotMeatReserve;
        }

        public bool CheckHave(int quantity, MealType mealType)
        {
            if (mealType == MealType.AllTypes)
                return AllReserve() > quantity;
            return CheckHaveIn(ref ReserveByMealType(mealType), quantity);
        }

        private static bool CheckHaveIn(ref int reserve, int quantity)
        {
            return reserve >= quantity;
        }

        public string GetInformation()
        {
            var info = "Inventory: \n";
            info += "plants: " + _plantReserve + '/' + _maxReserve;
            info += "meat: " + _meatReserve + '/' + _maxReserve;
            info += "rot meat: " + _rotMeatReserve + '/' + _maxReserve;
            return info;
        }
    }
}