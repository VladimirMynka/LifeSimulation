namespace LifeSimulation.myCs.Resources.UneatableResources
{
    public class CompostResource : UneatableResource
    {
        public CompostResource(int count = 0) : base(count)
        { }
        
        public CompostResource() : base(0)
        { }

        public override Resource Clone()
        {
            return new CompostResource(GetCount());
        }
    }
}