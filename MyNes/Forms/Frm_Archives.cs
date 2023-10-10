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
using System.IO;
using System.Windows.Forms;

namespace CycleMain
{
    public partial class Frm_Archives : Form
    {
        bool _OK;
        public bool OK
        { get { return _OK; } }
        public string SelectedRom
        { get { return listBox1.SelectedItem.ToString(); } }
        public Frm_Archives(string[] FILES)
        {
            InitializeComponent();
            for (int i = 0; i < FILES.Length; i++)
            {
                if (Path.GetExtension(FILES[i]).ToLower() == ".nes")
                {
                    listBox1.Items.Add(FILES[i]);
                }
            }
            listBox1.SelectedIndex = (listBox1.Items.Count > 0) ? 0 : -1;
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = listBox1.SelectedIndex >= 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _OK = true;
            Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            _OK = false;
            Close();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
                return;
            _OK = true;
            Close();
        }
    }
    public class Rom
    {
        string _Name = "";
        string _Path = "";
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }
    }
}
