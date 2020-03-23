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
    public partial class Form4 : Form
    {
        OracleCommand comm;
        OracleConnection conn;
        DataRow dr;
        DataSet ds;
        OracleDataAdapter da;
        DataTable dt;
        int i = 0;
        String eid1;
        public Form4(String eid1)
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

        private void Form4_Load(object sender, EventArgs e)
        {
            DB_Connect();
            comm = new OracleCommand();
            comm.CommandText = "select * from order_table order by order_id desc";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "tbl_ordertbl");
            dt = ds.Tables["tbl_ordertbl"];
            int t = dt.Rows.Count;
           
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "tbl_ordertbl";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(eid1);
            
            f2.Show();
            this.Dispose();
        }
    }
}
