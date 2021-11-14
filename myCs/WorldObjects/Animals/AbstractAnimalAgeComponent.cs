using System.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Animals.Mating;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public abstract class AbstractAnimalAgeComponent : AgeComponent
    {
        private readonly bool _isMale;
        private readonly CreatureType _creatureType;
        private readonly int _pregnantPeriod;
        private readonly bool _byEggs;
        
        public AbstractAnimalAgeComponent(
            WorldObject owner,
            CreatureType creatureType,
            Effect effect, 
            bool isMale,
            Image image,
            int layer,
            int pregnantPeriod,
            bool byEggs,
            int[] transAges = null) : base(owner, effect, image, layer)
        {
            _isMale = isMale;
            AgeStage = AgeStage.Adult;
            _creatureType = creatureType;
            transitionalAges = transAges;
            _pregnantPeriod = pregnantPeriod;
            _byEggs = byEggs;

            if (transitionalAges != null && transitionalAges.Length == 3) return;
            transitionalAges = new int[3];
            transitionalAges[0] = Defaults.AnimalTeenagePeriod;
            transitionalAges[1] = Defaults.AnimalDiedAge;
            transitionalAges[2] = Defaults.AnimalDiedAge;
        }

        protected override void NextStage()
        {
            base.NextStage();
            switch (AgeStage)
            {
                case AgeStage.Mother:
                    GrowToMother();
                    return;
            }
        }

        protected override int GetStageIndex(AgeStage stage)
        {
            switch (stage)
            {
                case AgeStage.Adult:
                    return 0;
                case AgeStage.Mother:
                    return 1;
                case AgeStage.Died:
                    return 2;
                default:
                    return 0;
            }
        }

        protected override AgeStage GetAgeStageByIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return AgeStage.Adult;
                case 1:
                    return AgeStage.Mother;
                case 2:
                    return AgeStage.Died;
                default:
                    return AgeStage.Adult;
            }
        }

        protected virtual void GrowToMother()
        {
            if (_isMale)
                AddMaleComponent();
            else 
                AddFemaleComponent(_byEggs, _pregnantPeriod);
        }

        protected abstract void AddMaleComponent();
        protected abstract void AddFemaleComponent(bool byEggs, int pregnantPeriod);

        protected override void OnDestroy()
        {
            base.OnDestroy();
            new RotMeat.RotMeat(WorldObject.Cell, image, _creatureType, layer);
        }
    }
}