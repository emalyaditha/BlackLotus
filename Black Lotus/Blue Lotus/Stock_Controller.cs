using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Blue_Lotus
{
    public partial class Stock_Controller : Form
    {
        SqlConnection con = new SqlConnection();

        public Stock_Controller()
        {
            InitializeComponent();
            con.ConnectionString = @"Data Source=.;Initial Catalog=BlackLotus;Integrated Security=True;";
            con.Open();
            loaddata();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bool valid = true;
                if (string.IsNullOrEmpty(txtID.Text) || (string.IsNullOrEmpty(txtStock.Text) || string.IsNullOrEmpty(cmbStatus.Text) || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtquantity.Text)))
                {
                    MessageBox.Show("Fields cannot be empty.", "Stock form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    valid = false;
                }
                if (valid)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Stock1 VALUES (@id, @product_Name, @status, @price, @quantity)", con);
                    cmd.Parameters.Add(new SqlParameter("id", txtID.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("product_Name", txtStock.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("status", cmbStatus.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("price", txtPrice.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("quantity", txtquantity.Text.ToString()));

                    int temp = cmd.ExecuteNonQuery();
                    if (temp > 0)
                    {
                        MessageBox.Show("Record Successfuly Added", "Employee Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Record Fail to Added", "Employee Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error insering" + ex, "Buyer Form", MessageBoxButtons.OK);
            }

        }


        private void btnClose2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void lblTime_Click(object sender, EventArgs e)
        {
  
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            this.CenterToScreen();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
       
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {    
                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Stock1 where ID ='" + txtID.Text + "'", con);
                    DataTable dt = new DataTable();
                   
                    sda.Fill(dt);
                    txtID.Text = dt.Rows[0][0].ToString();
                    txtStock.Text = dt.Rows[0][1].ToString();
                    cmbStatus.Text = dt.Rows[0][2].ToString();
                    txtPrice.Text = dt.Rows[0][3].ToString();
                    txtquantity.Text = dt.Rows[0][4].ToString();
                    MessageBox.Show("Search Found");
                  }
                catch (Exception ex)
                {
                    MessageBox.Show("Search not Found");
                }
            
        }

        private void loaddata()
        {
            SqlDataAdapter sqlda = new SqlDataAdapter("select * from Stock1", con);
            DataTable dtb1 = new DataTable();
            sqlda.Fill(dtb1);

            dataGridView2.DataSource = dtb1;
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
                try
                {
                    SqlCommand updatecommand = new SqlCommand("update Stock1 set product_Name= @product_Name, status= @status, price= @price, quantity= @quantity where id=@id", con);
                    updatecommand.Parameters.AddWithValue("id", txtID.Text.ToString());
                    updatecommand.Parameters.AddWithValue("product_Name", txtStock.Text.ToString());
                    updatecommand.Parameters.AddWithValue("status", cmbStatus.Text.ToString());
                    updatecommand.Parameters.AddWithValue("price", txtPrice.Text.ToString());
                    updatecommand.Parameters.AddWithValue("quantity", txtquantity.Text.ToString());
                    updatecommand.ExecuteNonQuery();
                    MessageBox.Show("Stock Updated Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error Updating" + ex, "Stock Controller Form", MessageBoxButtons.OK);

                }
            }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Stock1 where id ='" + txtID.Text + "'";
            cmd.ExecuteNonQuery();

            MessageBox.Show("Stock Delete Successfully");
        }

        private void txtStock_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void lblProductName_Click(object sender, EventArgs e)
        {

        }

        private void lblProductID_Click(object sender, EventArgs e)
        {

        }

        private void lblquantity_Click(object sender, EventArgs e)
        {

        }

        private void txtquantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Going Back");
            Login objForm1 = new Login();
            objForm1.Show();
            this.Hide();
        }

        private void btnPlaceorders_Click(object sender, EventArgs e)
        {
            Buyer objForm2 = new Buyer();
            objForm2.Show();
            this.Hide();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            txtStock.Clear();
            cmbStatus.ResetText();
            txtPrice.Clear();
            txtquantity.Clear();

        }

        private void Stock_Controller_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select id from Stock1  ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                string id = "";
                Boolean records = dr.HasRows;
                if (records)
                {
                    while (dr.Read())
                    {
                        id = dr[0].ToString();
                    }
                    string idString = id.Substring(1);
                    int CTR = Int32.Parse(idString);
                    if (CTR >= 1 && CTR < 9)
                    {
                        CTR = CTR + 1;
                        txtID.Text = "S00" + CTR;
                    }
                    else if (CTR >= 10 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        txtID.Text = "S0" + CTR;
                    }
                    else if (CTR > 99)
                    {
                        CTR = CTR + 1;
                        txtID.Text = "S" + CTR;
                    }

                }

                else
                {
                    txtID.Text = "B001";
                }
                dr.Close();
            }
            catch (Exception e1)
            {
                MessageBox.Show("error auto id" + e1, "Employee Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
