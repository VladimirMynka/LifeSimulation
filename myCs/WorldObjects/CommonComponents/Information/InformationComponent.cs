using System.Collections.Generic;

namespace LifeSimulation.myCs.WorldObjects.CommonComponents.Information
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

        public override void Update()
        {
            base.Update();
            if (!_isChecking)
                return;
            Information = GetAllInformation();
            CheckAndRemove();
        }

        public void Open()
        {
            _isChecking = true;
            _components = GetComponents<IHaveInformation>();
            _components.Sort(_comparer);
        }

        public void Close()
        {
            _isChecking = false;
            Information = "";
            _components = null;
        }

        protected virtual string GetAllInformation()
        {
            var info = GetInfoAboutCoords();
            foreach (var component in _components)
            {
                info += "\n\n" + component;
            }

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
            if (_components == null)
                return;
            var index = _components.BinarySearch(component, _comparer);
            if (index < 0)
                _components.Insert(~index, component);
        }

        private void CheckAndRemove()
        {
            if (_components == null)
                return;
            var clone = new IHaveInformation[_components.Count];
            _components.CopyTo(clone);

            foreach (var component in clone)
            {
                if (CheckWereDestroyed(component))
                    _components.Remove(component);
            }
        }

        private static bool CheckWereDestroyed(IHaveInformation component)
        {
            return CheckWereDestroyed(component as WorldObjectComponent);
        }
    }
}