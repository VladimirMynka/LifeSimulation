using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Eggs;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating
{
    public class PregnantComponent : WorldObjectComponent, IHaveInformation
    {
        private EatableComponent _eatableComponent;
        private int _ticksToBirthday;
        private readonly bool _byEggs;

        public PregnantComponent(WorldObject owner, 
            int ticksToBirthday = Defaults.PregnantPeriod, 
            bool byEggs = false) 
            : base(owner)
        {
            _ticksToBirthday = ticksToBirthday;
            _byEggs = byEggs;
        }

        public override void Start()
        {
            base.Start();
            _eatableComponent = WorldObject.GetComponent<EatableComponent>();
        }

        public override void Update()
        {
            base.Update();
            if (_ticksToBirthday > 0)
                _ticksToBirthday--;
            else
                Destroy();
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

        public override string ToString()
        {
            var info = "";
            info += "Ticks to birthday: " + _ticksToBirthday;
            return info;
        }

        public int GetInformationPriority()
        {
            return 6;
        }
    }
}