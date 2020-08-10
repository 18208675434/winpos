namespace ZhuiZhi_Integral_Scale_UncleFruit.HelperUI
{
    partial class FormPurchaseLimit
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
            this.lbtnOK = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbtnOK
            // 
            this.lbtnOK.AutoSize = true;
            this.lbtnOK.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lbtnOK.ForeColor = System.Drawing.Color.DeepPink;
            this.lbtnOK.Location = new System.Drawing.Point(595, 158);
            this.lbtnOK.Name = "lbtnOK";
            this.lbtnOK.Size = new System.Drawing.Size(78, 27);
            this.lbtnOK.TabIndex = 14;
            this.lbtnOK.Text = "知道了";
            this.lbtnOK.Click += new System.EventHandler(this.lbtnOK_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblMsg.Location = new System.Drawing.Point(22, 67);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(651, 71);
            this.lblMsg.TabIndex = 12;
            this.lblMsg.Text = "同一商品的重量不同重量商品参加一个限购活动，如麦菜200g和麦菜300g属于麦菜的不同重量商品，参加同一限购活动。";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblTitle.Location = new System.Drawing.Point(22, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(101, 30);
            this.lblTitle.TabIndex = 11;
            this.lblTitle.Text = "温馨提示";
            // 
            // FormPurchaseLimit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(700, 200);
            this.Controls.Add(this.lbtnOK);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPurchaseLimit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormPurchaseLimit";
            this.Resize += new System.EventHandler(this.FormPurchaseLimit_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbtnOK;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblTitle;
    }
}