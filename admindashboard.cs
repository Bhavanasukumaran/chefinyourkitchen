using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Chef_in_your_kitchen
{
    public partial class AdminDashboard : Form
    {
        private SqlConnection cn;
        private Panel sideMenuPanel;
        private Panel mainContentPanel;
        private Panel actionPanel;
        private Label lblTitle;
        private DataGridView dataGridView;
        private string currentTableName;

        public AdminDashboard()
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
            sideMenuPanel = new Panel
            {
                BackColor = Color.FromArgb(51, 51, 76),
                Dock = DockStyle.Left,
                Width = 220
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
                Text = "Admin Dashboard",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter
            };
            sideMenuPanel.Controls.Add(dashboardLabel);

            Button logoutButton = new Button
            {
                Text = "Logout",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(255, 69, 0),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Bottom,
                Height = 45
            };
            logoutButton.FlatAppearance.BorderSize = 0;
            logoutButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 99, 71);
            logoutButton.Click += (s, e) => Logout();
            sideMenuPanel.Controls.Add(logoutButton);

            Button backButton = new Button
            {
                Text = "Back",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(51, 51, 76),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Top,
                Height = 45
            };
            backButton.FlatAppearance.BorderSize = 0;
            backButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(75, 75, 112);
            backButton.Click += (s, e) => GoBackToForm1();
            sideMenuPanel.Controls.Add(backButton);

            string[] menuItems = { "Users Management", "Chefs Management", "Organizations", "Markets", "Delivery Partners" };
            foreach (string menuItem in menuItems)
            {
                Button button = new Button
                {
                    Text = menuItem,
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(51, 51, 76),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10),
                    Dock = DockStyle.Top,
                    Height = 45
                };
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(75, 75, 112);
                button.Click += (s, e) => LoadSection(menuItem);
                sideMenuPanel.Controls.Add(button);
            }
        }

        private void InitializeMainContent()
        {
            lblTitle = new Label
            {
                Text = "Dashboard Overview",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            mainContentPanel.Controls.Add(lblTitle);

            Panel dataGridPanel = new Panel
            {
                Location = new Point(20, lblTitle.Bottom + 10),
                Size = new Size(mainContentPanel.Width - 40, mainContentPanel.Height - lblTitle.Bottom - 70),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            mainContentPanel.Controls.Add(dataGridPanel);

            dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
            dataGridView.CellDoubleClick += DataGridView_CellDoubleClick; // Event for row double-click
            dataGridPanel.Controls.Add(dataGridView);

            actionPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.White
            };

            string[] actionButtons = { "Add New", "Edit", "Delete" };
            int xPosition = 20;

            foreach (string action in actionButtons)
            {
                Button actionButton = new Button
                {
                    Text = action,
                    Location = new Point(xPosition, 15),
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

        private void LoadSection(string section)
        {
            switch (section)
            {
                case "Users Management":
                    currentTableName = "UserTable";
                    break;
                case "Chefs Management":
                    currentTableName = "ChefTable";
                    break;
                case "Organizations":
                    currentTableName = "OrgTable";
                    break;
                case "Markets":
                    currentTableName = "MarketTable";
                    break;
                case "Delivery Partners":
                    currentTableName = "DelpTable";
                    break;
                default:
                    currentTableName = "";
                    break;
            }

            lblTitle.Text = $"{section}";
            LoadData(currentTableName);
        }

        private void LoadData(string tableName)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf"";Integrated Security=True"))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {tableName}", connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView.DataSource = dataTable;
            }
        }

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                OpenEditForm(row);
            }
        }

        private void OpenEditForm(DataGridViewRow row)
        {
            using (Form editForm = new Form())
            {
                editForm.Text = row == null ? "Add New Record" : $"Edit Record in {currentTableName}";
                editForm.Size = new Size(400, 520);
                editForm.StartPosition = FormStartPosition.CenterScreen;

                // Get column names
                string[] columnNames = GetColumnNames(currentTableName);
                if (columnNames.Length == 0)
                {
                    MessageBox.Show("No columns found for the selected table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit if no columns are found
                }

                TextBox[] textBoxes = new TextBox[columnNames.Length]; // Create text boxes based on column count
                int yPosition = 40;

                for (int i = 0; i < columnNames.Length; i++)
                {
                    Label label = new Label
                    {
                        Text = columnNames[i] + ":",
                        Location = new Point(20, yPosition - 20),
                        AutoSize = true
                    };
                    editForm.Controls.Add(label);

                    textBoxes[i] = new TextBox
                    {
                        Text = row != null ? row.Cells[i].Value.ToString() : string.Empty, // If adding, leave empty
                        Location = new Point(20, yPosition),
                        Width = 340
                    };
                    editForm.Controls.Add(textBoxes[i]);
                    yPosition += 50;
                }

                Button saveButton = new Button
                {
                    Text = "Save",
                    Location = new Point(20, yPosition + 10),
                    Size = new Size(100, 35),
                    BackColor = Color.FromArgb(51, 51, 76),
                    ForeColor = Color.White
                };
                saveButton.Click += (s, e) =>
                {
                    if (row == null) // Adding new record
                    {
                        AddNewRecord(textBoxes);
                    }
                    else // Editing existing record
                    {
                        UpdateRecord(row, textBoxes);
                    }
                    editForm.Close();
                };
                editForm.Controls.Add(saveButton);

                editForm.ShowDialog();
            }
        }

        private string[] GetColumnNames(string tableName)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf"";Integrated Security=True"))
            {
                connection.Open();
                DataTable schemaTable = connection.GetSchema("Columns", new string[] { null, null, tableName, null });
                string[] columnNames = new string[schemaTable.Rows.Count];

                for (int i = 0; i < schemaTable.Rows.Count; i++)
                {
                    columnNames[i] = schemaTable.Rows[i]["COLUMN_NAME"].ToString();
                }

                return columnNames;
            }
        }


        private void UpdateRecord(DataGridViewRow row, TextBox[] textBoxes)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf"";Integrated Security=True"))
            {
                connection.Open();
                string updateQuery = $"UPDATE {currentTableName} SET ";

                for (int i = 1; i < textBoxes.Length; i++)
                {
                    updateQuery += $"{dataGridView.Columns[i].HeaderText} = @value{i}";
                    if (i < textBoxes.Length - 1)
                        updateQuery += ", ";
                }
                updateQuery += $" WHERE {dataGridView.Columns[0].HeaderText} = @primaryKey";

                SqlCommand command = new SqlCommand(updateQuery, connection);

                for (int i = 1; i < textBoxes.Length; i++)
                {
                    command.Parameters.AddWithValue($"@value{i}", textBoxes[i].Text);
                }
                command.Parameters.AddWithValue("@primaryKey", row.Cells[0].Value);

                command.ExecuteNonQuery();
                LoadData(currentTableName);
            }
        }

        private void HandleAction(string action)
        {
            if (action == "Add New")
            {
                OpenEditForm(null); // Pass null to indicate a new record
            }
            else if (action == "Edit")
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dataGridView.SelectedRows[0];
                    OpenEditForm(row); // Pass the selected row for editing
                }
            }
            else if (action == "Delete")
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DeleteSelectedRow();
                }
            }
            else if (action == "Refresh")
            {
                LoadData(currentTableName);
            }
        }

        private void DeleteSelectedRow()
        {
            DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
            var primaryKeyValue = selectedRow.Cells[0].Value;

            var confirmResult = MessageBox.Show(
                "Are you sure you want to delete this record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmResult == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf"";Integrated Security=True"))
                {
                    connection.Open();
                    string deleteQuery = $"DELETE FROM {currentTableName} WHERE {dataGridView.Columns[0].HeaderText} = @primaryKeyValue";
                    SqlCommand command = new SqlCommand(deleteQuery, connection);
                    command.Parameters.AddWithValue("@primaryKeyValue", primaryKeyValue);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(currentTableName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Logout()
        {
            MessageBox.Show("Logged out successfully.");
            this.Close();
            Form1 mainForm = new Form1();
            mainForm.Show();
        }

        private void GoBackToForm1()
        {
            Form1 mainForm = new Form1();
            mainForm.Show();
            this.Close();
        }
        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            // Code to execute when the AdminDashboard is loaded
            // For example, you can load data or set initial states for UI components
        }
        private void AddNewRecord(TextBox[] textBoxes)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Bhavana Sukumaran\source\repos\Chef_in_your_kitchen\Chef_in_your_kitchen\Database1.mdf"";Integrated Security=True"))
            {
                try
                {
                    connection.Open();
                    string insertQuery = $"INSERT INTO {currentTableName} (";

                    // Build the column names part of the query
                    for (int i = 0; i < textBoxes.Length; i++)
                    {
                        insertQuery += $"{dataGridView.Columns[i].HeaderText}";
                        if (i < textBoxes.Length - 1)
                            insertQuery += ", ";
                    }
                    insertQuery += ") VALUES (";

                    // Build the values part of the query
                    for (int i = 0; i < textBoxes.Length; i++)
                    {
                        insertQuery += $"@value{i}";
                        if (i < textBoxes.Length - 1)
                            insertQuery += ", ";
                    }
                    insertQuery += ")";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Add parameters for each text box
                        for (int i = 0; i < textBoxes.Length; i++)
                        {
                            // Optional: Validate input before adding to parameters
                            if (string.IsNullOrWhiteSpace(textBoxes[i].Text))
                            {
                                MessageBox.Show($"Please fill in the value for {dataGridView.Columns[i].HeaderText}.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return; // Exit if validation fails
                            }
                            command.Parameters.AddWithValue($"@value{i}", textBoxes[i].Text);
                        }

                        // Execute the command
                        command.ExecuteNonQuery();
                        LoadData(currentTableName);
                        MessageBox.Show("Record added successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}