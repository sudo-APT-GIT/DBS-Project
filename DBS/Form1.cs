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
    public partial class Form1 : Form
    {
        OracleCommand comm;
        OracleConnection conn;
        DataView dv;
        DataRow dr;
        DataSet ds;
        OracleDataAdapter da;
        DataTable dt;
        int i = 0;
        public Form1()
        {
            InitializeComponent();
        }
        public void DB_Connect()
        {
            String oradb = "Data source=HP-PC; User ID=system; Password=student";
            conn = new OracleConnection(oradb);
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB_Connect();
            String uid = textBox1.Text;
            string pwd = textBox2.Text;
            String dbpwd = "none";
            String dbuid;
            comm = new OracleCommand();
            comm.CommandText = "select emp_password from employees where emp_id=" + "'" + uid + "'";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "tbl_employees");
            dt = ds.Tables["tbl_employees"];
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["emp_password"] == DBNull.Value)
                    continue;
                else
                {
                    dbpwd = dr["emp_password"].ToString();
                    break;
                }

            }
            if(dbpwd.CompareTo(pwd)==0)
            {
                Form2 f2 = new Form2(uid);
                f2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid credentials");
            }


            
        }
    }
}
