using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Imaging;
using CycleCore.Nes;
using CycleCore.Nes.Output.Video;
using System.Reflection;
using MyNes.Windows;

namespace CycleMain
{
    public partial class mainWindow : Form
    {
        Thread mainThread;
        public NesSystem NES;
        IVideoDevice VideoDevice;
        int SlotIndex = 0;
        int statusTime = 0;
        private int phase = 0;
        private Bitmap gradientBitmap = null;
        private float angle = 0;
        private int redPhase = 25;
        private int greenPhase = 85;
        private int bluePhase = 170;
        private int backgroundPhase = 0;

        public mainWindow(string[] Args)
        {
            InitializeComponent();
            LoadSettings();
        }

        void LoadSettings()
        {
            this.Size = Program.Settings.Win_size;
            this.Location = Program.Settings.Win_location;
            //region
            if (Program.Settings.AutoSwitchTvformat)
                autoToolStripMenuItem1.Checked = true;
            else
                switch (Program.Settings.TVSystem)
                {
                    case RegionFormat.NTSC: nTSCUSAJapanToolStripMenuItem.Checked = true; break;
                    case RegionFormat.PAL: pALEuropeToolStripMenuItem.Checked = true; break;
                }
            //video size
            switch (Program.Settings.VideoSize.ToLower())
            {
                case "x1": x1ToolStripMenuItem1.Checked = true; break;
                case "x2": x2ToolStripMenuItem1.Checked = true; break;
                case "x3": x3ToolStripMenuItem1.Checked = true; break;
                case "x4": x4ToolStripMenuItem1.Checked = true; break;
                case "x5": x5ToolStripMenuItem1.Checked = true; break;
                case "x6": x6ToolStripMenuItem1.Checked = true; break;
                case "stretch": stretchToolStripMenuItem1.Checked = true; break;
            }

            VideoDevice = new VideoD3D(RegionFormat.NTSC, null);

            RefreshControlsProfiles();
            SetVolumeLabel();
        }

        void SaveSettings()
        {
            Program.Settings.Win_size = this.Size;
            Program.Settings.Win_location = this.Location;
            Program.Settings.Save();
        }
        public void OpenRom(string FileName, bool AutoStart)
        {
            // check out the opened rom
            NesCartridge cart = new NesCartridge(null);
            LoadRomStatus status = cart.Load(FileName, new FileStream(FileName, FileMode.Open, FileAccess.Read), null, true);
            switch (status)
            {
                case LoadRomStatus.LoadFailure: MessageBox.Show("Unable to load this ROM image, not iNES format or damaged header", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop); return;
                case LoadRomStatus.InvalidHeader: MessageBox.Show("Unable to load this ROM image, not iNES format", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop); return;
                case LoadRomStatus.InvalidMapper: MessageBox.Show("Unable to load this ROM image, unsupported mapper # " + cart.Mapper, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop); return;
            }
            // turn off the old nes
            if (this.NES != null)
                this.NES.ShutDown();

            // new nes
            this.NES = new CycleCore.Nes.NesSystem(new TIMER());
            //create rom file stream
            Stream romStream = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            //create the sram file
            this.NES.Cartridge.SavePath = FileName.Substring(0, FileName.Length - 3) + "sav";
            Stream sramStream = Stream.Null;
            if (File.Exists(this.NES.Cartridge.SavePath))
                sramStream = new FileStream(this.NES.Cartridge.SavePath, FileMode.Open, FileAccess.ReadWrite);

            //load the cart
            this.NES.LoadRom(FileName, romStream, sramStream);
            this.NES.TurnOn();

            // settings
            ApplyVideoSettings();
            ApplySoundSettings();
            ApplyControlsSettings();
            this.NES.AutoSaveSRAM = Program.Settings.AutoSaveSRAM;

            // Run !!
            if (AutoStart)
            {
                mainThread = new Thread(new ThreadStart(NES.Execute));
                mainThread.Start();
            }
            AddRecent(FileName);
            RefreshStateFiles();

            uint crc32 = CalculateCRC32(FileName);
            string convertString = crc32.ToString("X8");

            string targetCRC = convertString;
            string gameName = romDB.FindGameNameByCRC(targetCRC);

            Text = gameName + " | CycleFC";
        }

