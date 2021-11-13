using LifeSimulation.myCs.WorldObjects.Animals.Mating;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Animals
{
    public class AnimalInformationComponent : InformationComponent
    {
        private EaterComponent _eaterComponent;
        private HealthComponent _healthComponent;
        private AnimalAgeComponent _animalAgeComponent;
        private MatingComponent _matingComponent;
        private MovingComponent _movingComponent;
        
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
        }

        protected override string GetAllInformation()
        {
            string info = "";
            info += GetInfoAboutCoords() + "\n\n";
            info += _eaterComponent.GetInformation() + "\n\n";
            info += _healthComponent.GetInformation() + "\n\n";
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