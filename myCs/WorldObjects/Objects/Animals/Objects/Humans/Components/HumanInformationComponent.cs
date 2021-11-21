using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Moving;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components
{
    public class HumanInformationComponent : InformationComponent
    {
        private InventoryComponent _inventory;
        private HumanEaterComponent _humanEaterComponent;
        private VisibilityComponent _visibilityComponent;
        private HealthComponent _healthComponent;
        private HumanAgeComponent _humanAgeComponent;
        private MatingComponent _matingComponent;
        private MovingComponent _movingComponent;
        public HumanInformationComponent(WorldObject owner) : base(owner)
        {
        }

        public override void Start()
        {
            base.Start();
            _inventory = GetComponent<InventoryComponent>();
            _humanEaterComponent = GetComponent<HumanEaterComponent>();
            _healthComponent = GetComponent<HealthComponent>();
            _humanAgeComponent = GetComponent<HumanAgeComponent>();
            _movingComponent = GetComponent<MovingComponent>();
            _visibilityComponent = GetComponent<VisibilityComponent>();
        }

        protected override string GetAllInformation()
        {
            var info = GetInfoAboutCoords() + "\n\n";
            info += _humanEaterComponent.GetInformation() + "\n\n";
            info += _inventory.GetInformation() + "\n\n";
            info += _healthComponent.GetInformation() + "\n\n";
            info += _visibilityComponent.GetInformation() + "\n\n";
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