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
    public partial class Form3 : Form
    {
        OracleCommand comm;
        OracleCommand comm1 , comm11 , comm2 , comm22 , comm3 , comm4,comm111 , com11;
        OracleCommand c9,c8;
        OracleConnection conn;
        DataRow dr , dr1 , dr2 , dr3 , dr4,dr111,dr9,dr8;
        DataView dv;
        DataView dv1;
        DataSet ds;
        DataSet ds1 , ds2 , ds3 , ds4,ds111,ds9,ds8;
        String str;

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            str = textBox2.Text;
        }

        OracleDataAdapter da;
        OracleDataAdapter da1 , da2 , da3 , da4,da111,da9,da8;
        DataTable dt;
        DataTable dt1 , dt2 , dt3 , dt4,dt111,dt9,dt8;
        int i = 0;
        int i1 = 0;
        String pid, pname,newid;
        int pqty, ptot;
        int punit;
        String eid1;
        
        public Form3(String eid1)
        {
            InitializeComponent();
            this.eid1 = eid1;
        }
        public void DB_Connect()
        {
            String oradb = "Data Source=HP-PC; User ID=system; Password=student";
            conn = new OracleConnection(oradb);
            conn.Open();
        }

        private void Form3_Load(object sender, EventArgs e)
        {            
            DB_Connect();
            Form2 f2 = new Form2(eid1);
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "delete from temp_orders";
            cmd.ExecuteNonQuery();
            comm = new OracleCommand();
            comm.CommandText = "select product_ID from stock";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "tbl_stock");
            dt = ds.Tables["tbl_stock"];
            comboBox1.DataSource = dt.DefaultView;
            comboBox1.DisplayMember = "product_ID";
            conn.Close();
            DB_Connect();

            comm1 = new OracleCommand();
            comm1.CommandText = "select order_ID  from order_table order by order_ID DESC";
            comm1.CommandType = CommandType.Text;
            ds1 = new DataSet();
            da1 = new OracleDataAdapter(comm1.CommandText, conn);
            da1.Fill(ds1, "tmp");
            dt1 = ds1.Tables["tmp"];
            dr1 = dt1.Rows[0];            
            label8.Text = dr1["order_ID"].ToString();
            String t1 = label8.Text;           
            String [] temp2=t1.Split('D');            
            int oid;
            int.TryParse(temp2[1], out oid);
            oid++;
            oid.ToString();
            newid = "ORD" + oid;
            label8.Text = newid;

            conn.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            DB_Connect();
            Form2 f2 = new Form2(eid1);
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "delete from temp_orders";
            cmd.ExecuteNonQuery();
            f2.Show();
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DB_Connect();
            comm = new OracleCommand();
            com11 = new OracleCommand();
            comm1 = new OracleCommand();
            comm11 = new OracleCommand();
            comm2 = new OracleCommand();
            comm22 = new OracleCommand();
            comm3 = new OracleCommand();
            comm111 = new OracleCommand();
            String x = label8.Text;
            String y = textBox2.Text;

            //Executing proc dbs2 to check if client exists
            c8 = new OracleCommand();
            c8.CommandText = "dbs2";
            c8.CommandType = CommandType.StoredProcedure;
            c8.Connection = conn;
            OracleParameter pa88 = new OracleParameter();
            pa88.ParameterName = "counter";
            pa88.DbType = DbType.Int32;
            pa88 = comm.Parameters.Add("counter", OracleDbType.Int32, ParameterDirection.Output);
           
            int cx  ;
            int.TryParse(pa88.ToString(), out cx);
            OracleParameter pa8 = new OracleParameter();
            pa8.ParameterName = "cid";
            pa8.Value = y;
            pa8.DbType = DbType.String;
            c8.Parameters.Add(pa8);
            Console.WriteLine(cx);
           
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No Products selected to Confirm Order !");
            }
            else if(textBox2.Text == "")
            {
                MessageBox.Show("Enter valid client ID");
                
            }
           
            else 
            {                
                //insertion into order_table(order_ID , ord_amt , order_DATE) Table
                DateTime tod = DateTime.UtcNow.Date;
                string strDate = tod.ToString("yyyy-MM-dd");
                String strDate1 = "2019-04-16";
                comm111.CommandText = "select product_ID , qty from temp_orders";
                comm111.CommandType = CommandType.Text;
                comm111.Connection = conn;
                ds111 = new DataSet();
                da111 = new OracleDataAdapter(comm111.CommandText, conn);
                da111.Fill(ds111, "tmp1236");
                dt111 = ds111.Tables["tmp1236"];
                int yy = dt111.Rows.Count-1;
                int ordq = 0;
                while(yy >= 0)
                {
                    dr111 = dt111.Rows[yy];
                    String d = dr111["product_ID"].ToString();
                    int quan;
                    int.TryParse(dr111["qty"].ToString(), out quan);
                    com11.CommandText = "insert into tempord values ('" + x + "','" + d + "'," + quan + ")";
                    com11.CommandType = CommandType.Text;
                    com11.Connection = conn;
                    com11.ExecuteNonQuery();
                    ordq += quan;
                    yy--;
                }

                comm3.Connection = conn;
                comm3.CommandText = "insert into order_table values ('" + x + "'," + ordq + ", date '" + strDate1 + "')";
                comm3.CommandType = CommandType.Text;                
                comm3.ExecuteNonQuery();
                
                OracleCommand cmd11 = new OracleCommand();
                cmd11.Connection = conn;
                cmd11.CommandText = "delete from tempord";
                cmd11.ExecuteNonQuery();           
                
                    //insertion into place_order(order_ID , cust_ID) Table
                    comm.CommandText = "insert into place_order values ('" + x + "','" + y + "')";
                    comm.CommandType = CommandType.Text;
                    comm.Connection = conn;
                    comm.ExecuteNonQuery();
                   

                    //insertion into order_items_table Table
                    comm2.CommandText = "select product_ID from temp_orders";
                    comm2.CommandType = CommandType.Text;
                    comm2.Connection = conn;
                    ds2 = new DataSet();
                    da2 = new OracleDataAdapter(comm2.CommandText, conn);
                    da2.Fill(ds2, "tmp1234");
                    dt2 = ds2.Tables["tmp1234"];
                    int q1 = dt2.Rows.Count - 1;
                    while (q1 >= 0)
                    {
                        dr2 = dt2.Rows[q1];
                        String d = dr2["product_ID"].ToString();
                        comm22.Connection = conn;
                        comm22.CommandText = "insert into order_items_table values ('" + x + "','" + d + "')";
                        comm22.CommandType = CommandType.Text;
                        comm22.ExecuteNonQuery();
                        q1--;
                    }
                   
                    //insertion into order_details_table(order_id , prod_ID , qty) Table
                    comm1.CommandText = "select product_ID , qty from temp_orders";
                    comm1.CommandType = CommandType.Text;
                    comm1.Connection = conn;
                    ds1 = new DataSet();
                    da1 = new OracleDataAdapter(comm1.CommandText, conn);
                    da1.Fill(ds1, "tmp123");
                    dt1 = ds1.Tables["tmp123"];
                    q1 = dt1.Rows.Count - 1;
                    while (q1 >= 0)
                    {
                        dr1 = dt1.Rows[q1];
                        String d = dr1["product_ID"].ToString();
                        int quan;
                        int.TryParse(dr1["qty"].ToString(), out quan);
                        comm11.CommandText = "insert into order_details_table values ('" + x + "','" + d + "'," + quan + ")";
                        comm11.CommandType = CommandType.Text;
                        comm11.Connection = conn;
                        comm11.ExecuteNonQuery();
                        q1--;
                    }
                 
                    c9 = new OracleCommand();
                    c9.CommandText = "dbs1";
                    c9.CommandType = CommandType.StoredProcedure;
                    c9.Connection = conn;
                    OracleParameter pa1 = new OracleParameter();
                    pa1.Value = label8.Text;
                    pa1.ParameterName = "oid";
                    pa1.DbType = DbType.String;
                    c9.Parameters.Add(pa1);

                    c9.ExecuteNonQuery();
                    MessageBox.Show("Procedure executed!");
                    new Form5(newid , y,eid1).Show();
                    this.Hide();
            }
            conn.Close();
            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pid = comboBox1.Text;
            DB_Connect();
            comm = new OracleCommand();

            comm.CommandText = "select product_name from stock where product_ID='" + pid + "'";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "tbl_temp");
            dt = ds.Tables["tbl_temp"];           
            foreach(DataRow dr in dt.Rows)
            {
                if (dr["product_name"] == DBNull.Value)
                    continue;
                else
                {
                    label2.Text = dr["product_name"].ToString();
                    break;
                }

            }
            pname = label2.Text;
            
            comm1 = new OracleCommand();
            comm1.CommandText = "select unit_price from stock where product_ID='" + pid + "'";
            comm1.CommandType = CommandType.Text;
            ds1 = new DataSet();
            da1 = new OracleDataAdapter(comm1.CommandText, conn);
            da1.Fill(ds1, "tbl_temp");
            dt1 = ds1.Tables["tbl_temp"];
            foreach(DataRow dr1 in dt1.Rows)
            {
                if (dr1["unit_price"] == DBNull.Value)
                    continue;
                else
                {
                    label3.Text = dr1["unit_price"].ToString();
                    break;
                }
            }
            String unit = label3.Text;
            int.TryParse(unit,out punit);

            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB_Connect();
            String sqty;
            int qty;
            sqty = textBox1.Text;
            int.TryParse(sqty,out pqty);
            if(pqty==0)
            {
                MessageBox.Show("Please enter quantity");
            }
            else
            {
                ptot = pqty * punit;
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "insert into temp_orders values('" + pid + "','" + pname + "'," + pqty + "," + ptot + ")";
                cmd.ExecuteNonQuery();


                comm = new OracleCommand();
                comm.CommandText = "select * from temp_orders";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);

                da.Fill(ds, "tbl_temporders");
                dt = ds.Tables["tbl_temporders"];
                dr = dt.Rows[i];
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "tbl_temporders";
            }
            
        }
    }
}
