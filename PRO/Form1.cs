using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace PRO
{
    public partial class Form1 : Form
    {
        String labelText = "GW3 MV7 93V CN7 WV7 4HC JF4 \nKF3 TE8 476 K38 3DK FKH WQ5";

        public Form1()
        {
            InitializeComponent();

            label1.Text = labelText;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            checkPAS(passwordField.Text);
        }

        private void checkPAS(String text){
            String[] tokens = labelText.Replace("\n", "").Split(' ');
            String password = "";

            for (int i = 0; i < 7; i++) {
                password += tokens[i] += (i == 6 ? "" : " ");
            }

            if (password.Equals(text)) {
                this.Hide();
                Form2 from2 = new Form2();
                from2.ShowDialog();
                this.Close();
            }
        }

        private void passwordField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.CapsLock)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
