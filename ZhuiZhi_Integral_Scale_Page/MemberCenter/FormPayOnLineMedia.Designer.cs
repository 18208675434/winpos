namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    partial class FormPayOnLineMedia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPayOnLineMedia));
            this.lblPayInfo1 = new System.Windows.Forms.Label();
            this.picPayInfo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picPayInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPayInfo1
            // 
            this.lblPayInfo1.Font = new System.Drawing.Font("微软雅黑", 22F);
            this.lblPayInfo1.ForeColor = System.Drawing.Color.Black;
            this.lblPayInfo1.Location = new System.Drawing.Point(318, 595);
            this.lblPayInfo1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPayInfo1.Name = "lblPayInfo1";
            this.lblPayInfo1.Size = new System.Drawing.Size(534, 55);
            this.lblPayInfo1.TabIndex = 11;
            this.lblPayInfo1.Text = "请出示微信或支付宝付款码";
            this.lblPayInfo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picPayInfo
            // 
            this.picPayInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picPayInfo.BackgroundImage")));
            this.picPayInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picPayInfo.Location = new System.Drawing.Point(337, 73);
            this.picPayInfo.Margin = new System.Windows.Forms.Padding(2);
            this.picPayInfo.Name = "picPayInfo";
            this.picPayInfo.Size = new System.Drawing.Size(500, 500);
            this.picPayInfo.TabIndex = 10;
            this.picPayInfo.TabStop = false;
            // 
            // FormPayOnLineMedia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.Controls.Add(this.lblPayInfo1);
            this.Controls.Add(this.picPayInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPayOnLineMedia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormPayOnLineMedia";
            ((System.ComponentModel.ISupportInitialize)(this.picPayInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPayInfo1;
        private System.Windows.Forms.PictureBox picPayInfo;
    }
}