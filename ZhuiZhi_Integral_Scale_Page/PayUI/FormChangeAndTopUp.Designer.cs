namespace ZhuiZhi_Integral_Scale_UncleFruit.PayUI
{
    partial class FormChangeAndTopUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChangeAndTopUp));
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalChange = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCurrentChange = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSaveBalance = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnDecimalPoint = new System.Windows.Forms.Button();
            this.numTxtBalance = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox();
            this.lblYuan = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.label2.Location = new System.Drawing.Point(21, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 30);
            this.label2.TabIndex = 71;
            this.label2.Text = "现金找零";
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnCancel.Location = new System.Drawing.Point(561, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(30, 30);
            this.btnCancel.TabIndex = 72;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label1.Location = new System.Drawing.Point(22, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 24);
            this.label1.TabIndex = 73;
            this.label1.Text = "当前需要找零";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label3.Location = new System.Drawing.Point(22, 93);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(370, 24);
            this.label3.TabIndex = 74;
            this.label3.Text = "您可以询问顾客是否找零或将零钱存入余额中";
            // 
            // lblTotalChange
            // 
            this.lblTotalChange.AutoSize = true;
            this.lblTotalChange.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.lblTotalChange.ForeColor = System.Drawing.Color.Tomato;
            this.lblTotalChange.Location = new System.Drawing.Point(145, 56);
            this.lblTotalChange.Name = "lblTotalChange";
            this.lblTotalChange.Size = new System.Drawing.Size(86, 31);
            this.lblTotalChange.TabIndex = 75;
            this.lblTotalChange.Text = "￥0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label4.Location = new System.Drawing.Point(22, 139);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(208, 24);
            this.label4.TabIndex = 76;
            this.label4.Text = "可以选择自定义转存金额";
            // 
            // lblCurrentChange
            // 
            this.lblCurrentChange.AutoSize = true;
            this.lblCurrentChange.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.lblCurrentChange.ForeColor = System.Drawing.Color.Tomato;
            this.lblCurrentChange.Location = new System.Drawing.Point(397, 237);
            this.lblCurrentChange.Name = "lblCurrentChange";
            this.lblCurrentChange.Size = new System.Drawing.Size(86, 31);
            this.lblCurrentChange.TabIndex = 79;
            this.lblCurrentChange.Text = "￥0.00";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label6.Location = new System.Drawing.Point(274, 244);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 24);
            this.label6.TabIndex = 78;
            this.label6.Text = "元，还需找零";
            // 
            // btnSaveBalance
            // 
            this.btnSaveBalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(190)))), ((int)(((byte)(139)))));
            this.btnSaveBalance.FlatAppearance.BorderSize = 0;
            this.btnSaveBalance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveBalance.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnSaveBalance.ForeColor = System.Drawing.Color.White;
            this.btnSaveBalance.Location = new System.Drawing.Point(26, 283);
            this.btnSaveBalance.Name = "btnSaveBalance";
            this.btnSaveBalance.Size = new System.Drawing.Size(271, 60);
            this.btnSaveBalance.TabIndex = 80;
            this.btnSaveBalance.Text = "存入余额";
            this.btnSaveBalance.UseVisualStyleBackColor = false;
            this.btnSaveBalance.Click += new System.EventHandler(this.btnSaveBalance_Click);
            // 
            // btnChange
            // 
            this.btnChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(204)))));
            this.btnChange.FlatAppearance.BorderSize = 0;
            this.btnChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChange.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnChange.ForeColor = System.Drawing.Color.White;
            this.btnChange.Location = new System.Drawing.Point(303, 283);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(271, 60);
            this.btnChange.TabIndex = 81;
            this.btnChange.Text = "找零";
            this.btnChange.UseVisualStyleBackColor = false;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnAll
            // 
            this.btnAll.BackColor = System.Drawing.Color.White;
            this.btnAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(204)))));
            this.btnAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAll.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(204)))));
            this.btnAll.Location = new System.Drawing.Point(26, 175);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(114, 41);
            this.btnAll.TabIndex = 82;
            this.btnAll.Text = "全部";
            this.btnAll.UseVisualStyleBackColor = false;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnDecimalPoint
            // 
            this.btnDecimalPoint.BackColor = System.Drawing.Color.White;
            this.btnDecimalPoint.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(204)))));
            this.btnDecimalPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecimalPoint.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnDecimalPoint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(204)))));
            this.btnDecimalPoint.Location = new System.Drawing.Point(151, 175);
            this.btnDecimalPoint.Name = "btnDecimalPoint";
            this.btnDecimalPoint.Size = new System.Drawing.Size(114, 41);
            this.btnDecimalPoint.TabIndex = 83;
            this.btnDecimalPoint.Text = "角、分";
            this.btnDecimalPoint.UseVisualStyleBackColor = false;
            this.btnDecimalPoint.Click += new System.EventHandler(this.btnDecimalPoint_Click);
            // 
            // numTxtBalance
            // 
            this.numTxtBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numTxtBalance.DecimalDigits = 2;
            this.numTxtBalance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numTxtBalance.Location = new System.Drawing.Point(26, 222);
            this.numTxtBalance.LockFocus = true;
            this.numTxtBalance.MaxDeciaml = ((long)(100000000));
            this.numTxtBalance.MaxLength = 32767;
            this.numTxtBalance.Name = "numTxtBalance";
            this.numTxtBalance.NeedBoard = false;
            this.numTxtBalance.OnlyNumber = true;
            this.numTxtBalance.Size = new System.Drawing.Size(239, 47);
            this.numTxtBalance.TabIndex = 84;
            this.numTxtBalance.WaterText = "请输入转存金额";
            this.numTxtBalance.DataChanged += new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox.DataRecHandleDelegate(this.numTxtBalance_DataChanged);
            // 
            // lblYuan
            // 
            this.lblYuan.AutoSize = true;
            this.lblYuan.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblYuan.Location = new System.Drawing.Point(488, 244);
            this.lblYuan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblYuan.Name = "lblYuan";
            this.lblYuan.Size = new System.Drawing.Size(28, 24);
            this.lblYuan.TabIndex = 85;
            this.lblYuan.Text = "元";
            // 
            // FormChangeAndTopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.lblYuan);
            this.Controls.Add(this.numTxtBalance);
            this.Controls.Add(this.btnDecimalPoint);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.btnSaveBalance);
            this.Controls.Add(this.lblCurrentChange);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblTotalChange);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormChangeAndTopUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormChangeAndTopUp";
            this.Load += new System.EventHandler(this.FormChangeAndTopUp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalChange;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCurrentChange;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSaveBalance;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnDecimalPoint;
        private MyControl.NumberTextBox numTxtBalance;
        private System.Windows.Forms.Label lblYuan;
    }
}