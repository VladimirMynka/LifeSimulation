using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LifeSimulation.myCs.Drawer;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects;

namespace LifeSimulation
{
    public partial class Form1 : Form
    {
        private World _world;
        private Bitmap _bitmap;
        private Graphics _graphics;
        private Drawer _drawer;
        private bool _updateAll = false;
        private bool _pause = false;
        private List<InformationComponent> _informationComponents;
        public Form1()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(Update);
            timer1.Enabled = true;

            _informationComponents = new List<InformationComponent>();
            _bitmap = new Bitmap(1000, 1000);
            _graphics = Graphics.FromImage(_bitmap);
            _drawer = new Drawer(_graphics);
            _world = new World(1000, 1000, _drawer);
            _drawer.Update();
        }

        private void Update(object sender, EventArgs e)
        {
            if (_world == null || _pause) return;
            _graphics = Graphics.FromImage(_bitmap);
            _drawer.UpdateGraphics(_graphics);
            _world.Update(_updateAll);
            UpdateInfo();
            _updateAll = false;
            _drawer.Update();
            pictureBox1.Image = _bitmap;
        }

        private void UpdateInfo()
        {
            var info = "";
            foreach (var informationComponent in _informationComponents)
            {
                info += informationComponent.Information += "\n----------\n";
            }

            InfoTextBox.Text = info;
        }

        private void pixelSizeInput_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var newSize = int.Parse(pixelSizeInput.Text);
                if (newSize <= 0) return;
                _drawer.PixelSize = newSize;
                _drawer.DrawOffsets();
                _updateAll = true;
            }
            catch
            {
                // ignored
            }
        }

        private void offsetLeftInput_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (offsetLeftInput.Text == "") return;
                _drawer.OffsetLeft = Convert.ToInt32(offsetLeftInput.Text);
                _drawer.DrawOffsets();
                _updateAll = true;
            }
            catch
            {
                //ignored
            }
        }
        
        private void offsetTopInput_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (offsetTopInput.Text == "") return;
                _drawer.OffsetTop = Convert.ToInt32(offsetTopInput.Text);
                _drawer.DrawOffsets();
                _updateAll = true;
            }
            catch
            {
                //ignored
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var newInterval = Convert.ToInt32(timeoutInput.Text);
                timer1.Interval = (newInterval <= 0) ? timer1.Interval : newInterval;
            }
            catch (Exception exception)
            {
                //ignored
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_pause)
            {
                _pause = false;
                button1.Text = "pause";
            }
            else
            {
                _pause = true;
                button1.Text = "play";
            }
        }
        
        private void world_Click(object sender, MouseEventArgs e)
        {
            var coords = _drawer.CellCoordsFromPixelCoords(e.Location);
            foreach (var informationComponent in _informationComponents)
            {
                informationComponent.Close();
            }
            
            _informationComponents.Clear();
            _informationComponents = _world.GetCell(coords.X, coords.Y).GetAllInformation();
            foreach (var informationComponent in _informationComponents)
            {
                informationComponent.Open();
            }
        }
        
        private void world_DoubleClick(object sender, MouseEventArgs e)
        {
            _drawer.ZoomOnCell(_drawer.CellCoordsFromPixelCoords(e.Location));
            _drawer.DrawOffsets();
            pixelSizeInput.Text = _drawer.PixelSize.ToString();
            offsetLeftInput.Text = _drawer.OffsetLeft.ToString();
            offsetTopInput.Text = _drawer.OffsetTop.ToString();
            _updateAll = true;
        }
    }
}