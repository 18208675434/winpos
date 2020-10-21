namespace WinSaasPOS
{
    partial class frmAddUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddUser));
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.pnlName = new System.Windows.Forms.Panel();
            this.PnlPhone = new System.Windows.Forms.Panel();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlName.SuspendLayout();
            this.PnlPhone.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Gainsboro;
            this.lblName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblName.ForeColor = System.Drawing.Color.Gray;
            this.lblName.Location = new System.Drawing.Point(12, 11);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(107, 20);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "请输入员工姓名";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.Gainsboro;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtName.Location = new System.Drawing.Point(8, 8);
            this.txtName.MaxLength = 4;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(252, 25);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // pnlName
            // 
            this.pnlName.BackColor = System.Drawing.Color.Transparent;
            this.pnlName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlName.Controls.Add(this.lblName);
            this.pnlName.Controls.Add(this.txtName);
            this.pnlName.Location = new System.Drawing.Point(58, 50);
            this.pnlName.Name = "pnlName";
            this.pnlName.Size = new System.Drawing.Size(271, 40);
            this.pnlName.TabIndex = 1;
            this.pnlName.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlName_Paint);
            // 
            // PnlPhone
            // 
            this.PnlPhone.BackColor = System.Drawing.Color.Transparent;
            this.PnlPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PnlPhone.Controls.Add(this.lblPhone);
            this.PnlPhone.Controls.Add(this.txtPhone);
            this.PnlPhone.Location = new System.Drawing.Point(58, 107);
            this.PnlPhone.Name = "PnlPhone";
            this.PnlPhone.Size = new System.Drawing.Size(271, 40);
            this.PnlPhone.TabIndex = 3;
            this.PnlPhone.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlName_Paint);
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.BackColor = System.Drawing.Color.Gainsboro;
            this.lblPhone.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblPhone.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblPhone.ForeColor = System.Drawing.Color.Gray;
            this.lblPhone.Location = new System.Drawing.Point(12, 11);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(121, 20);
            this.lblPhone.TabIndex = 2;
            this.lblPhone.Text = "请输入员工手机号";
            this.lblPhone.Click += new System.EventHandler(this.lblPhone_Click);
            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.Color.Gainsboro;
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhone.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtPhone.Location = new System.Drawing.Point(8, 8);
            this.txtPhone.MaxLength = 11;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(252, 25);
            this.txtPhone.TabIndex = 1;
            this.txtPhone.TextChanged += new System.EventHandler(this.txtPhone_TextChanged);
            this.txtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextNUMBER_KeyPress);
            // 
            // btnCancle
            // 
            this.btnCancle.BackColor = System.Drawing.Color.Transparent;
            this.btnCancle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancle.BackgroundImage")));
            this.btnCancle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancle.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnCancle.FlatAppearance.BorderSize = 0;
            this.btnCancle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancle.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnCancle.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btnCancle.Location = new System.Drawing.Point(93, 172);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(80, 35);
            this.btnCancle.TabIndex = 29;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = false;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(205, 172);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 35);
            this.btnOK.TabIndex = 28;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmAddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 240);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.PnlPhone);
            this.Controls.Add(this.pnlName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAddUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmAddUser";
            this.pnlName.ResumeLayout(false);
            this.pnlName.PerformLayout();
            this.PnlPhone.ResumeLayout(false);
            this.PnlPhone.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Panel pnlName;
        private System.Windows.Forms.Panel PnlPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnOK;
    }
}