namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    partial class frmScaleResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScaleResult));
            this.picStatus = new System.Windows.Forms.PictureBox();
            this.lblSuccess = new System.Windows.Forms.Label();
            this.btnKnow = new System.Windows.Forms.Button();
            this.btnAgain = new System.Windows.Forms.Button();
            this.picSecuss = new System.Windows.Forms.PictureBox();
            this.picFaile = new System.Windows.Forms.PictureBox();
            this.lblScaleName = new System.Windows.Forms.Label();
            this.lblErrorMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSecuss)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFaile)).BeginInit();
            this.SuspendLayout();
            // 
            // picStatus
            // 
            this.picStatus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picStatus.BackgroundImage")));
            this.picStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picStatus.Location = new System.Drawing.Point(112, 66);
            this.picStatus.Name = "picStatus";
            this.picStatus.Size = new System.Drawing.Size(45, 45);
            this.picStatus.TabIndex = 3;
            this.picStatus.TabStop = false;
            // 
            // lblSuccess
            // 
            this.lblSuccess.AutoSize = true;
            this.lblSuccess.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.lblSuccess.Location = new System.Drawing.Point(150, 16);
            this.lblSuccess.Name = "lblSuccess";
            this.lblSuccess.Size = new System.Drawing.Size(110, 31);
            this.lblSuccess.TabIndex = 2;
            this.lblSuccess.Text = "下发成功";
            this.lblSuccess.Visible = false;
            // 
            // btnKnow
            // 
            this.btnKnow.BackColor = System.Drawing.Color.Transparent;
            this.btnKnow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnKnow.BackgroundImage")));
            this.btnKnow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnKnow.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnKnow.FlatAppearance.BorderSize = 0;
            this.btnKnow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnKnow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnKnow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKnow.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnKnow.ForeColor = System.Drawing.Color.White;
            this.btnKnow.Location = new System.Drawing.Point(60, 146);
            this.btnKnow.Name = "btnKnow";
            this.btnKnow.Size = new System.Drawing.Size(125, 38);
            this.btnKnow.TabIndex = 29;
            this.btnKnow.Text = "知道了";
            this.btnKnow.UseVisualStyleBackColor = false;
            this.btnKnow.Click += new System.EventHandler(this.btnKnow_Click);
            // 
            // btnAgain
            // 
            this.btnAgain.BackColor = System.Drawing.Color.Transparent;
            this.btnAgain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAgain.BackgroundImage")));
            this.btnAgain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAgain.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAgain.FlatAppearance.BorderSize = 0;
            this.btnAgain.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAgain.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAgain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgain.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnAgain.ForeColor = System.Drawing.Color.White;
            this.btnAgain.Location = new System.Drawing.Point(208, 146);
            this.btnAgain.Name = "btnAgain";
            this.btnAgain.Size = new System.Drawing.Size(125, 38);
            this.btnAgain.TabIndex = 30;
            this.btnAgain.Text = "重新下发";
            this.btnAgain.UseVisualStyleBackColor = false;
            this.btnAgain.Click += new System.EventHandler(this.btnAgain_Click);
            // 
            // picSecuss
            // 
            this.picSecuss.Image = ((System.Drawing.Image)(resources.GetObject("picSecuss.Image")));
            this.picSecuss.Location = new System.Drawing.Point(12, 68);
            this.picSecuss.Name = "picSecuss";
            this.picSecuss.Size = new System.Drawing.Size(34, 32);
            this.picSecuss.TabIndex = 31;
            this.picSecuss.TabStop = false;
            this.picSecuss.Visible = false;
            // 
            // picFaile
            // 
            this.picFaile.Image = ((System.Drawing.Image)(resources.GetObject("picFaile.Image")));
            this.picFaile.Location = new System.Drawing.Point(12, 112);
            this.picFaile.Name = "picFaile";
            this.picFaile.Size = new System.Drawing.Size(34, 32);
            this.picFaile.TabIndex = 32;
            this.picFaile.TabStop = false;
            this.picFaile.Visible = false;
            // 
            // lblScaleName
            // 
            this.lblScaleName.AutoSize = true;
            this.lblScaleName.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblScaleName.Location = new System.Drawing.Point(178, 66);
            this.lblScaleName.Name = "lblScaleName";
            this.lblScaleName.Size = new System.Drawing.Size(82, 24);
            this.lblScaleName.TabIndex = 33;
            this.lblScaleName.Text = "下发成功";
            this.lblScaleName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblErrorMsg
            // 
            this.lblErrorMsg.AutoSize = true;
            this.lblErrorMsg.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblErrorMsg.Location = new System.Drawing.Point(178, 90);
            this.lblErrorMsg.Name = "lblErrorMsg";
            this.lblErrorMsg.Size = new System.Drawing.Size(82, 24);
            this.lblErrorMsg.TabIndex = 34;
            this.lblErrorMsg.Text = "下发成功";
            this.lblErrorMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmScaleResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(411, 220);
            this.Controls.Add(this.lblErrorMsg);
            this.Controls.Add(this.lblScaleName);
            this.Controls.Add(this.picFaile);
            this.Controls.Add(this.picSecuss);
            this.Controls.Add(this.btnAgain);
            this.Controls.Add(this.btnKnow);
            this.Controls.Add(this.picStatus);
            this.Controls.Add(this.lblSuccess);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmScaleResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmScaleResult";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmScaleResult_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSecuss)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFaile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picStatus;
        private System.Windows.Forms.Label lblSuccess;
        private System.Windows.Forms.Button btnKnow;
        private System.Windows.Forms.Button btnAgain;
        private System.Windows.Forms.PictureBox picSecuss;
        private System.Windows.Forms.PictureBox picFaile;
        private System.Windows.Forms.Label lblScaleName;
        private System.Windows.Forms.Label lblErrorMsg;
    }
}