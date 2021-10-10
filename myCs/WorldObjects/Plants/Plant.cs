namespace LifeSimulation.myCs.WorldObjects.Plants
{
    public class Plant : AbstractPlant
    {
        
        public Plant(
            Cell keeper, 
            int newColor = Colors.Plant1Const,
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
            if (transitionalAges != null) return;
            transitionalAges = new int[4];
            transitionalAges[0] = Defaults.SeedPeriod;
            transitionalAges[1] = Defaults.PlantTeenagePeriod;
            transitionalAges[2] = Defaults.PlantDieAge;
            transitionalAges[3] = Defaults.PlantRotAge;
        }

        public override void Update()
        {
            base.Update();
            if (age > transitionalAges[(int)stage])
            {
                switch (stage)
                {
                    case PlantStage.Seed:
                        GrowToAdult();
                        break;
                    case PlantStage.CanBeEaten:
                        GrowToMother();
                        break;
                    case PlantStage.CanBeMother:
                        GrowToDyingStage();
                        break;
                    case PlantStage.Died:
                        Die();
                        break;
                    default:
                        Die();
                        break;
                }
            }

            if (stage == PlantStage.CanBeMother) Reproduce();
        }

        public bool CheckItsAdult()
        {
            return (stage != PlantStage.Seed); 
        }

        private void GrowToAdult()
        {
            stage = PlantStage.CanBeEaten;
            if (cell.CurrentObjects[1] == null) cell.SetColor(color);
        }

        private void GrowToMother()
        {
            stage = PlantStage.CanBeMother;
        }

        private void GrowToDyingStage()
        {
            stage = PlantStage.Died;
            color = Colors.DiedPlant1Const;
            if (cell.CurrentObjects[1] == null) cell.SetColor(color);
        }

        private void Reproduce()
        {
            if (World.Random.Next(100) > Defaults.ReproduceChance) return;

            var localCoords = Direction.GetRandomDirectionVector();
            var x = localCoords[0] + cell.Coords[0];
            var y = localCoords[1] + cell.Coords[1];
            var neighCell = world.GetCell(x, y);
            if (neighCell == null) return;
            if (neighCell.CurrentObjects[0] != null) return;

            if (World.Random.Next(2) == 0)
                new Plant(neighCell, color, isEatable, Effect, NutritionalValue, transitionalAges);
            else
            {
                var fruitColor = Colors.NormalFruit1Const;
                if (!isEatable) fruitColor = Colors.UneatableFruit1Const;
                else if (Effect == Effect.None) fruitColor = Colors.NormalFruit1Const;
                new Fruit(neighCell, fruitColor, isEatable, Effect);
            }
        }
    }
}