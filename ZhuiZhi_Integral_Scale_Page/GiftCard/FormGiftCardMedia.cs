using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.PayUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard
{
    public partial class FormGiftCardMedia : Form
    {

        #region  成员变量

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        #endregion
        #region 页面加载与退出
        public FormGiftCardMedia()
        {
            InitializeComponent();
        }

        private void FormGiftCard_Shown(object sender, EventArgs e)
        {
          
        }


        public void UpdateCartInfo(CartAloneUpdate cart)
        {
            try
            {
                Invoke((new Action(() =>
                {
                    dgvCart.Rows.Clear();

                    if (cart!=null && cart.products != null && cart.products.Count > 0)
                    {
                        foreach (CardProduct pro in cart.products)
                        {
                            dgvCart.Rows.Add(GetDgvRow(pro, 0));
                        }

                        lblProCount.Text = "(" + cart.products.Count + "件商品)";

                        lblCartTotal.Text = "￥" + cart.pspamt.ToString("f2");

                        lblTotal.Text =cart.pspamt.ToString("f2");
                        lblNeedPay.Text = "￥" + cart.pspamt.ToString("f2"); 
                    }
                    else
                    {
                        lblProCount.Text = "(" + 0 + "件商品)";

                        lblCartTotal.Text = "￥0.00";

                        lblTotal.Text = "0.00";
                        lblNeedPay.Text = "￥0.00";
                    }

                })));
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新礼品卡购买客屏异常"+ex.Message);
            }
        }

        public void LoadMember(Member member)
        {
           
                try
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new InvokeHandler(delegate()
                        {
                            lblMember.Text = "会员信息";
                            pnlMemberInfo.Visible = true;

                            lblMemberPhone.Text = member.memberheaderresponsevo.mobile;
                            lblMemberBalance.Text = "￥" + member.barcoderecognitionresponse.balance.ToString("f2");
                            lblMemberCredit.Text = member.creditaccountrepvo.availablecredit.ToString();
                        }));
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("加载客屏会员异常" + ex.StackTrace);
                }
          
        }

        public void ClearMember()
        {
            try
            {
                lblMember.Text = "请绑定会员";

                pnlMemberInfo.Visible = false;
            }
            catch { }
        }

        private Bitmap GetDgvRow(CardProduct pro, int rownum)
        {
            try
            {
                Bitmap bmpPro;

                lblSkuName.Text = pro.title;
                lblSkuCode.Text = pro.cardnumber;

                lblSaleprice.Text = "￥" + pro.saleprice.ToString("f2");
                lblTotalPrice.Text = "￥" + pro.saleprice.ToString("f2");

            

               if (pro.bindcardsecret == 0)
                {
                    lblBindingInfo.Visible = false;
                    btnBindingPwd.Visible = true;
                }
                else if (pro.bindcardsecret == 1)
                {
                    lblBindingInfo.Visible = true;
                    btnBindingPwd.Visible = false;
                }

                  bmpPro = new Bitmap(pnlCartItem.Width, pnlCartItem.Height);
                bmpPro.Tag = pro;
                pnlCartItem.DrawToBitmap(bmpPro, new Rectangle(0, 0, pnlCartItem.Width, pnlCartItem.Height));

                bmpPro.MakeTransparent(Color.White);

                return bmpPro;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public void ShowPayInfo(bool whethershow)
        {
            pnlPayInfo.Visible = whethershow;
        }
        #endregion

    }
}
