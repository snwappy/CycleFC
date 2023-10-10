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
                if (Program.Form_Main.NES.Cartridge.DataBaseInfo.Game_Name != null)
                {
                    //Game info
                    FieldInfo[] Fields = typeof(NesDatabaseGameInfo).GetFields(BindingFlags.Public
                    | BindingFlags.Instance);
                    bool ColoOr = false;
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        if (Fields[i].FieldType == typeof(System.String))
                        {
                            listView1.Items.Add(Fields[i].Name.Replace("_", " "));
                            try
                            {
                                listView1.Items[listView1.Items.Count - 1].SubItems.Add(Fields[i].GetValue(Program.Form_Main.NES.Cartridge.DataBaseInfo).ToString());
                            }
                            catch
                            {
                                listView1.Items[listView1.Items.Count - 1].SubItems.Add("");
                            }
                            if (ColoOr)
                                listView1.Items[listView1.Items.Count - 1].BackColor = Color.WhiteSmoke;
                            ColoOr = !ColoOr;
                        }
                    }
                    //chips
                    for (int i = 0; i < Program.Form_Main.NES.Cartridge.DataBaseInfo.chip_type.Count; i++)
                    {
                        listView1.Items.Add("Chip " + (i + 1));
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add(Program.Form_Main.NES.Cartridge.DataBaseInfo.chip_type[i]);
                        if (ColoOr)
                            listView1.Items[listView1.Items.Count - 1].BackColor = Color.WhiteSmoke;
                        ColoOr = !ColoOr;
                    }
                    //Cartidge
                    Fields = typeof(NesDatabaseCartridgeInfo).GetFields(BindingFlags.Public
                    | BindingFlags.Instance);
                    ColoOr = false;
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        if (Fields[i].FieldType == typeof(System.String))
                        {
                            listView2.Items.Add(Fields[i].Name.Replace("_", " "));
                            try
                            {
                                listView2.Items[listView2.Items.Count - 1].SubItems.Add(Fields[i].GetValue(Program.Form_Main.NES.Cartridge.DataBaseCartInfo).ToString());
                            }
                            catch
                            {
                                listView2.Items[listView2.Items.Count - 1].SubItems.Add("");
                            }
                            if (ColoOr)
                                listView2.Items[listView2.Items.Count - 1].BackColor = Color.WhiteSmoke;
                            ColoOr = !ColoOr;
                        }
                    }
                    //DataBase
                    Fields = typeof(NesDatabase).GetFields(BindingFlags.Public
                  | BindingFlags.Static);
                    ColoOr = false;
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        if (Fields[i].FieldType == typeof(System.String))
                        {
                            listView3.Items.Add(Fields[i].Name.Remove(0, 2));
                            try
                            {
                                listView3.Items[listView3.Items.Count - 1].SubItems.Add(Fields[i].GetValue(Program.Form_Main.NES.Cartridge.DataBaseCartInfo).ToString());
                            }
                            catch
                            {
                                listView3.Items[listView3.Items.Count - 1].SubItems.Add("");
                            }
                            if (ColoOr)
                                listView3.Items[listView3.Items.Count - 1].BackColor = Color.WhiteSmoke;
                            ColoOr = !ColoOr;
                        }
                    }
                }
                else
                {
                    groupBox1.Enabled = false;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            RichTextBox textBox = new RichTextBox();
            foreach (ListViewItem item in listView1.Items)
            {
                textBox.Text += item.Text + ": " + item.SubItems[1].Text + "\n";
            }
            textBox.SelectAll();
            textBox.Copy();
            MessageBox.Show("Done !!");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            RichTextBox textBox = new RichTextBox();
            foreach (ListViewItem item in listView2.Items)
            {
                textBox.Text += item.Text + ": " + item.SubItems[1].Text + "\n";
            }
            textBox.SelectAll();
            textBox.Copy();
            MessageBox.Show("Done !!");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            RichTextBox textBox = new RichTextBox();
            foreach (ListViewItem item in listView3.Items)
            {
                textBox.Text += item.Text + ": " + item.SubItems[1].Text + "\n";
            }
            textBox.SelectAll();
            textBox.Copy();
            MessageBox.Show("Done !!");
        }
    }
}
