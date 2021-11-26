using System.Collections.Generic;
using System.Linq;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents
{
    public class InformationComponent : WorldObjectComponent
    {
        public string Information;
        private bool _isChecking;
        private List<IHaveInformation> _components;
        private readonly HaveInformationComparer _comparer = new HaveInformationComparer();

        public InformationComponent(WorldObject owner) : base(owner)
        {
            Information = "";
            _isChecking = false;
        }

        public override void Start()
        {
            base.Start();
            _components = GetComponents<IHaveInformation>();
            _components.Sort(_comparer);
        }
        
        public override void Update()
        {
            base.Update();
            if (!_isChecking)
                return;
            Information = GetAllInformation();
        }

        public void Open()
        {
            _isChecking = true;
        }

        public void Close()
        {
            _isChecking = false;
            Information = "";
        }

        protected virtual string GetAllInformation()
        {
            var info = GetInfoAboutCoords();
            info = _components.Aggregate(info, 
                (current, component) => current + "\n\n" + component);
            return info;
        }
        
        protected string GetInfoAboutCoords()
        {
            return "Coords: " + GetInfoAboutCoords(this);
        }

        public static string GetInfoAboutCoords(WorldObjectComponent component)
        {
            return CheckWereDestroyed(component)
                ? "none" 
                : GetInfoAboutCoords(component.WorldObject);
        }
        
        public static string GetInfoAboutCoords(WorldObject worldObject)
        {
            return CheckWereDestroyed(worldObject) 
                ? "none" 
                : worldObject.Cell.Coords[0].ToString() +
                  ',' + worldObject.Cell.Coords[1];
        }

        public void AddComponent(IHaveInformation component)
        {
            var index = _components.BinarySearch(component, _comparer);
            if (index < 0)
                _components.Insert(~index, component);
        }
    }
}