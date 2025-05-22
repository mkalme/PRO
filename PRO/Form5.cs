using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PRO
{
    public partial class Form5 : Form
    {
        public static Form2 form2;

        public Form5()
        {
            InitializeComponent();
            textBox1.Text = form2.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            timer1.Interval = 1;
            timer1.Start();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            String filePath = Form2.directoryNow + @"\" + Form2.allFileNames[(int)form2.dataGridView1.SelectedCells[6].Value];
            if (form2.checkIfDirectory(filePath))
            {
                String newDirectory = Directory.GetParent(filePath).FullName + "/" + convert.convertTo(textBox1.Text);
                Directory.Move(filePath, newDirectory);

                form2.addDataGridViewFiles();
                timer1.Stop();
                Close();
            }
            else
            {
                String newDirectory = Directory.GetParent(filePath).FullName + "/" + convert.convertTo(textBox1.Text);
                Directory.Move(filePath, newDirectory);
                Form2.createFile(newDirectory + "/" + Form3.findFile(newDirectory, Form2.fileNames[0]), convert.convertTo(textBox1.Text));

                form2.addDataGridViewFiles();
                timer1.Stop();
                Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text.Trim()) && !textBox1.Text.Equals(form2.dataGridView1.CurrentRow.Cells[1].Value.ToString()) && !form2.checkIfExists(textBox1.Text))
            {
                doneButton.Enabled = true;
            }
            else {
                doneButton.Enabled = false;
            }
        }
    }
}
