namespace LifeSimulation.myCs.Resources.EatableResources
{
    public class UntypedMealResource : EatableResource
    {
        public UntypedMealResource(int count = 0) : base(count)
        { }
        
        public UntypedMealResource() : base()
        { }

        public override Resource Clone()
        {
            return new UntypedMealResource(GetCount());
        }
    }
}