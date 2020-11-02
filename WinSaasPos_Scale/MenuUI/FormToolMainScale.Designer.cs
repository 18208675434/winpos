namespace WinSaasPOS_Scale
{
    partial class FormToolMainScale
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormToolMainScale));
            this.pnlLine2 = new System.Windows.Forms.Panel();
            this.pnlExit = new System.Windows.Forms.Panel();
            this.lblExit = new System.Windows.Forms.Label();
            this.picExit = new System.Windows.Forms.PictureBox();
            this.pnlLine4 = new System.Windows.Forms.Panel();
            this.pnlChangeMode = new System.Windows.Forms.Panel();
            this.lblChangeMode = new System.Windows.Forms.Label();
            this.picChangeMode = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblDeviceSN = new System.Windows.Forms.Label();
            this.pnlExit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExit)).BeginInit();
            this.pnlChangeMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picChangeMode)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLine2
            // 
            this.pnlLine2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.pnlLine2.Location = new System.Drawing.Point(7, 56);
            this.pnlLine2.Name = "pnlLine2";
            this.pnlLine2.Size = new System.Drawing.Size(160, 1);
            this.pnlLine2.TabIndex = 16;
            // 
            // pnlExit
            // 
            this.pnlExit.Controls.Add(this.lblExit);
            this.pnlExit.Controls.Add(this.picExit);
            this.pnlExit.Location = new System.Drawing.Point(12, 12);
            this.pnlExit.Name = "pnlExit";
            this.pnlExit.Size = new System.Drawing.Size(147, 39);
            this.pnlExit.TabIndex = 15;
            this.pnlExit.Click += new System.EventHandler(this.pnlExit_Click);
            // 
            // lblExit
            // 
            this.lblExit.AutoSize = true;
            this.lblExit.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lblExit.ForeColor = System.Drawing.Color.White;
            this.lblExit.Location = new System.Drawing.Point(48, 10);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(39, 20);
            this.lblExit.TabIndex = 1;
            this.lblExit.Text = "退出";
            this.lblExit.Click += new System.EventHandler(this.pnlExit_Click);
            // 
            // picExit
            // 
            this.picExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picExit.BackgroundImage")));
            this.picExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picExit.Location = new System.Drawing.Point(13, 7);
            this.picExit.Name = "picExit";
            this.picExit.Size = new System.Drawing.Size(24, 24);
            this.picExit.TabIndex = 0;
            this.picExit.TabStop = false;
            this.picExit.Click += new System.EventHandler(this.pnlExit_Click);
            // 
            // pnlLine4
            // 
            this.pnlLine4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.pnlLine4.Location = new System.Drawing.Point(7, 106);
            this.pnlLine4.Name = "pnlLine4";
            this.pnlLine4.Size = new System.Drawing.Size(160, 1);
            this.pnlLine4.TabIndex = 18;
            // 
            // pnlChangeMode
            // 
            this.pnlChangeMode.Controls.Add(this.lblChangeMode);
            this.pnlChangeMode.Controls.Add(this.picChangeMode);
            this.pnlChangeMode.Location = new System.Drawing.Point(12, 63);
            this.pnlChangeMode.Name = "pnlChangeMode";
            this.pnlChangeMode.Size = new System.Drawing.Size(147, 39);
            this.pnlChangeMode.TabIndex = 17;
            this.pnlChangeMode.Click += new System.EventHandler(this.pnlScaleModel_Click);
            // 
            // lblChangeMode
            // 
            this.lblChangeMode.AutoSize = true;
            this.lblChangeMode.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lblChangeMode.ForeColor = System.Drawing.Color.White;
            this.lblChangeMode.Location = new System.Drawing.Point(43, 10);
            this.lblChangeMode.Name = "lblChangeMode";
            this.lblChangeMode.Size = new System.Drawing.Size(103, 20);
            this.lblChangeMode.TabIndex = 1;
            this.lblChangeMode.Text = "切换收银模式";
            this.lblChangeMode.Click += new System.EventHandler(this.pnlScaleModel_Click);
            // 
            // picChangeMode
            // 
            this.picChangeMode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picChangeMode.BackgroundImage")));
            this.picChangeMode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picChangeMode.Location = new System.Drawing.Point(13, 7);
            this.picChangeMode.Name = "picChangeMode";
            this.picChangeMode.Size = new System.Drawing.Size(24, 24);
            this.picChangeMode.TabIndex = 0;
            this.picChangeMode.TabStop = false;
            this.picChangeMode.Click += new System.EventHandler(this.pnlScaleModel_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblVersion.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblVersion.Location = new System.Drawing.Point(9, 136);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(56, 17);
            this.lblVersion.TabIndex = 20;
            this.lblVersion.Text = "版本号：";
            // 
            // lblDeviceSN
            // 
            this.lblDeviceSN.AutoSize = true;
            this.lblDeviceSN.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblDeviceSN.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblDeviceSN.Location = new System.Drawing.Point(9, 114);
            this.lblDeviceSN.Name = "lblDeviceSN";
            this.lblDeviceSN.Size = new System.Drawing.Size(56, 17);
            this.lblDeviceSN.TabIndex = 19;
            this.lblDeviceSN.Text = "设备号：";
            // 
            // FormToolMainScale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(178, 160);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblDeviceSN);
            this.Controls.Add(this.pnlLine4);
            this.Controls.Add(this.pnlChangeMode);
            this.Controls.Add(this.pnlLine2);
            this.Controls.Add(this.pnlExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormToolMainScale";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormToolMainScale";
            this.Deactivate += new System.EventHandler(this.FormToolMainScale_Deactivate);
            this.Shown += new System.EventHandler(this.FormToolMainScale_Shown);
            this.pnlExit.ResumeLayout(false);
            this.pnlExit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExit)).EndInit();
            this.pnlChangeMode.ResumeLayout(false);
            this.pnlChangeMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picChangeMode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlLine2;
        private System.Windows.Forms.Panel pnlExit;
        private System.Windows.Forms.Label lblExit;
        private System.Windows.Forms.PictureBox picExit;
        private System.Windows.Forms.Panel pnlLine4;
        private System.Windows.Forms.Panel pnlChangeMode;
        private System.Windows.Forms.Label lblChangeMode;
        private System.Windows.Forms.PictureBox picChangeMode;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblDeviceSN;
    }
}