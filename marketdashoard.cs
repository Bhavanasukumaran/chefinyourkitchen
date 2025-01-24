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
    public partial class marketdashoard : Form
    {
        private SqlConnection cn;
        private Panel sideMenuPanel;
        private Panel mainContentPanel;
        private Label lblTitle;
        private DataGridView dataGridView;
        public marketdashoard()
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
                Text = "Market Dashboard",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(10, 20),
                Size = new Size(200, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            sideMenuPanel.Controls.Add(dashboardLabel);

            // Menu Buttons
            string[] menuItems = { "View Orders", "Prepare Ingredients", "View Earnings" };
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
                Text = "Market Dashboard Overview",
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

            string[] actionButtons = { "View", "Prepare", "Track" };
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
            if (section == "Prepare Ingredients")
            {
                // Load the grocery items for the selected order
                if (dataGridView.SelectedRows.Count > 0)
                {
                    int orderId = (int)dataGridView.SelectedRows[0].Cells["OrderId"].Value;
                    PrepareIngredients(orderId);
                }
                else
                {
                    MessageBox.Show("Please select an order to prepare.", "No Order Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                LoadData(section); // Placeholder for loading relevant data
            }
        }
        private void PrepareIngredients(int orderId)
        {
            // Update the order status to "Preparing Order"
            string updateQuery = "UPDATE GroceryOrders SET Status = 'Preparing Order' WHERE OrderId = @OrderId";
            using (SqlCommand command = new SqlCommand(updateQuery, cn))
            {
                command.Parameters.AddWithValue("@OrderId", orderId);
                command.ExecuteNonQuery();
            }

            // Load grocery order items for the selected order
            LoadGroceryOrderItems(orderId);
            MessageBox.Show($"Order {orderId} is now being prepared.", "Preparing Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadGroceryOrderItems(int orderId)
        {
            // Create a new DataGridView for displaying order items
            DataGridView orderItemsGridView = new DataGridView
            {
                Location = new Point(20, 730), // Adjust location as needed
                Size = new Size(920, 200),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
            mainContentPanel.Controls.Add(orderItemsGridView);

            // Set up the DataGridView columns for order items
            orderItemsGridView.Columns.Add("ItemName", "Item Name");
            orderItemsGridView.Columns.Add("Price", "Price");

            // Fetch order items from the database
            string query = "SELECT ItemName, Price FROM GroceryOrderItems WHERE OrderId = @OrderId";
            using (SqlCommand command = new SqlCommand(query, cn))
            {
                command.Parameters.AddWithValue("@OrderId", orderId);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string itemName = reader.GetString(0);
                    decimal price = reader.GetDecimal(1);

                    // Add a new row for each item
                    orderItemsGridView.Rows.Add(itemName, price);
                }

                reader.Close();
            }
        }

        private void LoadData(string section)
        {
            if (section == "View Orders")
            {
                LoadOrders();
            }
            else if (section == "View Earnings")
            {
                LoadEarnings();
            }
        }

        private void LoadEarnings()
        {
            // Clear existing data in DataGridView
            dataGridView.Columns.Clear();

            // Set up the DataGridView columns for earnings
            dataGridView.Columns.Add("Earnings", "Total Earnings");

            // Fetch total earnings from the database
            string query = "SELECT SUM(TotalAmount) FROM GroceryOrders WHERE Status = 'Accepted'";
            using (SqlCommand command = new SqlCommand(query, cn))
            {
                var result = command.ExecuteScalar();
                decimal totalEarnings = result != DBNull.Value ? Convert.ToDecimal(result) : 0;

                // Add the total earnings to the DataGridView
                dataGridView.Rows.Add(totalEarnings);
            }
        }


        private void LoadOrders()
        {
            // Clear existing data in DataGridView
            dataGridView.Columns.Clear();

            // Set up the DataGridView columns based on the new schema
            dataGridView.Columns.Add("OrderId", "Order ID");
            dataGridView.Columns.Add("User Email", "User  Email");
            dataGridView.Columns.Add("OrderDate", "Order Date");
            dataGridView.Columns.Add("TotalAmount", "Total Amount");
            dataGridView.Columns.Add("Status", "Status");
            dataGridView.Columns.Add("DeliveryAddress", "Delivery Address");
            dataGridView.Columns.Add("GroceryItems", "Grocery Items");
            dataGridView.Columns.Add("Accept", "Accept");
            dataGridView.Columns.Add("Reject", "Reject");

            // Fetch orders from the database
            string query = "SELECT OrderId, UserEmail, OrderDate, TotalAmount, Status, DeliveryAddress, GroceryItems FROM GroceryOrders";
            SqlCommand command = new SqlCommand(query, cn);
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
                int rowIndex = dataGridView.Rows.Add(orderId, userEmail, orderDate, totalAmount, status, deliveryAddress, groceryItems);

                // Create Accept and Reject buttons
                DataGridViewButtonCell acceptButton = new DataGridViewButtonCell
                {
                    Value = "Accept",
                    Style = { BackColor = Color.Green, ForeColor = Color.White }
                };
                DataGridViewButtonCell rejectButton = new DataGridViewButtonCell
                {
                    Value = "Reject",
                    Style = { BackColor = Color.Red, ForeColor = Color.White }
                };

                dataGridView.Rows[rowIndex].Cells["Accept"] = acceptButton;
                dataGridView.Rows[rowIndex].Cells["Reject"] = rejectButton;
            }

            reader.Close();
            dataGridView.CellClick += DataGridView_CellClick; // Attach event handler for button clicks
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is clicked
            {
                if (e.ColumnIndex == dataGridView.Columns["Accept"].Index)
                {
                    // Handle Accept button click
                    int orderId = (int)dataGridView.Rows[e.RowIndex].Cells["OrderId"].Value;
                    AcceptOrder(orderId);
                }
                else if (e.ColumnIndex == dataGridView.Columns["Reject"].Index)
                {
                    // Handle Reject button click
                    int orderId = (int)dataGridView.Rows[e.RowIndex].Cells["OrderId"].Value;
                    RejectOrder(orderId);
                }
            }
        }

        private void AcceptOrder(int orderId)
        {
            // Logic to accept the order
            string query = "UPDATE GroceryOrders SET Status = 'Accepted' WHERE OrderId = @OrderId";
            using (SqlCommand command = new SqlCommand(query, cn))
            {
                command.Parameters.AddWithValue("@OrderId", orderId);
                command.ExecuteNonQuery();
            }

            MessageBox.Show($"Order {orderId} has been accepted. Notifying delivery partner.", "Order Accepted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadOrders(); // Refresh the orders to reflect changes
        }

        private void RejectOrder(int orderId)
        {
            // Logic to reject the order
            string query = "UPDATE GroceryOrders SET Status = 'Rejected' WHERE OrderId = @OrderId";
            using (SqlCommand command = new SqlCommand(query, cn))
            {
                command.Parameters.AddWithValue("@OrderId", orderId);
                command.ExecuteNonQuery();
            }

            MessageBox.Show($"Order {orderId} has been rejected.", "Order Rejected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadOrders(); // Refresh the orders to reflect changes
        }

        private void HandleAction(string action)
        {
            switch (action)
            {
                case "View":
                    // Logic to view orders
                    break;
                case "Prepare":
                    // Logic to mark ingredients as prepared
                    break;
                case "Track":
                    // Logic to track delivery status
                    break;
                case "Refresh":
                    // Logic to refresh data displayed
                    break;
                default:
                    break;
            }
        }

        private void MarketDashoard_Load(object sender, EventArgs e)
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