        void SetupScreenPosition()
        {
            if (panel_surface.Dock != DockStyle.Fill)
            {
                int X = (panel1.Width / 2) - (panel_surface.Width / 2);
                int Y = (panel1.Height / 2) - (panel_surface.Height / 2);
                panel_surface.Parent = panel1;
                panel_surface.Location = new Point(X, Y);
            }
        }
        void ApplyVideoSettings()
        {
            if (NES != null)
            {
                NES.PAUSE = true;
                if (NES.Ppu.Output != null)
                    NES.Ppu.Output.Dispose();

                switch (Program.Settings.VideoSize.ToLower())
                {
                    case "x1":
                        panel_surface.Dock = DockStyle.None;
                        panel_surface.Size = new Size(256, 224);
                        SetupScreenPosition();
                        break;
                    case "x2":
                        panel_surface.Dock = DockStyle.None;
                        panel_surface.Size = new Size(256 * 2, 224 * 2);
                        SetupScreenPosition();
                        break;
                    case "x3":
                        panel_surface.Dock = DockStyle.None;
                        panel_surface.Size = new Size(256 * 3, 224 * 3);
                        SetupScreenPosition();
                        break;
                    case "x4":
                        panel_surface.Dock = DockStyle.None;
                        panel_surface.Size = new Size(256 * 4, 224 * 4);
                        SetupScreenPosition();
                        break;
                    case "x5":
                        panel_surface.Dock = DockStyle.None;
                        panel_surface.Size = new Size(256 * 5, 224 * 5);
                        SetupScreenPosition();
                        break;
                    case "x6":
                        panel_surface.Dock = DockStyle.None;
                        panel_surface.Size = new Size(256 * 6, 224 * 6);
                        SetupScreenPosition();
                        break;
                    case "stretch": panel_surface.Dock = DockStyle.Fill; break;
                }
                //Set tv format to make sure
                if (Program.Settings.AutoSwitchTvformat)
                    if (NES.Cartridge.IsPAL)
                    {
                        Program.Settings.TVSystem = RegionFormat.PAL; CONSOLE.WriteLine(this, "PAL", DebugStatus.Notification);
                    }
                    else
                    {
                        Program.Settings.TVSystem = RegionFormat.NTSC; CONSOLE.WriteLine(this, "NTSC", DebugStatus.Notification);
                    }

                NES.Ppu.Output = new VideoD3D(Program.Settings.TVSystem, panel_surface);

                VideoDevice = NES.Ppu.Output;

                if (NES.Ppu.Output.SupportFullScreen)
                    NES.Ppu.Output.FullScreen = Program.Settings.Fullscreen;

                if (Program.Settings.PaletteFormat == null)
                    Program.Settings.PaletteFormat = new PaletteFormat();

                NES.SetTVFormat(Program.Settings.TVSystem, Program.Settings.PaletteFormat);

                if (NES != null)
                    NES.PAUSE = false;
            }
        }
        void ApplySoundSettings()
        {
            if (NES != null)
            {
                NES.PAUSE = true;
                if (NES.Apu.Output != null)
                    NES.Apu.Output.Shutdown();
                //Initialize device
                AudioDSD slm = new AudioDSD();
                slm.Initialize(this.Handle, NES);
                NES.Apu.Output = slm;

                NES.SoundEnabled = Program.Settings.SoundEnabled;
                NES.Apu.ChannelSq1.Audible = Program.Settings.Sq1Enabled;
                NES.Apu.ChannelSq2.Audible = Program.Settings.Sq2Enabled;
                NES.Apu.ChannelTri.Audible = Program.Settings.TriEnabled;
                NES.Apu.ChannelNoi.Audible = Program.Settings.NoiEnabled;
                NES.Apu.ChannelDpm.Audible = Program.Settings.DpmEnabled;

                if (NES.Apu.External != null)
                {
                    if (NES.Apu.External is Vrc6ExternalComponent)
                    {
                        (NES.Apu.External as Vrc6ExternalComponent).ChannelSq1.Audible = Program.Settings.Vrc6Sq1Enabled;
                        (NES.Apu.External as Vrc6ExternalComponent).ChannelSq2.Audible = Program.Settings.Vrc6Sq2Enabled;
                        (NES.Apu.External as Vrc6ExternalComponent).ChannelSaw.Audible = Program.Settings.Vrc6SawEnabled;
                    }
                    else if (NES.Apu.External is Mmc5ExternalComponent)
                    {
                        (NES.Apu.External as Mmc5ExternalComponent).ChannelSq1.Audible = Program.Settings.Mmc5Sq1Enabled;
                        (NES.Apu.External as Mmc5ExternalComponent).ChannelSq2.Audible = Program.Settings.Mmc5Sq2Enabled;
                        (NES.Apu.External as Mmc5ExternalComponent).ChannelPcm.Audible = Program.Settings.Mmc5DpmEnabled;
                    }
                    else if (NES.Apu.External is Ss5BExternalComponent)
                    {
                        (NES.Apu.External as Ss5BExternalComponent).ChannelVW1.Audible = Program.Settings.SS5BWv1Enabled;
                        (NES.Apu.External as Ss5BExternalComponent).ChannelVW2.Audible = Program.Settings.SS5BWv2Enabled;
                        (NES.Apu.External as Ss5BExternalComponent).ChannelVW3.Audible = Program.Settings.SS5BWv3Enabled;
                    }
                }

                NES.Apu.SetVolume(Program.Settings.Volume);

                if (!NES.SoundEnabled)
                {
                    NES.Apu.Stop();
                }

                NES.PAUSE = false;
            }
        }
        void ApplyControlsSettings()
        {
            if (NES != null)
            {
                NES.PAUSE = true;
                InputManager Manager = new InputManager(this.Handle);
                Joypad Joy1 = new Joypad(Manager);
                Joy1.A.Input = Program.Settings.CurrentControlProfile.Player1_A;
                Joy1.B.Input = Program.Settings.CurrentControlProfile.Player1_B;
                Joy1.Up.Input = Program.Settings.CurrentControlProfile.Player1_Up;
                Joy1.Down.Input = Program.Settings.CurrentControlProfile.Player1_Down;
                Joy1.Left.Input = Program.Settings.CurrentControlProfile.Player1_Left;
                Joy1.Right.Input = Program.Settings.CurrentControlProfile.Player1_Right;
                Joy1.Select.Input = Program.Settings.CurrentControlProfile.Player1_Select;
                Joy1.Start.Input = Program.Settings.CurrentControlProfile.Player1_Start;
                Joypad Joy2 = new Joypad(Manager);
                Joy2.A.Input = Program.Settings.CurrentControlProfile.Player2_A;
                Joy2.B.Input = Program.Settings.CurrentControlProfile.Player2_B;
                Joy2.Up.Input = Program.Settings.CurrentControlProfile.Player2_Up;
                Joy2.Down.Input = Program.Settings.CurrentControlProfile.Player2_Down;
                Joy2.Left.Input = Program.Settings.CurrentControlProfile.Player2_Left;
                Joy2.Right.Input = Program.Settings.CurrentControlProfile.Player2_Right;
                Joy2.Select.Input = Program.Settings.CurrentControlProfile.Player2_Select;
                Joy2.Start.Input = Program.Settings.CurrentControlProfile.Player2_Start;
                NES.CpuMemory.InputDevice = Manager;
                NES.CpuMemory.Joypad1 = Joy1;
                NES.CpuMemory.Joypad2 = Joy2;
                NES.PAUSE = false;
            }
        }
        void RefreshControlsProfiles()
        {
            profilesToolStripMenuItem.DropDownItems.Clear();
            foreach (ControlProfile profile in Program.Settings.ControlProfiles)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = profile.Name;
                profilesToolStripMenuItem.DropDownItems.Add(item);
                if (profile.Name == Program.Settings.CurrentControlProfile.Name)
                    item.Checked = true;
            }
        }
        void AddRecent(string FilePath)
        {
            if (Program.Settings.Recents == null)
                Program.Settings.Recents = new System.Collections.Specialized.StringCollection();
            for (int i = 0; i < Program.Settings.Recents.Count; i++)
            {
                if (FilePath == Program.Settings.Recents[i])
                {
                    Program.Settings.Recents.Remove(FilePath);
                }
            }
            Program.Settings.Recents.Insert(0, FilePath);
            //limit to 9 elements
            if (Program.Settings.Recents.Count > 9)
                Program.Settings.Recents.RemoveAt(9);
        }
        void SetVolumeLabel()
        {
            string vol = ((((100 * (3000 - Program.Settings.Volume)) / 3000) - 200) * -1).ToString() + " %";
            volumeToolStripMenuItem.Text = "&Volume " + vol;
        }
        void SetStatus(string status, int time)
        {
            statusTime = time;
        }
        void TakeSnapshot()
        {
            if (NES == null)
                return;
            //If there's no rom, get out !!
            if (!File.Exists(NES.Cartridge.FilePath))
            {
                return;
            }
            NES.PAUSE = true;
            Directory.CreateDirectory(Path.GetFullPath(Program.Settings.ImagesFolder));
            int i = 1;
            while (i != 0)
            {
                if (!File.Exists(Path.GetFullPath(Program.Settings.ImagesFolder) + "\\"
                    + Path.GetFileNameWithoutExtension(NES.Cartridge.FilePath) + "_" +
                    i.ToString() + Program.Settings.ImagesFormat))
                {
                    NES.Ppu.Output.TakeSnapshot(Path.GetFullPath(Program.Settings.ImagesFolder) + "\\"
                        + Path.GetFileNameWithoutExtension(NES.Cartridge.FilePath) + "_" +
                        i.ToString() + Program.Settings.ImagesFormat,
                       Program.Settings.ImagesFormat);
                    break;
                }
                i++;
            }
            NES.PAUSE = false;
            StatusData("Screenshot Taken");
        }
        void SaveState()
        {
            if (NES == null)
                return;
            string dir = Path.GetFullPath(Program.Settings.StatesFolder);
            Directory.CreateDirectory(dir);

            string filepath = Path.GetFullPath(Program.Settings.StatesFolder)
                 + "\\" + Path.GetFileNameWithoutExtension(NES.Cartridge.FilePath) + "_" + SlotIndex.ToString() + ".esav";
            Stream str = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            NES.SaveStateRequest(filepath, str);
            RefreshStateFiles();
        }
        void LoadState()
        {
            if (NES == null)
                return;
            string dir = Path.GetFullPath(Program.Settings.StatesFolder);
            Directory.CreateDirectory(dir);

            string filepath = Path.GetFullPath(Program.Settings.StatesFolder)
                 + "\\" + Path.GetFileNameWithoutExtension(NES.Cartridge.FilePath) + "_" + SlotIndex.ToString() + ".esav";
            if (File.Exists(filepath))
            {
                Stream str = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                NES.LoadStateRequest(filepath, str);
            }
            else
            {
                SetStatus("Unable to find the save state at this slot " + (SlotIndex + 1).ToString(), 4);
            }
        }
        void RefreshStateFiles()
        {
            //clear old
            toolStripMenuItem2.Text = "1 ";
            toolStripMenuItem3.Text = "2 ";
            toolStripMenuItem4.Text = "3 ";
            toolStripMenuItem5.Text = "4 ";
            toolStripMenuItem6.Text = "5 ";
            toolStripMenuItem7.Text = "6 ";
            toolStripMenuItem8.Text = "7 ";
            toolStripMenuItem9.Text = "8 ";
            toolStripMenuItem10.Text = "9 ";

            string[] dirs = Directory.GetFiles(Program.Settings.StatesFolder);
            foreach (string dir in dirs)
            {
                //if (this.NES.CheckStateFile(new FileStream(dir, FileMode.Open, FileAccess.Read)))
                if (Path.GetFileNameWithoutExtension(dir).Substring(0, Path.GetFileNameWithoutExtension(dir).Length - 2) ==
                    Path.GetFileNameWithoutExtension(NES.Cartridge.FilePath))
                {
                    //get state number
                    string no = dir.Substring(dir.Length - 5, 1);
                    //get date
                    FileInfo inf = new FileInfo(dir);
                    string date = "[" + inf.LastWriteTime.ToLocalTime().ToString() + "]";
                    if (no == "0")
                        toolStripMenuItem2.Text = "1 " + date;
                    if (no == "1")
                        toolStripMenuItem3.Text = "2 " + date;
                    if (no == "2")
                        toolStripMenuItem4.Text = "3 " + date;
                    if (no == "3")
                        toolStripMenuItem5.Text = "4 " + date;
                    if (no == "4")
                        toolStripMenuItem6.Text = "5 " + date;
                    if (no == "5")
                        toolStripMenuItem7.Text = "6 " + date;
                    if (no == "6")
                        toolStripMenuItem8.Text = "7 " + date;
                    if (no == "7")
                        toolStripMenuItem9.Text = "8 " + date;
                    if (no == "8")
                        toolStripMenuItem10.Text = "9 " + date;

                }
            }
        }
        void ToggleFullscreen()
        {
            if (NES != null)
            {
                NES.PAUSE = true;
                if (NES.Ppu.Output.SupportFullScreen)
                {
                    NES.Ppu.Output.FullScreen = Program.Settings.Fullscreen;
                    if (!Program.Settings.Fullscreen)
                    {
                        //restore the old status after the back
                        //from Fullscreen.
                        base.Size = this.MaximumSize;
                        this.Location = Program.Settings.Win_location;
                        this.Size = Program.Settings.Win_size;
                    }
                    else
                    {
                        //save the window status to restore when
                        //we back from Fullscreen.
                        Program.Settings.Win_location = this.Location;
                        Program.Settings.Win_size = this.Size;
                    }
                }
                else
                {
                    MessageBox.Show(NES.Ppu.Output.Name + " video mode doesn't support Fullscreen");
                }
                NES.PAUSE = false;
            }
        }

