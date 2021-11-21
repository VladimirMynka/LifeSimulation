using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.PetsOwner
{
    public class ProtectionComponent : GoodEffectComponent
    {
        private EatableComponent _eatableComponent;
        public ProtectionComponent(WorldObject owner, int period) : base(owner, period)
        {
        }

        public override void Start()
        {
            base.Start();
            _eatableComponent = GetComponent<EatableComponent>();
            _eatableComponent.SetEffect(Effect.Uneatable);
        }

        protected override void OnDestroy()
        {
            _eatableComponent.SetEffect(Effect.None);
        }
    }
}