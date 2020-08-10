namespace ZhuiZhi_Integral_Scale_UncleFruit.HelperUI
{
    partial class FormChangeScaleModel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChangeScaleModel));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lbtnOK = new System.Windows.Forms.Label();
            this.lbtnCancle = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(100, 24);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "切换秤模式";
            // 
            // lbtnOK
            // 
            this.lbtnOK.AutoSize = true;
            this.lbtnOK.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lbtnOK.ForeColor = System.Drawing.Color.DeepPink;
            this.lbtnOK.Location = new System.Drawing.Point(498, 200);
            this.lbtnOK.Name = "lbtnOK";
            this.lbtnOK.Size = new System.Drawing.Size(52, 27);
            this.lbtnOK.TabIndex = 12;
            this.lbtnOK.Text = "确定";
            this.lbtnOK.Click += new System.EventHandler(this.lbtnOK_Click);
            // 
            // lbtnCancle
            // 
            this.lbtnCancle.AutoSize = true;
            this.lbtnCancle.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lbtnCancle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.lbtnCancle.Location = new System.Drawing.Point(417, 200);
            this.lbtnCancle.Name = "lbtnCancle";
            this.lbtnCancle.Size = new System.Drawing.Size(52, 27);
            this.lbtnCancle.TabIndex = 11;
            this.lbtnCancle.Text = "取消";
            this.lbtnCancle.Click += new System.EventHandler(this.lbtnCancle_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lblInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblInfo.Location = new System.Drawing.Point(120, 22);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(214, 20);
            this.lblInfo.TabIndex = 13;
            this.lblInfo.Text = "(当前模式：称重收银一体模式)";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblMsg.Location = new System.Drawing.Point(20, 109);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(424, 24);
            this.lblMsg.TabIndex = 14;
            this.lblMsg.Text = "您确认要将称重收银一体模式切换为常规秤模式吗？";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Location = new System.Drawing.Point(0, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 1);
            this.panel1.TabIndex = 20;
            this.panel1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Location = new System.Drawing.Point(0, 175);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(600, 1);
            this.panel2.TabIndex = 21;
            this.panel2.Visible = false;
            // 
            // btnCancle
            // 
            this.btnCancle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancle.BackgroundImage")));
            this.btnCancle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancle.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancle.FlatAppearance.BorderSize = 0;
            this.btnCancle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCancle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnCancle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancle.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnCancle.Location = new System.Drawing.Point(562, 12);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(26, 26);
            this.btnCancle.TabIndex = 63;
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.lbtnCancle_Click);
            // 
            // FormChangeScaleModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 250);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lbtnOK);
            this.Controls.Add(this.lbtnCancle);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormChangeScaleModel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormChangeScaleModel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lbtnOK;
        private System.Windows.Forms.Label lbtnCancle;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancle;
    }
}