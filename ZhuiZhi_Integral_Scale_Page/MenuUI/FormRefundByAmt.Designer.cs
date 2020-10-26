﻿namespace ZhuiZhi_Integral_Scale_UncleFruit.MenuUI
{
    partial class FormRefundByAmt
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRefundByAmt));
            this.label1 = new System.Windows.Forms.Label();
            this.lblCash = new System.Windows.Forms.Label();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.proname = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlItem = new System.Windows.Forms.Panel();
            this.lblNum = new System.Windows.Forms.Label();
            this.lblTotalPrice = new System.Windows.Forms.Label();
            this.lblSkucode = new System.Windows.Forms.Label();
            this.lblSkuName = new System.Windows.Forms.Label();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.btnCancle = new System.Windows.Forms.Button();
            this.numTxt1 = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox();
            this.numTxt2 = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox();
            this.numTxt3 = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox();
            this.numTxt4 = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox();
            this.rbtnPageDown = new ZhuiZhi_Integral_Scale_UncleFruit.RoundButton();
            this.rbtnPageUp = new ZhuiZhi_Integral_Scale_UncleFruit.RoundButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.pnlItem.SuspendLayout();
            this.pnlControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 25);
            this.label1.TabIndex = 20;
            this.label1.Text = "退差价";
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblCash.ForeColor = System.Drawing.Color.Gray;
            this.lblCash.Location = new System.Drawing.Point(8, 47);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(555, 20);
            this.lblCash.TabIndex = 65;
            this.lblCash.Text = "请在需要退款的商品后输入退款金额，单个商品仅可退一次，退款金额不可超过应收金额";
            // 
            // dgvProduct
            // 
            this.dgvProduct.AllowUserToAddRows = false;
            this.dgvProduct.AllowUserToDeleteRows = false;
            this.dgvProduct.AllowUserToResizeColumns = false;
            this.dgvProduct.AllowUserToResizeRows = false;
            this.dgvProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProduct.BackgroundColor = System.Drawing.Color.White;
            this.dgvProduct.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProduct.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProduct.ColumnHeadersVisible = false;
            this.dgvProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.proname});
            this.dgvProduct.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvProduct.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvProduct.Location = new System.Drawing.Point(12, 81);
            this.dgvProduct.Name = "dgvProduct";
            this.dgvProduct.RowHeadersVisible = false;
            this.dgvProduct.RowTemplate.Height = 70;
            this.dgvProduct.Size = new System.Drawing.Size(576, 300);
            this.dgvProduct.TabIndex = 66;
            this.dgvProduct.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProduct_CellEnter);
            this.dgvProduct.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProduct_CellLeave);
            this.dgvProduct.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProduct_RowValidated);
            // 
            // proname
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 13F);
            dataGridViewCellStyle1.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle1.NullValue")));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.proname.DefaultCellStyle = dataGridViewCellStyle1;
            this.proname.FillWeight = 70F;
            this.proname.HeaderText = "商品名称";
            this.proname.Name = "proname";
            this.proname.ReadOnly = true;
            this.proname.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.proname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(205)))));
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(448, 392);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(140, 56);
            this.btnOK.TabIndex = 67;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(302, 392);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 56);
            this.btnCancel.TabIndex = 68;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlItem
            // 
            this.pnlItem.Controls.Add(this.lblNum);
            this.pnlItem.Controls.Add(this.lblTotalPrice);
            this.pnlItem.Controls.Add(this.lblSkucode);
            this.pnlItem.Controls.Add(this.lblSkuName);
            this.pnlItem.Location = new System.Drawing.Point(5, 32);
            this.pnlItem.Name = "pnlItem";
            this.pnlItem.Size = new System.Drawing.Size(395, 70);
            this.pnlItem.TabIndex = 72;
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lblNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lblNum.Location = new System.Drawing.Point(286, 37);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(53, 20);
            this.lblNum.TabIndex = 3;
            this.lblNum.Text = "label3";
            // 
            // lblTotalPrice
            // 
            this.lblTotalPrice.AutoSize = true;
            this.lblTotalPrice.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblTotalPrice.ForeColor = System.Drawing.Color.Red;
            this.lblTotalPrice.Location = new System.Drawing.Point(286, 13);
            this.lblTotalPrice.Name = "lblTotalPrice";
            this.lblTotalPrice.Size = new System.Drawing.Size(63, 24);
            this.lblTotalPrice.TabIndex = 2;
            this.lblTotalPrice.Text = "label2";
            // 
            // lblSkucode
            // 
            this.lblSkucode.AutoSize = true;
            this.lblSkucode.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lblSkucode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lblSkucode.Location = new System.Drawing.Point(17, 37);
            this.lblSkucode.Name = "lblSkucode";
            this.lblSkucode.Size = new System.Drawing.Size(53, 20);
            this.lblSkucode.TabIndex = 1;
            this.lblSkucode.Text = "label3";
            // 
            // lblSkuName
            // 
            this.lblSkuName.AutoSize = true;
            this.lblSkuName.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblSkuName.Location = new System.Drawing.Point(17, 13);
            this.lblSkuName.Name = "lblSkuName";
            this.lblSkuName.Size = new System.Drawing.Size(63, 24);
            this.lblSkuName.TabIndex = 0;
            this.lblSkuName.Text = "label2";
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlItem);
            this.pnlControl.Location = new System.Drawing.Point(7, -213);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(589, 137);
            this.pnlControl.TabIndex = 73;
            // 
            // btnCancle
            // 
            this.btnCancle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancle.BackgroundImage")));
            this.btnCancle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancle.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancle.FlatAppearance.BorderSize = 0;
            this.btnCancle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCancle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnCancle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancle.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnCancle.Location = new System.Drawing.Point(560, 12);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(28, 28);
            this.btnCancle.TabIndex = 64;
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // numTxt1
            // 
            this.numTxt1.BackColor = System.Drawing.Color.Gainsboro;
            this.numTxt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numTxt1.DecimalDigits = 2;
            this.numTxt1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numTxt1.Location = new System.Drawing.Point(433, 101);
            this.numTxt1.LockFocus = false;
            this.numTxt1.MaxDeciaml = ((long)(1000000));
            this.numTxt1.MaxLength = 32767;
            this.numTxt1.Name = "numTxt1";
            this.numTxt1.NeedBoard = true;
            this.numTxt1.OnlyNumber = true;
            this.numTxt1.Size = new System.Drawing.Size(150, 40);
            this.numTxt1.TabIndex = 0;
            this.numTxt1.WaterText = "请输入退款金额";
            this.numTxt1.Leave += new System.EventHandler(this.numTxt_Leave);
            // 
            // numTxt2
            // 
            this.numTxt2.BackColor = System.Drawing.Color.Gainsboro;
            this.numTxt2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numTxt2.DecimalDigits = 2;
            this.numTxt2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numTxt2.Location = new System.Drawing.Point(433, 172);
            this.numTxt2.LockFocus = false;
            this.numTxt2.MaxDeciaml = ((long)(1000000));
            this.numTxt2.MaxLength = 32767;
            this.numTxt2.Name = "numTxt2";
            this.numTxt2.NeedBoard = true;
            this.numTxt2.OnlyNumber = true;
            this.numTxt2.Size = new System.Drawing.Size(150, 40);
            this.numTxt2.TabIndex = 75;
            this.numTxt2.WaterText = "请输入退款金额";
            this.numTxt2.Leave += new System.EventHandler(this.numTxt_Leave);
            // 
            // numTxt3
            // 
            this.numTxt3.BackColor = System.Drawing.Color.Gainsboro;
            this.numTxt3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numTxt3.DecimalDigits = 2;
            this.numTxt3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numTxt3.Location = new System.Drawing.Point(433, 243);
            this.numTxt3.LockFocus = false;
            this.numTxt3.MaxDeciaml = ((long)(1000000));
            this.numTxt3.MaxLength = 32767;
            this.numTxt3.Name = "numTxt3";
            this.numTxt3.NeedBoard = true;
            this.numTxt3.OnlyNumber = true;
            this.numTxt3.Size = new System.Drawing.Size(150, 40);
            this.numTxt3.TabIndex = 76;
            this.numTxt3.WaterText = "请输入退款金额";
            this.numTxt3.Leave += new System.EventHandler(this.numTxt_Leave);
            // 
            // numTxt4
            // 
            this.numTxt4.BackColor = System.Drawing.Color.Gainsboro;
            this.numTxt4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numTxt4.DecimalDigits = 2;
            this.numTxt4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numTxt4.Location = new System.Drawing.Point(433, 313);
            this.numTxt4.LockFocus = false;
            this.numTxt4.MaxDeciaml = ((long)(1000000));
            this.numTxt4.MaxLength = 32767;
            this.numTxt4.Name = "numTxt4";
            this.numTxt4.NeedBoard = true;
            this.numTxt4.OnlyNumber = true;
            this.numTxt4.Size = new System.Drawing.Size(150, 40);
            this.numTxt4.TabIndex = 77;
            this.numTxt4.WaterText = "请输入退款金额";
            this.numTxt4.Leave += new System.EventHandler(this.numTxt_Leave);
            // 
            // rbtnPageDown
            // 
            this.rbtnPageDown.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(191)))), ((int)(((byte)(136)))));
            this.rbtnPageDown.BackColor = System.Drawing.Color.Silver;
            this.rbtnPageDown.Image = null;
            this.rbtnPageDown.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnPageDown.Location = new System.Drawing.Point(156, 392);
            this.rbtnPageDown.Name = "rbtnPageDown";
            this.rbtnPageDown.PenColor = System.Drawing.Color.Black;
            this.rbtnPageDown.PenWidth = 1;
            this.rbtnPageDown.RoundRadius = 1;
            this.rbtnPageDown.ShowImg = false;
            this.rbtnPageDown.ShowText = "下一页";
            this.rbtnPageDown.Size = new System.Drawing.Size(140, 56);
            this.rbtnPageDown.TabIndex = 79;
            this.rbtnPageDown.TextForeColor = System.Drawing.Color.White;
            this.rbtnPageDown.WhetherEnable = false;
            this.rbtnPageDown.ButtonClick += new System.EventHandler(this.rbtnPageDown_ButtonClick);
            // 
            // rbtnPageUp
            // 
            this.rbtnPageUp.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(160)))), ((int)(((byte)(110)))));
            this.rbtnPageUp.BackColor = System.Drawing.Color.Silver;
            this.rbtnPageUp.Image = null;
            this.rbtnPageUp.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnPageUp.Location = new System.Drawing.Point(10, 392);
            this.rbtnPageUp.Name = "rbtnPageUp";
            this.rbtnPageUp.PenColor = System.Drawing.Color.Black;
            this.rbtnPageUp.PenWidth = 1;
            this.rbtnPageUp.RoundRadius = 1;
            this.rbtnPageUp.ShowImg = false;
            this.rbtnPageUp.ShowText = "上一页";
            this.rbtnPageUp.Size = new System.Drawing.Size(140, 56);
            this.rbtnPageUp.TabIndex = 78;
            this.rbtnPageUp.TextForeColor = System.Drawing.Color.White;
            this.rbtnPageUp.WhetherEnable = false;
            this.rbtnPageUp.ButtonClick += new System.EventHandler(this.rbtnPageUp_ButtonClick);
            // 
            // FormRefundByAmt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 460);
            this.Controls.Add(this.rbtnPageDown);
            this.Controls.Add(this.rbtnPageUp);
            this.Controls.Add(this.numTxt4);
            this.Controls.Add(this.numTxt3);
            this.Controls.Add(this.numTxt2);
            this.Controls.Add(this.numTxt1);
            this.Controls.Add(this.pnlControl);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgvProduct);
            this.Controls.Add(this.lblCash);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRefundByAmt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormRefundByAmt";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRefundByAmt_FormClosing);
            this.Shown += new System.EventHandler(this.FormRefundByAmt_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            this.pnlItem.ResumeLayout(false);
            this.pnlItem.PerformLayout();
            this.pnlControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Label lblCash;
        private System.Windows.Forms.DataGridView dgvProduct;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlItem;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Label lblTotalPrice;
        private System.Windows.Forms.Label lblSkucode;
        private System.Windows.Forms.Label lblSkuName;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.DataGridViewImageColumn proname;
        private MyControl.NumberTextBox numTxt1;
        private MyControl.NumberTextBox numTxt2;
        private MyControl.NumberTextBox numTxt3;
        private MyControl.NumberTextBox numTxt4;
        private RoundButton rbtnPageDown;
        private RoundButton rbtnPageUp;
    }
}