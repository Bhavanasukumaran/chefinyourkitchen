using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Chef_in_your_kitchen
{
    public partial class chefdashboard : Form
    {
        private SqlConnection cn;
        private Panel sideMenuPanel;
        private Panel mainContentPanel;
        private Label lblTitle;
        private DataGridView dataGridView;

        public chefdashboard()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            InitializePanels();
            InitializeMenuButtons();
            InitializeMainContent();
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
            string[] menuItems = {
                "Profile Management", "Booking Management", "Ingredient Requirements",
                "Reviews and Feedback", "Logout"
            };

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
        }

        private void InitializeMainContent()
        {
            lblTitle = new Label
            {
                Text = "Chef Dashboard Overview",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(400, 30)
            };
            mainContentPanel.Controls.Add(lblTitle);

            dataGridView = new DataGridView
            {
                Location = new Point(20, 70),
                Size = new Size(920, 600),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            mainContentPanel.Controls.Add(dataGridView);
        }

        private void LoadSection(string section)
        {
            lblTitle.Text = section;

            switch (section)
            {
                case "Profile Management":
                    LoadProfileManagement();
                    break;
                case "Booking Management":
                    LoadBookingManagement();
                    break;
                case "Ingredient Requirements":
                    LoadIngredientRequirements();
                    break;
                case "Reviews and Feedback":
                    LoadReviewsAndFeedback();
                    break;
                case "Logout":
                    Logout();
                    break;
                default:
                    break;
            }
        }

        private void LoadProfileManagement()
        {
            lblTitle.Text = "Profile Management";

            // Clear existing controls in the main content panel
            mainContentPanel.Controls.Clear();
            mainContentPanel.Controls.Add(lblTitle);

            // Create controls to display and edit profile information
            int positionY = 70;

            // Name
            Label lblName = new Label { Text = "Name:", Location = new Point(20, positionY), AutoSize = true };
            TextBox txtName = new TextBox { Location = new Point(100, positionY), Width = 200 };
            mainContentPanel.Controls.Add(lblName);
            mainContentPanel.Controls.Add(txtName);
            positionY += 30;

            // Email
            Label lblEmail = new Label { Text = "Email:", Location = new Point(20, positionY), AutoSize = true };
            TextBox txtEmail = new TextBox { Location = new Point(100, positionY), Width = 200 };
            mainContentPanel.Controls.Add(lblEmail);
            mainContentPanel.Controls.Add(txtEmail);
            positionY += 30;

            // Category
            Label lblCategory = new Label { Text = "Category:", Location = new Point(20, positionY), AutoSize = true };
            TextBox txtCategory = new TextBox { Location = new Point(100, positionY), Width = 200 };
            mainContentPanel.Controls.Add(lblCategory);
            mainContentPanel.Controls.Add(txtCategory);
            positionY += 30;

            // Specialty
            Label lblSpecialty = new Label { Text = "Specialty:", Location = new Point(20, positionY), AutoSize = true };
            TextBox txtSpecialty = new TextBox { Location = new Point(100, positionY), Width = 200 };
            mainContentPanel.Controls.Add(lblSpecialty);
            mainContentPanel.Controls.Add(txtSpecialty);
            positionY += 30;

            // Experience Years
            Label lblExperience = new Label { Text = "Experience (Years):", Location = new Point(20, positionY), AutoSize = true };
            NumericUpDown numExperience = new NumericUpDown { Location = new Point(150, positionY), Width = 50, Minimum = 0, Maximum = 50 };
            mainContentPanel.Controls.Add(lblExperience);
            mainContentPanel.Controls.Add(numExperience);
            positionY += 30;

            // Rating
            Label lblRating = new Label { Text = "Rating (0-5):", Location = new Point(20, positionY), AutoSize = true };
            NumericUpDown numRating = new NumericUpDown { Location = new Point(150, positionY), Width = 50, Minimum = 0, Maximum = 5, DecimalPlaces = 2, Increment = 0.1M };
            mainContentPanel.Controls.Add(lblRating);
            mainContentPanel.Controls.Add(numRating);
            positionY += 30;

            // Bio
            Label lblBio = new Label { Text = "Bio:", Location = new Point(20, positionY), AutoSize = true };
            TextBox txtBio = new TextBox { Location = new Point(100, positionY), Width = 200, Multiline = true, Height = 60 };
            mainContentPanel.Controls.Add(lblBio);
            mainContentPanel.Controls.Add(txtBio);
            positionY += 70;

            // Contact Number
            Label lblContact = new Label { Text = "Contact Number:", Location = new Point(20, positionY), AutoSize = true };
            TextBox txtContact = new TextBox { Location = new Point(150, positionY), Width = 200 };
            mainContentPanel.Controls.Add(lblContact);
            mainContentPanel.Controls.Add(txtContact);
            positionY += 30;

            // Address
            Label lblAddress = new Label { Text = "Address:", Location = new Point(20, positionY), AutoSize = true };
            TextBox txtAddress = new TextBox { Location = new Point(100, positionY), Width = 200 };
            mainContentPanel.Controls.Add(lblAddress);
            mainContentPanel.Controls.Add(txtAddress);
            positionY += 30;

            // Profile Picture URL
            Label lblProfilePicture = new Label { Text = "Profile Picture URL:", Location = new Point(20, positionY), AutoSize = true };
            TextBox txtProfilePicture = new TextBox { Location = new Point(150, positionY), Width = 200 };
            mainContentPanel.Controls.Add(lblProfilePicture);
            mainContentPanel.Controls.Add(txtProfilePicture);
            positionY += 30;

            // Load the chef's profile data from the database
            LoadChefProfileData(txtName, txtEmail, txtCategory, txtSpecialty, numExperience, numRating, txtBio, txtContact, txtAddress, txtProfilePicture);

            // Save Button
            Button btnSave = new Button { Text = "Save Changes", Location = new Point(100, positionY), Width = 100 };
            btnSave.Click += (s, e) => SaveProfileChanges(txtName.Text, txtEmail.Text, txtCategory.Text, txtSpecialty.Text, (int)numExperience.Value, (decimal)numRating.Value, txtBio.Text, txtContact.Text, txtAddress.Text, txtProfilePicture.Text);
            mainContentPanel.Controls.Add(btnSave);
        }

        private void LoadChefProfileData(TextBox txtName, TextBox txtEmail, TextBox txtCategory, TextBox txtSpecialty, NumericUpDown numExperience, NumericUpDown numRating, TextBox txtBio, TextBox txtContact, TextBox txtAddress, TextBox txtProfilePicture)
        {
            int chefId = 1; // Replace with actual chef ID

            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                string query = "SELECT Name, Email, Category, Specialty, ExperienceYears, Rating, Bio, ContactNumber, Address, ProfilePictureURL FROM ChefProfile WHERE ChefId = @ChefId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ChefId", chefId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtName.Text = reader.GetString(0);
                            txtEmail.Text = reader.GetString(1);
                            txtCategory.Text = reader.GetString(2);
                            txtSpecialty.Text = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                            numExperience.Value = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                            numRating.Value = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5);
                            txtBio.Text = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                            txtContact.Text = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                            txtAddress.Text = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                            txtProfilePicture.Text = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        }
                    }
                }
            }
        }

        private void SaveProfileChanges(string name, string email, string category, string specialty, int experienceYears, decimal rating, string bio, string contactNumber, string address, string profilePictureURL)
        {
            int chefId = 1; // Replace with actual chef ID

            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                string query = "UPDATE ChefProfile SET Name = @Name, Email = @Email, Category = @Category, Specialty = @Specialty, ExperienceYears = @ExperienceYears, Rating = @Rating, Bio = @Bio, ContactNumber = @ContactNumber, Address = @Address, ProfilePictureURL = @ProfilePictureURL WHERE ChefId = @ChefId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Category", category);
                    command.Parameters.AddWithValue("@Specialty", specialty);
                    command.Parameters.AddWithValue("@ExperienceYears", experienceYears);
                    command.Parameters.AddWithValue("@Rating", rating);
                    command.Parameters.AddWithValue("@Bio", bio);
                    command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@ProfilePictureURL", profilePictureURL);
                    command.Parameters.AddWithValue("@ChefId", chefId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Profile updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Error updating profile. Please try again.");
                    }
                }
            }
        }

        private void LoadBookingManagement()
        {
            lblTitle.Text = "Booking Management";

            // Clear existing controls in the main content panel
            mainContentPanel.Controls.Clear();
            mainContentPanel.Controls.Add(lblTitle);

            // Create a DataGridView to display bookings
            DataGridView bookingsDataGridView = new DataGridView
            {
                Location = new Point(20, 70),
                Size = new Size(920, 400),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            mainContentPanel.Controls.Add(bookingsDataGridView);

            // Load bookings data from the database
            LoadBookingsData(bookingsDataGridView);

            // Create Accept and Decline buttons ```csharp
            Button btnAccept = new Button { Text = "Accept Booking", Location = new Point(20, 490), Width = 120 };
            Button btnDecline = new Button { Text = "Decline Booking", Location = new Point(160, 490), Width = 120 };

            btnAccept.Click += (s, e) => AcceptBooking(bookingsDataGridView);
            btnDecline.Click += (s, e) => DeclineBooking(bookingsDataGridView);

            mainContentPanel.Controls.Add(btnAccept);
            mainContentPanel.Controls.Add(btnDecline);

            // Create a TextBox for messaging customers with placeholder behavior
            TextBox txtMessage = CreateMessageTextBox();
            Button btnSendMessage = new Button { Text = "Send Message", Location = new Point(910, 490), Width = 120 };

            btnSendMessage.Click += (s, e) => SendMessageToCustomer(bookingsDataGridView, txtMessage.Text);

            mainContentPanel.Controls.Add(txtMessage);
            mainContentPanel.Controls.Add(btnSendMessage);
        }

        private TextBox CreateMessageTextBox()
        {
            TextBox txtMessage = new TextBox
            {
                Location = new Point(300, 490),
                Width = 600,
                Height = 50,
                Multiline = true,
                Text = "Type your message here...", // Set the placeholder text
                ForeColor = Color.Gray // Set the placeholder text color
            };

            // Handle the Enter event to clear the placeholder
            txtMessage.Enter += (s, e) =>
            {
                if (txtMessage.Text == "Type your message here...")
                {
                    txtMessage.Text = "";
                    txtMessage.ForeColor = Color.Black; // Change text color to black
                }
            };

            // Handle the Leave event to restore the placeholder if empty
            txtMessage.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtMessage.Text))
                {
                    txtMessage.Text = "Type your message here...";
                    txtMessage.ForeColor = Color.Gray; // Change text color back to gray
                }
            };

            return txtMessage;
        }

        private void LoadBookingsData(DataGridView bookingsDataGridView)
        {
            int chefId = 1; // Replace with actual chef ID

            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                string query = "SELECT Id, UserId, BookingTime, SpecialRequests, Status FROM BookingTable WHERE ChefId = @ChefId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ChefId", chefId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable bookingsTable = new DataTable();
                        bookingsTable.Load(reader);
                        bookingsDataGridView.DataSource = bookingsTable;
                    }
                }
            }
        }

        private void AcceptBooking(DataGridView bookingsDataGridView)
        {
            if (bookingsDataGridView.SelectedRows.Count > 0)
            {
                int bookingId = Convert.ToInt32(bookingsDataGridView.SelectedRows[0].Cells["Id"].Value);
                UpdateBookingStatus(bookingId, "Accepted");
            }
            else
            {
                MessageBox.Show("Please select a booking to accept.");
            }
        }

        private void DeclineBooking(DataGridView bookingsDataGridView)
        {
            if (bookingsDataGridView.SelectedRows.Count > 0)
            {
                int bookingId = Convert.ToInt32(bookingsDataGridView.SelectedRows[0].Cells["Id"].Value);
                UpdateBookingStatus(bookingId, "Declined");
            }
            else
            {
                MessageBox.Show("Please select a booking to decline.");
            }
        }

        private void UpdateBookingStatus(int bookingId, string status)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                string query = "UPDATE [dbo].[BookingTable] SET Status = @Status, UpdatedAt = GETDATE() WHERE Id = @BookingId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@BookingId", bookingId);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void SendMessageToCustomer(DataGridView bookingsDataGridView, string message)
        {
            if (bookingsDataGridView.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(bookingsDataGridView.SelectedRows[0].Cells["User Id"].Value);
                // Logic to send message to the customer based on userId
                MessageBox.Show($"Message sent to User ID {userId}: {message}");
            }
            else
            {
                MessageBox.Show("Please select a booking to send a message.");
            }
        }

        private void LoadIngredientRequirements()
        {
            lblTitle.Text = "Ingredient Requirements";

            // Clear existing controls in the main content panel
            mainContentPanel.Controls.Clear();
            mainContentPanel.Controls.Add(lblTitle);

            // Create a DataGridView to display menu items and their ingredient requirements
            DataGridView ingredientsDataGridView = new DataGridView
            {
                Location = new Point(20, 70),
                Size = new Size(920, 400),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ReadOnly = false, // Allow editing
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            mainContentPanel.Controls.Add(ingredientsDataGridView);

            // Load ingredient requirements data from the database
            LoadIngredientData(ingredientsDataGridView);

            // Create a button to submit ingredient requirements
            Button btnSubmit = new Button { Text = "Submit Requirements", Location = new Point(20, 490), Width = 150 };
            btnSubmit.Click += (s, e) => SubmitIngredientRequirements(ingredientsDataGridView);
            mainContentPanel.Controls.Add(btnSubmit);

            // Create a button to request groceries
            Button btnRequestGroceries = new Button { Text = "Request Groceries", Location = new Point(200, 490), Width = 150 };
            btnRequestGroceries.Click += (s, e) => RequestGroceries(ingredientsDataGridView);
            mainContentPanel.Controls.Add(btnRequestGroceries);
        }

        private void LoadIngredientData(DataGridView ingredientsDataGridView)
        {
            int chefId = 1; // Replace with actual chef ID

            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                string query = "SELECT MenuItemId, MenuItemName, IngredientName, RequiredQuantity FROM IngredientRequirements WHERE ChefId = @ChefId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ChefId", chefId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable ingredientsTable = new DataTable();
                        ingredientsTable.Load(reader);
                        ingredientsDataGridView.DataSource = ingredientsTable;

                        // Add a column for chefs to enter the required quantity
                        ingredientsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            Name = "EnteredQuantity",
                            HeaderText = "Quantity Required",
                            Width = 150
                        });
                    }
                }
            }
        }

        private void SubmitIngredientRequirements(DataGridView ingredientsDataGridView)
        {
            foreach (DataGridViewRow row in ingredientsDataGridView.Rows)
            {
                if (row.Cells["EnteredQuantity"].Value != null)
                {
                    int menuItemId = Convert.ToInt32(row.Cells["MenuItemId"].Value);
                    int enteredQuantity = Convert.ToInt32(row.Cells["EnteredQuantity"].Value);

                    // Update the ingredient requirements in the database
                    UpdateIngredientRequirement(menuItemId, enteredQuantity);
                }
            }

            MessageBox.Show("Ingredient requirements submitted successfully.");
        }

        private void UpdateIngredientRequirement(int menuItemId, int quantity)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                string query = "UPDATE IngredientRequirements SET RequiredQuantity = @Quantity WHERE MenuItemId = @MenuItemId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@MenuItemId", menuItemId);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void RequestGroceries(DataGridView ingredientsDataGridView)
        {
            // Logic to request groceries based on the entered quantities
            foreach (DataGridViewRow row in ingredientsDataGridView.Rows)
            {
                if (row.Cells["EnteredQuantity"].Value != null)
                {
                    int enteredQuantity = Convert.ToInt32(row.Cells["EnteredQuantity"].Value);
                    string ingredientName = row.Cells["IngredientName"].Value.ToString();

                    // Logic to send a request for groceries
                    ViewGroceryRequirements(ingredientName, enteredQuantity);
                }
            }

            MessageBox.Show("Grocery requests have been sent successfully.");
        }

        private void ViewGroceryRequirements(string ingredientName, int enteredQuantity)
        {
            // Logic to handle the grocery request
            // For example, you could log this request to a database or send an email

            // Example: Log to console (replace with your actual logic)
            Console.WriteLine($"Requesting {enteredQuantity} of {ingredientName}");

            // If you want to log this to a database, you can implement that logic here
        }

        private void LoadReviewsAndFeedback()
        {
            lblTitle.Text = "Customer Reviews and Feedback";

            // Clear existing controls in the main content panel
            mainContentPanel.Controls.Clear();
            mainContentPanel.Controls.Add(lblTitle);

            // Create a DataGridView to display reviews
            DataGridView reviewsDataGridView = new DataGridView
            {
                Location = new Point(20, 70),
                Size = new Size(920, 400),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            mainContentPanel.Controls.Add(reviewsDataGridView);

            // Load reviews data from the database
            LoadReviewsData(reviewsDataGridView);

            // Create a button to respond to reviews
            Button btnRespond = new Button { Text = "Respond to Review", Location = new Point(20, 490), Width = 150 };
            btnRespond.Click += (s, e) => RespondToReview(reviewsDataGridView);
            mainContentPanel.Controls.Add(btnRespond);
        }

        private void LoadReviewsData(DataGridView reviewsDataGridView)
        {
            int chefId = 1; // Replace with actual chef ID

            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                string query = "SELECT ReviewId, ReviewerName, ReviewText, Rating, Date FROM Reviews WHERE ChefId = @ChefId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ChefId", chefId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable reviewsTable = new DataTable();
                        reviewsTable.Load(reader);
                        reviewsDataGridView.DataSource = reviewsTable;
                    }
                }
            }
        }

        private void RespondToReview(DataGridView reviewsDataGridView)
        {
            if (reviewsDataGridView.SelectedRows.Count > 0)
            {
                int reviewId = Convert.ToInt32(reviewsDataGridView.SelectedRows[0].Cells["ReviewId"].Value);
                string response = PromptForResponse();

                if (!string.IsNullOrEmpty(response))
                {
                    SaveResponse(reviewId, response);
                    MessageBox.Show("Response saved successfully!");
                }
            }
            else
            {
                MessageBox.Show("Please select a review to respond to.");
            }
        }

        private string PromptForResponse()
        {
            using (Form responseForm = new Form())
            {
                TextBox txtResponse = new TextBox { Multiline = true, Width = 400, Height = 100 };
                Button btnSubmit = new Button { Text = "Submit", DialogResult = DialogResult.OK };
                responseForm.Controls.Add(txtResponse);
                responseForm.Controls.Add(btnSubmit);
                responseForm.AcceptButton = btnSubmit;

                return responseForm.ShowDialog() == DialogResult.OK ? txtResponse.Text : null;
            }
        }

        private void SaveResponse(int reviewId, string response)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen \Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                string query = "INSERT INTO ReviewResponses (ReviewId, ResponseText, ResponseDate) VALUES (@ReviewId, @ResponseText, GETDATE())";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReviewId", reviewId);
                    command.Parameters.AddWithValue("@ResponseText", response);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void Logout()
        {
            MessageBox.Show("You have been logged out.");
            Form1 mainForm = new Form1();
            mainForm.Show();
            this.Hide();
        }

        private void ChefDashboard_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf;Integrated Security=True");
            cn.Open();
        }

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