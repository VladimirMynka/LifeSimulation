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
        }

        private void Update(object sender, EventArgs e)
        {
            Invalidate();
        }


        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (_world == null)
            {
                _world = new World(10, 10, e.Graphics);
            }
            else _world.Update(e.Graphics);
        }
    }
}