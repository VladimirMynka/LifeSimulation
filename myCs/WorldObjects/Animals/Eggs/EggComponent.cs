using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Animals.Animals;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Eggs
{
    public class EggComponent : WorldObjectComponent
    {
        private int _ticksToBirthday;
        private readonly CreatureType _creatureType;

        public EggComponent(
            WorldObject owner, 
            CreatureType creatureType,
            int ticksToBirthday = Defaults.AnimalEggPeriod 
            ) 
            : base(owner)
        {
            _ticksToBirthday = ticksToBirthday;
            _creatureType = creatureType;
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
            AnimalsSpawner.SpawnWithRandomGender(WorldObject.Cell, _creatureType);
        }

        public string GetInformation()
        {
            var info = "Type: " + _creatureType + '\n';
            info = "Ticks to birthday: " + _ticksToBirthday;
            return info;
        }
    }
}