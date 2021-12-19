using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Effects.Sicknesses
{
    public class StupidPlagueComponent : SicknessComponent
    {
        private static readonly Pill[] CanBeHeal = new[]{Pill.LivingWater};
        private static readonly Pill[] CanBeDecrease = new[]{Pill.BitterSyrup};
        
        private HealthComponent _healthComponent;
        private EaterComponent _eaterComponent;
        private InventoryComponent<Resource> _inventoryComponent;
        private int _savedHealth;
        private int _savedSatiety;
        private int _savedInventory;
        
        public StupidPlagueComponent(WorldObject owner, int period) 
            : base(owner, period, CanBeHeal, CanBeDecrease)
        {
        }

        public override void Start()
        {
            base.Start();
            _healthComponent = GetComponent<HealthComponent>();
            _eaterComponent = GetComponent<EaterComponent>();
            _inventoryComponent = GetComponent<InventoryComponent<Resource>>();
            _savedHealth = _healthComponent.MaxHealth;
            _savedSatiety = _eaterComponent.MaxSatiety;
            _savedInventory = _inventoryComponent.MaxCount;
            _healthComponent.MaxHealth = _savedHealth / 2;
            _eaterComponent.MaxSatiety = _savedSatiety / 2;
            _inventoryComponent.MaxCount = _savedInventory / 2;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _healthComponent.MaxHealth = _savedHealth;
            _eaterComponent.MaxSatiety = _savedSatiety;
            _inventoryComponent.MaxCount = _savedInventory;
        }

        public override SicknessComponent Clone(WorldObject worldObject)
        {
            return new StupidPlagueComponent(worldObject, beginTimer);
        }
    }
}