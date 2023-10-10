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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CycleCore.Nes;
using CycleCore.Nes.Database;
using System.Reflection;

namespace CycleMain
{
    public partial class romInfoWindow : Form
    {
        public romInfoWindow(string RomPath)
        {
            InitializeComponent();
            if (Program.Form_Main.NES != null)
            {
                textBox1_Name.Text = Path.GetFileNameWithoutExtension(RomPath);
                textBox1_prgs.Text = Program.Form_Main.NES.Cartridge.PrgPages.ToString();
                textBox2_Mapper.Text = Program.Form_Main.NES.Cartridge.Mapper.ToString();
                textBox2_chr.Text = Program.Form_Main.NES.Cartridge.ChrPages.ToString();
                textBox3_mirroring.Text = Program.Form_Main.NES.Cartridge.Mirroring == Mirroring.ModeVert ? "Vertical" : "Horizontal";
                if (Program.Form_Main.NES.Cartridge.Mirroring == Mirroring.ModeFull)
                    textBox3_mirroring.Text = "Four Screen";
                checkBox1_saveram.Checked = Program.Form_Main.NES.Cartridge.HasSaveRam;
                checkBox2_trainer.Checked = Program.Form_Main.NES.Cartridge.HasTrainer;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            RichTextBox textBox = new RichTextBox();
            textBox.SelectAll();
            textBox.Copy();
            MessageBox.Show("Done !!");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            RichTextBox textBox = new RichTextBox();
            textBox.SelectAll();
            textBox.Copy();
            MessageBox.Show("Done !!");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            RichTextBox textBox = new RichTextBox();
            textBox.SelectAll();
            textBox.Copy();
            MessageBox.Show("Done !!");
        }
    }
}
