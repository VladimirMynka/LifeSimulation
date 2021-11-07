﻿using System.Drawing;
using LifeSimulation.myCs.Drawer;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Plants.Plants
{
    public class PlantAgeComponent : AgeComponent
    {
        private DrawableComponent _drawableComponent;
        public PlantAgeComponent(
            WorldObject owner, 
            Effect effect, 
            Image image,
            int layer,
            int[] transAges = null) : base(owner, effect, image, layer)
        {
            ageStage = AgeStage.Child;
            transitionalAges = transAges;

            if (transitionalAges != null && transitionalAges.Length == 4) return;
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
                WorldObject.AddComponent(new EatableComponent(WorldObject, MealType.Plant, effect));
            }
        }
        
        private void GrowToMother()
        {
            WorldObject.AddComponent(new PlantReproducerComponent(WorldObject));
        }
        
        private void GrowToDyingStage()
        {
            _drawableComponent.Image = Pictures.Plant;
        }
    }
}