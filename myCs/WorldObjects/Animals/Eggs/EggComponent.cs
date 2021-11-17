﻿using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Animals.Animals;

namespace LifeSimulation.myCs.WorldObjects.Animals.Eggs
{
    public class EggComponent : WorldObjectComponent, IHaveInformation
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
                WorldObject.Destroy();
        }

        protected override void OnDestroy()
        {
            Animal.SpawnWithRandomGender(WorldObject.Cell, _creatureType);
        }

        public string GetInformation()
        {
            var info = "Type: " + _creatureType + '\n';
            info += "Ticks to birthday: " + _ticksToBirthday;
            return info;
        }
    }
}