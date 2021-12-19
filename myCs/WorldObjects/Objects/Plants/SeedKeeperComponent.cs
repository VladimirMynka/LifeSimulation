using System;
using LifeSimulation.myCs.Resources.UneatableResources;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;

namespace LifeSimulation.myCs.WorldObjects.Objects.Plants
{
    public class SeedKeeperComponent : WorldObjectComponent, IHaveInformation
    {
        private int _currentSeedCount;
        public const int MaxSeedCount = 10;
        private int _forSeedAddTimer = 5;
        private readonly CreatureType _creatureType;
        public SeedKeeperComponent(WorldObject owner, CreatureType creatureType) : base(owner)
        {
            _creatureType = creatureType;
        }
        
        public override void Update()
        {
            _forSeedAddTimer--;
            if (_forSeedAddTimer <= 0)
            {
                _currentSeedCount = Math.Min(_currentSeedCount + 1, MaxSeedCount);
                _forSeedAddTimer = 5;
            }
        }
        
        public SeedResource GetResource()
        {
            var forReturn = _currentSeedCount;
            _currentSeedCount = 0;
            return new SeedResource(forReturn, _creatureType);
        }

        public bool IsRipe()
        {
            return _currentSeedCount >= 5;
        }

        public int GetCount()
        {
            return _currentSeedCount;
        }

        public int GetInformationPriority()
        {
            return Defaults.InfoPriorityResourceKeeper;
        }

        public override string ToString()
        {
            return "Seeds count: " + _currentSeedCount;
        }
    }
}