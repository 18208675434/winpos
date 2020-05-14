namespace MQTTClient
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSubscribe = new System.Windows.Forms.Button();
            this.BtnPublish = new System.Windows.Forms.Button();
            this.txtSendMessage = new System.Windows.Forms.TextBox();
            this.txtPubTopic = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReceiveMessage = new System.Windows.Forms.TextBox();
            this.txtSubTopic = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSubscribe
            // 
            this.btnSubscribe.Location = new System.Drawing.Point(495, 66);
            this.btnSubscribe.Name = "btnSubscribe";
            this.btnSubscribe.Size = new System.Drawing.Size(60, 23);
            this.btnSubscribe.TabIndex = 15;
            this.btnSubscribe.Text = "订阅";
            this.btnSubscribe.UseVisualStyleBackColor = true;
            this.btnSubscribe.Click += new System.EventHandler(this.BtnSubscribe_ClickAsync);
            // 
            // BtnPublish
            // 
            this.BtnPublish.Location = new System.Drawing.Point(266, 436);
            this.BtnPublish.Name = "BtnPublish";
            this.BtnPublish.Size = new System.Drawing.Size(95, 36);
            this.BtnPublish.TabIndex = 14;
            this.BtnPublish.Text = "发布";
            this.BtnPublish.UseVisualStyleBackColor = true;
            this.BtnPublish.Click += new System.EventHandler(this.BtnPublish_Click);
            // 
            // txtSendMessage
            // 
            this.txtSendMessage.Location = new System.Drawing.Point(119, 353);
            this.txtSendMessage.Multiline = true;
            this.txtSendMessage.Name = "txtSendMessage";
            this.txtSendMessage.Size = new System.Drawing.Size(425, 77);
            this.txtSendMessage.TabIndex = 13;
            // 
            // txtPubTopic
            // 
            this.txtPubTopic.Location = new System.Drawing.Point(176, 313);
            this.txtPubTopic.Name = "txtPubTopic";
            this.txtPubTopic.Size = new System.Drawing.Size(368, 21);
            this.txtPubTopic.TabIndex = 12;
            this.txtPubTopic.Text = "manipulated";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 322);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "发布主题";
            // 
            // txtReceiveMessage
            // 
            this.txtReceiveMessage.Location = new System.Drawing.Point(119, 114);
            this.txtReceiveMessage.Multiline = true;
            this.txtReceiveMessage.Name = "txtReceiveMessage";
            this.txtReceiveMessage.Size = new System.Drawing.Size(425, 180);
            this.txtReceiveMessage.TabIndex = 10;
            // 
            // txtSubTopic
            // 
            this.txtSubTopic.Location = new System.Drawing.Point(176, 69);
            this.txtSubTopic.Name = "txtSubTopic";
            this.txtSubTopic.Size = new System.Drawing.Size(297, 21);
            this.txtSubTopic.TabIndex = 9;
            this.txtSubTopic.Text = "promo:change: 0210000001:508888";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(117, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "订阅主题";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(716, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 36);
            this.button1.TabIndex = 16;
            this.button1.Text = "发布";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 641);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSubscribe);
            this.Controls.Add(this.BtnPublish);
            this.Controls.Add(this.txtSendMessage);
            this.Controls.Add(this.txtPubTopic);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtReceiveMessage);
            this.Controls.Add(this.txtSubTopic);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubscribe;
        private System.Windows.Forms.Button BtnPublish;
        private System.Windows.Forms.TextBox txtSendMessage;
        private System.Windows.Forms.TextBox txtPubTopic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtReceiveMessage;
        private System.Windows.Forms.TextBox txtSubTopic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}

