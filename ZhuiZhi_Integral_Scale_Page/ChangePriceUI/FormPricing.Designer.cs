namespace ZhuiZhi_Integral_Scale_UncleFruit.ChangePriceUI
{
    partial class FormPricing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPricing));
            this.lblPrice = new System.Windows.Forms.Label();
            this.numBoard = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberBoard();
            this.lblStrPrice = new System.Windows.Forms.Label();
            this.rbtnOK = new ZhuiZhi_Integral_Scale_UncleFruit.RoundButton();
            this.btnCancle = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrice = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox();
            this.btnUnitPrice = new System.Windows.Forms.Button();
            this.btnTotalPrice = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPrice.Location = new System.Drawing.Point(90, 190);
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
            this.numBoard.Location = new System.Drawing.Point(8, 218);
            this.numBoard.Margin = new System.Windows.Forms.Padding(0);
            this.numBoard.Name = "numBoard";
            this.numBoard.Size = new System.Drawing.Size(402, 258);
            this.numBoard.TabIndex = 115;
            this.numBoard.Press += new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberBoard.KeyboardHandler(this.MiniKeyboardHandler);
            // 
            // lblStrPrice
            // 
            this.lblStrPrice.AutoSize = true;
            this.lblStrPrice.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStrPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblStrPrice.Location = new System.Drawing.Point(8, 190);
            this.lblStrPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStrPrice.Name = "lblStrPrice";
            this.lblStrPrice.Size = new System.Drawing.Size(78, 23);
            this.lblStrPrice.TabIndex = 114;
            this.lblStrPrice.Text = "当前单价";
            // 
            // rbtnOK
            // 
            this.rbtnOK.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(205)))));
            this.rbtnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(205)))));
            this.rbtnOK.Image = null;
            this.rbtnOK.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnOK.Location = new System.Drawing.Point(18, 488);
            this.rbtnOK.Name = "rbtnOK";
            this.rbtnOK.PenColor = System.Drawing.Color.Black;
            this.rbtnOK.PenWidth = 1;
            this.rbtnOK.RoundRadius = 10;
            this.rbtnOK.ShowImg = false;
            this.rbtnOK.ShowText = "确认";
            this.rbtnOK.Size = new System.Drawing.Size(380, 60);
            this.rbtnOK.TabIndex = 113;
            this.rbtnOK.TextForeColor = System.Drawing.Color.White;
            this.rbtnOK.WhetherEnable = true;
            this.rbtnOK.ButtonClick += new System.EventHandler(this.rbtnOK_ButtonClick);
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
            this.btnCancle.Location = new System.Drawing.Point(375, 7);
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
            this.lblInfo.Location = new System.Drawing.Point(11, 9);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(52, 27);
            this.lblInfo.TabIndex = 49;
            this.lblInfo.Text = "调价";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Location = new System.Drawing.Point(-3, 179);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(429, 10);
            this.panel3.TabIndex = 115;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.label3.Location = new System.Drawing.Point(32, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 19);
            this.label3.TabIndex = 117;
            this.label3.Text = "若不输入金额则视为不调价";
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
            this.btnClear.Location = new System.Drawing.Point(301, 112);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(30, 30);
            this.btnClear.TabIndex = 116;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label2.Location = new System.Drawing.Point(360, 116);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 23);
            this.label2.TabIndex = 115;
            this.label2.Text = "元";
            // 
            // txtPrice
            // 
            this.txtPrice.BackColor = System.Drawing.Color.White;
            this.txtPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrice.DecimalDigits = 3;
            this.txtPrice.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPrice.Location = new System.Drawing.Point(31, 104);
            this.txtPrice.LockFocus = true;
            this.txtPrice.MaxDeciaml = ((long)(100000));
            this.txtPrice.MaxLength = 100;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.NeedBoard = false;
            this.txtPrice.OnlyNumber = true;
            this.txtPrice.Size = new System.Drawing.Size(323, 45);
            this.txtPrice.TabIndex = 0;
            this.txtPrice.WaterText = "请输入价格";
            // 
            // btnUnitPrice
            // 
            this.btnUnitPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(205)))));
            this.btnUnitPrice.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(205)))));
            this.btnUnitPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnitPrice.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUnitPrice.ForeColor = System.Drawing.Color.White;
            this.btnUnitPrice.Location = new System.Drawing.Point(31, 49);
            this.btnUnitPrice.Name = "btnUnitPrice";
            this.btnUnitPrice.Size = new System.Drawing.Size(166, 49);
            this.btnUnitPrice.TabIndex = 118;
            this.btnUnitPrice.Text = "单价调价";
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
            this.btnTotalPrice.Location = new System.Drawing.Point(221, 49);
            this.btnTotalPrice.Name = "btnTotalPrice";
            this.btnTotalPrice.Size = new System.Drawing.Size(166, 49);
            this.btnTotalPrice.TabIndex = 119;
            this.btnTotalPrice.Text = "总价调价";
            this.btnTotalPrice.UseVisualStyleBackColor = false;
            this.btnTotalPrice.Click += new System.EventHandler(this.btnTotalPrice_Click);
            // 
            // FormPricing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(420, 560);
            this.Controls.Add(this.btnTotalPrice);
            this.Controls.Add(this.btnUnitPrice);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numBoard);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblStrPrice);
            this.Controls.Add(this.rbtnOK);
            this.Controls.Add(this.lblInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPricing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormPricing";
            this.Shown += new System.EventHandler(this.FormPricing_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RoundButton rbtnOK;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblStrPrice;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label label2;
        private MyControl.NumberBoard numBoard;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label3;
        private MyControl.NumberTextBox txtPrice;
        private System.Windows.Forms.Button btnUnitPrice;
        private System.Windows.Forms.Button btnTotalPrice;
    }
}