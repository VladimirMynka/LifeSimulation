using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.Instruments;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents.Resources
{
    public interface IResourceKeeper<out T> where T : Resource
    {
        bool CanBeExtractUsing(InstrumentType instrumentType);

        T Extract(InstrumentType instrumentType);

        int[] GetCoords();

        WorldObject GetWorldObject();

        bool CheckWereDestroyed();
    }
}