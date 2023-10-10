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
using System.Linq;
using System.Windows.Forms;
using System.IO;
using MyNes.Nes;
using MyNes.Nes.Database;
namespace MyNes
{
    static class Program
    {
        static mainWindow _mainForm;
        static Properties.Settings _settings ;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _settings = new Properties.Settings();
            _settings.Reload();

            BuildControlProfile();
            CheckOutFolders();

            MapperManager.Cache();

            if (File.Exists(".\\database.xml"))
                NesDatabase.LoadDatabase(new FileStream(".\\database.xml", FileMode.Open, FileAccess.Read));

            _mainForm = new mainWindow(Args);

            Application.Run(_mainForm);
        }
        /// <summary>
        /// Get the application settings class
        /// </summary>
        public static Properties.Settings Settings
        { get { return _settings; } }

        public static void BuildControlProfile()
        {
            if (Program.Settings.ControlProfiles == null)
            {
                Program.Settings.ControlProfiles = new ControlProfilesCollection();
                Program.Settings.CurrentControlProfile = new ControlProfile();
                Program.Settings.CurrentControlProfile.Name = "<Default>";
                Program.Settings.ControlProfiles.Add(Program.Settings.CurrentControlProfile);
                Program.Settings.Save();
            }
            else if (Program.Settings.ControlProfiles.Count == 0)//Make the compiler happy
            {
                Program.Settings.ControlProfiles = new ControlProfilesCollection();
                Program.Settings.CurrentControlProfile = new ControlProfile();
                Program.Settings.CurrentControlProfile.Name = "<Default>";
                Program.Settings.ControlProfiles.Add(Program.Settings.CurrentControlProfile);
                Program.Settings.Save();
            }
        }
        static void CheckOutFolders()
        {
            try
            {
                if (_settings.StatesFolder.Substring(0, 2) == @".\")
                {
                    _settings.StatesFolder = Path.GetFullPath(_settings.StatesFolder);
                }
                if (_settings.ImagesFolder.Substring(0, 2) == @".\")
                {
                    _settings.ImagesFolder = Path.GetFullPath(_settings.ImagesFolder);
                }

                //create folders
                Directory.CreateDirectory(_settings.StatesFolder);
                Directory.CreateDirectory(_settings.ImagesFolder);
            }
            catch { }
        }

        /// <summary>
        /// Get file size
        /// </summary>
        /// <param name="FilePath">Full path of the file</param>
        /// <returns>Return file size + unit lable</returns>
        public static string GetFileSize(string FilePath)
        {
            if (File.Exists(Path.GetFullPath(FilePath)) == true)
            {
                FileInfo Info = new FileInfo(FilePath);
                string Unit = " Byte";
                double Len = Info.Length;
                if (Info.Length >= 1024)
                {
                    Len = Info.Length / 1024.00;
                    Unit = " KB";
                }
                if (Len >= 1024)
                {
                    Len /= 1024.00;
                    Unit = " MB";
                }
                if (Len >= 1024)
                {
                    Len /= 1024.00;
                    Unit = " GB";
                }
                return Len.ToString("F2") + Unit;
            }
            return "";
        }

        public static mainWindow Form_Main
        { get { return _mainForm; } }
    }
}
