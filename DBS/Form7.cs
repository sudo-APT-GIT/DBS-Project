using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBS
{
    public partial class Form7 : Form
    {
        String eid1;
        public Form7(String eid1)
        {
            InitializeComponent();
            this.eid1 = eid1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8(eid1);
            f8.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(eid1);
            f2.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form9 f9 = new Form9(eid1);
            f9.Show();
            this.Hide();
        }
    }
}
