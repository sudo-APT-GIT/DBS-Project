using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace DBS
{
    public partial class Form9 : Form
    {
        OracleCommand comm, comm1;
        OracleConnection conn;
        DataRow dr, dr1;
        DataSet ds, ds1;
        OracleDataAdapter da, da1;
        DataTable dt, dt1;
        String eid1;

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(eid1);
            f2.Show();
            this.Hide();
        }

        int i = 0;
        public Form9(String eid1)
        {
            InitializeComponent();
            this.eid1 = eid1;
        }
        public void DB_Connect()
        {
            string oradb = "Data source = HP-PC;User ID=system;Password=student";
            conn = new OracleConnection(oradb);
            conn.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DB_Connect();
            String eid = textBox1.Text;
            String sal = textBox2.Text;
            int isal;
            int.TryParse(sal, out isal);
            comm = new OracleCommand();
            comm.CommandText = "update employees set emp_salary='"+isal+"'where emp_id='"+eid+"'";
            comm.CommandType = CommandType.Text;
            comm.Connection = conn;
            comm.ExecuteNonQuery();
            MessageBox.Show("Employee salary updated");
            conn.Close();
            
        }
    }
}
