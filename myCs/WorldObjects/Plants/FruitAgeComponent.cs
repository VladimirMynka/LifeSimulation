using LifeSimulation.myCs.Settings;

namespace LifeSimulation.myCs.WorldObjects.Plants
{
    public class FruitAgeComponent : AgeComponent
    {
        public FruitAgeComponent(WorldObject owner, Effect effect, int[] transAges = null) : base(owner)
        {
            ageStage = AgeStage.Child;
            transitionalAges = transAges;
            if (transitionalAges.Length == 2) return;
            transitionalAges = new int[2];
            transitionalAges[0] = Defaults.FruitLivePeriod;
            transitionalAges[1] = Defaults.FruitRotAge;
        }

        public override void Start()
        {
            base.Start();
            if (_effect != Effect.Uneatable)
            {
                worldObject.AddComponent(new EatableComponent(worldObject, MealType.Plant, _effect));
            }
        }

        protected override void NextStage()
        {
            base.NextStage();
            if (ageStage == AgeStage.Died)
                GrowToDyingStage();
        }

        protected override int getStageIndex(AgeStage stage)
        {
            switch (stage)
            {
                case AgeStage.Adult:
                    return 0;
                case AgeStage.Died:
                    return 1;
                default:
                    return 0;
            }
        }

        protected override AgeStage getAgeStageByIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return AgeStage.Adult;
                case 1:
                    return AgeStage.Died;
                default:
                    return AgeStage.Adult;
            }
        }
        
        private void GrowToDyingStage()
        {
            ageStage = AgeStage.Died;
            worldObject.Color = Colors.RotFruit1Const;
            if (worldObject.Cell.CurrentObjects[1] == null) worldObject.Cell.SetColor(worldObject.Color);
        }
    }
}