using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace RotationTest
{
    public partial class Form1 : Form
    {
        DispatcherTimer timer;
        List<Lever> levers;
        public Form1()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.IsEnabled = true;
            timer.Tick += On_Timer_Tick;
            timer.Start();
            levers = new List<Lever>();
            levers.Add(new Lever(new Axis(300, 100), new PhysicalPoint(10, 100, 2f), new PhysicalPoint(300, 100, 1)));
            Drawer.Levers = levers;
        }

        private void On_Timer_Tick(object sender, EventArgs e)
        {
            Physics.RunLeverDynamics(levers);
            Drawer.Draw(pictureBox1);
        }
    }
}
