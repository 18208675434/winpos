namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    partial class frmScale
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScale));
            this.dgvScale = new System.Windows.Forms.DataGridView();
            this.serianno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scalename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scaleip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scaletype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lasttime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScaleStatus = new System.Windows.Forms.DataGridViewImageColumn();
            this.operation = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnExits = new System.Windows.Forms.Button();
            this.btnTransferAll = new System.Windows.Forms.Button();
            this.picSendScale = new System.Windows.Forms.PictureBox();
            this.pnlDgvHead = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.picScreen = new System.Windows.Forms.PictureBox();
            this.picScaleFaild = new System.Windows.Forms.PictureBox();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.lblMenu = new System.Windows.Forms.Label();
            this.picMenu = new System.Windows.Forms.PictureBox();
            this.lblShopName = new System.Windows.Forms.Label();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnSendScale = new System.Windows.Forms.Button();
            this.btnFaile = new System.Windows.Forms.Button();
            this.btnSuccess = new System.Windows.Forms.Button();
            this.rbtnPageDown = new ZhuiZhi_Integral_Scale_UncleFruit.RoundButton();
            this.rbtnPageUp = new ZhuiZhi_Integral_Scale_UncleFruit.RoundButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSendScale)).BeginInit();
            this.pnlDgvHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picScaleFaild)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvScale
            // 
            this.dgvScale.AllowUserToAddRows = false;
            this.dgvScale.AllowUserToDeleteRows = false;
            this.dgvScale.AllowUserToResizeColumns = false;
            this.dgvScale.AllowUserToResizeRows = false;
            this.dgvScale.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvScale.BackgroundColor = System.Drawing.Color.White;
            this.dgvScale.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvScale.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvScale.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvScale.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvScale.ColumnHeadersHeight = 50;
            this.dgvScale.ColumnHeadersVisible = false;
            this.dgvScale.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serianno,
            this.scalename,
            this.scaleip,
            this.scaletype,
            this.lasttime,
            this.ScaleStatus,
            this.operation});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvScale.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvScale.GridColor = System.Drawing.Color.Silver;
            this.dgvScale.Location = new System.Drawing.Point(12, 125);
            this.dgvScale.MultiSelect = false;
            this.dgvScale.Name = "dgvScale";
            this.dgvScale.ReadOnly = true;
            this.dgvScale.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvScale.RowHeadersVisible = false;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.White;
            this.dgvScale.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvScale.RowTemplate.Height = 50;
            this.dgvScale.Size = new System.Drawing.Size(1154, 554);
            this.dgvScale.TabIndex = 25;
            this.dgvScale.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvScale_CellContentClick);
            // 
            // serianno
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.serianno.DefaultCellStyle = dataGridViewCellStyle2;
            this.serianno.FillWeight = 10F;
            this.serianno.HeaderText = "编号";
            this.serianno.Name = "serianno";
            this.serianno.ReadOnly = true;
            this.serianno.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // scalename
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.scalename.DefaultCellStyle = dataGridViewCellStyle3;
            this.scalename.FillWeight = 25F;
            this.scalename.HeaderText = "秤名称";
            this.scalename.Name = "scalename";
            this.scalename.ReadOnly = true;
            this.scalename.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // scaleip
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(5);
            this.scaleip.DefaultCellStyle = dataGridViewCellStyle4;
            this.scaleip.FillWeight = 25F;
            this.scaleip.HeaderText = "秤IP";
            this.scaleip.Name = "scaleip";
            this.scaleip.ReadOnly = true;
            this.scaleip.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.scaleip.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // scaletype
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.scaletype.DefaultCellStyle = dataGridViewCellStyle5;
            this.scaletype.FillWeight = 25F;
            this.scaletype.HeaderText = "电子秤类型";
            this.scaletype.Name = "scaletype";
            this.scaletype.ReadOnly = true;
            this.scaletype.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // lasttime
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lasttime.DefaultCellStyle = dataGridViewCellStyle6;
            this.lasttime.FillWeight = 25F;
            this.lasttime.HeaderText = "上次同步时间";
            this.lasttime.Name = "lasttime";
            this.lasttime.ReadOnly = true;
            this.lasttime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ScaleStatus
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.NullValue = null;
            this.ScaleStatus.DefaultCellStyle = dataGridViewCellStyle7;
            this.ScaleStatus.FillWeight = 9F;
            this.ScaleStatus.HeaderText = "传秤状态";
            this.ScaleStatus.Name = "ScaleStatus";
            this.ScaleStatus.ReadOnly = true;
            this.ScaleStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // operation
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.NullValue = null;
            this.operation.DefaultCellStyle = dataGridViewCellStyle8;
            this.operation.FillWeight = 15F;
            this.operation.HeaderText = "操作";
            this.operation.Name = "operation";
            this.operation.ReadOnly = true;
            this.operation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.operation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btnExits
            // 
            this.btnExits.BackColor = System.Drawing.Color.Transparent;
            this.btnExits.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExits.BackgroundImage")));
            this.btnExits.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExits.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExits.FlatAppearance.BorderSize = 0;
            this.btnExits.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExits.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExits.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnExits.ForeColor = System.Drawing.Color.White;
            this.btnExits.Location = new System.Drawing.Point(945, 704);
            this.btnExits.Name = "btnExits";
            this.btnExits.Size = new System.Drawing.Size(90, 35);
            this.btnExits.TabIndex = 29;
            this.btnExits.Text = "退出";
            this.btnExits.UseVisualStyleBackColor = false;
            this.btnExits.Visible = false;
            this.btnExits.Click += new System.EventHandler(this.btnExits_Click);
            // 
            // btnTransferAll
            // 
            this.btnTransferAll.BackColor = System.Drawing.Color.Transparent;
            this.btnTransferAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTransferAll.BackgroundImage")));
            this.btnTransferAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTransferAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnTransferAll.FlatAppearance.BorderSize = 0;
            this.btnTransferAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnTransferAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnTransferAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransferAll.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnTransferAll.ForeColor = System.Drawing.Color.White;
            this.btnTransferAll.Location = new System.Drawing.Point(1041, 695);
            this.btnTransferAll.Name = "btnTransferAll";
            this.btnTransferAll.Size = new System.Drawing.Size(125, 50);
            this.btnTransferAll.TabIndex = 28;
            this.btnTransferAll.Text = "一键传秤";
            this.btnTransferAll.UseVisualStyleBackColor = false;
            this.btnTransferAll.Click += new System.EventHandler(this.btnTransferAll_Click);
            // 
            // picSendScale
            // 
            this.picSendScale.Image = ((System.Drawing.Image)(resources.GetObject("picSendScale.Image")));
            this.picSendScale.Location = new System.Drawing.Point(615, 704);
            this.picSendScale.Name = "picSendScale";
            this.picSendScale.Size = new System.Drawing.Size(155, 44);
            this.picSendScale.TabIndex = 30;
            this.picSendScale.TabStop = false;
            this.picSendScale.Visible = false;
            // 
            // pnlDgvHead
            // 
            this.pnlDgvHead.BackColor = System.Drawing.Color.White;
            this.pnlDgvHead.Controls.Add(this.label6);
            this.pnlDgvHead.Controls.Add(this.label5);
            this.pnlDgvHead.Controls.Add(this.label4);
            this.pnlDgvHead.Controls.Add(this.label3);
            this.pnlDgvHead.Controls.Add(this.label2);
            this.pnlDgvHead.Controls.Add(this.label1);
            this.pnlDgvHead.Location = new System.Drawing.Point(12, 73);
            this.pnlDgvHead.Name = "pnlDgvHead";
            this.pnlDgvHead.Size = new System.Drawing.Size(1154, 49);
            this.pnlDgvHead.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(1059, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 21);
            this.label6.TabIndex = 37;
            this.label6.Text = "操作";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(868, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 21);
            this.label5.TabIndex = 36;
            this.label5.Text = "上次同步时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(577, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 21);
            this.label4.TabIndex = 35;
            this.label4.Text = "电子秤类型";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(386, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 21);
            this.label3.TabIndex = 34;
            this.label3.Text = "秤IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(157, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 21);
            this.label2.TabIndex = 33;
            this.label2.Text = "秤名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(18, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 21);
            this.label1.TabIndex = 32;
            this.label1.Text = "编号";
            // 
            // picScreen
            // 
            this.picScreen.BackColor = System.Drawing.Color.Red;
            this.picScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picScreen.Location = new System.Drawing.Point(0, 0);
            this.picScreen.Name = "picScreen";
            this.picScreen.Size = new System.Drawing.Size(10, 10);
            this.picScreen.TabIndex = 32;
            this.picScreen.TabStop = false;
            this.picScreen.Visible = false;
            // 
            // picScaleFaild
            // 
            this.picScaleFaild.Image = ((System.Drawing.Image)(resources.GetObject("picScaleFaild.Image")));
            this.picScaleFaild.Location = new System.Drawing.Point(776, 704);
            this.picScaleFaild.Name = "picScaleFaild";
            this.picScaleFaild.Size = new System.Drawing.Size(153, 44);
            this.picScaleFaild.TabIndex = 34;
            this.picScaleFaild.TabStop = false;
            this.picScaleFaild.Visible = false;
            // 
            // pnlHead
            // 
            this.pnlHead.BackColor = System.Drawing.Color.Black;
            this.pnlHead.Controls.Add(this.pnlMenu);
            this.pnlHead.Controls.Add(this.lblShopName);
            this.pnlHead.Controls.Add(this.btnCancle);
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(1178, 60);
            this.pnlHead.TabIndex = 35;
            // 
            // pnlMenu
            // 
            this.pnlMenu.Controls.Add(this.lblMenu);
            this.pnlMenu.Controls.Add(this.picMenu);
            this.pnlMenu.Location = new System.Drawing.Point(1020, 8);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(150, 45);
            this.pnlMenu.TabIndex = 50;
            // 
            // lblMenu
            // 
            this.lblMenu.AutoSize = true;
            this.lblMenu.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblMenu.ForeColor = System.Drawing.Color.White;
            this.lblMenu.Location = new System.Drawing.Point(103, 12);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(42, 21);
            this.lblMenu.TabIndex = 38;
            this.lblMenu.Text = "店铺";
            // 
            // picMenu
            // 
            this.picMenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picMenu.BackgroundImage")));
            this.picMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picMenu.Location = new System.Drawing.Point(69, 12);
            this.picMenu.Name = "picMenu";
            this.picMenu.Size = new System.Drawing.Size(21, 21);
            this.picMenu.TabIndex = 0;
            this.picMenu.TabStop = false;
            // 
            // lblShopName
            // 
            this.lblShopName.AutoSize = true;
            this.lblShopName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblShopName.ForeColor = System.Drawing.Color.White;
            this.lblShopName.Location = new System.Drawing.Point(20, 20);
            this.lblShopName.Name = "lblShopName";
            this.lblShopName.Size = new System.Drawing.Size(42, 21);
            this.lblShopName.TabIndex = 49;
            this.lblShopName.Text = "店铺";
            // 
            // btnCancle
            // 
            this.btnCancle.BackColor = System.Drawing.Color.Black;
            this.btnCancle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancle.BackgroundImage")));
            this.btnCancle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancle.FlatAppearance.BorderSize = 0;
            this.btnCancle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancle.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.btnCancle.ForeColor = System.Drawing.Color.White;
            this.btnCancle.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnCancle.Location = new System.Drawing.Point(880, 14);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(95, 35);
            this.btnCancle.TabIndex = 34;
            this.btnCancle.Text = "返回";
            this.btnCancle.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnCancle.UseVisualStyleBackColor = false;
            this.btnCancle.Click += new System.EventHandler(this.btnExits_Click);
            // 
            // btnSendScale
            // 
            this.btnSendScale.BackColor = System.Drawing.Color.Transparent;
            this.btnSendScale.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSendScale.BackgroundImage")));
            this.btnSendScale.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSendScale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendScale.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSendScale.ForeColor = System.Drawing.Color.White;
            this.btnSendScale.Location = new System.Drawing.Point(1041, 183);
            this.btnSendScale.Margin = new System.Windows.Forms.Padding(2);
            this.btnSendScale.Name = "btnSendScale";
            this.btnSendScale.Size = new System.Drawing.Size(110, 40);
            this.btnSendScale.TabIndex = 38;
            this.btnSendScale.Text = "传秤";
            this.btnSendScale.UseVisualStyleBackColor = false;
            this.btnSendScale.Visible = false;
            // 
            // btnFaile
            // 
            this.btnFaile.BackColor = System.Drawing.Color.Transparent;
            this.btnFaile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFaile.BackgroundImage")));
            this.btnFaile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFaile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFaile.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnFaile.ForeColor = System.Drawing.Color.White;
            this.btnFaile.Location = new System.Drawing.Point(963, 189);
            this.btnFaile.Margin = new System.Windows.Forms.Padding(2);
            this.btnFaile.Name = "btnFaile";
            this.btnFaile.Size = new System.Drawing.Size(74, 30);
            this.btnFaile.TabIndex = 39;
            this.btnFaile.Text = "传秤失败";
            this.btnFaile.UseVisualStyleBackColor = false;
            this.btnFaile.Visible = false;
            // 
            // btnSuccess
            // 
            this.btnSuccess.BackColor = System.Drawing.Color.Transparent;
            this.btnSuccess.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSuccess.BackgroundImage")));
            this.btnSuccess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSuccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuccess.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnSuccess.ForeColor = System.Drawing.Color.White;
            this.btnSuccess.Location = new System.Drawing.Point(871, 189);
            this.btnSuccess.Margin = new System.Windows.Forms.Padding(2);
            this.btnSuccess.Name = "btnSuccess";
            this.btnSuccess.Size = new System.Drawing.Size(74, 30);
            this.btnSuccess.TabIndex = 40;
            this.btnSuccess.Text = "传秤成功";
            this.btnSuccess.UseVisualStyleBackColor = false;
            this.btnSuccess.Visible = false;
            // 
            // rbtnPageDown
            // 
            this.rbtnPageDown.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(191)))), ((int)(((byte)(136)))));
            this.rbtnPageDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(191)))), ((int)(((byte)(136)))));
            this.rbtnPageDown.Image = null;
            this.rbtnPageDown.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnPageDown.Location = new System.Drawing.Point(162, 695);
            this.rbtnPageDown.Name = "rbtnPageDown";
            this.rbtnPageDown.PenColor = System.Drawing.Color.Black;
            this.rbtnPageDown.PenWidth = 1;
            this.rbtnPageDown.RoundRadius = 10;
            this.rbtnPageDown.ShowImg = false;
            this.rbtnPageDown.ShowText = "下一页";
            this.rbtnPageDown.Size = new System.Drawing.Size(140, 50);
            this.rbtnPageDown.TabIndex = 43;
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
            this.rbtnPageUp.Location = new System.Drawing.Point(12, 695);
            this.rbtnPageUp.Name = "rbtnPageUp";
            this.rbtnPageUp.PenColor = System.Drawing.Color.Black;
            this.rbtnPageUp.PenWidth = 1;
            this.rbtnPageUp.RoundRadius = 10;
            this.rbtnPageUp.ShowImg = false;
            this.rbtnPageUp.ShowText = "上一页";
            this.rbtnPageUp.Size = new System.Drawing.Size(140, 50);
            this.rbtnPageUp.TabIndex = 42;
            this.rbtnPageUp.TextForeColor = System.Drawing.Color.White;
            this.rbtnPageUp.WhetherEnable = true;
            this.rbtnPageUp.ButtonClick += new System.EventHandler(this.rbtnPageUp_ButtonClick);
            // 
            // frmScale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1178, 760);
            this.ControlBox = false;
            this.Controls.Add(this.picScreen);
            this.Controls.Add(this.btnSuccess);
            this.Controls.Add(this.btnFaile);
            this.Controls.Add(this.btnSendScale);
            this.Controls.Add(this.pnlHead);
            this.Controls.Add(this.picScaleFaild);
            this.Controls.Add(this.pnlDgvHead);
            this.Controls.Add(this.picSendScale);
            this.Controls.Add(this.btnExits);
            this.Controls.Add(this.btnTransferAll);
            this.Controls.Add(this.dgvScale);
            this.Controls.Add(this.rbtnPageDown);
            this.Controls.Add(this.rbtnPageUp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmScale";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmScale_FormClosing);
            this.Shown += new System.EventHandler(this.frmScale_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSendScale)).EndInit();
            this.pnlDgvHead.ResumeLayout(false);
            this.pnlDgvHead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picScaleFaild)).EndInit();
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvScale;
        private System.Windows.Forms.Button btnExits;
        private System.Windows.Forms.Button btnTransferAll;
        private System.Windows.Forms.PictureBox picSendScale;
        private System.Windows.Forms.Panel pnlDgvHead;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picScreen;
        private System.Windows.Forms.PictureBox picScaleFaild;
        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnSendScale;
        private System.Windows.Forms.Button btnFaile;
        private System.Windows.Forms.Button btnSuccess;
        private System.Windows.Forms.DataGridViewTextBoxColumn serianno;
        private System.Windows.Forms.DataGridViewTextBoxColumn scalename;
        private System.Windows.Forms.DataGridViewTextBoxColumn scaleip;
        private System.Windows.Forms.DataGridViewTextBoxColumn scaletype;
        private System.Windows.Forms.DataGridViewTextBoxColumn lasttime;
        private System.Windows.Forms.DataGridViewImageColumn ScaleStatus;
        private System.Windows.Forms.DataGridViewImageColumn operation;
        private RoundButton rbtnPageDown;
        private RoundButton rbtnPageUp;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.PictureBox picMenu;
        private System.Windows.Forms.Label lblShopName;
    }
}