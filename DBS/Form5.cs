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
    public partial class Form5 : Form
    {
        OracleCommand comm,comm1,comm2,comm3,comm4,comm11,comm5,comm6,comm7;
        OracleConnection conn;
        DataRow dr,dr1,dr2,dr3,dr11 , dr5,dr7;
        String eid1;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            
            if(checkBox1.Enabled==true)
                textBox1.Enabled = true;
            else
                textBox1.Enabled = false;
            if (checkBox1.Enabled == false)
                textBox1.Enabled = false;
            else
                textBox1.Enabled = true;


        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        DataSet ds, ds1, ds2,ds3,ds11 , ds5,ds7;
        OracleDataAdapter da,da1,da2,da3,da11 , da5,da7;
        DataTable dt, dt1, dt2, dt3,dt11 , dt5,dt7;
        String n;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form3(eid1).Show();
        }

        

        private void Form5_Load(object sender, EventArgs e)
        {
            DB_Connect();
            label3.Text = "";
            textBox1.Enabled = false;
            comm1 = new OracleCommand();
            comm1.CommandText = "select billing_ID  from billing order by billing_id DESC";
            comm1.CommandType = CommandType.Text;
            ds1 = new DataSet();
            da1 = new OracleDataAdapter(comm1.CommandText, conn);
            da1.Fill(ds1, "tmp");
            dt1 = ds1.Tables["tmp"];
            dr1 = dt1.Rows[0];
            String x = dr1["billing_id"].ToString();
            String[] temp2 = x.Split('B');
            int bid;
            int.TryParse(temp2[1], out bid);
            bid++;
            bid.ToString();
            n = "B" + bid;
            int z = 0;
            comm2 = new OracleCommand();
            comm2.CommandText = "insert into billing values('"+n+"','"+clid+"',"+z+")";
            comm2.CommandType = CommandType.Text;
            comm2.Connection = conn;
            comm2.ExecuteNonQuery();
           
            conn.Close();
        }

        int i = 0;
        String newid1 , clid;
        public Form5(String newid1 ,String clid,String eid1)
        {
            InitializeComponent();
            this.newid1 = newid1;
            this.clid = clid;
            this.eid1 = eid1;
        }
        public void DB_Connect()
        {
            String oradb = "Data Source=HP-PC; User ID=system; Password=student";
            conn = new OracleConnection(oradb);
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB_Connect();                           
            comm = new OracleCommand();
            comm.CommandText ="update billing set total_amt=(with temptbl(orda) as (select sum(qty*unit_price) from order_details_table natural join stock where order_id='" + newid1 + "') select orda from temptbl where customer_id='" + clid + "' ) where billing_ID='"+n+"'";
            comm.CommandType = CommandType.Text;
            comm.Connection = conn;
            comm.ExecuteNonQuery();
           
            comm11 = new OracleCommand();
            comm11.CommandText = "select total_amt from billing where billing_id='"+n+"'";
            comm11.CommandType = CommandType.Text;
            comm11.Connection = conn;
            ds11 = new DataSet();
            OracleDataAdapter da11 = new OracleDataAdapter(comm11.CommandText, conn);
            da11.Fill(ds11, "tmp1");
            dt11 = ds11.Tables["tmp1"];
            dr11 = dt11.Rows[i];           
            String samt = dr11["total_amt"].ToString();
            int iamt;
            int.TryParse(samt, out iamt);
         
            //discount

            comm3 = new OracleCommand();
            comm3.CommandText = "INSERT INTO tot VALUES ("+iamt+")";
            comm3.CommandType = CommandType.Text;
            comm3.Connection = conn;
            comm3.ExecuteNonQuery();
           

            comm4 = new OracleCommand();
            comm4.CommandText = "delete from tot";
            comm4.CommandType = CommandType.Text;
            comm4.Connection = conn;
            comm4.ExecuteNonQuery();
            

            if(checkBox1.Checked)
            {
                
                comm5 = new OracleCommand();
                comm5.CommandText = "INSERT into uses_voucher values('"+n+"','"+textBox1.Text+"')";
                comm5.CommandType = CommandType.Text;
                comm5.Connection = conn;
                comm5.ExecuteNonQuery();

                comm6 = new OracleCommand();
                comm6.CommandText = "update billing set total_amt=(with temptab(vamt) as (select (total_amt-(0.01*v_disc)*total_amt) from billing natural join uses_voucher natural join voucher where billing_id='"+n+"') select vamt from temptab) where billing_ID ='"+n+"'";
                comm6.CommandType = CommandType.Text;
                comm6.Connection = conn;
                
                comm6.ExecuteNonQuery();
               
            }

            MessageBox.Show("Updated Billing table!");
            comm7 = new OracleCommand();
            comm7.CommandText = "select total_amt from billing where billing_id='" + n + "'";
            comm7.CommandType = CommandType.Text;
            comm7.Connection = conn;
            ds7 = new DataSet();
            da7 = new OracleDataAdapter(comm7.CommandText, conn);
            da7.Fill(ds7, "tt1");
            dt7 = ds7.Tables["tt1"];
            dr7 = dt7.Rows[0];
            label3.Text = "Rs. "+dr7["total_amt"].ToString()+" is the final bill amount.";
            conn.Close();
            
        }
    }
}
