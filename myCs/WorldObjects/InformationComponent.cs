namespace LifeSimulation.myCs.WorldObjects
{
    public abstract class InformationComponent : WorldObjectComponent
    {
        private string _information;
        private bool _isChecking;
        private System.Windows.Forms.RichTextBox _display;
        
        protected InformationComponent(WorldObject owner) : base(owner)
        {
            _information = "";
            _isChecking = false;
        }

        public override void Update()
        {
            base.Update();
            if (!_isChecking)
                return;
            _information = GetAllInformation();
            _display.Text = _information;
        }

        public void ConnectWith(System.Windows.Forms.RichTextBox display)
        {
            _display = display;
            _isChecking = true;
        }

        public void Disconnect()
        {
            _display = null;
            _isChecking = false;
            _information = "";
        }

        protected abstract string GetAllInformation();
    }
}