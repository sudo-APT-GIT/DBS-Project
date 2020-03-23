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
    public partial class Form2 : Form
    {
        OracleCommand comm , comm1;
        OracleConnection conn;
        DataRow dr , dr1;
        DataSet ds , ds1;
        OracleDataAdapter da , da1;
        DataTable dt , dt1;
        int i = 0;
        String eid1;

        public Form2(String eid1)
        {
            InitializeComponent();
            this.eid1 = eid1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            DB_Connect();
            comm = new OracleCommand();
            comm.CommandText = "select emp_id from employees,manager where emp_id=mang_id and emp_id='"+eid1+"'";
            comm.CommandType = CommandType.Text;
            comm.Connection = conn;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "tmp2");
            dt = ds.Tables["tmp2"];
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[i];
                String strr = dr["emp_id"].ToString();
                if (strr.Equals(eid1))
                {
                    Form7 f7 = new Form7(eid1);
                    f7.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("You are not a manager. Sorry.");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public void DB_Connect()
        {
            String oradb = "Data source=HP-PC; User ID = system;Password=student";
            conn = new OracleConnection(oradb);
            conn.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(eid1);
            f3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4(eid1);
            f4.Show();
            this.Hide();                       
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6(eid1);
            f6.Show();
            this.Hide();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DB_Connect();
            comm1 = new OracleCommand();
            comm1.CommandText = "select * from account";
            comm1.CommandType = CommandType.Text;
            comm1.Connection = conn;
            da1 = new OracleDataAdapter(comm1.CommandText, conn);
            ds1 = new DataSet();
            da1.Fill(ds1, "dum");
            dt1 = ds1.Tables["dum"];
            dr1 = dt1.Rows[0];
            MessageBox.Show("Rs. "+dr1["balance"].ToString() + " is the current balance in the account " + dr1["acc_no"]);
            conn.Close();

        }
    }
}
