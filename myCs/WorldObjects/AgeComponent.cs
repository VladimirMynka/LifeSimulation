using System.Drawing;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects
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
        private bool pause;

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
            if (pause)
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

        public virtual string GetInformation()
        {
            var info = "Age: " + age + '\n';
            info += "Age stage: " + AgeStage;

            return info;
        }

        public void Wait()
        {
            pause = true;
        }

        public void StopWait()
        {
            pause = false;
        }
    }
}