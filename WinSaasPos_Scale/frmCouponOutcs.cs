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
using WinSaasPOS_Scale.Model;

namespace WinSaasPOS_Scale
{
    public partial class frmCouponOutcs : Form
    {

        private Availablecoupon currentcoupon = new Availablecoupon();
        public frmCouponOutcs()
        {
            InitializeComponent();
        }

        public frmCouponOutcs(Availablecoupon coupon)
        {
            InitializeComponent();
            currentcoupon = coupon;
            laodCoupon(coupon);

        }

        private void laodCoupon(Availablecoupon couponsBean)
        {
            try
            {
                string content = "";

                if ("Cash" == couponsBean.catalog)
                {
                    content = "      现金券";
                }
                else if ("DiscountCoupon" == couponsBean.catalog)
                {
                    if (couponsBean.orderminamount <= 0)
                    {
                        content = "    无门槛使用";
                    }
                    else
                    {
                        content = "    满" + couponsBean.orderminamount + "元使用";
                    }
                }
                else if ("CashReduction" == couponsBean.catalog)
                {
                    if (couponsBean.orderminamount <= 0)
                    {
                        content = "    无门槛使用";
                    }
                    else
                    {
                        content = "    满" + couponsBean.orderminamount + "元使用";
                    }
                }

                string starttime = MainModel.GetDateTimeByStamp(couponsBean.enabledfrom.ToString()).ToString("yyyy-MM-dd");
                string endtime = MainModel.GetDateTimeByStamp(couponsBean.enabledto.ToString()).ToString("yyyy-MM-dd");
                string couponcode = couponsBean.couponcode;
                string select = "";

                lblAmount.Text = couponsBean.amount.ToString("f2");
                lblContent.Text = content;
                lblTime.Text = starttime + "至" + endtime;


            }
            catch (Exception ex)
            {
                MainModel.ShowLog("显示优惠券详情异常" + ex.Message, true);
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //TODO 完成收银
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
