using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Mating
{
    public class PregnantComponent : WorldObjectComponent
    {
        protected EaterComponent eaterComponent;
        private int _ticksToBirthday;
        private bool _byEggs;

        public PregnantComponent(WorldObject owner, 
            int ticksToBirthday = Defaults.PregnantPeriod, 
            bool byEggs = false) 
            : base(owner)
        {
            _ticksToBirthday = ticksToBirthday;
            _byEggs = byEggs;
        }

        public override void Start()
        {
            base.Start();
            eaterComponent = WorldObject.GetComponent<EaterComponent>();
        }

        public override void Update()
        {
            base.Update();
            if (_ticksToBirthday > 0)
                _ticksToBirthday--;
            else
                Destroy();
        }

        protected override void OnDestroy()
        {
            if(_byEggs) 
                SpawnEgg();
            else
                SpawnAnimal();
        }

        private void SpawnAnimal()
        {
            if (eaterComponent == null) 
                return;
            bool childIsMale = (World.World.Random.Next(2) == 1);
            switch (eaterComponent.MealType)
            {
                case MealType.Plant:
                    AnimalsSpawner.SpawnHerbivoreAnimal(WorldObject.Cell, childIsMale);
                    return;
                case MealType.AllTypes:
                    AnimalsSpawner.SpawnOmnivoreAnimal(WorldObject.Cell, childIsMale);
                    return;
                case MealType.FreshMeat:
                    AnimalsSpawner.SpawnPredatorAnimal(WorldObject.Cell, childIsMale);
                    return;
                case MealType.DeadMeat:
                    AnimalsSpawner.SpawnScavengerAnimal(WorldObject.Cell, childIsMale);
                    return;
            }
        }

        private void SpawnEgg()
        {
            if (eaterComponent == null) 
                return;
            switch (eaterComponent.MealType)
            {
                case MealType.Plant:
                    EggsSpawner.SpawnHerbivoreEgg(WorldObject.Cell);
                    return;
                case MealType.AllTypes:
                    EggsSpawner.SpawnOmnivoreEgg(WorldObject.Cell);
                    return;
                case MealType.FreshMeat:
                    EggsSpawner.SpawnPredatorEgg(WorldObject.Cell);
                    return;
                case MealType.DeadMeat:
                    EggsSpawner.SpawnScavengerEgg(WorldObject.Cell);
                    return;
            }
        }
    }
}