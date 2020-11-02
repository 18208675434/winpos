using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WinSaasPOS
{
    public partial class CircleButton : System.Windows.Forms.UserControl
    {
        public CircleButton()
        {
            InitializeComponent();
        }


           /// <summary>
        /// 注册点击事件 窗体和控件都用这个
        /// </summary>
        public event EventHandler ButtonClick
        {
            add { lblText.Click += value; this.Click += value; }
            remove { lblText.Click -= value; this.Click -= value; }
        }

        public string ShowText
        {
            get { return lblText.Text; }
            set
            {
                lblText.Text = value;                
            }
        }

        //public override Font Font {

        //    get { return lblText.Font; }
        //    set
        //    {
        //        lblText.Font = value;
        //    }
        
        //}

        public Font ShowFont
        {
            get { return lblText.Font; }
            set
            {
                lblText.Font = value;
            }
        }

        public Color TextForeColor
        {
            get { return lblText.ForeColor;  }
            set { lblText.ForeColor = value; }
        }

        public Color TextBackColor
        {
            get { return lblText.BackColor; }
            set { lblText.BackColor = value; }
        }

        public int roundradius = 10;
        /// <summary>
        /// 圆角尺寸
        /// </summary>
        public int RoundRadius
        {
            get { return roundradius; }
            set
            {
                roundradius = value;
            }
        }

        public int penwidth = 1;
        public int PenWidth
        {
            get { return penwidth; }
            set { penwidth = value; }
        }

        public Color pencolor = Color.Black;
        public Color PenColor
        {
            get { return pencolor; }
            set { pencolor = value; }
        }

        public Color fillcolor = Color.White;
        public Color FillColor
        {
            get { return fillcolor; }
            set { fillcolor = value; }
        }

        private void lblText_SizeChanged(object sender, EventArgs e)
        {
            lblText.Top = (this.Height - lblText.Height) / 2;

            lblText.Left = (this.Width - lblText.Width) / 2;
        }

        private void RoundButton_SizeChanged(object sender, EventArgs e)
        {
            lblText.Top = (this.Height - lblText.Height) / 2;

            lblText.Left = (this.Width - lblText.Width) / 2;
        }

        private void RoundButton_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Pen p = new Pen(pencolor, penwidth);

                //if (penwidth == 1)
                //{
                //    penwidth = 2;
                //}

                int hslfpenwidth = (penwidth + 1) / 2;
                //抗锯齿
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                //边框工作区
                Rectangle rect = this.ClientRectangle;
                GraphicsPath Rect = new GraphicsPath();
                // 添加圆弧
                Rect.AddArc(hslfpenwidth / 2, hslfpenwidth / 2, roundradius, roundradius, 180, 90);  //左上角
                Rect.AddArc(rect.Width - roundradius - hslfpenwidth / 2-1, hslfpenwidth / 2, roundradius, roundradius, 270, 90); //右上角
                Rect.AddArc(rect.Width - roundradius - hslfpenwidth / 2-1, rect.Height - roundradius - hslfpenwidth / 2-1, roundradius, roundradius, 0, 90); //右下角
                Rect.AddArc(hslfpenwidth / 2, rect.Height - roundradius - hslfpenwidth / 2-1, roundradius, roundradius, 90, 90);  //左下角
                Rect.CloseFigure();
                e.Graphics.DrawPath(p, Rect);

                LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush(e.ClipRectangle, fillcolor, fillcolor, LinearGradientMode.Vertical);
                e.Graphics.FillPath(myLinearGradientBrush, DrawRoundRect(e.ClipRectangle.X + hslfpenwidth, e.ClipRectangle.Y + hslfpenwidth, e.ClipRectangle.Width - hslfpenwidth, e.ClipRectangle.Height - hslfpenwidth, roundradius));
            }
            catch (Exception ex)
            {

            }
            }


        public static GraphicsPath DrawRoundRect(int x, int y, int width, int height, int radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(x, y, radius, radius, 180, 90);
            gp.AddArc(width - radius, y, radius, radius, 270, 90);
            gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            gp.AddArc(x, height - radius, radius, radius, 90, 90);
            gp.CloseAllFigures();

            return gp;
        }


    
    }
}
