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
        private PetsOwnerComponent _petsOwnerComponent;
        private BehaviourChangerComponent _behaviourComponent;
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
            _petsOwnerComponent = GetComponent<PetsOwnerComponent>();
            _behaviourComponent = GetComponent<BehaviourChangerComponent>();
        }

        protected override string GetAllInformation()
        {
            var info = GetInfoAboutCoords() + "\n\n";
            info += _humanEaterComponent + "\n\n";
            info += _inventory + "\n\n";
            info += _healthComponent + "\n\n";
            info += _visibilityComponent + "\n\n";
            info += _humanAgeComponent + "\n\n";
            info += _movingComponent.ToString() + "\n\n";
            if (_matingComponent == null)
                _matingComponent = GetComponent<MatingComponent>();
            else
                info += _matingComponent + "\n\n";
            info += _petsOwnerComponent + "\n\n";
            info += _behaviourComponent;
            
            return info;
        }
    }
}