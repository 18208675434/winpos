namespace ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI
{
    partial class FormBrokenBatch
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBrokenBatch));
            this.label1 = new System.Windows.Forms.Label();
            this.dgvCoupon = new System.Windows.Forms.DataGridView();
            this.brokeykey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brokenvalue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.couponcode = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnCancle = new System.Windows.Forms.Button();
            this.picCheck = new System.Windows.Forms.PictureBox();
            this.rbtnPageDown = new ZhuiZhi_Integral_Scale_UncleFruit.RoundButton();
            this.rbtnPageUp = new ZhuiZhi_Integral_Scale_UncleFruit.RoundButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoupon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 24);
            this.label1.TabIndex = 53;
            this.label1.Text = "批量设置报损原因";
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
            this.dgvCoupon.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvCoupon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCoupon.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
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
            this.brokeykey,
            this.brokenvalue,
            this.couponcode});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCoupon.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvCoupon.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.dgvCoupon.Location = new System.Drawing.Point(12, 61);
            this.dgvCoupon.Name = "dgvCoupon";
            this.dgvCoupon.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvCoupon.RowHeadersVisible = false;
            this.dgvCoupon.RowTemplate.Height = 70;
            this.dgvCoupon.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvCoupon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCoupon.Size = new System.Drawing.Size(356, 347);
            this.dgvCoupon.TabIndex = 52;
            this.dgvCoupon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCoupon_CellClick);
            // 
            // brokeykey
            // 
            this.brokeykey.HeaderText = "报损key";
            this.brokeykey.Name = "brokeykey";
            this.brokeykey.Visible = false;
            // 
            // brokenvalue
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.brokenvalue.DefaultCellStyle = dataGridViewCellStyle3;
            this.brokenvalue.HeaderText = "报损value";
            this.brokenvalue.Name = "brokenvalue";
            // 
            // couponcode
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle4.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle4.NullValue")));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            this.couponcode.DefaultCellStyle = dataGridViewCellStyle4;
            this.couponcode.FillWeight = 20F;
            this.couponcode.HeaderText = "优惠券号";
            this.couponcode.Name = "couponcode";
            this.couponcode.ReadOnly = true;
            this.couponcode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
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
            this.btnCancle.Location = new System.Drawing.Point(341, 12);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(26, 26);
            this.btnCancle.TabIndex = 56;
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // picCheck
            // 
            this.picCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picCheck.BackgroundImage")));
            this.picCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picCheck.Location = new System.Drawing.Point(258, 20);
            this.picCheck.Name = "picCheck";
            this.picCheck.Size = new System.Drawing.Size(25, 25);
            this.picCheck.TabIndex = 57;
            this.picCheck.TabStop = false;
            this.picCheck.Visible = false;
            // 
            // rbtnPageDown
            // 
            this.rbtnPageDown.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(191)))), ((int)(((byte)(136)))));
            this.rbtnPageDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(191)))), ((int)(((byte)(136)))));
            this.rbtnPageDown.Image = null;
            this.rbtnPageDown.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnPageDown.Location = new System.Drawing.Point(197, 414);
            this.rbtnPageDown.Name = "rbtnPageDown";
            this.rbtnPageDown.PenColor = System.Drawing.Color.Black;
            this.rbtnPageDown.PenWidth = 1;
            this.rbtnPageDown.RoundRadius = 1;
            this.rbtnPageDown.ShowImg = false;
            this.rbtnPageDown.ShowText = "下一页";
            this.rbtnPageDown.Size = new System.Drawing.Size(170, 45);
            this.rbtnPageDown.TabIndex = 55;
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
            this.rbtnPageUp.Location = new System.Drawing.Point(12, 414);
            this.rbtnPageUp.Name = "rbtnPageUp";
            this.rbtnPageUp.PenColor = System.Drawing.Color.Black;
            this.rbtnPageUp.PenWidth = 1;
            this.rbtnPageUp.RoundRadius = 1;
            this.rbtnPageUp.ShowImg = false;
            this.rbtnPageUp.ShowText = "上一页";
            this.rbtnPageUp.Size = new System.Drawing.Size(170, 45);
            this.rbtnPageUp.TabIndex = 54;
            this.rbtnPageUp.TextForeColor = System.Drawing.Color.White;
            this.rbtnPageUp.WhetherEnable = true;
            this.rbtnPageUp.ButtonClick += new System.EventHandler(this.rbtnPageUp_ButtonClick);
            // 
            // FormBrokenBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(380, 480);
            this.Controls.Add(this.picCheck);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.rbtnPageDown);
            this.Controls.Add(this.rbtnPageUp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvCoupon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormBrokenBatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormBrokenBatch";
            this.Shown += new System.EventHandler(this.FormBrokenBatch_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoupon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCheck)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RoundButton rbtnPageDown;
        private RoundButton rbtnPageUp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvCoupon;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.PictureBox picCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn brokeykey;
        private System.Windows.Forms.DataGridViewTextBoxColumn brokenvalue;
        private System.Windows.Forms.DataGridViewImageColumn couponcode;
    }
}