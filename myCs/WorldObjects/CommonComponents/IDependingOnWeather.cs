using LifeSimulation.myCs.WorldStructure.Weather;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents
{
    public interface IDependingOnWeather
    {
        void ConfigureByWeather(Weather weather);
    }
}