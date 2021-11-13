using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Animals
{
    public class AnimalInformationComponent : InformationComponent
    {
        private EatableComponent _eatableComponent;
        public AnimalInformationComponent(WorldObject owner) : base(owner)
        {
        }

        public override void Start()
        {
            base.Start();
            _eatableComponent = GetComponent<EatableComponent>();
        }

        protected override string GetAllInformation()
        {
            return WorldObject.Cell.Coords[0].ToString() + 
                   ':' + WorldObject.Cell.Coords[1].ToString() +
                   '\n' + _eatableComponent.CreatureType.ToString() + 
                   '\n' + '\n';
        }
    }
}