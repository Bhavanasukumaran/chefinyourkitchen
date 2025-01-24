using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Chef_in_your_kitchen
{
    public partial class deliverypregistration : Form
    {
        private SqlConnection cn;

        public deliverypregistration()
        {
            InitializeComponent();
            // Initialize the SQL connection
            cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf"";Integrated Security=True");
        }

        private void del_register_Click(object sender, EventArgs e)
        {
            string name = deli_name.Text;
            string emailAddress = deli_email.Text;
            string phone = deli_phone.Text;
            string address = deli_address.Text;
            string password = deli_password.Text;
            string confirmPassword = deli_confirmpassword.Text;

            // Validate inputs
            if (!ValidateInput(name, emailAddress, phone, address, password, confirmPassword))
                return;

            try
            {
                cn.Open();

                // Insert data into the database without hashing the password
                string query = "INSERT INTO DelpTable (Name, Address, Email, Phone, Password, UserRole) VALUES (@Name, @Address, @Email, @Phone, @Password, 'Deliverypartner')";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Email", emailAddress);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Password", password); // Store password directly

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Delivery partner registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Optionally clear the fields after successful registration
                        ClearFields();
                        Form1 mainForm = new Form1();
                        mainForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Registration failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        private bool ValidateInput(string name, string email, string phone, string address, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(address) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill all fields", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Additional validation for email or phone format could go here

            return true;
        }

        private void ClearFields()
        {
            deli_name.Clear();
            deli_email.Clear();
            deli_phone.Clear();
            deli_address.Clear();
            deli_password.Clear();
            deli_confirmpassword.Clear();
        }

        private void del_clear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void deliverypregistration_Load(object sender, EventArgs e)
        {
            // Code for Load event, if needed
        }

        private void cususer_Click(object sender, EventArgs e)
        {

        }

        // Event handlers for UI elements can be added here as needed
    }
}