using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LifeSimulation.myCs;

namespace LifeSimulation
{
    public partial class Form1 : Form
    {
        private World _world;
        private Bitmap _bitmap;
        private Graphics _graphics;
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
            _world = new World(1000, 1000, _graphics);
        }

        private void Update(object sender, EventArgs e)
        {
            if (_world == null) return;
            _graphics = Graphics.FromImage(_bitmap);
            _world.Update();
            pictureBox1.Image = _bitmap;
        }

    }
}