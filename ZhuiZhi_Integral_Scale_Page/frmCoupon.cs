using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
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

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class frmCoupon : Form
    {
        /// <summary>
        /// 选中的券码
        /// </summary>
        public string SelectCouponCode = "";

        /// <summary>
        /// 选中券的促销码
        /// </summary>
        public OrderCouponVo SelectPromotionCode = null;

        public string StrValue = "";

        /// <summary>
        /// 当前购物车
        /// </summary>
        private Cart CurrentCart = new Cart();

        private List<OrderCouponVo> CurrentCoupons = new List<OrderCouponVo>();
        public frmCoupon(Cart cart,string selectcoupon)
        {
            InitializeComponent();
            CurrentCart = (Cart)cart.qianClone();
            SelectCouponCode = selectcoupon;
            SelectPromotionCode = MainModel.Currentavailabecoupno;
        }


        private void frmCoupon_Shown(object sender, EventArgs e)
        {
            try
            {
                CurrentCoupons = GetOrderCoupons();

                if (CurrentCoupons != null && CurrentCoupons.Count > 0)
                {
                    lblTitle.Text ="共"+ CurrentCoupons.Count + "张,"+GetAvailableCount()+"张可用";
                    CurrentPage = 1;
                    LoadDgvCoupon(true);
                }
               
               
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载优惠券页面异常"+ex.Message,true);
            }
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

                OrderCouponVo couponsBean = (OrderCouponVo)selectimg.Tag;
                //不可用优惠券不能选择
                if (couponsBean == null || !couponsBean.enabled)
                {
                    return;
                }

                //可能选择的有优惠券，目前只支持单张优惠券  不能计算打折券
                if (("Cash" == couponsBean.catalog || "CashReduction" == couponsBean.catalog) && (Math.Max (CurrentCart.totalpayment,CurrentCart.totalpaymentbeforefix )+ CurrentCart.couponpromoamt) <= couponsBean.amount)
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
                        SelectPromotionCode = couponsBean;
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
                    SelectPromotionCode = couponsBean;
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
            SelectPromotionCode = null;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private Image GetItemImg(OrderCouponVo couponsBean)
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
                        content = "无门槛使用";
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
                else if ("ExchangeCoupon".Equals(couponsBean.catalog))
                {
                    if (couponsBean.exchangeconditioncontext != null)
                    {
                        content = couponsBean.availableskudesc;
                        if (couponsBean.exchangeconditioncontext.exchangetype == 1)
                        {
                            lblUnit.Text = "";
                            lblAmount.Text = "兑换券";
                            lblAmount.Left = 10;
                        }
                        else if (couponsBean.exchangeconditioncontext.exchangetype == 2)
                        {
                            lblUnit.Text = "";
                            lblAmount.Text = couponsBean.exchangeconditioncontext.exchangeamount + "元兑";
                            lblAmount.Left = 10;
                        }
                    }
                }
                lblUnit.Top = lblAmount.Top + lblAmount.Height - lblUnit.Height;
                
                
                lblContent.Text = content;
  
                string starttime = MainModel.GetDateTimeByStamp(couponsBean.enabledfrom.ToString()).ToString("yyyy-MM-dd");
                string endtime = MainModel.GetDateTimeByStamp(couponsBean.enabledto.ToString()).ToString("yyyy-MM-dd");

                lblDate.Text = starttime + "至" + endtime;

                if (couponsBean.enabled)
                {
                    picItem.Visible = true;
                    pnlItem.Enabled = true;

                }
                else
                {
                    picItem.Visible = false;
                    pnlItem.Enabled = false;

                }
                picItem.Visible = couponsBean.enabled;
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


        #region 分页
        private int CurrentPage = 1;

        private void rbtnPageUp_ButtonClick(object sender, EventArgs e)
        {
            if (!rbtnPageUp.WhetherEnable)
            {
                return;
            }
            CurrentPage--;
            LoadDgvCoupon(false);
        }

        private void rbtnPageDown_ButtonClick(object sender, EventArgs e)
        {
            if (!rbtnPageDown.WhetherEnable)
            {
                return;
            }
            CurrentPage++;
            LoadDgvCoupon(false);
        }


        /// <summary>
        /// 分页加载电子秤列表
        /// </summary>
        /// <param name="needRefresh">是否需要刷新</param>
        private void LoadDgvCoupon(bool needRefresh)
        {
            try
            {
                dgvCoupon.Rows.Clear();
                if (CurrentCoupons == null || CurrentCoupons.Count == 0)
                {
                    return;
                }
                if (CurrentPage > 1)
                {
                    rbtnPageUp.WhetherEnable = true;
                }
                else
                {
                    rbtnPageUp.WhetherEnable = false;
                }
                int startindex = (CurrentPage - 1) * 4;

                int lastindex = Math.Min(CurrentCoupons.Count - 1, startindex + 3);

                List<OrderCouponVo> LstTempcoupon = CurrentCoupons.GetRange(startindex, lastindex - startindex + 1);

                foreach (OrderCouponVo couponsBean in LstTempcoupon)
                {
                    dgvCoupon.Rows.Add(GetItemImg(couponsBean));
                }
                dgvCoupon.ClearSelection();
                Application.DoEvents();

                if (CurrentCoupons.Count > CurrentPage * 4)
                {
                    rbtnPageDown.WhetherEnable = true;
                }
                else
                {
                    rbtnPageDown.WhetherEnable = false;
                }

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载订单列表异常" + ex.Message, true);
            }
        }

        #endregion

        /// <summary>
        /// 获取购物车所有优惠券
        /// </summary>
        /// <returns></returns>
        private List<OrderCouponVo> GetOrderCoupons()
        {
            try
            {
                List<OrderCouponVo> coupongs = new List<OrderCouponVo>();

                if (CurrentCart == null)
                {
                    return null;
                }

                if (CurrentCart.availablecoupons != null && CurrentCart.availablecoupons.Count > 0)
                {
                    CurrentCart.availablecoupons.ForEach(r => r.enabled = true);
                    coupongs.AddRange(CurrentCart.availablecoupons);
                }

                if (CurrentCart.unavailablecoupons != null && CurrentCart.unavailablecoupons.Count > 0)
                {
                    CurrentCart.unavailablecoupons.ForEach(r => r.enabled = false);
                    coupongs.AddRange(CurrentCart.unavailablecoupons);
                }

                return coupongs;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("解析优惠券信息异常"+ex.Message,true);
                return null;
            }
        }


        private int GetAvailableCount()
        {
            if (CurrentCart.availablecoupons != null && CurrentCart.availablecoupons.Count > 0)
            {
                return CurrentCart.availablecoupons.Count;
            }
            else
            {
                return 0;
            }
        }
    }
}
