using LifeSimulation.myCs.World.Weather;

namespace LifeSimulation.myCs.WorldObjects
{
    public interface IDependingOnWeather
    {
        void ConfigureByWeather(Weather weather);
    }
}