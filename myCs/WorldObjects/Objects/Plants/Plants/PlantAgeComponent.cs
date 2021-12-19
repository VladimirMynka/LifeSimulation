using System.Drawing;
using LifeSimulation.myCs.Drawing;
using LifeSimulation.myCs.Resources.Instruments;
using LifeSimulation.myCs.Resources.UneatableResources;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Age;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;

namespace LifeSimulation.myCs.WorldObjects.Objects.Plants.Plants
{
    public class PlantAgeComponent : AgeComponent
    {
        private DrawableComponent _drawableComponent;
        private EatableComponent _eatableComponent;
        private ResourceKeeperComponent<WoodResource> _woodComponent;
        private SeedKeeperComponent _seedKeeperComponent;
        public readonly CreatureType CreatureType;
        public PlantAgeComponent(
            WorldObject owner, 
            CreatureType creatureType,
            Effect effect, 
            Image image,
            int layer,
            int[] transAges = null) : base(owner, effect, image, layer)
        {
            CreatureType = creatureType;
            AgeStage = AgeStage.Child;
            transitionalAges = transAges;

            if (transitionalAges != null && transitionalAges.Length == 4) return;
            transitionalAges = new int[4];
            transitionalAges[0] = Defaults.SeedPeriod;
            transitionalAges[1] = Defaults.PlantTeenagePeriod;
            transitionalAges[2] = Defaults.PlantDieAge;
            transitionalAges[3] = Defaults.PlantRotAge;
        }

        protected override void NextStage()
        {
            base.NextStage();
            switch (AgeStage)
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

        protected override int GetStageIndex(AgeStage stage)
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

        protected override AgeStage GetAgeStageByIndex(int index)
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
            _drawableComponent = new DrawableComponent(WorldObject, image, layer);
            WorldObject.AddComponent(_drawableComponent);
            if (effect != Effect.Uneatable)
            {
                _eatableComponent = new EatableComponent(WorldObject, CreatureType, MealType.Plant, effect);
                WorldObject.AddComponent(_eatableComponent);
            }
            else
            {
                _woodComponent = new ResourceKeeperComponent<WoodResource>(
                    WorldObject,
                    new WoodResource(),
                    50,
                    new InstrumentType[]{InstrumentType.Axe});
                WorldObject.AddComponent(_woodComponent);
            }

            WorldObject.Cell.ReportAboutUpdating();
        }
        
        private void GrowToMother()
        {
            _seedKeeperComponent = new SeedKeeperComponent(WorldObject, CreatureType);
            WorldObject.AddComponent(_seedKeeperComponent);
            WorldObject.AddComponent(new PlantReproducerComponent(WorldObject));
        }
        
        private void GrowToDyingStage()
        {
            _drawableComponent.Image = Pictures.DeadPlant;
            WorldObject.RemoveComponent(_seedKeeperComponent);
            if (effect != Effect.Uneatable)
            {
                WorldObject.RemoveComponent(_eatableComponent);
                _eatableComponent = null;
            }
            else
            {
                WorldObject.RemoveComponent(_woodComponent);
                _woodComponent = null;
                WorldObject.AddComponent(new ResourceKeeperComponent<WoodResource>(WorldObject,
                    new WoodResource(), 20, new InstrumentType[]{InstrumentType.None}));
            }
            if (!CheckWereDestroyed(this))
                WorldObject.Cell.ReportAboutUpdating();
        }

        public override string ToString()
        {
            var info = "Type: " + CreatureType + '\n';
            info += "On eat effect: " + effect + '\n';
            info += "Now eatable: ";
            if (_eatableComponent == null)
                info += "no";
            else
                info += "yes";
            info += '\n';
                info += base.ToString();
            return info;
        }
    }
}