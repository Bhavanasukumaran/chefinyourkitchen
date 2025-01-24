using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Chef_in_your_kitchen;

namespace Chef_in_your_kitchen
{
    public partial class Form1 : Form
    {
        private SqlConnection cn;

        public Form1()
        {
            InitializeComponent();
            SetupUI(); // Call to setup UI elements
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize and open the database connection
            cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf"";Integrated Security=True");
            cn.Open();
        }

        private void SetupUI()
        {
            // Create and set properties for the logo PictureBox
            PictureBox logoPictureBox = new PictureBox
            {
                Image = Properties.Resources.chef_in_your_Kitchen, // Ensure you add an image to the Resources
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new System.Drawing.Size(150, 150),
                Location = new System.Drawing.Point((this.ClientSize.Width - 150) / 2, 20) // Centering the logo
            };
            this.Controls.Add(logoPictureBox);

            // Create Labels, TextBoxes, and Buttons
            Label emailLabel = new Label
            {
                Text = "Email:",
                Location = new System.Drawing.Point((this.ClientSize.Width - 100) / 2, 250), // Center
                AutoSize = true
            };
            this.Controls.Add(emailLabel);

            TextBox txtuname = new TextBox
            {
                Name = "txtuname",
                Size = new System.Drawing.Size(200, 30),
                Location = new System.Drawing.Point((this.ClientSize.Width - 200) / 2, 280) // Center
            };
            this.Controls.Add(txtuname);

            Label passwordLabel = new Label
            {
                Text = "Password:",
                Location = new System.Drawing.Point((this.ClientSize.Width - 100) / 2, 320), // Center
                AutoSize = true
            };
            this.Controls.Add(passwordLabel);

            TextBox txtpass = new TextBox
            {
                Name = "txtpass",
                Size = new System.Drawing.Size(200, 30),
                Location = new System.Drawing.Point((this.ClientSize.Width - 200) / 2, 350), // Center
                PasswordChar = '*'
            };
            this.Controls.Add(txtpass);

            Button loginButton = new Button
            {
                Text = "Login",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point((this.ClientSize.Width - 100) / 2, 400) // Center
            };
            loginButton.Click += button1_Click; // Login button click event
            this.Controls.Add(loginButton);

            LinkLabel registrationLink = new LinkLabel
            {
                Text = "Register",
                Location = new System.Drawing.Point((this.ClientSize.Width - 100) / 2, 450), // Center
                AutoSize = true
            };
            registrationLink.LinkClicked += linkLabel1_LinkClicked; // Registration link event
            this.Controls.Add(registrationLink);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Navigate to registration form
            this.Hide();
            using (Registration registration = new Registration())
            {
                registration.ShowDialog();
            }
            this.Show(); // Return to the login form after registration
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Retrieve user credentials
            string email = txtuname.Text; // Email input
            string password = txtpass.Text; // Password input

            // Check for admin login
            if (email.Equals("admin", StringComparison.OrdinalIgnoreCase) && password == "Admin@123")
            {
                MessageBox.Show("Admin login successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                AdminDashboard adminDashboard = new AdminDashboard();
                adminDashboard.ShowDialog();
                this.Close(); // Close login form after admin dashboard
                return; // Exit method
            }

            // Validate credentials for other roles
            string userRole = ValidateUserCredentials(email, password);

            if (!string.IsNullOrEmpty(userRole))
            {
                MessageBox.Show("Login successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NavigateToDashboard(userRole);
            }
            else
            {
                MessageBox.Show("Invalid email or password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ValidateUserCredentials(string email, string password)
        {
            // Check credentials across different databases
            string userRole = GetRoleFromUserDatabase(email, password);
            if (string.IsNullOrEmpty(userRole)) userRole = GetRoleFromChefDatabase(email, password);
            if (string.IsNullOrEmpty(userRole)) userRole = GetRoleFromMarketDatabase(email, password);
            if (string.IsNullOrEmpty(userRole)) userRole = GetRoleFromDeliveryDatabase(email, password);
            if (string.IsNullOrEmpty(userRole)) userRole = GetRoleFromOrgDatabase(email, password);

            return userRole; // Return the found user role, or null if not found
        }

        private void NavigateToDashboard(string userRole)
        {
            // Hide the current form and navigate to the respective dashboard
            this.Hide();
            Form dashboardForm = null;

            switch (userRole.ToLower())
            {
                case "user":
                    dashboardForm = new UserDashboard(); // Ensure the form is named correctly
                    break;
                case "chef":
                    dashboardForm = new chefdashboard();
                    break;
                case "market":
                    dashboardForm = new marketdashoard(); // Correct the typo in class name
                    break;
                case "deliverypartner":
                    dashboardForm = new deliverypartnerdashboard();
                    break;
                case "organization":
                    dashboardForm = new OrganizationDashboard();
                    break;
                default:
                    MessageBox.Show("User role not recognized", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Show(); // Show login form again in case of error
                    return;
            }

            dashboardForm.ShowDialog();
            this.Close(); // Close login form after navigation
        }

        // Database checking methods
        private string GetRoleFromUserDatabase(string email, string password)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT userrole FROM UserTable WHERE Email = @Email AND Password = @Password", cn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password); // In production, use hashed passwords

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["userrole"].ToString(); // Return the user role
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null; // No matching user found
        }

        private string GetRoleFromChefDatabase(string email, string password)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT userrole FROM ChefTable WHERE email = @Email AND password = @Password", cn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password); // In production, use hashed passwords

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["userrole"].ToString(); // Return the user role
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null; // No matching chef found
        }

        private string GetRoleFromMarketDatabase(string email, string password)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT userrole FROM MarketTable WHERE Email = @Email AND Password = @Password", cn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password); // In production, use hashed passwords

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["userrole"].ToString(); // Return the user role
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null; // No matching market found
        }

        private string GetRoleFromDeliveryDatabase(string email, string password)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT userrole FROM DelpTable WHERE Email = @Email AND Password = @Password", cn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password); // In production, use hashed passwords

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["userrole"].ToString(); // Return the user role
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null; // No matching delivery partner found
        }

        private string GetRoleFromOrgDatabase(string email, string password)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT userrole FROM OrgTable WHERE Email = @Email AND Password = @Password", cn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password); // In production, use hashed passwords

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["userrole"].ToString(); // Return the user role
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null; // No matching organization found
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ensure to close the database connection on form closing
            if (cn != null)
            {
                cn.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}