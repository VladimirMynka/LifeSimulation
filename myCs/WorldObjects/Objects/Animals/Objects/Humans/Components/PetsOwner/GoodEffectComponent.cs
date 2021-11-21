namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.PetsOwner
{
    public class GoodEffectComponent : WorldObjectComponent
    {
        private int _timer;
        public GoodEffectComponent(WorldObject owner, int period) : base(owner)
        {
            _timer = period;
        }

        public override void Update()
        {
            base.Update();
            _timer--;
            if (_timer < 0)
                Destroy();
        }
    }
}