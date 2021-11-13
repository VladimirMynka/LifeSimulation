using LifeSimulation.myCs.WorldObjects.Animals.Mating;

namespace LifeSimulation.myCs.WorldObjects.Animals.Eggs
{
    public class EggInformationComponent : InformationComponent
    {
        private EggComponent _eggComponent;

        public EggInformationComponent(WorldObject owner) : base(owner)
        {
        }

        public override void Start()
        {
            base.Start();
            _eggComponent = GetComponent<EggComponent>();
        }

        protected override string GetAllInformation()
        {
            string info = "";
            info += GetInfoAboutCoords() + '\n';
            info += _eggComponent.GetInformation() + '\n';
            
            return info;
        }
    }
}