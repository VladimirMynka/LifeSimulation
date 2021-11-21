using LifeSimulation.myCs.WorldObjects.CommonComponents;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.RotMeat.Components
{
    public class RotMeatInformationComponent : InformationComponent
    {
        private RotMeatComponent _rotMeatComponent;

        public RotMeatInformationComponent(WorldObject owner) : base(owner)
        {
        }

        public override void Start()
        {
            base.Start();
            _rotMeatComponent = GetComponent<RotMeatComponent>();
        }

        protected override string GetAllInformation()
        {
            string info = "";
            info += GetInfoAboutCoords() + '\n';
            info += _rotMeatComponent.ToString() + '\n';
            
            return info;
        }
    }
}