namespace ZhuiZhi_Integral_Scale_UncleFruit.HelperUI
{
    partial class FormNumber
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNumber));
            this.lblg = new System.Windows.Forms.Label();
            this.lblShuiyin = new System.Windows.Forms.Label();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btn00 = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.rbtnOK = new ZhuiZhi_Integral_Scale_UncleFruit.RoundButton();
            this.timerFocus = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblg
            // 
            this.lblg.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.lblg.Location = new System.Drawing.Point(336, 57);
            this.lblg.Name = "lblg";
            this.lblg.Size = new System.Drawing.Size(33, 44);
            this.lblg.TabIndex = 64;
            this.lblg.Text = "g";
            this.lblg.Visible = false;
            // 
            // lblShuiyin
            // 
            this.lblShuiyin.AutoSize = true;
            this.lblShuiyin.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblShuiyin.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.lblShuiyin.ForeColor = System.Drawing.Color.Gray;
            this.lblShuiyin.Location = new System.Drawing.Point(31, 65);
            this.lblShuiyin.Name = "lblShuiyin";
            this.lblShuiyin.Size = new System.Drawing.Size(129, 23);
            this.lblShuiyin.TabIndex = 65;
            this.lblShuiyin.Text = "请输入实收现金";
            this.lblShuiyin.Click += new System.EventHandler(this.lblShuiyin_Click);
            // 
            // txtNum
            // 
            this.txtNum.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNum.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtNum.Location = new System.Drawing.Point(24, 65);
            this.txtNum.Margin = new System.Windows.Forms.Padding(2);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(342, 25);
            this.txtNum.TabIndex = 1;
            this.txtNum.TextChanged += new System.EventHandler(this.txtNum_TextChanged);
            // 
            // btnBack
            // 
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Location = new System.Drawing.Point(19, 58);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(350, 43);
            this.btnBack.TabIndex = 67;
            this.btnBack.UseVisualStyleBackColor = true;
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
            this.btnCancle.Location = new System.Drawing.Point(342, 12);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(26, 26);
            this.btnCancle.TabIndex = 62;
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnDel
            // 
            this.btnDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDel.BackgroundImage")));
            this.btnDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDel.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btnDel.Location = new System.Drawing.Point(247, 319);
            this.btnDel.Margin = new System.Windows.Forms.Padding(2);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(115, 68);
            this.btnDel.TabIndex = 61;
            this.btnDel.TabStop = false;
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btn00
            // 
            this.btn00.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btn00.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn00.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btn00.Location = new System.Drawing.Point(133, 319);
            this.btn00.Margin = new System.Windows.Forms.Padding(2);
            this.btn00.Name = "btn00";
            this.btn00.Size = new System.Drawing.Size(115, 68);
            this.btn00.TabIndex = 60;
            this.btn00.TabStop = false;
            this.btn00.Text = "00";
            this.btn00.UseVisualStyleBackColor = true;
            this.btn00.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn0
            // 
            this.btn0.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btn0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn0.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btn0.Location = new System.Drawing.Point(19, 319);
            this.btn0.Margin = new System.Windows.Forms.Padding(2);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(115, 68);
            this.btn0.TabIndex = 59;
            this.btn0.TabStop = false;
            this.btn0.Text = "0";
            this.btn0.UseVisualStyleBackColor = true;
            this.btn0.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn9
            // 
            this.btn9.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btn9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn9.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btn9.Location = new System.Drawing.Point(247, 252);
            this.btn9.Margin = new System.Windows.Forms.Padding(2);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(115, 68);
            this.btn9.TabIndex = 58;
            this.btn9.TabStop = false;
            this.btn9.Text = "9";
            this.btn9.UseVisualStyleBackColor = true;
            this.btn9.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn8
            // 
            this.btn8.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btn8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn8.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btn8.Location = new System.Drawing.Point(133, 252);
            this.btn8.Margin = new System.Windows.Forms.Padding(2);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(115, 68);
            this.btn8.TabIndex = 57;
            this.btn8.TabStop = false;
            this.btn8.Text = "8";
            this.btn8.UseVisualStyleBackColor = true;
            this.btn8.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn7
            // 
            this.btn7.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btn7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn7.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btn7.Location = new System.Drawing.Point(19, 252);
            this.btn7.Margin = new System.Windows.Forms.Padding(2);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(115, 68);
            this.btn7.TabIndex = 56;
            this.btn7.TabStop = false;
            this.btn7.Text = "7";
            this.btn7.UseVisualStyleBackColor = true;
            this.btn7.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn6
            // 
            this.btn6.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btn6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn6.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btn6.Location = new System.Drawing.Point(247, 185);
            this.btn6.Margin = new System.Windows.Forms.Padding(2);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(115, 68);
            this.btn6.TabIndex = 55;
            this.btn6.TabStop = false;
            this.btn6.Text = "6";
            this.btn6.UseVisualStyleBackColor = true;
            this.btn6.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn5
            // 
            this.btn5.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn5.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btn5.Location = new System.Drawing.Point(133, 185);
            this.btn5.Margin = new System.Windows.Forms.Padding(2);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(115, 68);
            this.btn5.TabIndex = 54;
            this.btn5.TabStop = false;
            this.btn5.Text = "5";
            this.btn5.UseVisualStyleBackColor = true;
            this.btn5.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn4
            // 
            this.btn4.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn4.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btn4.Location = new System.Drawing.Point(19, 185);
            this.btn4.Margin = new System.Windows.Forms.Padding(2);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(115, 68);
            this.btn4.TabIndex = 53;
            this.btn4.TabStop = false;
            this.btn4.Text = "4";
            this.btn4.UseVisualStyleBackColor = true;
            this.btn4.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn3
            // 
            this.btn3.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn3.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btn3.Location = new System.Drawing.Point(247, 118);
            this.btn3.Margin = new System.Windows.Forms.Padding(2);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(115, 68);
            this.btn3.TabIndex = 52;
            this.btn3.TabStop = false;
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn2
            // 
            this.btn2.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn2.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btn2.Location = new System.Drawing.Point(133, 118);
            this.btn2.Margin = new System.Windows.Forms.Padding(2);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(115, 68);
            this.btn2.TabIndex = 51;
            this.btn2.TabStop = false;
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn1
            // 
            this.btn1.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn1.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btn1.Location = new System.Drawing.Point(19, 118);
            this.btn1.Margin = new System.Windows.Forms.Padding(2);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(115, 68);
            this.btn1.TabIndex = 50;
            this.btn1.TabStop = false;
            this.btn1.Text = "1";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Silver;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(-1, 290);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(343, 60);
            this.btnOK.TabIndex = 49;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "确 定";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Visible = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.lblInfo.Location = new System.Drawing.Point(22, 22);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(112, 23);
            this.lblInfo.TabIndex = 48;
            this.lblInfo.Text = "输入商品条码";
            // 
            // rbtnOK
            // 
            this.rbtnOK.AllBackColor = System.Drawing.Color.LightGray;
            this.rbtnOK.BackColor = System.Drawing.Color.LightGray;
            this.rbtnOK.Image = null;
            this.rbtnOK.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnOK.Location = new System.Drawing.Point(19, 401);
            this.rbtnOK.Name = "rbtnOK";
            this.rbtnOK.PenColor = System.Drawing.Color.Black;
            this.rbtnOK.PenWidth = 1;
            this.rbtnOK.RoundRadius = 10;
            this.rbtnOK.ShowImg = false;
            this.rbtnOK.ShowText = "确认";
            this.rbtnOK.Size = new System.Drawing.Size(343, 60);
            this.rbtnOK.TabIndex = 68;
            this.rbtnOK.TextForeColor = System.Drawing.Color.White;
            this.rbtnOK.WhetherEnable = true;
            this.rbtnOK.ButtonClick += new System.EventHandler(this.btnOK_Click);
            // 
            // timerFocus
            // 
            this.timerFocus.Interval = 1000;
            this.timerFocus.Tick += new System.EventHandler(this.timerFocus_Tick);
            // 
            // FormNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 480);
            this.Controls.Add(this.rbtnOK);
            this.Controls.Add(this.lblg);
            this.Controls.Add(this.lblShuiyin);
            this.Controls.Add(this.txtNum);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btn00);
            this.Controls.Add(this.btn0);
            this.Controls.Add(this.btn9);
            this.Controls.Add(this.btn8);
            this.Controls.Add(this.btn7);
            this.Controls.Add(this.btn6);
            this.Controls.Add(this.btn5);
            this.Controls.Add(this.btn4);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormNumber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormNumber";
            this.Activated += new System.EventHandler(this.FormNumber_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormNumber_FormClosing);
            this.Resize += new System.EventHandler(this.FormNumber_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblg;
        private System.Windows.Forms.Label lblShuiyin;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btn00;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblInfo;
        private RoundButton rbtnOK;
        private System.Windows.Forms.Timer timerFocus;
    }
}