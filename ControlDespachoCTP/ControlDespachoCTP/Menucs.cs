using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlDespachoCTP
{
    public partial class Menucs : Form
    {
        public Menucs()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Revision men = new Revision();
            men.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Historico_Revision men = new Historico_Revision();
            men.Show();
            this.Hide();
        }
    }
}
