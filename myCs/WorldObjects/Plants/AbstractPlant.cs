namespace LifeSimulation.myCs.WorldObjects.Plants
{
    public abstract class AbstractPlant : WorldObject
    {
        protected int age;
        protected PlantStage stage;
        protected int[] transitionalAges;
        protected readonly bool isEatable;
        public readonly Effect Effect;
        public int NutritionalValue;

        protected AbstractPlant(
            Cell keeper, 
            int newColor = Colors.Plant1Const,
            bool isEatable = true,
            Effect effect = Effect.None,
            int nutVal = Defaults.NutritionalValue, 
            int[] transAges = null
            ) : base(keeper, newColor)
        {
            age = 0;
            stage = PlantStage.Seed;
            this.isEatable = isEatable;
            Effect = effect;
            NutritionalValue = nutVal;
            transitionalAges = transAges;
            color = newColor;
        }

        public override void Update()
        {
            base.Update();
            age++;
        }

        public bool IsEatable()
        {
            return (isEatable && stage >= PlantStage.CanBeEaten && stage <= PlantStage.CanBeMother);
        }

        public void Die()
        {
            if (cell.CurrentObjects[1] == null) cell.ThrowOffColor();
            cell.CurrentObjects[0] = null;
        }
    }
}