using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using CycleCore.Nes;
using CycleCore.Nes.Output.Video;

namespace CycleMain
{
    public partial class mainWindow : Form
    {
        Thread mainThread;
        public NesSystem NES;
        IVideoDevice VideoDevice;
        int SlotIndex = 0;
        int statusTime = 0;
        Frm_Console Debugger;

        public mainWindow(string[] Args)
        {
            InitializeComponent();
            LoadSettings();
            CONSOLE.DebugRised += new EventHandler<DebugArg>(CONSOLE_DebugRised);
        }

        void CONSOLE_DebugRised(object sender, DebugArg e)
        {
            if (e.Status == DebugStatus.Notification)
                SetStatus(e.DebugLine, 3);
        }

        void LoadSettings()
        {
            this.Size = Program.Settings.Win_size;
            this.Location = Program.Settings.Win_location;
            //region
            if (Program.Settings.AutoSwitchTvformat)
                autoToolStripMenuItem.Checked = true;
            else
                switch (Program.Settings.TVSystem)
                {
                    case RegionFormat.NTSC: uSANTSCToolStripMenuItem.Checked = true; break;
                    case RegionFormat.PAL: eUPALToolStripMenuItem.Checked = true; break;
                }
            //video size
            switch (Program.Settings.VideoSize.ToLower())
            {
                case "x1": x1ToolStripMenuItem.Checked = true; break;
                case "x2": x2ToolStripMenuItem.Checked = true; break;
                case "x3": x3ToolStripMenuItem.Checked = true; break;
                case "x4": x4ToolStripMenuItem.Checked = true; break;
                case "x5": x5ToolStripMenuItem.Checked = true; break;
                case "x6": x6ToolStripMenuItem.Checked = true; break;
                case "stretch": stretchToolStripMenuItem.Checked = true; break;
            }

            VideoDevice = new VideoD3D(RegionFormat.NTSC, null);

            RefreshControlsProfiles();
            RefreshRecents();
            SetVolumeLabel();

            if (Program.Settings.RunConsole)
            {
                Debugger = new Frm_Console();
                Debugger.FormClosed += new FormClosedEventHandler(Debugger_FormClosed);
                Debugger.Show();
            }
        }

