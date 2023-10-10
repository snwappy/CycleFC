using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MyNes.Windows
{
    public partial class AboutDiag : Form
    {
        public AboutDiag()
        {
            InitializeComponent();
        }

        private void AboutDiag_Load(object sender, EventArgs e)
        {
            Version dotNetVersion = Environment.Version;
            VersionString.Text = "1.0.0.1-dev " + $" | .NET Runtime version: {dotNetVersion}";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string emailAddress = "mailto:ahdsoftwares@hotmail.com";
            Process.Start(new ProcessStartInfo
            {
                FileName = emailAddress,
                UseShellExecute = true
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://github.com/snwappy/";
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "http://www.gnu.org/licenses/";
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
    }
}
