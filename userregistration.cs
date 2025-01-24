using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Chef_in_your_kitchen
{
    public partial class User_registration : Form
    {
        private SqlConnection cn;

        public User_registration()
        {
            InitializeComponent();
            // Initialize the SQL connection
            cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf"";Integrated Security=True");
        }

        private void cus_register_Click(object sender, EventArgs e)
        {
            string name = cust_name.Text;
            string address = cust_address.Text;
            string emailAddress = email.Text;
            string phone = cust_phone.Text;
            string password = cust_password.Text;
            string confirmPassword = cust_confirmpassword.Text;

            // Validate inputs
            if (!ValidateInput(name, address, emailAddress, phone, password, confirmPassword))
                return;

            try
            {
                cn.Open();
                // Updated query to include userrole
                string query = "INSERT INTO UserTable (Name, Address, Email, Phone, Password, UserRole) VALUES (@Name, @Address, @Email, @Phone, @Password, 'user')";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Email", emailAddress);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Password", password); // Consider hashing in production

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Go back to Form1
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


        private bool ValidateInput(string name, string address, string email, string phone, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
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
            cust_name.Clear();
            cust_address.Clear();
            email.Clear();
            cust_phone.Clear();
            cust_password.Clear();
            cust_confirmpassword.Clear();
        }

        private void cus_clear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void User_registration_Load(object sender, EventArgs e)
        {
            // Code for Load event, if needed
        }

        // Event handlers for UI elements
        private void label1_Click(object sender, EventArgs e)
        {
            // You can add any functionality you want here.
        }

        private void cus_address_Click(object sender, EventArgs e)
        {
            // You can add any functionality you want here.
        }

        private void cust_name_TextChanged(object sender, EventArgs e)
        {
            // You can add any functionality you want here.
        }

        private void cust_address_TextChanged(object sender, EventArgs e)
        {
            // You can add any functionality you want here.
        }

        private void email_TextChanged(object sender, EventArgs e)
        {
            // You can add any functionality you want here.
        }

        private void cust_phone_TextChanged(object sender, EventArgs e)
        {
            // You can add any functionality you want here.
        }

        private void cust_password_TextChanged(object sender, EventArgs e)
        {
            // You can add any functionality you want here.
        }

        private void cust_confirmpassword_TextChanged(object sender, EventArgs e)
        {
            // You can add any functionality you want here.
        }
    }
}
