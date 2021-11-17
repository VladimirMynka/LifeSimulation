namespace LifeSimulation
{
    partial class MyForm
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pixelSizeInput = new System.Windows.Forms.TextBox();
            this.offsetLeftInput = new System.Windows.Forms.TextBox();
            this.offsetTopInput = new System.Windows.Forms.TextBox();
            this.timeoutInput = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.InfoTextBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(88, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1000, 1000);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.world_Click);
            this.pictureBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.world_DoubleClick);
            // 
            // pixelSizeInput
            // 
            this.pixelSizeInput.Location = new System.Drawing.Point(12, 70);
            this.pixelSizeInput.Name = "pixelSizeInput";
            this.pixelSizeInput.Size = new System.Drawing.Size(42, 20);
            this.pixelSizeInput.TabIndex = 1;
            this.pixelSizeInput.Text = "10";
            this.pixelSizeInput.TextChanged += new System.EventHandler(this.pixelSizeInput_TextChanged);
            // 
            // offsetLeftInput
            // 
            this.offsetLeftInput.Location = new System.Drawing.Point(12, 125);
            this.offsetLeftInput.Name = "offsetLeftInput";
            this.offsetLeftInput.Size = new System.Drawing.Size(42, 20);
            this.offsetLeftInput.TabIndex = 2;
            this.offsetLeftInput.Text = "0";
            this.offsetLeftInput.TextChanged += new System.EventHandler(this.offsetLeftInput_TextChanged);
            // 
            // offsetTopInput
            // 
            this.offsetTopInput.Location = new System.Drawing.Point(12, 177);
            this.offsetTopInput.Name = "offsetTopInput";
            this.offsetTopInput.Size = new System.Drawing.Size(42, 20);
            this.offsetTopInput.TabIndex = 3;
            this.offsetTopInput.Text = "0";
            this.offsetTopInput.TextChanged += new System.EventHandler(this.offsetTopInput_TextChanged);
            // 
            // timeoutInput
            // 
            this.timeoutInput.Location = new System.Drawing.Point(12, 230);
            this.timeoutInput.Name = "timeoutInput";
            this.timeoutInput.Size = new System.Drawing.Size(42, 20);
            this.timeoutInput.TabIndex = 4;
            this.timeoutInput.Text = "1000";
            this.timeoutInput.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(11, 266);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "pause";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.MenuText;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox2.Location = new System.Drawing.Point(12, 51);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(70, 13);
            this.textBox2.TabIndex = 7;
            this.textBox2.Text = "CellSize";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.MenuText;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox3.Location = new System.Drawing.Point(60, 73);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(21, 13);
            this.textBox3.TabIndex = 8;
            this.textBox3.Text = "px";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.MenuText;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox4.Location = new System.Drawing.Point(12, 106);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(70, 13);
            this.textBox4.TabIndex = 9;
            this.textBox4.Text = "Left offset";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.MenuText;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox5.Location = new System.Drawing.Point(60, 128);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(21, 13);
            this.textBox5.TabIndex = 10;
            this.textBox5.Text = "cells";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.MenuText;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox6.Location = new System.Drawing.Point(60, 180);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(21, 13);
            this.textBox6.TabIndex = 11;
            this.textBox6.Text = "cells";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.MenuText;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox7.Location = new System.Drawing.Point(11, 158);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(70, 13);
            this.textBox7.TabIndex = 12;
            this.textBox7.Text = "Top offset";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.SystemColors.MenuText;
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox8.Location = new System.Drawing.Point(12, 211);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(70, 13);
            this.textBox8.TabIndex = 13;
            this.textBox8.Text = "Tick";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.SystemColors.MenuText;
            this.textBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox9.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox9.Location = new System.Drawing.Point(60, 233);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(21, 13);
            this.textBox9.TabIndex = 14;
            this.textBox9.Text = "ms";
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.SystemColors.WindowText;
            this.textBox10.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox10.Location = new System.Drawing.Point(1094, 48);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox10.Size = new System.Drawing.Size(191, 20);
            this.textBox10.TabIndex = 15;
            this.textBox10.Text = "Information";
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // InfoTextBox
            // 
            this.InfoTextBox.BackColor = System.Drawing.SystemColors.MenuText;
            this.InfoTextBox.ForeColor = System.Drawing.SystemColors.Window;
            this.InfoTextBox.Location = new System.Drawing.Point(1094, 74);
            this.InfoTextBox.Name = "InfoTextBox";
            this.InfoTextBox.ReadOnly = true;
            this.InfoTextBox.Size = new System.Drawing.Size(191, 312);
            this.InfoTextBox.TabIndex = 16;
            this.InfoTextBox.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(1343, 530);
            this.Controls.Add(this.InfoTextBox);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.timeoutInput);
            this.Controls.Add(this.offsetTopInput);
            this.Controls.Add(this.offsetLeftInput);
            this.Controls.Add(this.pixelSizeInput);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.pixelSizeInput_TextChanged);
            ((System.ComponentModel.ISupportInitialize) (this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.RichTextBox InfoTextBox;

        private System.Windows.Forms.TextBox textBox10;

        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;

        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;

        private System.Windows.Forms.TextBox textBox4;

        private System.Windows.Forms.TextBox textBox3;

        private System.Windows.Forms.TextBox textBox2;

        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.TextBox timeoutInput;

        private System.Windows.Forms.TextBox offsetTopInput;

        private System.Windows.Forms.TextBox offsetLeftInput;

        private System.Windows.Forms.TextBox pixelSizeInput;
        
        private System.Windows.Forms.PictureBox pictureBox1;

        private System.Windows.Forms.Timer timer1;
        
        #endregion
    }
}