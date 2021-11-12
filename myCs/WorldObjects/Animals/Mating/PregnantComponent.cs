using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Animals.Animals;
using LifeSimulation.myCs.WorldObjects.Animals.Eggs;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Mating
{
    public class PregnantComponent : WorldObjectComponent
    {
        private EatableComponent _eatableComponent;
        private int _ticksToBirthday;
        private bool _byEggs;

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

        protected override void OnDestroy()
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
            AnimalsSpawner.SpawnWithRandomGender(WorldObject.Cell, _eatableComponent.CreatureType);
        }

        private void SpawnEgg()
        {
            if (_eatableComponent == null) 
                return;
            EggsSpawner.SpawnEggByType(WorldObject.Cell, _eatableComponent.CreatureType);
        }
    }
}