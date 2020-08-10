namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    partial class FormBrokenDetail
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBrokenDetail));
            this.dgvGood = new System.Windows.Forms.DataGridView();
            this.barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.lblMenu = new System.Windows.Forms.Label();
            this.picMenu = new System.Windows.Forms.PictureBox();
            this.btnCancle = new System.Windows.Forms.Button();
            this.lblShopName = new System.Windows.Forms.Label();
            this.pnlDgvHead = new System.Windows.Forms.Panel();
            this.pnlDgvItem = new System.Windows.Forms.Panel();
            this.pnlTotal = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.pnlAdd = new System.Windows.Forms.Panel();
            this.btnIncrease = new System.Windows.Forms.Button();
            this.pnlNum = new System.Windows.Forms.Panel();
            this.btnNum = new System.Windows.Forms.Button();
            this.pnlSinglePrice = new System.Windows.Forms.Panel();
            this.lblSinglePrice = new System.Windows.Forms.Label();
            this.pnlBarCode = new System.Windows.Forms.Panel();
            this.lblSkuCode = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picDelete = new System.Windows.Forms.PictureBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.rbtnPageDown = new ZhuiZhi_Integral_Scale_UncleFruit.RoundButton();
            this.rbtnPageUp = new ZhuiZhi_Integral_Scale_UncleFruit.RoundButton();
            this.picLoading = new ZhuiZhi_Integral_Scale_UncleFruit.transparentPic(this.components);
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblSkuAmount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblBrokenAmount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.pnlMember = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGood)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMenu)).BeginInit();
            this.pnlDgvHead.SuspendLayout();
            this.pnlDgvItem.SuspendLayout();
            this.pnlTotal.SuspendLayout();
            this.pnlAdd.SuspendLayout();
            this.pnlNum.SuspendLayout();
            this.pnlSinglePrice.SuspendLayout();
            this.pnlBarCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDelete)).BeginInit();
            this.pnlMember.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvGood
            // 
            this.dgvGood.AllowUserToAddRows = false;
            this.dgvGood.AllowUserToDeleteRows = false;
            this.dgvGood.AllowUserToResizeColumns = false;
            this.dgvGood.AllowUserToResizeRows = false;
            this.dgvGood.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGood.BackgroundColor = System.Drawing.Color.White;
            this.dgvGood.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGood.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvGood.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 17F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGood.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGood.ColumnHeadersHeight = 30;
            this.dgvGood.ColumnHeadersVisible = false;
            this.dgvGood.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.barcode,
            this.num,
            this.delete});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGood.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvGood.GridColor = System.Drawing.Color.LightGray;
            this.dgvGood.Location = new System.Drawing.Point(11, 142);
            this.dgvGood.MultiSelect = false;
            this.dgvGood.Name = "dgvGood";
            this.dgvGood.ReadOnly = true;
            this.dgvGood.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvGood.RowHeadersVisible = false;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            this.dgvGood.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvGood.RowTemplate.Height = 90;
            this.dgvGood.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvGood.Size = new System.Drawing.Size(817, 556);
            this.dgvGood.TabIndex = 17;
            // 
            // barcode
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 13F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.barcode.DefaultCellStyle = dataGridViewCellStyle2;
            this.barcode.FillWeight = 150F;
            this.barcode.HeaderText = "商品/条码";
            this.barcode.Name = "barcode";
            this.barcode.ReadOnly = true;
            this.barcode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // num
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.num.DefaultCellStyle = dataGridViewCellStyle3;
            this.num.HeaderText = "成本价";
            this.num.Name = "num";
            this.num.ReadOnly = true;
            this.num.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.num.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // delete
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.delete.DefaultCellStyle = dataGridViewCellStyle4;
            this.delete.FillWeight = 70F;
            this.delete.HeaderText = "数量";
            this.delete.Name = "delete";
            this.delete.ReadOnly = true;
            this.delete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label12.Location = new System.Drawing.Point(354, 23);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 24);
            this.label12.TabIndex = 3;
            this.label12.Text = "成本价(元)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label9.Location = new System.Drawing.Point(631, 23);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 24);
            this.label9.TabIndex = 2;
            this.label9.Text = "数量";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(19, 23);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 24);
            this.label6.TabIndex = 0;
            this.label6.Text = "商品/条码";
            // 
            // pnlHead
            // 
            this.pnlHead.BackColor = System.Drawing.Color.Black;
            this.pnlHead.Controls.Add(this.pnlMenu);
            this.pnlHead.Controls.Add(this.btnCancle);
            this.pnlHead.Controls.Add(this.lblShopName);
            this.pnlHead.Font = new System.Drawing.Font("宋体", 9F);
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(1180, 60);
            this.pnlHead.TabIndex = 31;
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
            this.btnCancle.TabIndex = 43;
            this.btnCancle.Text = "返回";
            this.btnCancle.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnCancle.UseVisualStyleBackColor = false;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // lblShopName
            // 
            this.lblShopName.AutoSize = true;
            this.lblShopName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblShopName.ForeColor = System.Drawing.Color.White;
            this.lblShopName.Location = new System.Drawing.Point(20, 20);
            this.lblShopName.Name = "lblShopName";
            this.lblShopName.Size = new System.Drawing.Size(42, 21);
            this.lblShopName.TabIndex = 37;
            this.lblShopName.Text = "店铺";
            // 
            // pnlDgvHead
            // 
            this.pnlDgvHead.BackColor = System.Drawing.Color.White;
            this.pnlDgvHead.Controls.Add(this.label12);
            this.pnlDgvHead.Controls.Add(this.label9);
            this.pnlDgvHead.Controls.Add(this.label6);
            this.pnlDgvHead.Location = new System.Drawing.Point(11, 74);
            this.pnlDgvHead.Name = "pnlDgvHead";
            this.pnlDgvHead.Size = new System.Drawing.Size(817, 66);
            this.pnlDgvHead.TabIndex = 41;
            // 
            // pnlDgvItem
            // 
            this.pnlDgvItem.BackColor = System.Drawing.Color.PaleTurquoise;
            this.pnlDgvItem.Controls.Add(this.pnlTotal);
            this.pnlDgvItem.Controls.Add(this.pnlAdd);
            this.pnlDgvItem.Controls.Add(this.pnlNum);
            this.pnlDgvItem.Controls.Add(this.pnlSinglePrice);
            this.pnlDgvItem.Controls.Add(this.pnlBarCode);
            this.pnlDgvItem.Controls.Add(this.picDelete);
            this.pnlDgvItem.Location = new System.Drawing.Point(12, -360);
            this.pnlDgvItem.Name = "pnlDgvItem";
            this.pnlDgvItem.Size = new System.Drawing.Size(816, 106);
            this.pnlDgvItem.TabIndex = 47;
            // 
            // pnlTotal
            // 
            this.pnlTotal.BackColor = System.Drawing.Color.White;
            this.pnlTotal.Controls.Add(this.lblTotal);
            this.pnlTotal.Location = new System.Drawing.Point(577, 7);
            this.pnlTotal.Name = "pnlTotal";
            this.pnlTotal.Size = new System.Drawing.Size(124, 88);
            this.pnlTotal.TabIndex = 42;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblTotal.ForeColor = System.Drawing.Color.Black;
            this.lblTotal.Location = new System.Drawing.Point(32, 28);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(64, 21);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "label10";
            // 
            // pnlAdd
            // 
            this.pnlAdd.BackColor = System.Drawing.Color.White;
            this.pnlAdd.Controls.Add(this.btnIncrease);
            this.pnlAdd.Location = new System.Drawing.Point(525, 7);
            this.pnlAdd.Name = "pnlAdd";
            this.pnlAdd.Size = new System.Drawing.Size(46, 88);
            this.pnlAdd.TabIndex = 43;
            // 
            // btnIncrease
            // 
            this.btnIncrease.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnIncrease.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIncrease.FlatAppearance.BorderSize = 0;
            this.btnIncrease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIncrease.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnIncrease.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIncrease.Location = new System.Drawing.Point(0, 24);
            this.btnIncrease.Margin = new System.Windows.Forms.Padding(0);
            this.btnIncrease.Name = "btnIncrease";
            this.btnIncrease.Size = new System.Drawing.Size(35, 35);
            this.btnIncrease.TabIndex = 0;
            this.btnIncrease.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIncrease.UseVisualStyleBackColor = false;
            // 
            // pnlNum
            // 
            this.pnlNum.BackColor = System.Drawing.Color.White;
            this.pnlNum.Controls.Add(this.btnNum);
            this.pnlNum.Location = new System.Drawing.Point(395, 7);
            this.pnlNum.Name = "pnlNum";
            this.pnlNum.Size = new System.Drawing.Size(124, 88);
            this.pnlNum.TabIndex = 42;
            // 
            // btnNum
            // 
            this.btnNum.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNum.FlatAppearance.BorderSize = 0;
            this.btnNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNum.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnNum.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNum.Location = new System.Drawing.Point(31, 24);
            this.btnNum.Name = "btnNum";
            this.btnNum.Size = new System.Drawing.Size(93, 35);
            this.btnNum.TabIndex = 0;
            this.btnNum.Text = "123456kg";
            this.btnNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNum.UseVisualStyleBackColor = true;
            // 
            // pnlSinglePrice
            // 
            this.pnlSinglePrice.BackColor = System.Drawing.Color.White;
            this.pnlSinglePrice.Controls.Add(this.lblSinglePrice);
            this.pnlSinglePrice.Location = new System.Drawing.Point(244, 7);
            this.pnlSinglePrice.Name = "pnlSinglePrice";
            this.pnlSinglePrice.Size = new System.Drawing.Size(145, 88);
            this.pnlSinglePrice.TabIndex = 41;
            // 
            // lblSinglePrice
            // 
            this.lblSinglePrice.AutoSize = true;
            this.lblSinglePrice.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblSinglePrice.Location = new System.Drawing.Point(46, 28);
            this.lblSinglePrice.Name = "lblSinglePrice";
            this.lblSinglePrice.Size = new System.Drawing.Size(55, 21);
            this.lblSinglePrice.TabIndex = 2;
            this.lblSinglePrice.Text = "label5";
            // 
            // pnlBarCode
            // 
            this.pnlBarCode.BackColor = System.Drawing.Color.White;
            this.pnlBarCode.Controls.Add(this.lblSkuCode);
            this.pnlBarCode.Controls.Add(this.lblTitle);
            this.pnlBarCode.Location = new System.Drawing.Point(5, 7);
            this.pnlBarCode.Name = "pnlBarCode";
            this.pnlBarCode.Size = new System.Drawing.Size(233, 88);
            this.pnlBarCode.TabIndex = 40;
            // 
            // lblSkuCode
            // 
            this.lblSkuCode.AutoSize = true;
            this.lblSkuCode.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblSkuCode.Location = new System.Drawing.Point(16, 42);
            this.lblSkuCode.Name = "lblSkuCode";
            this.lblSkuCode.Size = new System.Drawing.Size(55, 21);
            this.lblSkuCode.TabIndex = 2;
            this.lblSkuCode.Text = "label3";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblTitle.Location = new System.Drawing.Point(16, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(55, 21);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "label2";
            // 
            // picDelete
            // 
            this.picDelete.BackColor = System.Drawing.Color.White;
            this.picDelete.Image = ((System.Drawing.Image)(resources.GetObject("picDelete.Image")));
            this.picDelete.Location = new System.Drawing.Point(730, 33);
            this.picDelete.Name = "picDelete";
            this.picDelete.Size = new System.Drawing.Size(36, 43);
            this.picDelete.TabIndex = 25;
            this.picDelete.TabStop = false;
            this.picDelete.Visible = false;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewImageColumn1.FillWeight = 89.07754F;
            this.dataGridViewImageColumn1.HeaderText = "操作";
            this.dataGridViewImageColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn1.Image")));
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // rbtnPageDown
            // 
            this.rbtnPageDown.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(191)))), ((int)(((byte)(136)))));
            this.rbtnPageDown.BackColor = System.Drawing.Color.Silver;
            this.rbtnPageDown.Image = null;
            this.rbtnPageDown.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnPageDown.Location = new System.Drawing.Point(688, 704);
            this.rbtnPageDown.Name = "rbtnPageDown";
            this.rbtnPageDown.PenColor = System.Drawing.Color.Black;
            this.rbtnPageDown.PenWidth = 1;
            this.rbtnPageDown.RoundRadius = 10;
            this.rbtnPageDown.ShowImg = false;
            this.rbtnPageDown.ShowText = "下一页";
            this.rbtnPageDown.Size = new System.Drawing.Size(140, 50);
            this.rbtnPageDown.TabIndex = 59;
            this.rbtnPageDown.TextForeColor = System.Drawing.Color.White;
            this.rbtnPageDown.WhetherEnable = false;
            this.rbtnPageDown.ButtonClick += new System.EventHandler(this.rbtnPageDown_ButtonClick);
            // 
            // rbtnPageUp
            // 
            this.rbtnPageUp.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(160)))), ((int)(((byte)(110)))));
            this.rbtnPageUp.BackColor = System.Drawing.Color.Silver;
            this.rbtnPageUp.Image = null;
            this.rbtnPageUp.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnPageUp.Location = new System.Drawing.Point(538, 704);
            this.rbtnPageUp.Name = "rbtnPageUp";
            this.rbtnPageUp.PenColor = System.Drawing.Color.Black;
            this.rbtnPageUp.PenWidth = 1;
            this.rbtnPageUp.RoundRadius = 10;
            this.rbtnPageUp.ShowImg = false;
            this.rbtnPageUp.ShowText = "上一页";
            this.rbtnPageUp.Size = new System.Drawing.Size(140, 50);
            this.rbtnPageUp.TabIndex = 58;
            this.rbtnPageUp.TextForeColor = System.Drawing.Color.White;
            this.rbtnPageUp.WhetherEnable = false;
            this.rbtnPageUp.ButtonClick += new System.EventHandler(this.rbtnPageUp_ButtonClick);
            // 
            // picLoading
            // 
            this.picLoading.CanMove = false;
            this.picLoading.Image = ((System.Drawing.Image)(resources.GetObject("picLoading.Image")));
            this.picLoading.IsFocus = false;
            this.picLoading.Location = new System.Drawing.Point(430, 300);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(50, 54);
            this.picLoading.TabIndex = 60;
            this.picLoading.Text = "transparentPic1";
            this.picLoading.unitScale = null;
            this.picLoading.UnitType = ZhuiZhi_Integral_Scale_UncleFruit.MyControl.EnumUnitType.Station;
            this.picLoading.Visible = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.White;
            this.label18.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label18.Location = new System.Drawing.Point(11, 8);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(88, 25);
            this.label18.TabIndex = 0;
            this.label18.Text = "报损明细";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.White;
            this.label17.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label17.Location = new System.Drawing.Point(11, 48);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(82, 24);
            this.label17.TabIndex = 1;
            this.label17.Text = "报损种类";
            // 
            // lblSkuAmount
            // 
            this.lblSkuAmount.BackColor = System.Drawing.Color.White;
            this.lblSkuAmount.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblSkuAmount.Location = new System.Drawing.Point(171, 48);
            this.lblSkuAmount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSkuAmount.Name = "lblSkuAmount";
            this.lblSkuAmount.Size = new System.Drawing.Size(131, 20);
            this.lblSkuAmount.TabIndex = 4;
            this.lblSkuAmount.Text = "0";
            this.lblSkuAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label2.Location = new System.Drawing.Point(11, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "报损金额";
            // 
            // lblBrokenAmount
            // 
            this.lblBrokenAmount.BackColor = System.Drawing.Color.White;
            this.lblBrokenAmount.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblBrokenAmount.Location = new System.Drawing.Point(171, 81);
            this.lblBrokenAmount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBrokenAmount.Name = "lblBrokenAmount";
            this.lblBrokenAmount.Size = new System.Drawing.Size(131, 21);
            this.lblBrokenAmount.TabIndex = 6;
            this.lblBrokenAmount.Text = "￥0.00";
            this.lblBrokenAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label4.Location = new System.Drawing.Point(11, 115);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "操作人";
            // 
            // lblUserName
            // 
            this.lblUserName.BackColor = System.Drawing.Color.White;
            this.lblUserName.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblUserName.Location = new System.Drawing.Point(171, 115);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(131, 21);
            this.lblUserName.TabIndex = 8;
            this.lblUserName.Text = "--";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlMember
            // 
            this.pnlMember.BackColor = System.Drawing.Color.Transparent;
            this.pnlMember.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlMember.BackgroundImage")));
            this.pnlMember.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMember.Controls.Add(this.lblUserName);
            this.pnlMember.Controls.Add(this.label4);
            this.pnlMember.Controls.Add(this.lblBrokenAmount);
            this.pnlMember.Controls.Add(this.label2);
            this.pnlMember.Controls.Add(this.lblSkuAmount);
            this.pnlMember.Controls.Add(this.label17);
            this.pnlMember.Controls.Add(this.label18);
            this.pnlMember.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlMember.Location = new System.Drawing.Point(849, 74);
            this.pnlMember.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMember.Name = "pnlMember";
            this.pnlMember.Size = new System.Drawing.Size(320, 165);
            this.pnlMember.TabIndex = 54;
            // 
            // FormBrokenDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.rbtnPageDown);
            this.Controls.Add(this.rbtnPageUp);
            this.Controls.Add(this.pnlMember);
            this.Controls.Add(this.pnlDgvItem);
            this.Controls.Add(this.pnlDgvHead);
            this.Controls.Add(this.pnlHead);
            this.Controls.Add(this.dgvGood);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormBrokenDetail";
            this.Text = "formbrokencreate";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGood)).EndInit();
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMenu)).EndInit();
            this.pnlDgvHead.ResumeLayout(false);
            this.pnlDgvHead.PerformLayout();
            this.pnlDgvItem.ResumeLayout(false);
            this.pnlTotal.ResumeLayout(false);
            this.pnlTotal.PerformLayout();
            this.pnlAdd.ResumeLayout(false);
            this.pnlNum.ResumeLayout(false);
            this.pnlSinglePrice.ResumeLayout(false);
            this.pnlSinglePrice.PerformLayout();
            this.pnlBarCode.ResumeLayout(false);
            this.pnlBarCode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDelete)).EndInit();
            this.pnlMember.ResumeLayout(false);
            this.pnlMember.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGood;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.PictureBox picDelete;
        //private UserControl.transparentPic picBirthday;
        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Label lblShopName;
        // private UserControl.transparentPic picLoading;
        private System.Windows.Forms.Panel pnlDgvHead;
        private System.Windows.Forms.Panel pnlDgvItem;
        private System.Windows.Forms.Panel pnlTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel pnlAdd;
        private System.Windows.Forms.Panel pnlNum;
        private System.Windows.Forms.Button btnNum;
        private System.Windows.Forms.Panel pnlSinglePrice;
        private System.Windows.Forms.Label lblSinglePrice;
        private System.Windows.Forms.Panel pnlBarCode;
        private System.Windows.Forms.Label lblSkuCode;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnIncrease;
        private System.Windows.Forms.Button btnCancle;
        private RoundButton rbtnPageDown;
        private RoundButton rbtnPageUp;
        private transparentPic picLoading;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblSkuAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblBrokenAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Panel pnlMember;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
        private System.Windows.Forms.DataGridViewTextBoxColumn delete;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.PictureBox picMenu;
    }
}