namespace ToledoTest
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnZero = new System.Windows.Forms.Button();
            this.btnSendTare = new System.Windows.Forms.Button();
            this.txtTare = new System.Windows.Forms.TextBox();
            this.txtPreTare = new System.Windows.Forms.TextBox();
            this.btnPreTare = new System.Windows.Forms.Button();
            this.txtTareBykey = new System.Windows.Forms.TextBox();
            this.btnTareBYkey = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(39, 93);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(450, 444);
            this.listBox1.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(39, 57);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(56, 24);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "读取";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(229, 48);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(88, 26);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "1000";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "interval";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(383, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 4;
            this.button1.Text = "清空";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnZero
            // 
            this.btnZero.Location = new System.Drawing.Point(696, 57);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(75, 33);
            this.btnZero.TabIndex = 5;
            this.btnZero.Text = "清零";
            this.btnZero.UseVisualStyleBackColor = true;
            this.btnZero.Click += new System.EventHandler(this.btnZero_Click);
            // 
            // btnSendTare
            // 
            this.btnSendTare.Location = new System.Drawing.Point(696, 127);
            this.btnSendTare.Name = "btnSendTare";
            this.btnSendTare.Size = new System.Drawing.Size(75, 33);
            this.btnSendTare.TabIndex = 6;
            this.btnSendTare.Text = "设置皮重";
            this.btnSendTare.UseVisualStyleBackColor = true;
            this.btnSendTare.Click += new System.EventHandler(this.btnSendTare_Click);
            // 
            // txtTare
            // 
            this.txtTare.Location = new System.Drawing.Point(592, 130);
            this.txtTare.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTare.Name = "txtTare";
            this.txtTare.Size = new System.Drawing.Size(88, 26);
            this.txtTare.TabIndex = 7;
            this.txtTare.Text = "0.020";
            // 
            // txtPreTare
            // 
            this.txtPreTare.Location = new System.Drawing.Point(592, 208);
            this.txtPreTare.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPreTare.Name = "txtPreTare";
            this.txtPreTare.Size = new System.Drawing.Size(88, 26);
            this.txtPreTare.TabIndex = 9;
            this.txtPreTare.Text = "0.020";
            // 
            // btnPreTare
            // 
            this.btnPreTare.Location = new System.Drawing.Point(696, 205);
            this.btnPreTare.Name = "btnPreTare";
            this.btnPreTare.Size = new System.Drawing.Size(75, 33);
            this.btnPreTare.TabIndex = 8;
            this.btnPreTare.Text = "预置去皮";
            this.btnPreTare.UseVisualStyleBackColor = true;
            this.btnPreTare.Click += new System.EventHandler(this.btnPreTare_Click);
            // 
            // txtTareBykey
            // 
            this.txtTareBykey.Location = new System.Drawing.Point(796, 279);
            this.txtTareBykey.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTareBykey.Name = "txtTareBykey";
            this.txtTareBykey.Size = new System.Drawing.Size(88, 26);
            this.txtTareBykey.TabIndex = 11;
            // 
            // btnTareBYkey
            // 
            this.btnTareBYkey.Location = new System.Drawing.Point(696, 276);
            this.btnTareBYkey.Name = "btnTareBYkey";
            this.btnTareBYkey.Size = new System.Drawing.Size(75, 33);
            this.btnTareBYkey.TabIndex = 10;
            this.btnTareBYkey.Text = "按键去皮";
            this.btnTareBYkey.UseVisualStyleBackColor = true;
            this.btnTareBYkey.Click += new System.EventHandler(this.btnTareBYkey_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 552);
            this.Controls.Add(this.txtTareBykey);
            this.Controls.Add(this.btnTareBYkey);
            this.Controls.Add(this.txtPreTare);
            this.Controls.Add(this.btnPreTare);
            this.Controls.Add(this.txtTare);
            this.Controls.Add(this.btnSendTare);
            this.Controls.Add(this.btnZero);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.listBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnZero;
        private System.Windows.Forms.Button btnSendTare;
        private System.Windows.Forms.TextBox txtTare;
        private System.Windows.Forms.TextBox txtPreTare;
        private System.Windows.Forms.Button btnPreTare;
        private System.Windows.Forms.TextBox txtTareBykey;
        private System.Windows.Forms.Button btnTareBYkey;
    }
}

