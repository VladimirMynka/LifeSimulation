using System;
using System.Drawing;
using System.Windows.Forms;
using LifeSimulation.myCs;

namespace LifeSimulation
{
    public partial class Form1 : Form
    {
        private World _world;
        private Bitmap _bitmap;
        private Graphics _graphics;
        private Drawer _drawer;
        private bool _updateAll = false;
        private bool pause = false;
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
            
            _bitmap = new Bitmap(1000, 1000);
            _graphics = Graphics.FromImage(_bitmap);
            _drawer = new Drawer(_graphics);
            _world = new World(1000, 1000, _drawer);
            _drawer.Update();
        }

        private void Update(object sender, EventArgs e)
        {
            if (_world == null || pause) return;
            _graphics = Graphics.FromImage(_bitmap);
            _drawer.UpdateGraphics(_graphics);
            _world.Update(_updateAll);
            _updateAll = false;
            _drawer.Update();
            pictureBox1.Image = _bitmap;
        }

        private void pixelSizeInput_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var newSize = int.Parse(pixelSizeInput.Text);
                if (newSize <= 0) return;
                _drawer.PixelSize = newSize;
                _drawer.ConfigureOffsets();
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
                _drawer.ConfigureOffsets();
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
                _drawer.ConfigureOffsets();
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
            if (pause)
            {
                pause = false;
                button1.Text = "pause";
            }
            else
            {
                pause = true;
                button1.Text = "play";
            }
        }
    }
}