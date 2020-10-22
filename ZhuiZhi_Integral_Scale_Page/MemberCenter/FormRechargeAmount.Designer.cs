namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    partial class FormRechargeAmount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRechargeAmount));
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlItem = new System.Windows.Forms.Panel();
            this.picCheck = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRewardAmount = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.listData = new System.Windows.Forms.ListBox();
            this.pnlItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblTitle.Location = new System.Drawing.Point(17, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(145, 30);
            this.lblTitle.TabIndex = 7;
            this.lblTitle.Text = "设置充值金额";
            // 
            // pnlItem
            // 
            this.pnlItem.BackColor = System.Drawing.Color.White;
            this.pnlItem.Controls.Add(this.picCheck);
            this.pnlItem.Controls.Add(this.label1);
            this.pnlItem.Controls.Add(this.lblRewardAmount);
            this.pnlItem.Controls.Add(this.lblAmount);
            this.pnlItem.Location = new System.Drawing.Point(22, 662);
            this.pnlItem.Name = "pnlItem";
            this.pnlItem.Size = new System.Drawing.Size(538, 100);
            this.pnlItem.TabIndex = 72;
            // 
            // picCheck
            // 
            this.picCheck.BackgroundImage = global::ZhuiZhi_Integral_Scale_UncleFruit.Properties.Resources.check;
            this.picCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picCheck.Location = new System.Drawing.Point(479, 33);
            this.picCheck.Name = "picCheck";
            this.picCheck.Size = new System.Drawing.Size(34, 34);
            this.picCheck.TabIndex = 1;
            this.picCheck.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(24, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "￥";
            // 
            // lblRewardAmount
            // 
            this.lblRewardAmount.AutoSize = true;
            this.lblRewardAmount.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRewardAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblRewardAmount.Location = new System.Drawing.Point(263, 40);
            this.lblRewardAmount.Name = "lblRewardAmount";
            this.lblRewardAmount.Size = new System.Drawing.Size(137, 22);
            this.lblRewardAmount.TabIndex = 0;
            this.lblRewardAmount.Text = "赠送金额0元";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAmount.ForeColor = System.Drawing.Color.Red;
            this.lblAmount.Location = new System.Drawing.Point(49, 32);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(168, 33);
            this.lblAmount.TabIndex = 0;
            this.lblAmount.Text = "lblAmount";
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnCancel.Location = new System.Drawing.Point(555, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(30, 30);
            this.btnCancel.TabIndex = 71;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // listData
            // 
            this.listData.FormattingEnabled = true;
            this.listData.ItemHeight = 12;
            this.listData.Location = new System.Drawing.Point(22, 63);
            this.listData.Name = "listData";
            this.listData.Size = new System.Drawing.Size(540, 376);
            this.listData.TabIndex = 73;
            // 
            // FormRechargeAmount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(610, 800);
            this.Controls.Add(this.listData);
            this.Controls.Add(this.pnlItem);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRechargeAmount";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormConfirm";
            this.Shown += new System.EventHandler(this.FormRechargeAmount_Shown);
            this.Resize += new System.EventHandler(this.FormConfirm_Resize);
            this.pnlItem.ResumeLayout(false);
            this.pnlItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCheck)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlItem;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picCheck;
        private System.Windows.Forms.Label lblRewardAmount;
        private System.Windows.Forms.ListBox listData;
    }
}