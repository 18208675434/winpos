namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    partial class FormChangePhonePhysicalCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChangePhonePhysicalCard));
            this.labform = new System.Windows.Forms.Label();
            this.btOldCardOK = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtOldCardNumber = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labform
            // 
            this.labform.AutoSize = true;
            this.labform.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labform.Location = new System.Drawing.Point(11, 10);
            this.labform.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labform.Name = "labform";
            this.labform.Size = new System.Drawing.Size(112, 27);
            this.labform.TabIndex = 45;
            this.labform.Text = "实体卡验证";
            // 
            // btOldCardOK
            // 
            this.btOldCardOK.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btOldCardOK.FlatAppearance.BorderSize = 0;
            this.btOldCardOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btOldCardOK.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOldCardOK.ForeColor = System.Drawing.Color.White;
            this.btOldCardOK.Location = new System.Drawing.Point(16, 115);
            this.btOldCardOK.Margin = new System.Windows.Forms.Padding(2);
            this.btOldCardOK.Name = "btOldCardOK";
            this.btOldCardOK.Size = new System.Drawing.Size(343, 53);
            this.btOldCardOK.TabIndex = 46;
            this.btOldCardOK.Text = "确定";
            this.btOldCardOK.UseVisualStyleBackColor = false;
            this.btOldCardOK.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(326, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(27, 27);
            this.pictureBox1.TabIndex = 66;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // txtOldCardNumber
            // 
            this.txtOldCardNumber.BackColor = System.Drawing.Color.White;
            this.txtOldCardNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOldCardNumber.DecimalDigits = 3;
            this.txtOldCardNumber.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOldCardNumber.Location = new System.Drawing.Point(12, 52);
            this.txtOldCardNumber.LockFocus = true;
            this.txtOldCardNumber.MaxDeciaml = ((long)(0));
            this.txtOldCardNumber.MaxLength = 100;
            this.txtOldCardNumber.Name = "txtOldCardNumber";
            this.txtOldCardNumber.NeedBoard = true;
            this.txtOldCardNumber.OnlyNumber = true;
            this.txtOldCardNumber.Size = new System.Drawing.Size(347, 50);
            this.txtOldCardNumber.TabIndex = 102;
            this.txtOldCardNumber.WaterText = "请输入或扫描实体卡号";
            this.txtOldCardNumber.Load += new System.EventHandler(this.txtOldCardNumber_Load);
            // 
            // FormChangePhonePhysicalCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 197);
            this.Controls.Add(this.txtOldCardNumber);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btOldCardOK);
            this.Controls.Add(this.labform);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormChangePhonePhysicalCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormChangePhonePhysicalCard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormChangePhonePhysicalCard_FormClosing);
            this.Shown += new System.EventHandler(this.FormChangePhonePhysicalCard_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labform;
        private System.Windows.Forms.Button btOldCardOK;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MyControl.NumberTextBox txtOldCardNumber;
    }
}