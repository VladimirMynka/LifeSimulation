using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Plants;

namespace LifeSimulation.myCs.WorldObjects
{
    public class RotComponent : WorldObjectComponent
    {
        private EatableComponent _eatableComponent;
        public RotComponent(WorldObject owner) : base(owner)
        {
            
        }

        public override void Start()
        {
            base.Start();
            _eatableComponent = worldObject.GetComponent<EatableComponent>();
        }

        protected override void OnDestroy()
        {
            SpawnPlant();
        }

        private void SpawnPlant()
        {
            if (_eatableComponent == null)
                PlantsSpawner.SpawnUneatablePlant(worldObject.Cell);
            else if (_eatableComponent.IsPoisonous())
                PlantsSpawner.SpawnPoisonousPlant(worldObject.Cell);
            else
                PlantsSpawner.SpawnNormalPlant(worldObject.Cell);
        }
    }
}