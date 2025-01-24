using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Chef_in_your_kitchen
{
    public partial class UserDashboard : Form
    {
        private SqlConnection cn;
        private Panel sideMenuPanel;
        private Panel mainContentPanel;
        private Label lblTitle;
        private TextBox txtName, txtAddress, txtEmail, txtPhone, txtPassword;
        private PictureBox profilePictureBox;
        private Button saveButton, uploadImageButton, logoutButton;
        private readonly string userEmail; // Make it readonly

        public UserDashboard(string email = "")
        {
            InitializeComponent();
            userEmail = email;
            SetupUI();
            ShowWelcomeMessage();
            if (!string.IsNullOrEmpty(email))
            {
                LoadUserData(); // Corrected method name
            }
        }

        private void SetupUI()
        {
            InitializePanels();
            InitializeMenuButtons();
            InitializeMainContent();
            HideProfileControls(); // Hide profile controls initially
        }

        private void InitializePanels()
        {
            sideMenuPanel = new Panel
            {
                BackColor = Color.FromArgb(51, 51, 76),
                Dock = DockStyle.Left,
                Size = new Size(220, 800)
            };

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
            Label dashboardLabel = new Label
            {
                Text = "User  Dashboard",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(10, 20),
                Size = new Size(200, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            sideMenuPanel.Controls.Add(dashboardLabel);

            string[] menuItems = { "Edit Profile", "Browse Chefs","Order Groceries", "Track Groceries", "Submit Complaint", "Payment Options" };
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

            // Add Logout Button
            logoutButton = new Button
            {
                Text = "Logout",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(51, 51, 76),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, positionY + 50),
                Size = new Size(220, 45),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0)
            };
            logoutButton.FlatAppearance.BorderSize = 0;
            logoutButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(75, 75, 112);
            logoutButton.Click += LogoutButton_Click;
            sideMenuPanel.Controls.Add(logoutButton);
        }

        private void InitializeMainContent()
        {
            lblTitle = new Label
            {
                Text = "Welcome to User Dashboard",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(400, 30)
            };
            mainContentPanel.Controls.Add(lblTitle);

            // Initialize Labels and TextBoxes for profile fields
            Label lblName = new Label { Text = "Name:", Location = new Point(20, 70), Size = new Size(100, 30) };
            txtName = new TextBox { Location = new Point(120, 70), Size = new Size(200, 30) };

            Label lblAddress = new Label { Text = "Address:", Location = new Point(20, 110), Size = new Size(100, 30) };
            txtAddress = new TextBox { Location = new Point(120, 110), Size = new Size(200, 30) };

            Label lblEmail = new Label { Text = "Email:", Location = new Point(20, 150), Size = new Size(100, 30) };
            txtEmail = new TextBox { Location = new Point(120, 150), Size = new Size(200, 30), ReadOnly = true };

            Label lblPhone = new Label { Text = "Phone:", Location = new Point(20, 190), Size = new Size(100, 30) };
            txtPhone = new TextBox { Location = new Point(120, 190), Size = new Size(200, 30) };

            Label lblPassword = new Label { Text = "Password:", Location = new Point(20, 230), Size = new Size(100, 30) };
            txtPassword = new TextBox { Location = new Point(120, 230), Size = new Size(200, 30), PasswordChar = '*' };

            profilePictureBox = new PictureBox
            {
                Location = new Point(350, 70),
                Size = new Size(100, 100),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BorderStyle = BorderStyle.FixedSingle
            };

            uploadImageButton = new Button
            {
                Text = "Upload Image",
                Location = new Point(350, 180),
                Size = new Size(100, 30)
            };
            uploadImageButton.Click += UploadImageButton_Click;

            saveButton = new Button
            {
                Text = "Save Changes",
                Location = new Point(120, 270),
                Size = new Size(100, 30)
            };
            saveButton.Click += SaveButton_Click;

            // Add controls to the main content panel
            mainContentPanel.Controls.Add(lblName);
            mainContentPanel.Controls.Add(txtName);
            mainContentPanel.Controls.Add(lblAddress);
            mainContentPanel.Controls.Add(txtAddress);
            mainContentPanel.Controls.Add(lblEmail);
            mainContentPanel.Controls.Add(txtEmail);
            mainContentPanel.Controls.Add(lblPhone);
            mainContentPanel.Controls.Add(txtPhone);
            mainContentPanel.Controls.Add(lblPassword);
            mainContentPanel.Controls.Add(txtPassword);
            mainContentPanel.Controls.Add(profilePictureBox);
            mainContentPanel.Controls.Add(uploadImageButton);
            mainContentPanel.Controls.Add(saveButton);
        }

        private void LoadSection(string menuItem)
        {
            if (menuItem == "Edit Profile")
            {
                ShowProfileControls();
                LoadUserData();
            }
            else if (menuItem == "Browse Chefs")
            {
                ShowBrowseChefsPanel();
            }
            else if (menuItem == "Order Groceries")
            {
                ShowOrderGroceriesPanel(); // Handle ordering groceries
            }
            else if (menuItem == "Track Groceries")
            {
                ShowTrackGroceriesPanel();
            }
            else if (menuItem == "Submit Complaint")
            {
                ShowSubmitComplaintPanel();
            }
            else if (menuItem == "Payment Options")
            {
                ShowPaymentOptionsPanel();
            }
            else
            {
                MessageBox.Show($"Loading {menuItem} section...");
            }
        }

        private void ShowPaymentOptionsPanel()
        {
            // Clear previous content
            mainContentPanel.Controls.Clear();

            // Create a label for the title
            Label lblTitle = new Label
            {
                Text = "Payment Options",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(400, 30)
            };
            mainContentPanel.Controls.Add(lblTitle);

            // Simulated order data (replace this with actual data from your database)
            var orders = GetOrders(); // Method to fetch orders from the database
            int positionY = 70; // Starting Y position for displaying orders

            foreach (var order in orders)
            {
                // Create a label for the order details
                Label lblOrder = new Label
                {
                    Text = $"Order ID: {order.OrderId}, Cost: ${order.Cost}",
                    Location = new Point(20, positionY),
                    Size = new Size(300, 30)
                };
                mainContentPanel.Controls.Add(lblOrder);

                // Create a Pay button for each order
                Button payButton = new Button
                {
                    Text = "Pay",
                    Location = new Point(350, positionY),
                    Size = new Size(100, 30)
                };
                mainContentPanel.Controls.Add(payButton);

                // Add event handler for the Pay button
                payButton.Click += (s, e) =>
                {
                    // Here you can add actual payment processing logic if needed
                    MessageBox.Show("Payment Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                };

                positionY += 40; // Increment Y position for the next order
            }

            if (orders.Count == 0)
            {
                Label lblNoOrders = new Label
                {
                    Text = "No orders available.",
                    Location = new Point(20, positionY),
                    Size = new Size(300, 30)
                };
                mainContentPanel.Controls.Add(lblNoOrders);
            }
        }

        // Simulated method to fetch orders from the database
        private List<Order> GetOrders()
        {
            // Replace this with actual database retrieval logic
            return new List<Order>
    {
        new Order { OrderId = 1, Cost = 20.00 },
        new Order { OrderId = 2, Cost = 35.50 },
        new Order { OrderId = 3, Cost = 15.75 }
    };
        }

        // Order class to represent an order
        public class Order
        {
            public int OrderId { get; set; }
            public double Cost { get; set; }
        }
        private void ShowSubmitComplaintPanel()
        {
            // Clear previous content
            mainContentPanel.Controls.Clear();

            // Create a label for the title
            Label lblTitle = new Label
            {
                Text = "Submit a Complaint",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(400, 30)
            };
            mainContentPanel.Controls.Add(lblTitle);

            // Add a TextBox for the complaint
            TextBox txtComplaint = new TextBox
            {
                Location = new Point(20, 70),
                Size = new Size(400, 100),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            mainContentPanel.Controls.Add(txtComplaint);

            // Add placeholder behavior (if PlaceholderText is not available)
            Label placeholderLabel = new Label
            {
                Text = "Enter your complaint here...",
                ForeColor = Color.Gray,
                Location = new Point(25, 75), // Adjust position to fit inside the TextBox
                Size = new Size(400, 30),
                BackColor = Color.Transparent
            };
            mainContentPanel.Controls.Add(placeholderLabel);

            // Event handler to show/hide placeholder
            txtComplaint.Enter += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtComplaint.Text))
                {
                    placeholderLabel.Visible = false;
                }
            };

            txtComplaint.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtComplaint.Text))
                {
                    placeholderLabel.Visible = true;
                }
            };

            // Add a button to submit the complaint
            Button submitButton = new Button
            {
                Text = "Submit Complaint",
                Location = new Point(20, 180),
                Size = new Size(150, 30)
            };
            mainContentPanel.Controls.Add(submitButton);

            // Add event handler for the submit button
            submitButton.Click += (s, e) =>
            {
                string complaintText = txtComplaint.Text.Trim();
                if (string.IsNullOrEmpty(complaintText))
                {
                    MessageBox.Show("Please enter a complaint before submitting.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Call the method to save the complaint to the database
                SubmitComplaint(complaintText);
            };
        }

        private void SubmitComplaint(string complaintText)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf"";Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Complaints (User Email, ComplaintText, ComplaintDate) VALUES (@Email, @ComplaintText, GETDATE())";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", userEmail);
                        command.Parameters.AddWithValue("@ComplaintText", complaintText);

                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Your complaint has been submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error submitting complaint: " + ex.Message);
                }
            }
        }

        private void ShowOrderGroceriesPanel()
        {
            // Clear previous content
            mainContentPanel.Controls.Clear();

            // Create a label for the title
            Label lblTitle = new Label
            {
                Text = "Order Groceries",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(400, 30)
            };
            mainContentPanel.Controls.Add(lblTitle);

            // Add a TextBox to input grocery items
            TextBox txtItem = new TextBox
            {
                Location = new Point(20, 70),
                Size = new Size(200, 30)
            };
            mainContentPanel.Controls.Add(txtItem);

            // Add a label for placeholder text
            Label placeholderLabel = new Label
            {
                Text = "Enter grocery item",
                ForeColor = Color.Gray,
                Location = new Point(24, 75), // Adjust to fit the TextBox
                Size = new Size(200, 30)
            };
            mainContentPanel.Controls.Add(placeholderLabel);

            // Add a button to add items to the order
            Button addButton = new Button
            {
                Text = "Add Item",
                Location = new Point(230, 70),
                Size = new Size(100, 30)
            };
            mainContentPanel.Controls.Add(addButton);

            // Add a list view to display added items
            ListBox orderListBox = new ListBox
            {
                Location = new Point(20, 110),
                Size = new Size(310, 200)
            };
            mainContentPanel.Controls.Add(orderListBox);

            // Add a button to place the order
            Button orderButton = new Button
            {
                Text = "Place Order",
                Location = new Point(20, 320),
                Size = new Size(310, 30)
            };
            mainContentPanel.Controls.Add(orderButton);

            // Event handlers for the TextBox to show/hide placeholder
            txtItem.Enter += (sender, e) =>
            {
                if (txtItem.Text == "")
                {
                    placeholderLabel.Visible = false;
                }
            };

            txtItem.Leave += (sender, e) =>
            {
                if (txtItem.Text == "")
                {
                    placeholderLabel.Visible = true;
                }
            };

            // Add item button click event
            addButton.Click += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(txtItem.Text))
                {
                    orderListBox.Items.Add(txtItem.Text);
                    txtItem.Clear();
                    placeholderLabel.Visible = true; // Show placeholder when cleared
                }
            };

            // Place order button click event
            orderButton.Click += (sender, e) =>
            {
                PlaceGroceryOrder(orderListBox.Items.Cast<string>().ToList());
            };
        }

        private void PlaceGroceryOrder(List<string> orderItems)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf"";Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Start a transaction to ensure all items are added together
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        string orderQuery = "INSERT INTO GroceryOrders (UserEmail, Status, OrderDate, TotalAmount) OUTPUT INSERTED.OrderId VALUES (@Email, 'Processing', GETDATE(), 0)";
                        using (SqlCommand command = new SqlCommand(orderQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Email", userEmail);

                            // Get the OrderId for the inserted order
                            int orderId = (int)command.ExecuteScalar();

                            decimal totalAmount = 0;
                            foreach (var item in orderItems)
                            {
                                // For now, assume each item costs $10
                                decimal itemPrice = 10.00M;
                                totalAmount += itemPrice;

                                string itemQuery = "INSERT INTO GroceryOrderItems (OrderId, ItemName, Price) VALUES (@OrderId, @ItemName, @Price)";
                                using (SqlCommand itemCommand = new SqlCommand(itemQuery, connection, transaction))
                                {
                                    itemCommand.Parameters.AddWithValue("@OrderId", orderId);
                                    itemCommand.Parameters.AddWithValue("@ItemName", item);
                                    itemCommand.Parameters.AddWithValue("@Price", itemPrice);
                                    itemCommand.ExecuteNonQuery();
                                }
                            }

                            // Update total amount after adding all items
                            string updateOrderQuery = "UPDATE GroceryOrders SET TotalAmount = @TotalAmount WHERE OrderId = @OrderId";
                            using (SqlCommand updateCommand = new SqlCommand(updateOrderQuery, connection, transaction))
                            {
                                updateCommand.Parameters.AddWithValue("@TotalAmount", totalAmount);
                                updateCommand.Parameters.AddWithValue("@OrderId", orderId);
                                updateCommand.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Your grocery order has been placed successfully!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error placing grocery order: " + ex.Message);
                }
            }
        }


        private void ShowTrackGroceriesPanel()
        {
            // Clear the main content panel
            mainContentPanel.Controls.Clear();

            // Create a label for the title
            Label lblTitle = new Label
            {
                Text = "Track Your Grocery Orders",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(400, 30)
            };
            mainContentPanel.Controls.Add(lblTitle);

            // Create a FlowLayoutPanel to display the orders
            FlowLayoutPanel ordersPanel = new FlowLayoutPanel
            {
                Location = new Point(20, 70),
                Size = new Size(600, 400),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown
            };
            mainContentPanel.Controls.Add(ordersPanel);

            // Load the grocery orders
            LoadGroceryOrders(ordersPanel);

            // Show a message indicating that the order has been received
            MessageBox.Show("Your grocery order has been successfully received by the market!", "Order Received", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadGroceryOrders(FlowLayoutPanel ordersPanel)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf"";Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT OrderId, Status, OrderDate, TotalAmount FROM GroceryOrders WHERE UserEmail = @Email ORDER BY OrderDate DESC";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", userEmail);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Create a panel for each order
                                Panel orderPanel = new Panel
                                {
                                    Size = new Size(600, 60),
                                    BackColor = Color.LightGray,
                                    BorderStyle = BorderStyle.FixedSingle
                                };

                                Label lblOrderId = new Label
                                {
                                    Text = $"Order ID: {reader["OrderId"]}",
                                    Location = new Point(10, 10),
                                    Size = new Size(200, 20)
                                };
                                Label lblStatus = new Label
                                {
                                    Text = $"Status: {reader["Status"]}",
                                    Location = new Point(10, 30),
                                    Size = new Size(200, 20)
                                };
                                Label lblDate = new Label
                                {
                                    Text = $"Order Date: {reader["OrderDate"]}",
                                    Location = new Point(220, 10),
                                    Size = new Size(200, 20)
                                };
                                Label lblAmount = new Label
                                {
                                    Text = $"Amount: ${reader["TotalAmount"]}",
                                    Location = new Point(220, 30),
                                    Size = new Size(200, 20)
                                };

                                // Add the labels to the order panel
                                orderPanel.Controls.Add(lblOrderId);
                                orderPanel.Controls.Add(lblStatus);
                                orderPanel.Controls.Add(lblDate);
                                orderPanel.Controls.Add(lblAmount);

                                // Add the order panel to the orders flow panel
                                ordersPanel.Controls.Add(orderPanel);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading grocery orders: " + ex.Message);
                }
            }
        }

        private void ShowProfileControls()
        {
            lblTitle.Text = "Edit Profile";
            saveButton.Visible = true;
            txtName.Visible = true;
            txtAddress.Visible = true;
            txtEmail.Visible = true;
            txtPhone.Visible = true;
            txtPassword.Visible = true;
            profilePictureBox.Visible = true;
            uploadImageButton.Visible = true;
        }

        private void HideProfileControls()
        {
            saveButton.Visible = false;
            txtName.Visible = false;
            txtAddress.Visible = false;
            txtEmail.Visible = false;
            txtPhone.Visible = false;
            txtPassword.Visible = false;
            profilePictureBox.Visible = false;
            uploadImageButton.Visible = false;
        }

        private void LoadUserData()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf"";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM UserTable WHERE Email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", userEmail);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtName.Text = reader["Name"].ToString();
                                txtAddress.Text = reader["Address"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                                txtPhone.Text = reader["Phone"].ToString();
                                txtPassword.Text = reader["Password"].ToString();

                                // Load profile picture if it exists
                                string imagePath = reader["ProfileImagePath"].ToString();
                                if (File.Exists(imagePath))
                                {
                                    profilePictureBox.Image = Image.FromFile(imagePath);
                                }
                                else
                                {
                                    profilePictureBox.Image = null;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading user data: " + ex.Message);
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf"";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE UserTable SET Name = @Name, Address = @Address, Phone = @Phone, Password = @Password WHERE Email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", txtName.Text);
                        command.Parameters.AddWithValue("@Address", txtAddress.Text);
                        command.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        command.Parameters.AddWithValue("@Password", txtPassword.Text);
                        command.Parameters.AddWithValue("@Email", userEmail);

                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Profile updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving profile: " + ex.Message);
                }
            }
        }

        private void UploadImageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    profilePictureBox.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            // Logic for logging out, e.g., closing the current form and showing the login form
            this.Close();
        }

        private void ShowBrowseChefsPanel()
        {
            // Clear the main content panel
            mainContentPanel.Controls.Clear();

            // Create a label for the title
            Label lblTitle = new Label
            {
                Text = "Browse Chefs",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(400, 30)
            };
            mainContentPanel.Controls.Add(lblTitle);

            // Add a TextBox to search chefs
            TextBox txtSearch = new TextBox
            {
                Location = new Point(20, 70),
                Size = new Size(200, 30)
            };

            // Set placeholder text manually
            txtSearch.Text = "Search chefs...";
            txtSearch.ForeColor = Color.Gray;

            // Handle focus (Enter) and focus lost (Leave) events
            txtSearch.Enter += (sender, args) =>
            {
                if (txtSearch.Text == "Search chefs...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };

            txtSearch.Leave += (sender, args) =>
            {
                if (txtSearch.Text == "")
                {
                    txtSearch.Text = "Search chefs...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };

            mainContentPanel.Controls.Add(txtSearch);

            mainContentPanel.Controls.Add(txtSearch);

            Button searchButton = new Button
            {
                Text = "Search",
                Location = new Point(230, 70),
                Size = new Size(100, 30)
            };
            mainContentPanel.Controls.Add(searchButton);

            // Create a FlowLayoutPanel to display chefs
            FlowLayoutPanel chefsPanel = new FlowLayoutPanel
            {
                Location = new Point(20, 110),
                Size = new Size(600, 400),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown
            };
            mainContentPanel.Controls.Add(chefsPanel);

            searchButton.Click += (sender, args) =>
            {
                // Perform search action
                SearchChefs(txtSearch.Text, chefsPanel);
            };
        }

        private void SearchChefs(string searchTerm, FlowLayoutPanel chefsPanel)
        {
            // Clear previous results
            chefsPanel.Controls.Clear();

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\path\to\Database1.mdf"";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ChefName FROM ChefProfile WHERE Name LIKE @SearchTerm";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string chefName = reader["Name"].ToString();
                                Button chefButton = new Button
                                {
                                    Text = chefName,
                                    Size = new Size(200, 50)
                                };
                                chefButton.Click += (s, e) => MessageBox.Show($"You selected {chefName}");
                                chefsPanel.Controls.Add(chefButton);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading chefs: " + ex.Message);
                }
            }
        }
        private void ShowWelcomeMessage()
        {
            lblTitle.Text = "Welcome to User Dashboard!";
        }
        private void UserDashboard_Load(object sender, EventArgs e)
        {
            // Any initialization logic you need to perform when the form loads
            lblTitle.Text = "Welcome to the User Dashboard!";
        }

    }
}
