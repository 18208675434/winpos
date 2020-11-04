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
    public partial class RoundButton : System.Windows.Forms.UserControl
    {
        public RoundButton()
        {
            InitializeComponent();

           // lblText.Click += Click += new MouseEventHandler(this.Click);
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

        public override Font Font {

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

        public Color allbackcolor;
        public Color AllBackColor
        {
            get { return allbackcolor; }
            set { lblText.BackColor = value; this.BackColor = value; allbackcolor = value; }
        }

        bool whetherenable = true;
        public bool WhetherEnable
        {
            get { return whetherenable; }
            set { whetherenable = value;
            if (value)
            {
                lblText.BackColor = allbackcolor;
                this.BackColor = allbackcolor;
            }
            else
            {
                lblText.BackColor = Color.Silver;
                this.BackColor = Color.Silver;
            }
            }
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

        public bool ShowImg
        {
            get { return picture.Visible; }
            set { picture.Visible = value; }
        }

        public Image Image
        {
            get { return picture.BackgroundImage; }
            set { picture.BackgroundImage = value; }
        }

        public Size ImageSize
        {
            get { return picture.Size; }
            set { picture.Size = value; }
        }

        private void lblText_SizeChanged(object sender, EventArgs e)
        {

           
           
                picture.Top = (this.Height - picture.Height) / 2;
                lblText.Top = (this.Height - lblText.Height) / 2;

                picture.Left = (this.Width - lblText.Width - picture.Width) / 2;

                lblText.Left = picture.Left + picture.Width;
           
        }

        private void RoundButton_SizeChanged(object sender, EventArgs e)
        {
           
                picture.Top = (this.Height - picture.Height) / 2;
                lblText.Top = (this.Height - lblText.Height) / 2;

                picture.Left = (this.Width - lblText.Width - picture.Width) / 2;

                lblText.Left = picture.Left + picture.Width;            
        }    



        private void RoundButton_Resize(object sender, EventArgs e)
        {
            SetWindowRegion();
        }

        /// <summary>
        /// 设置窗体的Region   画半径为10的圆角
        /// </summary>
        public void SetWindowRegion()
        {
            try
            {
                GraphicsPath FormPath;
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                FormPath = GetRoundedRectPath(rect, roundradius);
                this.Region = new Region(FormPath);
            }
            catch (Exception ex)
            {

            }

        }
        /// <summary>
        /// 绘制圆角路径
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            try
            {
                int diameter = radius;
                Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
                GraphicsPath path = new GraphicsPath();

                // 左上角
                path.AddArc(arcRect, 180, 90);

                // 右上角
                arcRect.X = rect.Right - diameter;
                path.AddArc(arcRect, 270, 90);

                // 右下角
                arcRect.Y = rect.Bottom - diameter;
                path.AddArc(arcRect, 0, 90);

                // 左下角
                arcRect.X = rect.Left;
                path.AddArc(arcRect, 90, 90);
                path.CloseFigure();//闭合曲线
                return path;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

     
      


    }
}
