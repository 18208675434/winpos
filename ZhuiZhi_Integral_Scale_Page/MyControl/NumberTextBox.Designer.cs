namespace ZhuiZhi_Integral_Scale_UncleFruit.MyControl
{
    partial class NumberTextBox
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtData = new System.Windows.Forms.TextBox();
            this.lblShuiyin = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtData
            // 
            this.txtData.BackColor = System.Drawing.SystemColors.Control;
            this.txtData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtData.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtData.Location = new System.Drawing.Point(3, 19);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(128, 22);
            this.txtData.TabIndex = 0;
            this.txtData.Click += new System.EventHandler(this.lblShuiyin_Click);
            this.txtData.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblShuiyin
            // 
            this.lblShuiyin.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblShuiyin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.lblShuiyin.Location = new System.Drawing.Point(25, 20);
            this.lblShuiyin.Name = "lblShuiyin";
            this.lblShuiyin.Size = new System.Drawing.Size(63, 21);
            this.lblShuiyin.TabIndex = 1;
            this.lblShuiyin.Text = "shuiyin";
            this.lblShuiyin.Click += new System.EventHandler(this.lblShuiyin_Click);
            // 
            // NumberTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblShuiyin);
            this.Controls.Add(this.txtData);
            this.Name = "NumberTextBox";
            this.Size = new System.Drawing.Size(195, 55);
            this.Load += new System.EventHandler(this.NumberTextBox_Load);
            this.BackColorChanged += new System.EventHandler(this.myTextBox_BackColorChanged);
            this.FontChanged += new System.EventHandler(this.myTextBox_FontChanged);
            this.SizeChanged += new System.EventHandler(this.myTextBox_SizeChanged);
            this.Click += new System.EventHandler(this.lblShuiyin_Click);
            this.Leave += new System.EventHandler(this.myTextBox_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Label lblShuiyin;
    }
}
