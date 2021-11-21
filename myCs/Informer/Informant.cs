using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.myCs.Drawing;
using LifeSimulation.myCs.WorldObjects;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.Informer
{
    public class Informant
    {
        private readonly Drawer _drawer;
        private readonly World _world;
        private readonly System.Windows.Forms.RichTextBox _infoTextBox;
        private List<InformationComponent> _informationComponents;
        private readonly List<InformationComponent> _removingComponents;


        public Informant(Drawer drawer, World world, System.Windows.Forms.RichTextBox infoTextBox)
        {
            _drawer = drawer;
            _world = world;
            _infoTextBox = infoTextBox;
            _informationComponents = new List<InformationComponent>();
            _removingComponents = new List<InformationComponent>();
        }

        public void Update()
        {
            UpdateInfo();
        }
        
        private void UpdateInfo()
        {
            var info = "";
            info += _world.Weather.GetInformation() + "\n\n\n----Objects----\n";
            foreach (var component in _informationComponents)
            {
                if (WorldObjectComponent.CheckWereDestroyed(component))
                {
                    _removingComponents.Add(component);
                    continue;
                }
                info += component.Information + "\n----------\n";
            }

            _infoTextBox.Text = info;
            RemoveComponents();
        }

        private void RemoveComponents()
        {
            foreach (var component in _removingComponents)
            {
                _informationComponents.Remove(component);
            }
            _removingComponents.Clear();
        }

        public void UpdateComponents(Point point)
        {
            var coords = _drawer.CellCoordsFromPixelCoords(point);
            
            ClearComponents();
            AddComponents(_world.GetCell(coords.X, coords.Y));
        }

        private void ClearComponents()
        {
            foreach (var informationComponent in _informationComponents)
            {
                informationComponent.Close();
            }
            _informationComponents.Clear();
        }

        private void AddComponents(Cell cell)
        {
            _informationComponents = cell.GetAllInformation();
            foreach (var informationComponent in _informationComponents)
            {
                informationComponent.Open();
            }
        }
    }
}