namespace LifeSimulation
{
    partial class Form1
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
            // 
            // pixelSizeInput
            // 
            this.pixelSizeInput.Location = new System.Drawing.Point(12, 73);
            this.pixelSizeInput.Name = "pixelSizeInput";
            this.pixelSizeInput.Size = new System.Drawing.Size(70, 20);
            this.pixelSizeInput.TabIndex = 1;
            this.pixelSizeInput.Text = "10";
            this.pixelSizeInput.TextChanged += new System.EventHandler(this.pixelSizeInput_TextChanged);
            // 
            // offsetLeftInput
            // 
            this.offsetLeftInput.Location = new System.Drawing.Point(12, 99);
            this.offsetLeftInput.Name = "offsetLeftInput";
            this.offsetLeftInput.Size = new System.Drawing.Size(70, 20);
            this.offsetLeftInput.TabIndex = 2;
            this.offsetLeftInput.Text = "0";
            this.offsetLeftInput.TextChanged += new System.EventHandler(this.offsetLeftInput_TextChanged);
            // 
            // offsetTopInput
            // 
            this.offsetTopInput.Location = new System.Drawing.Point(12, 125);
            this.offsetTopInput.Name = "offsetTopInput";
            this.offsetTopInput.Size = new System.Drawing.Size(70, 20);
            this.offsetTopInput.TabIndex = 3;
            this.offsetTopInput.Text = "0";
            this.offsetTopInput.TextChanged += new System.EventHandler(this.offsetTopInput_TextChanged);
            // 
            // timeoutInput
            // 
            this.timeoutInput.Location = new System.Drawing.Point(12, 151);
            this.timeoutInput.Name = "timeoutInput";
            this.timeoutInput.Size = new System.Drawing.Size(70, 20);
            this.timeoutInput.TabIndex = 4;
            this.timeoutInput.Text = "1000";
            this.timeoutInput.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "pause";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(925, 530);
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