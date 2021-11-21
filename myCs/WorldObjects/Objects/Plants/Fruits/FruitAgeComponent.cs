using System.Drawing;
using LifeSimulation.myCs.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Objects.Plants.Fruits
{
    public class FruitAgeComponent : AgeComponent
    {
        private DrawableComponent _drawableComponent;
        private readonly CreatureType _creatureType;
        public FruitAgeComponent(
            WorldObject owner, 
            CreatureType creatureType,
            Effect effect, 
            Image image,
            int layer,
            int[] transAges = null) : base(owner, effect, image, layer)
        {
            AgeStage = AgeStage.Adult;
            transitionalAges = transAges;
            _creatureType = creatureType;
            if (transitionalAges != null && transitionalAges.Length == 2) return;
            transitionalAges = new int[2];
            transitionalAges[0] = Defaults.FruitLivePeriod;
            transitionalAges[1] = Defaults.FruitRotAge;
        }

        public override void Start()
        {
            base.Start();
            _drawableComponent = new DrawableComponent(WorldObject, image, layer);
            WorldObject.AddComponent(_drawableComponent);
            if (effect != Effect.Uneatable)
            {
                WorldObject.AddComponent(new EatableComponent(WorldObject, _creatureType, MealType.Plant, effect));
            }
            WorldObject.Cell.ReportAboutUpdating();
        }

        protected override void NextStage()
        {
            base.NextStage();
            if (AgeStage == AgeStage.Died)
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
            _drawableComponent.Image = Pictures.Fruit;
            if (WorldObject != null && WorldObject.Cell != null)
                WorldObject.Cell.ReportAboutUpdating();
        }

        public override string GetInformation()
        {
            var info = "Type: " + _creatureType + "(fruit)\n";
            info += "Effect: " + effect + '\n';
            info += base.GetInformation();
            return info;
        }
    }
}