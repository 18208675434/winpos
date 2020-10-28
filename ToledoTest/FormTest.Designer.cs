namespace ToledoTest
{
    partial class FormTest
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
            this.button2 = new System.Windows.Forms.Button();
            this.txtPrintText = new System.Windows.Forms.TextBox();
            this.txtFontSize = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.txtprinttype = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtTareBykey
            // 
            this.txtTareBykey.Location = new System.Drawing.Point(320, 243);
            this.txtTareBykey.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTareBykey.Name = "txtTareBykey";
            this.txtTareBykey.Size = new System.Drawing.Size(88, 21);
            this.txtTareBykey.TabIndex = 23;
            // 
            // btnTareBYkey
            // 
            this.btnTareBYkey.Location = new System.Drawing.Point(320, 211);
            this.btnTareBYkey.Name = "btnTareBYkey";
            this.btnTareBYkey.Size = new System.Drawing.Size(75, 33);
            this.btnTareBYkey.TabIndex = 22;
            this.btnTareBYkey.Text = "按键去皮";
            this.btnTareBYkey.UseVisualStyleBackColor = true;
            this.btnTareBYkey.Click += new System.EventHandler(this.btnTareBYkey_Click);
            // 
            // txtPreTare
            // 
            this.txtPreTare.Location = new System.Drawing.Point(320, 140);
            this.txtPreTare.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPreTare.Name = "txtPreTare";
            this.txtPreTare.Size = new System.Drawing.Size(88, 21);
            this.txtPreTare.TabIndex = 21;
            this.txtPreTare.Text = "20";
            // 
            // btnPreTare
            // 
            this.btnPreTare.Location = new System.Drawing.Point(320, 160);
            this.btnPreTare.Name = "btnPreTare";
            this.btnPreTare.Size = new System.Drawing.Size(75, 33);
            this.btnPreTare.TabIndex = 20;
            this.btnPreTare.Text = "预置去皮";
            this.btnPreTare.UseVisualStyleBackColor = true;
            this.btnPreTare.Click += new System.EventHandler(this.btnPreTare_Click);
            // 
            // btnZero
            // 
            this.btnZero.Location = new System.Drawing.Point(349, 86);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(75, 33);
            this.btnZero.TabIndex = 17;
            this.btnZero.Text = "清零";
            this.btnZero.UseVisualStyleBackColor = true;
            this.btnZero.Click += new System.EventHandler(this.btnZero_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(214, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 16;
            this.button1.Text = "清空";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "interval";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(92, 14);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(88, 21);
            this.textBox1.TabIndex = 14;
            this.textBox1.Text = "1000";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 41);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "读取";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(13, 77);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(276, 436);
            this.listBox1.TabIndex = 12;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(596, 231);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 33);
            this.button2.TabIndex = 24;
            this.button2.Text = "PrintText";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtPrintText
            // 
            this.txtPrintText.Location = new System.Drawing.Point(596, 160);
            this.txtPrintText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPrintText.Name = "txtPrintText";
            this.txtPrintText.Size = new System.Drawing.Size(128, 21);
            this.txtPrintText.TabIndex = 25;
            this.txtPrintText.Text = "1000";
            // 
            // txtFontSize
            // 
            this.txtFontSize.Location = new System.Drawing.Point(596, 202);
            this.txtFontSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFontSize.Name = "txtFontSize";
            this.txtFontSize.Size = new System.Drawing.Size(128, 21);
            this.txtFontSize.TabIndex = 26;
            this.txtFontSize.Text = "40";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(596, 341);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 33);
            this.button3.TabIndex = 27;
            this.button3.Text = "beginprint";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtprinttype
            // 
            this.txtprinttype.Location = new System.Drawing.Point(596, 312);
            this.txtprinttype.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtprinttype.Name = "txtprinttype";
            this.txtprinttype.Size = new System.Drawing.Size(128, 21);
            this.txtprinttype.TabIndex = 28;
            this.txtprinttype.Text = "0";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(445, 451);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(113, 62);
            this.button4.TabIndex = 29;
            this.button4.Text = "Form1";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 548);
            this.Controls.Add(this.button4);
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
            this.Name = "FormTest";
            this.Text = "FormTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtPrintText;
        private System.Windows.Forms.TextBox txtFontSize;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtprinttype;
        private System.Windows.Forms.Button button4;
    }
}