using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public class EggComponent : WorldObjectComponent
    {
        private int _ticksToBirthday;
        private readonly MealType _mealType;

        public EggComponent(
            WorldObject owner, 
            int ticksToBirthday = Defaults.AnimalEggPeriod, 
            MealType mealType = MealType.Plant) 
            : base(owner)
        {
            _ticksToBirthday = ticksToBirthday;
            _mealType = mealType;
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
            bool childIsMale = (World.World.Random.Next(2) == 1);
            switch (_mealType)
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
    }
}