using LifeSimulation.myCs.WorldObjects.CommonComponents;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Eggs.Components
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
            info += _eggComponent.ToString() + '\n';
            
            return info;
        }
    }
}