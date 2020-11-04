namespace QDAMAPOS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOnLine));
            this.lblPriceStr = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblChangeStr = new System.Windows.Forms.Label();
            this.lblChange = new System.Windows.Forms.Label();
            this.lblCash = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblOnlinePay = new System.Windows.Forms.Label();
            this.btnPayNext = new System.Windows.Forms.Button();
            this.lbtnCancle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPriceStr
            // 
            this.lblPriceStr.AutoSize = true;
            this.lblPriceStr.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblPriceStr.Location = new System.Drawing.Point(11, 80);
            this.lblPriceStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPriceStr.Name = "lblPriceStr";
            this.lblPriceStr.Size = new System.Drawing.Size(69, 30);
            this.lblPriceStr.TabIndex = 2;
            this.lblPriceStr.Text = "应收 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.label3.Location = new System.Drawing.Point(11, 120);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 30);
            this.label3.TabIndex = 3;
            this.label3.Text = "实收现金 :";
            // 
            // lblChangeStr
            // 
            this.lblChangeStr.AutoSize = true;
            this.lblChangeStr.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblChangeStr.Location = new System.Drawing.Point(11, 161);
            this.lblChangeStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChangeStr.Name = "lblChangeStr";
            this.lblChangeStr.Size = new System.Drawing.Size(130, 30);
            this.lblChangeStr.TabIndex = 4;
            this.lblChangeStr.Text = "还需支付 ：";
            // 
            // lblChange
            // 
            this.lblChange.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblChange.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblChange.Location = new System.Drawing.Point(181, 158);
            this.lblChange.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(188, 35);
            this.lblChange.TabIndex = 7;
            this.lblChange.Text = "￥100.00";
            this.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCash
            // 
            this.lblCash.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblCash.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblCash.Location = new System.Drawing.Point(181, 117);
            this.lblCash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(188, 35);
            this.lblCash.TabIndex = 6;
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
            this.lblPrice.TabIndex = 5;
            this.lblPrice.Text = "￥100.00";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.lblOnlinePay.TabIndex = 9;
            this.lblOnlinePay.Text = "继续收银";
            // 
            // btnPayNext
            // 
            this.btnPayNext.BackColor = System.Drawing.Color.OrangeRed;
            this.btnPayNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayNext.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnPayNext.ForeColor = System.Drawing.Color.White;
            this.btnPayNext.Image = ((System.Drawing.Image)(resources.GetObject("btnPayNext.Image")));
            this.btnPayNext.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPayNext.Location = new System.Drawing.Point(11, 312);
            this.btnPayNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnPayNext.Name = "btnPayNext";
            this.btnPayNext.Size = new System.Drawing.Size(358, 57);
            this.btnPayNext.TabIndex = 10;
            this.btnPayNext.TabStop = false;
            this.btnPayNext.Text = "微信/支付宝";
            this.btnPayNext.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPayNext.UseVisualStyleBackColor = false;
            this.btnPayNext.Click += new System.EventHandler(this.btnPayNext_Click);
            // 
            // lbtnCancle
            // 
            this.lbtnCancle.AutoSize = true;
            this.lbtnCancle.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbtnCancle.Location = new System.Drawing.Point(277, 9);
            this.lbtnCancle.Name = "lbtnCancle";
            this.lbtnCancle.Size = new System.Drawing.Size(91, 21);
            this.lbtnCancle.TabIndex = 34;
            this.lbtnCancle.Text = "返回上层 >";
            this.lbtnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // frmOnLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 520);
            this.Controls.Add(this.lbtnCancle);
            this.Controls.Add(this.btnPayNext);
            this.Controls.Add(this.lblOnlinePay);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.lblCash);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblChangeStr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPriceStr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmOnLine";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmCash";
            this.SizeChanged += new System.EventHandler(this.frmOnLine_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPriceStr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblChangeStr;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Label lblCash;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblOnlinePay;
        private System.Windows.Forms.Button btnPayNext;
        private System.Windows.Forms.Label lbtnCancle;
    }
}