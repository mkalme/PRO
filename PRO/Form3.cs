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

namespace PRO
{
    public partial class Form3 : Form
    {
        public static Form2 form2;
        public static String type = "new";
        public static String filePath = "";

        private static String[] original = new String[12];
        private static String[] current = new String[12];
        private static String[] fileNames = { "name-----", "website--", "password-", "notes----", "entry1---", "entry1Pas-", "entry2---", "entry2Pas-", "labelname", "labelnam1", "enable---", "email----"};

        public Form3()
        {
            InitializeComponent();

            timer1.Interval = 20;
            timer1.Start();

            if (!type.Equals("new"))
            {
                load();
            }

            Form4.form3 = this;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            bool ifExists = form2.checkIfExists(nameField.Text);
            if (!string.IsNullOrEmpty(nameField.Text.Trim())) {
                if (!type.Equals("new"))
                {
                    saveCurrent();

                    if (checkIfDifferent())
                    {
                        form2.addDataGridViewFiles();

                        timer1.Stop();
                        Close();
                    }
                    else {
                        label1.Text = "No changes have been made.";
                    }
                }
                else if(!form2.checkIfExists(nameField.Text)){
                    createAllFiles();
                    form2.addDataGridViewFiles();

                    timer1.Stop();
                    Close();
                }
            }
        }

        public void load(){
            bool check = read.start(filePath, Form2.fileNames[9]) == "True" ? true : false;
            
            nameField.Text = read.start(filePath, Form2.fileNames[0]);
            websiteField.Text = read.start(filePath, Form2.fileNames[1]);
            passwordField.Text = read.start(filePath, Form2.fileNames[2]);
            noteBox.Text = read.start(filePath, Form2.fileNames[3]);
            nameField1.Text = read.start(filePath, Form2.fileNames[4]);
            passwordField1.Text = read.start(filePath, Form2.fileNames[5]);
            nameField2.Text = read.start(filePath, Form2.fileNames[6]);
            passwordField2.Text = read.start(filePath, Form2.fileNames[7]);
            labelName.Text = read.start(filePath, Form2.fileNames[11]);
            labelName1.Text = read.start(filePath, Form2.fileNames[12]);
            emailField.Text = read.start(filePath, Form2.fileNames[13]);
            enableEditing.Checked = check;

            saveOriginal();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool check = enableEditing.Checked;

            nameField.Enabled = check;
            websiteField.Enabled = check;
            passwordField.Enabled = check;
            noteBox.Enabled = check;
            nameField1.Enabled = check;
            passwordField1.Enabled = check;
            nameField2.Enabled = check;
            passwordField2.Enabled = check;
            nameField.Enabled = check;
            passwordGenField.Enabled = check;
            passwordGenButton.Enabled = check;
            maskedTextBox4.Enabled = check;
            changeName1.Enabled = check;
            changeName2.Enabled = check;
            emailField.Enabled = check;
        }

        private void passwordGenButton_Click(object sender, EventArgs e)
        {
            passwordGenField.Text = (!string.IsNullOrEmpty(maskedTextBox4.Text.Replace("_", "")) ? randomText(Int32.Parse(maskedTextBox4.Text.Replace("_", ""))) : "");
        }

        private String randomText(int length) {
            String text = "";

            Random ran = new Random();

            for (int i = 0; i < length; i++) {
                text += convert.characters[ran.Next(1, convert.characters.Length - 1)];
            }

            return text;
        }

        private void passwordLink1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(string.IsNullOrEmpty(passwordField.Text) ? " " : passwordField.Text);
        }

