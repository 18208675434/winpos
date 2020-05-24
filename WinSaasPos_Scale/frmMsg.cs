using WinSaasPOS_Scale.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WinSaasPOS_Scale
{
    public partial class frmMsg : Form
    {


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        
        public frmMsg()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;      
        }

          public frmMsg(string Msg)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            try
            {
                lblMsg.Text = Msg;

                timerClose.Interval = 3000;
                timerClose.Enabled = true;

                this.Size = new System.Drawing.Size(lblMsg.Width + 40, lblMsg.Size.Height + 10);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化消息页面异常"+ex.Message);
            }
         
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            try
            {
                timerClose.Enabled = false;
                this.Dispose();
                //this.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    this.Dispose();
                   // this.Close();
                }
                catch { }
                LogManager.WriteLog("页面关闭异常"+ex.Message);
                this.Close();
            }
        }

        private void frmMsg_SizeChanged(object sender, EventArgs e)
        {

            //<summary>
            //按比例缩放页面及控件
            //</summary>
           // AutoSizeFormUtil asf = new AutoSizeFormUtil();
        }

        
        /// <summary>

        /// 关闭命令

        /// </summary>

        public void closeOrder()
        {

            if (this.InvokeRequired)
            {

                //这里利用委托进行窗体的操作，避免跨线程调用时抛异常，后面给出具体定义

                CONSTANTDEFINE.SetUISomeInfo UIinfo = new CONSTANTDEFINE.SetUISomeInfo(new Action(() =>
                {

                    while (!this.IsHandleCreated)
                    {

                        ;

                    }

                    if (this.IsDisposed)

                        return;

                    if (!this.IsDisposed)
                    {

                        this.Dispose();

                    }



                }));

                this.Invoke(UIinfo);

            }

            else
            {

                if (this.IsDisposed)

                    return;

                if (!this.IsDisposed)
                {

                    this.Dispose();

                }

            }

        }



        private void LoaderForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!this.IsDisposed)
            {

                this.Dispose(true);

            }
        }

        private void frmMsg_Resize(object sender, EventArgs e)
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
                FormPath = GetRoundedRectPath(rect, 10);
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
