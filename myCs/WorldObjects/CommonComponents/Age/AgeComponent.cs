﻿using System.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents.Age
{
    public abstract class AgeComponent : WorldObjectComponent, IHaveInformation
    {
        protected int age;
        public AgeStage AgeStage;
        public int[] transitionalAges;
        private int _stageIndex;
        protected Effect effect;
        protected Image image;
        protected int layer;
        private bool _pause;

        protected AgeComponent(
            WorldObject owner, 
            Effect effect,
            Image image,
            int layer) : base(owner)
        {
            this.effect = effect;
            this.image = image;
            this.layer = layer;
        }
        
        public override void Start()
        {
            age = 0;
            _stageIndex = 0;
        }

        public override void Update()
        {
            if (_pause)
                return;
            age++;
            if (age > transitionalAges[_stageIndex])
                NextStage();
        }

        protected virtual void NextStage()
        {
            if (AgeStage == AgeStage.Died)
            {
                WorldObject.Destroy();
                return;
            }
            _stageIndex++;
            AgeStage = GetAgeStageByIndex(_stageIndex);
        }

        protected virtual int GetStageIndex(AgeStage stage)
        {
            return (int) stage;
        }

        protected virtual AgeStage GetAgeStageByIndex(int index)
        {
            return (AgeStage) index;
        }

        public override string ToString()
        {
            var info = "Age: " + age + '/' + transitionalAges[transitionalAges.Length - 1] + '\n';
            info += "Age stage: " + AgeStage;

            return info;
        }

        public int GetInformationPriority()
        {
            return Defaults.InfoPriorityAge;
        }

        public void Wait()
        {
            _pause = true;
        }

        public void StopWait()
        {
            _pause = false;
        }
    }
}