        private void Frm_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            if (this.NES != null)
                this.NES.ShutDown();
            if (mainThread != null)
                Environment.Exit(0);
        }
        //Open rom
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = true;
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Open INES rom";
            op.Filter = "All Supported Files |*.nes;*.NES;*.7z;*.7Z;*.rar;*.RAR;*.zip;*.ZIP|INES rom (*.nes)|*.nes;*.NES|Archives (*.7z *.rar *.zip)|*.7z;*.7Z;*.rar;*.RAR;*.zip;*.ZIP";
            if (op.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                OpenRom(op.FileName, true);
            }
            if (NES != null)
                NES.PAUSE = false;
        }
        //FPS timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (NES != null)
            {
                //toolStripStatusLabel1.Text = "FPS: " + this.NES.FPS;
                if (NES.Apu.Output != null)
                    if (NES.Apu.Output.Recorder.IsRecording)
                        SetStatus("Recording sound [" + TimeSpan.FromSeconds(NES.Apu.Output.Recorder.Time) + "]", 2);
                this.NES.FPS = 0;
            }
            if (statusTime > 0)
                statusTime--;
            else
            {
                //StatusLabel.Text = ""; statusTime = -1; 
            }

        }

        public void turnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mainThread != null)
                mainThread.Abort();
            if (this.NES != null)
                this.NES.ShutDown();
            NES = null;
            this.Text = "My Nes";
            panel1.Refresh();
        }
        private void togglePauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = !NES.PAUSE;
        }
        private void hardResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
            {
                OpenRom(NES.Cartridge.FilePath, true);
            }
        }
        private void softResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
            {
                NES.Cpu.SoftReset();
            }
        }

        private void descreptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = true;
            MessageBox.Show(VideoDevice.Desc);
            if (NES != null)
                NES.PAUSE = false;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            SetupScreenPosition();
        }
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, ".\\Help.chm", HelpNavigator.TableOfContents);
        }
        private void soundChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = true;
            audioOptionsWindow au = new audioOptionsWindow();
            au.ShowDialog(this);
            if (au.OK)
            {
                ApplySoundSettings();
            }
            if (NES != null)
                NES.PAUSE = false;
        }
        private void upToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.Settings.Volume < 0)
            {
                Program.Settings.Volume += 200;
            }
            if (NES != null)
                NES.Apu.Output.SetVolume(Program.Settings.Volume);
            SetVolumeLabel();
            string vol = ((((100 * (3000 - Program.Settings.Volume)) / 3000) - 200) * -1).ToString() + " %";
            SetStatus("Volume " + vol, 4);
        }
        private void downToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.Settings.Volume > -3000)
            {
                Program.Settings.Volume -= 200;
            }
            if (NES != null)
                NES.Apu.Output.SetVolume(Program.Settings.Volume);
            SetVolumeLabel();
            string vol = ((((100 * (3000 - Program.Settings.Volume)) / 3000) - 200) * -1).ToString() + " %";
            SetStatus("Volume " + vol, 4);
        }
        //KEYS...
        private void Frm_main_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Oemplus)
                upToolStripMenuItem_Click(this, null);
            else if (e.KeyData == Keys.OemMinus)
                downToolStripMenuItem_Click(this, null);
            else if (e.KeyData == Keys.D1 & !e.Control)
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem2));
            else if (e.KeyData == Keys.D2 & !e.Control)
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem3));
            else if (e.KeyData == Keys.D3 & !e.Control)
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem4));
            else if (e.KeyData == Keys.D4 & !e.Control)
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem5));
            else if (e.KeyData == Keys.D5 & !e.Control)
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem6));
            else if (e.KeyData == Keys.D6 & !e.Control)
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem7));
            else if (e.KeyData == Keys.D7 & !e.Control)
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem8));
            else if (e.KeyData == Keys.D8 & !e.Control)
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem9));
            else if (e.KeyData == Keys.D9 & !e.Control)
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem10));
            else if (((e.KeyData & Keys.Control) == Keys.Control) & ((e.KeyData & Keys.O) == Keys.O))
                toolStripButton1_Click(this, null);
        }
        private void foldersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = true;
            pathWindow au = new pathWindow();
            au.ShowDialog(this);
            if (NES != null)
            {
                NES.PAUSE = false;
            }
        }
        private void Frm_main_Deactivate(object sender, EventArgs e)
        {
            if (NES != null & Program.Settings.PauseWhenFocusLost)
                NES.PAUSE = true;
        }
        private void Frm_main_Activated(object sender, EventArgs e)
        {
            if (NES != null & Program.Settings.PauseWhenFocusLost)
                NES.PAUSE = false;
        }
        private void takeSnapshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeSnapshot();
        }

        private void slotToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int i = 0;
            foreach (ToolStripMenuItem item in slotsToolStripMenuItem.DropDownItems)
            {
                if (e.ClickedItem == item)
                {
                    item.Checked = true; SlotIndex = i;
                }
                else
                    item.Checked = false;
                i++;
            }
            SetStatus("State slot " + (SlotIndex + 1).ToString() + " selected.", 3);
        }
        private void saveStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveState();
        }
        private void saveStateAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES == null)
            {
                MessageBox.Show("Nes is off !!");
                return;
            }
            SaveFileDialog sav = new SaveFileDialog();
            sav.Filter = "My Nes state file(*.mns)|*.mns";
            if (sav.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Stream str = new FileStream(sav.FileName, FileMode.Create, FileAccess.Write);
                NES.SaveStateRequest(sav.FileName, str);
            }
        }
        private void loadStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadState();
        }
        private void loadStateAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES == null)
            {
                MessageBox.Show("Nes is off !!");
                return;
            }
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "My Nes state file(*.mns)|*.mns";
            if (op.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Stream str = new FileStream(op.FileName, FileMode.Open, FileAccess.Read);
                NES.LoadStateRequest(op.FileName, str);
            }
        }
        private void romInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
            {
                NES.PAUSE = true;
                romInfoWindow rr = new romInfoWindow(NES.Cartridge.FilePath);
                rr.ShowDialog(this);
                NES.PAUSE = false;
            }
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = true;
            if (VideoDevice == null)
                VideoDevice = new VideoD3D(RegionFormat.NTSC, panel_surface);
            VideoDevice.ChangeSettings();
            if (NES != null)
                NES.PAUSE = false;
        }
        private void generalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = true;
            generalOptionsWindow au = new generalOptionsWindow();
            au.ShowDialog(this);
            if (NES != null)
            {
                NES.PAUSE = false;
            }
        }
        private void saveSRAMAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
            {
                NES.PAUSE = true;
                if (!NES.Cartridge.HasSaveRam)
                {
                    if (MessageBox.Show("This rom is not Battery Backed, are you sure to save S-RAM ?", "Save S-RAM",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;
                }
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "S-RAM (*.sav)|*.sav";
                if (save.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    Stream str = new FileStream(save.FileName, FileMode.Create, FileAccess.Write);
                    str.Write(NES.CpuMemory.srm, 0, 0x2000);
                    str.Close();
                }
                NES.PAUSE = false;
            }
            else
            {
                MessageBox.Show("Nes is off");
            }
        }
        private void loadSRAMAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
            {
                NES.PAUSE = true;
                if (!NES.Cartridge.HasSaveRam)
                {
                    if (MessageBox.Show("This rom is not Battery Backed, are you sure to load S-RAM ?", "Load S-RAM",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;
                }
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "S-RAM (*.sav)|*.sav";
                if (op.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    Stream str = new FileStream(op.FileName, FileMode.Open, FileAccess.Read);
                    str.Read(NES.CpuMemory.srm, 0, 0x2000);
                    str.Close();
                }
                NES.PAUSE = false;
            }
            else
            {
                MessageBox.Show("Nes is off");
            }
        }

        private void panel_surface_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Frm_main_Load(object sender, EventArgs e)
        {
            RestoreTitleString();
            AnimTimer.Start();
            AnimTimer.Enabled = true;
            pictureBox1.Visible = true;
            WaitClock.Interval = 3000;
            WaitClock.Tick += new EventHandler(WaitClock_Tick);
            StatusData("CycleFC Initialized.");
        }

        private void openROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = true;
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Open NES / Famicom ROM images";
            op.Filter = "NES / Famicom ROM Files (*.nes)|*.nes;*.NES";
            if (op.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                OpenRom(op.FileName, true);

                AnimTimer.Stop();
                AnimTimer.Enabled = false;
                pictureBox1.Visible = false;

                uint crc32 = CalculateCRC32(op.FileName);
                string convertString = crc32.ToString("X8");
                string targetCRC = convertString;
                string gameName = romDB.FindGameNameByCRC(targetCRC);

                StatusData("Loaded ROM Image: " + op.SafeFileName + $" | {gameName}");
            }
            if (NES != null)
                NES.PAUSE = false;
        }

        private void softResetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (NES != null)
            {
                NES.Cpu.SoftReset();
            }
        }

        private void hardResetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (NES != null)
            {
                OpenRom(NES.Cartridge.FilePath, true);
            }
        }

        private void closeROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.NES != null)
                this.NES.ShutDown();
            NES = null;
            AnimTimer.Start();
            AnimTimer.Enabled = true;
            pictureBox1.Visible = true;
            StatusData("Unloaded ROM");
            RestoreTitleString();
            panel1.Refresh();
        }

        private void RestoreTitleString()
        {
            Text = "CycleFC ";
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = !NES.PAUSE;

            if (pauseToolStripMenuItem.Checked == true)
            {
                label1.Visible = true;
                label1.Text = "Paused.";
            }
            else
            {
                StatusData("Resumed.");
            }
        }

        private void autoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            nTSCUSAJapanToolStripMenuItem.Checked = false;
            pALEuropeToolStripMenuItem.Checked = false;
            autoToolStripMenuItem1.Checked = true;
            Program.Settings.TVSystem = RegionFormat.NTSC;
            Program.Settings.AutoSwitchTvformat = true;
            StatusData("Switched to Automatic region detection mode.");
            ApplyVideoSettings();
        }

        private void pALEuropeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nTSCUSAJapanToolStripMenuItem.Checked = false;
            pALEuropeToolStripMenuItem.Checked = true;
            autoToolStripMenuItem1.Checked = false;
            Program.Settings.TVSystem = RegionFormat.PAL;
            Program.Settings.AutoSwitchTvformat = false;
            StatusData("Switched to PAL region mode.");
            ApplyVideoSettings();
        }

        private void nTSCUSAJapanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nTSCUSAJapanToolStripMenuItem.Checked = true;
            pALEuropeToolStripMenuItem.Checked = false;
            autoToolStripMenuItem1.Checked = false;
            Program.Settings.TVSystem = RegionFormat.NTSC;
            Program.Settings.AutoSwitchTvformat = false;
            StatusData("Switched to NTSC region mode.");
            ApplyVideoSettings();
        }

        private void noLimiterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loadStateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LoadState();
        }

        private void loadStateAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "State File(*.esav)|*.esav";
            if (op.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Stream str = new FileStream(op.FileName, FileMode.Open, FileAccess.Read);
                NES.LoadStateRequest(op.FileName, str);
                StatusData("Loaded state file: " + op.SafeFileName);
            }
        }

        private void saveStateAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sav = new SaveFileDialog();
            sav.Filter = "State File(*.esav)|*.esav";
            if (sav.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Stream str = new FileStream(sav.FileName, FileMode.Create, FileAccess.Write);
                NES.SaveStateRequest(sav.FileName, str);
                StatusData("Saved state file: " + sav.FileName);
            }
        }

        private void saveSRAMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
            {
                NES.PAUSE = true;
                if (!NES.Cartridge.HasSaveRam)
                {
                    if (MessageBox.Show("This game doesn't use S-RAM, do you want to force saving it?", "Save S-RAM",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;
                }
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "S-RAM (*.sav)|*.sav";
                if (save.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    Stream str = new FileStream(save.FileName, FileMode.Create, FileAccess.Write);
                    str.Write(NES.CpuMemory.srm, 0, 0x2000);
                    str.Close();
                    StatusData("Save S-RAM file: " + save.FileName);
                }
                NES.PAUSE = false;
            }
        }

        private void loadSRAMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
            {
                NES.PAUSE = true;
                if (!NES.Cartridge.HasSaveRam)
                {
                    if (MessageBox.Show("This game doesn't use S-RAM, do you want to force loading it?", "Load S-RAM",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;
                }
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "S-RAM (*.sav)|*.sav";
                if (op.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    Stream str = new FileStream(op.FileName, FileMode.Open, FileAccess.Read);
                    str.Read(NES.CpuMemory.srm, 0, 0x2000);
                    str.Close();
                    StatusData("Loaded S-RAM file: " + op.FileName);
                }
                NES.PAUSE = false;
            }
        }

        private void fullscreenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void FullScreenCheck(object sender, EventArgs e)
        {
            Program.Settings.Fullscreen = fullScreenModeToolStripMenuItem.Checked;
            ToggleFullscreen();
        }

        private void takeScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeSnapshot();
        }

        private void x1ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            x1ToolStripMenuItem1.Checked = true;
            x2ToolStripMenuItem1.Checked = false;
            x3ToolStripMenuItem1.Checked = false;
            x4ToolStripMenuItem1.Checked = false;
            x5ToolStripMenuItem1.Checked = false;
            x6ToolStripMenuItem1.Checked = false;
            stretchToolStripMenuItem1.Checked = false;
            Program.Settings.VideoSize = "x1";
            ApplyVideoSettings();
        }

        private void x2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            x1ToolStripMenuItem1.Checked = false;
            x2ToolStripMenuItem1.Checked = true;
            x3ToolStripMenuItem1.Checked = false;
            x4ToolStripMenuItem1.Checked = false;
            x5ToolStripMenuItem1.Checked = false;
            x6ToolStripMenuItem1.Checked = false;
            stretchToolStripMenuItem1.Checked = false;
            Program.Settings.VideoSize = "x2";
            ApplyVideoSettings();
        }

        private void x3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            x1ToolStripMenuItem1.Checked = false;
            x2ToolStripMenuItem1.Checked = false;
            x3ToolStripMenuItem1.Checked = true;
            x4ToolStripMenuItem1.Checked = false;
            x5ToolStripMenuItem1.Checked = false;
            x6ToolStripMenuItem1.Checked = false;
            stretchToolStripMenuItem1.Checked = false;
            Program.Settings.VideoSize = "x3";
            ApplyVideoSettings();
        }

        private void x4ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            x1ToolStripMenuItem1.Checked = false;
            x2ToolStripMenuItem1.Checked = false;
            x3ToolStripMenuItem1.Checked = false;
            x4ToolStripMenuItem1.Checked = true;
            x5ToolStripMenuItem1.Checked = false;
            x6ToolStripMenuItem1.Checked = false;
            stretchToolStripMenuItem1.Checked = false;
            Program.Settings.VideoSize = "x4";
            ApplyVideoSettings();
        }

        private void x5ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            x1ToolStripMenuItem1.Checked = false;
            x2ToolStripMenuItem1.Checked = false;
            x3ToolStripMenuItem1.Checked = false;
            x4ToolStripMenuItem1.Checked = false;
            x5ToolStripMenuItem1.Checked = true;
            x6ToolStripMenuItem1.Checked = false;
            stretchToolStripMenuItem1.Checked = false;
            Program.Settings.VideoSize = "x5";
            ApplyVideoSettings();
        }

        private void x6ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            x1ToolStripMenuItem1.Checked = false;
            x2ToolStripMenuItem1.Checked = false;
            x3ToolStripMenuItem1.Checked = false;
            x4ToolStripMenuItem1.Checked = false;
            x5ToolStripMenuItem1.Checked = false;
            x6ToolStripMenuItem1.Checked = true;
            stretchToolStripMenuItem1.Checked = false;
            Program.Settings.VideoSize = "x6";
            ApplyVideoSettings();
        }

        private void stretchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            x1ToolStripMenuItem1.Checked = false;
            x2ToolStripMenuItem1.Checked = false;
            x3ToolStripMenuItem1.Checked = false;
            x4ToolStripMenuItem1.Checked = false;
            x5ToolStripMenuItem1.Checked = false;
            x6ToolStripMenuItem1.Checked = false;
            stretchToolStripMenuItem1.Checked = true;
            Program.Settings.VideoSize = "stretch";
            ApplyVideoSettings();
        }

        private void audioChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = true;
            audioOptionsWindow au = new audioOptionsWindow();
            au.ShowDialog(this);
            if (au.OK)
            {
                ApplySoundSettings();
            }
            if (NES != null)
                NES.PAUSE = false;
        }

        private void audioRecorderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
            {
                if (!NES.Apu.Output.Recorder.IsRecording)
                {
                    NES.PAUSE = true;
                    SaveFileDialog SAV = new SaveFileDialog();
                    SAV.Title = "Save wave file";
                    SAV.Filter = "WAV PCM (*.wav)|*.wav";
                    if (SAV.ShowDialog(this) == DialogResult.OK)
                    {
                        NES.Apu.Output.Recorder.Record(SAV.FileName, false);
                        audioRecorderToolStripMenuItem.Text = "Stop Recording";
                        StatusData("Recording Sound...");
                    }
                    NES.PAUSE = false;
                }
                else
                {
                    NES.Apu.Output.Recorder.Stop();
                    audioRecorderToolStripMenuItem.Text = "Audio Recorder";
                    StatusData("Audio saved.");
                }
            }
        }

        private void toolStripMenuItem_slot1_Click(object sender, EventArgs e)
        {

        }

        private void rOMImageInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
            {
                NES.PAUSE = true;
                romInfoWindow rr = new romInfoWindow(NES.Cartridge.FilePath);
                rr.ShowDialog();
                NES.PAUSE = false;
            }
            else
            {
                MessageBox.Show("Please load the ROM first!", "No ROM info data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void configToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = true;
            controlSettingsWindow settings = new controlSettingsWindow();
            settings.ShowDialog();

            RefreshControlsProfiles();
            if (NES != null)
            {
                if (settings.OK)
                {
                    ApplyControlsSettings();
                }
                NES.PAUSE = false;
            }
        }

        private void profilesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void videosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = true;
            if (VideoDevice == null)
                VideoDevice = new VideoD3D(RegionFormat.NTSC, panel_surface);
            VideoDevice.ChangeSettings();
            if (NES != null)
                NES.PAUSE = false;
        }

        private void foldersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = true;
            pathWindow au = new pathWindow();
            au.ShowDialog(this);
            if (NES != null)
            {
                NES.PAUSE = false;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = true;
            AboutDiag about = new AboutDiag();
            about.ShowDialog(this);
            if (NES != null)
                NES.PAUSE = false;
        }

        private void volume100ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void downToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Program.Settings.Volume > -3000)
            {
                Program.Settings.Volume -= 200;
            }
            if (NES != null)
                NES.Apu.Output.SetVolume(Program.Settings.Volume);
            SetVolumeLabel();
            string vol = ((((100 * (3000 - Program.Settings.Volume)) / 3000) - 200) * -1).ToString() + " %";
            StatusData("Volume " + vol);
        }

        private void paletteWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (NES != null)
            //    NES.PAUSE = true;
            bool old = Program.Settings.PauseWhenFocusLost;
            Program.Settings.PauseWhenFocusLost = false;
            var form = new FormPalette();
            form.ShowDialog(this);

            if (NES != null)
            {
                NES.SetTVFormat(Program.Settings.TVSystem, Program.Settings.PaletteFormat);
                NES.PAUSE = false;
            }
            Program.Settings.PauseWhenFocusLost = old;
        }

        private void upToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Program.Settings.Volume < 0)
            {
                Program.Settings.Volume += 200;
            }
            if (NES != null)
                NES.Apu.Output.SetVolume(Program.Settings.Volume);
            SetVolumeLabel();
            string vol = ((((100 * (3000 - Program.Settings.Volume)) / 3000) - 200) * -1).ToString() + " %";
            StatusData("Volume " + vol);
        }

        private void generalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            generalOptionsWindow au = new generalOptionsWindow();
            au.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSettings();
            if (this.NES != null)
                this.NES.ShutDown();
            if (mainThread != null)
                Environment.Exit(0);
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            if (gradientBitmap != null)
            {
                gradientBitmap.Dispose();
            }
            int red = (255 + redPhase) % 256;
            int green = (255 + greenPhase) % 256;
            int blue = (255 + bluePhase) % 256;
            try
            {
                gradientBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to resize...");
            }

            using (Graphics g = Graphics.FromImage(gradientBitmap))
            {
                Rectangle rect = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
                PointF center = new PointF(rect.Width / 2f, rect.Height / 2f);
                using (System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(rect, Color.FromArgb(red, green, blue), Color.FromArgb(0, 85, 255), angle))
                {
                    g.FillRectangle(brush, rect);
                }
            }
            pictureBox1.Image = gradientBitmap;
            redPhase = (redPhase + 2) % 256;
            greenPhase = (greenPhase + 1) % 256;
            bluePhase = (bluePhase + 4) % 256;
            backgroundPhase = (backgroundPhase + 1) % 256;
            angle += 1;
            if (angle >= 360)
            {
                angle = 0;
            }
        }

        private void WaitClock_Tick(object sender, EventArgs e)
        {
            label1.Visible = false;
            WaitClock.Stop();
        }

        public string StatusData(String str)
        {
            WaitClock.Stop();
            label1.Visible = true;
            label1.Text = str;
            WaitClock.Start();
            return str;
        }

        public static class Crc32Helper
        {
            private static readonly uint[] Crc32Table;

            static Crc32Helper()
            {
                Crc32Table = new uint[256];

                for (uint i = 0; i < 256; i++)
                {
                    uint crc = i;
                    for (int j = 0; j < 8; j++)
                    {
                        crc = (crc & 1) == 1 ? (crc >> 1) ^ 0xEDB88320 : crc >> 1;
                    }
                    Crc32Table[i] = crc;
                }
            }

            public static uint UpdateCrc32(uint crc32, byte[] buffer, int offset, int length)
            {
                for (int i = offset; i < offset + length; i++)
                {
                    crc32 = (crc32 >> 8) ^ Crc32Table[(crc32 ^ buffer[i]) & 0xFF];
                }
                return crc32;
            }
        }

        public static uint CalculateCRC32(string filePath)
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                uint crc32 = 0xFFFFFFFF;

                // Skip the first 16 bytes
                fileStream.Seek(16, SeekOrigin.Begin);

                int bytesRead;
                byte[] buffer = new byte[4096];

                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    crc32 = Crc32Helper.UpdateCrc32(crc32, buffer, 0, bytesRead);
                }

                crc32 = ~crc32;

                return crc32;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}