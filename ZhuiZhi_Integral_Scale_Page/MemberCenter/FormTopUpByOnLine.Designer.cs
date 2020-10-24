namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    partial class FormTopUpByOnLine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTopUpByOnLine));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblExit = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timerSyncTrade = new System.Windows.Forms.Timer(this.components);
            this.pnlLoading = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.label1.Location = new System.Drawing.Point(106, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "微信/支付宝支付";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.Location = new System.Drawing.Point(100, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "请使用扫码枪扫描付款码";
            // 
            // lblExit
            // 
            this.lblExit.AutoSize = true;
            this.lblExit.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblExit.ForeColor = System.Drawing.Color.Tomato;
            this.lblExit.Location = new System.Drawing.Point(316, 9);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(52, 27);
            this.lblExit.TabIndex = 2;
            this.lblExit.Text = "取消";
            this.lblExit.Click += new System.EventHandler(this.lblExit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(92, 153);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 220);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // timerSyncTrade
            // 
            this.timerSyncTrade.Interval = 2000;
            this.timerSyncTrade.Tick += new System.EventHandler(this.timerSyncTrade_Tick);
            // 
            // pnlLoading
            // 
            this.pnlLoading.BackColor = System.Drawing.Color.White;
            this.pnlLoading.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlLoading.BackgroundImage")));
            this.pnlLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlLoading.Location = new System.Drawing.Point(160, 230);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(60, 60);
            this.pnlLoading.TabIndex = 63;
            this.pnlLoading.Visible = false;
            // 
            // FormTopUpByOnLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 520);
            this.Controls.Add(this.pnlLoading);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblExit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormTopUpByOnLine";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmPayOnline";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOnLinePayResult_FormClosing);
            this.Shown += new System.EventHandler(this.frmOnLinePayResult_Shown);
            this.SizeChanged += new System.EventHandler(this.frmOnLinePayResult_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblExit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timerSyncTrade;
        private System.Windows.Forms.Panel pnlLoading;
    }
}