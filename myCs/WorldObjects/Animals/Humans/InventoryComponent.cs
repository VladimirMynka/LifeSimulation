namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
{
    public class InventoryComponent : WorldObjectComponent
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
    }
}