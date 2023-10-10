/*********************************************************************\
*This file is part of My Nes                                          *
*A Nintendo Entertainment System Emulator.                            *
*                                                                     *
*Copyright © Ala Hadid 2009 - 2011                                    *
*E-mail: mailto:ahdsoftwares@hotmail.com                              *
*                                                                     *
*My Nes is free software: you can redistribute it and/or modify       *
*it under the terms of the GNU General Public License as published by *
*the Free Software Foundation, either version 3 of the License, or    *
*(at your option) any later version.                                  *
*                                                                     *
*My Nes is distributed in the hope that it will be useful,            *
*but WITHOUT ANY WARRANTY; without even the implied warranty of       *
*MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the        *
*GNU General Public License for more details.                         *
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
using MyNes.Nes.Output.Video;

namespace MyNes
{
    public partial class Frm_D3DOptions : Form
    {
        VideoD3D _D3D;
        public Frm_D3DOptions(VideoD3D D3D)
        {
            _D3D = D3D;
            InitializeComponent();
            checkBox1.Checked = !Program.Settings.D3D_IsImmediate;
            checkBox2.Checked = Program.Settings.D3D_HideUpperScanlines;
            comboBox1.Items.Clear();
            for (int i = 0; i < _D3D.d3d.Adapters[0].GetDisplayModes(SlimDX.Direct3D9.Format.X8R8G8B8).Count; i++)
            {
                comboBox1.Items.Add(_D3D.d3d.Adapters[0].GetDisplayModes(SlimDX.Direct3D9.Format.X8R8G8B8)[i].Width + " x " + _D3D.d3d.Adapters[0].GetDisplayModes(SlimDX.Direct3D9.Format.X8R8G8B8)[i].Height + " " + _D3D.d3d.Adapters[0].GetDisplayModes(SlimDX.Direct3D9.Format.X8R8G8B8)[i].RefreshRate + " Hz");
            }
            try
            {

                comboBox1.SelectedIndex = Program.Settings.D3D_FullscreenResIndex;
            }
            catch
            {
                comboBox1.SelectedIndex = 0;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Program.Settings.D3D_FullscreenResIndex = comboBox1.SelectedIndex;
            Program.Settings.D3D_IsImmediate = !checkBox1.Checked;
            Program.Settings.D3D_HideUpperScanlines = checkBox2.Checked;
            Program.Settings.Save();
            _D3D.ApplySettings();
            this.Close();
        }
    }
}
