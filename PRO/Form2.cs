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
using System.Diagnostics;
using System.Collections;

namespace PRO
{
    public partial class Form2 : Form
    {
        public static String directory = @"C:\Users\Michael\AppData\Roaming\Tools PRO\Tools PRO";
        public static String directoryNow = @"C:\Users\Michael\AppData\Roaming\Tools PRO\Tools PRO";
        public static String[] fileNames = {"name-----", "website--", "password-", "notes----", "entry1---", "entry1Pas-", "entry2---", "entry2Pas-", "type-----", "enable---", "edited--", "labelname", "labelnam1", "email----"};
        public static List<String> allFileNames = new List<string>();

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //CHECK IF DIRECTORY EXISTS
            checkDirectory();

            //PASS FORM AS PARAMETER
            Form3.form2 = this;
            Form5.form2 = this;

            //DATA GRID VIEW
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.Columns["idCol"].Visible = false;
            addDataGridViewFiles();

            timer2.Interval = 1;
            timer2.Start();
        }

        public void addDataGridViewFiles() {
            dataGridView1.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F2F2F2");
            timer1.Interval = 100;
            timer1.Start();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            allFileNames.Clear();

            string[] Files = Directory.GetDirectories(directoryNow);

            for (int i = 0; i < Files.Length; i++){
                bool isFile = checkIfDirectory(Files[i]);

                if (!isFile)
                {
                    dataGridView1.Rows.Add(Properties.Resources.fileImage, convert.convertFrom(Path.GetFileName(Files[i])), read.start(Files[i], fileNames[1]), read.start(Files[i], fileNames[2]).Length.ToString(), read.start(Files[i], fileNames[13]), read.start(Files[i], fileNames[10]), i);
                }
                else {
                    dataGridView1.Rows.Add(Properties.Resources.packageImage, convert.convertFrom(Path.GetFileName(Files[i])), "", "", read.start(Files[i], fileNames[13]), Directory.GetCreationTime(Files[i]).ToString("dd.MM.yyyy  HH:mm"), i);
                }
                allFileNames.Add(Path.GetFileName(Files[i]));
            }
        }

        public bool checkIfDirectory(String path){
            string[] files = Directory.GetFiles(path);
            
            return (files.Length > 0 ? false : true);
        }

        private void checkDirectory() {
            Directory.CreateDirectory(directory);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Form3.type = "new";
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        public static void createFile(String path, String text) {
            using (var tw = new StreamWriter(path, false))
            {
                tw.WriteLine(text);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this?\t\t\t\t\t", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Directory.Delete(directoryNow + @"\" + allFileNames[(int)dataGridView1.SelectedCells[6].Value], true);
                    addDataGridViewFiles();
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) {
                if (checkIfDirectory(directoryNow + @"\" + allFileNames[(int)dataGridView1.SelectedCells[6].Value]))
                {
                    directoryNow += @"\" + allFileNames[(int)dataGridView1.SelectedCells[6].Value];
                    backButton.Enabled = true;

                    string[] tokens = (directoryNow.Substring(directory.Length + 1, (directoryNow.Length - directory.Length - 1))).Split('\\');

                    pathLabel.Text = getCurrentPath();
                    addDataGridViewFiles();
                }
                else {
                    Form3.type = "view";
                    Form3.filePath = directoryNow + @"\" + allFileNames[(int)dataGridView1.SelectedCells[6].Value];
                    Form3 form3 = new Form3();
                    form3.ShowDialog();
                }
            }
        }

        private void addDirectoryButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(directoryNameField.Text.Trim()) && !checkIfExists(directoryNameField.Text)) {
                Directory.CreateDirectory(directoryNow + @"\" + convert.convertTo(directoryNameField.Text));
                addDataGridViewFiles();

                directoryNameField.Text = "";
            }
        }

        public bool checkIfExists(String name) {
            bool exists = false;

            for (int i = 0; i < allFileNames.Count; i++) {
                if (convert.convertFrom(allFileNames[i]).Equals(name)) {
                    exists = true;
                }
            }

            return exists;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            directoryNow = Directory.GetParent(directoryNow).FullName;
            backButton.Enabled = (directoryNow.Equals(directory) ? false : true);
            pathLabel.Text = getCurrentPath();
            addDataGridViewFiles();
        }

        private void clearClipboardLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.Clear();
        }

        private void directoryNameField_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(directoryNameField.Text.Trim()) && !checkIfExists(directoryNameField.Text))
            {
                addDirectoryButton.Enabled = true;
            }
            else {
                addDirectoryButton.Enabled = false;
            }
        }

        private String getCurrentPath() {
            String text = "";
            if (!directory.Equals(directoryNow)) {
                string[] tokens = (directoryNow.Substring(directory.Length + 1, (directoryNow.Length - directory.Length - 1))).Split('\\');
                for (int i = 0; i < tokens.Length; i++)
                {
                    text += convert.convertFrom(tokens[i]);
                    text += (text.Length == i - 1 ? "" : @" \ ");
                }
            }

            return text;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            timer1.Stop();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            addDataGridViewFiles();
        }

        private void renameLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form5 form5 = new Form5();
            form5.ShowDialog();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                renameLink.Enabled = true;
                deleteButton.Enabled = true;
            }
            else {
                renameLink.Enabled = false;
                deleteButton.Enabled = false;
            }
        }

        private void windowsExplorerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(directoryNow);
        }
    }
}
