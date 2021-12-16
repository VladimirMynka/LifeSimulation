using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.DependingOnWeather;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldStructure;
using LifeSimulation.myCs.WorldStructure.Weather;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents
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

        public delegate bool CheckObjectGood(WorldObject worldObject);

        public delegate bool CheckComponentGood<in T>(T worldObjectComponent) where T : class;

        private static T GetComponentFor<T>(Cell cell, CheckComponentGood<T> checker = null) where T : class
        {
            if (cell == null)
                return null;
            
            foreach (var worldObject in cell.CurrentObjects)
            {
                var component = worldObject.GetComponent<T>();
                if (component != null && (checker == null || checker(component))) 
                    return component;
            }
            return null;
        }
            
        public T Search<T>(CheckComponentGood<T> checker) where T : class          
        {
            var x = WorldObject.Cell.Coords[0];
            var y = WorldObject.Cell.Coords[1];
            for (var radius = 0; radius < _visibility; radius++)
            {
                for (var j = 0; j <= radius; j++)
                {
                    var i = radius - j;
                    while (SqrSum(i, j) <= Sqr(radius - 1))
                        i++;
                    while (SqrSum(i, j) <= Sqr(radius))
                    {
                        var component = SearchWithCoords(x, y, i, j, checker);
                        if (component != null)
                            return component;
                        i++;
                    }
                }
            }

            return null;
        }

        private static int Sqr(int x)
        {
            return x * x;
        }

        private static int SqrSum(int x, int y)
        {
            return x * x + y * y;
        }

        private T SearchWithCoords<T>(int x, int y, int localX, int localY, 
            CheckComponentGood<T> checker) where T : class
        {
            var currentCell = world.GetCell(x + localX, y + localY);
            var component = GetComponentFor(currentCell, checker);
            if (component != null)
                return component;
                    
            if (localY != 0)
            {
                currentCell = world.GetCell(x + localX, y - localY);
                component = GetComponentFor(currentCell, checker);
                if (component != null)
                    return component;
            }
                    
            if (localX == 0) 
                return null;
            currentCell = world.GetCell(x - localX, y + localY);
            component = GetComponentFor(currentCell, checker);
            if (component != null)
                return component;
                    
            if (localY == 0) 
                return null;
            currentCell = world.GetCell(x + localX, y + localY);
            component = GetComponentFor(currentCell, checker);
            return component;
        }
        
        public void ConfigureByWeather(Weather weather)
        {
            switch (weather.GetPrecipitation())
            {
                case Precipitation.Sun:
                    _visibility = _normalVisibility;
                    return;
                case Precipitation.Rain:
                    _visibility = _normalVisibility * 3 / 4;
                    return;
                case Precipitation.Fog:
                    _visibility = _normalVisibility / 2;
                    return;
            }
        }
        
        public override string ToString()
        {
            var info = "";
            info += "Visibility: " + _visibility;
            return info;
        }

        public int GetInformationPriority()
        {
            return Defaults.InfoPriorityVisibility;
        }
    }
}