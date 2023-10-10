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
    partial class controlSettingsWindow
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
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            Player1_Down = new System.Windows.Forms.Button();
            Player1_Right = new System.Windows.Forms.Button();
            Player1_Select = new System.Windows.Forms.Button();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            Player1_Left = new System.Windows.Forms.Button();
            Player1_Up = new System.Windows.Forms.Button();
            Player1_Start = new System.Windows.Forms.Button();
            Player1_B = new System.Windows.Forms.Button();
            Player1_A = new System.Windows.Forms.Button();
            tabPage2 = new System.Windows.Forms.TabPage();
            Player2_Select = new System.Windows.Forms.Button();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            Player2_Down = new System.Windows.Forms.Button();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            Player2_Right = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            Player2_Left = new System.Windows.Forms.Button();
            Player2_Up = new System.Windows.Forms.Button();
            Player2_Start = new System.Windows.Forms.Button();
            Player2_B = new System.Windows.Forms.Button();
            Player2_A = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            comboBox1 = new System.Windows.Forms.ComboBox();
            linkLabel3 = new System.Windows.Forms.LinkLabel();
            linkLabel2 = new System.Windows.Forms.LinkLabel();
            linkLabel1 = new System.Windows.Forms.LinkLabel();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new System.Drawing.Point(14, 74);
            tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(441, 335);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            tabPage1.Controls.Add(Player1_Down);
            tabPage1.Controls.Add(Player1_Right);
            tabPage1.Controls.Add(Player1_Select);
            tabPage1.Controls.Add(label9);
            tabPage1.Controls.Add(label10);
            tabPage1.Controls.Add(label11);
            tabPage1.Controls.Add(label12);
            tabPage1.Controls.Add(label13);
            tabPage1.Controls.Add(label14);
            tabPage1.Controls.Add(label15);
            tabPage1.Controls.Add(label16);
            tabPage1.Controls.Add(Player1_Left);
            tabPage1.Controls.Add(Player1_Up);
            tabPage1.Controls.Add(Player1_Start);
            tabPage1.Controls.Add(Player1_B);
            tabPage1.Controls.Add(Player1_A);
            tabPage1.Location = new System.Drawing.Point(4, 24);
            tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Size = new System.Drawing.Size(433, 307);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Player 1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // Player1_Down
            // 
            Player1_Down.Location = new System.Drawing.Point(65, 132);
            Player1_Down.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Player1_Down.Name = "Player1_Down";
            Player1_Down.Size = new System.Drawing.Size(358, 27);
            Player1_Down.TabIndex = 6;
            Player1_Down.Text = "button4";
            Player1_Down.UseVisualStyleBackColor = true;
            Player1_Down.Click += Player1_Down_Click;
            // 
            // Player1_Right
            // 
            Player1_Right.Location = new System.Drawing.Point(65, 65);
            Player1_Right.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Player1_Right.Name = "Player1_Right";
            Player1_Right.Size = new System.Drawing.Size(358, 27);
            Player1_Right.TabIndex = 5;
            Player1_Right.Text = "button4";
            Player1_Right.UseVisualStyleBackColor = true;
            Player1_Right.Click += Player1_Right_Click;
            // 
            // Player1_Select
            // 
            Player1_Select.Location = new System.Drawing.Point(65, 265);
            Player1_Select.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Player1_Select.Name = "Player1_Select";
            Player1_Select.Size = new System.Drawing.Size(358, 27);
            Player1_Select.TabIndex = 3;
            Player1_Select.Text = "button4";
            Player1_Select.UseVisualStyleBackColor = true;
            Player1_Select.Click += Player1_Select_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label9.Location = new System.Drawing.Point(7, 270);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(56, 14);
            label9.TabIndex = 31;
            label9.Text = "Select : ";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label10.Location = new System.Drawing.Point(7, 237);
            label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(51, 14);
            label10.TabIndex = 30;
            label10.Text = "Start : ";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label11.Location = new System.Drawing.Point(7, 203);
            label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(27, 14);
            label11.TabIndex = 29;
            label11.Text = "B : ";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label12.Location = new System.Drawing.Point(7, 170);
            label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(28, 14);
            label12.TabIndex = 28;
            label12.Text = "A : ";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label13.Location = new System.Drawing.Point(7, 136);
            label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(55, 14);
            label13.TabIndex = 27;
            label13.Text = "Down : ";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label14.Location = new System.Drawing.Point(7, 103);
            label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(35, 14);
            label14.TabIndex = 26;
            label14.Text = "Up : ";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label15.Location = new System.Drawing.Point(7, 69);
            label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(53, 14);
            label15.TabIndex = 25;
            label15.Text = "Right : ";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label16.Location = new System.Drawing.Point(7, 36);
            label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(44, 14);
            label16.TabIndex = 24;
            label16.Text = "Left : ";
            // 
            // Player1_Left
            // 
            Player1_Left.Location = new System.Drawing.Point(65, 31);
            Player1_Left.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Player1_Left.Name = "Player1_Left";
            Player1_Left.Size = new System.Drawing.Size(358, 27);
            Player1_Left.TabIndex = 7;
            Player1_Left.Text = "button4";
            Player1_Left.UseVisualStyleBackColor = true;
            Player1_Left.Click += Player1_Left_Click;
            // 
            // Player1_Up
            // 
            Player1_Up.Location = new System.Drawing.Point(65, 98);
            Player1_Up.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Player1_Up.Name = "Player1_Up";
            Player1_Up.Size = new System.Drawing.Size(358, 27);
            Player1_Up.TabIndex = 4;
            Player1_Up.Text = "button4";
            Player1_Up.UseVisualStyleBackColor = true;
            Player1_Up.Click += Player1_Up_Click;
            // 
            // Player1_Start
            // 
            Player1_Start.Location = new System.Drawing.Point(65, 232);
            Player1_Start.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Player1_Start.Name = "Player1_Start";
            Player1_Start.Size = new System.Drawing.Size(358, 27);
            Player1_Start.TabIndex = 2;
            Player1_Start.Text = "button4";
            Player1_Start.UseVisualStyleBackColor = true;
            Player1_Start.Click += Player1_Start_Click;
            // 
            // Player1_B
            // 
            Player1_B.Location = new System.Drawing.Point(65, 198);
            Player1_B.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Player1_B.Name = "Player1_B";
            Player1_B.Size = new System.Drawing.Size(358, 27);
            Player1_B.TabIndex = 1;
            Player1_B.Text = "button4";
            Player1_B.UseVisualStyleBackColor = true;
            Player1_B.Click += Player1_B_Click;
            // 
            // Player1_A
            // 
            Player1_A.Location = new System.Drawing.Point(65, 165);
            Player1_A.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Player1_A.Name = "Player1_A";
            Player1_A.Size = new System.Drawing.Size(358, 27);
            Player1_A.TabIndex = 0;
            Player1_A.Text = "button4";
            Player1_A.UseVisualStyleBackColor = true;
            Player1_A.Click += Player1_A_Click;
            // 
            // tabPage2
            // 
            tabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            tabPage2.Controls.Add(Player2_Select);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(Player2_Down);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(Player2_Right);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(Player2_Left);
            tabPage2.Controls.Add(Player2_Up);
            tabPage2.Controls.Add(Player2_Start);
            tabPage2.Controls.Add(Player2_B);
            tabPage2.Controls.Add(Player2_A);
            tabPage2.Location = new System.Drawing.Point(4, 24);
            tabPage2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage2.Size = new System.Drawing.Size(433, 307);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Player 2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // Player2_Select
            // 
            Player2_Select.Location = new System.Drawing.Point(65, 265);
            Player2_Select.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Player2_Select.Name = "Player2_Select";
            Player2_Select.Size = new System.Drawing.Size(358, 27);
            Player2_Select.TabIndex = 11;
            Player2_Select.Text = "button4";
            Player2_Select.UseVisualStyleBackColor = true;
            Player2_Select.Click += Player2_Select_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label8.Location = new System.Drawing.Point(7, 270);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(56, 14);
            label8.TabIndex = 23;
            label8.Text = "Select : ";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label7.Location = new System.Drawing.Point(7, 237);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(51, 14);
            label7.TabIndex = 22;
            label7.Text = "Start : ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label6.Location = new System.Drawing.Point(7, 203);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(27, 14);
            label6.TabIndex = 21;
            label6.Text = "B : ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label5.Location = new System.Drawing.Point(7, 170);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(28, 14);
            label5.TabIndex = 20;
            label5.Text = "A : ";
            // 
            // Player2_Down
            // 
            Player2_Down.Location = new System.Drawing.Point(65, 132);
            Player2_Down.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Player2_Down.Name = "Player2_Down";
            Player2_Down.Size = new System.Drawing.Size(358, 27);
            Player2_Down.TabIndex = 14;
            Player2_Down.Text = "button4";
            Player2_Down.UseVisualStyleBackColor = true;
            Player2_Down.Click += Player2_Down_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label4.Location = new System.Drawing.Point(7, 136);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(55, 14);
            label4.TabIndex = 19;
            label4.Text = "Down : ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label3.Location = new System.Drawing.Point(7, 103);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(35, 14);
            label3.TabIndex = 18;
            label3.Text = "Up : ";
            // 
            // Player2_Right
            // 
            Player2_Right.Location = new System.Drawing.Point(65, 65);
            Player2_Right.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Player2_Right.Name = "Player2_Right";
            Player2_Right.Size = new System.Drawing.Size(358, 27);
            Player2_Right.TabIndex = 13;
            Player2_Right.Text = "button4";
            Player2_Right.UseVisualStyleBackColor = true;
            Player2_Right.Click += Player2_Right_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label2.Location = new System.Drawing.Point(7, 69);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(53, 14);
            label2.TabIndex = 17;
            label2.Text = "Right : ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(7, 36);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(44, 14);
            label1.TabIndex = 16;
            label1.Text = "Left : ";
            // 
            // Player2_Left
            // 
            Player2_Left.Location = new System.Drawing.Point(65, 31);
            Player2_Left.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Player2_Left.Name = "Player2_Left";
            Player2_Left.Size = new System.Drawing.Size(358, 27);
            Player2_Left.TabIndex = 15;
            Player2_Left.Text = "button4";
            Player2_Left.UseVisualStyleBackColor = true;
            Player2_Left.Click += Player2_Left_Click;
            // 
            // Player2_Up
            // 
            Player2_Up.Location = new System.Drawing.Point(65, 98);
            Player2_Up.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Player2_Up.Name = "Player2_Up";
            Player2_Up.Size = new System.Drawing.Size(358, 27);
            Player2_Up.TabIndex = 12;
            Player2_Up.Text = "button4";
            Player2_Up.UseVisualStyleBackColor = true;
            Player2_Up.Click += Player2_Up_Click;
            // 
            // Player2_Start
            // 
            Player2_Start.Location = new System.Drawing.Point(65, 232);
            Player2_Start.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Player2_Start.Name = "Player2_Start";
            Player2_Start.Size = new System.Drawing.Size(358, 27);
            Player2_Start.TabIndex = 10;
            Player2_Start.Text = "button4";
            Player2_Start.UseVisualStyleBackColor = true;
            Player2_Start.Click += Player2_Start_Click;
            // 
            // Player2_B
            // 
            Player2_B.Location = new System.Drawing.Point(65, 198);
            Player2_B.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Player2_B.Name = "Player2_B";
            Player2_B.Size = new System.Drawing.Size(358, 27);
            Player2_B.TabIndex = 9;
            Player2_B.Text = "button4";
            Player2_B.UseVisualStyleBackColor = true;
            Player2_B.Click += Player2_B_Click;
            // 
            // Player2_A
            // 
            Player2_A.Location = new System.Drawing.Point(65, 165);
            Player2_A.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Player2_A.Name = "Player2_A";
            Player2_A.Size = new System.Drawing.Size(358, 27);
            Player2_A.TabIndex = 8;
            Player2_A.Text = "button4";
            Player2_A.UseVisualStyleBackColor = true;
            Player2_A.Click += Player2_A_Click;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(368, 415);
            button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(88, 27);
            button1.TabIndex = 1;
            button1.Text = "&Save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(273, 415);
            button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(88, 27);
            button2.TabIndex = 2;
            button2.Text = "&Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(linkLabel3);
            groupBox1.Controls.Add(linkLabel2);
            groupBox1.Controls.Add(linkLabel1);
            groupBox1.Location = new System.Drawing.Point(14, 14);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Size = new System.Drawing.Size(441, 53);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Profile";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(7, 22);
            comboBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(268, 23);
            comboBox1.TabIndex = 3;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.Location = new System.Drawing.Point(380, 25);
            linkLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new System.Drawing.Size(50, 15);
            linkLabel3.TabIndex = 2;
            linkLabel3.TabStop = true;
            linkLabel3.Text = "Rename";
            linkLabel3.LinkClicked += linkLabel3_LinkClicked;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new System.Drawing.Point(320, 25);
            linkLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new System.Drawing.Size(50, 15);
            linkLabel2.TabIndex = 1;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Remove";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new System.Drawing.Point(282, 25);
            linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new System.Drawing.Size(29, 15);
            linkLabel1.TabIndex = 0;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Add";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // controlSettingsWindow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(467, 455);
            ControlBox = false;
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(tabControl1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "controlSettingsWindow";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Controllers Settings";
            Load += controlSettingsWindow_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button Player1_A;
        private System.Windows.Forms.Button Player1_Left;
        private System.Windows.Forms.Button Player1_Down;
        private System.Windows.Forms.Button Player1_Right;
        private System.Windows.Forms.Button Player1_Up;
        private System.Windows.Forms.Button Player1_Select;
        private System.Windows.Forms.Button Player1_Start;
        private System.Windows.Forms.Button Player1_B;
        private System.Windows.Forms.Button Player2_Left;
        private System.Windows.Forms.Button Player2_Down;
        private System.Windows.Forms.Button Player2_Right;
        private System.Windows.Forms.Button Player2_Up;
        private System.Windows.Forms.Button Player2_Select;
        private System.Windows.Forms.Button Player2_Start;
        private System.Windows.Forms.Button Player2_B;
        private System.Windows.Forms.Button Player2_A;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}