﻿/*********************************************************************\r
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CycleMain
{
    public partial class generalOptionsWindow : Form
    {
        public generalOptionsWindow()
        {
            InitializeComponent();
            switch (Program.Settings.ImagesFormat)
            {
                case ".bmp":
                    radioButton1.Checked = true;
                    break;
                case ".jpg":
                    radioButton2.Checked = true;
                    break;
                case ".gif":
                    radioButton3.Checked = true;
                    break;
                case ".png":
                    radioButton4.Checked = true;
                    break;
                case ".tiff":
                    radioButton5.Checked = true;
                    break;
            }
            checkBox1_sramsave.Checked = Program.Settings.AutoSaveSRAM;
            checkBox1_pause.Checked = Program.Settings.PauseWhenFocusLost;
            checkBox_FadeIn.Checked = Program.Settings.EnableFadeInAnimation;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            checkBox1_pause.Checked = true;
            checkBox1_sramsave.Checked = true;
        }
        //save
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            { Program.Settings.ImagesFormat = ".bmp"; }
            if (radioButton2.Checked)
            { Program.Settings.ImagesFormat = ".jpg"; }
            if (radioButton3.Checked)
            { Program.Settings.ImagesFormat = ".gif"; }
            if (radioButton4.Checked)
            { Program.Settings.ImagesFormat = ".png"; }
            if (radioButton5.Checked)
            { Program.Settings.ImagesFormat = ".tiff"; }
            Program.Settings.AutoSaveSRAM = checkBox1_sramsave.Checked;
            Program.Settings.PauseWhenFocusLost = checkBox1_pause.Checked;
            Program.Settings.EnableFadeInAnimation = checkBox_FadeIn.Checked;
            Program.Settings.Save();
            this.Close();
        }

        private void generalOptionsWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
