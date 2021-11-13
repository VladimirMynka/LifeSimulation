namespace LifeSimulation.myCs.WorldObjects
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
            return "Coords: " + WorldObject.Cell.Coords[0] + 
                   ',' + WorldObject.Cell.Coords[1];
        }
    }
}