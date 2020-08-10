namespace ZhuiZhi_Integral_Scale_UncleFruit.PayUI
{
    partial class FormPaySuccess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPaySuccess));
            this.lblSecond = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCashierInfo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timerClose = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSecond
            // 
            this.lblSecond.AutoSize = true;
            this.lblSecond.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblSecond.ForeColor = System.Drawing.Color.DarkGray;
            this.lblSecond.Location = new System.Drawing.Point(459, 499);
            this.lblSecond.Name = "lblSecond";
            this.lblSecond.Size = new System.Drawing.Size(21, 24);
            this.lblSecond.TabIndex = 15;
            this.lblSecond.Text = "3";
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.OrangeRed;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(458, 553);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(235, 58);
            this.btnOK.TabIndex = 14;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "继续收银";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblTime.ForeColor = System.Drawing.Color.DarkGray;
            this.lblTime.Location = new System.Drawing.Point(494, 499);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(190, 24);
            this.lblTime.TabIndex = 13;
            this.lblTime.Text = "秒后自动返回收银界面";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblMsg.Location = new System.Drawing.Point(412, 457);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(316, 25);
            this.lblMsg.TabIndex = 12;
            this.lblMsg.Text = "小票打印完成，请提醒用户取走小票";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(269, 402);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(673, 21);
            this.label2.TabIndex = 11;
            this.label2.Text = "———————————————————————————————————————";
            // 
            // lblCashierInfo
            // 
            this.lblCashierInfo.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblCashierInfo.Location = new System.Drawing.Point(158, 313);
            this.lblCashierInfo.Name = "lblCashierInfo";
            this.lblCashierInfo.Size = new System.Drawing.Size(892, 31);
            this.lblCashierInfo.TabIndex = 10;
            this.lblCashierInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(447, 139);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 80);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 24F);
            this.label1.Location = new System.Drawing.Point(550, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 41);
            this.label1.TabIndex = 8;
            this.label1.Text = "收银成功";
            // 
            // timerClose
            // 
            this.timerClose.Interval = 1000;
            this.timerClose.Tick += new System.EventHandler(this.timerClose_Tick);
            // 
            // FormPaySuccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.Controls.Add(this.lblSecond);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCashierInfo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPaySuccess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormPaySuccess";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPaySuccess_FormClosing);
            this.Shown += new System.EventHandler(this.frmCashierResult_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSecond;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCashierInfo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerClose;
    }
}