namespace QiandamaPOS
{
    partial class frmGood
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGood));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl5 = new System.Windows.Forms.Panel();
            this.picbtnDel = new System.Windows.Forms.PictureBox();
            this.pnl4 = new System.Windows.Forms.Panel();
            this.lblSalePrice = new System.Windows.Forms.Label();
            this.pnlNum = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            this.lblNum = new System.Windows.Forms.Label();
            this.pnl2 = new System.Windows.Forms.Panel();
            this.lblItemPrice = new System.Windows.Forms.Label();
            this.pnl1 = new System.Windows.Forms.Panel();
            this.lblBarCode = new System.Windows.Forms.Label();
            this.lblProName = new System.Windows.Forms.Label();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbtnDel)).BeginInit();
            this.pnl4.SuspendLayout();
            this.pnlNum.SuspendLayout();
            this.pnl2.SuspendLayout();
            this.pnl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.56559F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.95552F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.05672F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.95552F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.46664F));
            this.tableLayoutPanel1.Controls.Add(this.pnl5, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnl4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlNum, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnl2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnl1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 1);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(668, 66);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnl5
            // 
            this.pnl5.Controls.Add(this.picbtnDel);
            this.pnl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl5.Location = new System.Drawing.Point(577, 2);
            this.pnl5.Margin = new System.Windows.Forms.Padding(2);
            this.pnl5.Name = "pnl5";
            this.pnl5.Size = new System.Drawing.Size(89, 62);
            this.pnl5.TabIndex = 4;
            // 
            // picbtnDel
            // 
            this.picbtnDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picbtnDel.BackgroundImage")));
            this.picbtnDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picbtnDel.Location = new System.Drawing.Point(32, 12);
            this.picbtnDel.Margin = new System.Windows.Forms.Padding(2);
            this.picbtnDel.Name = "picbtnDel";
            this.picbtnDel.Size = new System.Drawing.Size(27, 33);
            this.picbtnDel.TabIndex = 0;
            this.picbtnDel.TabStop = false;
            this.picbtnDel.Click += new System.EventHandler(this.picbtnDel_Click);
            // 
            // pnl4
            // 
            this.pnl4.Controls.Add(this.lblSalePrice);
            this.pnl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl4.Location = new System.Drawing.Point(458, 2);
            this.pnl4.Margin = new System.Windows.Forms.Padding(2);
            this.pnl4.Name = "pnl4";
            this.pnl4.Size = new System.Drawing.Size(115, 62);
            this.pnl4.TabIndex = 3;
            // 
            // lblSalePrice
            // 
            this.lblSalePrice.AutoSize = true;
            this.lblSalePrice.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.lblSalePrice.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblSalePrice.Location = new System.Drawing.Point(7, 12);
            this.lblSalePrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSalePrice.Name = "lblSalePrice";
            this.lblSalePrice.Size = new System.Drawing.Size(97, 31);
            this.lblSalePrice.TabIndex = 3;
            this.lblSalePrice.Text = "lblPrice";
            // 
            // pnlNum
            // 
            this.pnlNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNum.Controls.Add(this.btnAdd);
            this.pnlNum.Controls.Add(this.btnMinus);
            this.pnlNum.Controls.Add(this.lblNum);
            this.pnlNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNum.Location = new System.Drawing.Point(311, 2);
            this.pnlNum.Margin = new System.Windows.Forms.Padding(2);
            this.pnlNum.Name = "pnlNum";
            this.pnlNum.Size = new System.Drawing.Size(143, 62);
            this.pnlNum.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Location = new System.Drawing.Point(102, -1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(40, 62);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnMinus
            // 
            this.btnMinus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMinus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMinus.BackgroundImage")));
            this.btnMinus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMinus.Location = new System.Drawing.Point(-1, -1);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(40, 62);
            this.btnMinus.TabIndex = 4;
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // lblNum
            // 
            this.lblNum.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblNum.Location = new System.Drawing.Point(38, 14);
            this.lblNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(64, 30);
            this.lblNum.TabIndex = 3;
            this.lblNum.Text = "1";
            this.lblNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl2
            // 
            this.pnl2.Controls.Add(this.lblItemPrice);
            this.pnl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl2.Location = new System.Drawing.Point(192, 2);
            this.pnl2.Margin = new System.Windows.Forms.Padding(2);
            this.pnl2.Name = "pnl2";
            this.pnl2.Size = new System.Drawing.Size(115, 62);
            this.pnl2.TabIndex = 1;
            // 
            // lblItemPrice
            // 
            this.lblItemPrice.AutoSize = true;
            this.lblItemPrice.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblItemPrice.Location = new System.Drawing.Point(16, 15);
            this.lblItemPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblItemPrice.Name = "lblItemPrice";
            this.lblItemPrice.Size = new System.Drawing.Size(76, 30);
            this.lblItemPrice.TabIndex = 2;
            this.lblItemPrice.Text = "label2";
            // 
            // pnl1
            // 
            this.pnl1.Controls.Add(this.lblBarCode);
            this.pnl1.Controls.Add(this.lblProName);
            this.pnl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl1.Location = new System.Drawing.Point(2, 2);
            this.pnl1.Margin = new System.Windows.Forms.Padding(2);
            this.pnl1.Name = "pnl1";
            this.pnl1.Size = new System.Drawing.Size(186, 62);
            this.pnl1.TabIndex = 0;
            // 
            // lblBarCode
            // 
            this.lblBarCode.AutoSize = true;
            this.lblBarCode.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblBarCode.Location = new System.Drawing.Point(9, 31);
            this.lblBarCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBarCode.Name = "lblBarCode";
            this.lblBarCode.Size = new System.Drawing.Size(67, 25);
            this.lblBarCode.TabIndex = 1;
            this.lblBarCode.Text = "label2";
            // 
            // lblProName
            // 
            this.lblProName.AutoSize = true;
            this.lblProName.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblProName.Location = new System.Drawing.Point(9, 6);
            this.lblProName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProName.Name = "lblProName";
            this.lblProName.Size = new System.Drawing.Size(67, 25);
            this.lblProName.TabIndex = 0;
            this.lblProName.Text = "label1";
            // 
            // pnlLine
            // 
            this.pnlLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLine.BackColor = System.Drawing.Color.DarkGray;
            this.pnlLine.Location = new System.Drawing.Point(0, 69);
            this.pnlLine.Margin = new System.Windows.Forms.Padding(2);
            this.pnlLine.Name = "pnlLine";
            this.pnlLine.Size = new System.Drawing.Size(668, 1);
            this.pnlLine.TabIndex = 1;
            // 
            // frmGood
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(670, 74);
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmGood";
            this.Text = "frmGood";
            this.SizeChanged += new System.EventHandler(this.frmGood_SizeChanged);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picbtnDel)).EndInit();
            this.pnl4.ResumeLayout(false);
            this.pnl4.PerformLayout();
            this.pnlNum.ResumeLayout(false);
            this.pnl2.ResumeLayout(false);
            this.pnl2.PerformLayout();
            this.pnl1.ResumeLayout(false);
            this.pnl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnl5;
        private System.Windows.Forms.PictureBox picbtnDel;
        private System.Windows.Forms.Panel pnl4;
        private System.Windows.Forms.Label lblSalePrice;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Panel pnl2;
        private System.Windows.Forms.Label lblItemPrice;
        private System.Windows.Forms.Panel pnl1;
        private System.Windows.Forms.Label lblBarCode;
        private System.Windows.Forms.Label lblProName;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.Panel pnlNum;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnMinus;
    }
}