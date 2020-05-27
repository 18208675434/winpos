using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinSaasPOS_Scale.Common
{

    public class AutoSizeFormUtil
    {
        /// <summary>
        /// 声明结构 记录控件位置和大小
        /// </summary>
        public struct ControlRect
        {
            public int Left;
            public int Top;
            public int Width;
            public int Height;
            public float Size;
        }

        public List<ControlRect> _oldCtrl = new List<ControlRect>();
        private int _ctrlNo = 0;

        private void AddControl(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                ControlRect cR;
                cR.Left = c.Left;
                cR.Top = c.Top;
                cR.Width = c.Width;
                cR.Height = c.Height;
                cR.Size = c.Font.Size;
                _oldCtrl.Add(cR);

                // 控件可能嵌套子控件
                if (c.Controls.Count > 0)
                    AddControl(c);
            }
        }

        public void ControlAutoSize(Control mForm)
        {
            if (_ctrlNo == 0)
            {
                ControlRect cR;
                cR.Left = mForm.Left;
                cR.Top = mForm.Top;
                cR.Width = mForm.Width;
                cR.Height = mForm.Height;
                cR.Size = mForm.Font.Size;

                _oldCtrl.Add(cR);

                AddControl(mForm);
            }

            _ctrlNo = 1;
            float wScale = (float)mForm.Width / _oldCtrl[0].Width;
            float hScale = (float)mForm.Height / _oldCtrl[0].Height;
            AutoScaleControl(mForm, wScale, hScale);
        }

        private void AutoScaleControl(Control mForm, float wScale, float hScale)
        {
            int ctrlLeft, ctrlTop, ctrlWidth, ctrlHeight;
            float ctrlFontSize, hSize, wSize;

            string debug = "";
            string tet = debug;
            string name = mForm.Name;

            foreach (Control c in mForm.Controls)
            {
                ctrlLeft = _oldCtrl[_ctrlNo].Left;
                ctrlTop = _oldCtrl[_ctrlNo].Top;
                ctrlWidth = _oldCtrl[_ctrlNo].Width;
                ctrlHeight = _oldCtrl[_ctrlNo].Height;
                ctrlFontSize = _oldCtrl[_ctrlNo].Size;

                c.Left = (int)Math.Ceiling(ctrlLeft * wScale);
                c.Top = (int)Math.Ceiling(ctrlTop * hScale);
                c.Width = (int)Math.Ceiling(ctrlWidth * wScale);
                c.Height = (int)Math.Ceiling(ctrlHeight * hScale);

                wSize = ctrlFontSize * wScale;
                hSize = ctrlFontSize * hScale;
                c.Font = new Font(c.Font.Name, Math.Min(hSize, wSize), c.Font.Style, c.Font.Unit);

                if (c.Name.Length > 0 && c.Name.Substring(0, 3) == "dgv")
                {
                    try
                    {
                        DataGridView dgv = (DataGridView)c;


                        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
                        dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", Math.Min(dgv.RowsDefaultCellStyle.Font.Size * wScale, dgv.RowsDefaultCellStyle.Font.Size * hScale));
                        dgv.RowsDefaultCellStyle = dataGridViewCellStyle1;

                        //foreach (DataGridViewColumn dr in dgv.Columns)
                        //{


                        //    if (dr.AutoSizeMode == System.Windows.Forms.DataGridViewAutoSizeColumnMode.None)
                        //        dr.Width = (int)Math.Ceiling(dr.Width * wScale);
                        //    // dr.DefaultCellStyle.Font = new Font(dr.DefaultCellStyle.Font.Name, Math.Min(hSize, wSize), dr.DefaultCellStyle.Font.Style, dr.DefaultCellStyle.Font.Unit);

                        //}
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("更改控件大小异常" + ex.Message);
                    }
                }

                _ctrlNo++;

                // 先缩放控件本身 再缩放子控件
                if (c.Controls.Count > 0)
                {
                    if (c.Name.Length > 0 && c.Name.Substring(0, 3) == "dgv")
                    {


                    }
                    else
                    {

                    }
                    AutoScaleControl(c, wScale, hScale);
                }
            }
        }

        float wScale = 1;
        float hScale = 1;
        public void AutoScaleControlTest(Control mForm, float OldWidth, float OldHeight, float NewWidth, float NewHeight, bool isParent)
        {
            try
            {

                Type type = mForm.GetType();
                string test = type.Name;
                if (isParent)
                {
                    wScale = (float)NewWidth / OldWidth;
                    hScale = (float)NewHeight / OldHeight;
                    mForm.Size = new System.Drawing.Size((int)Math.Ceiling(OldWidth * wScale), (int)Math.Ceiling(OldHeight * hScale));
                }

                int ctrlLeft, ctrlTop, ctrlWidth, ctrlHeight;
                float ctrlFontSize, hSize, wSize;
                foreach (Control c in mForm.Controls)
                {
                    string name = c.Name;
                    string testname = name;


                    if (c.Name.Length > 0 && c.Name == "toolStripMain")
                    {
                        string wre = "test";
                        string er = wre;
                        // c.Paint += control_Paint;

                    }

                    if (c.Name != "KBoard")
                    {
                        c.Left = (int)Math.Ceiling(c.Left * wScale);
                        c.Top = (int)Math.Ceiling(c.Top * hScale);

                        c.Width = (int)Math.Ceiling(c.Width * wScale);
                        c.Height = (int)Math.Ceiling(c.Height * hScale);

                      
                    }

                    wSize = c.Font.Size * wScale;
                    hSize = c.Font.Size * hScale;
                   
                    if (c.Name.Length > 0 && c.Name.Substring(0, 3) == "dgv")
                    {
                    }
                    else if (c.Controls.Count > 0)
                    {

                    }
                    else
                    {
                        c.Font = new Font(c.Font.Name, Math.Min(hSize, wSize), c.Font.Style, c.Font.Unit);

                    }
                    

                    if (c.Name.Length > 0 && c.Name.Substring(0, 3) == "dgv")
                    {
                        try
                        {
                            DataGridView dgv = (DataGridView)c;

                            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();


                            dgv.RowTemplate.Height = Convert.ToInt16(dgv.RowTemplate.Height * hScale);


                            if (dgv.RowsDefaultCellStyle.Font != null)
                            {

                                dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", Math.Min(dgv.RowsDefaultCellStyle.Font.Size * wScale, dgv.RowsDefaultCellStyle.Font.Size * hScale));
                                dgv.RowsDefaultCellStyle = dataGridViewCellStyle1;
                            }
                            if (dgv.ColumnHeadersDefaultCellStyle.Font != null)
                            {
                                dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                                dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", Math.Min(dgv.ColumnHeadersDefaultCellStyle.Font.Size * wScale, dgv.ColumnHeadersDefaultCellStyle.Font.Size * hScale));

                                dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
                            }
                            foreach (DataGridViewColumn dr in dgv.Columns)
                            {
                                if (dr.AutoSizeMode == System.Windows.Forms.DataGridViewAutoSizeColumnMode.None)
                                    dr.Width = (int)Math.Ceiling(dr.Width * wScale);

                                if (dr.DefaultCellStyle.Font != null)
                                {
                                    dr.DefaultCellStyle.Font = new Font(dr.DefaultCellStyle.Font.Name, Math.Min(dr.DefaultCellStyle.Font.Size * wScale, dr.DefaultCellStyle.Font.Size * hScale), dr.DefaultCellStyle.Font.Style, dr.DefaultCellStyle.Font.Unit);
                                    dr.DefaultCellStyle.Alignment = dr.DefaultCellStyle.Alignment;
                                }

                            }

                            dgv.AllowUserToResizeColumns = false;
                            dgv.AllowUserToResizeRows = false;
                        }
                        catch (Exception ex)
                        {
                            LogManager.WriteLog("更改控件大小异常" + ex.Message);
                        }
                    }


                    // 先缩放控件本身 再缩放子控件
                    if (c.Controls.Count > 0 && c.Name!="KBoard")
                    {
                        AutoScaleControlTest(c, 0, 0, 0, 0, false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("缩放页面比例异常：" + ex.Message);
            }
        }

        private void control_Paint(object sender, PaintEventArgs e)
        {
            Control con = (Control)sender;
            Color co = con.BackColor;
            if (con.Tag == null)
            {
                con.Tag = co;
            }

            con.BackColor = Color.Transparent;
            Draw(e.ClipRectangle, e.Graphics, Math.Min(Math.Min(con.Width / 5, con.Height / 5), 18), false, (Color)con.Tag, (Color)con.Tag);
            //base.OnPaint(e);
            Graphics g = e.Graphics;

            Font fnt2 = con.Font;
            SizeF size2 = con.CreateGraphics().MeasureString(con.Text, fnt2);

            g.DrawString(con.Text, con.Font, new SolidBrush(Color.White), new PointF((con.Width - size2.Width) / 2, (con.Height - size2.Height) / 2));

            // con.Paint -= control_Paint;
        }


        private void Draw(Rectangle rectangle, Graphics g, int _radius, bool cusp, Color begin_color, Color end_color)
        {
            int span = 2;
            //抗锯齿
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //渐变填充
            LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush(rectangle, begin_color, end_color, LinearGradientMode.Vertical);
            //画尖角
            if (cusp)
            {
                span = 10;
                PointF p1 = new PointF(rectangle.Width - 12, rectangle.Y + 10);
                PointF p2 = new PointF(rectangle.Width - 12, rectangle.Y + 30);
                PointF p3 = new PointF(rectangle.Width, rectangle.Y + 20);
                PointF[] ptsArray = { p1, p2, p3 };
                g.FillPolygon(myLinearGradientBrush, ptsArray);
            }
            //填充
            g.FillPath(myLinearGradientBrush, DrawRoundRect(rectangle.X, rectangle.Y, rectangle.Width - span, rectangle.Height - 1, _radius));
        }

        public static GraphicsPath DrawRoundRect(int x, int y, int width, int height, int radius)
        {
            //四边圆角
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
