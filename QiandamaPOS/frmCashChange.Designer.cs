namespace QiandamaPOS
{
    partial class frmCashChange
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
            this.lblCashPay = new System.Windows.Forms.Label();
            this.btnCancle = new System.Windows.Forms.Button();
            this.lblPriceStr = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblChangeStr = new System.Windows.Forms.Label();
            this.lblChange = new System.Windows.Forms.Label();
            this.lblCash = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.btnPayOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCashPay
            // 
            this.lblCashPay.AutoSize = true;
            this.lblCashPay.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblCashPay.Location = new System.Drawing.Point(36, 61);
            this.lblCashPay.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCashPay.Name = "lblCashPay";
            this.lblCashPay.Size = new System.Drawing.Size(88, 25);
            this.lblCashPay.TabIndex = 0;
            this.lblCashPay.Text = "现金收银";
            // 
            // btnCancle
            // 
            this.btnCancle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancle.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnCancle.Location = new System.Drawing.Point(253, 11);
            this.btnCancle.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(116, 38);
            this.btnCancle.TabIndex = 1;
            this.btnCancle.TabStop = false;
            this.btnCancle.Text = "返回上层》";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // lblPriceStr
            // 
            this.lblPriceStr.AutoSize = true;
            this.lblPriceStr.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.lblPriceStr.Location = new System.Drawing.Point(35, 147);
            this.lblPriceStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPriceStr.Name = "lblPriceStr";
            this.lblPriceStr.Size = new System.Drawing.Size(84, 35);
            this.lblPriceStr.TabIndex = 2;
            this.lblPriceStr.Text = "应收 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.label3.Location = new System.Drawing.Point(35, 239);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 35);
            this.label3.TabIndex = 3;
            this.label3.Text = "实收现金 :";
            // 
            // lblChangeStr
            // 
            this.lblChangeStr.AutoSize = true;
            this.lblChangeStr.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.lblChangeStr.Location = new System.Drawing.Point(35, 326);
            this.lblChangeStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChangeStr.Name = "lblChangeStr";
            this.lblChangeStr.Size = new System.Drawing.Size(104, 35);
            this.lblChangeStr.TabIndex = 4;
            this.lblChangeStr.Text = "找零 ：";
            // 
            // lblChange
            // 
            this.lblChange.AutoSize = true;
            this.lblChange.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.lblChange.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblChange.Location = new System.Drawing.Point(215, 326);
            this.lblChange.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(129, 35);
            this.lblChange.TabIndex = 7;
            this.lblChange.Text = "￥100.00";
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.lblCash.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblCash.Location = new System.Drawing.Point(215, 239);
            this.lblCash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(129, 35);
            this.lblCash.TabIndex = 6;
            this.lblCash.Text = "￥100.00";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.lblPrice.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblPrice.Location = new System.Drawing.Point(215, 147);
            this.lblPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(129, 35);
            this.lblPrice.TabIndex = 5;
            this.lblPrice.Text = "￥100.00";
            // 
            // btnPayOK
            // 
            this.btnPayOK.BackColor = System.Drawing.Color.OrangeRed;
            this.btnPayOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPayOK.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.btnPayOK.ForeColor = System.Drawing.Color.White;
            this.btnPayOK.Location = new System.Drawing.Point(23, 421);
            this.btnPayOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnPayOK.Name = "btnPayOK";
            this.btnPayOK.Size = new System.Drawing.Size(333, 55);
            this.btnPayOK.TabIndex = 8;
            this.btnPayOK.TabStop = false;
            this.btnPayOK.Text = "完成交易";
            this.btnPayOK.UseVisualStyleBackColor = false;
            this.btnPayOK.Click += new System.EventHandler(this.btnPayOK_Click);
            // 
            // frmCashChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(380, 520);
            this.Controls.Add(this.btnPayOK);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.lblCash);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblChangeStr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPriceStr);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.lblCashPay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmCashChange";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmCash";
            this.SizeChanged += new System.EventHandler(this.frmCashChange_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCashPay;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Label lblPriceStr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblChangeStr;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Label lblCash;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Button btnPayOK;
    }
}