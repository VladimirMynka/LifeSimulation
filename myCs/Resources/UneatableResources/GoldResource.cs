namespace LifeSimulation.myCs.Resources.UneatableResources
{
    public class GoldResource : UneatableResource
    {
        public GoldResource(int count) : base(count)
        { }
        
        public GoldResource() : base(0)
        { }

        public override Resource Clone()
        {
            return new GoldResource(GetCount());
        }
    }
}