namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    partial class FormPayByBalance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPayByBalance));
            this.lblExit = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnByBalancePwd = new System.Windows.Forms.Button();
            this.lblOr = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblExit
            // 
            this.lblExit.AutoSize = true;
            this.lblExit.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblExit.ForeColor = System.Drawing.Color.Tomato;
            this.lblExit.Location = new System.Drawing.Point(316, 9);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(52, 27);
            this.lblExit.TabIndex = 2;
            this.lblExit.Text = "取消";
            this.lblExit.Click += new System.EventHandler(this.lblExit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(90, 141);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 220);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.Location = new System.Drawing.Point(95, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 21);
            this.label2.TabIndex = 8;
            this.label2.Text = "请使用扫码枪扫描付款码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.label1.Location = new System.Drawing.Point(135, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 30);
            this.label1.TabIndex = 7;
            this.label1.Text = "余额支付";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnByBalancePwd
            // 
            this.btnByBalancePwd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnByBalancePwd.BackgroundImage")));
            this.btnByBalancePwd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnByBalancePwd.FlatAppearance.BorderSize = 0;
            this.btnByBalancePwd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnByBalancePwd.ForeColor = System.Drawing.Color.White;
            this.btnByBalancePwd.Location = new System.Drawing.Point(85, 400);
            this.btnByBalancePwd.Name = "btnByBalancePwd";
            this.btnByBalancePwd.Size = new System.Drawing.Size(210, 44);
            this.btnByBalancePwd.TabIndex = 9;
            this.btnByBalancePwd.Text = "使用支付密码";
            this.btnByBalancePwd.UseVisualStyleBackColor = true;
            this.btnByBalancePwd.Click += new System.EventHandler(this.btnByBalancePwd_Click);
            // 
            // lblOr
            // 
            this.lblOr.AutoSize = true;
            this.lblOr.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblOr.Location = new System.Drawing.Point(176, 372);
            this.lblOr.Name = "lblOr";
            this.lblOr.Size = new System.Drawing.Size(26, 21);
            this.lblOr.TabIndex = 10;
            this.lblOr.Text = "或";
            // 
            // FormPayByBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 520);
            this.Controls.Add(this.lblOr);
            this.Controls.Add(this.btnByBalancePwd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblExit);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormPayByBalance";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmPayOnline";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOnLinePayResult_FormClosing);
            this.Shown += new System.EventHandler(this.frmBalancePayResult_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblExit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnByBalancePwd;
        private System.Windows.Forms.Label lblOr;
    }
}