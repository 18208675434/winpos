using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.Model;
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
    public partial class frmCoupon : Form
    {
        /// <summary>
        /// 选中的券码
        /// </summary>
        public string SelectCouponCode = "";

        public string StrValue = "";

        /// <summary>
        /// 当前购物车
        /// </summary>
        private Cart CurrentCart = new Cart();
        public frmCoupon(Cart cart,string selectcoupon)
        {
            InitializeComponent();
            CurrentCart = (Cart)cart.qianClone();
            SelectCouponCode = selectcoupon;
           
        }


        private void frmCoupon_Shown(object sender, EventArgs e)
        {
            UpdateDgvCoupon(CurrentCart, SelectCouponCode);

            dgvCoupon.ClearSelection();
        }
        private void UpdateDgvCoupon(Cart cart, string selectcoupon)
        {
            try
            {
                lblTitle.Text = cart.availablecoupons.Length + "张可用";
                
                if (!string.IsNullOrEmpty(selectcoupon))
                {
                    picNotUse.BackgroundImage = picNotSelect.Image;
                }
                else
                {
                    picNotUse.BackgroundImage = picSelect.Image;
                }

                dgvCoupon.Rows.Clear();
                if (cart != null && cart.availablecoupons != null && cart.availablecoupons.Length > 0)
                {
                    foreach (Availablecoupon couponsBean in cart.availablecoupons)
                    {                      
                        dgvCoupon.Rows.Add(GetItemImg(couponsBean));
                    }


                    dgvCoupon.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR","加载优惠券列表异常"+ex.Message);
            }
        }

        private void frmCoupon_FormClosing(object sender, FormClosingEventArgs e)
        {

        }


        private void dgvCoupon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                   Image selectimg = (Image)dgvCoupon.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if(selectimg==null || selectimg.Tag==null){
                    return;
                }

                Availablecoupon couponsBean = (Availablecoupon)selectimg.Tag;

                //可能选择的有优惠券，目前只支持单张优惠券
                if ((CurrentCart.totalpayment+CurrentCart.couponpromoamt) <= couponsBean.amount)
                {
                    this.Hide();

                    frmCouponOutcs frmcoupon = new frmCouponOutcs(couponsBean);
                    frmcoupon.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    frmcoupon.TopMost = true;
                    frmcoupon.ShowDialog();

                    //收银完成
                    if(frmcoupon.DialogResult==DialogResult.OK)
                    {
                        SelectCouponCode = couponsBean.couponcode;
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                    else if (frmcoupon.DialogResult == DialogResult.Cancel)
                    {
                        this.Close();

                    }else
                    {
                        this.Show();
                    }
                }
                else
                {
                    SelectCouponCode = couponsBean.couponcode;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

                dgvCoupon.ClearSelection();
                                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("选择优惠券异常"+ex.Message+ex.StackTrace);
            }
        }

        private void pnlCouponNone_Click(object sender, EventArgs e)
        {
            SelectCouponCode = "";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private Image GetItemImg(Availablecoupon couponsBean)
        {
            try
            {
                Image img;


                string content = "";

                lblAmount.Text = couponsBean.amount.ToString();

                if ("Cash" == couponsBean.catalog)
                {
                    content = "现金券";
                    lblUnit.Text = "￥";
                    lblUnit.Left = 15;
                    lblAmount.Left = lblUnit.Right;
                }
                else if ("DiscountCoupon" == couponsBean.catalog)
                {
                    if (couponsBean.orderminamount <= 0)
                    {
                        content = "    无门槛使用";
                    }
                    else
                    {
                        content = "满" + couponsBean.orderminamount + "元使用";
                    }

                    lblAmount.Left = 15;
                    lblUnit.Text = "折";
                    lblUnit.Left = lblAmount.Right;
                }
                else if ("CashReduction" == couponsBean.catalog)
                {
                    if (couponsBean.orderminamount <= 0)
                    {
                        content = "无门槛使用";
                    }
                    else
                    {
                        content = "满" + couponsBean.orderminamount + "元使用";
                    }

                    lblUnit.Text = "￥";
                    lblUnit.Left = 15;
                    lblAmount.Left = lblUnit.Right;
                }
                
                lblContent.Text = content;
  
                string starttime = MainModel.GetDateTimeByStamp(couponsBean.enabledfrom.ToString()).ToString("yyyy-MM-dd");
                string endtime = MainModel.GetDateTimeByStamp(couponsBean.enabledto.ToString()).ToString("yyyy-MM-dd");

                lblDate.Text = starttime + "至" + endtime;

                if (SelectCouponCode == couponsBean.couponcode)
                {
                    picItem.BackgroundImage = picSelect.Image;
                }
                else
                {
                    picItem.BackgroundImage = picNotSelect.Image;
                }

                img = MainModel.GetControlImage(pnlItem);
                img.Tag = couponsBean;
                return img;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("解析优惠券信息异常"+ex.Message,true);
                return null;
            }
        }

        private void frmCoupon_Resize(object sender, EventArgs e)
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
