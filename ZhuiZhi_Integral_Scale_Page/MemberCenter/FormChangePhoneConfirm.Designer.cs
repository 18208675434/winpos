namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    partial class FormChangePhoneConfirm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.lblConfirmChangeNamber = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(722, 189);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 68);
            this.btnOK.TabIndex = 22;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnCancle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancle.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnCancle.Location = new System.Drawing.Point(525, 189);
            this.btnCancle.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(150, 68);
            this.btnCancle.TabIndex = 21;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // lblConfirmChangeNamber
            // 
            this.lblConfirmChangeNamber.AutoSize = true;
            this.lblConfirmChangeNamber.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblConfirmChangeNamber.Location = new System.Drawing.Point(44, 70);
            this.lblConfirmChangeNamber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConfirmChangeNamber.Name = "lblConfirmChangeNamber";
            this.lblConfirmChangeNamber.Size = new System.Drawing.Size(223, 35);
            this.lblConfirmChangeNamber.TabIndex = 20;
            this.lblConfirmChangeNamber.Text = "确认更换手机号为";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblTitle.Location = new System.Drawing.Point(42, 12);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(77, 39);
            this.lblTitle.TabIndex = 19;
            this.lblTitle.Text = "提示";
            // 
            // FormChangePhoneConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(933, 319);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.lblConfirmChangeNamber);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormChangePhoneConfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormChangePhoneConfirm";
            this.Load += new System.EventHandler(this.FormChangePhoneConfirm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Label lblConfirmChangeNamber;
        private System.Windows.Forms.Label lblTitle;
    }
}