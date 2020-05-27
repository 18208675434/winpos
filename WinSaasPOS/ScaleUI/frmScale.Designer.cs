namespace WinSaasPOS
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScale));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.btnOnLineType = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.Button();
            this.btnWindows = new System.Windows.Forms.Button();
            this.lblShopName = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnSendScale = new System.Windows.Forms.Button();
            this.btnFaile = new System.Windows.Forms.Button();
            this.btnSuccess = new System.Windows.Forms.Button();
            this.pnlSending = new System.Windows.Forms.Panel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.picLoading = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSendScale)).BeginInit();
            this.pnlDgvHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picScaleFaild)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.pnlSending.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
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
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvScale.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle21;
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
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle29.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle29.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle29.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle29.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvScale.DefaultCellStyle = dataGridViewCellStyle29;
            this.dgvScale.GridColor = System.Drawing.Color.Silver;
            this.dgvScale.Location = new System.Drawing.Point(12, 125);
            this.dgvScale.MultiSelect = false;
            this.dgvScale.Name = "dgvScale";
            this.dgvScale.ReadOnly = true;
            this.dgvScale.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvScale.RowHeadersVisible = false;
            dataGridViewCellStyle30.SelectionBackColor = System.Drawing.Color.White;
            this.dgvScale.RowsDefaultCellStyle = dataGridViewCellStyle30;
            this.dgvScale.RowTemplate.Height = 50;
            this.dgvScale.Size = new System.Drawing.Size(1154, 554);
            this.dgvScale.TabIndex = 25;
            this.dgvScale.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvScale_CellContentClick);
            // 
            // serianno
            // 
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle22.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle22.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.serianno.DefaultCellStyle = dataGridViewCellStyle22;
            this.serianno.FillWeight = 10F;
            this.serianno.HeaderText = "编号";
            this.serianno.Name = "serianno";
            this.serianno.ReadOnly = true;
            this.serianno.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // scalename
            // 
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle23.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle23.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.scalename.DefaultCellStyle = dataGridViewCellStyle23;
            this.scalename.FillWeight = 25F;
            this.scalename.HeaderText = "秤名称";
            this.scalename.Name = "scalename";
            this.scalename.ReadOnly = true;
            this.scalename.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // scaleip
            // 
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle24.Padding = new System.Windows.Forms.Padding(5);
            this.scaleip.DefaultCellStyle = dataGridViewCellStyle24;
            this.scaleip.FillWeight = 25F;
            this.scaleip.HeaderText = "秤IP";
            this.scaleip.Name = "scaleip";
            this.scaleip.ReadOnly = true;
            this.scaleip.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.scaleip.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // scaletype
            // 
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle25.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle25.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.scaletype.DefaultCellStyle = dataGridViewCellStyle25;
            this.scaletype.FillWeight = 25F;
            this.scaletype.HeaderText = "电子秤类型";
            this.scaletype.Name = "scaletype";
            this.scaletype.ReadOnly = true;
            this.scaletype.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // lasttime
            // 
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lasttime.DefaultCellStyle = dataGridViewCellStyle26;
            this.lasttime.FillWeight = 25F;
            this.lasttime.HeaderText = "上次同步时间";
            this.lasttime.Name = "lasttime";
            this.lasttime.ReadOnly = true;
            this.lasttime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ScaleStatus
            // 
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle27.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle27.NullValue")));
            this.ScaleStatus.DefaultCellStyle = dataGridViewCellStyle27;
            this.ScaleStatus.FillWeight = 9F;
            this.ScaleStatus.HeaderText = "传秤状态";
            this.ScaleStatus.Name = "ScaleStatus";
            this.ScaleStatus.ReadOnly = true;
            this.ScaleStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // operation
            // 
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle28.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle28.NullValue")));
            this.operation.DefaultCellStyle = dataGridViewCellStyle28;
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
            this.btnExits.Location = new System.Drawing.Point(773, 711);
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
            this.picSendScale.Location = new System.Drawing.Point(41, 704);
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
            this.picScaleFaild.Location = new System.Drawing.Point(402, 704);
            this.picScaleFaild.Name = "picScaleFaild";
            this.picScaleFaild.Size = new System.Drawing.Size(153, 44);
            this.picScaleFaild.TabIndex = 34;
            this.picScaleFaild.TabStop = false;
            this.picScaleFaild.Visible = false;
            // 
            // pnlHead
            // 
            this.pnlHead.BackColor = System.Drawing.Color.Black;
            this.pnlHead.Controls.Add(this.btnOnLineType);
            this.pnlHead.Controls.Add(this.btnMenu);
            this.pnlHead.Controls.Add(this.btnWindows);
            this.pnlHead.Controls.Add(this.lblShopName);
            this.pnlHead.Controls.Add(this.lblTime);
            this.pnlHead.Controls.Add(this.btnCancle);
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(1178, 60);
            this.pnlHead.TabIndex = 35;
            // 
            // btnOnLineType
            // 
            this.btnOnLineType.BackColor = System.Drawing.Color.Black;
            this.btnOnLineType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOnLineType.BackgroundImage")));
            this.btnOnLineType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOnLineType.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnOnLineType.FlatAppearance.BorderSize = 0;
            this.btnOnLineType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOnLineType.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnOnLineType.ForeColor = System.Drawing.Color.White;
            this.btnOnLineType.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnOnLineType.Location = new System.Drawing.Point(268, 17);
            this.btnOnLineType.Name = "btnOnLineType";
            this.btnOnLineType.Size = new System.Drawing.Size(60, 25);
            this.btnOnLineType.TabIndex = 46;
            this.btnOnLineType.Text = "   在线";
            this.btnOnLineType.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnOnLineType.UseVisualStyleBackColor = false;
            // 
            // btnMenu
            // 
            this.btnMenu.AutoSize = true;
            this.btnMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMenu.BackColor = System.Drawing.Color.Black;
            this.btnMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMenu.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMenu.FlatAppearance.BorderSize = 0;
            this.btnMenu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btnMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenu.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnMenu.ForeColor = System.Drawing.Color.White;
            this.btnMenu.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu.Image")));
            this.btnMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenu.Location = new System.Drawing.Point(1033, 13);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(142, 30);
            this.btnMenu.TabIndex = 44;
            this.btnMenu.Text = "某某某，你好  ∨";
            this.btnMenu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMenu.UseVisualStyleBackColor = false;
            // 
            // btnWindows
            // 
            this.btnWindows.BackColor = System.Drawing.Color.Black;
            this.btnWindows.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnWindows.BackgroundImage")));
            this.btnWindows.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnWindows.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnWindows.FlatAppearance.BorderSize = 0;
            this.btnWindows.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWindows.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnWindows.ForeColor = System.Drawing.Color.White;
            this.btnWindows.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnWindows.Location = new System.Drawing.Point(8, 13);
            this.btnWindows.Name = "btnWindows";
            this.btnWindows.Size = new System.Drawing.Size(37, 31);
            this.btnWindows.TabIndex = 43;
            this.btnWindows.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnWindows.UseVisualStyleBackColor = false;
            this.btnWindows.Visible = false;
            this.btnWindows.Click += new System.EventHandler(this.btnWindows_Click);
            // 
            // lblShopName
            // 
            this.lblShopName.AutoSize = true;
            this.lblShopName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblShopName.ForeColor = System.Drawing.Color.White;
            this.lblShopName.Location = new System.Drawing.Point(220, 19);
            this.lblShopName.Name = "lblShopName";
            this.lblShopName.Size = new System.Drawing.Size(42, 21);
            this.lblShopName.TabIndex = 42;
            this.lblShopName.Text = "店铺";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(51, 19);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(163, 21);
            this.lblTime.TabIndex = 41;
            this.lblTime.Text = "2019-10-10 12:12:39";
            // 
            // btnCancle
            // 
            this.btnCancle.BackColor = System.Drawing.Color.Black;
            this.btnCancle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancle.BackgroundImage")));
            this.btnCancle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancle.FlatAppearance.BorderSize = 0;
            this.btnCancle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancle.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnCancle.ForeColor = System.Drawing.Color.White;
            this.btnCancle.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnCancle.Location = new System.Drawing.Point(901, 16);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(95, 28);
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
            // pnlSending
            // 
            this.pnlSending.BackColor = System.Drawing.Color.Silver;
            this.pnlSending.Controls.Add(this.lblMsg);
            this.pnlSending.Controls.Add(this.picLoading);
            this.pnlSending.Location = new System.Drawing.Point(271, 314);
            this.pnlSending.Name = "pnlSending";
            this.pnlSending.Size = new System.Drawing.Size(168, 135);
            this.pnlSending.TabIndex = 41;
            this.pnlSending.Visible = false;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblMsg.ForeColor = System.Drawing.Color.White;
            this.lblMsg.Location = new System.Drawing.Point(13, 86);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(136, 24);
            this.lblMsg.TabIndex = 4;
            this.lblMsg.Text = "传秤数据下发中";
            // 
            // picLoading
            // 
            this.picLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picLoading.Image = ((System.Drawing.Image)(resources.GetObject("picLoading.Image")));
            this.picLoading.Location = new System.Drawing.Point(55, 18);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(54, 52);
            this.picLoading.TabIndex = 3;
            this.picLoading.TabStop = false;
            // 
            // frmScale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1178, 760);
            this.ControlBox = false;
            this.Controls.Add(this.pnlSending);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmScale";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
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
            this.pnlSending.ResumeLayout(false);
            this.pnlSending.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
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
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button btnWindows;
        private System.Windows.Forms.Label lblShopName;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnSendScale;
        private System.Windows.Forms.Button btnFaile;
        private System.Windows.Forms.Button btnSuccess;
        private System.Windows.Forms.Button btnOnLineType;
        private System.Windows.Forms.DataGridViewTextBoxColumn serianno;
        private System.Windows.Forms.DataGridViewTextBoxColumn scalename;
        private System.Windows.Forms.DataGridViewTextBoxColumn scaleip;
        private System.Windows.Forms.DataGridViewTextBoxColumn scaletype;
        private System.Windows.Forms.DataGridViewTextBoxColumn lasttime;
        private System.Windows.Forms.DataGridViewImageColumn ScaleStatus;
        private System.Windows.Forms.DataGridViewImageColumn operation;
        private System.Windows.Forms.Panel pnlSending;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.PictureBox picLoading;
    }
}