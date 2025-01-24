using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Chef_in_your_kitchen
{
    public partial class organizationregistration : Form
    {
        private SqlConnection cn;

        public organizationregistration()
        {
            InitializeComponent();
            // Initialize the SQL connection
            cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf"";Integrated Security=True");
        }

        private void org_register_Click(object sender, EventArgs e)
        {
            string name = txtorg_name.Text;
            string address = txtorg_address.Text;
            string email = txtorg_email.Text;
            string phone = txtorg_phone.Text;
            string password = txtorg_password.Text;
            string confirmPassword = txtorg_confirmpassword.Text;

            // Validate inputs
            if (!ValidateInput(name, address, email, phone, password, confirmPassword))
                return;

            try
            {
                cn.Open();
                string query = "INSERT INTO OrgTable (Name, Address, Email, Phone, Password, UserRole) VALUES (@Name, @Address, @Email, @Phone, @Password, 'organization')";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Password", password); // Consider hashing in production

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Organization registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Optionally navigate back to the main form or clear fields
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
            txtorg_name.Clear();
            txtorg_address.Clear();
            txtorg_email.Clear();
            txtorg_phone.Clear();
            txtorg_password.Clear();
            txtorg_confirmpassword.Clear();
        }

        private void org_clear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
