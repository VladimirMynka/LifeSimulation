namespace LifeSimulation.myCs.WorldObjects
{
    public class Plant : WorldObject
    {
        private int _age;
        private PlantStage _stage;
        private readonly int[] _transitionalAges;
        public int NutritionalValue;

        public Plant(Cell keeper, 
            int newColor = 0, 
            int nutrVal = (int) Defaults.NutritionalValue, 
            int[] transAges = null) 
            : base(keeper, newColor)
        {
            _age = 0;
            _stage = PlantStage.Seed;
            NutritionalValue = nutrVal;
            _transitionalAges = transAges;

            if (_transitionalAges == null)
            {
                _transitionalAges = new int[3];
                _transitionalAges[0] = (int) Defaults.SeedPeriod;
                _transitionalAges[1] = (int) Defaults.PlantTeenagePeriod;
                _transitionalAges[2] = (int) Defaults.PlantDieAge;
            }
        }

        public override void Update()
        {
            _age++;
            if (_age > _transitionalAges[(int)_stage])
            {
                if (_stage == PlantStage.Seed) _stage = PlantStage.CanBeEaten;
                if (_stage == PlantStage.CanBeEaten) _stage = PlantStage.CanBeMother;
                else Die();
            }

            if (_stage == PlantStage.CanBeMother) Reproduce();
        }

        public void Die()
        {
            
        }

        private void Reproduce()
        {
            
        }

        protected override void SetColor(int newColor)
        {
            cell.SetColor(newColor);
            color = newColor;
        }
    }
}