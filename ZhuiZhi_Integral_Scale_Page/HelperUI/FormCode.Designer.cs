namespace ZhuiZhi_Integral_Scale_UncleFruit.HelperUI
{
    partial class FormCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCode));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.timerFocus = new System.Windows.Forms.Timer(this.components);
            this.txtCode = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.keyBoardAll1 = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.KeyBoardAll();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(205)))));
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(26, 158);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(377, 58);
            this.btnOK.TabIndex = 73;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
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
            this.btnCancel.Location = new System.Drawing.Point(387, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(30, 30);
            this.btnCancel.TabIndex = 70;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(22, 33);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(88, 25);
            this.lblTitle.TabIndex = 69;
            this.lblTitle.Text = "输入编码";
            // 
            // timerFocus
            // 
            this.timerFocus.Interval = 1000;
            this.timerFocus.Tick += new System.EventHandler(this.timerFocus_Tick);
            // 
            // txtCode
            // 
            this.txtCode.BackColor = System.Drawing.Color.White;
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCode.DecimalDigits = 0;
            this.txtCode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCode.Location = new System.Drawing.Point(26, 86);
            this.txtCode.LockFocus = true;
            this.txtCode.MaxDeciaml = ((long)(0));
            this.txtCode.MaxLength = 100;
            this.txtCode.Name = "txtCode";
            this.txtCode.NeedBoard = false;
            this.txtCode.OnlyNumber = false;
            this.txtCode.Size = new System.Drawing.Size(377, 53);
            this.txtCode.TabIndex = 74;
            this.txtCode.WaterText = "输入会员账号";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Location = new System.Drawing.Point(363, 121);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(431, 228);
            this.panel1.TabIndex = 75;
            // 
            // keyBoardAll1
            // 
            this.keyBoardAll1.AllowHide = false;
            this.keyBoardAll1.CharType = ZhuiZhi_Integral_Scale_UncleFruit.MyControl.KeyBorderCharType.NUMBER;
            this.keyBoardAll1.Location = new System.Drawing.Point(6, 468);
            this.keyBoardAll1.Name = "keyBoardAll1";
            this.keyBoardAll1.Size = new System.Drawing.Size(1165, 289);
            this.keyBoardAll1.TabIndex = 76;
            // 
            // FormCode
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.Controls.Add(this.keyBoardAll1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Name = "FormCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormVoucher";
            this.TransparencyKey = System.Drawing.Color.DarkGray;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormVoucher_FormClosing);
            this.Shown += new System.EventHandler(this.FormCode_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Timer timerFocus;
        private System.Windows.Forms.Button btnOK;
        private MyControl.NumberTextBox txtCode;
        private System.Windows.Forms.Panel panel1;
        private MyControl.KeyBoardAll keyBoardAll1;
    }
}