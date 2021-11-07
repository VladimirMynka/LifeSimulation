using System.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Plants.Fruits
{
    public class FruitAgeComponent : AgeComponent
    {
        private DrawableComponent _drawableComponent;
        public FruitAgeComponent(
            WorldObject owner, 
            Effect effect, 
            Image image,
            int layer,
            int[] transAges = null) : base(owner, effect, image, layer)
        {
            ageStage = AgeStage.Child;
            transitionalAges = transAges;
            if (transitionalAges != null && transitionalAges.Length == 2) return;
            transitionalAges = new int[2];
            transitionalAges[0] = Defaults.FruitLivePeriod;
            transitionalAges[1] = Defaults.FruitRotAge;
        }

        public override void Start()
        {
            base.Start();
            _drawableComponent = new DrawableComponent(WorldObject, image, layer);
            if (effect != Effect.Uneatable)
            {
                WorldObject.AddComponent(new EatableComponent(WorldObject, MealType.Plant, effect));
            }
        }

        protected override void NextStage()
        {
            base.NextStage();
            if (ageStage == AgeStage.Died)
                GrowToDyingStage();
        }

        protected override int GetStageIndex(AgeStage stage)
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

        protected override AgeStage GetAgeStageByIndex(int index)
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
            //WorldObject.Color = Colors.RotFruit1Const;
        }
    }
}