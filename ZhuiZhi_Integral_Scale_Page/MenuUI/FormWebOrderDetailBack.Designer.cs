namespace ZhuiZhi_Integral_Scale_UncleFruit.MenuUI
{
    partial class FormWebOrderDetailBack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWebOrderDetailBack));
            this.picClose = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            this.SuspendLayout();
            // 
            // picClose
            // 
            this.picClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picClose.BackgroundImage")));
            this.picClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picClose.Location = new System.Drawing.Point(738, 311);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(57, 69);
            this.picClose.TabIndex = 0;
            this.picClose.TabStop = false;
            this.picClose.Visible = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // FormWebOrderDetailBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.picClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormWebOrderDetailBack";
            this.Opacity = 0.5D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormWebOrderDetailBack";
            this.Load += new System.EventHandler(this.Frm_Load);
            this.Click += new System.EventHandler(this.FormWebOrderDetailBack_Click);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picClose;
    }
}