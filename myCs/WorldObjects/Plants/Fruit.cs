namespace LifeSimulation.myCs.WorldObjects.Plants
{
    public class Fruit : AbstractPlant
    {
        
        public Fruit(
            Cell keeper, 
            int newColor = Colors.NormalFruit1Const,
            bool isEatable = true,
            Effect effect = Effect.None,
            int nutVal = Defaults.NutritionalValue, 
            int[] transAges = null
            ) : base(
            keeper, 
            newColor,
            isEatable,
            effect,
            nutVal,
            transAges
            )
        {
            stage = PlantStage.CanBeEaten;
            if (cell.CurrentObjects[1] == null) cell.SetColor(newColor);

            if (transitionalAges != null) return;
            transitionalAges = new int[2];
            transitionalAges[0] = Defaults.FruitLivePeriod;
            transitionalAges[1] = Defaults.FruitRotAge;
        }

        public override void Update()
        {
            base.Update();
            if (age > transitionalAges[0] && stage == PlantStage.CanBeEaten) GrowToDyingStage();
            else if (age > transitionalAges[1] && stage == PlantStage.Died) RotAtAll();
        }

        private void GrowToDyingStage()
        {
            stage = PlantStage.Died;
            color = Colors.RotFruit1Const;
            if (cell.CurrentObjects[1] == null) cell.SetColor(color);
        }

        private void RotAtAll()
        {
            var thisCell = cell;
            Die();
            
            var plantColor = Colors.Plant1Const;
            if (!isEatable) plantColor = Colors.Tree1Const;
            else if (Effect == Effect.Heart) plantColor = Colors.Poisonous1Const;
            new Plant(thisCell, plantColor, isEatable, Effect);
        }
    }
}