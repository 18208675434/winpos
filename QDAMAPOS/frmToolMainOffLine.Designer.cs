namespace QDAMAPOS
{
    partial class frmToolMainOffLine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmToolMainOffLine));
            this.pnlReceipt = new System.Windows.Forms.Panel();
            this.lblReceipt = new System.Windows.Forms.Label();
            this.picReceipt = new System.Windows.Forms.PictureBox();
            this.pnlExit = new System.Windows.Forms.Panel();
            this.lblExit = new System.Windows.Forms.Label();
            this.picExit = new System.Windows.Forms.PictureBox();
            this.pnlChangeMode = new System.Windows.Forms.Panel();
            this.lblChangeMode = new System.Windows.Forms.Label();
            this.picChangeMode = new System.Windows.Forms.PictureBox();
            this.lblDeviceSN = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.pnlLine1 = new System.Windows.Forms.Panel();
            this.pnlLine2 = new System.Windows.Forms.Panel();
            this.pnlLine4 = new System.Windows.Forms.Panel();
            this.pnlReceipt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReceipt)).BeginInit();
            this.pnlExit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExit)).BeginInit();
            this.pnlChangeMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picChangeMode)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlReceipt
            // 
            this.pnlReceipt.Controls.Add(this.lblReceipt);
            this.pnlReceipt.Controls.Add(this.picReceipt);
            this.pnlReceipt.Location = new System.Drawing.Point(11, 15);
            this.pnlReceipt.Name = "pnlReceipt";
            this.pnlReceipt.Size = new System.Drawing.Size(147, 39);
            this.pnlReceipt.TabIndex = 0;
            this.pnlReceipt.Click += new System.EventHandler(this.pnlReceipt_Click);
            // 
            // lblReceipt
            // 
            this.lblReceipt.AutoSize = true;
            this.lblReceipt.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lblReceipt.ForeColor = System.Drawing.Color.White;
            this.lblReceipt.Location = new System.Drawing.Point(66, 9);
            this.lblReceipt.Name = "lblReceipt";
            this.lblReceipt.Size = new System.Drawing.Size(39, 20);
            this.lblReceipt.TabIndex = 1;
            this.lblReceipt.Text = "交班";
            this.lblReceipt.Click += new System.EventHandler(this.pnlReceipt_Click);
            // 
            // picReceipt
            // 
            this.picReceipt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picReceipt.BackgroundImage")));
            this.picReceipt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picReceipt.Location = new System.Drawing.Point(13, 7);
            this.picReceipt.Name = "picReceipt";
            this.picReceipt.Size = new System.Drawing.Size(24, 24);
            this.picReceipt.TabIndex = 0;
            this.picReceipt.TabStop = false;
            this.picReceipt.Click += new System.EventHandler(this.pnlReceipt_Click);
            // 
            // pnlExit
            // 
            this.pnlExit.Controls.Add(this.lblExit);
            this.pnlExit.Controls.Add(this.picExit);
            this.pnlExit.Location = new System.Drawing.Point(11, 65);
            this.pnlExit.Name = "pnlExit";
            this.pnlExit.Size = new System.Drawing.Size(147, 39);
            this.pnlExit.TabIndex = 2;
            this.pnlExit.Click += new System.EventHandler(this.pnlExit_Click);
            // 
            // lblExit
            // 
            this.lblExit.AutoSize = true;
            this.lblExit.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lblExit.ForeColor = System.Drawing.Color.White;
            this.lblExit.Location = new System.Drawing.Point(66, 9);
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
            // pnlChangeMode
            // 
            this.pnlChangeMode.Controls.Add(this.lblChangeMode);
            this.pnlChangeMode.Controls.Add(this.picChangeMode);
            this.pnlChangeMode.Location = new System.Drawing.Point(11, 116);
            this.pnlChangeMode.Name = "pnlChangeMode";
            this.pnlChangeMode.Size = new System.Drawing.Size(147, 39);
            this.pnlChangeMode.TabIndex = 5;
            this.pnlChangeMode.Click += new System.EventHandler(this.pnlChangeMode_Click);
            // 
            // lblChangeMode
            // 
            this.lblChangeMode.AutoSize = true;
            this.lblChangeMode.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lblChangeMode.ForeColor = System.Drawing.Color.White;
            this.lblChangeMode.Location = new System.Drawing.Point(49, 9);
            this.lblChangeMode.Name = "lblChangeMode";
            this.lblChangeMode.Size = new System.Drawing.Size(73, 20);
            this.lblChangeMode.TabIndex = 1;
            this.lblChangeMode.Text = "切换模式";
            this.lblChangeMode.Click += new System.EventHandler(this.pnlChangeMode_Click);
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
            this.picChangeMode.Click += new System.EventHandler(this.pnlChangeMode_Click);
            // 
            // lblDeviceSN
            // 
            this.lblDeviceSN.AutoSize = true;
            this.lblDeviceSN.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblDeviceSN.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblDeviceSN.Location = new System.Drawing.Point(3, 173);
            this.lblDeviceSN.Name = "lblDeviceSN";
            this.lblDeviceSN.Size = new System.Drawing.Size(56, 17);
            this.lblDeviceSN.TabIndex = 2;
            this.lblDeviceSN.Text = "设备号：";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblVersion.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblVersion.Location = new System.Drawing.Point(3, 195);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(56, 17);
            this.lblVersion.TabIndex = 12;
            this.lblVersion.Text = "版本号：";
            // 
            // pnlLine1
            // 
            this.pnlLine1.BackColor = System.Drawing.Color.White;
            this.pnlLine1.Location = new System.Drawing.Point(6, 58);
            this.pnlLine1.Name = "pnlLine1";
            this.pnlLine1.Size = new System.Drawing.Size(160, 1);
            this.pnlLine1.TabIndex = 13;
            // 
            // pnlLine2
            // 
            this.pnlLine2.BackColor = System.Drawing.Color.White;
            this.pnlLine2.Location = new System.Drawing.Point(6, 109);
            this.pnlLine2.Name = "pnlLine2";
            this.pnlLine2.Size = new System.Drawing.Size(160, 1);
            this.pnlLine2.TabIndex = 14;
            // 
            // pnlLine4
            // 
            this.pnlLine4.BackColor = System.Drawing.Color.White;
            this.pnlLine4.Location = new System.Drawing.Point(6, 159);
            this.pnlLine4.Name = "pnlLine4";
            this.pnlLine4.Size = new System.Drawing.Size(160, 1);
            this.pnlLine4.TabIndex = 14;
            // 
            // frmToolMainOffLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(178, 220);
            this.Controls.Add(this.pnlLine4);
            this.Controls.Add(this.pnlLine2);
            this.Controls.Add(this.pnlLine1);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblDeviceSN);
            this.Controls.Add(this.pnlChangeMode);
            this.Controls.Add(this.pnlExit);
            this.Controls.Add(this.pnlReceipt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmToolMainOffLine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmToolMainOffLine";
            this.Deactivate += new System.EventHandler(this.frmToolMain_Deactivate);
            this.Shown += new System.EventHandler(this.frmToolMain_Shown);
            this.pnlReceipt.ResumeLayout(false);
            this.pnlReceipt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReceipt)).EndInit();
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

        private System.Windows.Forms.Panel pnlReceipt;
        private System.Windows.Forms.PictureBox picReceipt;
        private System.Windows.Forms.Label lblReceipt;
        private System.Windows.Forms.Panel pnlExit;
        private System.Windows.Forms.Label lblExit;
        private System.Windows.Forms.PictureBox picExit;
        private System.Windows.Forms.Panel pnlChangeMode;
        private System.Windows.Forms.Label lblChangeMode;
        private System.Windows.Forms.PictureBox picChangeMode;
        private System.Windows.Forms.Label lblDeviceSN;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Panel pnlLine1;
        private System.Windows.Forms.Panel pnlLine2;
        private System.Windows.Forms.Panel pnlLine4;
    }
}