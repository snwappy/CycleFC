/*********************************************************************\r
*This file is part of CycleFC                                         *               
*                                                                     *
*Original code © Ala Hadid 2009 - 2011                                *
*Code maintainance by Snappy Pupper (@snappypupper on Twitter)        *
*Code updated: October 2023                                           *
*                                                                     *                                                                     *
*CycleFC is a fork of the original CycleMain,                             *
*which is free software: you can redistribute it and/or modify        *
*it under the terms of the GNU General Public License as published by *
*the Free Software Foundation, either version 3 of the License, or    *
*(at your option) any later version.                                  *
*                                                                     *
*You should have received a copy of the GNU General Public License    *
*along with this program.  If not, see <http://www.gnu.org/licenses/>.*
\*********************************************************************/
namespace CycleMain
{
    partial class mainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWindow));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_surface = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.hardResetToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.softResetToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.consoleRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
            this.nTSCUSAJapanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pALEuropeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.loadStateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadStateAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            this.saveStateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveStateAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.saveSRAMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSRAMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullScreenModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeScreenshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator31 = new System.Windows.Forms.ToolStripSeparator();
            this.windowSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x1ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.x2ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.x3ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.x4ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.x5ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.x6ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stretchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.paletteWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioRecorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator33 = new System.Windows.Forms.ToolStripSeparator();
            this.volumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.downToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            this.rOMImageInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator32 = new System.Windows.Forms.ToolStripSeparator();
            this.showStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showFPSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
            this.controlProfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.profilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.foldersToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.generalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.videosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cycleFCEmulatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 361);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(487, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(50, 17);
            this.toolStripStatusLabel1.Text = "FPS: 0";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Enabled = false;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.panel_surface);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(487, 337);
            this.panel1.TabIndex = 3;
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // panel_surface
            // 
            this.panel_surface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_surface.Location = new System.Drawing.Point(0, 0);
            this.panel_surface.Name = "panel_surface";
            this.panel_surface.Size = new System.Drawing.Size(487, 337);
            this.panel_surface.TabIndex = 0;
            this.panel_surface.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_surface_Paint);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.systemToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.cycleFCEmulatorToolStripMenuItem,
            this.helpToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(487, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openROMToolStripMenuItem,
            this.closeROMToolStripMenuItem,
            this.toolStripSeparator11,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openROMToolStripMenuItem
            // 
            this.openROMToolStripMenuItem.Name = "openROMToolStripMenuItem";
            this.openROMToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openROMToolStripMenuItem.Text = "&Open ROM";
            this.openROMToolStripMenuItem.Click += new System.EventHandler(this.openROMToolStripMenuItem_Click);
            // 
            // closeROMToolStripMenuItem
            // 
            this.closeROMToolStripMenuItem.Name = "closeROMToolStripMenuItem";
            this.closeROMToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeROMToolStripMenuItem.Text = "&Close ROM";
            this.closeROMToolStripMenuItem.Click += new System.EventHandler(this.closeROMToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(177, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pauseToolStripMenuItem,
            this.toolStripSeparator22,
            this.hardResetToolStripMenuItem1,
            this.softResetToolStripMenuItem1,
            this.toolStripSeparator23,
            this.consoleRegionToolStripMenuItem,
            this.toolStripSeparator24,
            this.loadStateToolStripMenuItem1,
            this.loadStateAsToolStripMenuItem1,
            this.toolStripSeparator28,
            this.saveStateToolStripMenuItem1,
            this.saveStateAsToolStripMenuItem1,
            this.toolStripSeparator29,
            this.saveSRAMToolStripMenuItem,
            this.loadSRAMToolStripMenuItem,
            this.toolStripSeparator30,
            this.videoToolStripMenuItem,
            this.audioToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.systemToolStripMenuItem.Text = "&System";
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.CheckOnClick = true;
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pauseToolStripMenuItem.Text = "Pause";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(177, 6);
            // 
            // hardResetToolStripMenuItem1
            // 
            this.hardResetToolStripMenuItem1.Name = "hardResetToolStripMenuItem1";
            this.hardResetToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.hardResetToolStripMenuItem1.Text = "Hard reset";
            this.hardResetToolStripMenuItem1.Click += new System.EventHandler(this.hardResetToolStripMenuItem1_Click);
            // 
            // softResetToolStripMenuItem1
            // 
            this.softResetToolStripMenuItem1.Name = "softResetToolStripMenuItem1";
            this.softResetToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.softResetToolStripMenuItem1.Text = "Soft reset";
            this.softResetToolStripMenuItem1.Click += new System.EventHandler(this.softResetToolStripMenuItem1_Click);
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(177, 6);
            // 
            // consoleRegionToolStripMenuItem
            // 
            this.consoleRegionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoToolStripMenuItem1,
            this.toolStripSeparator26,
            this.nTSCUSAJapanToolStripMenuItem,
            this.pALEuropeToolStripMenuItem});
            this.consoleRegionToolStripMenuItem.Name = "consoleRegionToolStripMenuItem";
            this.consoleRegionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.consoleRegionToolStripMenuItem.Text = "Console region";
            // 
            // autoToolStripMenuItem1
            // 
            this.autoToolStripMenuItem1.Name = "autoToolStripMenuItem1";
            this.autoToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.autoToolStripMenuItem1.Text = "Auto";
            this.autoToolStripMenuItem1.Click += new System.EventHandler(this.autoToolStripMenuItem1_Click);
            // 
            // toolStripSeparator26
            // 
            this.toolStripSeparator26.Name = "toolStripSeparator26";
            this.toolStripSeparator26.Size = new System.Drawing.Size(177, 6);
            // 
            // nTSCUSAJapanToolStripMenuItem
            // 
            this.nTSCUSAJapanToolStripMenuItem.Name = "nTSCUSAJapanToolStripMenuItem";
            this.nTSCUSAJapanToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.nTSCUSAJapanToolStripMenuItem.Text = "NTSC (USA, Japan)";
            this.nTSCUSAJapanToolStripMenuItem.Click += new System.EventHandler(this.nTSCUSAJapanToolStripMenuItem_Click);
            // 
            // pALEuropeToolStripMenuItem
            // 
            this.pALEuropeToolStripMenuItem.Name = "pALEuropeToolStripMenuItem";
            this.pALEuropeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pALEuropeToolStripMenuItem.Text = "PAL (Europe)";
            this.pALEuropeToolStripMenuItem.Click += new System.EventHandler(this.pALEuropeToolStripMenuItem_Click);
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(177, 6);
            // 
            // loadStateToolStripMenuItem1
            // 
            this.loadStateToolStripMenuItem1.Name = "loadStateToolStripMenuItem1";
            this.loadStateToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.loadStateToolStripMenuItem1.Text = "Load State";
            this.loadStateToolStripMenuItem1.Click += new System.EventHandler(this.loadStateToolStripMenuItem1_Click);
            // 
            // loadStateAsToolStripMenuItem1
            // 
            this.loadStateAsToolStripMenuItem1.Name = "loadStateAsToolStripMenuItem1";
            this.loadStateAsToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.loadStateAsToolStripMenuItem1.Text = "Load State as";
            this.loadStateAsToolStripMenuItem1.Click += new System.EventHandler(this.loadStateAsToolStripMenuItem1_Click);
            // 
            // toolStripSeparator28
            // 
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            this.toolStripSeparator28.Size = new System.Drawing.Size(177, 6);
            // 
            // saveStateToolStripMenuItem1
            // 
            this.saveStateToolStripMenuItem1.Name = "saveStateToolStripMenuItem1";
            this.saveStateToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.saveStateToolStripMenuItem1.Text = "Save State";
            // 
            // saveStateAsToolStripMenuItem1
            // 
            this.saveStateAsToolStripMenuItem1.Name = "saveStateAsToolStripMenuItem1";
            this.saveStateAsToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.saveStateAsToolStripMenuItem1.Text = "Save State as";
            this.saveStateAsToolStripMenuItem1.Click += new System.EventHandler(this.saveStateAsToolStripMenuItem1_Click);
            // 
            // toolStripSeparator29
            // 
            this.toolStripSeparator29.Name = "toolStripSeparator29";
            this.toolStripSeparator29.Size = new System.Drawing.Size(177, 6);
            // 
            // saveSRAMToolStripMenuItem
            // 
            this.saveSRAMToolStripMenuItem.Name = "saveSRAMToolStripMenuItem";
            this.saveSRAMToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveSRAMToolStripMenuItem.Text = "Save S-RAM";
            this.saveSRAMToolStripMenuItem.Click += new System.EventHandler(this.saveSRAMToolStripMenuItem_Click);
            // 
            // loadSRAMToolStripMenuItem
            // 
            this.loadSRAMToolStripMenuItem.Name = "loadSRAMToolStripMenuItem";
            this.loadSRAMToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadSRAMToolStripMenuItem.Text = "Load S-RAM";
            this.loadSRAMToolStripMenuItem.Click += new System.EventHandler(this.loadSRAMToolStripMenuItem_Click);
            // 
            // toolStripSeparator30
            // 
            this.toolStripSeparator30.Name = "toolStripSeparator30";
            this.toolStripSeparator30.Size = new System.Drawing.Size(177, 6);
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullScreenModeToolStripMenuItem,
            this.takeScreenshotToolStripMenuItem,
            this.toolStripSeparator31,
            this.windowSizeToolStripMenuItem,
            this.paletteWindowToolStripMenuItem});
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.videoToolStripMenuItem.Text = "Video";
            // 
            // fullScreenModeToolStripMenuItem
            // 
            this.fullScreenModeToolStripMenuItem.CheckOnClick = true;
            this.fullScreenModeToolStripMenuItem.Name = "fullScreenModeToolStripMenuItem";
            this.fullScreenModeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fullScreenModeToolStripMenuItem.Text = "Full Screen Mode";
            this.fullScreenModeToolStripMenuItem.CheckedChanged += new System.EventHandler(this.FullScreenCheck);
            // 
            // takeScreenshotToolStripMenuItem
            // 
            this.takeScreenshotToolStripMenuItem.Name = "takeScreenshotToolStripMenuItem";
            this.takeScreenshotToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.takeScreenshotToolStripMenuItem.Text = "Take screenshot";
            this.takeScreenshotToolStripMenuItem.Click += new System.EventHandler(this.takeScreenshotToolStripMenuItem_Click);
            // 
            // toolStripSeparator31
            // 
            this.toolStripSeparator31.Name = "toolStripSeparator31";
            this.toolStripSeparator31.Size = new System.Drawing.Size(177, 6);
            // 
            // windowSizeToolStripMenuItem
            // 
            this.windowSizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x1ToolStripMenuItem1,
            this.x2ToolStripMenuItem1,
            this.x3ToolStripMenuItem1,
            this.x4ToolStripMenuItem1,
            this.x5ToolStripMenuItem1,
            this.x6ToolStripMenuItem1,
            this.stretchToolStripMenuItem1});
            this.windowSizeToolStripMenuItem.Name = "windowSizeToolStripMenuItem";
            this.windowSizeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.windowSizeToolStripMenuItem.Text = "Window Size";
            // 
            // x1ToolStripMenuItem1
            // 
            this.x1ToolStripMenuItem1.Name = "x1ToolStripMenuItem1";
            this.x1ToolStripMenuItem1.Size = new System.Drawing.Size(111, 22);
            this.x1ToolStripMenuItem1.Text = "x1";
            this.x1ToolStripMenuItem1.Click += new System.EventHandler(this.x1ToolStripMenuItem1_Click);
            // 
            // x2ToolStripMenuItem1
            // 
            this.x2ToolStripMenuItem1.Name = "x2ToolStripMenuItem1";
            this.x2ToolStripMenuItem1.Size = new System.Drawing.Size(111, 22);
            this.x2ToolStripMenuItem1.Text = "x2";
            this.x2ToolStripMenuItem1.Click += new System.EventHandler(this.x2ToolStripMenuItem1_Click);
            // 
            // x3ToolStripMenuItem1
            // 
            this.x3ToolStripMenuItem1.Name = "x3ToolStripMenuItem1";
            this.x3ToolStripMenuItem1.Size = new System.Drawing.Size(111, 22);
            this.x3ToolStripMenuItem1.Text = "x3";
            this.x3ToolStripMenuItem1.Click += new System.EventHandler(this.x3ToolStripMenuItem1_Click);
            // 
            // x4ToolStripMenuItem1
            // 
            this.x4ToolStripMenuItem1.Name = "x4ToolStripMenuItem1";
            this.x4ToolStripMenuItem1.Size = new System.Drawing.Size(111, 22);
            this.x4ToolStripMenuItem1.Text = "x4";
            this.x4ToolStripMenuItem1.Click += new System.EventHandler(this.x4ToolStripMenuItem1_Click);
            // 
            // x5ToolStripMenuItem1
            // 
            this.x5ToolStripMenuItem1.Name = "x5ToolStripMenuItem1";
            this.x5ToolStripMenuItem1.Size = new System.Drawing.Size(111, 22);
            this.x5ToolStripMenuItem1.Text = "x5";
            this.x5ToolStripMenuItem1.Click += new System.EventHandler(this.x5ToolStripMenuItem1_Click);
            // 
            // x6ToolStripMenuItem1
            // 
            this.x6ToolStripMenuItem1.Name = "x6ToolStripMenuItem1";
            this.x6ToolStripMenuItem1.Size = new System.Drawing.Size(111, 22);
            this.x6ToolStripMenuItem1.Text = "x6";
            this.x6ToolStripMenuItem1.Click += new System.EventHandler(this.x6ToolStripMenuItem1_Click);
            // 
            // stretchToolStripMenuItem1
            // 
            this.stretchToolStripMenuItem1.Name = "stretchToolStripMenuItem1";
            this.stretchToolStripMenuItem1.Size = new System.Drawing.Size(111, 22);
            this.stretchToolStripMenuItem1.Text = "Stretch";
            this.stretchToolStripMenuItem1.Click += new System.EventHandler(this.stretchToolStripMenuItem1_Click);
            // 
            // paletteWindowToolStripMenuItem
            // 
            this.paletteWindowToolStripMenuItem.Name = "paletteWindowToolStripMenuItem";
            this.paletteWindowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.paletteWindowToolStripMenuItem.Text = "Palette Window";
            this.paletteWindowToolStripMenuItem.Click += new System.EventHandler(this.paletteWindowToolStripMenuItem_Click);
            // 
            // audioToolStripMenuItem
            // 
            this.audioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.audioChannelsToolStripMenuItem,
            this.audioRecorderToolStripMenuItem,
            this.toolStripSeparator33,
            this.volumeToolStripMenuItem});
            this.audioToolStripMenuItem.Name = "audioToolStripMenuItem";
            this.audioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.audioToolStripMenuItem.Text = "Audio";
            // 
            // audioChannelsToolStripMenuItem
            // 
            this.audioChannelsToolStripMenuItem.Name = "audioChannelsToolStripMenuItem";
            this.audioChannelsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.audioChannelsToolStripMenuItem.Text = "Audio channels";
            this.audioChannelsToolStripMenuItem.Click += new System.EventHandler(this.audioChannelsToolStripMenuItem_Click);
            // 
            // audioRecorderToolStripMenuItem
            // 
            this.audioRecorderToolStripMenuItem.Name = "audioRecorderToolStripMenuItem";
            this.audioRecorderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.audioRecorderToolStripMenuItem.Text = "Audio Recorder";
            this.audioRecorderToolStripMenuItem.Click += new System.EventHandler(this.audioRecorderToolStripMenuItem_Click);
            // 
            // toolStripSeparator33
            // 
            this.toolStripSeparator33.Name = "toolStripSeparator33";
            this.toolStripSeparator33.Size = new System.Drawing.Size(177, 6);
            // 
            // volumeToolStripMenuItem
            // 
            this.volumeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.upToolStripMenuItem1,
            this.downToolStripMenuItem1});
            this.volumeToolStripMenuItem.Name = "volumeToolStripMenuItem";
            this.volumeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.volumeToolStripMenuItem.Text = "Volume";
            // 
            // upToolStripMenuItem1
            // 
            this.upToolStripMenuItem1.Name = "upToolStripMenuItem1";
            this.upToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.upToolStripMenuItem1.Text = "+ Up";
            this.upToolStripMenuItem1.Click += new System.EventHandler(this.upToolStripMenuItem1_Click);
            // 
            // downToolStripMenuItem1
            // 
            this.downToolStripMenuItem1.Name = "downToolStripMenuItem1";
            this.downToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.downToolStripMenuItem1.Text = "- Down";
            this.downToolStripMenuItem1.Click += new System.EventHandler(this.downToolStripMenuItem1_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slotsToolStripMenuItem,
            this.rOMImageInfoToolStripMenuItem,
            this.toolStripSeparator32,
            this.showStatusToolStripMenuItem,
            this.showFPSToolStripMenuItem,
            this.toolStripSeparator27,
            this.controlProfilesToolStripMenuItem,
            this.configsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // slotsToolStripMenuItem
            // 
            this.slotsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10,
            this.toolStripSeparator25});
            this.slotsToolStripMenuItem.Name = "slotsToolStripMenuItem";
            this.slotsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.slotsToolStripMenuItem.Text = "Slots";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem2.Text = "1";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem3.Text = "2";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem4.Text = "3";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem5.Text = "4";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem6.Text = "5";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem7.Text = "6";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem8.Text = "7";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem9.Text = "8";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem10.Text = "9";
            // 
            // toolStripSeparator25
            // 
            this.toolStripSeparator25.Name = "toolStripSeparator25";
            this.toolStripSeparator25.Size = new System.Drawing.Size(77, 6);
            // 
            // rOMImageInfoToolStripMenuItem
            // 
            this.rOMImageInfoToolStripMenuItem.Name = "rOMImageInfoToolStripMenuItem";
            this.rOMImageInfoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rOMImageInfoToolStripMenuItem.Text = "ROM Image Info";
            this.rOMImageInfoToolStripMenuItem.Click += new System.EventHandler(this.rOMImageInfoToolStripMenuItem_Click);
            // 
            // toolStripSeparator32
            // 
            this.toolStripSeparator32.Name = "toolStripSeparator32";
            this.toolStripSeparator32.Size = new System.Drawing.Size(177, 6);
            // 
            // showStatusToolStripMenuItem
            // 
            this.showStatusToolStripMenuItem.Name = "showStatusToolStripMenuItem";
            this.showStatusToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showStatusToolStripMenuItem.Text = "Show Status";
            // 
            // showFPSToolStripMenuItem
            // 
            this.showFPSToolStripMenuItem.Name = "showFPSToolStripMenuItem";
            this.showFPSToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showFPSToolStripMenuItem.Text = "Show FPS";
            // 
            // toolStripSeparator27
            // 
            this.toolStripSeparator27.Name = "toolStripSeparator27";
            this.toolStripSeparator27.Size = new System.Drawing.Size(177, 6);
            // 
            // controlProfilesToolStripMenuItem
            // 
            this.controlProfilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem2,
            this.profilesToolStripMenuItem});
            this.controlProfilesToolStripMenuItem.Name = "controlProfilesToolStripMenuItem";
            this.controlProfilesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.controlProfilesToolStripMenuItem.Text = "Controllers";
            // 
            // configToolStripMenuItem2
            // 
            this.configToolStripMenuItem2.Name = "configToolStripMenuItem2";
            this.configToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.configToolStripMenuItem2.Text = "Setup";
            this.configToolStripMenuItem2.Click += new System.EventHandler(this.configToolStripMenuItem2_Click);
            // 
            // profilesToolStripMenuItem
            // 
            this.profilesToolStripMenuItem.Name = "profilesToolStripMenuItem";
            this.profilesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.profilesToolStripMenuItem.Text = "Profiles";
            this.profilesToolStripMenuItem.Click += new System.EventHandler(this.profilesToolStripMenuItem_Click);
            // 
            // configsToolStripMenuItem
            // 
            this.configsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.foldersToolStripMenuItem1,
            this.generalToolStripMenuItem1,
            this.videosToolStripMenuItem});
            this.configsToolStripMenuItem.Name = "configsToolStripMenuItem";
            this.configsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.configsToolStripMenuItem.Text = "Configs";
            // 
            // foldersToolStripMenuItem1
            // 
            this.foldersToolStripMenuItem1.Name = "foldersToolStripMenuItem1";
            this.foldersToolStripMenuItem1.Size = new System.Drawing.Size(188, 22);
            this.foldersToolStripMenuItem1.Text = "Folders...";
            this.foldersToolStripMenuItem1.Click += new System.EventHandler(this.foldersToolStripMenuItem1_Click);
            // 
            // generalToolStripMenuItem1
            // 
            this.generalToolStripMenuItem1.Name = "generalToolStripMenuItem1";
            this.generalToolStripMenuItem1.Size = new System.Drawing.Size(188, 22);
            this.generalToolStripMenuItem1.Text = "Application settings...";
            // 
            // videosToolStripMenuItem
            // 
            this.videosToolStripMenuItem.Name = "videosToolStripMenuItem";
            this.videosToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.videosToolStripMenuItem.Text = "Videos...";
            this.videosToolStripMenuItem.Click += new System.EventHandler(this.videosToolStripMenuItem_Click);
            // 
            // cycleFCEmulatorToolStripMenuItem
            // 
            this.cycleFCEmulatorToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cycleFCEmulatorToolStripMenuItem.Enabled = false;
            this.cycleFCEmulatorToolStripMenuItem.Name = "cycleFCEmulatorToolStripMenuItem";
            this.cycleFCEmulatorToolStripMenuItem.Size = new System.Drawing.Size(113, 20);
            this.cycleFCEmulatorToolStripMenuItem.Text = "CycleFC Emulator";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem1.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 383);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "mainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CycleFC (MainWindowEvent)";
            this.Activated += new System.EventHandler(this.Frm_main_Activated);
            this.Deactivate += new System.EventHandler(this.Frm_main_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_main_FormClosing);
            this.Load += new System.EventHandler(this.Frm_main_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Frm_main_KeyUp);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel_surface;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openROMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeROMToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
        private System.Windows.Forms.ToolStripMenuItem hardResetToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem softResetToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
        private System.Windows.Forms.ToolStripMenuItem consoleRegionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem slotsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator25;
        private System.Windows.Forms.ToolStripMenuItem rOMImageInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cycleFCEmulatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator26;
        private System.Windows.Forms.ToolStripMenuItem nTSCUSAJapanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pALEuropeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadStateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadStateAsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator28;
        private System.Windows.Forms.ToolStripMenuItem saveStateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveStateAsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator29;
        private System.Windows.Forms.ToolStripMenuItem saveSRAMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSRAMToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator30;
        private System.Windows.Forms.ToolStripMenuItem videoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullScreenModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeScreenshotToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator31;
        private System.Windows.Forms.ToolStripMenuItem windowSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x1ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem x2ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem x3ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem x4ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem x5ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem x6ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem stretchToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem paletteWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem audioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem audioChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem audioRecorderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator32;
        private System.Windows.Forms.ToolStripMenuItem controlProfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem profilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showFPSToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator27;
        private System.Windows.Forms.ToolStripMenuItem configsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem foldersToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem generalToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem videosToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator33;
        private System.Windows.Forms.ToolStripMenuItem volumeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem upToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem downToolStripMenuItem1;
    }
}