        private void notesLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(string.IsNullOrEmpty(noteBox.Text) ? " " : noteBox.Text);
        }

        private void passwordLink2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(string.IsNullOrEmpty(passwordField1.Text) ? " " : passwordField1.Text);
        }

        private void passwordLink3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(string.IsNullOrEmpty(passwordField2.Text) ? " " : passwordField2.Text);
        }

        private void passwordGeneratorLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(string.IsNullOrEmpty(passwordGenField.Text) ? " " : passwordGenField.Text);
        }

        private void emailLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(string.IsNullOrEmpty(emailField.Text) ? " " : emailField.Text);
        }

        private void changeName1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form4.label = "1";
            Form4 form = new Form4();
            form.ShowDialog();
        }

        private void changeName2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form4.label = "2";
            Form4 form = new Form4();
            form.ShowDialog();
        }

        public void changeLabel(String text) {
            if (Form4.label.Equals("1"))
            {
                labelName.Text = text;
            }
            else {
                labelName1.Text = text;
            }
        }

        private void saveOriginal() {
            original[0] = nameField.Text;
            original[1] = websiteField.Text;
            original[2] = passwordField.Text;
            original[3] = noteBox.Text;
            original[4] = nameField1.Text;
            original[5] = passwordField1.Text;
            original[6] = nameField2.Text;
            original[7] = passwordField2.Text;
            original[8] = labelName.Text;
            original[9] = labelName1.Text;
            original[10] = enableEditing.Checked.ToString();
            original[11] = emailField.Text;
        }

        private void saveCurrent() {
            current[0] = nameField.Text;
            current[1] = websiteField.Text;
            current[2] = passwordField.Text;
            current[3] = noteBox.Text;
            current[4] = nameField1.Text;
            current[5] = passwordField1.Text;
            current[6] = nameField2.Text;
            current[7] = passwordField2.Text;
            current[8] = labelName.Text;
            current[9] = labelName1.Text;
            current[10] = enableEditing.Checked.ToString();
            current[11] = emailField.Text;
        }

        private void createAllFiles() {
            String directoryName = convert.convertTo(nameField.Text);
            Directory.CreateDirectory(Form2.directoryNow + "/" + directoryName);

            String directory = Form2.directoryNow + "/" + directoryName + "/";

            Form2.createFile(directory + convert.convertTo(Form2.fileNames[0]), convert.convertTo(nameField.Text));
            Form2.createFile(directory + convert.convertTo(Form2.fileNames[1]), convert.convertTo(websiteField.Text));
            Form2.createFile(directory + convert.convertTo(Form2.fileNames[2]), convert.convertTo(passwordField.Text));
            Form2.createFile(directory + convert.convertTo(Form2.fileNames[3]), convert.convertTo(noteBox.Text));
            Form2.createFile(directory + convert.convertTo(Form2.fileNames[4]), convert.convertTo(nameField1.Text));
            Form2.createFile(directory + convert.convertTo(Form2.fileNames[5]), convert.convertTo(passwordField1.Text));
            Form2.createFile(directory + convert.convertTo(Form2.fileNames[6]), convert.convertTo(nameField2.Text));
            Form2.createFile(directory + convert.convertTo(Form2.fileNames[7]), convert.convertTo(passwordField2.Text));
            Form2.createFile(directory + convert.convertTo(Form2.fileNames[8]), convert.convertTo("File"));
            Form2.createFile(directory + convert.convertTo(Form2.fileNames[9]), convert.convertTo(enableEditing.Checked.ToString()));
            Form2.createFile(directory + convert.convertTo(Form2.fileNames[10]), convert.convertTo(DateTime.Now.ToString("dd.MM.yyyy  HH:mm")));
            Form2.createFile(directory + convert.convertTo(Form2.fileNames[11]), convert.convertTo(labelName.Text));
            Form2.createFile(directory + convert.convertTo(Form2.fileNames[12]), convert.convertTo(labelName1.Text));
            Form2.createFile(directory + convert.convertTo(Form2.fileNames[13]), convert.convertTo(emailField.Text));
        }

        private bool checkIfDifferent() {
            bool different = false;
            for (int i = 0; i < original.Length; i++) {
                if (!original[i].Equals(current[i])) {
                    different = true;
                    if (i > 0)
                    {
                        Form2.createFile(filePath + "/" + findFile(filePath, fileNames[i]), convert.convertTo(current[i]));
                    }
                    else {
                        String newDirectory = Directory.GetParent(filePath).FullName + "/" + convert.convertTo(nameField.Text);
                        Directory.Move(filePath, newDirectory);
                        filePath = newDirectory;
                        Form2.createFile(newDirectory + "/" + findFile(newDirectory, fileNames[i]), convert.convertTo(current[i]));
                    }
                }
            }

            return different;
        }

        public static String findFile(String fileP, String typeOfFile)
        {
            String text = "";

            string[] Files = Directory.GetFiles(fileP);

            for (int i = 0; i < Files.Length; i++)
            {
                if (convert.convertFrom(Path.GetFileName(Files[i])).Equals(typeOfFile))
                {
                    text = Path.GetFileName(Files[i]);
                    i = Files.Length;
                    break;
                }
            }

            return text;
        }
    }
}