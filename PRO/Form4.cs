using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRO
{
    public partial class Form4 : Form
    {
        public static Form3 form3;
        public static String label = "";

        public Form4()
        {
            InitializeComponent();

            textBox1.Text = (label.Equals("1") ? form3.labelName.Text : form3.labelName1.Text);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            form3.changeLabel(textBox1.Text);
            Close();
        }
    }
}
