namespace LifeSimulation.myCs.WorldObjects.Animals.RotMeat
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
            info += _rotMeatComponent.GetInformation() + '\n';
            
            return info;
        }
    }
}