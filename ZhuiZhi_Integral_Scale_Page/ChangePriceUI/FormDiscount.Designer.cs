namespace ZhuiZhi_Integral_Scale_UncleFruit.ChangePriceUI
{
    partial class FormDiscount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiscount));
            this.pnlNumber = new System.Windows.Forms.Panel();
            this.lblDiscountPrice = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.numBoard = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberBoard();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtnOK = new ZhuiZhi_Integral_Scale_UncleFruit.RoundButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancle = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.picNotSelect = new System.Windows.Forms.PictureBox();
            this.picSelect = new System.Windows.Forms.PictureBox();
            this.btn9 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblShuiyin = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlNumber.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNotSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSelect)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlNumber
            // 
            this.pnlNumber.BackColor = System.Drawing.Color.White;
            this.pnlNumber.Controls.Add(this.lblDiscountPrice);
            this.pnlNumber.Controls.Add(this.label4);
            this.pnlNumber.Controls.Add(this.lblPrice);
            this.pnlNumber.Controls.Add(this.numBoard);
            this.pnlNumber.Controls.Add(this.label1);
            this.pnlNumber.Controls.Add(this.rbtnOK);
            this.pnlNumber.Location = new System.Drawing.Point(5, 172);
            this.pnlNumber.Name = "pnlNumber";
            this.pnlNumber.Size = new System.Drawing.Size(410, 380);
            this.pnlNumber.TabIndex = 116;
            // 
            // lblDiscountPrice
            // 
            this.lblDiscountPrice.AutoSize = true;
            this.lblDiscountPrice.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblDiscountPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(121)))), ((int)(((byte)(49)))));
            this.lblDiscountPrice.Location = new System.Drawing.Point(260, 11);
            this.lblDiscountPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDiscountPrice.Name = "lblDiscountPrice";
            this.lblDiscountPrice.Size = new System.Drawing.Size(73, 27);
            this.lblDiscountPrice.TabIndex = 118;
            this.lblDiscountPrice.Text = "￥0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label4.Location = new System.Drawing.Point(182, 14);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 23);
            this.label4.TabIndex = 117;
            this.label4.Text = "折后单价";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPrice.Location = new System.Drawing.Point(86, 14);
            this.lblPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(61, 23);
            this.lblPrice.TabIndex = 116;
            this.lblPrice.Text = "￥0.00";
            // 
            // numBoard
            // 
            this.numBoard.BackColor = System.Drawing.Color.White;
            this.numBoard.KeyClear = null;
            this.numBoard.KeyDelete = "";
            this.numBoard.KeyDot = "·";
            this.numBoard.KeyEnter = null;
            this.numBoard.KeyHide = null;
            this.numBoard.Location = new System.Drawing.Point(4, 49);
            this.numBoard.Margin = new System.Windows.Forms.Padding(0);
            this.numBoard.Name = "numBoard";
            this.numBoard.Size = new System.Drawing.Size(394, 250);
            this.numBoard.TabIndex = 115;
            this.numBoard.Press += new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberBoard.KeyboardHandler(this.MiniKeyboardHandler);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 23);
            this.label1.TabIndex = 114;
            this.label1.Text = "当前单价";
            // 
            // rbtnOK
            // 
            this.rbtnOK.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(205)))));
            this.rbtnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(205)))));
            this.rbtnOK.Image = null;
            this.rbtnOK.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnOK.Location = new System.Drawing.Point(13, 309);
            this.rbtnOK.Name = "rbtnOK";
            this.rbtnOK.PenColor = System.Drawing.Color.Black;
            this.rbtnOK.PenWidth = 1;
            this.rbtnOK.RoundRadius = 10;
            this.rbtnOK.ShowImg = false;
            this.rbtnOK.ShowText = "确认";
            this.rbtnOK.Size = new System.Drawing.Size(383, 60);
            this.rbtnOK.TabIndex = 113;
            this.rbtnOK.TextForeColor = System.Drawing.Color.White;
            this.rbtnOK.WhetherEnable = true;
            this.rbtnOK.ButtonClick += new System.EventHandler(this.rbtnOK_ButtonClick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btnCancle);
            this.panel2.Controls.Add(this.lblInfo);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(420, 60);
            this.panel2.TabIndex = 117;
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
            this.btnCancle.Location = new System.Drawing.Point(373, 16);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(30, 30);
            this.btnCancle.TabIndex = 63;
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblInfo.Location = new System.Drawing.Point(11, 16);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(52, 27);
            this.lblInfo.TabIndex = 49;
            this.lblInfo.Text = "折让";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.btnClear);
            this.panel3.Controls.Add(this.btn5);
            this.panel3.Controls.Add(this.btn6);
            this.panel3.Controls.Add(this.btn7);
            this.panel3.Controls.Add(this.btn8);
            this.panel3.Controls.Add(this.picNotSelect);
            this.panel3.Controls.Add(this.picSelect);
            this.panel3.Controls.Add(this.btn9);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.lblShuiyin);
            this.panel3.Controls.Add(this.txtPrice);
            this.panel3.Controls.Add(this.btnBack);
            this.panel3.Location = new System.Drawing.Point(5, 66);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(410, 100);
            this.panel3.TabIndex = 118;
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnClear.Location = new System.Drawing.Point(296, 17);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(30, 30);
            this.btnClear.TabIndex = 123;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btn5
            // 
            this.btn5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn5.BackgroundImage")));
            this.btn5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn5.FlatAppearance.BorderSize = 0;
            this.btn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn5.Location = new System.Drawing.Point(241, 62);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(50, 25);
            this.btn5.TabIndex = 122;
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
            this.btn6.Location = new System.Drawing.Point(184, 62);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(50, 25);
            this.btn6.TabIndex = 121;
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
            this.btn7.Location = new System.Drawing.Point(127, 62);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(50, 25);
            this.btn7.TabIndex = 120;
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
            this.btn8.Location = new System.Drawing.Point(70, 62);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(50, 25);
            this.btn8.TabIndex = 119;
            this.btn8.Tag = "8";
            this.btn8.Text = "8折";
            this.btn8.UseVisualStyleBackColor = true;
            this.btn8.Click += new System.EventHandler(this.btnDiscount_Click);
            // 
            // picNotSelect
            // 
            this.picNotSelect.Image = ((System.Drawing.Image)(resources.GetObject("picNotSelect.Image")));
            this.picNotSelect.Location = new System.Drawing.Point(310, 62);
            this.picNotSelect.Name = "picNotSelect";
            this.picNotSelect.Size = new System.Drawing.Size(23, 33);
            this.picNotSelect.TabIndex = 118;
            this.picNotSelect.TabStop = false;
            this.picNotSelect.Visible = false;
            // 
            // picSelect
            // 
            this.picSelect.Image = ((System.Drawing.Image)(resources.GetObject("picSelect.Image")));
            this.picSelect.Location = new System.Drawing.Point(342, 62);
            this.picSelect.Name = "picSelect";
            this.picSelect.Size = new System.Drawing.Size(20, 33);
            this.picSelect.TabIndex = 117;
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
            this.btn9.Location = new System.Drawing.Point(13, 62);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(50, 25);
            this.btn9.TabIndex = 116;
            this.btn9.Tag = "9";
            this.btn9.Text = "9折";
            this.btn9.UseVisualStyleBackColor = true;
            this.btn9.Click += new System.EventHandler(this.btnDiscount_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label2.Location = new System.Drawing.Point(338, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 23);
            this.label2.TabIndex = 115;
            this.label2.Text = "折";
            // 
            // lblShuiyin
            // 
            this.lblShuiyin.AutoSize = true;
            this.lblShuiyin.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblShuiyin.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.lblShuiyin.ForeColor = System.Drawing.Color.Gray;
            this.lblShuiyin.Location = new System.Drawing.Point(22, 21);
            this.lblShuiyin.Name = "lblShuiyin";
            this.lblShuiyin.Size = new System.Drawing.Size(95, 23);
            this.lblShuiyin.TabIndex = 68;
            this.lblShuiyin.Text = "请输入折让";
            this.lblShuiyin.Click += new System.EventHandler(this.lblShuiyin_Click);
            // 
            // txtPrice
            // 
            this.txtPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPrice.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.txtPrice.Location = new System.Drawing.Point(15, 19);
            this.txtPrice.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(276, 29);
            this.txtPrice.TabIndex = 69;
            this.txtPrice.TextChanged += new System.EventHandler(this.txtPrice_TextChanged);
            // 
            // btnBack
            // 
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Location = new System.Drawing.Point(10, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(323, 43);
            this.btnBack.TabIndex = 70;
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // FormDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 560);
            this.Controls.Add(this.pnlNumber);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDiscount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormDiscount";
            this.Shown += new System.EventHandler(this.FormDiscount_Shown);
            this.pnlNumber.ResumeLayout(false);
            this.pnlNumber.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNotSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSelect)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlNumber;
        private System.Windows.Forms.Label lblPrice;
        private MyControl.NumberBoard numBoard;
        private System.Windows.Forms.Label label1;
        private RoundButton rbtnOK;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblShuiyin;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblDiscountPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.PictureBox picNotSelect;
        private System.Windows.Forms.PictureBox picSelect;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btnClear;
    }
}