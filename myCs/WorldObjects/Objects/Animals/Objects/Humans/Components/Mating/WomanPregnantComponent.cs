using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Behaviour;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Mating
{
    public class WomanPregnantComponent : PregnantComponent, IHaveTarget
    {
        private WarehousesOwnerComponent _warehousesOwnerComponent;
        private CitizenComponent _citizenComponent;
        
        public WomanPregnantComponent(WorldObject owner, int ticksToBirthday) 
            : base(owner, ticksToBirthday)
        {
        }

        public override void Start()
        {
            base.Start();
            _warehousesOwnerComponent = GetComponent<WarehousesOwnerComponent>();
            _citizenComponent = GetComponent<CitizenComponent>();
        }

        protected override bool IsReadyToGiveBirth()
        {
            return base.IsReadyToGiveBirth() && OnOneCellWith(_warehousesOwnerComponent.House);
        }

        public override void OnDestroy()
        {
            if (!IsReadyToGiveBirth())
                return;
            var child = Human.SpawnHumanWithRandomGender(WorldObject.Cell);
            var childCitizen = new CitizenComponent(child, _citizenComponent.Village);
            child.AddComponent(childCitizen);
            child.GetComponent<WarehousesOwnerComponent>().House = _warehousesOwnerComponent.House;
            _citizenComponent.Village.AddNewCitizen(childCitizen);
        }

        public int GetPriorityInBehaviour()
        {
            return Defaults.BehaviourHumanPregnant;
        }

        public WorldObject GetTarget()
        {
            return _warehousesOwnerComponent.House;
        }
    }
}