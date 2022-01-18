using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LastBattleShipGame
{
    public partial class ShootWindow : Form
    {
        public ShootWindow()
        {
            InitializeComponent();
        }

        public string Coordinates { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            GetCords();
        }

        private void GetCords()
        {
            Coordinates = textBox1.Text;
            this.Close();
        }
    }
}
