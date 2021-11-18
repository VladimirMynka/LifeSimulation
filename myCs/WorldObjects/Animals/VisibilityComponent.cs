using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World.Weather;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public class VisibilityComponent : WorldObjectComponent, IDependingOnWeather, IHaveInformation
    {
        private int _visibility;
        private readonly int _normalVisibility;
        
        public VisibilityComponent(WorldObject owner, int visibility) : base(owner)
        {
            _visibility = visibility;
            _normalVisibility = visibility;
        }

        public int GetVisibility()
        {
            return _visibility;
        }
        
        public void ConfigureByWeather(Weather weather)
        {
            switch (weather.GetPrecipitation())
            {
                case Precipitation.Sun:
                    _visibility = _normalVisibility;
                    return;
                case Precipitation.Rain:
                    _visibility = _normalVisibility / 2;
                    return;
                case Precipitation.Fog:
                    _visibility = _normalVisibility / 4;
                    return;
            }
        }
        
        //delegate bool ItIsGood(
        public string GetInformation()
        {
            var info = "";
            info += "Visibility: " + _visibility;
            return info;
        }
    }
}