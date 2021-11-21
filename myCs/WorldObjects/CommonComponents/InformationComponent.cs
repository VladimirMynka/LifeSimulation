namespace LifeSimulation.myCs.WorldObjects.CommonComponents
{
    public abstract class InformationComponent : WorldObjectComponent
    {
        public string Information;
        private bool _isChecking;
        
        protected InformationComponent(WorldObject owner) : base(owner)
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

        protected abstract string GetAllInformation();
        
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
    }
}