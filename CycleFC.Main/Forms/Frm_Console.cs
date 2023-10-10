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
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MyNes.Nes;

namespace MyNes
{
    public partial class Frm_Console : Form
    {
        public Frm_Console()
        {
            InitializeComponent();
            this.Location = Program.Settings.DebuggerLocation;
            this.Size = Program.Settings.DebuggerSize;
            CONSOLE.DebugRised += new EventHandler<DebugArg>(DEBUG_DebugRised);
            checkBox1.Checked = Program.Settings.RunConsole;
            ClearDebugger();
        }
        private delegate void WriteDebugLineDelegate(string Text, DebugStatus State);
        void WriteDebugLine(string Text, DebugStatus State)
        {
            if (!this.InvokeRequired)
            {
                this.WriteLine(Text, State);
            }
            else
            {
                this.Invoke(new WriteDebugLineDelegate(WriteLine), new object[] { Text, State });
            }
        }
        void DEBUG_DebugRised(object sender, DebugArg e)
        {
            WriteDebugLine(e.DebugLine, e.Status);
        }
        public void SaveSettings()
        {
            Program.Settings.DebuggerLocation = this.Location;
            Program.Settings.DebuggerSize = this.Size;
            Program.Settings.Save();
        }
        public void WriteLine(string Text, DebugStatus State)
        {
            try
            {
                if (State == DebugStatus.Notification)
                    return;
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                switch (State)
                {
                    case DebugStatus.None:
                        richTextBox1.SelectionColor = Color.White;
                        break;
                    case DebugStatus.Cool:
                        richTextBox1.SelectionColor = Color.Green;
                        break;
                    case DebugStatus.Error:
                        richTextBox1.SelectionColor = Color.Red;
                        break;
                    case DebugStatus.Warning:
                        richTextBox1.SelectionColor = Color.Yellow;
                        break;
                }
                richTextBox1.SelectedText = Text + "\n";
                richTextBox1.SelectionColor = Color.Green;
                richTextBox1.ScrollToCaret();
            }
            catch { }
        }
        public void WriteLine(string Text)
        { WriteLine(Text, DebugStatus.None); }
        public void ClearDebugger()
        {
            richTextBox1.Text = "";
            WriteLine("Welcome to My Nes console");
            WriteLine(@"Write ""Help"" to see a list of instructions.");
            CONSOLE.WriteSeparateLine(this, DebugStatus.None);
        }

