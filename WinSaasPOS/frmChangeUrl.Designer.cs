namespace WinSaasPOS
{
    partial class frmChangeUrl
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
            this.TXTURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrivateKey = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.rdoZheng = new System.Windows.Forms.RadioButton();
            this.rdoQa = new System.Windows.Forms.RadioButton();
            this.rdoStage = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // TXTURL
            // 
            this.TXTURL.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TXTURL.Location = new System.Drawing.Point(181, 85);
            this.TXTURL.Name = "TXTURL";
            this.TXTURL.Size = new System.Drawing.Size(385, 29);
            this.TXTURL.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(117, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "域名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(117, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "私钥：";
            // 
            // txtPrivateKey
            // 
            this.txtPrivateKey.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPrivateKey.Location = new System.Drawing.Point(181, 135);
            this.txtPrivateKey.Name = "txtPrivateKey";
            this.txtPrivateKey.Size = new System.Drawing.Size(385, 29);
            this.txtPrivateKey.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(229, 194);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(165, 38);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rdoZheng
            // 
            this.rdoZheng.AutoSize = true;
            this.rdoZheng.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoZheng.Location = new System.Drawing.Point(77, 384);
            this.rdoZheng.Name = "rdoZheng";
            this.rdoZheng.Size = new System.Drawing.Size(563, 25);
            this.rdoZheng.TabIndex = 10;
            this.rdoZheng.TabStop = true;
            this.rdoZheng.Text = "https://pos.zhuizhikeji.com    fbNZhX5LSUUhKnCpZo6uZLUVQpmewP";
            this.rdoZheng.UseVisualStyleBackColor = true;
            this.rdoZheng.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // rdoQa
            // 
            this.rdoQa.AutoSize = true;
            this.rdoQa.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoQa.Location = new System.Drawing.Point(77, 342);
            this.rdoQa.Name = "rdoQa";
            this.rdoQa.Size = new System.Drawing.Size(458, 25);
            this.rdoQa.TabIndex = 9;
            this.rdoQa.TabStop = true;
            this.rdoQa.Text = "https://pos-qa.zhuizhikeji.com    kVl55eO1n3DZhWC8Z7";
            this.rdoQa.UseVisualStyleBackColor = true;
            this.rdoQa.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // rdoStage
            // 
            this.rdoStage.AutoSize = true;
            this.rdoStage.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoStage.Location = new System.Drawing.Point(77, 302);
            this.rdoStage.Name = "rdoStage";
            this.rdoStage.Size = new System.Drawing.Size(480, 25);
            this.rdoStage.TabIndex = 8;
            this.rdoStage.TabStop = true;
            this.rdoStage.Text = "https://pos-stage.zhuizhikeji.com    kVl55eO1n3DZhWC8Z7";
            this.rdoStage.UseVisualStyleBackColor = true;
            this.rdoStage.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // frmChangeUrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 554);
            this.Controls.Add(this.rdoZheng);
            this.Controls.Add(this.rdoQa);
            this.Controls.Add(this.rdoStage);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPrivateKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TXTURL);
            this.Name = "frmChangeUrl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmChangeUrl";
            this.Shown += new System.EventHandler(this.frmChangeUrl_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TXTURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPrivateKey;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RadioButton rdoZheng;
        private System.Windows.Forms.RadioButton rdoQa;
        private System.Windows.Forms.RadioButton rdoStage;
    }
}