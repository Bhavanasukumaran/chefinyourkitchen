namespace Chef_in_your_kitchen
{
    partial class User_registration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cususer = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.cus_name = new System.Windows.Forms.Label();
            this.cust_name = new System.Windows.Forms.TextBox();
            this.cus_address = new System.Windows.Forms.Label();
            this.cust_address = new System.Windows.Forms.TextBox();
            this.cust_email = new System.Windows.Forms.Label();
            this.email = new System.Windows.Forms.TextBox();
            this.cus_phone = new System.Windows.Forms.Label();
            this.cust_phone = new System.Windows.Forms.TextBox();
            this.cus_password = new System.Windows.Forms.Label();
            this.cust_password = new System.Windows.Forms.TextBox();
            this.cus_confirmpass = new System.Windows.Forms.Label();
            this.cust_confirmpassword = new System.Windows.Forms.TextBox();
            this.cus_user = new System.Windows.Forms.Label();
            this.cus_clear = new System.Windows.Forms.Button();
            this.cus_register = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cususer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // cususer
            // 
            this.cususer.Image = global::Chef_in_your_kitchen.Properties.Resources.kitchen;
            this.cususer.Location = new System.Drawing.Point(-3, -10);
            this.cususer.Name = "cususer";
            this.cususer.Size = new System.Drawing.Size(810, 498);
            this.cususer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cususer.TabIndex = 0;
            this.cususer.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Chef_in_your_kitchen.Properties.Resources.chef_in_your_Kitchen;
            this.pictureBox2.Location = new System.Drawing.Point(22, 30);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(110, 110);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // cus_name
            // 
            this.cus_name.AutoSize = true;
            this.cus_name.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cus_name.Location = new System.Drawing.Point(251, 68);
            this.cus_name.Name = "cus_name";
            this.cus_name.Size = new System.Drawing.Size(56, 22);
            this.cus_name.TabIndex = 2;
            this.cus_name.Text = "Name";
            this.cus_name.Click += new System.EventHandler(this.label1_Click);
            // 
            // cust_name
            // 
            this.cust_name.Location = new System.Drawing.Point(361, 68);
            this.cust_name.Name = "cust_name";
            this.cust_name.Size = new System.Drawing.Size(202, 26);
            this.cust_name.TabIndex = 3;
            this.cust_name.TextChanged += new System.EventHandler(this.cust_name_TextChanged);
            // 
            // cus_address
            // 
            this.cus_address.AutoSize = true;
            this.cus_address.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cus_address.Location = new System.Drawing.Point(231, 113);
            this.cus_address.Name = "cus_address";
            this.cus_address.Size = new System.Drawing.Size(76, 22);
            this.cus_address.TabIndex = 4;
            this.cus_address.Text = "Address";
            this.cus_address.Click += new System.EventHandler(this.cus_address_Click);
            // 
            // cust_address
            // 
            this.cust_address.Location = new System.Drawing.Point(361, 111);
            this.cust_address.Multiline = true;
            this.cust_address.Name = "cust_address";
            this.cust_address.Size = new System.Drawing.Size(202, 26);
            this.cust_address.TabIndex = 5;
            this.cust_address.TextChanged += new System.EventHandler(this.cust_address_TextChanged);
            // 
            // cust_email
            // 
            this.cust_email.AutoSize = true;
            this.cust_email.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cust_email.Location = new System.Drawing.Point(250, 163);
            this.cust_email.Name = "cust_email";
            this.cust_email.Size = new System.Drawing.Size(57, 22);
            this.cust_email.TabIndex = 6;
            this.cust_email.Text = "Email";
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(361, 162);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(202, 26);
            this.email.TabIndex = 7;
            this.email.TextChanged += new System.EventHandler(this.email_TextChanged);
            // 
            // cus_phone
            // 
            this.cus_phone.AutoSize = true;
            this.cus_phone.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cus_phone.Location = new System.Drawing.Point(249, 215);
            this.cus_phone.Name = "cus_phone";
            this.cus_phone.Size = new System.Drawing.Size(58, 22);
            this.cus_phone.TabIndex = 8;
            this.cus_phone.Text = "Phone";
            // 
            // cust_phone
            // 
            this.cust_phone.Location = new System.Drawing.Point(361, 212);
            this.cust_phone.Name = "cust_phone";
            this.cust_phone.Size = new System.Drawing.Size(202, 26);
            this.cust_phone.TabIndex = 9;
            this.cust_phone.TextChanged += new System.EventHandler(this.cust_phone_TextChanged);
            // 
            // cus_password
            // 
            this.cus_password.AutoSize = true;
            this.cus_password.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cus_password.Location = new System.Drawing.Point(219, 272);
            this.cus_password.Name = "cus_password";
            this.cus_password.Size = new System.Drawing.Size(88, 22);
            this.cus_password.TabIndex = 10;
            this.cus_password.Text = "Password";
            // 
            // cust_password
            // 
            this.cust_password.Location = new System.Drawing.Point(361, 268);
            this.cust_password.Name = "cust_password";
            this.cust_password.PasswordChar = '*';
            this.cust_password.Size = new System.Drawing.Size(202, 26);
            this.cust_password.TabIndex = 11;
            this.cust_password.TextChanged += new System.EventHandler(this.cust_password_TextChanged);
            // 
            // cus_confirmpass
            // 
            this.cus_confirmpass.AutoSize = true;
            this.cus_confirmpass.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cus_confirmpass.Location = new System.Drawing.Point(149, 322);
            this.cus_confirmpass.Name = "cus_confirmpass";
            this.cus_confirmpass.Size = new System.Drawing.Size(158, 22);
            this.cus_confirmpass.TabIndex = 12;
            this.cus_confirmpass.Text = "Confirm Password";
            // 
            // cust_confirmpassword
            // 
            this.cust_confirmpassword.Location = new System.Drawing.Point(361, 320);
            this.cust_confirmpassword.Name = "cust_confirmpassword";
            this.cust_confirmpassword.PasswordChar = '*';
            this.cust_confirmpassword.Size = new System.Drawing.Size(202, 26);
            this.cust_confirmpassword.TabIndex = 13;
            this.cust_confirmpassword.TextChanged += new System.EventHandler(this.cust_confirmpassword_TextChanged);
            // 
            // cus_user
            // 
            this.cus_user.AutoSize = true;
            this.cus_user.Font = new System.Drawing.Font("Viner Hand ITC", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cus_user.Location = new System.Drawing.Point(240, 12);
            this.cus_user.Name = "cus_user";
            this.cus_user.Size = new System.Drawing.Size(284, 39);
            this.cus_user.TabIndex = 14;
            this.cus_user.Text = "USER REGISTRATION";
            // 
            // cus_clear
            // 
            this.cus_clear.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cus_clear.Location = new System.Drawing.Point(316, 372);
            this.cus_clear.Name = "cus_clear";
            this.cus_clear.Size = new System.Drawing.Size(75, 34);
            this.cus_clear.TabIndex = 15;
            this.cus_clear.Text = "Clear";
            this.cus_clear.UseVisualStyleBackColor = true;
            this.cus_clear.Click += new System.EventHandler(this.cus_clear_Click);
            // 
            // cus_register
            // 
            this.cus_register.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cus_register.Location = new System.Drawing.Point(425, 372);
            this.cus_register.Name = "cus_register";
            this.cus_register.Size = new System.Drawing.Size(86, 34);
            this.cus_register.TabIndex = 16;
            this.cus_register.Text = "Register";
            this.cus_register.UseVisualStyleBackColor = true;
            this.cus_register.Click += new System.EventHandler(this.cus_register_Click);
            // 
            // User_registration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cus_register);
            this.Controls.Add(this.cus_clear);
            this.Controls.Add(this.cus_user);
            this.Controls.Add(this.cust_confirmpassword);
            this.Controls.Add(this.cus_confirmpass);
            this.Controls.Add(this.cust_password);
            this.Controls.Add(this.cus_password);
            this.Controls.Add(this.cust_phone);
            this.Controls.Add(this.cus_phone);
            this.Controls.Add(this.email);
            this.Controls.Add(this.cust_email);
            this.Controls.Add(this.cust_address);
            this.Controls.Add(this.cus_address);
            this.Controls.Add(this.cust_name);
            this.Controls.Add(this.cus_name);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.cususer);
            this.Name = "User_registration";
            this.Load += new System.EventHandler(this.User_registration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cususer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox cususer;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label cus_name;
        private System.Windows.Forms.TextBox cust_name;
        private System.Windows.Forms.Label cus_address;
        private System.Windows.Forms.TextBox cust_address;
        private System.Windows.Forms.Label cust_email;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.Label cus_phone;
        private System.Windows.Forms.TextBox cust_phone;
        private System.Windows.Forms.Label cus_password;
        private System.Windows.Forms.TextBox cust_password;
        private System.Windows.Forms.Label cus_confirmpass;
        private System.Windows.Forms.TextBox cust_confirmpassword;
        private System.Windows.Forms.Label cus_user;
        private System.Windows.Forms.Button cus_clear;
        private System.Windows.Forms.Button cus_register;
    }
}