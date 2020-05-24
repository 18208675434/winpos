namespace WinSaasPOS_Scale
{
    partial class frmPrinterInfo
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
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblMsgStr = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblNotCheck = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lblMsg.ForeColor = System.Drawing.Color.Black;
            this.lblMsg.Location = new System.Drawing.Point(24, 45);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(444, 60);
            this.lblMsg.TabIndex = 4;
            this.lblMsg.Text = "没有检测到可用的打印机驱动，请确认打印机驱动是否正确安装\r\n如果安装多个打印机驱动，可在打印机设置页面选择使用的打印机\r\n如确认打印机及驱动连接完成可忽略此消息\r" +
    "\n";
            // 
            // lblMsgStr
            // 
            this.lblMsgStr.AutoSize = true;
            this.lblMsgStr.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblMsgStr.ForeColor = System.Drawing.Color.DarkSalmon;
            this.lblMsgStr.Location = new System.Drawing.Point(23, 9);
            this.lblMsgStr.Name = "lblMsgStr";
            this.lblMsgStr.Size = new System.Drawing.Size(107, 25);
            this.lblMsgStr.TabIndex = 3;
            this.lblMsgStr.Text = "温馨提示：";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(303, 124);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(150, 40);
            this.btnExit.TabIndex = 16;
            this.btnExit.Text = "转到打印机设置";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.LightSalmon;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(134, 124);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 40);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "忽 略";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblNotCheck
            // 
            this.lblNotCheck.AutoSize = true;
            this.lblNotCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNotCheck.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblNotCheck.ForeColor = System.Drawing.Color.Gray;
            this.lblNotCheck.Location = new System.Drawing.Point(48, 140);
            this.lblNotCheck.Name = "lblNotCheck";
            this.lblNotCheck.Size = new System.Drawing.Size(74, 21);
            this.lblNotCheck.TabIndex = 17;
            this.lblNotCheck.Text = "不再提示";
            this.lblNotCheck.Click += new System.EventHandler(this.lblNotCheck_Click);
            // 
            // frmPrinterInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 190);
            this.Controls.Add(this.lblNotCheck);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lblMsgStr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPrinterInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmPrinterInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblMsgStr;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblNotCheck;
    }
}