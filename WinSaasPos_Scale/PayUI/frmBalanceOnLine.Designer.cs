namespace WinSaasPOS_Scale
{
    partial class frmBalanceOnLine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBalanceOnLine));
            this.lbtnCancle = new System.Windows.Forms.Label();
            this.lblOnlinePay = new System.Windows.Forms.Label();
            this.lblTotalPay = new System.Windows.Forms.Label();
            this.lblCash = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblChangeStr = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPriceStr = new System.Windows.Forms.Label();
            this.btnPayNext = new System.Windows.Forms.Button();
            this.lblBalance = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.lblOnlinePay.Location = new System.Drawing.Point(16, 260);
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
            this.lblTotalPay.Location = new System.Drawing.Point(181, 149);
            this.lblTotalPay.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalPay.Name = "lblTotalPay";
            this.lblTotalPay.Size = new System.Drawing.Size(188, 35);
            this.lblTotalPay.TabIndex = 40;
            this.lblTotalPay.Text = "￥100.00";
            this.lblTotalPay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCash
            // 
            this.lblCash.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblCash.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblCash.Location = new System.Drawing.Point(181, 112);
            this.lblCash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(188, 35);
            this.lblCash.TabIndex = 39;
            this.lblCash.Text = "￥100.00";
            this.lblCash.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.lblChangeStr.Location = new System.Drawing.Point(11, 152);
            this.lblChangeStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChangeStr.Name = "lblChangeStr";
            this.lblChangeStr.Size = new System.Drawing.Size(113, 30);
            this.lblChangeStr.TabIndex = 37;
            this.lblChangeStr.Text = "还需支付 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.label3.Location = new System.Drawing.Point(11, 115);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 30);
            this.label3.TabIndex = 36;
            this.label3.Text = "实收现金 :";
            // 
            // lblPriceStr
            // 
            this.lblPriceStr.AutoSize = true;
            this.lblPriceStr.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblPriceStr.Location = new System.Drawing.Point(11, 80);
            this.lblPriceStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPriceStr.Name = "lblPriceStr";
            this.lblPriceStr.Size = new System.Drawing.Size(69, 30);
            this.lblPriceStr.TabIndex = 35;
            this.lblPriceStr.Text = "应收 :";
            // 
            // btnPayNext
            // 
            this.btnPayNext.BackColor = System.Drawing.Color.OrangeRed;
            this.btnPayNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayNext.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnPayNext.ForeColor = System.Drawing.Color.White;
            this.btnPayNext.Image = ((System.Drawing.Image)(resources.GetObject("btnPayNext.Image")));
            this.btnPayNext.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPayNext.Location = new System.Drawing.Point(16, 308);
            this.btnPayNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnPayNext.Name = "btnPayNext";
            this.btnPayNext.Size = new System.Drawing.Size(352, 57);
            this.btnPayNext.TabIndex = 42;
            this.btnPayNext.TabStop = false;
            this.btnPayNext.Text = "微信/支付宝";
            this.btnPayNext.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPayNext.UseVisualStyleBackColor = false;
            this.btnPayNext.Click += new System.EventHandler(this.btnPayNext_Click);
            // 
            // lblBalance
            // 
            this.lblBalance.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblBalance.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblBalance.Location = new System.Drawing.Point(186, 36);
            this.lblBalance.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(188, 35);
            this.lblBalance.TabIndex = 45;
            this.lblBalance.Text = "￥100.00";
            this.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBalance.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.label2.Location = new System.Drawing.Point(16, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 30);
            this.label2.TabIndex = 44;
            this.label2.Text = "余额抵扣 :";
            this.label2.Visible = false;
            // 
            // frmBalanceOnLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 520);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbtnCancle);
            this.Controls.Add(this.btnPayNext);
            this.Controls.Add(this.lblOnlinePay);
            this.Controls.Add(this.lblTotalPay);
            this.Controls.Add(this.lblCash);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblChangeStr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPriceStr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBalanceOnLine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmBalanceOnLine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBalanceOnLine_FormClosing);
            this.Shown += new System.EventHandler(this.frmBalanceOnLine_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbtnCancle;
        private System.Windows.Forms.Button btnPayNext;
        private System.Windows.Forms.Label lblOnlinePay;
        private System.Windows.Forms.Label lblTotalPay;
        private System.Windows.Forms.Label lblCash;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblChangeStr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPriceStr;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label label2;
    }
}