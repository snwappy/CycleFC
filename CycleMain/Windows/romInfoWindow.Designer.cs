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
namespace CycleMain
{
    partial class romInfoWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new System.Windows.Forms.Label();
            textBox1_Name = new System.Windows.Forms.TextBox();
            textBox2_Mapper = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            textBox1_prgs = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            textBox2_chr = new System.Windows.Forms.TextBox();
            label4_chrs = new System.Windows.Forms.Label();
            textBox3_mirroring = new System.Windows.Forms.TextBox();
            label5_mirroring = new System.Windows.Forms.Label();
            checkBox1_saveram = new System.Windows.Forms.CheckBox();
            checkBox2_trainer = new System.Windows.Forms.CheckBox();
            button1 = new System.Windows.Forms.Button();
            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(7, 25);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(48, 15);
            label1.TabIndex = 0;
            label1.Text = "Name : ";
            // 
            // textBox1_Name
            // 
            textBox1_Name.BackColor = System.Drawing.SystemColors.Control;
            textBox1_Name.Location = new System.Drawing.Point(76, 22);
            textBox1_Name.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox1_Name.Name = "textBox1_Name";
            textBox1_Name.ReadOnly = true;
            textBox1_Name.Size = new System.Drawing.Size(208, 23);
            textBox1_Name.TabIndex = 1;
            // 
            // textBox2_Mapper
            // 
            textBox2_Mapper.BackColor = System.Drawing.SystemColors.Control;
            textBox2_Mapper.Location = new System.Drawing.Point(360, 22);
            textBox2_Mapper.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox2_Mapper.Name = "textBox2_Mapper";
            textBox2_Mapper.ReadOnly = true;
            textBox2_Mapper.Size = new System.Drawing.Size(188, 23);
            textBox2_Mapper.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(292, 25);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(57, 15);
            label2.TabIndex = 2;
            label2.Text = "Mapper : ";
            // 
            // textBox1_prgs
            // 
            textBox1_prgs.BackColor = System.Drawing.SystemColors.Control;
            textBox1_prgs.Location = new System.Drawing.Point(76, 52);
            textBox1_prgs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox1_prgs.Name = "textBox1_prgs";
            textBox1_prgs.ReadOnly = true;
            textBox1_prgs.Size = new System.Drawing.Size(72, 23);
            textBox1_prgs.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(7, 55);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(43, 15);
            label3.TabIndex = 4;
            label3.Text = "PRG's :";
            // 
            // textBox2_chr
            // 
            textBox2_chr.BackColor = System.Drawing.SystemColors.Control;
            textBox2_chr.Location = new System.Drawing.Point(211, 52);
            textBox2_chr.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox2_chr.Name = "textBox2_chr";
            textBox2_chr.ReadOnly = true;
            textBox2_chr.Size = new System.Drawing.Size(73, 23);
            textBox2_chr.TabIndex = 7;
            // 
            // label4_chrs
            // 
            label4_chrs.AutoSize = true;
            label4_chrs.Location = new System.Drawing.Point(155, 55);
            label4_chrs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4_chrs.Name = "label4_chrs";
            label4_chrs.Size = new System.Drawing.Size(45, 15);
            label4_chrs.TabIndex = 6;
            label4_chrs.Text = "CHR's :";
            // 
            // textBox3_mirroring
            // 
            textBox3_mirroring.BackColor = System.Drawing.SystemColors.Control;
            textBox3_mirroring.Location = new System.Drawing.Point(360, 52);
            textBox3_mirroring.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox3_mirroring.Name = "textBox3_mirroring";
            textBox3_mirroring.ReadOnly = true;
            textBox3_mirroring.Size = new System.Drawing.Size(188, 23);
            textBox3_mirroring.TabIndex = 9;
            // 
            // label5_mirroring
            // 
            label5_mirroring.AutoSize = true;
            label5_mirroring.Location = new System.Drawing.Point(292, 55);
            label5_mirroring.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5_mirroring.Name = "label5_mirroring";
            label5_mirroring.Size = new System.Drawing.Size(66, 15);
            label5_mirroring.TabIndex = 8;
            label5_mirroring.Text = "Mirroring : ";
            // 
            // checkBox1_saveram
            // 
            checkBox1_saveram.AutoSize = true;
            checkBox1_saveram.Enabled = false;
            checkBox1_saveram.Location = new System.Drawing.Point(153, 82);
            checkBox1_saveram.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox1_saveram.Name = "checkBox1_saveram";
            checkBox1_saveram.Size = new System.Drawing.Size(115, 19);
            checkBox1_saveram.TabIndex = 10;
            checkBox1_saveram.Text = "Is Battery Backed";
            checkBox1_saveram.UseVisualStyleBackColor = true;
            // 
            // checkBox2_trainer
            // 
            checkBox2_trainer.AutoSize = true;
            checkBox2_trainer.Enabled = false;
            checkBox2_trainer.Location = new System.Drawing.Point(76, 82);
            checkBox2_trainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox2_trainer.Name = "checkBox2_trainer";
            checkBox2_trainer.Size = new System.Drawing.Size(61, 19);
            checkBox2_trainer.TabIndex = 11;
            checkBox2_trainer.Text = "Trainer";
            checkBox2_trainer.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(426, 137);
            button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(144, 35);
            button1.TabIndex = 12;
            button1.Text = "&Close";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(textBox1_Name);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(textBox2_Mapper);
            groupBox2.Controls.Add(checkBox2_trainer);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(checkBox1_saveram);
            groupBox2.Controls.Add(textBox1_prgs);
            groupBox2.Controls.Add(textBox3_mirroring);
            groupBox2.Controls.Add(label4_chrs);
            groupBox2.Controls.Add(label5_mirroring);
            groupBox2.Controls.Add(textBox2_chr);
            groupBox2.Location = new System.Drawing.Point(14, 14);
            groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Size = new System.Drawing.Size(556, 117);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "iNES ROM header";
            // 
            // romInfoWindow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(580, 180);
            ControlBox = false;
            Controls.Add(groupBox2);
            Controls.Add(button1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "romInfoWindow";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "ROM information";
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1_Name;
        private System.Windows.Forms.TextBox textBox2_Mapper;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1_prgs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2_chr;
        private System.Windows.Forms.Label label4_chrs;
        private System.Windows.Forms.TextBox textBox3_mirroring;
        private System.Windows.Forms.Label label5_mirroring;
        private System.Windows.Forms.CheckBox checkBox1_saveram;
        private System.Windows.Forms.CheckBox checkBox2_trainer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}