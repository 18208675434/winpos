namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    partial class FormEditMember
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditMember));
            this.panel1 = new System.Windows.Forms.Panel();
            this.picBirthday = new System.Windows.Forms.PictureBox();
            this.btnCancle = new System.Windows.Forms.Button();
            this.picSelect = new System.Windows.Forms.PictureBox();
            this.picNotSelect = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlWoman = new System.Windows.Forms.Panel();
            this.lblWoman = new System.Windows.Forms.Label();
            this.picWoman = new System.Windows.Forms.PictureBox();
            this.pnlMan = new System.Windows.Forms.Panel();
            this.lblMan = new System.Windows.Forms.Label();
            this.picMan = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBirthday = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBirthday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNotSelect)).BeginInit();
            this.pnlWoman.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWoman)).BeginInit();
            this.pnlMan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMan)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.picBirthday);
            this.panel1.Controls.Add(this.btnCancle);
            this.panel1.Controls.Add(this.picSelect);
            this.panel1.Controls.Add(this.picNotSelect);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pnlWoman);
            this.panel1.Controls.Add(this.pnlMan);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtBirthday);
           
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtName);

            this.panel1.Controls.Add(this.dtStart);
            this.panel1.Location = new System.Drawing.Point(397, 162);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(417, 373);
            this.panel1.TabIndex = 0;
            // 
            // picBirthday
            // 
            this.picBirthday.BackgroundImage = global::ZhuiZhi_Integral_Scale_UncleFruit.Properties.Resources.日期;
            this.picBirthday.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBirthday.Location = new System.Drawing.Point(350, 145);
            this.picBirthday.Name = "picBirthday";
            this.picBirthday.Size = new System.Drawing.Size(40, 30);
            this.picBirthday.TabIndex = 98;
            this.picBirthday.TabStop = false;
            this.picBirthday.Click += new System.EventHandler(this.txtBirthday_Enter);
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
            this.btnCancle.Location = new System.Drawing.Point(376, 11);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(30, 30);
            this.btnCancle.TabIndex = 97;
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // picSelect
            // 
            this.picSelect.Image = ((System.Drawing.Image)(resources.GetObject("picSelect.Image")));
            this.picSelect.Location = new System.Drawing.Point(244, 22);
            this.picSelect.Name = "picSelect";
            this.picSelect.Size = new System.Drawing.Size(36, 28);
            this.picSelect.TabIndex = 96;
            this.picSelect.TabStop = false;
            this.picSelect.Visible = false;
            // 
            // picNotSelect
            // 
            this.picNotSelect.Image = ((System.Drawing.Image)(resources.GetObject("picNotSelect.Image")));
            this.picNotSelect.Location = new System.Drawing.Point(191, 22);
            this.picNotSelect.Name = "picNotSelect";
            this.picNotSelect.Size = new System.Drawing.Size(36, 28);
            this.picNotSelect.TabIndex = 95;
            this.picNotSelect.TabStop = false;
            this.picNotSelect.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.label1.Location = new System.Drawing.Point(9, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 27);
            this.label1.TabIndex = 94;
            this.label1.Text = "会员信息修改";
            // 
            // pnlWoman
            // 
            this.pnlWoman.Controls.Add(this.lblWoman);
            this.pnlWoman.Controls.Add(this.picWoman);
            this.pnlWoman.Location = new System.Drawing.Point(230, 198);
            this.pnlWoman.Name = "pnlWoman";
            this.pnlWoman.Size = new System.Drawing.Size(95, 44);
            this.pnlWoman.TabIndex = 88;
            this.pnlWoman.Click += new System.EventHandler(this.pnlWoman_Click);
            // 
            // lblWoman
            // 
            this.lblWoman.AutoSize = true;
            this.lblWoman.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblWoman.Location = new System.Drawing.Point(44, 9);
            this.lblWoman.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWoman.Name = "lblWoman";
            this.lblWoman.Size = new System.Drawing.Size(31, 25);
            this.lblWoman.TabIndex = 50;
            this.lblWoman.Text = "女";
            this.lblWoman.Click += new System.EventHandler(this.pnlWoman_Click);
            // 
            // picWoman
            // 
            this.picWoman.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picWoman.BackgroundImage")));
            this.picWoman.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picWoman.Location = new System.Drawing.Point(3, 6);
            this.picWoman.Name = "picWoman";
            this.picWoman.Size = new System.Drawing.Size(36, 28);
            this.picWoman.TabIndex = 0;
            this.picWoman.TabStop = false;
            this.picWoman.Click += new System.EventHandler(this.pnlWoman_Click);
            // 
            // pnlMan
            // 
            this.pnlMan.Controls.Add(this.lblMan);
            this.pnlMan.Controls.Add(this.picMan);
            this.pnlMan.Location = new System.Drawing.Point(108, 198);
            this.pnlMan.Name = "pnlMan";
            this.pnlMan.Size = new System.Drawing.Size(95, 44);
            this.pnlMan.TabIndex = 87;
            this.pnlMan.Click += new System.EventHandler(this.pnlMan_Click);
            // 
            // lblMan
            // 
            this.lblMan.AutoSize = true;
            this.lblMan.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblMan.Location = new System.Drawing.Point(44, 9);
            this.lblMan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMan.Name = "lblMan";
            this.lblMan.Size = new System.Drawing.Size(31, 25);
            this.lblMan.TabIndex = 50;
            this.lblMan.Text = "男";
            this.lblMan.Click += new System.EventHandler(this.pnlMan_Click);
            // 
            // picMan
            // 
            this.picMan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picMan.BackgroundImage")));
            this.picMan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picMan.Location = new System.Drawing.Point(3, 6);
            this.picMan.Name = "picMan";
            this.picMan.Size = new System.Drawing.Size(36, 28);
            this.picMan.TabIndex = 0;
            this.picMan.TabStop = false;
            this.picMan.Click += new System.EventHandler(this.pnlMan_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(137)))), ((int)(((byte)(205)))));
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(13, 280);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(392, 70);
            this.btnOK.TabIndex = 86;
            this.btnOK.Text = "确认修改";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label4.Location = new System.Drawing.Point(9, 208);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 24);
            this.label4.TabIndex = 84;
            this.label4.Text = "性别";
            // 
            // txtBirthday
            // 
            this.txtBirthday.BackColor = System.Drawing.Color.White;
            this.txtBirthday.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBirthday.DecimalDigits = 3;
            this.txtBirthday.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBirthday.Location = new System.Drawing.Point(107, 134);
            this.txtBirthday.LockFocus = false;
            this.txtBirthday.MaxDeciaml = ((long)(0));
            this.txtBirthday.MaxLength = 100;
            this.txtBirthday.Name = "txtBirthday";
            this.txtBirthday.NeedBoard = false;
            this.txtBirthday.OnlyNumber = false;
            this.txtBirthday.Size = new System.Drawing.Size(298, 50);
            this.txtBirthday.TabIndex = 83;
            this.txtBirthday.WaterText = "选择会员生日，可不填";
            this.txtBirthday.DataChanged += new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox.DataRecHandleDelegate(this.txt_DataChanged);
            this.txtBirthday.Enter += new System.EventHandler(this.txtBirthday_Enter);
            // 
            // dtStart
            // 
            this.dtStart.CalendarFont = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtStart.CustomFormat = "yyyy-MM-dd";
            this.dtStart.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.Location = new System.Drawing.Point(107, 72);
            this.dtStart.Margin = new System.Windows.Forms.Padding(2);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(210, 30);
            this.dtStart.TabIndex = 93;
            this.dtStart.Value = new System.DateTime(2020, 11, 18, 0, 0, 0, 0);
            this.dtStart.CloseUp += new System.EventHandler(this.dtStart_CloseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label2.Location = new System.Drawing.Point(9, 83);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 24);
            this.label2.TabIndex = 80;
            this.label2.Text = "姓名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label3.Location = new System.Drawing.Point(9, 145);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 24);
            this.label3.TabIndex = 82;
            this.label3.Text = "生日";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.DecimalDigits = 3;
            this.txtName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(107, 72);
            this.txtName.LockFocus = false;
            this.txtName.MaxDeciaml = ((long)(0));
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.NeedBoard = true;
            this.txtName.OnlyNumber = false;
            this.txtName.Size = new System.Drawing.Size(298, 50);
            this.txtName.TabIndex = 81;
            this.txtName.WaterText = "请输入会员姓名，可不填";
            this.txtName.DataChanged += new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox.DataRecHandleDelegate(this.txt_DataChanged);
            // 
            // FormEditMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormEditMember";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormEditMember";
            this.TransparencyKey = System.Drawing.Color.DarkGray;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditMember_FormClosing);
            this.Shown += new System.EventHandler(this.FormEditMember_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBirthday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNotSelect)).EndInit();
            this.pnlWoman.ResumeLayout(false);
            this.pnlWoman.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWoman)).EndInit();
            this.pnlMan.ResumeLayout(false);
            this.pnlMan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlWoman;
        private System.Windows.Forms.Label lblWoman;
        private System.Windows.Forms.PictureBox picWoman;
        private System.Windows.Forms.Panel pnlMan;
        private System.Windows.Forms.Label lblMan;
        private System.Windows.Forms.PictureBox picMan;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label4;
        private MyControl.NumberTextBox txtBirthday;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private MyControl.NumberTextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picSelect;
        private System.Windows.Forms.PictureBox picNotSelect;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.PictureBox picBirthday;
    }
}