        void DoInstruction(string Instruction)
        {
            WriteLine("] " + Instruction);
            bool found = false;
            foreach (string item in comboBox1.Items)
            {
                if (item.ToLower() == Instruction.ToLower())
                { found = true; break; }
            }
            if (!found)
                comboBox1.Items.Add(Instruction);
            switch (Instruction.ToLower())
            {
                case "cl d": ClearDebugger(); break;
                case "exit": this.Close(); break;
                case "help": Help(); break;
                case "commandlines":
                    WriteLine("List of the available command lines:\n" +
                                    "* -cm : show this list !!\n" +
                                    "* -reset : reset My Nes to it's default settings\n" +
                                    "* -ls x : load the state from slot number x (x = slot number from 1 to 9)\n" +
                                    "* -lsa <StateFilePath> : load a state file at the path <StateFilePath>\n" +
                                    "* -ss x : select the state slot number x (x = slot number from 1 to 9)\n" +
                                    "* -pal : select the PAL region\n" +
                                    "* -ntsc : select the NTSC region\n" +
                                    "* -st x : Enable / Disable no limiter (x=0 disable, x=1 enable)\n" +
                                    "* -s x : switch the size (x=x1 use size X1 '256x240', x=x2 use size X2 '512x480', x=str use Stretch)\n" +
                                    "* -r <WavePath> : record the audio wave into <WavePath>\n" +
                                    "* -p <PaletteFilePath> : load the palette <PaletteFilePath> and use it\n" +
                                    "* -w_size w h : resize the window (W=width, h=height)\n" +
                                    "* -w_pos x y : move the window into a specific location (x=X coordinate, y=Y coordinate)\n" +
                                    "* -w_max : maximize the window\n" +
                                    "* -w_min : minimize the window");
                    break;
                case "mappers":
                    WriteLine("Supported mappers:");
                    string[] mappers = MapperManager.SupportedMappers;
                    foreach (string i in mappers)
                        WriteLine(i);
                    break;
                case "romsha1":
                    if (Program.Form_Main.NES != null)
                    {
                        WriteLine("SHA1 =" + Program.Form_Main.NES.Cartridge.Sha1, DebugStatus.Cool);
                    }
                    else
                    {
                        WriteLine("SYSTEM IS OFF", DebugStatus.Error);
                    }
                    break;
                case "settings_reset":
                    Program.Settings.Reset();
                    WriteLine("Settings reset successfuly, please restart My Nes to apply.", DebugStatus.Cool);
                    break;
                case "sr":
                    if (Program.Form_Main.NES != null)
                    {
                        Program.Form_Main.NES.Cpu.SoftReset();
                    }
                    else
                    {
                        WriteLine("SYSTEM IS OFF", DebugStatus.Error);
                    }
                    break;
                case "hr":
                    if (Program.Form_Main.NES != null)
                    {
                        Program.Form_Main.OpenRom(Program.Form_Main.NES.Cartridge.FilePath, true);
                    }
                    else
                    {
                        WriteLine("SYSTEM IS OFF", DebugStatus.Error);
                    }
                    break;
                case "off":
                    if (Program.Form_Main.NES != null)
                    {
                        Program.Form_Main.turnOffToolStripMenuItem_Click(this, null);
                    }
                    else
                    {
                        WriteLine("SYSTEM IS OFF", DebugStatus.Error);
                    }
                    break;
                case "save":
                    SaveFileDialog sav = new SaveFileDialog();
                    sav.Title = "Save console lines to file";
                    sav.Filter = "TEXT FILE (*.txt)|*.txt";
                    if (sav.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        if (Path.GetExtension(sav.FileName).ToLower() == ".txt")
                        {
                            File.WriteAllLines(sav.FileName, richTextBox1.Lines);
                        }
                        else
                        {
                            richTextBox1.SaveFile(sav.FileName, RichTextBoxStreamType.RichText);
                        }
                    }
                    break;
                case "rom":
                    if (Program.Form_Main.NES != null)
                    {
                        WriteLine("Rom info:", DebugStatus.None);
                        string st =
                        "PRG count: " + Program.Form_Main.NES.Cartridge.PrgPages.ToString() + "\n" +
                        "CHR count: " + Program.Form_Main.NES.Cartridge.ChrPages.ToString() + "\n" +
                        "Mapper: #" + Program.Form_Main.NES.Cartridge.Mapper.ToString() + " [" + Program.Form_Main.NES.Mapper.Name + "]\n";
                        WriteLine(st, DebugStatus.Cool);
                    }
                    else
                    {
                        WriteLine("SYSTEM IS OFF", DebugStatus.Error);
                    }
                    break;
                case "r":
                    WriteLine("Cpu registers:", DebugStatus.None);
                    if (Program.Form_Main.NES != null)
                    {
                        string st =
                            "PC: $" + string.Format("{0:X}", Program.Form_Main.NES.Cpu.RegPC) + "\n"
                            + "A: $" + string.Format("{0:X}", Program.Form_Main.NES.Cpu.RegA) + "\n"
                            + "X: $" + string.Format("{0:X}", Program.Form_Main.NES.Cpu.RegX) + "\n"
                            + "Y: $" + string.Format("{0:X}", Program.Form_Main.NES.Cpu.RegY) + "\n"
                            + "S: $" + string.Format("{0:X}", Program.Form_Main.NES.Cpu.RegS) + "\n"
                            + "P: $" + string.Format("{0:X}", Program.Form_Main.NES.Cpu.RegSR);
                        WriteLine(st, DebugStatus.Cool);
                        WriteLine("Flags", DebugStatus.Cool);
                        st =
                            "C : " + (Program.Form_Main.NES.Cpu.FlagC ? "1" : "0") + "\n" +
                            "D : " + (Program.Form_Main.NES.Cpu.FlagD ? "1" : "0") + "\n" +
                            "I : " + (Program.Form_Main.NES.Cpu.FlagI ? "1" : "0") + "\n" +
                            "N : " + (Program.Form_Main.NES.Cpu.FlagN ? "1" : "0") + "\n" +
                            "V : " + (Program.Form_Main.NES.Cpu.FlagV ? "1" : "0") + "\n" +
                            "Z : " + (Program.Form_Main.NES.Cpu.FlagZ ? "1" : "0");
                        WriteLine(st, DebugStatus.Cool);
                    }
                    else
                    {
                        WriteLine("SYSTEM IS OFF", DebugStatus.Error);
                    }
                    break;
                case "cl sram":
                    if (Program.Form_Main.NES != null)
                    {
                        WriteLine("Clearing the S-RAM ....", DebugStatus.Warning);
                        Program.Form_Main.NES.CpuMemory.srm = new byte[0x2000];
                        WriteLine("Done.", DebugStatus.Cool);
                    }
                    else
                    {
                        WriteLine("SYSTEM IS OFF", DebugStatus.Error);
                    }
                    break;
                default://Memory instructions handled here
                    try
                    {
                        if (Instruction == "")
                        { WriteLine("Enter something !!", DebugStatus.Warning); break; }
                        else if (Instruction.ToLower().Substring(0, 2) == "w ")
                        {
                            if (Program.Form_Main.NES == null)
                                WriteLine("SYSTEM IS OFF", DebugStatus.Error);
                            try
                            {
                                ushort ADDRESS = Convert.ToUInt16(Instruction.ToLower().Substring(3, 4), 16);
                                byte VALUE = Convert.ToByte(Instruction.ToLower().Substring(9, 2), 16);
                                Program.Form_Main.NES.CpuMemory[ADDRESS] = VALUE;
                                WriteLine("Done.", DebugStatus.Cool);
                            }
                            catch { WriteLine("Bad address or value.", DebugStatus.Error); }
                        }
                        else if (Instruction.ToLower().Substring(0, 2) == "m ")
                        {
                            if (Program.Form_Main.NES == null)
                                WriteLine("SYSTEM IS OFF", DebugStatus.Error);
                            try
                            {
                                ushort ADDRESS = Convert.ToUInt16(Instruction.ToLower().Substring(3, 4), 16);
                                byte VALUE = Program.Form_Main.NES.CpuMemory[ADDRESS];
                                WriteLine("Done, the value = $" + string.Format("{0:X}", VALUE), DebugStatus.Cool);
                            }
                            catch { WriteLine("Bad address.", DebugStatus.Error); }
                        }
                        else if (Instruction.ToLower().Substring(0, 3) == "mr ")
                        {
                            if (Program.Form_Main.NES == null)
                                WriteLine("SYSTEM IS OFF", DebugStatus.Error);
                            try
                            {
                                ushort ADDRESS = Convert.ToUInt16(Instruction.ToLower().Substring(4, 4), 16);
                                int countt = Convert.ToInt32(Instruction.ToLower().Substring(9, Instruction.ToLower().Length - 9));
                                for (int i = 0; i < countt; i++)
                                {
                                    byte VALUE = Program.Form_Main.NES.CpuMemory[(ushort)(ADDRESS + i)];
                                    WriteLine("$" + string.Format("{0:X}", (ADDRESS + i)) + " : $" + string.Format("{0:X}", VALUE), DebugStatus.Cool);
                                }
                            }
                            catch { WriteLine("Bad address or range out of memory.", DebugStatus.Error); }
                        }
                        else if (Instruction.ToLower().Substring(0, 4) == "snt ")//switch nametable
                        {
                            if (Program.Form_Main.NES == null)
                                WriteLine("SYSTEM IS OFF", DebugStatus.Error);
                            try
                            {
                                byte start = Convert.ToByte(Instruction.ToLower().Substring(4, 1));
                                byte area = Convert.ToByte(Instruction.ToLower().Substring(6, 1));
                                Program.Form_Main.NES.PpuMemory.nmtBank[area] = start;
                                WriteLine("Selecting NT " + start + " at area 0x2" + string.Format("{0:X}", (area * 4)) + "00", DebugStatus.Warning);
                                WriteLine("Done.", DebugStatus.Cool);
                            }
                            catch { WriteLine("Bad address or value.", DebugStatus.Error); }
                        }
                        else
                            WriteLine("Bad command ...", DebugStatus.Warning); break;
                    }
                    catch
                    { WriteLine("Bad command ...", DebugStatus.Warning); break; }
            }
            try
            {
                comboBox1.SelectAll();
            }
            catch { }
        }

