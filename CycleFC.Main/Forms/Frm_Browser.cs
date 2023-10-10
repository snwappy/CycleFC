/*********************************************************************\r
*This file is part of CycleFC                                         *               
*                                                                     *
*Original code © Ala Hadid 2009 - 2011                                *
*Code maintainance by Snappy Pupper (@snappypupper on Twitter)        *
*Code updated: October 2023                                           *
*                                                                     *                                                                     *
*CycleFC is a fork of the original MyNES,                             *
*which is free software: you can redistribute it and/or modify        *
*it under the terms of the GNU General Public License as published by *
*the Free Software Foundation, either version 3 of the License, or    *
*(at your option) any later version.                                  *
*                                                                     *
*You should have received a copy of the GNU General Public License    *
*along with this program.  If not, see <http://www.gnu.org/licenses/>.*
\*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

using MyNes.Nes;

namespace MyNes
{
    public partial class Frm_Browser : Form
    {
        int RatingX = 0;
        bool IsClosing = false;
        Frm_main _TheMainForm;
        TreeNode_Folder _SelectedFolder;
        public bool ShouldSaveFolders = false;
        FoldersHolder BASE = new FoldersHolder();
        List<int> SearchResultIndexes = new List<int>();
        int searchResultIndex = 0;
        CompareMode sortMode = CompareMode.None;
        bool AtoZ = false;
        bool IsScrolling = false;

        public Frm_Browser(Frm_main TheMainForm)
        {
            _TheMainForm = TheMainForm;
            InitializeComponent();
        }
        void SaveFolders()
        {
            if (BASE.FOLDERS == null)
                BASE.FOLDERS = new List<MFolder>();
            FileStream fs = new FileStream(Program.Settings.BrowserDataBasePath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, BASE);
            fs.Close();
        }
        void LoadFolders()
        {
            try
            {
                FileStream fs = new FileStream(Program.Settings.BrowserDataBasePath, FileMode.Open, FileAccess.Read);
                BinaryFormatter formatter = new BinaryFormatter();
                BASE = (FoldersHolder)formatter.Deserialize(fs);
                fs.Close();
            }
            catch
            {
                BASE = new FoldersHolder();
            }
        }
        protected override void OnShown(EventArgs e)
        {
            //Load settings
            checkBox1.Checked = Program.Settings.ShowBrowser;
            this.Location = Program.Settings.BrowserLocation;
            this.Size = Program.Settings.BrowserSize;
            splitContainer1.SplitterDistance = Program.Settings.Container1;
            splitContainer2.SplitterDistance = Program.Settings.Container2;
            columnHeader3.Width = Program.Settings.Column_Name;
            columnHeader4.Width = Program.Settings.Column_Size;
            columnHeader5.Width = Program.Settings.Column_Mapper;
            columnHeader_rating.Width = Program.Settings.Column_rating;
            if (Program.Settings.IsDetailsView)
            {
                listView1.View = View.Details;
                detailsToolStripMenuItem.Checked = true;
            }
            else
            {
                listView1.View = View.LargeIcon;
                thumbnailsToolStripMenuItem.Checked = true;
            }
            //Load folders
            LoadFolders();
            if (BASE.FOLDERS == null)
                BASE.FOLDERS = new List<MFolder>();
            RefreshFolders();
            base.OnShown(e);
        }
        void RefreshFolders()
        {
            treeView1.Nodes.Clear();
            foreach (MFolder fol in BASE.FOLDERS)
            {
                TreeNode_Folder TR = new TreeNode_Folder();
                TR.ImageIndex = 0;
                TR.SelectedImageIndex = 1;
                TR.Folder = fol;
                treeView1.Nodes.Add(TR);
            }
        }
        public void SaveSettings()
        {
            Program.Settings.ShowBrowser = checkBox1.Checked;
            Program.Settings.BrowserLocation = this.Location;
            Program.Settings.BrowserSize = this.Size;
            Program.Settings.Container1 = splitContainer1.SplitterDistance;
            Program.Settings.Container2 = splitContainer2.SplitterDistance;
            Program.Settings.Column_Name = columnHeader3.Width;
            Program.Settings.Column_Size = columnHeader4.Width;
            Program.Settings.Column_Mapper = columnHeader5.Width;
            Program.Settings.Column_rating = columnHeader_rating.Width;
        }
        void AddRootFolder()
        {
            FolderBrowserDialog fo = new FolderBrowserDialog();
            fo.Description = "Add roms folder";
            fo.ShowNewFolderButton = true;
            if (fo.ShowDialog(this) == DialogResult.OK)
            {
                MFolder folderr = new MFolder();
                folderr.Path = fo.SelectedPath;
                folderr.Name = Path.GetFileName(fo.SelectedPath);
                if (MessageBox.Show("List sub folders ?", "Add root folder", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    folderr.FindFolders();
                }
                if (MessageBox.Show("Do you want to show all files in this folder and in sub folders when selected ?", "Add root folder", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    folderr.IncludeSubFolders = true;
                }
                BASE.FOLDERS.Add(folderr);
                RefreshFolders();
                ShouldSaveFolders = true;
            }
        }
        void AddFolder()
        {
            if (treeView1.SelectedNode == null)
                return;
            FolderBrowserDialog fo = new FolderBrowserDialog();
            fo.Description = "Add roms folder";
            fo.ShowNewFolderButton = true;
            if (fo.ShowDialog(this) == DialogResult.OK)
            {
                MFolder folderr = new MFolder();
                folderr.Path = fo.SelectedPath;
                folderr.Name = Path.GetFileName(fo.SelectedPath);
                if (MessageBox.Show("List sub folders ?", "Add folder", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    folderr.FindFolders();
                }
                if (MessageBox.Show("Do you want to show all files in this folder and in sub folders when selected ?", "Add folder", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    folderr.IncludeSubFolders = true;
                }
                ((TreeNode_Folder)treeView1.SelectedNode).Folder.Folders.Add(folderr);
                ((TreeNode_Folder)treeView1.SelectedNode).FindFolders();
                treeView1.SelectedNode.Expand();
                ShouldSaveFolders = true;
            }
        }
        void BuildCache()
        {
            ShowStatusBar();
            string[] Dirs;
            if (_SelectedFolder.Folder.IncludeSubFolders)
                Dirs = Directory.GetFiles(_SelectedFolder.Folder.Path, "*", SearchOption.AllDirectories);
            else
                Dirs = Directory.GetFiles(_SelectedFolder.Folder.Path);
            SetProgressMaximum(Dirs.Length);
            int o = 0;
            foreach (string Dir in Dirs)
            {
                string EXT = Path.GetExtension(Dir);
                MFile fil = new MFile();
                NesCartridge header;
                switch (EXT.ToLower())
                {
                    case ".nes":
                        fil.Name = Path.GetFileName(Dir);
                        header = new NesCartridge(null);
                        LoadRomStatus status = header.Load(Dir, new FileStream(Dir, FileMode.Open, FileAccess.Read), null, true);
                        if (status == LoadRomStatus.LoadSuccess |
                            status == LoadRomStatus.InvalidMapper)
                        {
                            fil.SupportedMapper = header.SupportedMapper();
                            fil.Mapper = header.Mapper.ToString();
                        }
                        fil.IsBatteryBacked = header.HasSaveRam ? "Yes" : "No";
                        fil.IsPC10 = header.IsPC10 ? "Yes" : "No";
                        fil.IsTrainer = header.HasTrainer ? "Yes" : "No";
                        fil.IsVSUnisystem = header.IsVSUnisystem ? "Yes" : "No";
                        fil.Path = Dir;
                        fil.Size = Program.GetFileSize(Dir);
                        _SelectedFolder.Folder.Files.Add(fil);
                        break;
                    case ".zip":
                    case ".rar":
                    case ".7z":
                        //SevenZip.SevenZipExtractor EXTRACTOR = new SevenZip.SevenZipExtractor(Dir);
                        Directory.CreateDirectory(Path.GetTempPath() + "\\mynes\\");
                        for (int i = 0; i < Directory.GetFiles(Path.GetTempPath() + "\\mynes\\").Length; i++)
                        {
                            try
                            {
                                File.Delete(Directory.GetFiles(Path.GetTempPath() + "\\mynes\\")[i]);
                                i = -1;
                            }
                            catch { }
                        }
                        //EXTRACTOR.ExtractArchive(Path.GetTempPath() + "\\mynes\\");
                        bool foundsupported = false;
                        string mappers = "(" + Directory.GetFiles(Path.GetTempPath() + "\\mynes\\").Length.ToString() + " roms) ";
                        for (int i = 0; i < Directory.GetFiles(Path.GetTempPath() + "\\mynes\\").Length; i++)
                        {
                            if (Path.GetExtension(Directory.GetFiles(Path.GetTempPath() + "\\mynes\\")[i]).ToLower() == ".nes")
                            {
                                header = new NesCartridge(null);
                                LoadRomStatus stat = header.Load(Dir, new FileStream(Directory.GetFiles(Path.GetTempPath() + "\\mynes\\")[i], FileMode.Open, FileAccess.Read), null, true);
                                if (stat == LoadRomStatus.LoadSuccess |
                                    stat == LoadRomStatus.InvalidMapper)
                                {
                                    mappers += ", " + header.Mapper.ToString();
                                    if (header.SupportedMapper())
                                        foundsupported = true;
                                }
                            }
                        }
                        fil = new MFile();
                        if (foundsupported)
                            fil.SupportedMapper = true;
                        fil.Name = Path.GetFileName(Dir);
                        fil.Mapper = mappers;
                        fil.Path = Dir;
                        fil.IsBatteryBacked = "N/A";
                        fil.IsPC10 = "N/A";
                        fil.IsTrainer = "N/A";
                        fil.IsVSUnisystem = "N/A";
                        fil.Size = Program.GetFileSize(Dir);
                        _SelectedFolder.Folder.Files.Add(fil);
                        break;
                }
                SetProgressVal(o);
                o++;
            }
            HideStatusBar();
            ShouldSaveFolders = true;
        }
        void LoadFilesFromCache()
        {
            listView1.Visible = false;
            listView1.Items.Clear();
            foreach (MFile Fil in _SelectedFolder.Folder.Files)
            {
                ListViewItem_MFile IT = new ListViewItem_MFile();
                IT.MFile = Fil;
                if (Path.GetExtension(Fil.Path).ToLower() == ".nes")
                    IT.ImageIndex = 2;
                else
                    IT.ImageIndex = 3;
                switch (_SelectedFolder.Folder.Filter)
                {
                    case FolderFilter.All:
                        listView1.Items.Add(IT);
                        break;
                    case FolderFilter.SupportedMappersOnly:
                        if (Fil.SupportedMapper)
                        {
                            listView1.Items.Add(IT);
                        }
                        break;
                    case FolderFilter.NOTsupportedMapper:
                        if (!Fil.SupportedMapper)
                        {
                            listView1.Items.Add(IT);
                        }
                        break;
                    case FolderFilter.Mapper:
                        if (Fil.Mapper == TextBox1_mapper.Text &
                            Path.GetExtension(Fil.Path).ToLower() == ".nes")
                        {
                            listView1.Items.Add(IT);
                        }
                        break;
                }
            }
            label1_status.Text = listView1.Items.Count + " items found.";
            listView1.Visible = true;
        }

        void ShowStatusBar()
        {
            panel_Status.Visible = true;
            panel_Status.Refresh();
        }
        void SetProgressMaximum(int value)
        {
            progressBar1.Maximum = value;
        }
        void SetProgressVal(int value)
        {
            progressBar1.Value = value;
        }
        void HideStatusBar()
        {
            panel_Status.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void DeleteFolder(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            if (treeView1.SelectedNode.Parent == null)//It's a root folder
            {
                if (MessageBox.Show("Are you sure you want to delete selected folder from the list ?", "Delete folder", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    BASE.FOLDERS.RemoveAt(treeView1.SelectedNode.Index);
                    RefreshFolders();
                    ShouldSaveFolders = true;
                }
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to delete selected folder from the list ?", "Delete folder", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ((TreeNode_Folder)treeView1.SelectedNode.Parent).Folder.Folders.RemoveAt(treeView1.SelectedNode.Index);
                    ((TreeNode_Folder)treeView1.SelectedNode.Parent).FindFolders();
                    ShouldSaveFolders = true;
                }
            }
        }
        //After selecting a folder
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            listView1.Items.Clear();
            pictureBox1.Image = Properties.Resources.MyNesIcon;
            richTextBox1.Text = "";
            _SelectedFolder = (TreeNode_Folder)treeView1.SelectedNode;
            //Properties
            listView2.Items.Clear();
            listView2.Items.Add("Name");
            listView2.Items[listView2.Items.Count - 1].SubItems.Add(_SelectedFolder.Folder.Name);
            listView2.Items.Add("Path");
            listView2.Items[listView2.Items.Count - 1].SubItems.Add(_SelectedFolder.Folder.Path);
            listView2.Items.Add("Include Sub Folders");
            listView2.Items[listView2.Items.Count - 1].SubItems.Add(_SelectedFolder.Folder.IncludeSubFolders.ToString());
            listView2.Items.Add("Snapshots Path");
            listView2.Items[listView2.Items.Count - 1].SubItems.Add(_SelectedFolder.Folder.ImagesFolder);
            listView2.Items.Add("Info Texts Path");
            listView2.Items[listView2.Items.Count - 1].SubItems.Add(_SelectedFolder.Folder.InfosFolder);
            TextBox1_mapper.Text = _SelectedFolder.Folder.Mapper.ToString();
            switch (_SelectedFolder.Folder.Filter)
            {
                case FolderFilter.All: ComboBox1_nav.SelectedIndex = 0; TextBox1_mapper.Enabled = false; break;
                case FolderFilter.SupportedMappersOnly: ComboBox1_nav.SelectedIndex = 1; TextBox1_mapper.Enabled = false; break;
                case FolderFilter.NOTsupportedMapper: ComboBox1_nav.SelectedIndex = 2; TextBox1_mapper.Enabled = false; break;
                case FolderFilter.Mapper: ComboBox1_nav.SelectedIndex = 3; TextBox1_mapper.Enabled = true; break;
            }
            if (!Directory.Exists(_SelectedFolder.Folder.Path))
            {
                MessageBox.Show("This folder isn't exist on the disk !!");
                DeleteFolder(this, null);
            }
            else
            {
                if (_SelectedFolder.Folder.Files == null)
                {
                    _SelectedFolder.Folder.Files = new List<MFile>();
                    BuildCache();
                }
                else if (_SelectedFolder.Folder.Files.Count == 0)
                {
                    BuildCache();
                }
                LoadFilesFromCache();
            }
        }
        private void RenameSelected(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            treeView1.SelectedNode.BeginEdit();
        }
        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (treeView1.SelectedNode == null)
            { e.CancelEdit = true; return; }
            if (e.Label == null)
            { return; }
            TreeNode_Folder fol = (TreeNode_Folder)treeView1.SelectedNode;
            fol.Folder.Name = e.Label;
            fol.Text = e.Label;
            ShouldSaveFolders = true;
            //Properties
            listView2.Items.Clear();
            listView2.Items.Add("Name");
            listView2.Items[listView2.Items.Count - 1].SubItems.Add(((TreeNode_Folder)e.Node).Folder.Name);
            listView2.Items.Add("Path");
            listView2.Items[listView2.Items.Count - 1].SubItems.Add(((TreeNode_Folder)e.Node).Folder.Path);
            listView2.Items.Add("Snapshots Path");
            listView2.Items[listView2.Items.Count - 1].SubItems.Add(((TreeNode_Folder)e.Node).Folder.ImagesFolder);
            listView2.Items.Add("Info Texts Path");
            listView2.Items[listView2.Items.Count - 1].SubItems.Add(((TreeNode_Folder)e.Node).Folder.InfosFolder);
        }
        //Play
        private void PlayRom(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
                return;
            ListViewItem_MFile IT = (ListViewItem_MFile)listView1.SelectedItems[0];
            if (File.Exists(IT.MFile.Path))
            {
                if (ShouldSaveFolders)
                {
                    SaveFolders();
                    ShouldSaveFolders = false;
                }
                _TheMainForm.OpenRom(IT.MFile.Path, true);
                _TheMainForm.Select();
                SaveSettings();
            }
        }
        private void Frm_Browser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                PlayRom(this, null);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            PlayRom(this, null);
        }
        //snapshotes
        private void ChangeImageFolder(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            TreeNode_Folder TR = (TreeNode_Folder)treeView1.SelectedNode;
            FolderBrowserDialog Fol = new FolderBrowserDialog();
            Fol.Description = "Images folder";
            Fol.ShowNewFolderButton = true;
            Fol.SelectedPath = TR.Folder.ImagesFolder;
            ShouldSaveFolders = true;
            if (Fol.ShowDialog() == DialogResult.OK)
            {
                TR.Folder.ImagesFolder = Fol.SelectedPath;
                //Properties
                listView2.Items.Clear();
                listView2.Items.Add("Name");
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(TR.Folder.Name);
                listView2.Items.Add("Path");
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(TR.Folder.Path);
                listView2.Items.Add("Snapshots Path");
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(TR.Folder.ImagesFolder);
                listView2.Items.Add("Info Texts Path");
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(TR.Folder.InfosFolder);
            }
        }
        //text
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            TreeNode_Folder TR = (TreeNode_Folder)treeView1.SelectedNode;
            FolderBrowserDialog Fol = new FolderBrowserDialog();
            Fol.Description = "Info files folder";
            Fol.ShowNewFolderButton = true;
            Fol.SelectedPath = TR.Folder.InfosFolder;
            if (Fol.ShowDialog() == DialogResult.OK)
            {
                TR.Folder.InfosFolder = Fol.SelectedPath;
                ShouldSaveFolders = true;
                //Properties
                listView2.Items.Clear();
                listView2.Items.Add("Name");
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(TR.Folder.Name);
                listView2.Items.Add("Path");
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(TR.Folder.Path);
                listView2.Items.Add("Snapshots Path");
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(TR.Folder.ImagesFolder);
                listView2.Items.Add("Info Texts Path");
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(TR.Folder.InfosFolder);
            }
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
                return;
            if (treeView1.SelectedNode == null)
                return;
            if (_SelectedFolder == null)
                return;
            string FileName = ((ListViewItem_MFile)listView1.SelectedItems[0]).MFile.Path;
            if (Directory.Exists(_SelectedFolder.Folder.InfosFolder))
            {
                File.WriteAllLines(_SelectedFolder.Folder.InfosFolder + "//" + FileName
                    + ".txt", richTextBox1.Lines, Encoding.Unicode);
            }
            else
            {
                if (MessageBox.Show("This folder hasn't a folder for info files, do you want to browse for it now ?", "No Infos Folder !!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FolderBrowserDialog Fol = new FolderBrowserDialog();
                    Fol.Description = "Info files folder";
                    Fol.ShowNewFolderButton = true;
                    Fol.SelectedPath = _SelectedFolder.Folder.InfosFolder;
                    if (Fol.ShowDialog() == DialogResult.OK)
                    {
                        _SelectedFolder.Folder.InfosFolder = Fol.SelectedPath;
                        File.WriteAllLines(_SelectedFolder.Folder.InfosFolder + "//" + FileName
                   + ".txt", richTextBox1.Lines, Encoding.Unicode);
                    }
                }
            }
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count != 1)
                    return;
                if (treeView1.SelectedNode == null)
                    return;
                if (_SelectedFolder == null)
                    return;
                string FileName = Path.GetFileNameWithoutExtension(((ListViewItem_MFile)listView1.SelectedItems[0]).MFile.Path);
                richTextBox1.Text = ""; //Clear
                richTextBox1.Lines = File.ReadAllLines(_SelectedFolder.Folder.InfosFolder + "//" + FileName + ".txt");
            }
            catch
            {
                richTextBox1.Text = "";
            }
        }
        private void TextBox1_mapper_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(TextBox1_mapper.Text);
                if (Convert.ToInt32(TextBox1_mapper.Text) > 255)
                    TextBox1_mapper.Text = "255";
                if (_SelectedFolder != null)
                    _SelectedFolder.Folder.Mapper = Convert.ToInt32(TextBox1_mapper.Text);
                ShouldSaveFolders = true;
            }
            catch { TextBox1_mapper.Text = "0"; }
        }
        private void TextBox1_mapper_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(TextBox1_mapper.Text);
                if (Convert.ToInt32(TextBox1_mapper.Text) > 255)
                    TextBox1_mapper.Text = "255";
                if (_SelectedFolder != null)
                    _SelectedFolder.Folder.Mapper = Convert.ToInt32(TextBox1_mapper.Text);
            }
            catch { TextBox1_mapper.Text = "0"; }
        }
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            treeView1_AfterSelect(this, null);
        }
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
                return;
            if (TextBox1_search.Text.Length == 0)
                return;
            SearchResultIndexes.Clear();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (((ListViewItem_MFile)listView1.Items[i]).MFile.Name.Length >= TextBox1_search.Text.Length)
                    if (((ListViewItem_MFile)listView1.Items[i]).MFile.Name.ToLower().Substring(0, TextBox1_search.Text.Length) == TextBox1_search.Text.ToLower())
                    {
                        SearchResultIndexes.Add(i);
                    }
            }
            searchResultIndex = -1;
            label1_status.Text = SearchResultIndexes.Count.ToString() + " item(s) matched.";
            toolStripButton10_Click(this, null);
        }
        private void ComboBox1_nav_DropDownClosed(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            if (_SelectedFolder == null)
                return;
            switch (ComboBox1_nav.SelectedIndex)
            {
                case 0:
                    _SelectedFolder.Folder.Filter = FolderFilter.All;
                    TextBox1_mapper.Enabled = false;
                    break;
                case 1:
                    _SelectedFolder.Folder.Filter = FolderFilter.SupportedMappersOnly;
                    TextBox1_mapper.Enabled = false;
                    break;
                case 2:
                    _SelectedFolder.Folder.Filter = FolderFilter.NOTsupportedMapper;
                    TextBox1_mapper.Enabled = true;
                    break;
                case 3:
                    _SelectedFolder.Folder.Filter = FolderFilter.Mapper;
                    TextBox1_mapper.Enabled = true;
                    break;
            }
            treeView1_AfterSelect(this, null);
        }
        //folder properties double click
        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            if (listView2.SelectedItems.Count != 1)
                return;
            switch (listView2.SelectedItems[0].Text)
            {
                case "Snapshots Path":
                    ChangeImageFolder(this, null);
                    break;
                case "Include Sub Folders":
                    ((TreeNode_Folder)treeView1.SelectedNode).Folder.IncludeSubFolders =
                        !((TreeNode_Folder)treeView1.SelectedNode).Folder.IncludeSubFolders;
                    listView2.SelectedItems[0].SubItems[1].Text = 
                        ((TreeNode_Folder)treeView1.SelectedNode).Folder.IncludeSubFolders.ToString();
                    MessageBox.Show("'Include Sub Folders' option changed, please rebuild cache");
                    break;
                case "Info Texts Path":
                    toolStripButton7_Click(this, null);
                    break;
                case "Name":
                    RenameSelected(this, null);
                    break;
            }
        }
        private void rootFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddRootFolder();
        }
        private void folderInSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFolder();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(_SelectedFolder.Folder.Path))
            {
                MessageBox.Show("This folder isn't exist on the disk !!");
                DeleteFolder(this, null);
            }
            else
            {
                _SelectedFolder.Folder.Files = new List<MFile>();
                BuildCache();
                LoadFilesFromCache();
            }
        }

        private void Frm_Browser_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClosing = true;
            if (ShouldSaveFolders)
            {
                SaveFolders();
                ShouldSaveFolders = false;
            }
            SaveSettings();
            Program.Settings.Save();
        }
        //next search result index
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            try
            {
                if (SearchResultIndexes.Count == 0)
                    return;
                searchResultIndex++;
                if (searchResultIndex >= SearchResultIndexes.Count)
                    searchResultIndex = 0;
                listView1.Items[SearchResultIndexes[searchResultIndex]].Selected = true;
                listView1.Items[SearchResultIndexes[searchResultIndex]].EnsureVisible();
            }
            catch
            { }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.MyNesIcon;
            richTextBox1.Text = "";
            if (listView1.SelectedItems.Count != 1)
                return;
            if (treeView1.SelectedNode == null)
                return;
            if (_SelectedFolder == null)
                return;
            #region Load Image
            if (Directory.Exists(_SelectedFolder.Folder.ImagesFolder))
            {
                try
                {
                    string[] Exs = { ".jpg", ".JPG", ".png", ".PNG", ".bmp", ".BMP", ".gif", ".GIF", ".jpeg", ".JPEG", ".tiff", ".TIFF" };
                    string[] Names = Directory.GetFiles(_SelectedFolder.Folder.ImagesFolder);
                    string FileName = Path.GetFileNameWithoutExtension(((ListViewItem_MFile)listView1.SelectedItems[0]).MFile.Name);
                    foreach (string STR in Names)
                    {
                        if (Path.GetFileNameWithoutExtension(STR).Length >= Path.GetFileNameWithoutExtension(((ListViewItem_MFile)listView1.SelectedItems[0]).MFile.Name).Length)
                        {
                            if (Path.GetFileNameWithoutExtension(STR).Substring(0, Path.GetFileNameWithoutExtension(((ListViewItem_MFile)listView1.SelectedItems[0]).MFile.Name).Length) == Path.GetFileNameWithoutExtension(((ListViewItem_MFile)listView1.SelectedItems[0]).MFile.Name))
                            { FileName = Path.GetFileNameWithoutExtension(STR); break; }
                        }
                    }
                    foreach (string STR in Exs)
                    {
                        if (File.Exists(_SelectedFolder.Folder.ImagesFolder + "//" +
                            FileName + STR))
                        {
                            pictureBox1.Image = Image.FromFile(_SelectedFolder.Folder.ImagesFolder + "//" +
                            FileName + STR); break;
                        }
                    }
                }
                catch { pictureBox1.Image = Properties.Resources.MyNesIcon; }
            }
            #endregion
            if (Directory.Exists(_SelectedFolder.Folder.InfosFolder))
            {
                #region Load Info Text
                try
                {
                    string FileName = Path.GetFileNameWithoutExtension(((ListViewItem_MFile)listView1.SelectedItems[0]).MFile.Name);
                    richTextBox1.Text = ""; //Clear
                    richTextBox1.Lines = File.ReadAllLines(_SelectedFolder.Folder.InfosFolder + "//" + FileName + ".txt");
                }
                catch
                {
                    richTextBox1.Text = "";
                }
            }
                #endregion
        }
        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                PlayRom(this, null);
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
                PlayRom(this, null);
        }

        private void thumbnailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
            detailsToolStripMenuItem.Checked = false;
            thumbnailsToolStripMenuItem.Checked = true;
            trackBar1.Visible = true;
        }
        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            detailsToolStripMenuItem.Checked = true;
            thumbnailsToolStripMenuItem.Checked = false; trackBar1.Visible = false;
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
                sortMode = CompareMode.Name;
            else if (e.Column == 1)
                sortMode = CompareMode.Size;
            else if (e.Column == 2)
                sortMode = CompareMode.Rating;
            else if (e.Column == 3)
                sortMode = CompareMode.Mapper;
            else if (e.Column == 4)
                sortMode = CompareMode.IsBatteryBacked;
            else if (e.Column == 5)
                sortMode = CompareMode.IsTrainer;
            else if (e.Column == 6)
                sortMode = CompareMode.PC10;
            else if (e.Column == 7)
                sortMode = CompareMode.VSUnisystem;

            AtoZ = !AtoZ;
            _SelectedFolder.Folder.Files.Sort(new MFileComparer(AtoZ, sortMode));
            LoadFilesFromCache();
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            imageList2.ImageSize = new Size(trackBar1.Value, trackBar1.Value);
        }
        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            IsScrolling = true;
        }
        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            IsScrolling = false;
        }
        //draw stuff
        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (IsClosing)
                return;
            if (listView1.View == View.LargeIcon)
            {
                int h = imageList2.ImageSize.Height;
                int w = imageList2.ImageSize.Width;
                Image im = null;
                if (Directory.Exists(_SelectedFolder.Folder.ImagesFolder))
                {
                    try//Try to load image for the item
                    {
                        string[] Exs = { ".jpg", ".JPG", ".png", ".PNG", ".bmp", ".BMP", ".gif", ".GIF", ".jpeg", ".JPEG", ".tiff", ".TIFF" };
                        string[] Names = Directory.GetFiles(_SelectedFolder.Folder.ImagesFolder);
                        string FileName = Path.GetFileNameWithoutExtension(((ListViewItem_MFile)e.Item).MFile.Name);
                        foreach (string STR in Names)
                        {
                            if (Path.GetFileNameWithoutExtension(STR).Length >= Path.GetFileNameWithoutExtension(((ListViewItem_MFile)e.Item).MFile.Name).Length)
                            {
                                if (Path.GetFileNameWithoutExtension(STR).Substring(0, Path.GetFileNameWithoutExtension(((ListViewItem_MFile)e.Item).MFile.Name).Length) == Path.GetFileNameWithoutExtension(((ListViewItem_MFile)e.Item).MFile.Name))
                                { FileName = Path.GetFileNameWithoutExtension(STR); break; }
                            }
                        }
                        foreach (string STR in Exs)
                        {
                            if (File.Exists(_SelectedFolder.Folder.ImagesFolder + "//" +
                                FileName + STR))
                            {
                                im = Image.FromFile(_SelectedFolder.Folder.ImagesFolder + "//" +
                                FileName + STR); break;
                            }
                        }
                        if (im == null)
                        {
                            im = Properties.Resources.nes_j;
                        }
                    }
                    catch { im = Properties.Resources.nes_j; }
                }
                else
                {
                    im = Properties.Resources.nes_j;
                }
                if (e.Item.Selected)
                {
                    if (listView1.Focused)
                        e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight),
                          e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                    else
                        e.Graphics.FillRectangle(new SolidBrush(SystemColors.ButtonFace),
                    e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                }
                e.Graphics.DrawImage(im, e.Bounds.X + ((e.Bounds.Width / 2) - (w / 2)), e.Bounds.Y + 2, w, h);
                e.DrawText(TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis);
            }
        }
        private void listView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.Header == columnHeader_rating)
            {
                RatingX = e.Bounds.X;

                int rating = ((ListViewItem_MFile)e.Item).MFile.Rating;

                if (e.Item.Selected)
                {
                    if (listView1.Focused)
                        e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight),
                          e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                    else
                        e.Graphics.FillRectangle(new SolidBrush(SystemColors.ButtonFace),
                    e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(e.Item.BackColor),
     e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                }
                //Draw stars
                int g = 0;
                for (int i = 1; i < 6; i++)
                {
                    if (i <= rating)
                    {
                        e.Graphics.DrawImage(Properties.Resources.star, e.Bounds.X + 2 + g, e.Bounds.Y + 2, 16, 16);
                    }
                    else
                    {
                        e.Graphics.DrawImage(Properties.Resources.starunrate, e.Bounds.X + 2 + g, e.Bounds.Y + 2, 16, 16);
                    }
                    g += 15;
                }
            }
            else
                e.DrawDefault = true;
        }
        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawBackground();
            if (e.State == ListViewItemStates.Selected)
                e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 2, e.Bounds.Height - 2);
            if (e.State == ListViewItemStates.Hot)
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.LightYellow), 2), new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 2, e.Bounds.Height - 2));

            e.DrawText(TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);


            if (e.ColumnIndex == 0 & sortMode == CompareMode.Name)
            {
                if (AtoZ)
                    e.Graphics.DrawImage(Properties.Resources.Up, e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + 2, 15, 15);
                else
                    e.Graphics.DrawImage(Properties.Resources.Down, e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + 2, 15, 15);
            }
            else if (e.ColumnIndex == 1 & sortMode == CompareMode.Size)
            {
                if (AtoZ)
                    e.Graphics.DrawImage(Properties.Resources.Up, e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + 2, 15, 15);
                else
                    e.Graphics.DrawImage(Properties.Resources.Down, e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + 2, 15, 15);
            }
            else if (e.ColumnIndex == 2 & sortMode == CompareMode.Rating)
            {
                if (AtoZ)
                    e.Graphics.DrawImage(Properties.Resources.Up, e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + 2, 15, 15);
                else
                    e.Graphics.DrawImage(Properties.Resources.Down, e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + 2, 15, 15);
            }
            else if (e.ColumnIndex == 3 & sortMode == CompareMode.Mapper)
            {
                if (AtoZ)
                    e.Graphics.DrawImage(Properties.Resources.Up, e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + 2, 15, 15);
                else
                    e.Graphics.DrawImage(Properties.Resources.Down, e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + 2, 15, 15);
            }
            else if (e.ColumnIndex == 4 & sortMode == CompareMode.IsBatteryBacked)
            {
                if (AtoZ)
                    e.Graphics.DrawImage(Properties.Resources.Up, e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + 2, 15, 15);
                else
                    e.Graphics.DrawImage(Properties.Resources.Down, e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + 2, 15, 15);
            }
            else if (e.ColumnIndex == 5 & sortMode == CompareMode.IsTrainer)
            {
                if (AtoZ)
                    e.Graphics.DrawImage(Properties.Resources.Up, e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + 2, 15, 15);
                else
                    e.Graphics.DrawImage(Properties.Resources.Down, e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + 2, 15, 15);
            }
            else if (e.ColumnIndex == 6 & sortMode == CompareMode.PC10)
            {
                if (AtoZ)
                    e.Graphics.DrawImage(Properties.Resources.Up, e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + 2, 15, 15);
                else
                    e.Graphics.DrawImage(Properties.Resources.Down, e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + 2, 15, 15);
            }
            else if (e.ColumnIndex == 7 & sortMode == CompareMode.VSUnisystem)
            {
                if (AtoZ)
                    e.Graphics.DrawImage(Properties.Resources.Up, e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + 2, 15, 15);
                else
                    e.Graphics.DrawImage(Properties.Resources.Down, e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + 2, 15, 15);
            }

        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listView1.View == View.Details)
            {
                if (listView1.SelectedItems.Count == 1)
                {
                    if (e.X >= RatingX
                        & e.X <= RatingX + columnHeader_rating.Width
                        & e.Y >= listView1.SelectedItems[0].Position.Y
                        & e.Y <= listView1.SelectedItems[0].Position.Y + 17)
                    {
                        //calculat offset
                        int offset = e.X - RatingX;
                        int rat = 0;
                        if (offset < 15)
                            rat = 1;
                        else if (offset >= 15 & offset < 30)
                            rat = 2;
                        else if (offset >= 30 & offset < 45)
                            rat = 3;
                        else if (offset >= 45 & offset < 60)
                            rat = 4;
                        else if (offset >= 60 & offset < 75)
                            rat = 5;
                        else if (offset >= 75)
                            rat = 0;

                        ListViewItem_MFile item = (ListViewItem_MFile)listView1.SelectedItems[0];
                        item.MFile.Rating = rat;
                        if (item.Index + 1 < listView1.Items.Count)
                            listView1.RedrawItems(item.Index, item.Index + 1, false);
                        else
                            listView1.RedrawItems(item.Index, item.Index, false);
                    }
                }
            }
        }
    }
}
