namespace LifeSimulation.myCs.WorldObjects.Plants.Fruits
{
    public class FruitInformationComponent : InformationComponent
    {
        private FruitAgeComponent _fruitAgeComponent;

        public FruitInformationComponent(WorldObject owner) : base(owner)
        {
        }

        public override void Start()
        {
            base.Start();
            _fruitAgeComponent = GetComponent<FruitAgeComponent>();
        }

        protected override string GetAllInformation()
        {
            string info = "";
            info += GetInfoAboutCoords() + '\n';
            info += _fruitAgeComponent.GetInformation() + '\n';
            
            return info;
        }
    }
}