namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Effects
{
    public class EffectComponent : WorldObjectComponent
    {
        protected int timer;
        protected int beginTimer;
        public EffectComponent(WorldObject owner, int period) : base(owner)
        {
            timer = period;
            beginTimer = period;
        }

        public override void Update()
        {
            base.Update();
            timer--;
            if (timer < 0)
                Destroy();
        }
    }
}