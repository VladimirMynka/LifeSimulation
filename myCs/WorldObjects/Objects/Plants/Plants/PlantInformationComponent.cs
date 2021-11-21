using LifeSimulation.myCs.WorldObjects.CommonComponents;

namespace LifeSimulation.myCs.WorldObjects.Objects.Plants.Plants
{
    public class PlantInformationComponent : InformationComponent
    {
        private PlantAgeComponent _plantAgeComponent;

        public PlantInformationComponent(WorldObject owner) : base(owner)
        {
        }

        public override void Start()
        {
            base.Start();
            _plantAgeComponent = GetComponent<PlantAgeComponent>();
        }

        protected override string GetAllInformation()
        {
            string info = "";
            info += GetInfoAboutCoords() + '\n';
            info += _plantAgeComponent.GetInformation() + '\n';
            
            return info;
        }
    }
}