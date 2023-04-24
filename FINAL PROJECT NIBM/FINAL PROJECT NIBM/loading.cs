using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FINAL_PROJECT_NIBM
{
    public partial class loading : Form
    {
        public loading()
        {
            InitializeComponent();
        }

        private void cpbno_ValueChanged(object sender, EventArgs e)
        {

        }

       
        private void loading_Load(object sender, EventArgs e)
        {
            cbar.Value = 0;
            timer1.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            cbar.Increment(50);
            if (cbar.Value == 300)
            {
                timer1.Stop();
                login log = new login();
                log.Show();
                this.Hide();
            }
        }

        private void cbar_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
