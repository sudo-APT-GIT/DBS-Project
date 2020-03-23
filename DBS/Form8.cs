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
    public partial class Form8 : Form
    {
        OracleCommand comm,comm1;
        OracleConnection conn;
        DataRow dr,dr1;
        DataSet ds,ds1;
        OracleDataAdapter da,da1;
        DataTable dt,dt1;
        int i = 0;
        String newid;
        String gender;
        String newpwd;
        String name;
        int salary;
        String sal;
        String eid1;
        private void button2_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7(eid1);
            f7.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gender = comboBox1.Text;
        }      

        private void button1_Click(object sender, EventArgs e)
        {
            DB_Connect();
            sal = textBox5.Text;
            int.TryParse(sal, out salary);
            name = textBox2.Text;
            comm = new OracleCommand();
            comm.CommandText = "insert into employees values('"+newid+"','"+name+"','"+gender+"','"+newpwd+"',"+salary+")";
            comm.CommandType = CommandType.Text;
            comm.Connection = conn;
            comm.ExecuteNonQuery();
            MessageBox.Show("Details of new employee has been stored into the database.");
            conn.Close();

        }

        
        public Form8(String eid1)
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
        private void Form8_Load(object sender, EventArgs e)
        {
            DB_Connect();
            comm1 = new OracleCommand();
            comm1.CommandText = "select emp_id from employees order by emp_id desc";
            comm1.CommandType = CommandType.Text;
            ds1 = new DataSet();
            da1 = new OracleDataAdapter(comm1.CommandText, conn);
            da1.Fill(ds1, "tmp");
            dt1 = ds1.Tables["tmp"];
            dr1 = dt1.Rows[0];             
            String t1 = dr1["emp_id"].ToString();
            String[] temp2 = t1.Split('P');
            int eid;
            int.TryParse(temp2[1], out eid);
            eid++;
            eid.ToString();
            newid = "EMP" + eid;
            label6.Text = newid;
            label7.Text = "TREE" + eid;
            newpwd = label7.Text;
            
        }
    }
}
