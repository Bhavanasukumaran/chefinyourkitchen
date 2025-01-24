using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Chef_in_your_kitchen
{
    public partial class OrganizationDashboard : Form
    {
        private SqlConnection cn;
        private Panel sideMenuPanel;
        private Panel mainContentPanel;
        private Label lblTitle;
        private DataGridView dataGridView;
        private string currentTableName; // To track the currently displayed table
        private string userEmail;
        private string userPassword;

        public OrganizationDashboard()
        {
            InitializeComponent(); // Calls the designer method to set up the form

            SetupUI(); // Call a method to set up UI elements
        }

        private void SetupUI()
        {
            // Initialize Panels
            InitializePanels();

            // Initialize Menu Buttons
            InitializeMenuButtons();

            // Initialize Main Content
            InitializeMainContent();
        }

        private void InitializePanels()
        {
            // Side menu panel
            sideMenuPanel = new Panel
            {
                BackColor = Color.FromArgb(51, 51, 76),
                Dock = DockStyle.Left,
                Size = new Size(220, 800) // You can specify a fixed width
            };

            // Main content panel
            mainContentPanel = new Panel
            {
                BackColor = Color.FromArgb(240, 240, 240),
                Dock = DockStyle.Fill // This allows the panel to fill the remaining space
            };

            this.Controls.Add(mainContentPanel); // Add main content panel first
            this.Controls.Add(sideMenuPanel);     // Add side menu panel after
        }

        private void InitializeMenuButtons()
        {
            // Add Title Label to Side Menu
            Label dashboardLabel = new Label
            {
                Text = "Organization Dashboard",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(10, 20),
                Size = new Size(200, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            sideMenuPanel.Controls.Add(dashboardLabel);

            // Menu Buttons
            string[] menuItems = { "View Chefs", "View Payments", "View Feedback" };
            int positionY = 80;

            foreach (string menuItem in menuItems)
            {
                Button button = new Button
                {
                    Text = menuItem,
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(51, 51, 76),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10),
                    Location = new Point(0, positionY),
                    Size = new Size(220, 45),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(20, 0, 0, 0)
                };
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(75, 75, 112);
                positionY += 50;

                button.Click += (s, e) => LoadSection(menuItem);  // Dynamic event for loading sections
                sideMenuPanel.Controls.Add(button);
            }

            // Back Button
            Button backButton = new Button
            {
                Text = "Back",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(51, 51, 76),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, positionY),
                Size = new Size(220, 45),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0)
            };
            backButton.FlatAppearance.BorderSize = 0;
            backButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(75, 75, 112);
            backButton.Click += BackButton_Click; // Event handler for Back button
            sideMenuPanel.Controls.Add(backButton);
        }

        private void InitializeMainContent()
        {
            // Title Label
            lblTitle = new Label
            {
                Text = "Organization Dashboard Overview",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(400, 30)
            };
            mainContentPanel.Controls.Add(lblTitle);

            // Logout Button
            Button logoutButton = new Button
            {
                Text = "Logout",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(51, 51, 76),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                Size = new Size(100, 35),
                Location = new Point(mainContentPanel.Width - 120, 20), // Position at top-right
                Anchor = AnchorStyles.Top | AnchorStyles.Right // Anchor it to top-right so it stays there when resizing
            };
            logoutButton.FlatAppearance.BorderSize = 0;
            logoutButton.Click += LogoutButton_Click; // Event handler for Logout button
            mainContentPanel.Controls.Add(logoutButton);

            // Data Grid
            dataGridView = new DataGridView
            {
                Location = new Point(20, 70),
                Size = new Size(920, 600),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
            mainContentPanel.Controls.Add(dataGridView);

            // Action Buttons Panel
            Panel actionPanel = new Panel
            {
                Location = new Point(20, 680),
                Size = new Size(920, 50)
            };

            string[] actionButtons = { "View", "Manage", "Refresh" };
            int xPosition = 0;

            foreach (string action in actionButtons)
            {
                Button actionButton = new Button
                {
                    Text = action,
                    Location = new Point(xPosition, 0),
                    Size = new Size(100, 35),
                    BackColor = Color.FromArgb(51, 51, 76),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10)
                };
                xPosition += 120;

                actionButton.Click += (s, e) => HandleAction(action);  // Dynamic event for actions
                actionPanel.Controls.Add(actionButton);
            }

            mainContentPanel.Controls.Add(actionPanel);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            // Close the current form and show Form1
            Form1 mainForm = new Form1();
            mainForm.Show();
            this.Hide();
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            // Close the current form and return to the login form (Form1)
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Hide();
        }

        private void LoadSection(string section)
        {
            lblTitle.Text = $"{section}";
            // Here you can implement logic to load specific data or content based on the section selected
            LoadData(section); // Placeholder for loading relevant data
        }

        private void LoadData(string section)
        {
            try
            {
                // Clear existing data in the DataGridView
                dataGridView.DataSource = null;

                // Define the SQL query based on the section
                string query = string.Empty;

                switch (section)
                {
                    case "View Chefs":
                        query = "SELECT * FROM ChefProfile"; // Adjust the table name as necessary
                        break;
                    case "View Payments":
                        query = "SELECT * FROM Payments"; // Adjust the table name as necessary
                        break;
                    case "View Feedback":
                        query = "SELECT * FROM Reviews"; // Adjust the table name as necessary
                        break;
                    default:
                        MessageBox.Show("Invalid section selected.");
                        return;
                }

                // Create a SqlCommand to execute the query
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // Create a SqlDataAdapter to fill the DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();

                    // Fill the DataTable with the results from the database
                    adapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    dataGridView.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., show a message box)
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void HandleAction(string action)
        {
            switch (action)
            {
                case "View":
                    // Logic to view chefs or data
                    break;
                case "Manage":
                    // Logic to manage orders, payments, or chefs
                    break;
                case "Refresh":
                    // Logic to refresh data displayed
                    break;
                default:
                    break;
            }
        }

        private void OrganizationDashboard_Load(object sender, EventArgs e)
        {
            // Initialize and open the database connection (if necessary)
            cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf;Integrated Security=True");
            cn.Open();
        }

        // Make sure to close the connection when the form is closed
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (cn != null && cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
            base.OnFormClosed(e);
        }
    }
}