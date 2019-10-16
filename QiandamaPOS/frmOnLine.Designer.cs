namespace QiandamaPOS
{
    partial class frmOnLine
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
            this.btnCancle = new System.Windows.Forms.Button();
            this.lblPriceStr = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblChangeStr = new System.Windows.Forms.Label();
            this.lblChange = new System.Windows.Forms.Label();
            this.lblCash = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblOnlinePay = new System.Windows.Forms.Label();
            this.btnPayNext = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancle
            // 
            this.btnCancle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancle.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnCancle.Location = new System.Drawing.Point(261, 11);
            this.btnCancle.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(91, 32);
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
            this.lblPriceStr.Location = new System.Drawing.Point(22, 97);
            this.lblPriceStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPriceStr.Name = "lblPriceStr";
            this.lblPriceStr.Size = new System.Drawing.Size(138, 35);
            this.lblPriceStr.TabIndex = 2;
            this.lblPriceStr.Text = "应收总额 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.label3.Location = new System.Drawing.Point(22, 176);
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
            this.lblChangeStr.Location = new System.Drawing.Point(22, 259);
            this.lblChangeStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChangeStr.Name = "lblChangeStr";
            this.lblChangeStr.Size = new System.Drawing.Size(158, 35);
            this.lblChangeStr.TabIndex = 4;
            this.lblChangeStr.Text = "还需支付 ：";
            // 
            // lblChange
            // 
            this.lblChange.AutoSize = true;
            this.lblChange.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.lblChange.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblChange.Location = new System.Drawing.Point(211, 259);
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
            this.lblCash.Location = new System.Drawing.Point(211, 176);
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
            this.lblPrice.Location = new System.Drawing.Point(211, 97);
            this.lblPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(129, 35);
            this.lblPrice.TabIndex = 5;
            this.lblPrice.Text = "￥100.00";
            // 
            // lblOnlinePay
            // 
            this.lblOnlinePay.AutoSize = true;
            this.lblOnlinePay.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblOnlinePay.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblOnlinePay.Location = new System.Drawing.Point(23, 368);
            this.lblOnlinePay.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOnlinePay.Name = "lblOnlinePay";
            this.lblOnlinePay.Size = new System.Drawing.Size(88, 25);
            this.lblOnlinePay.TabIndex = 9;
            this.lblOnlinePay.Text = "继续收银";
            // 
            // btnPayNext
            // 
            this.btnPayNext.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnPayNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPayNext.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.btnPayNext.ForeColor = System.Drawing.Color.White;
            this.btnPayNext.Location = new System.Drawing.Point(21, 408);
            this.btnPayNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnPayNext.Name = "btnPayNext";
            this.btnPayNext.Size = new System.Drawing.Size(331, 70);
            this.btnPayNext.TabIndex = 10;
            this.btnPayNext.TabStop = false;
            this.btnPayNext.Text = "微信/支付宝支付";
            this.btnPayNext.UseVisualStyleBackColor = false;
            this.btnPayNext.Click += new System.EventHandler(this.btnPayNext_Click);
            // 
            // frmOnLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(380, 520);
            this.Controls.Add(this.btnPayNext);
            this.Controls.Add(this.lblOnlinePay);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.lblCash);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblChangeStr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPriceStr);
            this.Controls.Add(this.btnCancle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmOnLine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmCash";
            this.SizeChanged += new System.EventHandler(this.frmOnLine_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Label lblPriceStr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblChangeStr;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Label lblCash;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblOnlinePay;
        private System.Windows.Forms.Button btnPayNext;
    }
}