using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Moving;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals.Components
{
    public class AnimalInformationComponent : InformationComponent
    {
        private EaterComponent _eaterComponent;
        private HealthComponent _healthComponent;
        private AnimalAgeComponent _animalAgeComponent;
        private MatingComponent _matingComponent;
        private MovingComponent _movingComponent;
        private VisibilityComponent _visibilityComponent;
        private PetComponent _petComponent;
        
        public AnimalInformationComponent(WorldObject owner) : base(owner)
        {
        }

        public override void Start()
        {
            base.Start();
            _eaterComponent = GetComponent<EaterComponent>();
            _healthComponent = GetComponent<HealthComponent>();
            _animalAgeComponent = GetComponent<AnimalAgeComponent>();
            _movingComponent = GetComponent<MovingComponent>();
            _visibilityComponent = GetComponent<VisibilityComponent>();
            _petComponent = GetComponent<PetComponent>();
        }

        protected override string GetAllInformation()
        {
            string info = "";
            info += GetInfoAboutCoords() + "\n\n";
            info += _eaterComponent + "\n\n";
            info += _healthComponent + "\n\n";
            info += _visibilityComponent + "\n\n";
            info += _animalAgeComponent + "\n\n";
            info += _movingComponent.ToString() + "\n\n";
            if (_matingComponent == null)
                _matingComponent = GetComponent<MatingComponent>();
            else
                info += _matingComponent.ToString();

            if (_petComponent != null)
                info += "\n\n" + _petComponent;
            
            return info;
        }
    }
}