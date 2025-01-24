using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chef_in_your_kitchen
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            User_registration userregistration = new User_registration();
            userregistration.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            chefregistration chefregistration = new chefregistration();
            chefregistration.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            organizationregistration organizationregistration = new organizationregistration();
            organizationregistration.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide() ;
            deliverypregistration deliverypregistration = new deliverypregistration();
            deliverypregistration.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
