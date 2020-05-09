namespace WinSaasPOS
{
    partial class RoundButton
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
            this.lblText = new System.Windows.Forms.Label();
            this.picture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblText.ForeColor = System.Drawing.Color.White;
            this.lblText.Location = new System.Drawing.Point(106, 73);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(55, 21);
            this.lblText.TabIndex = 0;
            this.lblText.Text = "label1";
            this.lblText.SizeChanged += new System.EventHandler(this.lblText_SizeChanged);
            // 
            // picture
            // 
            this.picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picture.Location = new System.Drawing.Point(80, 74);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(0, 0);
            this.picture.TabIndex = 1;
            this.picture.TabStop = false;
            this.picture.Visible = false;
            // 
            // RoundButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.picture);
            this.Controls.Add(this.lblText);
            this.Name = "RoundButton";
            this.Size = new System.Drawing.Size(291, 178);
            this.SizeChanged += new System.EventHandler(this.RoundButton_SizeChanged);
            this.Resize += new System.EventHandler(this.RoundButton_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.PictureBox picture;
    }
}
