namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    partial class FormSevePassWoedOK
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTipsText = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timerSeavePassW = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(612, 183);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(233, 72);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "知道了";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTipsText
            // 
            this.lblTipsText.AutoSize = true;
            this.lblTipsText.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTipsText.Location = new System.Drawing.Point(121, 100);
            this.lblTipsText.Name = "lblTipsText";
            this.lblTipsText.Size = new System.Drawing.Size(295, 36);
            this.lblTipsText.TabIndex = 10;
            this.lblTipsText.Text = "用户设置支付密码成功";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(120, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 42);
            this.label2.TabIndex = 9;
            this.label2.Text = "提示";
            // 
            // FormSevePassWoedOK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 368);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblTipsText);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSevePassWoedOK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormSevePassWoedOK";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSevePassWoedOK_FormClosing);
            this.Shown += new System.EventHandler(this.FormSevePassWoedOK_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTipsText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timerSeavePassW;

    }
}