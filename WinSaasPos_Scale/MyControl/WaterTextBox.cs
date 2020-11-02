using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinSaasPOS_Scale.UserControl
{
    public partial class WaterTextBox : TextBox
    {
        private readonly Label lblwaterText = new Label();

        public WaterTextBox()
        {
            InitializeComponent();

            lblwaterText.BorderStyle = BorderStyle.None;
            lblwaterText.Enabled = false;
            lblwaterText.BackColor = this.BackColor;
            lblwaterText.AutoSize = false;
            lblwaterText.Top = 4;
            lblwaterText.Left = 2;
            lblwaterText.FlatStyle = FlatStyle.System;


            lblwaterText.TextAlign = ContentAlignment.MiddleLeft;


            Controls.Add(lblwaterText);
        }


        [Category("扩展属性"), Description("显示的提示信息")]
        public string WaterText
        {
            get { return lblwaterText.Text; }
            set { lblwaterText.Text = value;
            lblwaterText.BackColor = this.BackColor;
            }
        }

        public override string Text
        {
            set
            {
                lblwaterText.Visible = value == string.Empty;
                base.Text = value;
            }
            get
            {
                return base.Text;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (Multiline && (ScrollBars == ScrollBars.Vertical || ScrollBars == ScrollBars.Both))
                lblwaterText.Width = Width - 20;
            else
                lblwaterText.Width = Width;
            lblwaterText.Height = Height - 2;
            base.OnSizeChanged(e);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            Font font = this.Font;
            lblwaterText.Font = new Font(font.Name, font.Size *  60/ 100);

            base.OnFontChanged(e);
        }
        protected override void OnTextChanged(EventArgs e)
        {
            lblwaterText.Visible = base.Text == string.Empty;
            base.OnTextChanged(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            lblwaterText.Visible = false;
            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            lblwaterText.Visible = base.Text == string.Empty;
            base.OnMouseLeave(e);
        }

    }
}
