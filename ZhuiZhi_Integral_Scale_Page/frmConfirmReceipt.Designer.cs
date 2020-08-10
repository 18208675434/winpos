namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    partial class frmConfirmReceipt
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
            this.lblMsgStr = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lbtnCancle = new System.Windows.Forms.Label();
            this.lbtnOK = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMsgStr
            // 
            this.lblMsgStr.AutoSize = true;
            this.lblMsgStr.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblMsgStr.Location = new System.Drawing.Point(20, 20);
            this.lblMsgStr.Name = "lblMsgStr";
            this.lblMsgStr.Size = new System.Drawing.Size(126, 25);
            this.lblMsgStr.TabIndex = 1;
            this.lblMsgStr.Text = "确认交班成功";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblMsg.Location = new System.Drawing.Point(21, 76);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(234, 21);
            this.lblMsg.TabIndex = 2;
            this.lblMsg.Text = "请检查交班小票是否打印成功？";
            // 
            // lbtnCancle
            // 
            this.lbtnCancle.AutoSize = true;
            this.lbtnCancle.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lbtnCancle.ForeColor = System.Drawing.Color.DeepPink;
            this.lbtnCancle.Location = new System.Drawing.Point(312, 158);
            this.lbtnCancle.Name = "lbtnCancle";
            this.lbtnCancle.Size = new System.Drawing.Size(82, 24);
            this.lbtnCancle.TabIndex = 5;
            this.lbtnCancle.Text = "重新交班";
            this.lbtnCancle.Click += new System.EventHandler(this.lbtnCancle_Click);
            // 
            // lbtnOK
            // 
            this.lbtnOK.AutoSize = true;
            this.lbtnOK.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lbtnOK.ForeColor = System.Drawing.Color.DeepPink;
            this.lbtnOK.Location = new System.Drawing.Point(408, 158);
            this.lbtnOK.Name = "lbtnOK";
            this.lbtnOK.Size = new System.Drawing.Size(172, 24);
            this.lbtnOK.TabIndex = 6;
            this.lbtnOK.Text = "交班成功并退出登录";
            this.lbtnOK.Click += new System.EventHandler(this.lbtnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.Location = new System.Drawing.Point(21, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "若打印不成功可选择重新交班。";
            // 
            // frmConfirmReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 200);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbtnOK);
            this.Controls.Add(this.lbtnCancle);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lblMsgStr);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConfirmReceipt";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "若打印不成功可选择重新交班。";
            this.TopMost = true;
            this.SizeChanged += new System.EventHandler(this.frmDeleteGood_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMsgStr;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lbtnCancle;
        private System.Windows.Forms.Label lbtnOK;
        private System.Windows.Forms.Label label1;
    }
}