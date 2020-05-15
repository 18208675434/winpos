namespace WinSaasPOS
{
    partial class frmBalanceToMix
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBalanceToMix));
            this.lbtnCancle = new System.Windows.Forms.Label();
            this.lblOnlinePay = new System.Windows.Forms.Label();
            this.lblTotalPay = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblChangeStr = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPriceStr = new System.Windows.Forms.Label();
            this.btnOnLine = new System.Windows.Forms.Button();
            this.btnCash = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbtnCancle
            // 
            this.lbtnCancle.AutoSize = true;
            this.lbtnCancle.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbtnCancle.Location = new System.Drawing.Point(277, 9);
            this.lbtnCancle.Name = "lbtnCancle";
            this.lbtnCancle.Size = new System.Drawing.Size(91, 21);
            this.lbtnCancle.TabIndex = 43;
            this.lbtnCancle.Text = "返回上层 >";
            this.lbtnCancle.Click += new System.EventHandler(this.lbtnCancle_Click);
            // 
            // lblOnlinePay
            // 
            this.lblOnlinePay.AutoSize = true;
            this.lblOnlinePay.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblOnlinePay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblOnlinePay.Location = new System.Drawing.Point(11, 264);
            this.lblOnlinePay.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOnlinePay.Name = "lblOnlinePay";
            this.lblOnlinePay.Size = new System.Drawing.Size(101, 30);
            this.lblOnlinePay.TabIndex = 41;
            this.lblOnlinePay.Text = "继续收银";
            // 
            // lblTotalPay
            // 
            this.lblTotalPay.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblTotalPay.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblTotalPay.Location = new System.Drawing.Point(181, 158);
            this.lblTotalPay.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalPay.Name = "lblTotalPay";
            this.lblTotalPay.Size = new System.Drawing.Size(188, 35);
            this.lblTotalPay.TabIndex = 40;
            this.lblTotalPay.Text = "￥100.00";
            this.lblTotalPay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalance
            // 
            this.lblBalance.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblBalance.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblBalance.Location = new System.Drawing.Point(181, 117);
            this.lblBalance.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(188, 35);
            this.lblBalance.TabIndex = 39;
            this.lblBalance.Text = "￥100.00";
            this.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPrice
            // 
            this.lblPrice.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblPrice.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblPrice.Location = new System.Drawing.Point(181, 80);
            this.lblPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(188, 35);
            this.lblPrice.TabIndex = 38;
            this.lblPrice.Text = "￥100.00";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblChangeStr
            // 
            this.lblChangeStr.AutoSize = true;
            this.lblChangeStr.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblChangeStr.Location = new System.Drawing.Point(11, 161);
            this.lblChangeStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChangeStr.Name = "lblChangeStr";
            this.lblChangeStr.Size = new System.Drawing.Size(130, 30);
            this.lblChangeStr.TabIndex = 37;
            this.lblChangeStr.Text = "还需支付 ：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.label3.Location = new System.Drawing.Point(11, 120);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 30);
            this.label3.TabIndex = 36;
            this.label3.Text = "余额抵扣 :";
            // 
            // lblPriceStr
            // 
            this.lblPriceStr.AutoSize = true;
            this.lblPriceStr.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblPriceStr.Location = new System.Drawing.Point(11, 80);
            this.lblPriceStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPriceStr.Name = "lblPriceStr";
            this.lblPriceStr.Size = new System.Drawing.Size(113, 30);
            this.lblPriceStr.TabIndex = 35;
            this.lblPriceStr.Text = "应收总额 :";
            // 
            // btnOnLine
            // 
            this.btnOnLine.BackColor = System.Drawing.Color.OrangeRed;
            this.btnOnLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOnLine.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnOnLine.ForeColor = System.Drawing.Color.White;
            this.btnOnLine.Image = ((System.Drawing.Image)(resources.GetObject("btnOnLine.Image")));
            this.btnOnLine.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOnLine.Location = new System.Drawing.Point(11, 320);
            this.btnOnLine.Margin = new System.Windows.Forms.Padding(2);
            this.btnOnLine.Name = "btnOnLine";
            this.btnOnLine.Size = new System.Drawing.Size(175, 57);
            this.btnOnLine.TabIndex = 42;
            this.btnOnLine.TabStop = false;
            this.btnOnLine.Text = "微信/支付宝";
            this.btnOnLine.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOnLine.UseVisualStyleBackColor = false;
            this.btnOnLine.Click += new System.EventHandler(this.btnOnLine_Click);
            // 
            // btnCash
            // 
            this.btnCash.BackColor = System.Drawing.Color.DarkOrange;
            this.btnCash.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCash.FlatAppearance.BorderSize = 0;
            this.btnCash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCash.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnCash.ForeColor = System.Drawing.Color.White;
            this.btnCash.Image = ((System.Drawing.Image)(resources.GetObject("btnCash.Image")));
            this.btnCash.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCash.Location = new System.Drawing.Point(194, 320);
            this.btnCash.Name = "btnCash";
            this.btnCash.Size = new System.Drawing.Size(175, 57);
            this.btnCash.TabIndex = 45;
            this.btnCash.Text = "现 金";
            this.btnCash.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCash.UseVisualStyleBackColor = false;
            this.btnCash.Click += new System.EventHandler(this.btnCash_Click);
            // 
            // frmBalanceToMix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 520);
            this.Controls.Add(this.btnCash);
            this.Controls.Add(this.lbtnCancle);
            this.Controls.Add(this.btnOnLine);
            this.Controls.Add(this.lblOnlinePay);
            this.Controls.Add(this.lblTotalPay);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblChangeStr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPriceStr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBalanceToMix";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmBalanceToMix";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBalanceToMix_FormClosing);
            this.Shown += new System.EventHandler(this.frmBalanceToMix_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbtnCancle;
        private System.Windows.Forms.Button btnOnLine;
        private System.Windows.Forms.Label lblOnlinePay;
        private System.Windows.Forms.Label lblTotalPay;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblChangeStr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPriceStr;
        private System.Windows.Forms.Button btnCash;
    }
}