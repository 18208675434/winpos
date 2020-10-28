namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    partial class frmConfirmChange
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblConfim = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbtnOK = new System.Windows.Forms.Label();
            this.lbtnCancle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "切换模式";
            // 
            // lblConfim
            // 
            this.lblConfim.AutoSize = true;
            this.lblConfim.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblConfim.Location = new System.Drawing.Point(21, 76);
            this.lblConfim.Name = "lblConfim";
            this.lblConfim.Size = new System.Drawing.Size(282, 21);
            this.lblConfim.TabIndex = 1;
            this.lblConfim.Text = "您确认要将收银机切换为离线模式吗？";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label3.Location = new System.Drawing.Point(21, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "切换模式将自动为您进行一次交班";
            // 
            // lbtnOK
            // 
            this.lbtnOK.AutoSize = true;
            this.lbtnOK.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lbtnOK.ForeColor = System.Drawing.Color.Tomato;
            this.lbtnOK.Location = new System.Drawing.Point(430, 160);
            this.lbtnOK.Name = "lbtnOK";
            this.lbtnOK.Size = new System.Drawing.Size(46, 24);
            this.lbtnOK.TabIndex = 19;
            this.lbtnOK.Text = "确定";
            this.lbtnOK.Click += new System.EventHandler(this.lbtnOK_Click);
            // 
            // lbtnCancle
            // 
            this.lbtnCancle.AutoSize = true;
            this.lbtnCancle.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lbtnCancle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.lbtnCancle.Location = new System.Drawing.Point(350, 160);
            this.lbtnCancle.Name = "lbtnCancle";
            this.lbtnCancle.Size = new System.Drawing.Size(46, 24);
            this.lbtnCancle.TabIndex = 18;
            this.lbtnCancle.Text = "取消";
            this.lbtnCancle.Click += new System.EventHandler(this.lbtnCancle_Click);
            // 
            // frmConfirmChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 200);
            this.Controls.Add(this.lbtnOK);
            this.Controls.Add(this.lbtnCancle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblConfim);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConfirmChange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmConfirmChange";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblConfim;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbtnOK;
        private System.Windows.Forms.Label lbtnCancle;
    }
}