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
    public partial class Admin : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand comm = new SqlCommand();
        string searchcmd;
        public Admin()
        {
            InitializeComponent();
            conn.ConnectionString = @"Data Source=.;Initial Catalog=BlackLotus;Integrated Security=True;";
            conn.Open();
        }

        private void btnClose2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            this.CenterToScreen();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bool valid = true;
                if (string.IsNullOrEmpty(txtUsername1.Text) || (string.IsNullOrEmpty(txtPassword1.Text) || string.IsNullOrEmpty(cmdUsertype1.Text) || string.IsNullOrEmpty(txtid1.Text)))
                {
                    MessageBox.Show("Fields cannot be empty.", "Admin form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    valid = false;
                }
                if (valid)
                {
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO Users VALUES (@id, @userName, @password, @userType)", conn);
                    insertCommand.Parameters.Add(new SqlParameter("id", txtid1.Text.ToString()));
                    insertCommand.Parameters.Add(new SqlParameter("userName", txtUsername1.Text.ToString()));
                    insertCommand.Parameters.Add(new SqlParameter("password", txtPassword1.Text.ToString()));
                    insertCommand.Parameters.Add(new SqlParameter("userType", cmdUsertype1.Text.ToString()));
                    
                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("User Added", "user Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error insering" + ex, "user Form", MessageBoxButtons.OK);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try { 
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Users where id ='" + txtid1.Text + "'";
            cmd.ExecuteNonQuery();
            MessageBox.Show("User Delete Successfully");
             catch (Exception ex)
            {
                MessageBox.Show("error Deleting" + ex, "Admin Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand updatecommand = new SqlCommand("update Users set userName=@userName, password= @password, userType= @userType where id=@id", conn);
                updatecommand.Parameters.AddWithValue("id", txtid1.Text.ToString());
                updatecommand.Parameters.AddWithValue("userName", txtUsername1.Text.ToString());
                updatecommand.Parameters.AddWithValue("password", txtPassword1.Text.ToString());
                updatecommand.Parameters.AddWithValue("userType", cmdUsertype1.Text.ToString()); 
                updatecommand.ExecuteNonQuery();
                MessageBox.Show("User Updated Successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show("error Updating" + ex, "Admin Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Going Back");
            Login objForm1 = new Login();
            objForm1.Show();
            this.Hide();
        }

        private void lblTime_Click(object sender, EventArgs e)
        {

        }

        private void cmdUsertype1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtid1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername1_TextChanged(object sender, EventArgs e)
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Admin_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select id from Users", conn);
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
                        txtid1.Text = "E00" + CTR;
                    }
                    else if (CTR >= 10 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        txtid1.Text = "E0" + CTR;
                    }
                    else if (CTR > 99)
                    {
                        CTR = CTR + 1;
                        txtid1.Text = "E" + CTR;
                    }

                }

                else
                {
                    txtid1.Text = "E001";
                }
                dr.Close();
            }
            catch (Exception e1)
            {
                MessageBox.Show("error auto id" + e1, "Employee Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtid1.Clear();
            txtUsername1.Clear();
            txtPassword1.Clear();
            cmdUsertype1.ResetText();
            MessageBox.Show("Cleared");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Users where id ='" + txtid1.Text + "'", conn);
                DataTable dt = new DataTable();

                sda.Fill(dt);
                txtid1.Text = dt.Rows[0][0].ToString();
                txtUsername1.Text = dt.Rows[0][1].ToString();
                txtPassword1.Text = dt.Rows[0][2].ToString();
                cmdUsertype1.Text = dt.Rows[0][3].ToString();
                MessageBox.Show("Search Found");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search not Found");

            }
        }
    }  
}
