namespace WinSaasPOS
{
    partial class frmOrderHang
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
        /// Required method for Designer suppfort - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderHang));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvOrderOnLine = new System.Windows.Forms.DataGridView();
            this.serialno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hangtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Continue = new System.Windows.Forms.DataGridViewImageColumn();
            this.DelHang = new System.Windows.Forms.DataGridViewImageColumn();
            this.timerNow = new System.Windows.Forms.Timer(this.components);
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.btnMenu = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btnCancle = new System.Windows.Forms.ToolStripButton();
            this.lblTime = new System.Windows.Forms.ToolStripLabel();
            this.lblShopName = new System.Windows.Forms.ToolStripLabel();
            this.pnlDgvHead = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.picScreen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderOnLine)).BeginInit();
            this.toolStripMain.SuspendLayout();
            this.pnlDgvHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOrderOnLine
            // 
            this.dgvOrderOnLine.AllowUserToAddRows = false;
            this.dgvOrderOnLine.AllowUserToDeleteRows = false;
            this.dgvOrderOnLine.AllowUserToResizeColumns = false;
            this.dgvOrderOnLine.AllowUserToResizeRows = false;
            this.dgvOrderOnLine.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderOnLine.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrderOnLine.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrderOnLine.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvOrderOnLine.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrderOnLine.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOrderOnLine.ColumnHeadersHeight = 50;
            this.dgvOrderOnLine.ColumnHeadersVisible = false;
            this.dgvOrderOnLine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serialno,
            this.phone,
            this.title,
            this.hangtime,
            this.Continue,
            this.DelHang});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOrderOnLine.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvOrderOnLine.GridColor = System.Drawing.Color.Silver;
            this.dgvOrderOnLine.Location = new System.Drawing.Point(12, 124);
            this.dgvOrderOnLine.Name = "dgvOrderOnLine";
            this.dgvOrderOnLine.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvOrderOnLine.RowHeadersVisible = false;
            this.dgvOrderOnLine.RowHeadersWidth = 40;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.dgvOrderOnLine.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvOrderOnLine.RowTemplate.Height = 50;
            this.dgvOrderOnLine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderOnLine.Size = new System.Drawing.Size(1154, 635);
            this.dgvOrderOnLine.TabIndex = 1;
            this.dgvOrderOnLine.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrderOnLine_CellContentClick);
            // 
            // serialno
            // 
            this.serialno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.serialno.DefaultCellStyle = dataGridViewCellStyle2;
            this.serialno.FillWeight = 100.195F;
            this.serialno.HeaderText = "序号";
            this.serialno.Name = "serialno";
            this.serialno.Width = 85;
            // 
            // phone
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.phone.DefaultCellStyle = dataGridViewCellStyle3;
            this.phone.FillWeight = 100.195F;
            this.phone.HeaderText = "会员手机号";
            this.phone.Name = "phone";
            // 
            // title
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.title.DefaultCellStyle = dataGridViewCellStyle4;
            this.title.FillWeight = 100.195F;
            this.title.HeaderText = "商品明细";
            this.title.Name = "title";
            // 
            // hangtime
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.hangtime.DefaultCellStyle = dataGridViewCellStyle5;
            this.hangtime.FillWeight = 100.195F;
            this.hangtime.HeaderText = "挂单时间";
            this.hangtime.Name = "hangtime";
            // 
            // Continue
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 14F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle6.NullValue")));
            this.Continue.DefaultCellStyle = dataGridViewCellStyle6;
            this.Continue.FillWeight = 30F;
            this.Continue.HeaderText = "继续收银";
            this.Continue.Name = "Continue";
            this.Continue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Continue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // DelHang
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 14F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle7.NullValue")));
            this.DelHang.DefaultCellStyle = dataGridViewCellStyle7;
            this.DelHang.FillWeight = 30F;
            this.DelHang.HeaderText = "删除挂单";
            this.DelHang.Name = "DelHang";
            this.DelHang.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // timerNow
            // 
            this.timerNow.Tick += new System.EventHandler(this.timerNow_Tick);
            // 
            // toolStripMain
            // 
            this.toolStripMain.AutoSize = false;
            this.toolStripMain.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMenu,
            this.toolStripLabel1,
            this.btnCancle,
            this.lblTime,
            this.lblShopName});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(1178, 60);
            this.toolStripMain.TabIndex = 25;
            this.toolStripMain.Text = "取消交易";
            // 
            // btnMenu
            // 
            this.btnMenu.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnMenu.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnMenu.ForeColor = System.Drawing.Color.White;
            this.btnMenu.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu.Image")));
            this.btnMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Padding = new System.Windows.Forms.Padding(0, 0, 25, 0);
            this.btnMenu.Size = new System.Drawing.Size(152, 57);
            this.btnMenu.Text = "某某某，你好";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(32, 57);
            this.toolStripLabel1.Text = "      ";
            this.toolStripLabel1.ToolTipText = "54435435";
            // 
            // btnCancle
            // 
            this.btnCancle.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnCancle.AutoSize = false;
            this.btnCancle.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCancle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancle.BackgroundImage")));
            this.btnCancle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCancle.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnCancle.ForeColor = System.Drawing.Color.White;
            this.btnCancle.Image = ((System.Drawing.Image)(resources.GetObject("btnCancle.Image")));
            this.btnCancle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancle.Margin = new System.Windows.Forms.Padding(0, 1, 15, 2);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(78, 35);
            this.btnCancle.Text = "返回";
            this.btnCancle.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(163, 57);
            this.lblTime.Text = "2019-10-10 12:12:39";
            // 
            // lblShopName
            // 
            this.lblShopName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblShopName.ForeColor = System.Drawing.Color.White;
            this.lblShopName.Name = "lblShopName";
            this.lblShopName.Size = new System.Drawing.Size(90, 57);
            this.lblShopName.Text = "天河东路店";
            // 
            // pnlDgvHead
            // 
            this.pnlDgvHead.BackColor = System.Drawing.Color.White;
            this.pnlDgvHead.Controls.Add(this.label6);
            this.pnlDgvHead.Controls.Add(this.label5);
            this.pnlDgvHead.Controls.Add(this.label7);
            this.pnlDgvHead.Controls.Add(this.label10);
            this.pnlDgvHead.Controls.Add(this.label11);
            this.pnlDgvHead.Location = new System.Drawing.Point(12, 73);
            this.pnlDgvHead.Name = "pnlDgvHead";
            this.pnlDgvHead.Size = new System.Drawing.Size(1154, 49);
            this.pnlDgvHead.TabIndex = 33;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(1032, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 23);
            this.label6.TabIndex = 37;
            this.label6.Text = "操作";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(783, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 23);
            this.label5.TabIndex = 36;
            this.label5.Text = "挂单时间";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(484, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 23);
            this.label7.TabIndex = 35;
            this.label7.Text = "商户明细";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(183, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 23);
            this.label10.TabIndex = 33;
            this.label10.Text = "会员手机号";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(18, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 23);
            this.label11.TabIndex = 32;
            this.label11.Text = "序号";
            // 
            // picScreen
            // 
            this.picScreen.BackColor = System.Drawing.Color.Red;
            this.picScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picScreen.Location = new System.Drawing.Point(0, 0);
            this.picScreen.Name = "picScreen";
            this.picScreen.Size = new System.Drawing.Size(10, 10);
            this.picScreen.TabIndex = 24;
            this.picScreen.TabStop = false;
            this.picScreen.Visible = false;
            // 
            // frmOrderHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1178, 760);
            this.Controls.Add(this.picScreen);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.pnlDgvHead);
            this.Controls.Add(this.dgvOrderOnLine);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmOrderHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmOrderHang";
            this.Shown += new System.EventHandler(this.frmOrderHang_Shown);
            this.EnabledChanged += new System.EventHandler(this.frmOrderHang_EnabledChanged);
            this.SizeChanged += new System.EventHandler(this.frmOrderHang_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderOnLine)).EndInit();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.pnlDgvHead.ResumeLayout(false);
            this.pnlDgvHead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOrderOnLine;
        private System.Windows.Forms.PictureBox picScreen;
        private System.Windows.Forms.Timer timerNow;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton btnMenu;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnCancle;
        private System.Windows.Forms.ToolStripLabel lblTime;
        private System.Windows.Forms.ToolStripLabel lblShopName;
        private System.Windows.Forms.Panel pnlDgvHead;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialno;
        private System.Windows.Forms.DataGridViewTextBoxColumn phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewTextBoxColumn hangtime;
        private System.Windows.Forms.DataGridViewImageColumn Continue;
        private System.Windows.Forms.DataGridViewImageColumn DelHang;
    }
}