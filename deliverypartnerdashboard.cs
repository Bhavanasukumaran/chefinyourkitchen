using Chef_in_your_kitchen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chef_in_your_kitchen
{
    public partial class deliverypartnerdashboard : Form
    {
        private SqlConnection cn;
        private Panel sideMenuPanel;
        private Panel mainContentPanel;
        private Label lblTitle;
        private DataGridView dataGridView;
        private string userEmail;
        private string userPassword;
        public deliverypartnerdashboard()
        {
            InitializeComponent();
            SetupUI();
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
                Size = new Size(220, 800)
            };

            // Main content panel
            mainContentPanel = new Panel
            {
                BackColor = Color.FromArgb(240, 240, 240),
                Dock = DockStyle.Fill
            };

            this.Controls.Add(mainContentPanel);
            this.Controls.Add(sideMenuPanel);
        }

        private void InitializeMenuButtons()
        {
            // Add Title Label to Side Menu
            Label dashboardLabel = new Label
            {
                Text = "Delivery Partner Dashboard",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(10, 20),
                Size = new Size(200, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            sideMenuPanel.Controls.Add(dashboardLabel);

            // Menu Buttons
            string[] menuItems = { "View Orders", "Order Status", "Notifications" };
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

                button.Click += (s, e) => LoadSection(menuItem);
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
            backButton.Click += BackButton_Click;
            sideMenuPanel.Controls.Add(backButton);
        }

        private void InitializeMainContent()
        {
            // Title Label
            lblTitle = new Label
            {
                Text = "Delivery Partner Dashboard Overview",
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
                Location = new Point(mainContentPanel.Width - 120, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            logoutButton.FlatAppearance.BorderSize = 0;
            logoutButton.Click += LogoutButton_Click;
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

            string[] actionButtons = { "View", "Accept", "Complete", "Refresh" };
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

                actionButton.Click += (s, e) => HandleAction(action);
                actionPanel.Controls.Add(actionButton);
            }

            mainContentPanel.Controls.Add(actionPanel);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Form1 mainForm = new Form1();
            mainForm.Show();
            this.Hide();
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Hide();
        }

        private void LoadSection(string section)
        {
            lblTitle.Text = $"{section}";
            LoadData(section);
        }

        private void LoadData(string section)
        {
            if (section == "View Orders")
            {
                LoadOrders();
            }
            else if (section == "Order Status")
            {
                LoadOrderStatus();
            }
            else if (section == "Notifications")
            {
                LoadNotifications();
            }
        }

        private void LoadNotifications()
        {
            // Clear existing data in DataGridView
            dataGridView.Columns.Clear();

            // Set up the DataGridView columns
            dataGridView.Columns.Add("NotificationId", "Notification ID");
            dataGridView.Columns.Add("Message", "Message");
            dataGridView.Columns.Add("Action", "Action");

            // Fetch notifications from the database
            string query = "SELECT NotificationId, Message FROM Notifications"; // Adjust the query as per your database schema
            using (SqlCommand command = new SqlCommand(query, cn))
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int notificationId = reader.GetInt32(0);
                    string message = reader.GetString(1);

                    // Add a new row for each notification
                    int rowIndex = dataGridView.Rows.Add(notificationId, message, "Action");

                    // Create buttons for actions based on the message
                    DataGridViewButtonCell actionButton = new DataGridViewButtonCell
                    {
                        Value = "Accept", // Default action
                        Style = { BackColor = Color.Blue, ForeColor = Color.White }
                    };

                    // Determine the action based on the message content
                    if (message.Contains("delivery boy request"))
                    {
                        actionButton.Value = "Accept";
                    }
                    else if (message.Contains("order ready"))
                    {
                        actionButton.Value = "Collect";
                    }
                    else if (message.Contains("order delivered"))
                    {
                        actionButton.Value = "Complete";
                    }

                    dataGridView.Rows[rowIndex].Cells["Action"] = actionButton;
                }

                reader.Close();
            }

            // Attach CellClick event to handle button clicks
            dataGridView.CellClick += DataGridView_CellClick;
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is clicked
            {
                if (e.ColumnIndex == dataGridView.Columns["Action"].Index)
                {
                    int notificationId = (int)dataGridView.Rows[e.RowIndex].Cells["NotificationId"].Value;
                    string message = dataGridView.Rows[e.RowIndex].Cells["Message"].Value.ToString();

                    // Determine the action based on the button clicked
                    if (message.Contains("delivery boy request"))
                    {
                        AcceptDeliveryBoyRequest(notificationId);
                    }
                    else if (message.Contains("order ready"))
                    {
                        CollectOrder(notificationId);
                    }
                    else if (message.Contains("order delivered"))
                    {
                        CompleteOrder(notificationId);
                    }
                }
            }
        }

        // Placeholder methods for handling actions
        private void AcceptDeliveryBoyRequest(int notificationId)
        {
            // Logic to accept the delivery boy request
            MessageBox.Show($"Accepted delivery boy request for notification ID: {notificationId}");
            // Optionally, update the database and refresh notifications
        }

        private void CollectOrder(int notificationId)
        {
            // Logic to mark the order as collected
            MessageBox.Show($"Collected order for notification ID: {notificationId}");
            // Optionally, update the database and refresh notifications
        }

        private void CompleteOrder(int notificationId)
        {
            // Logic to mark the order as completed
            MessageBox.Show($"Completed order for notification ID: {notificationId}");
            // Optionally, update the database and refresh notifications
        }
        private void LoadOrderStatus()
        {
            // Clear existing data in DataGridView
            dataGridView.Columns.Clear();

            // Set up the DataGridView columns
            dataGridView.Columns.Add("OrderId", "Order ID");
            dataGridView.Columns.Add("User  Email", "User  Email");
            dataGridView.Columns.Add("Order Status", "Order Status");

            // Fetch order statuses from the database
            string query = "SELECT OrderId, UserEmail, Status FROM GroceryOrders";
            using (SqlCommand command = new SqlCommand(query, cn))
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int orderId = reader.GetInt32(0);
                    string userEmail = reader.GetString(1);
                    string status = reader.GetString(2);

                    // Add a new row for each order status
                    dataGridView.Rows.Add(orderId, userEmail, status);
                }

                reader.Close();
            }
        }
        private void LoadOrders()
        {
            // Clear existing data in DataGridView
            dataGridView.Columns.Clear();

            // Set up the DataGridView columns
            dataGridView.Columns.Add("OrderId", "Order ID");
            dataGridView.Columns.Add("User Email", "User  Email");
            dataGridView.Columns.Add("OrderDate", "Order Date");
            dataGridView.Columns.Add("TotalAmount", "Total Amount");
            dataGridView.Columns.Add("Status", "Status");
            dataGridView.Columns.Add("DeliveryAddress", "Delivery Address");
            dataGridView.Columns.Add("GroceryItems", "Grocery Items");

            // Fetch orders from the database
            string query = "SELECT OrderId, UserEmail, OrderDate, TotalAmount, Status, DeliveryAddress, GroceryItems FROM GroceryOrders";
            using (SqlCommand command = new SqlCommand(query, cn))
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int orderId = reader.GetInt32(0);
                    string userEmail = reader.GetString(1);
                    DateTime orderDate = reader.GetDateTime(2);
                    decimal totalAmount = reader.GetDecimal(3);
                    string status = reader.GetString(4);
                    string deliveryAddress = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    string groceryItems = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);

                    // Add a new row for each order
                    dataGridView.Rows.Add(orderId, userEmail, orderDate, totalAmount, status, deliveryAddress, groceryItems);
                }

                reader.Close();
            }
        }

        private void HandleAction(string action)
        {
            switch (action)
            {
                case "View":
                    // View current orders or notification
                    break;
                case "Accept":
                    // Accept the delivery order
                    break;
                case "Complete":
                    // Mark order as completed (order delivered)
                    break;
                case "Refresh":
                    // Refresh data (e.g., new orders, order status)
                    break;
                default:
                    break;
            }
        }

        private void DeliveryPartnerDashboard_Load(object sender, EventArgs e)
        {
            // Initialize and open the database connection
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