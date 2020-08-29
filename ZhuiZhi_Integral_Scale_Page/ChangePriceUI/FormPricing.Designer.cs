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
            this.pnlNumber = new System.Windows.Forms.Panel();
            this.lblPrice = new System.Windows.Forms.Label();
            this.numBoard = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberBoard();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtnOK = new ZhuiZhi_Integral_Scale_UncleFruit.RoundButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancle = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrice = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox();
            this.pnlNumber.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlNumber
            // 
            this.pnlNumber.BackColor = System.Drawing.Color.White;
            this.pnlNumber.Controls.Add(this.lblPrice);
            this.pnlNumber.Controls.Add(this.numBoard);
            this.pnlNumber.Controls.Add(this.label1);
            this.pnlNumber.Controls.Add(this.rbtnOK);
            this.pnlNumber.Location = new System.Drawing.Point(5, 159);
            this.pnlNumber.Name = "pnlNumber";
            this.pnlNumber.Size = new System.Drawing.Size(410, 393);
            this.pnlNumber.TabIndex = 113;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPrice.Location = new System.Drawing.Point(88, 16);
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
            this.numBoard.Location = new System.Drawing.Point(4, 50);
            this.numBoard.Margin = new System.Windows.Forms.Padding(0);
            this.numBoard.Name = "numBoard";
            this.numBoard.Size = new System.Drawing.Size(402, 258);
            this.numBoard.TabIndex = 115;
            this.numBoard.Press += new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberBoard.KeyboardHandler(this.MiniKeyboardHandler);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label1.Location = new System.Drawing.Point(6, 16);
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
            this.rbtnOK.Location = new System.Drawing.Point(14, 320);
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btnCancle);
            this.panel2.Controls.Add(this.lblInfo);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(420, 60);
            this.panel2.TabIndex = 114;
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
            this.btnCancle.Location = new System.Drawing.Point(375, 14);
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
            this.lblInfo.Text = "调价";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.btnClear);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtPrice);
            this.panel3.Location = new System.Drawing.Point(5, 66);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(410, 87);
            this.panel3.TabIndex = 115;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.label3.Location = new System.Drawing.Point(11, 59);
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
            this.btnClear.Location = new System.Drawing.Point(280, 18);
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
            this.label2.Location = new System.Drawing.Point(339, 22);
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
            this.txtPrice.Location = new System.Drawing.Point(10, 10);
            this.txtPrice.LockFocus = true;
            this.txtPrice.MaxDeciaml = ((long)(100000));
            this.txtPrice.MaxLength = 100;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.OnlyNumber = true;
            this.txtPrice.Size = new System.Drawing.Size(323, 45);
            this.txtPrice.TabIndex = 0;
            this.txtPrice.WaterText = "请输入价格";
            // 
            // FormPricing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 560);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPricing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormPricing";
            this.Shown += new System.EventHandler(this.FormPricing_Shown);
            this.pnlNumber.ResumeLayout(false);
            this.pnlNumber.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlNumber;
        private RoundButton rbtnOK;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label label2;
        private MyControl.NumberBoard numBoard;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label3;
        private MyControl.NumberTextBox txtPrice;
    }
}