namespace WinSaasPOS
{
    partial class frmCashCoupon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCashCoupon));
            this.label1 = new System.Windows.Forms.Label();
            this.pnlCashCoupons = new System.Windows.Forms.Panel();
            this.lblNeedCash = new System.Windows.Forms.Label();
            this.lblCash = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblChangeStr = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPriceStr = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblExit = new System.Windows.Forms.Label();
            this.btnPayOK = new System.Windows.Forms.Button();
            this.picScreen = new System.Windows.Forms.PictureBox();
            this.lblNext = new System.Windows.Forms.Label();
            this.btnPayByBalance = new System.Windows.Forms.Button();
            this.btnPayByCash = new System.Windows.Forms.Button();
            this.btnPayOnLine = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 12;
            this.label1.Text = "选择代金券";
            // 
            // pnlCashCoupons
            // 
            this.pnlCashCoupons.Location = new System.Drawing.Point(17, 82);
            this.pnlCashCoupons.Name = "pnlCashCoupons";
            this.pnlCashCoupons.Size = new System.Drawing.Size(351, 112);
            this.pnlCashCoupons.TabIndex = 13;
            // 
            // lblNeedCash
            // 
            this.lblNeedCash.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblNeedCash.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblNeedCash.Location = new System.Drawing.Point(178, 99);
            this.lblNeedCash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNeedCash.Name = "lblNeedCash";
            this.lblNeedCash.Size = new System.Drawing.Size(165, 30);
            this.lblNeedCash.TabIndex = 19;
            this.lblNeedCash.Text = "￥0.00";
            this.lblNeedCash.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCash
            // 
            this.lblCash.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblCash.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblCash.Location = new System.Drawing.Point(178, 59);
            this.lblCash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(165, 30);
            this.lblCash.TabIndex = 18;
            this.lblCash.Text = "￥0.00";
            this.lblCash.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPrice
            // 
            this.lblPrice.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblPrice.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblPrice.Location = new System.Drawing.Point(178, 19);
            this.lblPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(165, 30);
            this.lblPrice.TabIndex = 17;
            this.lblPrice.Text = "￥0.00";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblChangeStr
            // 
            this.lblChangeStr.AutoSize = true;
            this.lblChangeStr.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblChangeStr.Location = new System.Drawing.Point(18, 99);
            this.lblChangeStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChangeStr.Name = "lblChangeStr";
            this.lblChangeStr.Size = new System.Drawing.Size(113, 25);
            this.lblChangeStr.TabIndex = 16;
            this.lblChangeStr.Text = "还需支付 ：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label3.Location = new System.Drawing.Point(18, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 25);
            this.label3.TabIndex = 15;
            this.label3.Text = "代金券抵扣 :";
            // 
            // lblPriceStr
            // 
            this.lblPriceStr.AutoSize = true;
            this.lblPriceStr.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblPriceStr.Location = new System.Drawing.Point(18, 19);
            this.lblPriceStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPriceStr.Name = "lblPriceStr";
            this.lblPriceStr.Size = new System.Drawing.Size(93, 25);
            this.lblPriceStr.TabIndex = 14;
            this.lblPriceStr.Text = "应收总额:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.lblNeedCash);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lblPrice);
            this.panel2.Controls.Add(this.lblChangeStr);
            this.panel2.Controls.Add(this.lblCash);
            this.panel2.Controls.Add(this.lblPriceStr);
            this.panel2.Location = new System.Drawing.Point(17, 200);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(352, 152);
            this.panel2.TabIndex = 18;
            // 
            // lblExit
            // 
            this.lblExit.AutoSize = true;
            this.lblExit.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblExit.ForeColor = System.Drawing.Color.Black;
            this.lblExit.Location = new System.Drawing.Point(258, 9);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(118, 21);
            this.lblExit.TabIndex = 19;
            this.lblExit.Text = "返回收银方式>";
            this.lblExit.Click += new System.EventHandler(this.lblExit_Click);
            // 
            // btnPayOK
            // 
            this.btnPayOK.BackColor = System.Drawing.Color.Tomato;
            this.btnPayOK.CausesValidation = false;
            this.btnPayOK.FlatAppearance.BorderSize = 0;
            this.btnPayOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayOK.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnPayOK.ForeColor = System.Drawing.Color.White;
            this.btnPayOK.Location = new System.Drawing.Point(17, 401);
            this.btnPayOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnPayOK.Name = "btnPayOK";
            this.btnPayOK.Size = new System.Drawing.Size(350, 50);
            this.btnPayOK.TabIndex = 16;
            this.btnPayOK.TabStop = false;
            this.btnPayOK.Text = "完成交易";
            this.btnPayOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPayOK.UseVisualStyleBackColor = false;
            this.btnPayOK.Visible = false;
            this.btnPayOK.Click += new System.EventHandler(this.btnPayOK_Click);
            // 
            // picScreen
            // 
            this.picScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picScreen.Location = new System.Drawing.Point(0, 0);
            this.picScreen.Name = "picScreen";
            this.picScreen.Size = new System.Drawing.Size(10, 10);
            this.picScreen.TabIndex = 24;
            this.picScreen.TabStop = false;
            this.picScreen.Visible = false;
            // 
            // lblNext
            // 
            this.lblNext.AutoSize = true;
            this.lblNext.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblNext.ForeColor = System.Drawing.Color.Black;
            this.lblNext.Location = new System.Drawing.Point(13, 368);
            this.lblNext.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNext.Name = "lblNext";
            this.lblNext.Size = new System.Drawing.Size(82, 24);
            this.lblNext.TabIndex = 20;
            this.lblNext.Text = "继续收银";
            // 
            // btnPayByBalance
            // 
            this.btnPayByBalance.BackColor = System.Drawing.Color.Silver;
            this.btnPayByBalance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnPayByBalance.FlatAppearance.BorderSize = 0;
            this.btnPayByBalance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayByBalance.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this.btnPayByBalance.ForeColor = System.Drawing.Color.White;
            this.btnPayByBalance.Image = ((System.Drawing.Image)(resources.GetObject("btnPayByBalance.Image")));
            this.btnPayByBalance.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPayByBalance.Location = new System.Drawing.Point(262, 401);
            this.btnPayByBalance.Name = "btnPayByBalance";
            this.btnPayByBalance.Padding = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.btnPayByBalance.Size = new System.Drawing.Size(107, 50);
            this.btnPayByBalance.TabIndex = 28;
            this.btnPayByBalance.Text = "余 额";
            this.btnPayByBalance.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPayByBalance.UseVisualStyleBackColor = false;
            this.btnPayByBalance.Click += new System.EventHandler(this.btnPayByBalance_Click);
            // 
            // btnPayByCash
            // 
            this.btnPayByCash.BackColor = System.Drawing.Color.Silver;
            this.btnPayByCash.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnPayByCash.FlatAppearance.BorderSize = 0;
            this.btnPayByCash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayByCash.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this.btnPayByCash.ForeColor = System.Drawing.Color.White;
            this.btnPayByCash.Image = ((System.Drawing.Image)(resources.GetObject("btnPayByCash.Image")));
            this.btnPayByCash.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPayByCash.Location = new System.Drawing.Point(157, 401);
            this.btnPayByCash.Name = "btnPayByCash";
            this.btnPayByCash.Padding = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.btnPayByCash.Size = new System.Drawing.Size(101, 50);
            this.btnPayByCash.TabIndex = 27;
            this.btnPayByCash.Text = "现 金";
            this.btnPayByCash.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPayByCash.UseVisualStyleBackColor = false;
            this.btnPayByCash.Click += new System.EventHandler(this.btnPayByCash_Click);
            // 
            // btnPayOnLine
            // 
            this.btnPayOnLine.BackColor = System.Drawing.Color.Silver;
            this.btnPayOnLine.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnPayOnLine.FlatAppearance.BorderSize = 0;
            this.btnPayOnLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayOnLine.Font = new System.Drawing.Font("微软雅黑", 7.5F);
            this.btnPayOnLine.ForeColor = System.Drawing.Color.White;
            this.btnPayOnLine.Image = ((System.Drawing.Image)(resources.GetObject("btnPayOnLine.Image")));
            this.btnPayOnLine.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPayOnLine.Location = new System.Drawing.Point(17, 401);
            this.btnPayOnLine.Name = "btnPayOnLine";
            this.btnPayOnLine.Padding = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.btnPayOnLine.Size = new System.Drawing.Size(136, 50);
            this.btnPayOnLine.TabIndex = 26;
            this.btnPayOnLine.Text = "微信/支付宝/银联云闪付\r\n";
            this.btnPayOnLine.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPayOnLine.UseVisualStyleBackColor = false;
            this.btnPayOnLine.Click += new System.EventHandler(this.btnPayOnLine_Click_1);
            // 
            // frmCashCoupon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 480);
            this.Controls.Add(this.picScreen);
            this.Controls.Add(this.btnPayByBalance);
            this.Controls.Add(this.btnPayByCash);
            this.Controls.Add(this.btnPayOnLine);
            this.Controls.Add(this.lblNext);
            this.Controls.Add(this.btnPayOK);
            this.Controls.Add(this.lblExit);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlCashCoupons);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCashCoupon";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmCashCoupon";
            this.Shown += new System.EventHandler(this.frmCashCoupon_Shown);
            this.EnabledChanged += new System.EventHandler(this.frmCashCoupon_EnabledChanged);
            this.SizeChanged += new System.EventHandler(this.frmCashCoupon_SizeChanged);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlCashCoupons;
        private System.Windows.Forms.Label lblNeedCash;
        private System.Windows.Forms.Label lblCash;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblChangeStr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPriceStr;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblExit;
        private System.Windows.Forms.Button btnPayOK;
        private System.Windows.Forms.PictureBox picScreen;
        private System.Windows.Forms.Label lblNext;
        private System.Windows.Forms.Button btnPayByBalance;
        private System.Windows.Forms.Button btnPayByCash;
        private System.Windows.Forms.Button btnPayOnLine;
    }
}