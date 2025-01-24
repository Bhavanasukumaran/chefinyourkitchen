namespace Chef_in_your_kitchen
{
    partial class chefregistration
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
            this.chef_register = new System.Windows.Forms.Button();
            this.chef_clear = new System.Windows.Forms.Button();
            this.cus_user = new System.Windows.Forms.Label();
            this.txtchef_confirmpassword = new System.Windows.Forms.TextBox();
            this.chef_confirmpass = new System.Windows.Forms.Label();
            this.txtchef_password = new System.Windows.Forms.TextBox();
            this.chef_password = new System.Windows.Forms.Label();
            this.txtchef_phone = new System.Windows.Forms.TextBox();
            this.chef_phone = new System.Windows.Forms.Label();
            this.txtchef_email = new System.Windows.Forms.TextBox();
            this.chef_email = new System.Windows.Forms.Label();
            this.txtchef_address = new System.Windows.Forms.TextBox();
            this.chef_address = new System.Windows.Forms.Label();
            this.txtchef_name = new System.Windows.Forms.TextBox();
            this.chef_name = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.cususer = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cususer)).BeginInit();
            this.SuspendLayout();
            // 
            // chef_register
            // 
            this.chef_register.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chef_register.Location = new System.Drawing.Point(423, 373);
            this.chef_register.Name = "chef_register";
            this.chef_register.Size = new System.Drawing.Size(86, 31);
            this.chef_register.TabIndex = 33;
            this.chef_register.Text = "Register";
            this.chef_register.UseVisualStyleBackColor = true;
            this.chef_register.Click += new System.EventHandler(this.cus_register_Click);
            // 
            // chef_clear
            // 
            this.chef_clear.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chef_clear.Location = new System.Drawing.Point(314, 373);
            this.chef_clear.Name = "chef_clear";
            this.chef_clear.Size = new System.Drawing.Size(75, 31);
            this.chef_clear.TabIndex = 32;
            this.chef_clear.Text = "Clear";
            this.chef_clear.UseVisualStyleBackColor = true;
            this.chef_clear.Click += new System.EventHandler(this.cus_clear_Click);
            // 
            // cus_user
            // 
            this.cus_user.AutoSize = true;
            this.cus_user.Font = new System.Drawing.Font("Viner Hand ITC", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cus_user.Location = new System.Drawing.Point(238, -2);
            this.cus_user.Name = "cus_user";
            this.cus_user.Size = new System.Drawing.Size(284, 39);
            this.cus_user.TabIndex = 31;
            this.cus_user.Text = "CHEF REGISTRATION";
            this.cus_user.Click += new System.EventHandler(this.cus_user_Click);
            // 
            // txtchef_confirmpassword
            // 
            this.txtchef_confirmpassword.Location = new System.Drawing.Point(359, 259);
            this.txtchef_confirmpassword.Name = "txtchef_confirmpassword";
            this.txtchef_confirmpassword.Size = new System.Drawing.Size(202, 26);
            this.txtchef_confirmpassword.TabIndex = 30;
            this.txtchef_confirmpassword.TextChanged += new System.EventHandler(this.cust_confirmpassword_TextChanged);
            // 
            // chef_confirmpass
            // 
            this.chef_confirmpass.AutoSize = true;
            this.chef_confirmpass.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chef_confirmpass.Location = new System.Drawing.Point(147, 261);
            this.chef_confirmpass.Name = "chef_confirmpass";
            this.chef_confirmpass.Size = new System.Drawing.Size(158, 22);
            this.chef_confirmpass.TabIndex = 29;
            this.chef_confirmpass.Text = "Confirm Password";
            this.chef_confirmpass.Click += new System.EventHandler(this.cus_confirmpass_Click);
            // 
            // txtchef_password
            // 
            this.txtchef_password.Location = new System.Drawing.Point(359, 215);
            this.txtchef_password.Name = "txtchef_password";
            this.txtchef_password.PasswordChar = '*';
            this.txtchef_password.Size = new System.Drawing.Size(202, 26);
            this.txtchef_password.TabIndex = 28;
            this.txtchef_password.TextChanged += new System.EventHandler(this.cust_password_TextChanged);
            // 
            // chef_password
            // 
            this.chef_password.AutoSize = true;
            this.chef_password.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chef_password.Location = new System.Drawing.Point(218, 215);
            this.chef_password.Name = "chef_password";
            this.chef_password.Size = new System.Drawing.Size(88, 22);
            this.chef_password.TabIndex = 27;
            this.chef_password.Text = "Password";
            this.chef_password.Click += new System.EventHandler(this.cus_password_Click);
            // 
            // txtchef_phone
            // 
            this.txtchef_phone.Location = new System.Drawing.Point(359, 172);
            this.txtchef_phone.Name = "txtchef_phone";
            this.txtchef_phone.Size = new System.Drawing.Size(202, 26);
            this.txtchef_phone.TabIndex = 26;
            this.txtchef_phone.TextChanged += new System.EventHandler(this.cust_phone_TextChanged);
            // 
            // chef_phone
            // 
            this.chef_phone.AutoSize = true;
            this.chef_phone.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chef_phone.Location = new System.Drawing.Point(247, 176);
            this.chef_phone.Name = "chef_phone";
            this.chef_phone.Size = new System.Drawing.Size(58, 22);
            this.chef_phone.TabIndex = 25;
            this.chef_phone.Text = "Phone";
            this.chef_phone.Click += new System.EventHandler(this.cus_phone_Click);
            // 
            // txtchef_email
            // 
            this.txtchef_email.Location = new System.Drawing.Point(359, 129);
            this.txtchef_email.Name = "txtchef_email";
            this.txtchef_email.Size = new System.Drawing.Size(202, 26);
            this.txtchef_email.TabIndex = 24;
            this.txtchef_email.TextChanged += new System.EventHandler(this.email_TextChanged);
            // 
            // chef_email
            // 
            this.chef_email.AutoSize = true;
            this.chef_email.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chef_email.Location = new System.Drawing.Point(247, 129);
            this.chef_email.Name = "chef_email";
            this.chef_email.Size = new System.Drawing.Size(57, 22);
            this.chef_email.TabIndex = 23;
            this.chef_email.Text = "Email";
            this.chef_email.Click += new System.EventHandler(this.cust_email_Click);
            // 
            // txtchef_address
            // 
            this.txtchef_address.Location = new System.Drawing.Point(359, 88);
            this.txtchef_address.Multiline = true;
            this.txtchef_address.Name = "txtchef_address";
            this.txtchef_address.Size = new System.Drawing.Size(202, 24);
            this.txtchef_address.TabIndex = 22;
            this.txtchef_address.TextChanged += new System.EventHandler(this.cust_address_TextChanged);
            // 
            // chef_address
            // 
            this.chef_address.AutoSize = true;
            this.chef_address.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chef_address.Location = new System.Drawing.Point(228, 88);
            this.chef_address.Name = "chef_address";
            this.chef_address.Size = new System.Drawing.Size(76, 22);
            this.chef_address.TabIndex = 21;
            this.chef_address.Text = "Address";
            this.chef_address.Click += new System.EventHandler(this.cus_address_Click);
            // 
            // txtchef_name
            // 
            this.txtchef_name.Location = new System.Drawing.Point(359, 50);
            this.txtchef_name.Name = "txtchef_name";
            this.txtchef_name.Size = new System.Drawing.Size(202, 26);
            this.txtchef_name.TabIndex = 20;
            this.txtchef_name.TextChanged += new System.EventHandler(this.cust_name_TextChanged);
            // 
            // chef_name
            // 
            this.chef_name.AutoSize = true;
            this.chef_name.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chef_name.Location = new System.Drawing.Point(249, 54);
            this.chef_name.Name = "chef_name";
            this.chef_name.Size = new System.Drawing.Size(56, 22);
            this.chef_name.TabIndex = 19;
            this.chef_name.Text = "Name";
            this.chef_name.Click += new System.EventHandler(this.cus_name_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(225, 307);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 22);
            this.label1.TabIndex = 34;
            this.label1.Text = "Category";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Cook",
            "Chef"});
            this.comboBox1.Location = new System.Drawing.Point(359, 307);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(202, 30);
            this.comboBox1.TabIndex = 35;
            this.comboBox1.Text = "Choose....";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Chef_in_your_kitchen.Properties.Resources.chef_in_your_Kitchen;
            this.pictureBox2.Location = new System.Drawing.Point(20, 31);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(110, 95);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 18;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // cususer
            // 
            this.cususer.Image = global::Chef_in_your_kitchen.Properties.Resources.kitchen;
            this.cususer.Location = new System.Drawing.Point(-5, -9);
            this.cususer.Name = "cususer";
            this.cususer.Size = new System.Drawing.Size(810, 483);
            this.cususer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cususer.TabIndex = 17;
            this.cususer.TabStop = false;
            this.cususer.Click += new System.EventHandler(this.cususer_Click);
            // 
            // chefregistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chef_register);
            this.Controls.Add(this.chef_clear);
            this.Controls.Add(this.cus_user);
            this.Controls.Add(this.txtchef_confirmpassword);
            this.Controls.Add(this.chef_confirmpass);
            this.Controls.Add(this.txtchef_password);
            this.Controls.Add(this.chef_password);
            this.Controls.Add(this.txtchef_phone);
            this.Controls.Add(this.chef_phone);
            this.Controls.Add(this.txtchef_email);
            this.Controls.Add(this.chef_email);
            this.Controls.Add(this.txtchef_address);
            this.Controls.Add(this.chef_address);
            this.Controls.Add(this.txtchef_name);
            this.Controls.Add(this.chef_name);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.cususer);
            this.Name = "chefregistration";
            this.Text = "chefregistration";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cususer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button chef_register;
        private System.Windows.Forms.Button chef_clear;
        private System.Windows.Forms.Label cus_user;
        private System.Windows.Forms.TextBox txtchef_confirmpassword;
        private System.Windows.Forms.Label chef_confirmpass;
        private System.Windows.Forms.TextBox txtchef_password;
        private System.Windows.Forms.Label chef_password;
        private System.Windows.Forms.TextBox txtchef_phone;
        private System.Windows.Forms.Label chef_phone;
        private System.Windows.Forms.TextBox txtchef_email;
        private System.Windows.Forms.Label chef_email;
        private System.Windows.Forms.TextBox txtchef_address;
        private System.Windows.Forms.Label chef_address;
        private System.Windows.Forms.TextBox txtchef_name;
        private System.Windows.Forms.Label chef_name;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox cususer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}