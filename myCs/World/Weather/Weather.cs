using System;
using System.Drawing;
using LifeSimulation.myCs.WorldObjects;

namespace LifeSimulation.myCs.World.Weather
{
    public class Weather : IHaveInformation
    {
        private int _dayOfYear;
        private int _year;
        private int _temperature;
        private Season _season;
        private Precipitation _precipitation;
        private readonly Drawer.Drawer _drawer;

        public Weather(Drawer.Drawer drawer)
        {
            _dayOfYear = 250;
            _year = 0;
            _temperature = 20;
            _season = Season.Autumn;
            _precipitation = Precipitation.Sun;
            _drawer = drawer;
            drawer.SetBackground(Brushes.Gold);
        }

        public void Update()
        {
            IncreaseDay();
            ChangeTemperature();
            ChangePrecipitation();
        }
        
        private void IncreaseDay()
        {
            _dayOfYear++;
            if (_dayOfYear > 365)
            {
                _dayOfYear = 0;
                _year++;
            }
            ChangeSeason();
        }
        
        private void ChangeTemperature()
        {
            switch (_season)
            {
                case Season.Winter:
                    _temperature = World.SmoothRandom(_temperature, 20, 20, -40, 0);
                    return;
                case Season.Autumn:
                    _temperature = World.SmoothRandom(_temperature, 10, 5, -20, 20);
                    return;
                case Season.Spring:
                    _temperature = World.SmoothRandom(_temperature, 5, 10, -20, 20);
                    return;
                default:    
                    _temperature = World.SmoothRandom(_temperature, 20, 20, 0, 40);
                    return;
            }
        }
        
        private void ChangeSeason()
        {
            switch (_dayOfYear)
            {
                case 59:
                    _season = Season.Spring;
                    _drawer.SetBackground(Brushes.Chocolate);
                    return;
                case 151:
                    _season = Season.Summer;
                    _drawer.SetBackground(Brushes.Chartreuse);
                    return;
                case 243:
                    _season = Season.Autumn;
                    _drawer.SetBackground(Brushes.Gold);
                    return;
                case 334:
                    _season = Season.Winter;
                    _drawer.SetBackground(Brushes.Beige);
                    return;
                default:
                    return;
            }
        }

        private void ChangePrecipitation()
        {
            var temperatureInfluence = Math.Abs(_temperature) < 20 ? 2 : 1;
            var previousInfluence = _precipitation == Precipitation.Rain ? 4
                : _precipitation == Precipitation.Fog ? 2
                : 1;
            var random = World.Random.Next(1, 9);
            var result = temperatureInfluence * previousInfluence * random;
            if (result >= 24)
                SetRain();
            else if (result >= 8)
                SetFog();
            else
                SetSun();
        }

        private void SetRain()
        {
            _precipitation = Precipitation.Rain;
        }

        private void SetFog()
        {
            _precipitation = Precipitation.Fog;
        }

        private void SetSun()
        {
            _precipitation = Precipitation.Sun;
        }

        public int GetTemperature()
        {
            return _temperature;
        }

        public Season GetSeason()
        {
            return _season;
        }

        public Precipitation GetPrecipitation()
        {
            return _precipitation;
        }

        public string GetInformation()
        {
            var info = "";
            info += "Day of year: " + _dayOfYear + '\n';
            info += "Year: " + _year + '\n';
            info += "Season: " + _season + '\n';
            info += "Precipitation: " + _precipitation + '\n';
            info += "Temperature: " + _temperature;
            return info;
        }
    }
}