        void Help()
        {
            WriteLine("Instructions list :", DebugStatus.Cool);
            WriteLine("  > exit : Close the console");
            WriteLine("  > help : Display this list !!");
            WriteLine("  > rom : Show the current rom info.");
            WriteLine("  > r : Dump the cpu registers");
            WriteLine("  > w <address> <value> : Set a value into the memory at the specific address.");
            WriteLine("  The address and the value must be in hex starting with $");
            WriteLine("  e.g : w $1F23 $10");
            WriteLine("  > m <address> : Dumps the memory at the specific address.");
            WriteLine("  > mr <StartAddress> <Count> : Dumps the specified memory range.");
            WriteLine("  The address must be in hex starting with $, count must be dec");
            WriteLine("  e.g : mr $1F23 1000");
            WriteLine("  > cl d : Clear debugger");
            WriteLine("  > cl sram : Clear S-RAM");
            WriteLine("  > snt <nt> <area> : Switch a nametable at <area>. ");
            WriteLine("  e.g : 'snt 0 1'= switch nt0 at 0x2400");
            WriteLine("  > romsha1 : Show rom file SHA1");
            WriteLine("  > sr : Soft reset");
            WriteLine("  > hr : Hard reset");
            WriteLine("  > off : Turn off nes");
            WriteLine("  > save : Save console text to file");
            WriteLine("  > settings_reset : Reset My Nes settings to default values.");
            WriteLine("  > commandlines : Show list of available commandlines for My Nes");
            WriteLine("  > mappers : Show list of supported mappers by My Nes");

            WriteLine("Instructions are NOT case sensitive.");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Lines.Length > 0)
                richTextBox1.SelectedText = richTextBox1.Lines[richTextBox1.Lines.Length - 1];
            DoInstruction(comboBox1.Text);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ClearDebugger();
        }

        private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            comboBox1.SelectAll();
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (richTextBox1.Lines.Length > 0)
                    richTextBox1.SelectedText = richTextBox1.Lines[richTextBox1.Lines.Length - 1];
                DoInstruction(comboBox1.Text);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.RunConsole = checkBox1.Checked;
        }
    }
}
