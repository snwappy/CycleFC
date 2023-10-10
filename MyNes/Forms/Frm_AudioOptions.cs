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
using System.Windows.Forms;

namespace CycleMain
{
    public partial class Frm_AudioOptions : Form
    {
        bool _Ok;
        public bool OK
        { get { return _Ok; } }
        public Frm_AudioOptions()
        {
            InitializeComponent();
            checkBox1.Checked = Program.Settings.SoundEnabled;
            checkBox2.Checked = Program.Settings.Sq1Enabled;
            checkBox3.Checked = Program.Settings.Sq2Enabled;
            checkBox4.Checked = Program.Settings.NoiEnabled;
            checkBox5.Checked = Program.Settings.TriEnabled;
            checkBox6.Checked = Program.Settings.DpmEnabled;
            checkBox7.Checked = Program.Settings.Vrc6Sq1Enabled;
            checkBox8.Checked = Program.Settings.Vrc6Sq2Enabled;
            checkBox9.Checked = Program.Settings.Vrc6SawEnabled;
            checkBox10.Checked = Program.Settings.Mmc5Sq1Enabled;
            checkBox11.Checked = Program.Settings.Mmc5Sq2Enabled;
            checkBox12.Checked = Program.Settings.Mmc5DpmEnabled;
            checkBox15_fme71.Checked = Program.Settings.SS5BWv1Enabled;
            checkBox14_fme72.Checked = Program.Settings.SS5BWv2Enabled;
            checkBox13_fme73.Checked = Program.Settings.SS5BWv3Enabled;
            trackBar1.Value = Program.Settings.Volume;

            label1.Text = ((((100 * (3000 - trackBar1.Value)) / 3000) - 200) * -1).ToString() + " %";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.Settings.SoundEnabled = checkBox1.Checked;
            Program.Settings.Sq1Enabled = checkBox2.Checked;
            Program.Settings.Sq2Enabled = checkBox3.Checked;
            Program.Settings.NoiEnabled = checkBox4.Checked;
            Program.Settings.TriEnabled = checkBox5.Checked;
            Program.Settings.DpmEnabled = checkBox6.Checked;
            Program.Settings.Vrc6Sq1Enabled = checkBox7.Checked;
            Program.Settings.Vrc6Sq2Enabled = checkBox8.Checked;
            Program.Settings.Vrc6SawEnabled = checkBox9.Checked;
            Program.Settings.Mmc5Sq1Enabled = checkBox10.Checked;
            Program.Settings.Mmc5Sq2Enabled = checkBox11.Checked;
            Program.Settings.Mmc5DpmEnabled = checkBox12.Checked;
            Program.Settings.SS5BWv1Enabled = checkBox15_fme71.Checked;
            Program.Settings.SS5BWv2Enabled = checkBox14_fme72.Checked;
            Program.Settings.SS5BWv3Enabled = checkBox13_fme73.Checked;
            Program.Settings.Volume = trackBar1.Value;
            Program.Settings.Save();
            _Ok = true;
            Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            _Ok = false;
            Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            checkBox6.Checked = true;
            checkBox7.Checked = true;
            checkBox8.Checked = true;
            checkBox9.Checked = true;
            checkBox10.Checked = true;
            checkBox11.Checked = true;
            checkBox12.Checked = true;
            checkBox15_fme71.Checked = true;
            checkBox14_fme72.Checked = true;
            checkBox13_fme73.Checked = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            checkBox11.Checked = false;
            checkBox12.Checked = false;
            checkBox15_fme71.Checked = false;
            checkBox14_fme72.Checked = false;
            checkBox13_fme73.Checked = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = ((((100 * (3000 - trackBar1.Value)) / 3000) - 200) * -1).ToString() + " %";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = checkBox1.Checked;
        }
    }
}
