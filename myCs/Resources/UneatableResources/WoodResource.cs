namespace LifeSimulation.myCs.Resources.UneatableResources
{
    public class WoodResource : UneatableResource
    {
        public WoodResource(int count = 0) : base(count)
        { }
        
        public WoodResource() : base(0)
        { }

        public override Resource Clone()
        {
            return new WoodResource(GetCount());
        }
    }
}