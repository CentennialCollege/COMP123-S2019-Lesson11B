using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMP123_S2019_Lesson11B
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is the event handler for the  MainForm's FormClosing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// This is the event handler for the Exit Menu Item's Click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.aboutBox.ShowDialog();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            Program.aboutBox.ShowDialog();
        }

        private void exitToolStripButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'testDatabaseDataSet.StudentTable' table. You can move, or remove it, as needed.
            this.studentTableTableAdapter.Fill(this.testDatabaseDataSet.StudentTable);

        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Program.studentInfoForm.Show();
            this.Hide();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // configure the file dialog
            StudentSaveFileDialog.FileName = "Student.txt";
            StudentSaveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            StudentSaveFileDialog.Filter = "Text Files (*.txt)|*.txt| All Files (*.*)|*.*";

            // open file dialog - Modal Form
            var result = StudentSaveFileDialog.ShowDialog();
            if(result != DialogResult.Cancel)
            {
                // open file to write
                using (StreamWriter outputStream = new StreamWriter(
                    File.Open(StudentSaveFileDialog.FileName, FileMode.Create)))
                {
                    // write stuff to the file
                    outputStream.WriteLine(Program.student.id);
                    outputStream.WriteLine(Program.student.StudentID);
                    outputStream.WriteLine(Program.student.FirstName);
                    outputStream.WriteLine(Program.student.LastName);

                    // close the file
                    outputStream.Close();

                    // dispose of the memory
                    outputStream.Dispose();
                }

                MessageBox.Show("File Saved", "Saving...",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

           

               
        }

        private void StudentTableDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            // local scope aliases
            var rowIndex = StudentTableDataGridView.CurrentCell.RowIndex;
            var rows = StudentTableDataGridView.Rows;
            var cells = rows[rowIndex].Cells;
            var columnCount = StudentTableDataGridView.ColumnCount;

            StudentTableDataGridView.Rows[rowIndex].Selected = true;

            string outputString = string.Empty;
            for (int index = 0; index < columnCount; index++)
            {
                outputString += cells[index].Value.ToString() + " ";
            }

            SelectionLabel.Text = outputString;

            Program.student.id = int.Parse(cells[(int)StudentField.ID].Value.ToString());
            Program.student.StudentID = cells[(int)StudentField.STUDENT_ID].Value.ToString();
            Program.student.FirstName = cells[(int)StudentField.FIRST_NAME].Value.ToString();
            Program.student.LastName = cells[(int)StudentField.LAST_NAME].Value.ToString();
        }
    }
}
