namespace ScaleTest
{
    partial class FormWinTec
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
            this.components = new System.ComponentModel.Container();
            this.txtprinttype = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.txtFontSize = new System.Windows.Forms.TextBox();
            this.txtPrintText = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtTareBykey = new System.Windows.Forms.TextBox();
            this.btnTareBYkey = new System.Windows.Forms.Button();
            this.txtPreTare = new System.Windows.Forms.TextBox();
            this.btnPreTare = new System.Windows.Forms.Button();
            this.btnZero = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtcom = new System.Windows.Forms.TextBox();
            this.txtbaud = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtprinttype
            // 
            this.txtprinttype.Location = new System.Drawing.Point(607, 318);
            this.txtprinttype.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtprinttype.Name = "txtprinttype";
            this.txtprinttype.Size = new System.Drawing.Size(128, 21);
            this.txtprinttype.TabIndex = 43;
            this.txtprinttype.Text = "0";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(607, 347);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 33);
            this.button3.TabIndex = 42;
            this.button3.Text = "beginprint";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtFontSize
            // 
            this.txtFontSize.Location = new System.Drawing.Point(607, 208);
            this.txtFontSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFontSize.Name = "txtFontSize";
            this.txtFontSize.Size = new System.Drawing.Size(128, 21);
            this.txtFontSize.TabIndex = 41;
            this.txtFontSize.Text = "40";
            // 
            // txtPrintText
            // 
            this.txtPrintText.Location = new System.Drawing.Point(607, 166);
            this.txtPrintText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPrintText.Name = "txtPrintText";
            this.txtPrintText.Size = new System.Drawing.Size(128, 21);
            this.txtPrintText.TabIndex = 40;
            this.txtPrintText.Text = "1000";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(607, 237);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 33);
            this.button2.TabIndex = 39;
            this.button2.Text = "PrintText";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtTareBykey
            // 
            this.txtTareBykey.Location = new System.Drawing.Point(331, 249);
            this.txtTareBykey.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTareBykey.Name = "txtTareBykey";
            this.txtTareBykey.Size = new System.Drawing.Size(88, 21);
            this.txtTareBykey.TabIndex = 38;
            // 
            // btnTareBYkey
            // 
            this.btnTareBYkey.Location = new System.Drawing.Point(331, 217);
            this.btnTareBYkey.Name = "btnTareBYkey";
            this.btnTareBYkey.Size = new System.Drawing.Size(75, 33);
            this.btnTareBYkey.TabIndex = 37;
            this.btnTareBYkey.Text = "按键去皮";
            this.btnTareBYkey.UseVisualStyleBackColor = true;
            this.btnTareBYkey.Click += new System.EventHandler(this.btnTareBYkey_Click);
            // 
            // txtPreTare
            // 
            this.txtPreTare.Location = new System.Drawing.Point(331, 146);
            this.txtPreTare.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPreTare.Name = "txtPreTare";
            this.txtPreTare.Size = new System.Drawing.Size(88, 21);
            this.txtPreTare.TabIndex = 36;
            this.txtPreTare.Text = "20";
            // 
            // btnPreTare
            // 
            this.btnPreTare.Location = new System.Drawing.Point(331, 166);
            this.btnPreTare.Name = "btnPreTare";
            this.btnPreTare.Size = new System.Drawing.Size(75, 33);
            this.btnPreTare.TabIndex = 35;
            this.btnPreTare.Text = "预置去皮";
            this.btnPreTare.UseVisualStyleBackColor = true;
            this.btnPreTare.Click += new System.EventHandler(this.btnPreTare_Click);
            // 
            // btnZero
            // 
            this.btnZero.Location = new System.Drawing.Point(360, 92);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(75, 33);
            this.btnZero.TabIndex = 34;
            this.btnZero.Text = "清零";
            this.btnZero.UseVisualStyleBackColor = true;
            this.btnZero.Click += new System.EventHandler(this.btnZero_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(225, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 33;
            this.button1.Text = "清空";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 32;
            this.label1.Text = "interval";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(103, 20);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(88, 21);
            this.textBox1.TabIndex = 31;
            this.textBox1.Text = "1000";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(24, 47);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 30;
            this.checkBox1.Text = "读取";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(24, 83);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(276, 436);
            this.listBox1.TabIndex = 29;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(489, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 33);
            this.btnOpen.TabIndex = 44;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(594, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 33);
            this.btnClose.TabIndex = 45;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // txtcom
            // 
            this.txtcom.Location = new System.Drawing.Point(347, 9);
            this.txtcom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtcom.Name = "txtcom";
            this.txtcom.Size = new System.Drawing.Size(59, 21);
            this.txtcom.TabIndex = 46;
            this.txtcom.Text = "6";
            // 
            // txtbaud
            // 
            this.txtbaud.Location = new System.Drawing.Point(414, 9);
            this.txtbaud.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtbaud.Name = "txtbaud";
            this.txtbaud.Size = new System.Drawing.Size(68, 21);
            this.txtbaud.TabIndex = 47;
            this.txtbaud.Text = "9600";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(382, 417);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 33);
            this.button4.TabIndex = 48;
            this.button4.Text = "beginprint";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(607, 400);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 33);
            this.button5.TabIndex = 49;
            this.button5.Text = "切纸";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // FormWinTec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 539);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.txtbaud);
            this.Controls.Add(this.txtcom);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.txtprinttype);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtFontSize);
            this.Controls.Add(this.txtPrintText);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtTareBykey);
            this.Controls.Add(this.btnTareBYkey);
            this.Controls.Add(this.txtPreTare);
            this.Controls.Add(this.btnPreTare);
            this.Controls.Add(this.btnZero);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.listBox1);
            this.Name = "FormWinTec";
            this.Text = "FormWinTec";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtprinttype;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtFontSize;
        private System.Windows.Forms.TextBox txtPrintText;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtTareBykey;
        private System.Windows.Forms.Button btnTareBYkey;
        private System.Windows.Forms.TextBox txtPreTare;
        private System.Windows.Forms.Button btnPreTare;
        private System.Windows.Forms.Button btnZero;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtcom;
        private System.Windows.Forms.TextBox txtbaud;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}