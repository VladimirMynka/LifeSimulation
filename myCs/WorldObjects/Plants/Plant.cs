namespace LifeSimulation.myCs.WorldObjects.Plants
{
    public class Plant : WorldObject
    {
        private int _age;
        private PlantStage _stage;
        private readonly int[] _transitionalAges;
        public int NutritionalValue;

        public Plant(Cell keeper, 
            int newColor = 0, 
            int nutVal = (int) Defaults.NutritionalValue, 
            int[] transAges = null) 
            : base(keeper, newColor)
        {
            _age = 0;
            _stage = PlantStage.Seed;
            NutritionalValue = nutVal;
            _transitionalAges = transAges;

            if (_transitionalAges != null) return;
            _transitionalAges = new int[3];
            _transitionalAges[0] = (int) Defaults.SeedPeriod;
            _transitionalAges[1] = (int) Defaults.PlantTeenagePeriod;
            _transitionalAges[2] = (int) Defaults.PlantDieAge;
        }

        public override void Update()
        {
            _age++;
            if (_age > _transitionalAges[(int)_stage])
            {
                if (_stage == PlantStage.Seed) GrowToAdult();
                if (_stage == PlantStage.CanBeEaten) GrowToMother();
                else Die();
            }

            if (_stage == PlantStage.CanBeMother) Reproduce();
        }

        public void Die()
        {
            if (cell.CurrentObjects[1] == null) cell.ThrowOffColor();
            cell.CurrentObjects[0] = null;
        }
        
        public bool CheckItsAdult()
        {
            return (_stage != PlantStage.Seed); 
        }

        private void GrowToAdult()
        {
            _stage = PlantStage.CanBeEaten;
            if (cell.CurrentObjects[1] == null) cell.SetColor(color);
        }

        private void GrowToMother()
        {
            _stage = PlantStage.CanBeMother;
        }

        private void Reproduce()
        {
            if (World.Random.Next(100) < 80) return;

            var localCoords = Direction.GetRandomDirectionVector();
            var x = localCoords[0] + cell.Coords[0];
            var y = localCoords[1] + cell.Coords[1];
            var neighCell = world.GetCell(x, y);
            if (neighCell == null) return;
            if (neighCell.CurrentObjects[0] != null) return;
            else new Plant(neighCell, color, NutritionalValue, _transitionalAges);
        }
    }
}