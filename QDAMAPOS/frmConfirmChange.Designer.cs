namespace QDAMAPOS
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
            this.pnlPriceLine = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbtnOK = new System.Windows.Forms.Label();
            this.lbtnCancle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label1.Location = new System.Drawing.Point(31, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "切换模式";
            // 
            // lblConfim
            // 
            this.lblConfim.AutoSize = true;
            this.lblConfim.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblConfim.Location = new System.Drawing.Point(31, 96);
            this.lblConfim.Name = "lblConfim";
            this.lblConfim.Size = new System.Drawing.Size(316, 24);
            this.lblConfim.TabIndex = 1;
            this.lblConfim.Text = "您确认要将收银机切换为离线模式吗？";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label3.Location = new System.Drawing.Point(31, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(280, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "切换模式将自动为您进行一次交班";
            // 
            // pnlPriceLine
            // 
            this.pnlPriceLine.BackColor = System.Drawing.Color.LightGray;
            this.pnlPriceLine.Location = new System.Drawing.Point(0, 74);
            this.pnlPriceLine.Name = "pnlPriceLine";
            this.pnlPriceLine.Size = new System.Drawing.Size(500, 1);
            this.pnlPriceLine.TabIndex = 16;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Location = new System.Drawing.Point(0, 167);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 1);
            this.panel1.TabIndex = 17;
            // 
            // lbtnOK
            // 
            this.lbtnOK.AutoSize = true;
            this.lbtnOK.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbtnOK.ForeColor = System.Drawing.Color.Tomato;
            this.lbtnOK.Location = new System.Drawing.Point(420, 188);
            this.lbtnOK.Name = "lbtnOK";
            this.lbtnOK.Size = new System.Drawing.Size(42, 21);
            this.lbtnOK.TabIndex = 19;
            this.lbtnOK.Text = "确定";
            this.lbtnOK.Click += new System.EventHandler(this.lbtnOK_Click);
            // 
            // lbtnCancle
            // 
            this.lbtnCancle.AutoSize = true;
            this.lbtnCancle.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbtnCancle.ForeColor = System.Drawing.Color.DimGray;
            this.lbtnCancle.Location = new System.Drawing.Point(346, 188);
            this.lbtnCancle.Name = "lbtnCancle";
            this.lbtnCancle.Size = new System.Drawing.Size(42, 21);
            this.lbtnCancle.TabIndex = 18;
            this.lbtnCancle.Text = "取消";
            this.lbtnCancle.Click += new System.EventHandler(this.lbtnCancle_Click);
            // 
            // frmConfirmChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(490, 230);
            this.Controls.Add(this.lbtnOK);
            this.Controls.Add(this.lbtnCancle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlPriceLine);
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
        private System.Windows.Forms.Panel pnlPriceLine;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbtnOK;
        private System.Windows.Forms.Label lbtnCancle;
    }
}