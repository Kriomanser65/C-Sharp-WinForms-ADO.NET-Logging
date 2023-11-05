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

namespace Logging
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=DESKTOP-4SK39GD;Initial Catalog=ITacademy;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public Form1()
        {
            InitializeComponent();
            UpdateUserList();
        }
        public class AddUserForm : Form
        {

        }
        public class EditUserForm : Form
        {
            string userLogin;

            public EditUserForm(string login)
            {
                userLogin = login;
            }
        }
        private void UpdateUserList()
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select * from Users";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                listBox1.Items.Clear();
                while (reader.Read()) 
                {
                    string itemText = reader["Login"].ToString();
                    listBox1.Items.Add(itemText);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUserForm addForm = new AddUserForm();
            if(addForm.ShowDialog() == DialogResult.OK)
            {
                UpdateUserList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string login = listBox1.SelectedItem.ToString().Split(' ')[0];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"delete from Users where Login = '{login}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
                UpdateUserList();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string login = listBox1.SelectedItem.ToString().Split(' ')[0];
                EditUserForm editForm = new EditUserForm(login);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    UpdateUserList();
                }
            }
        }
    }
}
