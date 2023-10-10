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
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MyNes.Nes;

namespace MyNes
{
    public partial class FormPalette : Form
    {
        bool _Ok;
        public bool OK
        { get { return _Ok; } }
        unsafe void ShowPalette(int[] PaletteFormat)
        {
            Graphics GR = panel1.CreateGraphics();
            GR.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            int y = 0;
            int x = 0;
            int w = 16;
            int H = 10;
            for (int j = 0; j < PaletteFormat.Length; j++)
            {
                y = (j / 16) * H;
                x = (j % w) * w;
                Bitmap bmp = new Bitmap(w, H);
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, w, H),
                    ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
                int* numPtr = (int*)bmpData.Scan0;
                for (int i = 0; i < w * H; i++)
                {
                    numPtr[i] = PaletteFormat[j];
                }
                bmp.UnlockBits(bmpData);
                GR.DrawImage(bmp, x, y, w, H);
            }
        }
        public FormPalette()
        {
            InitializeComponent();
            //Load the settings
            if (Program.Settings.PaletteFormat == null)
                Program.Settings.PaletteFormat = new PaletteFormat();
            radioButton1_useInternal.Checked = Program.Settings.PaletteFormat.UseInternalPalette;
            radioButton2_useExternal.Checked = !Program.Settings.PaletteFormat.UseInternalPalette;
            groupBox2.Enabled = Program.Settings.PaletteFormat.UseInternalPalette;
            button1.Enabled = !Program.Settings.PaletteFormat.UseInternalPalette;
            textBox1.Enabled = !Program.Settings.PaletteFormat.UseInternalPalette;
            switch (Program.Settings.PaletteFormat.UseInternalPaletteMode)
            {
                case MyNes.Nes.UseInternalPaletteMode.Auto:
                    radioButton3_auto.Checked = true;
                    radioButton4_pal.Checked = false;
                    radioButton5_ntsc.Checked = false;
                    break;
                case MyNes.Nes.UseInternalPaletteMode.NTSC:
                    radioButton3_auto.Checked = false;
                    radioButton4_pal.Checked = false;
                    radioButton5_ntsc.Checked = true;
                    break;
                case MyNes.Nes.UseInternalPaletteMode.PAL:
                    radioButton3_auto.Checked = false;
                    radioButton4_pal.Checked = true;
                    radioButton5_ntsc.Checked = false;
                    break;
            }
            textBox1.Text = Program.Settings.PaletteFormat.ExternalPalettePath;
            //Load values
            hScrollBar_brightness.Value = (int)(Program.Settings.PaletteFormat.Brightness * 1000);
            hScrollBar_contrast.Value = (int)(Program.Settings.PaletteFormat.Contrast * 1000);
            hScrollBar_gamma.Value = (int)(Program.Settings.PaletteFormat.Gamma * 1000);
            hScrollBar_hue_tweak.Value = (int)(Program.Settings.PaletteFormat.Hue * 1000);
            hScrollBar_saturation.Value = (int)(Program.Settings.PaletteFormat.Saturation * 1000);

            label_bright.Text = Program.Settings.PaletteFormat.Brightness.ToString("F3");
            label_const.Text = Program.Settings.PaletteFormat.Contrast.ToString("F3");
            label_gamma.Text = Program.Settings.PaletteFormat.Gamma.ToString("F3");
            label_hue.Text = Program.Settings.PaletteFormat.Hue.ToString("F3");
            label_satur.Text = Program.Settings.PaletteFormat.Saturation.ToString("F3");
            //Apply to generator
            NTSCPaletteGenerator.brightness = Program.Settings.PaletteFormat.Brightness;
            NTSCPaletteGenerator.contrast = Program.Settings.PaletteFormat.Contrast;
            NTSCPaletteGenerator.gamma = Program.Settings.PaletteFormat.Gamma;
            NTSCPaletteGenerator.hue_tweak = Program.Settings.PaletteFormat.Hue;
            NTSCPaletteGenerator.saturation = Program.Settings.PaletteFormat.Saturation;
            //generate ntsc palette
            NesPalette.NTSCPalette = NTSCPaletteGenerator.GeneratePalette();
        }
        private void radioButton1_useInternal_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3_auto.Checked)
                ShowPalette(new int[64]);
            groupBox2.Enabled = radioButton1_useInternal.Checked;
            button1.Enabled = !radioButton1_useInternal.Checked;
            textBox1.Enabled = !radioButton1_useInternal.Checked;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Open PaletteFormat file";
            op.Filter = "PaletteFormat file (*.pal)|*.pal;*.PAL";
            if (op.ShowDialog(this) == DialogResult.OK)
            {
                if (NesPalette.LoadPalette(new FileStream(op.FileName, FileMode.Open, FileAccess.Read)) != null)
                {
                    textBox1.Text = op.FileName;
                    ShowPalette(NesPalette.LoadPalette(new FileStream(op.FileName, FileMode.Open, FileAccess.Read)));
                }
                else
                {
                    MessageBox.Show("Can't load this PaletteFormat file !!");
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            _Ok = false;
            this.Close();
        }
        //Save
        private void button2_Click(object sender, EventArgs e)
        {
            Program.Settings.PaletteFormat.UseInternalPalette = radioButton1_useInternal.Checked;
            Program.Settings.PaletteFormat.ExternalPalettePath = textBox1.Text;
            //Save values
            float v = hScrollBar_brightness.Value;
            Program.Settings.PaletteFormat.Brightness = (v / 1000);
            v = hScrollBar_contrast.Value;
            Program.Settings.PaletteFormat.Contrast = (v / 1000);
            v = hScrollBar_gamma.Value;
            Program.Settings.PaletteFormat.Gamma = (v / 1000);
            v = hScrollBar_hue_tweak.Value;
            Program.Settings.PaletteFormat.Hue = (v / 1000);
            v = hScrollBar_saturation.Value;
            Program.Settings.PaletteFormat.Saturation = (v / 1000);

            if (radioButton3_auto.Checked)
                Program.Settings.PaletteFormat.UseInternalPaletteMode = UseInternalPaletteMode.Auto;
            else if (radioButton4_pal.Checked)
                Program.Settings.PaletteFormat.UseInternalPaletteMode = UseInternalPaletteMode.PAL;
            else if (radioButton5_ntsc.Checked)
                Program.Settings.PaletteFormat.UseInternalPaletteMode = UseInternalPaletteMode.NTSC;
            Program.Settings.Save();
            _Ok = true;
            this.Close();
        }

        private void radioButton4_pal_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4_pal.Checked)
                ShowPalette(NesPalette.PALPalette);
        }
        private void radioButton5_ntsc_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5_ntsc.Checked)
                ShowPalette(NesPalette.NTSCPalette);
            groupBox4.Enabled = radioButton3_auto.Checked | radioButton5_ntsc.Checked;
        }
        private void radioButton2_useExternal_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2_useExternal.Checked)
            {
                try
                {
                    if (NesPalette.LoadPalette(new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read)) != null)
                        ShowPalette(NesPalette.LoadPalette(new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read)));
                }
                catch
                {
                    ShowPalette(new int[512]);
                }
            }
            groupBox4.Enabled = radioButton3_auto.Checked | radioButton5_ntsc.Checked;
        }
        private void radioButton3_auto_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3_auto.Checked)
                ShowPalette(NesPalette.NTSCPalette);
            groupBox4.Enabled = radioButton3_auto.Checked | radioButton5_ntsc.Checked;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Program.Settings.PaletteFormat = new PaletteFormat();
            //RELoad the settings
            radioButton1_useInternal.Checked = Program.Settings.PaletteFormat.UseInternalPalette;
            radioButton2_useExternal.Checked = !Program.Settings.PaletteFormat.UseInternalPalette;
            groupBox2.Enabled = Program.Settings.PaletteFormat.UseInternalPalette;
            button1.Enabled = !Program.Settings.PaletteFormat.UseInternalPalette;
            textBox1.Enabled = !Program.Settings.PaletteFormat.UseInternalPalette;
            switch (Program.Settings.PaletteFormat.UseInternalPaletteMode)
            {
                case MyNes.Nes.UseInternalPaletteMode.Auto:
                    radioButton3_auto.Checked = true;
                    radioButton4_pal.Checked = false;
                    radioButton5_ntsc.Checked = false;
                    break;
                case MyNes.Nes.UseInternalPaletteMode.NTSC:
                    radioButton3_auto.Checked = false;
                    radioButton4_pal.Checked = false;
                    radioButton5_ntsc.Checked = true;
                    break;
                case MyNes.Nes.UseInternalPaletteMode.PAL:
                    radioButton3_auto.Checked = false;
                    radioButton4_pal.Checked = true;
                    radioButton5_ntsc.Checked = false;
                    break;
            }
            textBox1.Text = Program.Settings.PaletteFormat.ExternalPalettePath;
            button5_Click(this, null);//reset values
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            switch (Program.Settings.PaletteFormat.UseInternalPaletteMode)
            {
                case MyNes.Nes.UseInternalPaletteMode.Auto:
                    ShowPalette(NesPalette.NTSCPalette);
                    break;
                case MyNes.Nes.UseInternalPaletteMode.NTSC:
                    ShowPalette(NesPalette.NTSCPalette);
                    break;
                case MyNes.Nes.UseInternalPaletteMode.PAL:
                    ShowPalette(NesPalette.PALPalette);
                    break;
            }
        }
        private void hScrollBar_saturation_Scroll(object sender, ScrollEventArgs e)
        {
            float v = hScrollBar_saturation.Value;
            NTSCPaletteGenerator.saturation = (v / 1000);
            label_satur.Text = NTSCPaletteGenerator.saturation.ToString("F3");
            NesPalette.NTSCPalette = NTSCPaletteGenerator.GeneratePalette();
            ShowPalette(NesPalette.NTSCPalette);
            if (Program.Form_Main.NES != null)
                Program.Form_Main.NES.Ppu.Palette = NesPalette.NTSCPalette;
        }
        private void hScrollBar_hue_tweak_Scroll(object sender, ScrollEventArgs e)
        {
            float v = hScrollBar_hue_tweak.Value;
            NTSCPaletteGenerator.hue_tweak = (v / 1000);
            label_hue.Text = NTSCPaletteGenerator.hue_tweak.ToString("F3");
            NesPalette.NTSCPalette = NTSCPaletteGenerator.GeneratePalette();
            ShowPalette(NesPalette.NTSCPalette);
            if (Program.Form_Main.NES != null)
                Program.Form_Main.NES.Ppu.Palette = NesPalette.NTSCPalette;
        }
        private void hScrollBar_contrast_Scroll(object sender, ScrollEventArgs e)
        {
            float v = hScrollBar_contrast.Value;
            NTSCPaletteGenerator.contrast = (v / 1000);
            label_const.Text = NTSCPaletteGenerator.contrast.ToString("F3");
            NesPalette.NTSCPalette = NTSCPaletteGenerator.GeneratePalette();
            ShowPalette(NesPalette.NTSCPalette);
            if (Program.Form_Main.NES != null)
                Program.Form_Main.NES.Ppu.Palette = NesPalette.NTSCPalette;
        }
        private void hScrollBar_brightness_Scroll(object sender, ScrollEventArgs e)
        {
            float v = hScrollBar_brightness.Value;
            NTSCPaletteGenerator.brightness = (v / 1000);
            label_bright.Text = NTSCPaletteGenerator.brightness.ToString("F3");
            NesPalette.NTSCPalette = NTSCPaletteGenerator.GeneratePalette();
            ShowPalette(NesPalette.NTSCPalette);
            if (Program.Form_Main.NES != null)
                Program.Form_Main.NES.Ppu.Palette = NesPalette.NTSCPalette;
        }
        private void hScrollBar_gamma_Scroll(object sender, ScrollEventArgs e)
        {
            float v = hScrollBar_gamma.Value;
            NTSCPaletteGenerator.gamma = (v / 1000);
            label_gamma.Text = NTSCPaletteGenerator.gamma.ToString("F3");
            NesPalette.NTSCPalette = NTSCPaletteGenerator.GeneratePalette();
            ShowPalette(NesPalette.NTSCPalette);
            if (Program.Form_Main.NES != null)
                Program.Form_Main.NES.Ppu.Palette = NesPalette.NTSCPalette;
        }
        //reset
        private void button5_Click(object sender, EventArgs e)
        {
            NTSCPaletteGenerator.saturation = 1.977f;
            hScrollBar_saturation.Value = (int)(1.977f * 1000);
            NTSCPaletteGenerator.hue_tweak = 0.187f;
            hScrollBar_hue_tweak.Value = (int)(0.187f * 1000);
            NTSCPaletteGenerator.contrast = 0.878f;
            hScrollBar_contrast.Value = (int)(0.878f * 1000);
            NTSCPaletteGenerator.brightness = 1.149f;
            hScrollBar_brightness.Value = (int)(1.149f * 1000);
            NTSCPaletteGenerator.gamma = 1.697f;
            hScrollBar_gamma.Value = (int)(1.697f * 1000);

            label_satur.Text = NTSCPaletteGenerator.saturation.ToString("F3");
            label_hue.Text = NTSCPaletteGenerator.hue_tweak.ToString("F3");
            label_const.Text = NTSCPaletteGenerator.contrast.ToString("F3");
            label_bright.Text = NTSCPaletteGenerator.brightness.ToString("F3");
            label_gamma.Text = NTSCPaletteGenerator.gamma.ToString("F3");

            NesPalette.NTSCPalette = NTSCPaletteGenerator.GeneratePalette();
            ShowPalette(NesPalette.NTSCPalette);
            if (Program.Form_Main.NES != null)
                Program.Form_Main.NES.Ppu.Palette = NesPalette.NTSCPalette;
        }
        //Flat
        private void button6_Click(object sender, EventArgs e)
        {
            NTSCPaletteGenerator.saturation = 1.0f;
            hScrollBar_saturation.Value = 1000;
            NTSCPaletteGenerator.hue_tweak = 0.0f;
            hScrollBar_hue_tweak.Value = 0;
            NTSCPaletteGenerator.contrast = 1.0f;
            hScrollBar_contrast.Value = 1000;
            NTSCPaletteGenerator.brightness = 1.0f;
            hScrollBar_brightness.Value = 1000;
            NTSCPaletteGenerator.gamma = 1.8f;
            hScrollBar_gamma.Value = 1800;

            label_satur.Text = NTSCPaletteGenerator.saturation.ToString("F3");
            label_hue.Text = NTSCPaletteGenerator.hue_tweak.ToString("F3");
            label_const.Text = NTSCPaletteGenerator.contrast.ToString("F3");
            label_bright.Text = NTSCPaletteGenerator.brightness.ToString("F3");
            label_gamma.Text = NTSCPaletteGenerator.gamma.ToString("F3");

            NesPalette.NTSCPalette = NTSCPaletteGenerator.GeneratePalette();
            ShowPalette(NesPalette.NTSCPalette);
            if (Program.Form_Main.NES != null)
                Program.Form_Main.NES.Ppu.Palette = NesPalette.NTSCPalette;
        }
        //Save as pal
        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog sav = new SaveFileDialog();
            sav.Filter = "Palette file '64 indexes' (*.pal)|*.pal|Palette file '512 indexes' (*.pal)|*.pal";
            if (sav.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                //get pallete
                List<byte> palette = new List<byte>();

                for (int i = 0; i < ((sav.FilterIndex == 1) ? 64 : 512); i++)
                {
                    int color = NesPalette.NTSCPalette[i];
                    palette.Add((byte)((color  >> 16) & 0xFF));//Red
                    palette.Add((byte)((color >> 8) & 0xFF));//Green
                    palette.Add((byte)((color >> 0) & 0xFF));//Blue
                }

                Stream str = new FileStream(sav.FileName, FileMode.Create, FileAccess.Write);
                str.Write(palette.ToArray(), 0, palette.Count);
                str.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SaveFileDialog sav = new SaveFileDialog();
            sav.Filter = "My Nes Palette Present (*.mnpp)|*.mnpp";
            if (sav.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                List<string> lines = new List<string>();
                lines.Add("Brightness=" + NTSCPaletteGenerator.brightness);
                lines.Add("Contrast=" + NTSCPaletteGenerator.contrast);
                lines.Add("Gamma=" + NTSCPaletteGenerator.gamma);
                lines.Add("Hue=" + NTSCPaletteGenerator.hue_tweak);
                lines.Add("Saturation=" + NTSCPaletteGenerator.saturation);
                File.WriteAllLines(sav.FileName, lines.ToArray());
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "My Nes Palette Present (*.mnpp)|*.mnpp";
            if (op.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(op.FileName);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] pars = lines[i].Split(new char[] {'=' });
                    switch (pars[0])
                    {
                        case "Brightness": 
                            NTSCPaletteGenerator.brightness = float.Parse(pars[1]);
                            hScrollBar_brightness.Value = (int)(NTSCPaletteGenerator.brightness * 1000);
                            break;
                        case "Contrast":
                            NTSCPaletteGenerator.contrast = float.Parse(pars[1]);
                            hScrollBar_contrast.Value = (int)(NTSCPaletteGenerator.contrast * 1000);
                            break;
                        case "Gamma":
                            NTSCPaletteGenerator.gamma = float.Parse(pars[1]);
                            hScrollBar_gamma.Value = (int)(NTSCPaletteGenerator.gamma * 1000);
                            break;
                        case "Hue":
                            NTSCPaletteGenerator.hue_tweak = float.Parse(pars[1]);
                            hScrollBar_hue_tweak.Value = (int)(NTSCPaletteGenerator.hue_tweak * 1000);
                            break;
                        case "Saturation":
                            NTSCPaletteGenerator.saturation = float.Parse(pars[1]);
                            hScrollBar_saturation.Value = (int)(NTSCPaletteGenerator.saturation * 1000);
                            break;
                    }
                }
                label_bright.Text = NTSCPaletteGenerator.brightness.ToString("F3");
                label_const.Text = NTSCPaletteGenerator.contrast.ToString("F3");
                label_gamma.Text = NTSCPaletteGenerator.gamma.ToString("F3");
                label_hue.Text = NTSCPaletteGenerator.hue_tweak.ToString("F3");
                label_satur.Text = NTSCPaletteGenerator.saturation.ToString("F3");
            }
            NesPalette.NTSCPalette = NTSCPaletteGenerator.GeneratePalette();
            ShowPalette(NesPalette.NTSCPalette);
            if (Program.Form_Main.NES != null)
                Program.Form_Main.NES.Ppu.Palette = NesPalette.NTSCPalette;
        }
    }
}
