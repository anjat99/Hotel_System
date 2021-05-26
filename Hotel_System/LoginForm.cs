using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_System
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                DBConnection conn = new DBConnection();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand();
                String query = "SELECT * FROM users WHERE username = @username AND password = @password";

                command.CommandText = query;
                command.Connection = conn.GetConnection();

                command.Parameters.Add("@username", MySqlDbType.VarChar).Value = tbUsername.Text;
                command.Parameters.Add("@password", MySqlDbType.VarChar).Value = tbPassword.Text;

                adapter.SelectCommand = command;
                adapter.Fill(table);

                //if the username and the password exists
                if (table.Rows.Count > 0)
                {
                    //show the main form
                    this.Hide();
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                }
                else
                {
                    if (tbUsername.Text.Trim().Equals(""))
                    {
                        MessageBox.Show("Enter your username to Login", "Empty Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (tbPassword.Text.Trim().Equals(""))
                    {
                        MessageBox.Show("Enter your Password to Login", "Empty Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("This username or password does not exists", "Wrong Username/Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           


           
        }

      
    }
}
