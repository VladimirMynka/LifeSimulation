using LifeSimulation.myCs.Settings;

namespace LifeSimulation.myCs.WorldObjects.Plants
{
    public class PlantAgeComponent : AgeComponent
    {
        public PlantAgeComponent(WorldObject owner, Effect effect, int[] transAges = null) : base(owner)
        {
            ageStage = AgeStage.Child;
            transitionalAges = transAges;
            if (transitionalAges.Length == 4) return;
            transitionalAges = new int[4];
            transitionalAges[0] = Defaults.SeedPeriod;
            transitionalAges[1] = Defaults.PlantTeenagePeriod;
            transitionalAges[2] = Defaults.PlantRotAge;
            transitionalAges[3] = Defaults.PlantDieAge;
        }

        protected override void NextStage()
        {
            base.NextStage();
            switch (ageStage)
            {
                case AgeStage.Adult:
                    GrowToAdult();
                    return;
                case AgeStage.Mother:
                    GrowToMother();
                    return;
                case AgeStage.Died:
                    GrowToDyingStage();
                    return;
            }
        }

        protected override int getStageIndex(AgeStage stage)
        {
            switch (stage)
            {
                case AgeStage.Child:
                    return 0;
                case AgeStage.Adult:
                    return 1;
                case AgeStage.Mother:
                    return 2;
                case AgeStage.Died:
                    return 3;
                default:
                    return 0;
            }
        }

        protected override AgeStage getAgeStageByIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return AgeStage.Child;
                case 1:
                    return AgeStage.Adult;
                case 2:
                    return AgeStage.Mother;
                case 3:
                    return AgeStage.Died;
                default:
                    return AgeStage.Child;
            }
        }
        
        private void GrowToAdult()
        {
            if (worldObject.Cell.CurrentObjects[1] == null) 
                worldObject.Cell.SetColor(worldObject.Color);
            if (_effect != Effect.Uneatable)
            {
                worldObject.AddComponent(new EatableComponent(worldObject, MealType.Plant, _effect));
            }
        }
        
        private void GrowToMother()
        {
            worldObject.AddComponent(new PlantReproducerComponent(worldObject));
        }
        
        private void GrowToDyingStage()
        {
            worldObject.Color = Colors.DiedPlant1Const;
            if (worldObject.Cell.CurrentObjects[1] == null) worldObject.Cell.SetColor(worldObject.Color);
        }
    }
}