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
            if (_world == null) return;
            _graphics = Graphics.FromImage(_bitmap);
            _drawer.UpdateGraphics(_graphics);
            _world.Update(_updateAll);
            _updateAll = false;
            _drawer.Update();
            pictureBox1.Image = _bitmap;
        }

        private void pixelSizeInput_TextChanged(object sender, EventArgs e)
        {
            _drawer.PixelSize = Convert.ToInt32(pixelSizeInput.Text);
            _drawer.ConfigureOffsets();
            _updateAll = true;
        }

        private void offsetLeftInput_TextChanged(object sender, EventArgs e)
        {
            _drawer.OffsetLeft = Convert.ToInt32(offsetLeftInput.Text);
            _drawer.ConfigureOffsets();
            _updateAll = true;
        }
        
        private void offsetTopInput_TextChanged(object sender, EventArgs e)
        {
            _drawer.OffsetTop = Convert.ToInt32(offsetTopInput.Text);
            _drawer.ConfigureOffsets();
            _updateAll = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(timeoutInput.Text);
        }
    }
}