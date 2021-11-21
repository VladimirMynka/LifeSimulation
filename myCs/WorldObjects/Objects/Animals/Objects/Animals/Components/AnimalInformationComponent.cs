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
        }

        protected override string GetAllInformation()
        {
            string info = "";
            info += GetInfoAboutCoords() + "\n\n";
            info += _eaterComponent.GetInformation() + "\n\n";
            info += _healthComponent.GetInformation() + "\n\n";
            info += _visibilityComponent.GetInformation() + "\n\n";
            info += _animalAgeComponent.GetInformation() + "\n\n";
            info += _movingComponent.GetInformation() + "\n\n";
            if (_matingComponent == null)
                _matingComponent = GetComponent<MatingComponent>();
            else
                info += _matingComponent.GetInformation();
            
            
            return info;
        }
    }
}