        void Debugger_FormClosed(object sender, FormClosedEventArgs e)
        {
            Debugger.SaveSettings();
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
            if (mainThread != null)
                mainThread.Abort();
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
            RefreshRecents();
            RefreshStateFiles();
            if (NES.Cartridge.DataBaseInfo.Game_Name != null)
            {
                string name = NES.Cartridge.DataBaseInfo.Game_Name;
                if (NES.Cartridge.DataBaseInfo.Game_AltName != null)
                    name += " (" + NES.Cartridge.DataBaseInfo.Game_AltName + ")";
                this.Text = name + " | CycleFC";
            }
            else
                this.Text = Path.GetFileNameWithoutExtension(FileName) + " | CycleFC";
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
                    { Program.Settings.TVSystem = RegionFormat.PAL; CONSOLE.WriteLine(this, "PAL", DebugStatus.Notification); }
                    else
                    { Program.Settings.TVSystem = RegionFormat.NTSC; CONSOLE.WriteLine(this, "NTSC", DebugStatus.Notification); }

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
            profileToolStripMenuItem.DropDownItems.Clear();
            foreach (ControlProfile profile in Program.Settings.ControlProfiles)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = profile.Name;
                profileToolStripMenuItem.DropDownItems.Add(item);
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
                { Program.Settings.Recents.Remove(FilePath); }
            }
            Program.Settings.Recents.Insert(0, FilePath);
            //limit to 9 elements
            if (Program.Settings.Recents.Count > 9)
                Program.Settings.Recents.RemoveAt(9);
        }
        void RefreshRecents()
        {
            if (Program.Settings.Recents == null)
                Program.Settings.Recents = new System.Collections.Specialized.StringCollection();
            toolStripSplitButton6.DropDownItems.Clear();
            int i = 1;
            foreach (string Recc in Program.Settings.Recents)
            {
                ToolStripMenuItem IT = new ToolStripMenuItem();
                IT.Text = Path.GetFileNameWithoutExtension(Recc);
                switch (i)//This for the recent item shortcut key
                //So that user can press CTRL + item No
                {
                    case 1:
                        IT.ShortcutKeys = ((System.Windows.Forms.Keys)
                            ((System.Windows.Forms.Keys.Control |
                            System.Windows.Forms.Keys.D1)));
                        break;
                    case 2:
                        IT.ShortcutKeys = ((System.Windows.Forms.Keys)
                            ((System.Windows.Forms.Keys.Control |
                            System.Windows.Forms.Keys.D2)));
                        break;
                    case 3:
                        IT.ShortcutKeys = ((System.Windows.Forms.Keys)
                            ((System.Windows.Forms.Keys.Control |
                            System.Windows.Forms.Keys.D3)));
                        break;
                    case 4:
                        IT.ShortcutKeys = ((System.Windows.Forms.Keys)
                            ((System.Windows.Forms.Keys.Control |
                            System.Windows.Forms.Keys.D4)));
                        break;
                    case 5:
                        IT.ShortcutKeys = ((System.Windows.Forms.Keys)
                            ((System.Windows.Forms.Keys.Control |
                            System.Windows.Forms.Keys.D5)));
                        break;
                    case 6:
                        IT.ShortcutKeys = ((System.Windows.Forms.Keys)
                            ((System.Windows.Forms.Keys.Control |
                            System.Windows.Forms.Keys.D6)));
                        break;
                    case 7:
                        IT.ShortcutKeys = ((System.Windows.Forms.Keys)
                            ((System.Windows.Forms.Keys.Control |
                            System.Windows.Forms.Keys.D7)));
                        break;
                    case 8:
                        IT.ShortcutKeys = ((System.Windows.Forms.Keys)
                            ((System.Windows.Forms.Keys.Control |
                            System.Windows.Forms.Keys.D8)));
                        break;
                    case 9:
                        IT.ShortcutKeys = ((System.Windows.Forms.Keys)
                            ((System.Windows.Forms.Keys.Control |
                            System.Windows.Forms.Keys.D9)));
                        break;
                }
                IT.ToolTipText = Recc;
                toolStripSplitButton6.DropDownItems.Add(IT);
                i++;
            }
        }
        void SetVolumeLabel()
        {
            string vol = ((((100 * (3000 - Program.Settings.Volume)) / 3000) - 200) * -1).ToString() + " %";
            volume100ToolStripMenuItem.Text = "&Volume " + vol;
        }
        void SetStatus(string status, int time)
        {
            StatusLabel.Text = status;
            statusTime = time;
        }
        void TakeSnapshot()
        {
            if (NES == null)
                return;
            //If there's no rom, get out !!
            if (!File.Exists(NES.Cartridge.FilePath))
            { return; }
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
            SetStatus("Snapshot taken", 3);
        }
        void SaveState()
        {
            if (NES == null)
                return;
            string dir = Path.GetFullPath(Program.Settings.StatesFolder);
            Directory.CreateDirectory(dir);

            string filepath = Path.GetFullPath(Program.Settings.StatesFolder)
                 + "\\" + Path.GetFileNameWithoutExtension(NES.Cartridge.FilePath) + "_" + SlotIndex.ToString() + ".mns";
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
                 + "\\" + Path.GetFileNameWithoutExtension(NES.Cartridge.FilePath) + "_" + SlotIndex.ToString() + ".mns";
            if (File.Exists(filepath))
            {
                Stream str = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                NES.LoadStateRequest(filepath, str);
            }
            else
            {
                SetStatus("No state found at slot " + (SlotIndex + 1).ToString(), 4);
            }
        }
        void RefreshStateFiles()
        {
            //clear old
            toolStripMenuItem_slot1.Text = "1 ";
            toolStripMenuItem_slot2.Text = "2 ";
            toolStripMenuItem_slot3.Text = "3 ";
            toolStripMenuItem_slot4.Text = "4 ";
            toolStripMenuItem_slot5.Text = "5 ";
            toolStripMenuItem_slot6.Text = "6 ";
            toolStripMenuItem_slot7.Text = "7 ";
            toolStripMenuItem_slot8.Text = "8 ";
            toolStripMenuItem_slot9.Text = "9 ";

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
                        toolStripMenuItem_slot1.Text = "1 " + date;
                    if (no == "1")
                        toolStripMenuItem_slot2.Text = "2 " + date;
                    if (no == "2")
                        toolStripMenuItem_slot3.Text = "3 " + date;
                    if (no == "3")
                        toolStripMenuItem_slot4.Text = "4 " + date;
                    if (no == "4")
                        toolStripMenuItem_slot5.Text = "5 " + date;
                    if (no == "5")
                        toolStripMenuItem_slot6.Text = "6 " + date;
                    if (no == "6")
                        toolStripMenuItem_slot7.Text = "7 " + date;
                    if (no == "7")
                        toolStripMenuItem_slot8.Text = "8 " + date;
                    if (no == "8")
                        toolStripMenuItem_slot9.Text = "9 " + date;

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
            if (mainThread != null)
                mainThread.Abort();
            if (this.NES != null)
                this.NES.ShutDown();
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
                toolStripStatusLabel1.Text = "FPS: " + this.NES.FPS;
                if (NES.Apu.Output != null)
                    if (NES.Apu.Output.Recorder.IsRecording)
                        SetStatus("Recording sound [" + TimeSpan.FromSeconds(NES.Apu.Output.Recorder.Time) + "]", 2);
                this.NES.FPS = 0;
            }
            if (statusTime > 0)
                statusTime--;
            else
            { StatusLabel.Text = ""; statusTime = -1; }

        }
        private void noLimiterToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (NES != null)
                NES.NoLimiter = noLimiterToolStripMenuItem.Checked;

            SetStatus(NES.NoLimiter ? "No limiter enabled" : "No limiter disabled", 3);
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
        private void x1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x1ToolStripMenuItem.Checked = true;
            x2ToolStripMenuItem.Checked = false;
            x3ToolStripMenuItem.Checked = false;
            x4ToolStripMenuItem.Checked = false;
            x5ToolStripMenuItem.Checked = false;
            x6ToolStripMenuItem.Checked = false;
            stretchToolStripMenuItem.Checked = false;
            Program.Settings.VideoSize = "x1";
            ApplyVideoSettings();
        }
        private void x2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x1ToolStripMenuItem.Checked = false;
            x2ToolStripMenuItem.Checked = true;
            x3ToolStripMenuItem.Checked = false;
            x4ToolStripMenuItem.Checked = false;
            x5ToolStripMenuItem.Checked = false;
            x6ToolStripMenuItem.Checked = false;
            stretchToolStripMenuItem.Checked = false;
            Program.Settings.VideoSize = "x2"; ApplyVideoSettings();
        }
        private void x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x1ToolStripMenuItem.Checked = false;
            x2ToolStripMenuItem.Checked = false;
            x3ToolStripMenuItem.Checked = true;
            x4ToolStripMenuItem.Checked = false;
            x5ToolStripMenuItem.Checked = false;
            x6ToolStripMenuItem.Checked = false;
            stretchToolStripMenuItem.Checked = false;
            Program.Settings.VideoSize = "x3"; ApplyVideoSettings();
        }
        private void x4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x1ToolStripMenuItem.Checked = false;
            x2ToolStripMenuItem.Checked = false;
            x3ToolStripMenuItem.Checked = false;
            x4ToolStripMenuItem.Checked = true;
            x5ToolStripMenuItem.Checked = false;
            x6ToolStripMenuItem.Checked = false;
            stretchToolStripMenuItem.Checked = false;
            Program.Settings.VideoSize = "x4"; ApplyVideoSettings();
        }
        private void x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x1ToolStripMenuItem.Checked = false;
            x2ToolStripMenuItem.Checked = false;
            x3ToolStripMenuItem.Checked = false;
            x4ToolStripMenuItem.Checked = false;
            x5ToolStripMenuItem.Checked = true;
            x6ToolStripMenuItem.Checked = false;
            stretchToolStripMenuItem.Checked = false;
            Program.Settings.VideoSize = "x5"; ApplyVideoSettings();
        }
        private void x6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x1ToolStripMenuItem.Checked = false;
            x2ToolStripMenuItem.Checked = false;
            x3ToolStripMenuItem.Checked = false;
            x4ToolStripMenuItem.Checked = false;
            x5ToolStripMenuItem.Checked = false;
            x6ToolStripMenuItem.Checked = true;
            stretchToolStripMenuItem.Checked = false;
            Program.Settings.VideoSize = "x6"; ApplyVideoSettings();
        }
        private void stretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x1ToolStripMenuItem.Checked = false;
            x2ToolStripMenuItem.Checked = false;
            x3ToolStripMenuItem.Checked = false;
            x4ToolStripMenuItem.Checked = false;
            x5ToolStripMenuItem.Checked = false;
            x6ToolStripMenuItem.Checked = false;
            stretchToolStripMenuItem.Checked = true;
            Program.Settings.VideoSize = "stretch"; ApplyVideoSettings();
        }
        private void paletteToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void configToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = true;
            Frm_ControlsSettings settings = new Frm_ControlsSettings();
            settings.ShowDialog(this);

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
        private void profileToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            foreach (ControlProfile profile in Program.Settings.ControlProfiles)
            {
                if (e.ClickedItem.Text == profile.Name)
                {
                    Program.Settings.CurrentControlProfile = profile;
                    RefreshControlsProfiles();
                    ApplyControlsSettings();
                    break;
                }
            }
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = true;
            Frm_About about = new Frm_About();
            about.ShowDialog(this);
            if (NES != null)
                NES.PAUSE = false;
        }
        private void toolStripSplitButton6_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            toolStripSplitButton6.HideDropDown();
            int index = 0;
            for (int i = 0; i < toolStripSplitButton6.DropDownItems.Count; i++)
            {
                if (e.ClickedItem == toolStripSplitButton6.DropDownItems[i])
                { index = i; break; }
            }
            if (File.Exists(Program.Settings.Recents[index]) == true)
            { OpenRom(Program.Settings.Recents[index], true); }
            else
            {
                MessageBox.Show("This rom is missing !!");
                Program.Settings.Recents.RemoveAt(index);
                RefreshRecents();
            }
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
            Frm_AudioOptions au = new Frm_AudioOptions();
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
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem_slot1));
            else if (e.KeyData == Keys.D2 & !e.Control)
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem_slot2));
            else if (e.KeyData == Keys.D3 & !e.Control)
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem_slot3));
            else if (e.KeyData == Keys.D4 & !e.Control)
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem_slot4));
            else if (e.KeyData == Keys.D5 & !e.Control)
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem_slot5));
            else if (e.KeyData == Keys.D6 & !e.Control)
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem_slot6));
            else if (e.KeyData == Keys.D7 & !e.Control)
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem_slot7));
            else if (e.KeyData == Keys.D8 & !e.Control)
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem_slot8));
            else if (e.KeyData == Keys.D9 & !e.Control)
                slotToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(toolStripMenuItem_slot9));
            else if (((e.KeyData & Keys.Control) == Keys.Control) & ((e.KeyData & Keys.O) == Keys.O))
                toolStripButton1_Click(this, null);
        }
        private void showToolStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip1.Visible = showToolStripToolStripMenuItem.Checked;
        }
        private void showStatusStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip1.Visible = showStatusStripToolStripMenuItem.Checked;
        }
        private void foldersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = true;
            Frm_PathsOptions au = new Frm_PathsOptions();
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
            foreach (ToolStripMenuItem item in slotToolStripMenuItem.DropDownItems)
            {
                if (e.ClickedItem == item)
                { item.Checked = true; SlotIndex = i; }
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
        private void recordeSoundToolStripMenuItem_Click(object sender, EventArgs e)
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
                        recordeSoundToolStripMenuItem.Text = "&Stop recording";
                    }
                    NES.PAUSE = false;
                }
                else
                {
                    NES.Apu.Output.Recorder.Stop();
                    recordeSoundToolStripMenuItem.Text = "&Record sound";
                    SetStatus("WAV SAVED", 5);
                }
            }
        }
        private void fullscreenToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.Fullscreen = fullscreenToolStripMenuItem.Checked;
            ToggleFullscreen();
        }
        //show browser
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {

        }
        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Debugger == null)
            {
                Debugger = new Frm_Console();
                Debugger.Show();
            }
            else if (!Debugger.Visible)
            {
                Debugger = new Frm_Console();
                Debugger.Show();
            }
            Debugger.FormClosed += new FormClosedEventHandler(Debugger_FormClosed);
        }

        private void autoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uSANTSCToolStripMenuItem.Checked = false;
            eUPALToolStripMenuItem.Checked = false;
            autoToolStripMenuItem.Checked = true;
            Program.Settings.TVSystem = RegionFormat.NTSC;
            Program.Settings.AutoSwitchTvformat = true;
            ApplyVideoSettings();
        }
        private void uSANTSCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uSANTSCToolStripMenuItem.Checked = true;
            eUPALToolStripMenuItem.Checked = false;
            autoToolStripMenuItem.Checked = false;
            Program.Settings.TVSystem = RegionFormat.NTSC;
            Program.Settings.AutoSwitchTvformat = false;
            ApplyVideoSettings();
        }
        private void eUPALToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uSANTSCToolStripMenuItem.Checked = false;
            eUPALToolStripMenuItem.Checked = true;
            autoToolStripMenuItem.Checked = false;
            Program.Settings.TVSystem = RegionFormat.PAL;
            Program.Settings.AutoSwitchTvformat = false;
            ApplyVideoSettings();
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
            Frm_GeneralOptions au = new Frm_GeneralOptions();
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
            if (mainThread != null)
                mainThread.Abort();
            if (this.NES != null)
                this.NES.ShutDown();
            NES = null;
            RestoreTitleString();
            panel1.Refresh();
        }

        private void RestoreTitleString()
        {
            this.Text = "CycleFC";
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NES != null)
                NES.PAUSE = !NES.PAUSE;
        }

        private void autoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            nTSCUSAJapanToolStripMenuItem.Checked = false;
            pALEuropeToolStripMenuItem.Checked = false;
            autoToolStripMenuItem1.Checked = true;
            Program.Settings.TVSystem = RegionFormat.NTSC;
            Program.Settings.AutoSwitchTvformat = true;
            ApplyVideoSettings();
        }

        private void pALEuropeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nTSCUSAJapanToolStripMenuItem.Checked = false;
            pALEuropeToolStripMenuItem.Checked = true;
            autoToolStripMenuItem1.Checked = false;
            Program.Settings.TVSystem = RegionFormat.PAL;
            Program.Settings.AutoSwitchTvformat = false;
            ApplyVideoSettings();
        }

        private void nTSCUSAJapanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nTSCUSAJapanToolStripMenuItem.Checked = true;
            pALEuropeToolStripMenuItem.Checked = false;
            autoToolStripMenuItem1.Checked = false;
            Program.Settings.TVSystem = RegionFormat.NTSC;
            Program.Settings.AutoSwitchTvformat = false;
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
                }
                NES.PAUSE = false;
            }
        }
    }
}