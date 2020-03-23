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
    public partial class Form6 : Form
    {
        OracleCommand comm, comm1;
        OracleConnection conn;
        DataRow dr, dr1;
        DataSet ds, ds1;
        OracleDataAdapter da, da1;
        DataTable dt, dt1;
        String eid1;

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(eid1);
            f2.Show();
            this.Hide();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB_Connect();
            String sqty = textBox1.Text;
            int iqty;
            int.TryParse(sqty, out iqty);
            comm = new OracleCommand();
            comm.CommandText ="(select product_ID,product_name from stock natural join order_details_table group by (product_ID,product_name) having sum(qty)<"+iqty+") union (select product_ID,product_name from stock where product_ID not in(select distinct(product_ID) from order_details_table))";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds,"tmp12");
            dt = ds.Tables["tmp12"];
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "tmp12";

        }

        int i = 0;
        public Form6(String eid1)
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
            comm = new OracleCommand();
            comm.CommandText= "select product_ID, product_name from manufacturer_details natural join stock where manufacturer_ID in(select manufacturer_ID from manufacturer_details where product_ID='"+textBox2.Text+"')";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "tmp1");
            dt = ds.Tables["tmp1"];
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "tmp1";
            
        }
    }
}
