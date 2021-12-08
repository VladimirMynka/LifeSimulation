using System.Collections.Generic;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents
{
    public class HaveInformationComparer : IComparer<IHaveInformation>
    {
        public int Compare(IHaveInformation x, IHaveInformation y)
        {
            if (x != null && y != null) 
                return x.GetInformationPriority() - y.GetInformationPriority();
            if (x == null)
                return -1;
            return 1;
        }
    }
}