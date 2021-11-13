using System.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Animals.Mating;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Animals
{
    public class AnimalAgeComponent : AgeComponent
    {
        private readonly bool _isMale;
        private readonly CreatureType _creatureType;
        
        public AnimalAgeComponent(
            WorldObject owner,
            CreatureType creatureType,
            Effect effect, 
            bool isMale,
            Image image,
            int layer,
            int[] transAges = null) : base(owner, effect, image, layer)
        {
            _isMale = isMale;
            ageStage = AgeStage.Adult;
            _creatureType = creatureType;
            transitionalAges = transAges;

            if (transitionalAges != null && transitionalAges.Length == 2) return;
            transitionalAges = new int[2];
            transitionalAges[0] = Defaults.AnimalTeenagePeriod;
            transitionalAges[1] = Defaults.AnimalDiedAge;
        }

        protected override void NextStage()
        {
            base.NextStage();
            switch (ageStage)
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

        private void GrowToMother()
        {
            if (_isMale)
                WorldObject.AddComponent(new MaleMatingComponent(WorldObject));
            else 
                WorldObject.AddComponent(new FemaleMatingComponent(WorldObject));
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            new RotMeat.RotMeat(WorldObject.Cell, image, _creatureType, layer);
        }
    }
}