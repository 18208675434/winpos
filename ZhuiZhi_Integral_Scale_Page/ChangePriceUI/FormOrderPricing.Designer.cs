namespace ZhuiZhi_Integral_Scale_UncleFruit.ChangePriceUI
{
    partial class FormOrderPricing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOrderPricing));
            this.numBoard = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberBoard();
            this.rbtnOK = new ZhuiZhi_Integral_Scale_UncleFruit.RoundButton();
            this.lblInfo = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblType = new System.Windows.Forms.Label();
            this.txtPrice = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox();
            this.btnUnitPrice = new System.Windows.Forms.Button();
            this.btnTotalPrice = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.picNotSelect = new System.Windows.Forms.PictureBox();
            this.picSelect = new System.Windows.Forms.PictureBox();
            this.btn9 = new System.Windows.Forms.Button();
            this.lblDiscountPrice = new System.Windows.Forms.Label();
            this.lblStrDiscount = new System.Windows.Forms.Label();
            this.lblBeforeFixTotal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlBtnDiscount = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picNotSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSelect)).BeginInit();
            this.pnlBtnDiscount.SuspendLayout();
            this.SuspendLayout();
            // 
            // numBoard
            // 
            this.numBoard.BackColor = System.Drawing.Color.White;
            this.numBoard.KeyClear = null;
            this.numBoard.KeyDelete = "";
            this.numBoard.KeyDot = "·";
            this.numBoard.KeyEnter = null;
            this.numBoard.KeyHide = null;
            this.numBoard.Location = new System.Drawing.Point(8, 253);
            this.numBoard.Margin = new System.Windows.Forms.Padding(0);
            this.numBoard.Name = "numBoard";
            this.numBoard.Size = new System.Drawing.Size(483, 258);
            this.numBoard.TabIndex = 115;
            this.numBoard.Press += new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberBoard.KeyboardHandler(this.MiniKeyboardHandler);
            // 
            // rbtnOK
            // 
            this.rbtnOK.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(205)))));
            this.rbtnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(205)))));
            this.rbtnOK.Image = null;
            this.rbtnOK.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnOK.Location = new System.Drawing.Point(8, 521);
            this.rbtnOK.Name = "rbtnOK";
            this.rbtnOK.PenColor = System.Drawing.Color.Black;
            this.rbtnOK.PenWidth = 1;
            this.rbtnOK.RoundRadius = 10;
            this.rbtnOK.ShowImg = false;
            this.rbtnOK.ShowText = "确认";
            this.rbtnOK.Size = new System.Drawing.Size(480, 69);
            this.rbtnOK.TabIndex = 113;
            this.rbtnOK.TextForeColor = System.Drawing.Color.White;
            this.rbtnOK.WhetherEnable = true;
            this.rbtnOK.ButtonClick += new System.EventHandler(this.rbtnOK_ButtonClick);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblInfo.Location = new System.Drawing.Point(11, 9);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(52, 27);
            this.lblInfo.TabIndex = 49;
            this.lblInfo.Text = "修改";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Location = new System.Drawing.Point(-3, 203);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(507, 10);
            this.panel3.TabIndex = 115;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblType.Location = new System.Drawing.Point(436, 125);
            this.lblType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(27, 23);
            this.lblType.TabIndex = 115;
            this.lblType.Text = "折";
            // 
            // txtPrice
            // 
            this.txtPrice.BackColor = System.Drawing.Color.White;
            this.txtPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrice.DecimalDigits = 4;
            this.txtPrice.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPrice.Location = new System.Drawing.Point(26, 112);
            this.txtPrice.LockFocus = true;
            this.txtPrice.MaxDeciaml = ((long)(10000000));
            this.txtPrice.MaxLength = 100;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.NeedBoard = false;
            this.txtPrice.OnlyNumber = true;
            this.txtPrice.Size = new System.Drawing.Size(405, 45);
            this.txtPrice.TabIndex = 0;
            this.txtPrice.WaterText = "请输入价格";
            this.txtPrice.DataChanged += new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox.DataRecHandleDelegate(this.txtPrice_DataChanged);
            // 
            // btnUnitPrice
            // 
            this.btnUnitPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(205)))));
            this.btnUnitPrice.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(205)))));
            this.btnUnitPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnitPrice.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUnitPrice.ForeColor = System.Drawing.Color.White;
            this.btnUnitPrice.Location = new System.Drawing.Point(26, 49);
            this.btnUnitPrice.Name = "btnUnitPrice";
            this.btnUnitPrice.Size = new System.Drawing.Size(191, 50);
            this.btnUnitPrice.TabIndex = 118;
            this.btnUnitPrice.Text = "整单折扣";
            this.btnUnitPrice.UseVisualStyleBackColor = false;
            this.btnUnitPrice.Click += new System.EventHandler(this.btnUnitPrice_Click);
            // 
            // btnTotalPrice
            // 
            this.btnTotalPrice.BackColor = System.Drawing.Color.White;
            this.btnTotalPrice.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(205)))));
            this.btnTotalPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTotalPrice.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTotalPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(205)))));
            this.btnTotalPrice.Location = new System.Drawing.Point(240, 49);
            this.btnTotalPrice.Name = "btnTotalPrice";
            this.btnTotalPrice.Size = new System.Drawing.Size(191, 50);
            this.btnTotalPrice.TabIndex = 119;
            this.btnTotalPrice.Text = "整单改价";
            this.btnTotalPrice.UseVisualStyleBackColor = false;
            this.btnTotalPrice.Click += new System.EventHandler(this.btnTotalPrice_Click);
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
            this.btnCancle.Location = new System.Drawing.Point(458, 6);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(30, 30);
            this.btnCancle.TabIndex = 63;
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btn5
            // 
            this.btn5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn5.BackgroundImage")));
            this.btn5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn5.FlatAppearance.BorderSize = 0;
            this.btn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn5.Location = new System.Drawing.Point(275, 3);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(60, 30);
            this.btn5.TabIndex = 129;
            this.btn5.Tag = "5";
            this.btn5.Text = "5折";
            this.btn5.UseVisualStyleBackColor = true;
            this.btn5.Click += new System.EventHandler(this.btnDiscount_Click);
            // 
            // btn6
            // 
            this.btn6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn6.BackgroundImage")));
            this.btn6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn6.FlatAppearance.BorderSize = 0;
            this.btn6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn6.Location = new System.Drawing.Point(209, 3);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(60, 30);
            this.btn6.TabIndex = 128;
            this.btn6.Tag = "6";
            this.btn6.Text = "6折";
            this.btn6.UseVisualStyleBackColor = true;
            this.btn6.Click += new System.EventHandler(this.btnDiscount_Click);
            // 
            // btn7
            // 
            this.btn7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn7.BackgroundImage")));
            this.btn7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn7.FlatAppearance.BorderSize = 0;
            this.btn7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn7.Location = new System.Drawing.Point(143, 3);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(60, 30);
            this.btn7.TabIndex = 127;
            this.btn7.Tag = "7";
            this.btn7.Text = "7折";
            this.btn7.UseVisualStyleBackColor = true;
            this.btn7.Click += new System.EventHandler(this.btnDiscount_Click);
            // 
            // btn8
            // 
            this.btn8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn8.BackgroundImage")));
            this.btn8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn8.FlatAppearance.BorderSize = 0;
            this.btn8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn8.Location = new System.Drawing.Point(77, 3);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(60, 30);
            this.btn8.TabIndex = 126;
            this.btn8.Tag = "8";
            this.btn8.Text = "8折";
            this.btn8.UseVisualStyleBackColor = true;
            this.btn8.Click += new System.EventHandler(this.btnDiscount_Click);
            // 
            // picNotSelect
            // 
            this.picNotSelect.Image = ((System.Drawing.Image)(resources.GetObject("picNotSelect.Image")));
            this.picNotSelect.Location = new System.Drawing.Point(359, 8);
            this.picNotSelect.Name = "picNotSelect";
            this.picNotSelect.Size = new System.Drawing.Size(57, 25);
            this.picNotSelect.TabIndex = 125;
            this.picNotSelect.TabStop = false;
            this.picNotSelect.Visible = false;
            // 
            // picSelect
            // 
            this.picSelect.Image = ((System.Drawing.Image)(resources.GetObject("picSelect.Image")));
            this.picSelect.Location = new System.Drawing.Point(437, 168);
            this.picSelect.Name = "picSelect";
            this.picSelect.Size = new System.Drawing.Size(54, 25);
            this.picSelect.TabIndex = 124;
            this.picSelect.TabStop = false;
            this.picSelect.Visible = false;
            // 
            // btn9
            // 
            this.btn9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn9.BackgroundImage")));
            this.btn9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn9.FlatAppearance.BorderSize = 0;
            this.btn9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn9.Location = new System.Drawing.Point(11, 3);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(60, 30);
            this.btn9.TabIndex = 123;
            this.btn9.Tag = "9";
            this.btn9.Text = "9折";
            this.btn9.UseVisualStyleBackColor = true;
            this.btn9.Click += new System.EventHandler(this.btnDiscount_Click);
            // 
            // lblDiscountPrice
            // 
            this.lblDiscountPrice.AutoSize = true;
            this.lblDiscountPrice.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblDiscountPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(121)))), ((int)(((byte)(49)))));
            this.lblDiscountPrice.Location = new System.Drawing.Point(258, 219);
            this.lblDiscountPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDiscountPrice.Name = "lblDiscountPrice";
            this.lblDiscountPrice.Size = new System.Drawing.Size(73, 27);
            this.lblDiscountPrice.TabIndex = 133;
            this.lblDiscountPrice.Text = "￥0.00";
            // 
            // lblStrDiscount
            // 
            this.lblStrDiscount.AutoSize = true;
            this.lblStrDiscount.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStrDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblStrDiscount.Location = new System.Drawing.Point(180, 222);
            this.lblStrDiscount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStrDiscount.Name = "lblStrDiscount";
            this.lblStrDiscount.Size = new System.Drawing.Size(78, 23);
            this.lblStrDiscount.TabIndex = 132;
            this.lblStrDiscount.Text = "折后应收";
            // 
            // lblBeforeFixTotal
            // 
            this.lblBeforeFixTotal.AutoSize = true;
            this.lblBeforeFixTotal.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBeforeFixTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblBeforeFixTotal.Location = new System.Drawing.Point(84, 222);
            this.lblBeforeFixTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBeforeFixTotal.Name = "lblBeforeFixTotal";
            this.lblBeforeFixTotal.Size = new System.Drawing.Size(61, 23);
            this.lblBeforeFixTotal.TabIndex = 131;
            this.lblBeforeFixTotal.Text = "￥0.00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label2.Location = new System.Drawing.Point(4, 222);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 23);
            this.label2.TabIndex = 130;
            this.label2.Text = "当前应收";
            // 
            // pnlBtnDiscount
            // 
            this.pnlBtnDiscount.Controls.Add(this.btn5);
            this.pnlBtnDiscount.Controls.Add(this.btn6);
            this.pnlBtnDiscount.Controls.Add(this.btn7);
            this.pnlBtnDiscount.Controls.Add(this.btn8);
            this.pnlBtnDiscount.Controls.Add(this.picNotSelect);
            this.pnlBtnDiscount.Controls.Add(this.btn9);
            this.pnlBtnDiscount.Location = new System.Drawing.Point(15, 160);
            this.pnlBtnDiscount.Name = "pnlBtnDiscount";
            this.pnlBtnDiscount.Size = new System.Drawing.Size(475, 43);
            this.pnlBtnDiscount.TabIndex = 134;
            // 
            // FormOrderPricing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 600);
            this.Controls.Add(this.pnlBtnDiscount);
            this.Controls.Add(this.lblDiscountPrice);
            this.Controls.Add(this.lblStrDiscount);
            this.Controls.Add(this.lblBeforeFixTotal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picSelect);
            this.Controls.Add(this.btnTotalPrice);
            this.Controls.Add(this.btnUnitPrice);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.numBoard);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.rbtnOK);
            this.Controls.Add(this.lblInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormOrderPricing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormPricing";
            this.Shown += new System.EventHandler(this.FormPricing_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picNotSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSelect)).EndInit();
            this.pnlBtnDiscount.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RoundButton rbtnOK;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblType;
        private MyControl.NumberBoard numBoard;
        private MyControl.NumberTextBox txtPrice;
        private System.Windows.Forms.Button btnUnitPrice;
        private System.Windows.Forms.Button btnTotalPrice;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.PictureBox picNotSelect;
        private System.Windows.Forms.PictureBox picSelect;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Label lblDiscountPrice;
        private System.Windows.Forms.Label lblStrDiscount;
        private System.Windows.Forms.Label lblBeforeFixTotal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlBtnDiscount;
    }
}