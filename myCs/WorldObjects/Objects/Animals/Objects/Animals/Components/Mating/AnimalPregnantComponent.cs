using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Eggs;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals.Components.Mating
{
    public class AnimalPregnantComponent : PregnantComponent
    {
        private EatableComponent _eatableComponent;
        private readonly bool _byEggs;

        public AnimalPregnantComponent(WorldObject owner, 
            int ticksToBirthday = Defaults.PregnantPeriod, 
            bool byEggs = false) 
            : base(owner, ticksToBirthday)
        {
            _byEggs = byEggs;
        }

        public override void Start()
        {
            base.Start();
            _eatableComponent = WorldObject.GetComponent<EatableComponent>();
        }

        public override void OnDestroy()
        {
            if(_byEggs) 
                SpawnEgg();
            else
                SpawnAnimal();
        }

        private void SpawnAnimal()
        {
            if (_eatableComponent == null) 
                return;
            Animal.SpawnWithRandomGender(WorldObject.Cell, _eatableComponent.CreatureType);
        }

        private void SpawnEgg()
        {
            if (_eatableComponent == null) 
                return;
            Egg.SpawnEggByType(WorldObject.Cell, _eatableComponent.CreatureType);
        }
    }
}