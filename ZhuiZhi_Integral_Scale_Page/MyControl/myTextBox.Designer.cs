namespace WinTest.myusercontrol
{
    partial class myTextBox
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
            this.txtData.BackColor = System.Drawing.Color.White;
            this.txtData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtData.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtData.Location = new System.Drawing.Point(3, 10);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(190, 22);
            this.txtData.TabIndex = 0;
            this.txtData.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblShuiyin
            // 
            this.lblShuiyin.BackColor = System.Drawing.Color.Coral;
            this.lblShuiyin.Location = new System.Drawing.Point(19, 13);
            this.lblShuiyin.Name = "lblShuiyin";
            this.lblShuiyin.Size = new System.Drawing.Size(55, 15);
            this.lblShuiyin.TabIndex = 1;
            this.lblShuiyin.Text = "label1";
            this.lblShuiyin.Click += new System.EventHandler(this.lblShuiyin_Click);
            // 
            // myTextBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Silver;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblShuiyin);
            this.Controls.Add(this.txtData);
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.ForeColor = System.Drawing.Color.Gray;
            this.Name = "myTextBox";
            this.Size = new System.Drawing.Size(108, 40);
            this.BackColorChanged += new System.EventHandler(this.myTextBox_BackColorChanged);
            this.FontChanged += new System.EventHandler(this.myTextBox_FontChanged);
            this.SizeChanged += new System.EventHandler(this.myTextBox_SizeChanged);
            this.Leave += new System.EventHandler(this.myTextBox_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Label lblShuiyin;
    }
}
