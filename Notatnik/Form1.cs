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

namespace Notatnik
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ZamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static string[] ReadTextFile(string fileName)
        {
            //var tekst = new List<string>();

            //try {
            //    using (StreamReader reader = new StreamReader(fileName))
            //    {
            //        string row;
            //        while ((row = reader.ReadLine()) != null)
            //        {
            //            tekst.Add(row);
            //        }

            //        return tekst.ToArray();
            //    }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show($"Błąd odczytu pliku {fileName}");
            //    return new string[0];
            //}

            return File.ReadAllLines(fileName);
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Zapisać zmiany w dokumencie?",
                this.Text,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button3);

            switch (result)
            {
                case DialogResult.Yes:
                    throw new NotImplementedException();
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.Cancel:
                case DialogResult.None:
                default:
                    e.Cancel = true;
                    break;
            }
        }

        private void PasekStanuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip1.Visible = pasekStanuToolStripMenuItem.Checked;
        }

        private void OwtórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = "txt";
            fileDialog.Title = "Wczytaj plik";
            fileDialog.InitialDirectory = ".";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileName = fileDialog.FileName;
                textBox1.Lines = ReadTextFile(fileName);

                int lastSlash = fileName.LastIndexOf('\\');
                toolStripStatusLabel1.Text = fileName.Substring(lastSlash + 1);
            }
        }
    }
}