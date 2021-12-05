using System.Collections.Generic;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
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

        public delegate bool CheckComponentGood<T>(T worldObjectComponent) where T : WorldObjectComponent;
        
        public delegate bool CheckSomethingGood<in T>(T something) where T : class;

        private static T GetComponentFor<T>(Cell cell, CheckComponentGood<T> checker = null) where T : WorldObjectComponent
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
        
        private static T GetComponentOf<T>(Cell cell, CheckSomethingGood<T> checker = null) where T : class
        {
            if (cell == null)
                return null;
            
            foreach (var worldObject in cell.CurrentObjects)
            {
                var component = worldObject.GetComponentOf<T>();
                if (component != null && (checker == null || checker(component))) 
                    return component;
            }
            return null;
        }
            
        public T Search<T>(CheckComponentGood<T> checker) where T : WorldObjectComponent          
        {
            var x = WorldObject.Cell.Coords[0];
            var y = WorldObject.Cell.Coords[1];
            for (var radius = 0; radius < _visibility; radius++)
            {
                for (var j = 0; j <= radius; j++)
                {
                    var i = radius - j;
                    var currentCell = world.GetCell(x + i, y + j);
                    var component = GetComponentFor(currentCell, checker);
                    if (component != null)
                        return component;
                    
                    if (j != 0)
                    {
                        currentCell = world.GetCell(x + i, y - j);
                        component = GetComponentFor(currentCell, checker);
                        if (component != null)
                            return component;
                    }
                    
                    if (i == 0) 
                        continue;
                    currentCell = world.GetCell(x - i, y + j);
                    component = GetComponentFor(currentCell, checker);
                    if (component != null)
                        return component;
                    
                    if (j == 0) 
                        continue;
                    currentCell = world.GetCell(x + i, y + j);
                    component = GetComponentFor(currentCell, checker);
                    if (component != null)
                        return component;
                }
            }

            return null;
        }
        
        public T SearchOf<T>(CheckSomethingGood<T> checker) where T : class
        {
            var x = WorldObject.Cell.Coords[0];
            var y = WorldObject.Cell.Coords[1];
            for (var radius = 0; radius < _visibility; radius++)
            {
                for (var j = 0; j <= radius; j++)
                {
                    var i = radius - j;
                    var currentCell = world.GetCell(x + i, y + j);
                    var component = GetComponentOf(currentCell, checker);
                    if (component != null)
                        return component;
                    
                    if (j != 0)
                    {
                        currentCell = world.GetCell(x + i, y - j);
                        component = GetComponentOf(currentCell, checker);
                        if (component != null)
                            return component;
                    }
                    
                    if (i == 0) 
                        continue;
                    currentCell = world.GetCell(x - i, y + j);
                    component = GetComponentOf(currentCell, checker);
                    if (component != null)
                        return component;
                    
                    if (j == 0) 
                        continue;
                    currentCell = world.GetCell(x + i, y + j);
                    component = GetComponentOf(currentCell, checker);
                    if (component != null)
                        return component;
                }
            }

            return null;
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
        
        public override string ToString()
        {
            var info = "";
            info += "Visibility: " + _visibility;
            return info;
        }

        public int GetInformationPriority()
        {
            return 40;
        }
    }
}