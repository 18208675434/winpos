namespace WinSaasPosStart
{
    partial class FormStart
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStart));
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblVersionStr = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.picExit = new System.Windows.Forms.PictureBox();
            this.btnStartPos = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDeviceSN = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCopyCode = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCopyMsg = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picExit)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMsg.Location = new System.Drawing.Point(29, 325);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(58, 21);
            this.lblMsg.TabIndex = 1;
            this.lblMsg.Text = "提示：";
            this.lblMsg.Visible = false;
            // 
            // lblVersionStr
            // 
            this.lblVersionStr.AutoSize = true;
            this.lblVersionStr.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblVersionStr.Location = new System.Drawing.Point(29, 53);
            this.lblVersionStr.Name = "lblVersionStr";
            this.lblVersionStr.Size = new System.Drawing.Size(90, 21);
            this.lblVersionStr.TabIndex = 2;
            this.lblVersionStr.Text = "最新版本：";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblVersion.Location = new System.Drawing.Point(109, 53);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(24, 21);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "--";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(29, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "版本信息";
            // 
            // txtVersion
            // 
            this.txtVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVersion.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtVersion.Location = new System.Drawing.Point(33, 110);
            this.txtVersion.Multiline = true;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(180, 199);
            this.txtVersion.TabIndex = 5;
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.AutoSize = true;
            this.lblCurrentVersion.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrentVersion.Location = new System.Drawing.Point(314, 53);
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            this.lblCurrentVersion.Size = new System.Drawing.Size(24, 21);
            this.lblCurrentVersion.TabIndex = 7;
            this.lblCurrentVersion.Text = "--";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(234, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 21);
            this.label3.TabIndex = 6;
            this.label3.Text = "当前版本：";
            // 
            // picExit
            // 
            this.picExit.BackColor = System.Drawing.Color.Transparent;
            this.picExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picExit.BackgroundImage")));
            this.picExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picExit.Location = new System.Drawing.Point(462, 2);
            this.picExit.Name = "picExit";
            this.picExit.Size = new System.Drawing.Size(25, 25);
            this.picExit.TabIndex = 13;
            this.picExit.TabStop = false;
            this.picExit.Click += new System.EventHandler(this.picExit_Click);
            // 
            // btnStartPos
            // 
            this.btnStartPos.BackColor = System.Drawing.Color.ForestGreen;
            this.btnStartPos.FlatAppearance.BorderSize = 0;
            this.btnStartPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartPos.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStartPos.ForeColor = System.Drawing.Color.White;
            this.btnStartPos.Location = new System.Drawing.Point(238, 265);
            this.btnStartPos.Name = "btnStartPos";
            this.btnStartPos.Size = new System.Drawing.Size(221, 44);
            this.btnStartPos.TabIndex = 14;
            this.btnStartPos.Text = "启动收银程序";
            this.btnStartPos.UseVisualStyleBackColor = false;
            this.btnStartPos.Click += new System.EventHandler(this.btnStartPos_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label4.Location = new System.Drawing.Point(234, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "设备号:";
            // 
            // txtDeviceSN
            // 
            this.txtDeviceSN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeviceSN.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDeviceSN.Location = new System.Drawing.Point(295, 114);
            this.txtDeviceSN.Name = "txtDeviceSN";
            this.txtDeviceSN.ReadOnly = true;
            this.txtDeviceSN.Size = new System.Drawing.Size(117, 16);
            this.txtDeviceSN.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(234, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 38);
            this.label2.TabIndex = 20;
            this.label2.Text = "*使用前请先将设备与门店进行绑定\r\n（可前往中台-设备管理进行绑定）";
            // 
            // btnCopyCode
            // 
            this.btnCopyCode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCopyCode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCopyCode.Location = new System.Drawing.Point(418, 110);
            this.btnCopyCode.Name = "btnCopyCode";
            this.btnCopyCode.Size = new System.Drawing.Size(41, 25);
            this.btnCopyCode.TabIndex = 21;
            this.btnCopyCode.Text = "复制";
            this.btnCopyCode.UseVisualStyleBackColor = true;
            this.btnCopyCode.Click += new System.EventHandler(this.btnCopyCode_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(260, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 19);
            this.label5.TabIndex = 22;
            this.label5.Text = "*首次启动会自动下载收银程序";
            this.label5.Visible = false;
            // 
            // lblCopyMsg
            // 
            this.lblCopyMsg.AutoSize = true;
            this.lblCopyMsg.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblCopyMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCopyMsg.Location = new System.Drawing.Point(394, 87);
            this.lblCopyMsg.Name = "lblCopyMsg";
            this.lblCopyMsg.Size = new System.Drawing.Size(65, 20);
            this.lblCopyMsg.TabIndex = 23;
            this.lblCopyMsg.Text = "版本信息";
            this.lblCopyMsg.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStatus.ForeColor = System.Drawing.Color.SandyBrown;
            this.lblStatus.Location = new System.Drawing.Point(75, 325);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(24, 21);
            this.lblStatus.TabIndex = 24;
            this.lblStatus.Text = "--";
            this.lblStatus.Visible = false;
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 369);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblCopyMsg);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCopyCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDeviceSN);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnStartPos);
            this.Controls.Add(this.picExit);
            this.Controls.Add(this.lblCurrentVersion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblVersionStr);
            this.Controls.Add(this.lblMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormStart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.FormStart_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picExit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblVersionStr;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label lblCurrentVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox picExit;
        private System.Windows.Forms.Button btnStartPos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDeviceSN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCopyCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCopyMsg;
        private System.Windows.Forms.Label lblStatus;
    }
}

