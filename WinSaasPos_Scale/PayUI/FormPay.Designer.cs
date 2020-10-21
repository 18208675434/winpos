namespace WinSaasPOS_Scale.PayUI
{
    partial class FormPay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPay));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblInfo = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTotalPay = new System.Windows.Forms.Label();
            this.btnCancle = new System.Windows.Forms.Button();
            this.pnlLine7 = new System.Windows.Forms.Panel();
            this.dgvCartDetail = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rbtnPayByCoupon = new WinSaasPOS_Scale.RoundButton();
            this.rbtnPayByBalance = new WinSaasPOS_Scale.RoundButton();
            this.rbtnPayOnLine = new WinSaasPOS_Scale.RoundButton();
            this.rbtnByCash = new WinSaasPOS_Scale.RoundButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.lblInfo.Location = new System.Drawing.Point(11, 24);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(44, 23);
            this.lblInfo.TabIndex = 34;
            this.lblInfo.Text = "结算";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label10.Location = new System.Drawing.Point(31, 241);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 20);
            this.label10.TabIndex = 45;
            this.label10.Text = "应收";
            // 
            // lblTotalPay
            // 
            this.lblTotalPay.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.lblTotalPay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(93)))), ((int)(((byte)(30)))));
            this.lblTotalPay.Location = new System.Drawing.Point(209, 231);
            this.lblTotalPay.Name = "lblTotalPay";
            this.lblTotalPay.Size = new System.Drawing.Size(140, 41);
            this.lblTotalPay.TabIndex = 46;
            this.lblTotalPay.Text = "￥0.00";
            this.lblTotalPay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.btnCancle.TabIndex = 35;
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // pnlLine7
            // 
            this.pnlLine7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.pnlLine7.Location = new System.Drawing.Point(35, 218);
            this.pnlLine7.Name = "pnlLine7";
            this.pnlLine7.Size = new System.Drawing.Size(314, 1);
            this.pnlLine7.TabIndex = 51;
            // 
            // dgvCartDetail
            // 
            this.dgvCartDetail.AllowUserToAddRows = false;
            this.dgvCartDetail.AllowUserToDeleteRows = false;
            this.dgvCartDetail.AllowUserToResizeColumns = false;
            this.dgvCartDetail.AllowUserToResizeRows = false;
            this.dgvCartDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCartDetail.BackgroundColor = System.Drawing.Color.White;
            this.dgvCartDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCartDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCartDetail.ColumnHeadersVisible = false;
            this.dgvCartDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCartDetail.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCartDetail.GridColor = System.Drawing.Color.White;
            this.dgvCartDetail.Location = new System.Drawing.Point(35, 72);
            this.dgvCartDetail.Name = "dgvCartDetail";
            this.dgvCartDetail.ReadOnly = true;
            this.dgvCartDetail.RowHeadersVisible = false;
            this.dgvCartDetail.RowTemplate.Height = 30;
            this.dgvCartDetail.Size = new System.Drawing.Size(314, 136);
            this.dgvCartDetail.TabIndex = 60;
            // 
            // Column1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column2
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // rbtnPayByCoupon
            // 
            this.rbtnPayByCoupon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(191)))), ((int)(((byte)(136)))));
            this.rbtnPayByCoupon.Image = ((System.Drawing.Image)(resources.GetObject("rbtnPayByCoupon.Image")));
            this.rbtnPayByCoupon.ImageSize = new System.Drawing.Size(24, 24);
            this.rbtnPayByCoupon.Location = new System.Drawing.Point(194, 384);
            this.rbtnPayByCoupon.Name = "rbtnPayByCoupon";
            this.rbtnPayByCoupon.PenColor = System.Drawing.Color.Black;
            this.rbtnPayByCoupon.PenWidth = 1;
            this.rbtnPayByCoupon.RoundRadius = 10;
            this.rbtnPayByCoupon.ShowImg = true;
            this.rbtnPayByCoupon.ShowText = "代金券";
            this.rbtnPayByCoupon.Size = new System.Drawing.Size(155, 65);
            this.rbtnPayByCoupon.TabIndex = 50;
            this.rbtnPayByCoupon.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(191)))), ((int)(((byte)(136)))));
            this.rbtnPayByCoupon.TextForeColor = System.Drawing.Color.White;
            this.rbtnPayByCoupon.ButtonClick += new System.EventHandler(this.rbtnPayByCoupon_ButtonClick);
            // 
            // rbtnPayByBalance
            // 
            this.rbtnPayByBalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(178)))), ((int)(((byte)(191)))));
            this.rbtnPayByBalance.Image = ((System.Drawing.Image)(resources.GetObject("rbtnPayByBalance.Image")));
            this.rbtnPayByBalance.ImageSize = new System.Drawing.Size(24, 24);
            this.rbtnPayByBalance.Location = new System.Drawing.Point(32, 384);
            this.rbtnPayByBalance.Name = "rbtnPayByBalance";
            this.rbtnPayByBalance.PenColor = System.Drawing.Color.Black;
            this.rbtnPayByBalance.PenWidth = 1;
            this.rbtnPayByBalance.RoundRadius = 10;
            this.rbtnPayByBalance.ShowImg = true;
            this.rbtnPayByBalance.ShowText = "余额";
            this.rbtnPayByBalance.Size = new System.Drawing.Size(155, 65);
            this.rbtnPayByBalance.TabIndex = 49;
            this.rbtnPayByBalance.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(178)))), ((int)(((byte)(191)))));
            this.rbtnPayByBalance.TextForeColor = System.Drawing.Color.White;
            this.rbtnPayByBalance.ButtonClick += new System.EventHandler(this.rbtnPayByBalance_ButtonClick);
            // 
            // rbtnPayOnLine
            // 
            this.rbtnPayOnLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(70)))), ((int)(((byte)(21)))));
            this.rbtnPayOnLine.Image = ((System.Drawing.Image)(resources.GetObject("rbtnPayOnLine.Image")));
            this.rbtnPayOnLine.ImageSize = new System.Drawing.Size(24, 24);
            this.rbtnPayOnLine.Location = new System.Drawing.Point(194, 312);
            this.rbtnPayOnLine.Name = "rbtnPayOnLine";
            this.rbtnPayOnLine.PenColor = System.Drawing.Color.Black;
            this.rbtnPayOnLine.PenWidth = 1;
            this.rbtnPayOnLine.RoundRadius = 10;
            this.rbtnPayOnLine.ShowImg = true;
            this.rbtnPayOnLine.ShowText = "微信支付宝";
            this.rbtnPayOnLine.Size = new System.Drawing.Size(155, 65);
            this.rbtnPayOnLine.TabIndex = 48;
            this.rbtnPayOnLine.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(70)))), ((int)(((byte)(21)))));
            this.rbtnPayOnLine.TextForeColor = System.Drawing.Color.White;
            this.rbtnPayOnLine.ButtonClick += new System.EventHandler(this.rbtnPayOnLine_ButtonClick);
            // 
            // rbtnByCash
            // 
            this.rbtnByCash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(146)))), ((int)(((byte)(27)))));
            this.rbtnByCash.Image = ((System.Drawing.Image)(resources.GetObject("rbtnByCash.Image")));
            this.rbtnByCash.ImageSize = new System.Drawing.Size(24, 24);
            this.rbtnByCash.Location = new System.Drawing.Point(32, 312);
            this.rbtnByCash.Name = "rbtnByCash";
            this.rbtnByCash.PenColor = System.Drawing.Color.Black;
            this.rbtnByCash.PenWidth = 1;
            this.rbtnByCash.RoundRadius = 10;
            this.rbtnByCash.ShowImg = true;
            this.rbtnByCash.ShowText = "现金";
            this.rbtnByCash.Size = new System.Drawing.Size(155, 65);
            this.rbtnByCash.TabIndex = 47;
            this.rbtnByCash.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(146)))), ((int)(((byte)(27)))));
            this.rbtnByCash.TextForeColor = System.Drawing.Color.White;
            this.rbtnByCash.ButtonClick += new System.EventHandler(this.rbtnByCash_ButtonClick);
            // 
            // FormPay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 480);
            this.Controls.Add(this.dgvCartDetail);
            this.Controls.Add(this.pnlLine7);
            this.Controls.Add(this.rbtnPayByCoupon);
            this.Controls.Add(this.rbtnPayByBalance);
            this.Controls.Add(this.rbtnPayOnLine);
            this.Controls.Add(this.rbtnByCash);
            this.Controls.Add(this.lblTotalPay);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.lblInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormPay";
            this.Resize += new System.EventHandler(this.FormPay_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTotalPay;
        private RoundButton rbtnByCash;
        private RoundButton rbtnPayOnLine;
        private RoundButton rbtnPayByCoupon;
        private RoundButton rbtnPayByBalance;
        private System.Windows.Forms.Panel pnlLine7;
        private System.Windows.Forms.DataGridView dgvCartDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}