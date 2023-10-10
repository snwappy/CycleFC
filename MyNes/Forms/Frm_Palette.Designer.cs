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
    partial class FormPalette
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPalette));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.radioButton1_useInternal = new System.Windows.Forms.RadioButton();
            this.radioButton2_useExternal = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton5_ntsc = new System.Windows.Forms.RadioButton();
            this.radioButton4_pal = new System.Windows.Forms.RadioButton();
            this.radioButton3_auto = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.hScrollBar_saturation = new System.Windows.Forms.HScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.hScrollBar_hue_tweak = new System.Windows.Forms.HScrollBar();
            this.label_gamma = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_bright = new System.Windows.Forms.Label();
            this.hScrollBar_contrast = new System.Windows.Forms.HScrollBar();
            this.label_const = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_hue = new System.Windows.Forms.Label();
            this.hScrollBar_brightness = new System.Windows.Forms.HScrollBar();
            this.label_satur = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.hScrollBar_gamma = new System.Windows.Forms.HScrollBar();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.radioButton1_useInternal);
            this.groupBox1.Controls.Add(this.radioButton2_useExternal);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 168);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(59, 140);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(255, 20);
            this.textBox1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 140);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 20);
            this.button1.TabIndex = 3;
            this.button1.Text = ".....";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // radioButton1_useInternal
            // 
            this.radioButton1_useInternal.AutoSize = true;
            this.radioButton1_useInternal.Location = new System.Drawing.Point(6, 19);
            this.radioButton1_useInternal.Name = "radioButton1_useInternal";
            this.radioButton1_useInternal.Size = new System.Drawing.Size(119, 17);
            this.radioButton1_useInternal.TabIndex = 0;
            this.radioButton1_useInternal.TabStop = true;
            this.radioButton1_useInternal.Text = "Use internal palette";
            this.radioButton1_useInternal.UseVisualStyleBackColor = true;
            this.radioButton1_useInternal.CheckedChanged += new System.EventHandler(this.radioButton1_useInternal_CheckedChanged);
            // 
            // radioButton2_useExternal
            // 
            this.radioButton2_useExternal.AutoSize = true;
            this.radioButton2_useExternal.Location = new System.Drawing.Point(6, 117);
            this.radioButton2_useExternal.Name = "radioButton2_useExternal";
            this.radioButton2_useExternal.Size = new System.Drawing.Size(97, 17);
            this.radioButton2_useExternal.TabIndex = 2;
            this.radioButton2_useExternal.TabStop = true;
            this.radioButton2_useExternal.Text = "Use palette file";
            this.toolTip1.SetToolTip(this.radioButton2_useExternal, "Use external palette from file, note that the basic palette (64 indexes, 192 byte" +
        "s)\r\nmay crash the games which uses Emphasis effects.");
            this.radioButton2_useExternal.UseVisualStyleBackColor = true;
            this.radioButton2_useExternal.CheckedChanged += new System.EventHandler(this.radioButton2_useExternal_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton5_ntsc);
            this.groupBox2.Controls.Add(this.radioButton4_pal);
            this.groupBox2.Controls.Add(this.radioButton3_auto);
            this.groupBox2.Location = new System.Drawing.Point(8, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(306, 88);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // radioButton5_ntsc
            // 
            this.radioButton5_ntsc.AutoSize = true;
            this.radioButton5_ntsc.Location = new System.Drawing.Point(6, 65);
            this.radioButton5_ntsc.Name = "radioButton5_ntsc";
            this.radioButton5_ntsc.Size = new System.Drawing.Size(128, 17);
            this.radioButton5_ntsc.TabIndex = 2;
            this.radioButton5_ntsc.TabStop = true;
            this.radioButton5_ntsc.Text = "Use the NTSC palette";
            this.toolTip1.SetToolTip(this.radioButton5_ntsc, "Always use the NTSC  palette even with PAL emulation");
            this.radioButton5_ntsc.UseVisualStyleBackColor = true;
            this.radioButton5_ntsc.CheckedChanged += new System.EventHandler(this.radioButton5_ntsc_CheckedChanged);
            // 
            // radioButton4_pal
            // 
            this.radioButton4_pal.AutoSize = true;
            this.radioButton4_pal.Location = new System.Drawing.Point(6, 42);
            this.radioButton4_pal.Name = "radioButton4_pal";
            this.radioButton4_pal.Size = new System.Drawing.Size(120, 17);
            this.radioButton4_pal.TabIndex = 1;
            this.radioButton4_pal.TabStop = true;
            this.radioButton4_pal.Text = "Use the PAL palette";
            this.toolTip1.SetToolTip(this.radioButton4_pal, "Always use the PAL palette even with NTSC emulation");
            this.radioButton4_pal.UseVisualStyleBackColor = true;
            this.radioButton4_pal.CheckedChanged += new System.EventHandler(this.radioButton4_pal_CheckedChanged);
            // 
            // radioButton3_auto
            // 
            this.radioButton3_auto.AutoSize = true;
            this.radioButton3_auto.Location = new System.Drawing.Point(6, 19);
            this.radioButton3_auto.Name = "radioButton3_auto";
            this.radioButton3_auto.Size = new System.Drawing.Size(141, 17);
            this.radioButton3_auto.TabIndex = 0;
            this.radioButton3_auto.TabStop = true;
            this.radioButton3_auto.Text = "Auto select (PAL, NTSC)";
            this.toolTip1.SetToolTip(this.radioButton3_auto, "My Nes will switch palette depending on rom region");
            this.radioButton3_auto.UseVisualStyleBackColor = true;
            this.radioButton3_auto.CheckedChanged += new System.EventHandler(this.radioButton3_auto_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(564, 412);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "&Ok";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(483, 412);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "&Cancel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(402, 412);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "&Defaults";
            this.toolTip1.SetToolTip(this.button4, "Reset settings to defaults");
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Location = new System.Drawing.Point(338, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(301, 394);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "View";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(295, 375);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.hScrollBar_saturation);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.hScrollBar_hue_tweak);
            this.groupBox4.Controls.Add(this.label_gamma);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label_bright);
            this.groupBox4.Controls.Add(this.hScrollBar_contrast);
            this.groupBox4.Controls.Add(this.label_const);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label_hue);
            this.groupBox4.Controls.Add(this.hScrollBar_brightness);
            this.groupBox4.Controls.Add(this.label_satur);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.hScrollBar_gamma);
            this.groupBox4.Location = new System.Drawing.Point(12, 186);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(320, 191);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "NTSC Palette Levels";
            // 
            // hScrollBar_saturation
            // 
            this.hScrollBar_saturation.Location = new System.Drawing.Point(72, 22);
            this.hScrollBar_saturation.Maximum = 5000;
            this.hScrollBar_saturation.Name = "hScrollBar_saturation";
            this.hScrollBar_saturation.Size = new System.Drawing.Size(205, 21);
            this.hScrollBar_saturation.TabIndex = 5;
            this.hScrollBar_saturation.Value = 1000;
            this.hScrollBar_saturation.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_saturation_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Saturation";
            // 
            // hScrollBar_hue_tweak
            // 
            this.hScrollBar_hue_tweak.Location = new System.Drawing.Point(72, 56);
            this.hScrollBar_hue_tweak.Maximum = 1000;
            this.hScrollBar_hue_tweak.Minimum = -1000;
            this.hScrollBar_hue_tweak.Name = "hScrollBar_hue_tweak";
            this.hScrollBar_hue_tweak.Size = new System.Drawing.Size(205, 21);
            this.hScrollBar_hue_tweak.TabIndex = 7;
            this.hScrollBar_hue_tweak.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_hue_tweak_Scroll);
            // 
            // label_gamma
            // 
            this.label_gamma.AutoSize = true;
            this.label_gamma.Location = new System.Drawing.Point(280, 162);
            this.label_gamma.Name = "label_gamma";
            this.label_gamma.Size = new System.Drawing.Size(23, 13);
            this.label_gamma.TabIndex = 19;
            this.label_gamma.Text = "1.8";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Hue";
            // 
            // label_bright
            // 
            this.label_bright.AutoSize = true;
            this.label_bright.Location = new System.Drawing.Point(280, 128);
            this.label_bright.Name = "label_bright";
            this.label_bright.Size = new System.Drawing.Size(13, 13);
            this.label_bright.TabIndex = 18;
            this.label_bright.Text = "1";
            // 
            // hScrollBar_contrast
            // 
            this.hScrollBar_contrast.Location = new System.Drawing.Point(72, 90);
            this.hScrollBar_contrast.Maximum = 2000;
            this.hScrollBar_contrast.Minimum = 500;
            this.hScrollBar_contrast.Name = "hScrollBar_contrast";
            this.hScrollBar_contrast.Size = new System.Drawing.Size(205, 21);
            this.hScrollBar_contrast.TabIndex = 9;
            this.hScrollBar_contrast.Value = 1000;
            this.hScrollBar_contrast.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_contrast_Scroll);
            // 
            // label_const
            // 
            this.label_const.AutoSize = true;
            this.label_const.Location = new System.Drawing.Point(280, 94);
            this.label_const.Name = "label_const";
            this.label_const.Size = new System.Drawing.Size(13, 13);
            this.label_const.TabIndex = 17;
            this.label_const.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Contrast";
            // 
            // label_hue
            // 
            this.label_hue.AutoSize = true;
            this.label_hue.Location = new System.Drawing.Point(280, 60);
            this.label_hue.Name = "label_hue";
            this.label_hue.Size = new System.Drawing.Size(13, 13);
            this.label_hue.TabIndex = 16;
            this.label_hue.Text = "0";
            // 
            // hScrollBar_brightness
            // 
            this.hScrollBar_brightness.Location = new System.Drawing.Point(72, 124);
            this.hScrollBar_brightness.Maximum = 2000;
            this.hScrollBar_brightness.Minimum = 500;
            this.hScrollBar_brightness.Name = "hScrollBar_brightness";
            this.hScrollBar_brightness.Size = new System.Drawing.Size(205, 21);
            this.hScrollBar_brightness.TabIndex = 11;
            this.hScrollBar_brightness.Value = 1000;
            this.hScrollBar_brightness.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_brightness_Scroll);
            // 
            // label_satur
            // 
            this.label_satur.AutoSize = true;
            this.label_satur.Location = new System.Drawing.Point(280, 26);
            this.label_satur.Name = "label_satur";
            this.label_satur.Size = new System.Drawing.Size(13, 13);
            this.label_satur.TabIndex = 15;
            this.label_satur.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Brightness";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Gamma";
            // 
            // hScrollBar_gamma
            // 
            this.hScrollBar_gamma.Location = new System.Drawing.Point(72, 158);
            this.hScrollBar_gamma.Maximum = 2500;
            this.hScrollBar_gamma.Minimum = 1000;
            this.hScrollBar_gamma.Name = "hScrollBar_gamma";
            this.hScrollBar_gamma.Size = new System.Drawing.Size(205, 21);
            this.hScrollBar_gamma.TabIndex = 13;
            this.hScrollBar_gamma.Value = 1800;
            this.hScrollBar_gamma.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_gamma_Scroll);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(12, 412);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(125, 23);
            this.button7.TabIndex = 22;
            this.button7.Text = "Save Palette As .Pal";
            this.toolTip1.SetToolTip(this.button7, "Save the palette (NTSC palette) into .pal file");
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(208, 383);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(59, 23);
            this.button6.TabIndex = 21;
            this.button6.Text = "Flat";
            this.toolTip1.SetToolTip(this.button6, "Flat levels");
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(273, 383);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(59, 23);
            this.button5.TabIndex = 20;
            this.button5.Text = "Reset";
            this.toolTip1.SetToolTip(this.button5, "Reset levels to My Nes default");
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(77, 383);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(59, 23);
            this.button8.TabIndex = 22;
            this.button8.Text = "Save";
            this.toolTip1.SetToolTip(this.button8, "Save present file which saves level values");
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(12, 383);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(59, 23);
            this.button9.TabIndex = 23;
            this.button9.Text = "Load";
            this.toolTip1.SetToolTip(this.button9, "Load present file");
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // FormPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(649, 447);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormPalette";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Palette";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton1_useInternal;
        private System.Windows.Forms.RadioButton radioButton2_useExternal;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioButton5_ntsc;
        private System.Windows.Forms.RadioButton radioButton4_pal;
        private System.Windows.Forms.RadioButton radioButton3_auto;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.HScrollBar hScrollBar_saturation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.HScrollBar hScrollBar_hue_tweak;
        private System.Windows.Forms.Label label_gamma;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_bright;
        private System.Windows.Forms.HScrollBar hScrollBar_contrast;
        private System.Windows.Forms.Label label_const;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_hue;
        private System.Windows.Forms.HScrollBar hScrollBar_brightness;
        private System.Windows.Forms.Label label_satur;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.HScrollBar hScrollBar_gamma;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}