using LifeSimulation.myCs.WorldStructure.Weather;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents.DependingOnWeather
{
    public interface IDependingOnWeather
    {
        void ConfigureByWeather(Weather weather);
    }
}