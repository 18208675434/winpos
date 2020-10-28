namespace ZhuiZhi_Integral_Scale_UncleFruit.PayUI
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTotalInfo = new System.Windows.Forms.Label();
            this.lblTotalPay = new System.Windows.Forms.Label();
            this.btnCancle = new System.Windows.Forms.Button();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.dgvCartDetail = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTotalPay = new System.Windows.Forms.Panel();
            this.btnMemberPromo = new System.Windows.Forms.Button();
            this.pnlPayType = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlPayByOther = new System.Windows.Forms.Panel();
            this.lblPayByOther = new System.Windows.Forms.Label();
            this.picPayByOther = new System.Windows.Forms.PictureBox();
            this.pnlPayByBalance = new System.Windows.Forms.Panel();
            this.lblPayByBalance = new System.Windows.Forms.Label();
            this.picPayByBalance = new System.Windows.Forms.PictureBox();
            this.pnlPayByCash = new System.Windows.Forms.Panel();
            this.lblPayByCash = new System.Windows.Forms.Label();
            this.picPayByCash = new System.Windows.Forms.PictureBox();
            this.pnlPayByOnLine = new System.Windows.Forms.Panel();
            this.lblPayByOnLine = new System.Windows.Forms.Label();
            this.picPayByOnLine = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartDetail)).BeginInit();
            this.pnlTotalPay.SuspendLayout();
            this.pnlPayType.SuspendLayout();
            this.pnlPayByOther.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPayByOther)).BeginInit();
            this.pnlPayByBalance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPayByBalance)).BeginInit();
            this.pnlPayByCash.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPayByCash)).BeginInit();
            this.pnlPayByOnLine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPayByOnLine)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.lblTitle.Location = new System.Drawing.Point(11, 24);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(44, 23);
            this.lblTitle.TabIndex = 34;
            this.lblTitle.Text = "结算";
            // 
            // lblTotalInfo
            // 
            this.lblTotalInfo.AutoSize = true;
            this.lblTotalInfo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblTotalInfo.ForeColor = System.Drawing.Color.Black;
            this.lblTotalInfo.Location = new System.Drawing.Point(2, 29);
            this.lblTotalInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalInfo.Name = "lblTotalInfo";
            this.lblTotalInfo.Size = new System.Drawing.Size(47, 21);
            this.lblTotalInfo.TabIndex = 45;
            this.lblTotalInfo.Text = "应收";
            // 
            // lblTotalPay
            // 
            this.lblTotalPay.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.lblTotalPay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(93)))), ((int)(((byte)(30)))));
            this.lblTotalPay.Location = new System.Drawing.Point(141, 15);
            this.lblTotalPay.Name = "lblTotalPay";
            this.lblTotalPay.Size = new System.Drawing.Size(217, 41);
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
            // pnlLine
            // 
            this.pnlLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnlLine.Location = new System.Drawing.Point(5, 11);
            this.pnlLine.Name = "pnlLine";
            this.pnlLine.Size = new System.Drawing.Size(353, 1);
            this.pnlLine.TabIndex = 51;
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
            this.dgvCartDetail.Location = new System.Drawing.Point(15, 57);
            this.dgvCartDetail.Name = "dgvCartDetail";
            this.dgvCartDetail.ReadOnly = true;
            this.dgvCartDetail.RowHeadersVisible = false;
            this.dgvCartDetail.RowTemplate.Height = 25;
            this.dgvCartDetail.Size = new System.Drawing.Size(352, 113);
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
            // pnlTotalPay
            // 
            this.pnlTotalPay.Controls.Add(this.btnMemberPromo);
            this.pnlTotalPay.Controls.Add(this.pnlLine);
            this.pnlTotalPay.Controls.Add(this.lblTotalPay);
            this.pnlTotalPay.Controls.Add(this.lblTotalInfo);
            this.pnlTotalPay.Location = new System.Drawing.Point(9, 179);
            this.pnlTotalPay.Name = "pnlTotalPay";
            this.pnlTotalPay.Size = new System.Drawing.Size(367, 86);
            this.pnlTotalPay.TabIndex = 61;
            // 
            // btnMemberPromo
            // 
            this.btnMemberPromo.BackColor = System.Drawing.Color.Linen;
            this.btnMemberPromo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnMemberPromo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMemberPromo.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnMemberPromo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnMemberPromo.Location = new System.Drawing.Point(5, 55);
            this.btnMemberPromo.Name = "btnMemberPromo";
            this.btnMemberPromo.Size = new System.Drawing.Size(353, 26);
            this.btnMemberPromo.TabIndex = 52;
            this.btnMemberPromo.UseVisualStyleBackColor = false;
            this.btnMemberPromo.Visible = false;
            // 
            // pnlPayType
            // 
            this.pnlPayType.Controls.Add(this.label1);
            this.pnlPayType.Controls.Add(this.pnlPayByOther);
            this.pnlPayType.Controls.Add(this.pnlPayByBalance);
            this.pnlPayType.Controls.Add(this.pnlPayByCash);
            this.pnlPayType.Controls.Add(this.pnlPayByOnLine);
            this.pnlPayType.Location = new System.Drawing.Point(9, 268);
            this.pnlPayType.Name = "pnlPayType";
            this.pnlPayType.Size = new System.Drawing.Size(367, 183);
            this.pnlPayType.TabIndex = 127;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(2, 154);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(346, 16);
            this.label1.TabIndex = 53;
            this.label1.Text = "*注:同时使用现金和会员支付时，请先使用会员支付后再使用现金支付";
            this.label1.Visible = false;
            // 
            // pnlPayByOther
            // 
            this.pnlPayByOther.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(147)))), ((int)(((byte)(225)))));
            this.pnlPayByOther.Controls.Add(this.lblPayByOther);
            this.pnlPayByOther.Controls.Add(this.picPayByOther);
            this.pnlPayByOther.Location = new System.Drawing.Point(186, 77);
            this.pnlPayByOther.Name = "pnlPayByOther";
            this.pnlPayByOther.Size = new System.Drawing.Size(173, 65);
            this.pnlPayByOther.TabIndex = 3;
            this.pnlPayByOther.Click += new System.EventHandler(this.pnlPayByOther_Click);
            // 
            // lblPayByOther
            // 
            this.lblPayByOther.AutoSize = true;
            this.lblPayByOther.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblPayByOther.ForeColor = System.Drawing.Color.White;
            this.lblPayByOther.Location = new System.Drawing.Point(60, 20);
            this.lblPayByOther.Name = "lblPayByOther";
            this.lblPayByOther.Size = new System.Drawing.Size(82, 24);
            this.lblPayByOther.TabIndex = 1;
            this.lblPayByOther.Text = "其他支付";
            this.lblPayByOther.Click += new System.EventHandler(this.pnlPayByOther_Click);
            // 
            // picPayByOther
            // 
            this.picPayByOther.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picPayByOther.BackgroundImage")));
            this.picPayByOther.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picPayByOther.Location = new System.Drawing.Point(29, 17);
            this.picPayByOther.Name = "picPayByOther";
            this.picPayByOther.Size = new System.Drawing.Size(28, 28);
            this.picPayByOther.TabIndex = 0;
            this.picPayByOther.TabStop = false;
            this.picPayByOther.Click += new System.EventHandler(this.pnlPayByOther_Click);
            // 
            // pnlPayByBalance
            // 
            this.pnlPayByBalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(178)))), ((int)(((byte)(191)))));
            this.pnlPayByBalance.Controls.Add(this.lblPayByBalance);
            this.pnlPayByBalance.Controls.Add(this.picPayByBalance);
            this.pnlPayByBalance.Location = new System.Drawing.Point(6, 77);
            this.pnlPayByBalance.Name = "pnlPayByBalance";
            this.pnlPayByBalance.Size = new System.Drawing.Size(173, 65);
            this.pnlPayByBalance.TabIndex = 3;
            this.pnlPayByBalance.Click += new System.EventHandler(this.pnlPayByBalance_Click);
            // 
            // lblPayByBalance
            // 
            this.lblPayByBalance.AutoSize = true;
            this.lblPayByBalance.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblPayByBalance.ForeColor = System.Drawing.Color.White;
            this.lblPayByBalance.Location = new System.Drawing.Point(66, 20);
            this.lblPayByBalance.Name = "lblPayByBalance";
            this.lblPayByBalance.Size = new System.Drawing.Size(82, 24);
            this.lblPayByBalance.TabIndex = 1;
            this.lblPayByBalance.Text = "会员支付";
            this.lblPayByBalance.Click += new System.EventHandler(this.pnlPayByBalance_Click);
            // 
            // picPayByBalance
            // 
            this.picPayByBalance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picPayByBalance.BackgroundImage")));
            this.picPayByBalance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picPayByBalance.Location = new System.Drawing.Point(35, 18);
            this.picPayByBalance.Name = "picPayByBalance";
            this.picPayByBalance.Size = new System.Drawing.Size(28, 28);
            this.picPayByBalance.TabIndex = 0;
            this.picPayByBalance.TabStop = false;
            this.picPayByBalance.Click += new System.EventHandler(this.pnlPayByBalance_Click);
            // 
            // pnlPayByCash
            // 
            this.pnlPayByCash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(146)))), ((int)(((byte)(27)))));
            this.pnlPayByCash.Controls.Add(this.lblPayByCash);
            this.pnlPayByCash.Controls.Add(this.picPayByCash);
            this.pnlPayByCash.Location = new System.Drawing.Point(6, 5);
            this.pnlPayByCash.Name = "pnlPayByCash";
            this.pnlPayByCash.Size = new System.Drawing.Size(173, 65);
            this.pnlPayByCash.TabIndex = 2;
            this.pnlPayByCash.Click += new System.EventHandler(this.pnlPayByCash_Click);
            // 
            // lblPayByCash
            // 
            this.lblPayByCash.AutoSize = true;
            this.lblPayByCash.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblPayByCash.ForeColor = System.Drawing.Color.White;
            this.lblPayByCash.Location = new System.Drawing.Point(66, 21);
            this.lblPayByCash.Name = "lblPayByCash";
            this.lblPayByCash.Size = new System.Drawing.Size(51, 24);
            this.lblPayByCash.TabIndex = 1;
            this.lblPayByCash.Text = "现金";
            this.lblPayByCash.Click += new System.EventHandler(this.pnlPayByCash_Click);
            // 
            // picPayByCash
            // 
            this.picPayByCash.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picPayByCash.BackgroundImage")));
            this.picPayByCash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picPayByCash.Location = new System.Drawing.Point(35, 18);
            this.picPayByCash.Name = "picPayByCash";
            this.picPayByCash.Size = new System.Drawing.Size(28, 28);
            this.picPayByCash.TabIndex = 0;
            this.picPayByCash.TabStop = false;
            this.picPayByCash.Click += new System.EventHandler(this.pnlPayByCash_Click);
            // 
            // pnlPayByOnLine
            // 
            this.pnlPayByOnLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(70)))), ((int)(((byte)(21)))));
            this.pnlPayByOnLine.Controls.Add(this.lblPayByOnLine);
            this.pnlPayByOnLine.Controls.Add(this.picPayByOnLine);
            this.pnlPayByOnLine.Location = new System.Drawing.Point(186, 5);
            this.pnlPayByOnLine.Name = "pnlPayByOnLine";
            this.pnlPayByOnLine.Size = new System.Drawing.Size(173, 65);
            this.pnlPayByOnLine.TabIndex = 0;
            this.pnlPayByOnLine.Click += new System.EventHandler(this.pnlPayByOnLine_Click);
            // 
            // lblPayByOnLine
            // 
            this.lblPayByOnLine.AutoSize = true;
            this.lblPayByOnLine.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblPayByOnLine.ForeColor = System.Drawing.Color.White;
            this.lblPayByOnLine.Location = new System.Drawing.Point(51, 21);
            this.lblPayByOnLine.Name = "lblPayByOnLine";
            this.lblPayByOnLine.Size = new System.Drawing.Size(108, 24);
            this.lblPayByOnLine.TabIndex = 1;
            this.lblPayByOnLine.Text = "微信/支付宝";
            this.lblPayByOnLine.Click += new System.EventHandler(this.pnlPayByOnLine_Click);
            // 
            // picPayByOnLine
            // 
            this.picPayByOnLine.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picPayByOnLine.BackgroundImage")));
            this.picPayByOnLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picPayByOnLine.Location = new System.Drawing.Point(20, 18);
            this.picPayByOnLine.Name = "picPayByOnLine";
            this.picPayByOnLine.Size = new System.Drawing.Size(28, 28);
            this.picPayByOnLine.TabIndex = 0;
            this.picPayByOnLine.TabStop = false;
            this.picPayByOnLine.Click += new System.EventHandler(this.pnlPayByOnLine_Click);
            // 
            // FormPay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 520);
            this.Controls.Add(this.pnlPayType);
            this.Controls.Add(this.pnlTotalPay);
            this.Controls.Add(this.dgvCartDetail);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.lblTitle);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormPay";
            this.Activated += new System.EventHandler(this.FormPay_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartDetail)).EndInit();
            this.pnlTotalPay.ResumeLayout(false);
            this.pnlTotalPay.PerformLayout();
            this.pnlPayType.ResumeLayout(false);
            this.pnlPayType.PerformLayout();
            this.pnlPayByOther.ResumeLayout(false);
            this.pnlPayByOther.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPayByOther)).EndInit();
            this.pnlPayByBalance.ResumeLayout(false);
            this.pnlPayByBalance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPayByBalance)).EndInit();
            this.pnlPayByCash.ResumeLayout(false);
            this.pnlPayByCash.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPayByCash)).EndInit();
            this.pnlPayByOnLine.ResumeLayout(false);
            this.pnlPayByOnLine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPayByOnLine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTotalInfo;
        private System.Windows.Forms.Label lblTotalPay;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.DataGridView dgvCartDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Panel pnlTotalPay;
        private System.Windows.Forms.Button btnMemberPromo;
        private System.Windows.Forms.Panel pnlPayType;
        private System.Windows.Forms.Panel pnlPayByOnLine;
        private System.Windows.Forms.Label lblPayByOnLine;
        private System.Windows.Forms.PictureBox picPayByOnLine;
        private System.Windows.Forms.Panel pnlPayByOther;
        private System.Windows.Forms.Label lblPayByOther;
        private System.Windows.Forms.PictureBox picPayByOther;
        private System.Windows.Forms.Panel pnlPayByBalance;
        private System.Windows.Forms.Label lblPayByBalance;
        private System.Windows.Forms.PictureBox picPayByBalance;
        private System.Windows.Forms.Panel pnlPayByCash;
        private System.Windows.Forms.Label lblPayByCash;
        private System.Windows.Forms.PictureBox picPayByCash;
        private System.Windows.Forms.Label label1;
    }
}