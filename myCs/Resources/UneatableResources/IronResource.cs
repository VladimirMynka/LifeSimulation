namespace LifeSimulation.myCs.Resources.UneatableResources
{
    public class IronResource : UneatableResource
    {
        public IronResource(int count = 0) : base(count)
        { }
        
        public IronResource() : base(0)
        { }

        public override Resource Clone()
        {
            return new IronResource(GetCount());
        }
    }
}