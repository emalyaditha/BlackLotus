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
    public partial class Buyer : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand comm = new SqlCommand();
        string searchcmd;
        int stock;
        public Buyer()
        {
            InitializeComponent();
            conn.ConnectionString = @"Data Source=.;Initial Catalog=BlackLotus;Integrated Security=True;";
            conn.Open();
        }

        private void lblTime_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Going Back");
            Stock_Controller objForm3 = new Stock_Controller();
            objForm3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stock_Controller objForm3 = new Stock_Controller();
            objForm3.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblTime1.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void btnClose2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtStock_TextChanged(object sender, EventArgs e)
        {

        }
        public void view_table(string srch)
        {

            SqlCommand search = new SqlCommand("SELECT * FROM Stock1 where id ='" + srch + "'", conn);
            search.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(search);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.CenterToScreen();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(conn.ConnectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("select * from Buyer1", sqlcon);
                DataTable dtb1 = new DataTable();
                sqlda.Fill(dtb1);

                dgv1.DataSource = dtb1;

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                bool valid = true;
                if (string.IsNullOrEmpty(txtBid.Text) || string.IsNullOrEmpty(txtBName.Text) || string.IsNullOrEmpty(txtPhoneNumber.Text) || string.IsNullOrEmpty(txtPName.Text) || string.IsNullOrEmpty(txtQuantity.Text) || string.IsNullOrEmpty(cmbStatus.Text) || string.IsNullOrEmpty(txtPrice.Text))
                {
                    MessageBox.Show("Cannot leave fields empty.", "Admin form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    valid = false;
                }
                if (valid)
                {
                    int Queantity = Convert.ToInt32(txtQuantity.Text.ToString());
                    int new_stock;
                    new_stock = stock - Queantity;
                    SqlCommand update = new SqlCommand("update Stock1 set Quantity = '" + new_stock + "' where product_Name='" + searchcmd + "'", conn);

                    update.ExecuteNonQuery();
                    view_table(searchcmd);
                    MessageBox.Show("Database Updated");

                    SqlCommand insertCommand = new SqlCommand("INSERT INTO Buyer1 VALUES (@Buyer_ID, @Buyer_Name, @Phone_Number, @Product_Name, @Quantity, @Status, @Price)", conn);
                    insertCommand.Parameters.Add(new SqlParameter("Buyer_ID", txtBid.Text.ToString()));
                    insertCommand.Parameters.Add(new SqlParameter("Buyer_Name", txtBName.Text.ToString()));
                    insertCommand.Parameters.Add(new SqlParameter("Phone_Number", txtPhoneNumber.Text.ToString()));
                    insertCommand.Parameters.Add(new SqlParameter("Product_Name", txtPName.Text.ToString()));
                    insertCommand.Parameters.Add(new SqlParameter("Quantity", txtQuantity.Text.ToString()));
                    insertCommand.Parameters.Add(new SqlParameter("Status", cmbStatus.Text.ToString()));
                    insertCommand.Parameters.Add(new SqlParameter("Price", txtPrice.Text.ToString()));
                    int temp = insertCommand.ExecuteNonQuery();

                    if (temp > 0)
                    {
                        MessageBox.Show("Buyer Successfuly Added", "Buyer Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Buyer Fail to Add", "Buyer Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting" + ex);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Buyer1 where Buyer_ID ='" + txtBid.Text + "'";
            cmd.ExecuteNonQuery();

            MessageBox.Show("Buyer Delete Successfully");
        }

        private void btnSearch1_Click(object sender, EventArgs e)
        {
            try {
                searchcmd = txtPName.Text.ToString();// product table id
                SqlCommand search = new SqlCommand("SELECT * FROM Stock1 where product_Name ='" + searchcmd + "'", conn);
                search.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(search);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                dataGridView1.DataSource = dt;

                stock = Convert.ToInt32(dt.Rows[0]["quantity"].ToString());
            }
            catch(Exception S)
            {
                MessageBox.Show("Flower not found" + S, "Buyer Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                    
            }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBid.Clear();
            txtBName.Clear();
            txtPhoneNumber.Clear();
            txtPName.Clear();
            txtQuantity.Clear();
            cmbStatus.ResetText();
            txtPrice.Clear();
            MessageBox.Show("Cleared");
        }

        private void lblBuyerID_Click(object sender, EventArgs e)
        {

        }

        private void lblPrice_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Buyer_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select Buyer_ID from Buyer1  ", conn);
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
                        txtBid.Text = "B00" + CTR;
                    }
                    else if (CTR >= 10 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        txtBid.Text = "B0" + CTR;
                    }
                    else if (CTR > 99)
                    {
                        CTR = CTR + 1;
                        txtBid.Text = "B" + CTR;
                    }

                }

                else
                {
                    txtBid.Text = "B001";
                }
                dr.Close();
            }
            catch (Exception e1)
            {
                MessageBox.Show("error auto id" + e1, "Employee Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            try
            {
                bool valid = true;
                if (string.IsNullOrEmpty(txtBid.Text) || string.IsNullOrEmpty(txtBName.Text) || string.IsNullOrEmpty(txtPhoneNumber.Text) || string.IsNullOrEmpty(txtPName.Text) || string.IsNullOrEmpty(txtQuantity.Text) || string.IsNullOrEmpty(cmbStatus.Text) || string.IsNullOrEmpty(txtPrice.Text))
                {
                    MessageBox.Show("Cannot leave fields empty.", "Admin form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    valid = false;
                }
                if (valid)
                {
                    SqlCommand updatecommand = new SqlCommand("update Buyer1 set Product_Name= @Product_Name, Buyer_Name= @Buyer_Name, Phone_Number= @Phone_Number, Quantity= @Quantity, Status= @Status, Price= @Price where Buyer_ID=@Buyer_ID", conn);
                    updatecommand.Parameters.AddWithValue("Buyer_ID", txtBid.Text.ToString());
                    updatecommand.Parameters.AddWithValue("Product_Name", txtPName.Text.ToString());
                    updatecommand.Parameters.AddWithValue("Buyer_Name", txtBName.Text.ToString());
                    updatecommand.Parameters.AddWithValue("Phone_Number", txtPhoneNumber.Text.ToString());                  
                    updatecommand.Parameters.AddWithValue("Quantity", txtQuantity.Text.ToString());
                    updatecommand.Parameters.AddWithValue("Status", cmbStatus.Text.ToString());
                    updatecommand.Parameters.AddWithValue("Price", txtPrice.Text.ToString());
                    updatecommand.ExecuteNonQuery();
                    MessageBox.Show("Buyer Updated Successfully");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error Updating" + ex, "Buyer Form", MessageBoxButtons.OK);
            }
        }

        private void txtBid_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
