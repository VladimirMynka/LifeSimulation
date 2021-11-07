using System.Drawing;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects
{
    public abstract class AgeComponent : WorldObjectComponent
    {
        protected int age;
        protected AgeStage ageStage;
        public int[] transitionalAges;
        private int _stageIndex;
        protected Effect effect;
        protected Image image;
        protected int layer;

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
            age++;
            if (age > transitionalAges[_stageIndex])
                NextStage();
        }

        protected virtual void NextStage()
        {
            _stageIndex++;
            if (ageStage == AgeStage.Died)
            {
                WorldObject.Destroy();
                return;
            }
            ageStage = GetAgeStageByIndex(_stageIndex);
        }

        protected virtual int GetStageIndex(AgeStage stage)
        {
            return (int) stage;
        }

        protected virtual AgeStage GetAgeStageByIndex(int index)
        {
            return (AgeStage) index;
        }
    }
}