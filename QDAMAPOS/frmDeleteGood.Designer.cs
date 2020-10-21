namespace QDAMAPOS
{
    partial class frmDeleteGood
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
            this.SuspendLayout();
            // 
            // lblMsgStr
            // 
            this.lblMsgStr.AutoSize = true;
            this.lblMsgStr.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblMsgStr.Location = new System.Drawing.Point(18, 10);
            this.lblMsgStr.Name = "lblMsgStr";
            this.lblMsgStr.Size = new System.Drawing.Size(189, 30);
            this.lblMsgStr.TabIndex = 1;
            this.lblMsgStr.Text = "是否确认删除商品";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblMsg.Location = new System.Drawing.Point(20, 56);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(70, 24);
            this.lblMsg.TabIndex = 2;
            this.lblMsg.Text = "lblMsg";
            // 
            // lbtnCancle
            // 
            this.lbtnCancle.AutoSize = true;
            this.lbtnCancle.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lbtnCancle.ForeColor = System.Drawing.Color.DeepPink;
            this.lbtnCancle.Location = new System.Drawing.Point(366, 124);
            this.lbtnCancle.Name = "lbtnCancle";
            this.lbtnCancle.Size = new System.Drawing.Size(46, 24);
            this.lbtnCancle.TabIndex = 5;
            this.lbtnCancle.Text = "取消";
            this.lbtnCancle.Click += new System.EventHandler(this.lbtnCancle_Click);
            // 
            // lbtnOK
            // 
            this.lbtnOK.AutoSize = true;
            this.lbtnOK.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lbtnOK.ForeColor = System.Drawing.Color.DeepPink;
            this.lbtnOK.Location = new System.Drawing.Point(447, 124);
            this.lbtnOK.Name = "lbtnOK";
            this.lbtnOK.Size = new System.Drawing.Size(46, 24);
            this.lbtnOK.TabIndex = 6;
            this.lbtnOK.Text = "确定";
            this.lbtnOK.Click += new System.EventHandler(this.lbtnOK_Click);
            // 
            // frmDeleteGood
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(520, 160);
            this.ControlBox = false;
            this.Controls.Add(this.lbtnOK);
            this.Controls.Add(this.lbtnCancle);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lblMsgStr);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDeleteGood";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
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
    }
}