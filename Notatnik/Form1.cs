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
        private string _fileName;
        private StringReader stringReader = null;

        private string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                int lastSlash = FileName.LastIndexOf('\\');
                toolStripStatusLabel1.Text = FileName.Substring(lastSlash + 1);
            }
        }

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
                    SaveTextToFile();
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
                FileName = fileDialog.FileName;
                
                textBox1.Lines = ReadTextFile(FileName);
            }
        }

        private void SaveTextToFile()
        {
            if (FileName?.Length > 0)
                saveFileDialog1.FileName = FileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileName = saveFileDialog1.FileName;
                File.WriteAllLines(FileName, textBox1.Lines);
            }
        }

        private void ZapiszJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveTextToFile();
        }

        private void cofnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void wytnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }

        private void kopiujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
        }

        private void wklejToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "";
        }

        private void zaznaczWszystkoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            var font = textBox1.Font;
            var rowHeight = (int)font.GetHeight(e.Graphics);
            var lineCount = e.MarginBounds.Height / rowHeight;

            if (stringReader == null)
                stringReader = new StringReader(textBox1.Text);

            e.HasMorePages = true;

            for (int i = 0; i < lineCount; i++)
            {
                var row = stringReader.ReadLine();
                if (row == null)
                {
                    e.HasMorePages = false;
                    stringReader = null;
                    break;
                }

                e.Graphics.DrawString(row, font, Brushes.Black,
                    e.MarginBounds.Left, e.MarginBounds.Top + i * rowHeight);
            }

            
        }

        private void drukujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.DocumentName = toolStripStatusLabel1.Text;
                printDocument1.Print();
            }
        }

        private void ustawienieWydrukuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }

        private void podgladWydrukuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }
    }
}