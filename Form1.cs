using System;
using System.Drawing;
using System.Windows.Forms;
using LifeSimulation.myCs.Drawing;
using LifeSimulation.myCs.Informer;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation
{
    public partial class MyForm : Form
    {
        private World _world;
        private Bitmap _bitmap;
        private Graphics _graphics;
        private Drawer _drawer;
        private Informant _informant;
        private bool _pause = false;
        public MyForm()
        {
            InitializeComponent();
            MyInitialize();
        }

        private void MyInitialize()
        {
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(Update);
            timer1.Enabled = true;

            _bitmap = new Bitmap(1024, 1024);
            _graphics = Graphics.FromImage(_bitmap);
            _drawer = new Drawer(_graphics, 1024, 1024, 16);
            _world = new World(1024, 1024, _drawer);
            _informant = new Informant(_drawer, _world, InfoTextBox);
            _drawer.Update();
        }

        private void Update(object sender, EventArgs e)
        {
            if (_world == null || _pause) return;
            _graphics = Graphics.FromImage(_bitmap);
            _drawer.UpdateGraphics(_graphics);
            _world.Update();
            _informant.Update();
            _drawer.UpdateAll = false;
            _drawer.Update();
            pictureBox1.Image = _bitmap;
        }

        private void pixelSizeInput_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var newSize = int.Parse(pixelSizeInput.Text);
                if (newSize <= 0) return;
                _drawer.SetCellSize(newSize);
                _drawer.DrawOffsets();
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
            _informant.UpdateComponents(e.Location);
        }
        
        private void world_DoubleClick(object sender, MouseEventArgs e)
        {
            _drawer.ZoomOnCell(_drawer.CellCoordsFromPixelCoords(e.Location));
            _drawer.DrawOffsets();
            pixelSizeInput.Text = _drawer.GetCellSize().ToString();
            offsetLeftInput.Text = _drawer.OffsetLeft.ToString();
            offsetTopInput.Text = _drawer.OffsetTop.ToString();
        }

        private void ZoomInTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var newSize = int.Parse(ZoomInTextBox.Text);
                if (newSize <= 0) return;
                _drawer.ZoomInPixelSize = newSize;
            }
            catch
            {
                // ignored
            }
        }

        private void ZoomOutTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var newSize = int.Parse(ZoomOutTextBox.Text);
                if (newSize <= 0) return;
                _drawer.ZoomOutPixelSize = newSize;
            }
            catch
            {
                // ignored
            }
        }
    }
}