﻿namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    partial class FormMemberRecevice
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
            this.btOldCardOK = new System.Windows.Forms.Button();
            this.labform = new System.Windows.Forms.Label();
            this.txtOldCardNumber = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btOldCardOK
            // 
            this.btOldCardOK.BackColor = System.Drawing.Color.OrangeRed;
            this.btOldCardOK.FlatAppearance.BorderSize = 0;
            this.btOldCardOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btOldCardOK.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOldCardOK.ForeColor = System.Drawing.Color.White;
            this.btOldCardOK.Location = new System.Drawing.Point(21, 124);
            this.btOldCardOK.Margin = new System.Windows.Forms.Padding(2);
            this.btOldCardOK.Name = "btOldCardOK";
            this.btOldCardOK.Size = new System.Drawing.Size(343, 53);
            this.btOldCardOK.TabIndex = 104;
            this.btOldCardOK.Text = "确定";
            this.btOldCardOK.UseVisualStyleBackColor = false;
            this.btOldCardOK.Click += new System.EventHandler(this.btOldCardOK_Click);
            // 
            // labform
            // 
            this.labform.AutoSize = true;
            this.labform.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labform.Location = new System.Drawing.Point(16, 19);
            this.labform.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labform.Name = "labform";
            this.labform.Size = new System.Drawing.Size(132, 27);
            this.labform.TabIndex = 103;
            this.labform.Text = "输入会员账号";
            // 
            // txtOldCardNumber
            // 
            this.txtOldCardNumber.BackColor = System.Drawing.Color.White;
            this.txtOldCardNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOldCardNumber.DecimalDigits = 3;
            this.txtOldCardNumber.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOldCardNumber.Location = new System.Drawing.Point(17, 61);
            this.txtOldCardNumber.LockFocus = true;
            this.txtOldCardNumber.MaxDeciaml = ((long)(0));
            this.txtOldCardNumber.MaxLength = 100;
            this.txtOldCardNumber.Name = "txtOldCardNumber";
            this.txtOldCardNumber.NeedBoard = true;
            this.txtOldCardNumber.OnlyNumber = true;
            this.txtOldCardNumber.Size = new System.Drawing.Size(347, 50);
            this.txtOldCardNumber.TabIndex = 0;
            this.txtOldCardNumber.WaterText = "输入会员账号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(322, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 21);
            this.label1.TabIndex = 106;
            this.label1.Text = "返回";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // FormMemberRecevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 225);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOldCardNumber);
            this.Controls.Add(this.btOldCardOK);
            this.Controls.Add(this.labform);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMemberRecevice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormMemberRecevice";
            this.Load += new System.EventHandler(this.FormMemberRecevice_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyControl.NumberTextBox txtOldCardNumber;
        private System.Windows.Forms.Button btOldCardOK;
        private System.Windows.Forms.Label labform;
        private System.Windows.Forms.Label label1;
    }
}