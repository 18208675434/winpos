using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QDAMAPOS.UserControl
{
    public partial class transparentPic : Control
    {
        #region 内部变量
        private EnumUnitType m_eUnitType;
        private bool m_bCanMove;
        private bool m_bMoving;
        private transparentPic m_uScale;//这个是例子中有大小图时使用，如果当前是大对象，则该变量为大对象在小图中对象的对象；如果当前对象是小对象，则该变量为小对象在大图中对应的对象
        private bool m_bIsFocus;   //判断当前对象时候获取焦点
        //public WzPngProperty info { get; set; }//保存图片信息
        private int startX = 0, startY = 0;
        #endregion

        #region 属性

        /// <summary>
        /// 该对象是否可以移动
        /// </summary>
        public bool CanMove
        {
            get { return m_bCanMove; }
            set { m_bCanMove = value; }
        }

        /// <summary>
        /// 该对象类型
        /// </summary>
        public EnumUnitType UnitType
        {
            get { return m_eUnitType; }
            set { m_eUnitType = value; }
        }
        /// <summary>
        /// 如果当前是大对象，则该变量为大对象在小图中对应的对象，如果当前对象是小对象，则该变量为小对象在大图中对应的对象
        /// </summary>
        public transparentPic unitScale
        {
            get { return m_uScale; }
            set { m_uScale = value; }
        }

        /// <summary>
        /// 判断当前对象是否获取焦点
        /// </summary>
        public bool IsFocus
        {
            get { return m_bIsFocus; }
            set { m_bIsFocus = value; }
        }
        #endregion

        #region 事件
        private void MovePicture_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (m_bCanMove)
                {
                    m_bMoving = true;
                    startX = e.X;
                    startY = e.Y;
                    SetFocus();
                }
            }
            else
            {
                m_bMoving = false;
            }
        }

        private void MovePicture_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
            if (e.Button == MouseButtons.Left)
            {
                if (m_bMoving)
                {
                    //int moveX = e.X - startX;
                    //int moveY = e.Y - startY;
                    //if ((this.Left + moveX) <= this.Parent.Left)
                    //{
                    //    moveX = 0;
                    //    this.Cursor = Cursors.No;
                    //}
                    //Console.WriteLine(this.Parent.Left + "......." + this.Parent.Top);
                    //Console.WriteLine((this.Parent.Left + this.Parent.Width) + "___" + (this.Parent.Top + this.Parent.Height));
                    //if ((this.Right + moveX) >= (this.Parent.Left + this.Parent.Width))
                    //{
                    //    moveX = 0;
                    //    this.Cursor = Cursors.No;
                    //}
                    //if ((this.Top + moveY) <= this.Parent.Top)
                    //{
                    //    moveY = 0;
                    //    this.Cursor = Cursors.No;
                    //}
                    //if ((this.Bottom + moveY) >= (this.Parent.Top + this.Parent.Height))
                    //{
                    //    moveY = 0;
                    //    this.Cursor = Cursors.No;
                    //}
                    int X = this.Left + e.X - this.Width / 2;
                    int Y = this.Top + e.Y - this.Height / 2;
                    if (X < 0)
                    {
                        X = 0;
                        this.Cursor = Cursors.No;
                    }
                    if (Y < 0)
                    {
                        Y = 0;
                        this.Cursor = Cursors.No;
                    }
                    if (this.Parent != null)
                    {
                        //控制不能超出画布的范围
                        if (X > this.Parent.Width - this.Width)
                        {
                            X = this.Parent.Width - this.Width;
                            this.Cursor = Cursors.No;
                        }
                        if (Y > this.Parent.Height - this.Height)
                        {
                            Y = this.Parent.Height - this.Height;
                            this.Cursor = Cursors.No;
                        }
                    }
                    this.Location = new Point(X, Y);// new Point(this.Location.X + moveX, this.Location.Y + moveY);
                    InvalidateEx();
                }
            }
        }

        private void MovePicture_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_bMoving = false;
            }
        }

        private void Parent_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_bCanMove)
            {
                this.Invalidate();
            }
        }

        private void MovePicture_ParentChanged(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.MouseDown += new MouseEventHandler(Parent_MouseDown);
            }
        }
        #endregion

        public transparentPic(Image pic)
        {
            InitializeComponent();
            image = pic;
            this.MouseDown += new MouseEventHandler(MovePicture_MouseDown);
            this.MouseMove += new MouseEventHandler(MovePicture_MouseMove);
            this.MouseUp += new MouseEventHandler(MovePicture_MouseUp);
            this.ParentChanged += new EventHandler(MovePicture_ParentChanged);
        }

        public transparentPic(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        private Image image;

        #region 方法
        private void InvalidateEx()
        {
            if (Parent == null)
                return;
            Rectangle rect = new Rectangle(this.Location, this.Size);
            Parent.Invalidate(rect, true);
        }

        public void SetFocus()
        {
            foreach (Control c in this.Parent.Controls)
            {
                if (c is transparentPic)
                {
                    transparentPic m = c as transparentPic;
                    if (m != this)
                    {
                        m.IsFocus = false;
                        m.Invalidate();
                    }
                }
            }

            //获取焦点
            m_bIsFocus = true;
            //选中时会在外边画一个黑色边框，不过这样影响对齐效果
            //Graphics g = this.CreateGraphics();
            //Pen p = new Pen(Color.Black,1);
            //g.DrawRectangle(p,new Rectangle(0,0,this.Width-1,this.Height-1));
            //p.Dispose();
            //p = null;
            //g.Dispose();
            //g = null;
        }
        #endregion
        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                if (image != null)
                {
                    this.Width = image.Width;
                    this.Height = image.Height;
                }
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020;
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (image != null)
            {
                Rectangle rt = new Rectangle(0, 0, this.Width, this.Height);
                e.Graphics.DrawImage(image, rt);
            }
            //base.OnPaint(e);
        }

    }
}
