namespace WinSaasPOS_Scale.BaseUI
{
    partial class FormTare
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTare));
            this.lblZero = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTare = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblZero
            // 
            this.lblZero.AutoSize = true;
            this.lblZero.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblZero.ForeColor = System.Drawing.Color.White;
            this.lblZero.Location = new System.Drawing.Point(51, 96);
            this.lblZero.Name = "lblZero";
            this.lblZero.Size = new System.Drawing.Size(50, 25);
            this.lblZero.TabIndex = 1;
            this.lblZero.Text = "置零";
            this.lblZero.Click += new System.EventHandler(this.lblZero_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.lblTare);
            this.panel1.Controls.Add(this.lblNumber);
            this.panel1.Controls.Add(this.lblZero);
            this.panel1.Location = new System.Drawing.Point(2, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(148, 136);
            this.panel1.TabIndex = 2;
            this.panel1.Leave += new System.EventHandler(this.panel1_Leave);
            // 
            // lblTare
            // 
            this.lblTare.AutoSize = true;
            this.lblTare.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblTare.ForeColor = System.Drawing.Color.White;
            this.lblTare.Location = new System.Drawing.Point(33, 16);
            this.lblTare.Name = "lblTare";
            this.lblTare.Size = new System.Drawing.Size(88, 25);
            this.lblTare.TabIndex = 3;
            this.lblTare.Text = "称重去皮";
            this.lblTare.Click += new System.EventHandler(this.lblTare_Click);
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblNumber.ForeColor = System.Drawing.Color.White;
            this.lblNumber.Location = new System.Drawing.Point(31, 56);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(88, 25);
            this.lblNumber.TabIndex = 2;
            this.lblNumber.Text = "数字去皮";
            this.lblNumber.Click += new System.EventHandler(this.lblNumber_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(67, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 15);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // FormTare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(154, 174);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormTare";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormTare";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Deactivate += new System.EventHandler(this.FormTare_Deactivate);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblZero;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTare;
        private System.Windows.Forms.Label lblNumber;
    }
}