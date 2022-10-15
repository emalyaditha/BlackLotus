using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace Blue_Lotus
{
    public partial class Login : Form
    {
        SqlConnection conn = new SqlConnection();
        public Login()
        {
            InitializeComponent();
            conn.ConnectionString = @"Data Source=.;Initial Catalog=BlackLotus;Integrated Security=True;";
            conn.Open();
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                bool valid = true;
                if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Enter UserName and Password.", "Login form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    valid = false;
                }
                if (valid)
                {
                    String UserType = null;
                    SqlCommand cmd = new SqlCommand
                        ("Select userType from Users where UserName = '" + txtUsername.Text + "' and Password = '" + txtPassword.Text + "' ", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    Boolean records = dr.HasRows;
                    if (records)
                    {
                        while (dr.Read())
                        {
                            UserType = dr[0].ToString();
                        }
                        if (UserType.Equals("Admin"))
                        {
                            MessageBox.Show("Admin Successfully Logged in");
                            Admin obj = new Admin();
                            obj.Show();
                            this.Hide();
                        }
                        else if (UserType.Equals("Stock"))
                        {
                            MessageBox.Show("Stock Controller Successfully Logged in");
                            Stock_Controller obj = new Stock_Controller();
                            obj.Show();
                            this.Hide();
                        }                    
                    }
                    else
                    {
                        MessageBox.Show("Login Failed!");
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login Failed!" +ex);
            }
        }
       
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
