namespace QiandamaPOS
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvCoupon = new System.Windows.Forms.DataGridView();
            this.pnlCouponNone = new System.Windows.Forms.Panel();
            this.btnCheckNone = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblExit = new System.Windows.Forms.Label();
            this.couponcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.content = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoupon)).BeginInit();
            this.pnlCouponNone.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblTitle.Location = new System.Drawing.Point(25, 34);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(156, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "1张可用的优惠券";
            // 
            // dgvCoupon
            // 
            this.dgvCoupon.AllowUserToAddRows = false;
            this.dgvCoupon.BackgroundColor = System.Drawing.Color.White;
            this.dgvCoupon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCoupon.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvCoupon.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvCoupon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCoupon.ColumnHeadersVisible = false;
            this.dgvCoupon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.couponcode,
            this.amount,
            this.content,
            this.select});
            this.dgvCoupon.GridColor = System.Drawing.Color.White;
            this.dgvCoupon.Location = new System.Drawing.Point(25, 178);
            this.dgvCoupon.Name = "dgvCoupon";
            this.dgvCoupon.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvCoupon.RowHeadersVisible = false;
            this.dgvCoupon.RowTemplate.Height = 60;
            this.dgvCoupon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCoupon.Size = new System.Drawing.Size(400, 396);
            this.dgvCoupon.TabIndex = 2;
            this.dgvCoupon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCoupon_CellClick);
            this.dgvCoupon.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvCoupon_CellPainting);
            // 
            // pnlCouponNone
            // 
            this.pnlCouponNone.BackColor = System.Drawing.Color.White;
            this.pnlCouponNone.Controls.Add(this.btnCheckNone);
            this.pnlCouponNone.Controls.Add(this.label2);
            this.pnlCouponNone.Location = new System.Drawing.Point(25, 92);
            this.pnlCouponNone.Name = "pnlCouponNone";
            this.pnlCouponNone.Size = new System.Drawing.Size(399, 70);
            this.pnlCouponNone.TabIndex = 3;
            this.pnlCouponNone.Click += new System.EventHandler(this.pnlCouponNone_Click);
            // 
            // btnCheckNone
            // 
            this.btnCheckNone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheckNone.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.btnCheckNone.Location = new System.Drawing.Point(330, 18);
            this.btnCheckNone.Name = "btnCheckNone";
            this.btnCheckNone.Size = new System.Drawing.Size(35, 35);
            this.btnCheckNone.TabIndex = 1;
            this.btnCheckNone.Text = "√";
            this.btnCheckNone.UseVisualStyleBackColor = true;
            this.btnCheckNone.Click += new System.EventHandler(this.pnlCouponNone_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(30, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "不使用优惠券";
            // 
            // lblExit
            // 
            this.lblExit.AutoSize = true;
            this.lblExit.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblExit.ForeColor = System.Drawing.Color.Tomato;
            this.lblExit.Location = new System.Drawing.Point(375, 9);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(50, 25);
            this.lblExit.TabIndex = 4;
            this.lblExit.Text = "关闭";
            this.lblExit.Click += new System.EventHandler(this.lblExit_Click);
            // 
            // couponcode
            // 
            this.couponcode.HeaderText = "优惠券号";
            this.couponcode.Name = "couponcode";
            this.couponcode.Visible = false;
            // 
            // amount
            // 
            this.amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.amount.DefaultCellStyle = dataGridViewCellStyle1;
            this.amount.HeaderText = "券面值";
            this.amount.Name = "amount";
            // 
            // content
            // 
            this.content.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.content.DefaultCellStyle = dataGridViewCellStyle2;
            this.content.HeaderText = "详情";
            this.content.Name = "content";
            // 
            // select
            // 
            this.select.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.NullValue = false;
            this.select.DefaultCellStyle = dataGridViewCellStyle3;
            this.select.HeaderText = "使用";
            this.select.Name = "select";
            // 
            // frmCoupon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(450, 600);
            this.Controls.Add(this.lblExit);
            this.Controls.Add(this.pnlCouponNone);
            this.Controls.Add(this.dgvCoupon);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCoupon";
            this.ShowInTaskbar = false;
            this.Text = "frmCoupon";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCoupon_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoupon)).EndInit();
            this.pnlCouponNone.ResumeLayout(false);
            this.pnlCouponNone.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvCoupon;
        private System.Windows.Forms.Panel pnlCouponNone;
        private System.Windows.Forms.Button btnCheckNone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn couponcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn content;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
    }
}