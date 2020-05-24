using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinSaasPOS.Common;

namespace WinSaasPOS.HelperUI
{
    public partial class FormToast : Form
    {
        public FormToast()
        {
            InitializeComponent();
            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public void UpInfo(string msg)
        {
            try
            {
                lblMsg.Text = msg;

                this.Size = new System.Drawing.Size(lblMsg.Width + 40, lblMsg.Size.Height + 20);
                this.Location = new Point((Screen.AllScreens[0].Bounds.Width - this.Width)/2, (Screen.AllScreens[0].Bounds.Height - this.Height) / 2);
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                try
                {
                    ToastHelper.frmtoast = null;
                    this.Dispose();
                }
                catch { }
                
                LogManager.WriteLog("信息框更新异常"+ex.Message);
            }
        }



        private void FormToast_Resize(object sender, EventArgs e)
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
