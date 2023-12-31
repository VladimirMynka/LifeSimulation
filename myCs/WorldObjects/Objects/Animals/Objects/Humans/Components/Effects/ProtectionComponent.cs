﻿using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Effects
{
    public class ProtectionComponent : EffectComponent
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

        public override void OnDestroy()
        {
            _eatableComponent.SetEffect(Effect.None);
        }
    }
}