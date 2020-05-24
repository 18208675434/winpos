namespace WinSaasPOS_Scale
{
    partial class FormPayCashToChange
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
            this.lbtnCancle = new System.Windows.Forms.Label();
            this.btnPayOK = new System.Windows.Forms.Button();
            this.lblChange = new System.Windows.Forms.Label();
            this.lblCash = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblChangeStr = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPriceStr = new System.Windows.Forms.Label();
            this.lblCashPay = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbtnCancle
            // 
            this.lbtnCancle.AutoSize = true;
            this.lbtnCancle.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lbtnCancle.Location = new System.Drawing.Point(269, 16);
            this.lbtnCancle.Name = "lbtnCancle";
            this.lbtnCancle.Size = new System.Drawing.Size(100, 24);
            this.lbtnCancle.TabIndex = 44;
            this.lbtnCancle.Text = "返回上层 >";
            this.lbtnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnPayOK
            // 
            this.btnPayOK.BackColor = System.Drawing.Color.OrangeRed;
            this.btnPayOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayOK.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnPayOK.ForeColor = System.Drawing.Color.White;
            this.btnPayOK.Location = new System.Drawing.Point(12, 450);
            this.btnPayOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnPayOK.Name = "btnPayOK";
            this.btnPayOK.Size = new System.Drawing.Size(357, 55);
            this.btnPayOK.TabIndex = 43;
            this.btnPayOK.TabStop = false;
            this.btnPayOK.Text = "完成交易";
            this.btnPayOK.UseVisualStyleBackColor = false;
            this.btnPayOK.Click += new System.EventHandler(this.btnPayOK_Click);
            // 
            // lblChange
            // 
            this.lblChange.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblChange.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblChange.Location = new System.Drawing.Point(207, 228);
            this.lblChange.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(162, 31);
            this.lblChange.TabIndex = 42;
            this.lblChange.Text = "￥0.00";
            this.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCash
            // 
            this.lblCash.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblCash.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblCash.Location = new System.Drawing.Point(207, 176);
            this.lblCash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(162, 31);
            this.lblCash.TabIndex = 41;
            this.lblCash.Text = "￥0.00";
            this.lblCash.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPrice
            // 
            this.lblPrice.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblPrice.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblPrice.Location = new System.Drawing.Point(201, 121);
            this.lblPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(168, 31);
            this.lblPrice.TabIndex = 40;
            this.lblPrice.Text = "￥0.00";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblChangeStr
            // 
            this.lblChangeStr.AutoSize = true;
            this.lblChangeStr.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblChangeStr.Location = new System.Drawing.Point(19, 228);
            this.lblChangeStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChangeStr.Name = "lblChangeStr";
            this.lblChangeStr.Size = new System.Drawing.Size(86, 30);
            this.lblChangeStr.TabIndex = 39;
            this.lblChangeStr.Text = "找零 ：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.label3.Location = new System.Drawing.Point(19, 176);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 30);
            this.label3.TabIndex = 38;
            this.label3.Text = "实收现金 :";
            // 
            // lblPriceStr
            // 
            this.lblPriceStr.AutoSize = true;
            this.lblPriceStr.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblPriceStr.Location = new System.Drawing.Point(19, 121);
            this.lblPriceStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPriceStr.Name = "lblPriceStr";
            this.lblPriceStr.Size = new System.Drawing.Size(69, 30);
            this.lblPriceStr.TabIndex = 37;
            this.lblPriceStr.Text = "应收 :";
            // 
            // lblCashPay
            // 
            this.lblCashPay.AutoSize = true;
            this.lblCashPay.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblCashPay.Location = new System.Drawing.Point(19, 67);
            this.lblCashPay.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCashPay.Name = "lblCashPay";
            this.lblCashPay.Size = new System.Drawing.Size(92, 27);
            this.lblCashPay.TabIndex = 36;
            this.lblCashPay.Text = "现金收银";
            // 
            // FormPayCashToChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 520);
            this.Controls.Add(this.lbtnCancle);
            this.Controls.Add(this.btnPayOK);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.lblCash);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblChangeStr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPriceStr);
            this.Controls.Add(this.lblCashPay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormPayCashToChange";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmCash";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbtnCancle;
        private System.Windows.Forms.Button btnPayOK;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Label lblCash;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblChangeStr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPriceStr;
        private System.Windows.Forms.Label lblCashPay;

    }
}