using LifeSimulation.myCs.WorldObjects.Animals.Animals;
using LifeSimulation.myCs.WorldObjects.Animals.Mating;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;

namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
{
    public class HumanInformationComponent : InformationComponent
    {
        private InventoryComponent _inventory;
        private HumanEaterComponent _humanEaterComponent;
        private HealthComponent _healthComponent;
        private HumanAgeComponent _humanAgeComponent;
        private MatingComponent _matingComponent;
        private MovingComponent _movingComponent;
        public HumanInformationComponent(WorldObject owner) : base(owner)
        {
        }

        public override void Start()
        {
            _inventory = GetComponent<InventoryComponent>();
            _humanEaterComponent = GetComponent<HumanEaterComponent>();
            _healthComponent = GetComponent<HealthComponent>();
            _humanAgeComponent = GetComponent<HumanAgeComponent>();
            _movingComponent = GetComponent<MovingComponent>();
        }

        protected override string GetAllInformation()
        {
            var info = "";
            info += _humanEaterComponent.GetInformation() + "\n\n";
            info += _inventory.GetInformation() + "\n\n";
            info += _healthComponent.GetInformation() + "\n\n";
            info += _humanAgeComponent.GetInformation() + "\n\n";
            info += _movingComponent.GetInformation() + "\n\n";
            if (_matingComponent == null)
                _matingComponent = GetComponent<MatingComponent>();
            else
                info += _matingComponent.GetInformation();
            
            return info;
        }
    }
}