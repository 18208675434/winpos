namespace QiandamaPOS
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
            this.tabPay = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnPayByBalance = new System.Windows.Forms.Button();
            this.btnPayByCash = new System.Windows.Forms.Button();
            this.btnPayOnLine = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnPayOK = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.tabPay.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 25);
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
            this.lblNeedCash.AutoSize = true;
            this.lblNeedCash.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblNeedCash.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblNeedCash.Location = new System.Drawing.Point(207, 99);
            this.lblNeedCash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNeedCash.Name = "lblNeedCash";
            this.lblNeedCash.Size = new System.Drawing.Size(79, 30);
            this.lblNeedCash.TabIndex = 19;
            this.lblNeedCash.Text = "￥0.00";
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblCash.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblCash.Location = new System.Drawing.Point(207, 59);
            this.lblCash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(79, 30);
            this.lblCash.TabIndex = 18;
            this.lblCash.Text = "￥0.00";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblPrice.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblPrice.Location = new System.Drawing.Point(207, 19);
            this.lblPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(79, 30);
            this.lblPrice.TabIndex = 17;
            this.lblPrice.Text = "￥0.00";
            // 
            // lblChangeStr
            // 
            this.lblChangeStr.AutoSize = true;
            this.lblChangeStr.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblChangeStr.Location = new System.Drawing.Point(18, 99);
            this.lblChangeStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChangeStr.Name = "lblChangeStr";
            this.lblChangeStr.Size = new System.Drawing.Size(130, 30);
            this.lblChangeStr.TabIndex = 16;
            this.lblChangeStr.Text = "还需支付 ：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.label3.Location = new System.Drawing.Point(18, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 30);
            this.label3.TabIndex = 15;
            this.label3.Text = "代金券折扣 :";
            // 
            // lblPriceStr
            // 
            this.lblPriceStr.AutoSize = true;
            this.lblPriceStr.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblPriceStr.Location = new System.Drawing.Point(18, 19);
            this.lblPriceStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPriceStr.Name = "lblPriceStr";
            this.lblPriceStr.Size = new System.Drawing.Size(106, 30);
            this.lblPriceStr.TabIndex = 14;
            this.lblPriceStr.Text = "应收总额:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
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
            this.lblExit.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblExit.ForeColor = System.Drawing.Color.Tomato;
            this.lblExit.Location = new System.Drawing.Point(319, 9);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(50, 25);
            this.lblExit.TabIndex = 19;
            this.lblExit.Text = "关闭";
            this.lblExit.Click += new System.EventHandler(this.lblExit_Click);
            // 
            // tabPay
            // 
            this.tabPay.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabPay.Controls.Add(this.tabPage1);
            this.tabPay.Controls.Add(this.tabPage2);
            this.tabPay.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabPay.ItemSize = new System.Drawing.Size(0, 1);
            this.tabPay.Location = new System.Drawing.Point(17, 358);
            this.tabPay.Name = "tabPay";
            this.tabPay.Padding = new System.Drawing.Point(0, 0);
            this.tabPay.SelectedIndex = 0;
            this.tabPay.Size = new System.Drawing.Size(352, 91);
            this.tabPay.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabPay.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.PaleTurquoise;
            this.tabPage1.Controls.Add(this.btnPayByBalance);
            this.tabPage1.Controls.Add(this.btnPayByCash);
            this.tabPage1.Controls.Add(this.btnPayOnLine);
            this.tabPage1.Location = new System.Drawing.Point(4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(344, 82);
            this.tabPage1.TabIndex = 0;
            // 
            // btnPayByBalance
            // 
            this.btnPayByBalance.BackColor = System.Drawing.Color.DarkTurquoise;
            this.btnPayByBalance.CausesValidation = false;
            this.btnPayByBalance.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPayByBalance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnPayByBalance.ForeColor = System.Drawing.Color.White;
            this.btnPayByBalance.Image = ((System.Drawing.Image)(resources.GetObject("btnPayByBalance.Image")));
            this.btnPayByBalance.Location = new System.Drawing.Point(239, 5);
            this.btnPayByBalance.Margin = new System.Windows.Forms.Padding(2);
            this.btnPayByBalance.Name = "btnPayByBalance";
            this.btnPayByBalance.Size = new System.Drawing.Size(100, 60);
            this.btnPayByBalance.TabIndex = 17;
            this.btnPayByBalance.TabStop = false;
            this.btnPayByBalance.Text = "余额";
            this.btnPayByBalance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPayByBalance.UseVisualStyleBackColor = false;
            this.btnPayByBalance.Click += new System.EventHandler(this.btnPayByBalance_Click);
            // 
            // btnPayByCash
            // 
            this.btnPayByCash.BackColor = System.Drawing.Color.DarkOrange;
            this.btnPayByCash.CausesValidation = false;
            this.btnPayByCash.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPayByCash.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnPayByCash.ForeColor = System.Drawing.Color.White;
            this.btnPayByCash.Image = ((System.Drawing.Image)(resources.GetObject("btnPayByCash.Image")));
            this.btnPayByCash.Location = new System.Drawing.Point(125, 5);
            this.btnPayByCash.Margin = new System.Windows.Forms.Padding(2);
            this.btnPayByCash.Name = "btnPayByCash";
            this.btnPayByCash.Size = new System.Drawing.Size(100, 60);
            this.btnPayByCash.TabIndex = 16;
            this.btnPayByCash.TabStop = false;
            this.btnPayByCash.Text = "现 金";
            this.btnPayByCash.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPayByCash.UseVisualStyleBackColor = false;
            this.btnPayByCash.Click += new System.EventHandler(this.btnPayByCash_Click);
            // 
            // btnPayOnLine
            // 
            this.btnPayOnLine.BackColor = System.Drawing.Color.Tomato;
            this.btnPayOnLine.CausesValidation = false;
            this.btnPayOnLine.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPayOnLine.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnPayOnLine.ForeColor = System.Drawing.Color.White;
            this.btnPayOnLine.Image = ((System.Drawing.Image)(resources.GetObject("btnPayOnLine.Image")));
            this.btnPayOnLine.Location = new System.Drawing.Point(5, 5);
            this.btnPayOnLine.Margin = new System.Windows.Forms.Padding(2);
            this.btnPayOnLine.Name = "btnPayOnLine";
            this.btnPayOnLine.Size = new System.Drawing.Size(100, 60);
            this.btnPayOnLine.TabIndex = 15;
            this.btnPayOnLine.TabStop = false;
            this.btnPayOnLine.Text = "微信/支付宝";
            this.btnPayOnLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPayOnLine.UseVisualStyleBackColor = false;
            this.btnPayOnLine.Click += new System.EventHandler(this.btnPayOnLine_Click_1);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.PaleTurquoise;
            this.tabPage2.Controls.Add(this.btnPayOK);
            this.tabPage2.Location = new System.Drawing.Point(4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(344, 82);
            this.tabPage2.TabIndex = 1;
            // 
            // btnPayOK
            // 
            this.btnPayOK.BackColor = System.Drawing.Color.Tomato;
            this.btnPayOK.CausesValidation = false;
            this.btnPayOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPayOK.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnPayOK.ForeColor = System.Drawing.Color.White;
            this.btnPayOK.Location = new System.Drawing.Point(28, 15);
            this.btnPayOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnPayOK.Name = "btnPayOK";
            this.btnPayOK.Size = new System.Drawing.Size(289, 52);
            this.btnPayOK.TabIndex = 16;
            this.btnPayOK.TabStop = false;
            this.btnPayOK.Text = "完成交易";
            this.btnPayOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPayOK.UseVisualStyleBackColor = false;
            this.btnPayOK.Click += new System.EventHandler(this.btnPayOK_Click);
            // 
            // frmCashCoupon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(380, 450);
            this.Controls.Add(this.tabPay);
            this.Controls.Add(this.lblExit);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlCashCoupons);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCashCoupon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmCashCoupon";
            this.Shown += new System.EventHandler(this.frmCashCoupon_Shown);
            this.SizeChanged += new System.EventHandler(this.frmCashCoupon_SizeChanged);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPay.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl tabPay;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnPayByBalance;
        private System.Windows.Forms.Button btnPayByCash;
        private System.Windows.Forms.Button btnPayOnLine;
        private System.Windows.Forms.Button btnPayOK;
    }
}