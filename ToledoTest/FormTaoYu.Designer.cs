namespace ToledoTest
{
    partial class FormTaoYu
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
            this.btnClear = new System.Windows.Forms.Button();
            this.listResult = new System.Windows.Forms.ListBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtCom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnGetWeight = new System.Windows.Forms.Button();
            this.btnTare = new System.Windows.Forms.Button();
            this.btnZero = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(24, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 33);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // listResult
            // 
            this.listResult.FormattingEnabled = true;
            this.listResult.ItemHeight = 21;
            this.listResult.Location = new System.Drawing.Point(24, 53);
            this.listResult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listResult.Name = "listResult";
            this.listResult.Size = new System.Drawing.Size(297, 508);
            this.listResult.TabIndex = 17;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(647, 89);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 33);
            this.btnOpen.TabIndex = 19;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtCom
            // 
            this.txtCom.Location = new System.Drawing.Point(477, 92);
            this.txtCom.Name = "txtCom";
            this.txtCom.Size = new System.Drawing.Size(145, 29);
            this.txtCom.TabIndex = 20;
            this.txtCom.Text = "COM2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(398, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 21);
            this.label1.TabIndex = 21;
            this.label1.Text = "COMNo";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(771, 89);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 33);
            this.btnClose.TabIndex = 22;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnGetWeight
            // 
            this.btnGetWeight.Location = new System.Drawing.Point(477, 179);
            this.btnGetWeight.Name = "btnGetWeight";
            this.btnGetWeight.Size = new System.Drawing.Size(151, 58);
            this.btnGetWeight.TabIndex = 23;
            this.btnGetWeight.Text = "读取重量";
            this.btnGetWeight.UseVisualStyleBackColor = true;
            this.btnGetWeight.Click += new System.EventHandler(this.btnGetWeight_Click);
            // 
            // btnTare
            // 
            this.btnTare.Location = new System.Drawing.Point(477, 270);
            this.btnTare.Name = "btnTare";
            this.btnTare.Size = new System.Drawing.Size(151, 58);
            this.btnTare.TabIndex = 24;
            this.btnTare.Text = "去皮";
            this.btnTare.UseVisualStyleBackColor = true;
            this.btnTare.Click += new System.EventHandler(this.btnTare_Click);
            // 
            // btnZero
            // 
            this.btnZero.Location = new System.Drawing.Point(477, 352);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(151, 58);
            this.btnZero.TabIndex = 25;
            this.btnZero.Text = "归零";
            this.btnZero.UseVisualStyleBackColor = true;
            this.btnZero.Click += new System.EventHandler(this.btnZero_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(477, 437);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 58);
            this.button1.TabIndex = 26;
            this.button1.Text = "设置单位";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormTaoYu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 594);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnZero);
            this.Controls.Add(this.btnTare);
            this.Controls.Add(this.btnGetWeight);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCom);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.listResult);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormTaoYu";
            this.Text = "FormTaoYu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ListBox listResult;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtCom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnGetWeight;
        private System.Windows.Forms.Button btnTare;
        private System.Windows.Forms.Button btnZero;
        private System.Windows.Forms.Button button1;
    }
}