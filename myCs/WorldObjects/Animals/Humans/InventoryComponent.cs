namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
{
    public class InventoryComponent : WorldObjectComponent, IHaveInformation
    {
        private int _reserve;
        private readonly int _maxReserve;
        public InventoryComponent(WorldObject owner, int reserve) : base(owner)
        {
            _reserve = 0;
            _maxReserve = reserve;
        }

        /// <summary>
        /// Returns quantity or removed quantity. 
        /// For negative quantity it works as Remove.
        /// </summary>
        public int Remove(int quantity)
        {
            if (quantity < 0)
                return Add(-quantity);
            if (_reserve > quantity)
            {
                _reserve -= quantity;
                return quantity;
            }
            var forReturn = _reserve;
            _reserve = 0;
            return forReturn;
        }
        
        /// <summary>
        /// Returns 0 or excess.
        /// For negative quantity it works as Add.
        /// </summary>
        public int Add(int quantity)
        {
            if (quantity < 0)
                return Remove(-quantity);
            if (_reserve + quantity < _maxReserve)
            {
                _reserve += quantity;
                return 0;
            }

            var excess = _reserve + quantity - _maxReserve;
            _reserve = _maxReserve;
            return excess;
        }

        public int RemoveAll()
        {
            var removedQuantity = _reserve;
            _reserve = 0;
            return removedQuantity;
        }

        public bool IsFilled()
        {
            return _reserve == _maxReserve;
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

        public string GetInformation()
        {
            var info = "Inventory: " + _reserve + '/' + _maxReserve;
            return info;
        }
    }
}