namespace LifeSimulation.myCs.WorldObjects
{
    public abstract class AgeComponent : WorldObjectComponent
    {
        protected int age;
        protected AgeStage ageStage;
        public int[] transitionalAges;
        private int _stageIndex;
        protected Effect _effect;

        protected AgeComponent(WorldObject owner) : base(owner)
        {
            
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
                worldObject.Destroy();
                return;
            }
            ageStage = getAgeStageByIndex(_stageIndex);
        }

        protected virtual int getStageIndex(AgeStage stage)
        {
            return (int) stage;
        }

        protected virtual AgeStage getAgeStageByIndex(int index)
        {
            return (AgeStage) index;
        }
    }
}