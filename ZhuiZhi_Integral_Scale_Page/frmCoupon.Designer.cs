namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    partial class frmCoupon
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCoupon));
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvCoupon = new System.Windows.Forms.DataGridView();
            this.couponcode = new System.Windows.Forms.DataGridViewImageColumn();
            this.pnlCouponNone = new System.Windows.Forms.Panel();
            this.picNotUse = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancle = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.picSelect = new System.Windows.Forms.PictureBox();
            this.picNotSelect = new System.Windows.Forms.PictureBox();
            this.pnlItem = new System.Windows.Forms.Panel();
            this.picItem = new System.Windows.Forms.PictureBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblContent = new System.Windows.Forms.Label();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.rbtnPageDown = new ZhuiZhi_Integral_Scale_UncleFruit.RoundButton();
            this.rbtnPageUp = new ZhuiZhi_Integral_Scale_UncleFruit.RoundButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoupon)).BeginInit();
            this.pnlCouponNone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNotUse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNotSelect)).BeginInit();
            this.pnlItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picItem)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lblTitle.ForeColor = System.Drawing.Color.Tomato;
            this.lblTitle.Location = new System.Drawing.Point(109, 27);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(63, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "0张可用";
            // 
            // dgvCoupon
            // 
            this.dgvCoupon.AllowUserToAddRows = false;
            this.dgvCoupon.AllowUserToDeleteRows = false;
            this.dgvCoupon.AllowUserToResizeColumns = false;
            this.dgvCoupon.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvCoupon.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCoupon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCoupon.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvCoupon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCoupon.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvCoupon.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCoupon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCoupon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCoupon.ColumnHeadersVisible = false;
            this.dgvCoupon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.couponcode});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCoupon.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCoupon.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvCoupon.Location = new System.Drawing.Point(12, 121);
            this.dgvCoupon.Name = "dgvCoupon";
            this.dgvCoupon.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvCoupon.RowHeadersVisible = false;
            this.dgvCoupon.RowTemplate.Height = 70;
            this.dgvCoupon.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvCoupon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCoupon.Size = new System.Drawing.Size(356, 289);
            this.dgvCoupon.TabIndex = 2;
            this.dgvCoupon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCoupon_CellClick);
            // 
            // couponcode
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle3.NullValue")));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            this.couponcode.DefaultCellStyle = dataGridViewCellStyle3;
            this.couponcode.HeaderText = "优惠券号";
            this.couponcode.Name = "couponcode";
            this.couponcode.ReadOnly = true;
            this.couponcode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // pnlCouponNone
            // 
            this.pnlCouponNone.BackColor = System.Drawing.Color.White;
            this.pnlCouponNone.Controls.Add(this.picNotUse);
            this.pnlCouponNone.Controls.Add(this.label2);
            this.pnlCouponNone.Location = new System.Drawing.Point(12, 62);
            this.pnlCouponNone.Name = "pnlCouponNone";
            this.pnlCouponNone.Size = new System.Drawing.Size(356, 53);
            this.pnlCouponNone.TabIndex = 3;
            this.pnlCouponNone.Click += new System.EventHandler(this.pnlCouponNone_Click);
            // 
            // picNotUse
            // 
            this.picNotUse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picNotUse.BackgroundImage")));
            this.picNotUse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picNotUse.Location = new System.Drawing.Point(305, 14);
            this.picNotUse.Name = "picNotUse";
            this.picNotUse.Size = new System.Drawing.Size(25, 25);
            this.picNotUse.TabIndex = 2;
            this.picNotUse.TabStop = false;
            this.picNotUse.Click += new System.EventHandler(this.pnlCouponNone_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.Location = new System.Drawing.Point(13, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "不使用优惠券";
            // 
            // btnCancle
            // 
            this.btnCancle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancle.BackgroundImage")));
            this.btnCancle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancle.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancle.FlatAppearance.BorderSize = 0;
            this.btnCancle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancle.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnCancle.Location = new System.Drawing.Point(342, 12);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(26, 26);
            this.btnCancle.TabIndex = 36;
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 24);
            this.label1.TabIndex = 37;
            this.label1.Text = "选择优惠券";
            // 
            // picSelect
            // 
            this.picSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picSelect.Image = ((System.Drawing.Image)(resources.GetObject("picSelect.Image")));
            this.picSelect.Location = new System.Drawing.Point(271, 0);
            this.picSelect.Name = "picSelect";
            this.picSelect.Size = new System.Drawing.Size(46, 38);
            this.picSelect.TabIndex = 47;
            this.picSelect.TabStop = false;
            this.picSelect.Visible = false;
            // 
            // picNotSelect
            // 
            this.picNotSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picNotSelect.Image = ((System.Drawing.Image)(resources.GetObject("picNotSelect.Image")));
            this.picNotSelect.Location = new System.Drawing.Point(227, 0);
            this.picNotSelect.Name = "picNotSelect";
            this.picNotSelect.Size = new System.Drawing.Size(38, 38);
            this.picNotSelect.TabIndex = 48;
            this.picNotSelect.TabStop = false;
            this.picNotSelect.Visible = false;
            // 
            // pnlItem
            // 
            this.pnlItem.BackColor = System.Drawing.Color.White;
            this.pnlItem.Controls.Add(this.picItem);
            this.pnlItem.Controls.Add(this.lblDate);
            this.pnlItem.Controls.Add(this.lblContent);
            this.pnlItem.Controls.Add(this.lblUnit);
            this.pnlItem.Controls.Add(this.lblAmount);
            this.pnlItem.Location = new System.Drawing.Point(12, -368);
            this.pnlItem.Name = "pnlItem";
            this.pnlItem.Size = new System.Drawing.Size(350, 66);
            this.pnlItem.TabIndex = 49;
            // 
            // picItem
            // 
            this.picItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picItem.BackgroundImage")));
            this.picItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picItem.Location = new System.Drawing.Point(308, 21);
            this.picItem.Name = "picItem";
            this.picItem.Size = new System.Drawing.Size(25, 25);
            this.picItem.TabIndex = 5;
            this.picItem.TabStop = false;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblDate.ForeColor = System.Drawing.Color.Gray;
            this.lblDate.Location = new System.Drawing.Point(103, 36);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(175, 20);
            this.lblDate.TabIndex = 4;
            this.lblDate.Text = "2020-03-03至2020-03-03";
            // 
            // lblContent
            // 
            this.lblContent.AutoSize = true;
            this.lblContent.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblContent.Location = new System.Drawing.Point(103, 15);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(92, 21);
            this.lblContent.TabIndex = 3;
            this.lblContent.Text = "满30元使用";
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblUnit.ForeColor = System.Drawing.Color.Red;
            this.lblUnit.Location = new System.Drawing.Point(70, 28);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(26, 21);
            this.lblUnit.TabIndex = 2;
            this.lblUnit.Text = "折";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.lblAmount.ForeColor = System.Drawing.Color.Red;
            this.lblAmount.Location = new System.Drawing.Point(32, 17);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(47, 35);
            this.lblAmount.TabIndex = 1;
            this.lblAmount.Text = "20";
            // 
            // rbtnPageDown
            // 
            this.rbtnPageDown.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(191)))), ((int)(((byte)(136)))));
            this.rbtnPageDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(191)))), ((int)(((byte)(136)))));
            this.rbtnPageDown.Image = null;
            this.rbtnPageDown.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnPageDown.Location = new System.Drawing.Point(197, 416);
            this.rbtnPageDown.Name = "rbtnPageDown";
            this.rbtnPageDown.PenColor = System.Drawing.Color.Black;
            this.rbtnPageDown.PenWidth = 1;
            this.rbtnPageDown.RoundRadius = 1;
            this.rbtnPageDown.ShowImg = false;
            this.rbtnPageDown.ShowText = "下一页";
            this.rbtnPageDown.Size = new System.Drawing.Size(170, 45);
            this.rbtnPageDown.TabIndex = 51;
            this.rbtnPageDown.TextForeColor = System.Drawing.Color.White;
            this.rbtnPageDown.WhetherEnable = true;
            this.rbtnPageDown.ButtonClick += new System.EventHandler(this.rbtnPageDown_ButtonClick);
            // 
            // rbtnPageUp
            // 
            this.rbtnPageUp.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(160)))), ((int)(((byte)(110)))));
            this.rbtnPageUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(160)))), ((int)(((byte)(110)))));
            this.rbtnPageUp.Image = null;
            this.rbtnPageUp.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnPageUp.Location = new System.Drawing.Point(12, 416);
            this.rbtnPageUp.Name = "rbtnPageUp";
            this.rbtnPageUp.PenColor = System.Drawing.Color.Black;
            this.rbtnPageUp.PenWidth = 1;
            this.rbtnPageUp.RoundRadius = 1;
            this.rbtnPageUp.ShowImg = false;
            this.rbtnPageUp.ShowText = "上一页";
            this.rbtnPageUp.Size = new System.Drawing.Size(170, 45);
            this.rbtnPageUp.TabIndex = 50;
            this.rbtnPageUp.TextForeColor = System.Drawing.Color.White;
            this.rbtnPageUp.WhetherEnable = true;
            this.rbtnPageUp.ButtonClick += new System.EventHandler(this.rbtnPageUp_ButtonClick);
            // 
            // frmCoupon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(380, 480);
            this.Controls.Add(this.rbtnPageDown);
            this.Controls.Add(this.rbtnPageUp);
            this.Controls.Add(this.pnlItem);
            this.Controls.Add(this.picSelect);
            this.Controls.Add(this.picNotSelect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.pnlCouponNone);
            this.Controls.Add(this.dgvCoupon);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCoupon";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmCoupon";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCoupon_FormClosing);
            this.Shown += new System.EventHandler(this.frmCoupon_Shown);
            this.Resize += new System.EventHandler(this.frmCoupon_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoupon)).EndInit();
            this.pnlCouponNone.ResumeLayout(false);
            this.pnlCouponNone.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNotUse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNotSelect)).EndInit();
            this.pnlItem.ResumeLayout(false);
            this.pnlItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvCoupon;
        private System.Windows.Forms.Panel pnlCouponNone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picNotUse;
        private System.Windows.Forms.PictureBox picSelect;
        private System.Windows.Forms.PictureBox picNotSelect;
        private System.Windows.Forms.Panel pnlItem;
        private System.Windows.Forms.PictureBox picItem;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.DataGridViewImageColumn couponcode;
        private RoundButton rbtnPageDown;
        private RoundButton rbtnPageUp;
    }
}