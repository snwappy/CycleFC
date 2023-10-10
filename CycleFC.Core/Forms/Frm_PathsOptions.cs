using System;
using System.Windows.Forms;

namespace MyNes
{
    public partial class Frm_PathsOptions : Form
    {
        public Frm_PathsOptions()
        {
            InitializeComponent();

            textBoxImages.Text = Program.Settings.ImagesFolder;
            textBoxStates.Text = Program.Settings.StatesFolder;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.Settings.ImagesFolder = textBoxImages.Text;
            Program.Settings.StatesFolder = textBoxStates.Text;
            Program.Settings.Save();
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBoxImages.Text = ".\\images\\";
            textBoxStates.Text = ".\\states\\";
        }
        private void buttonImages_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fol = new FolderBrowserDialog();
            fol.Description = "Snapshots folder";
            fol.ShowNewFolderButton = true;
            fol.SelectedPath = Application.StartupPath;
            if (fol.ShowDialog(this) == DialogResult.OK)
            {
                textBoxImages.Text = fol.SelectedPath;
            }
        }
        private void buttonStates_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fol = new FolderBrowserDialog();
            fol.Description = "State saves folder";
            fol.ShowNewFolderButton = true;
            fol.SelectedPath = Application.StartupPath;
            if (fol.ShowDialog(this) == DialogResult.OK)
            {
                textBoxStates.Text = fol.SelectedPath;
            }
        }
    }
}