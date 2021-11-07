using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Mating
{
    public class PregnantComponent : WorldObjectComponent
    {
        protected EaterComponent eaterComponent;
        private int _ticksToBirthday;

        public PregnantComponent(WorldObject owner, int ticksToBirthday = Defaults.PregnantPeriod) 
            : base(owner)
        {
            _ticksToBirthday = ticksToBirthday;
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
    }
}