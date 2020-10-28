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

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class FormPayCashToChange : Form
    {
        /// <summary>
        /// 现金收银界面传过来的 抹零后的cartModel
        /// </summary>
        private Cart CurrentCart;


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public FormPayCashToChange()
        {
            InitializeComponent();
        }

        public FormPayCashToChange(decimal total,decimal realy)
        {
            InitializeComponent();

            try
            {
                lblPrice.Text = "￥" + total.ToString("f2");
                lblCash.Text = "￥" + realy.ToString("f2");
                lblChange.Text = "￥" + (realy-total).ToString("f2");
            }
            catch { }
        }

        public void UpInfo(Cart cart)
        {
            try
            {
                CurrentCart = cart;
                lblPrice.Text = "￥" + cart.payamtbeforecash.ToString("f2");
                lblCash.Text = "￥" + cart.cashpayamt.ToString("f2");
                lblChange.Text = "￥" + cart.cashchangeamt.ToString("f2");

                Dictionary<string, string> dicdetail = new Dictionary<string, string>();
                dicdetail.Add("应付：", "￥" + cart.payamtbeforecash.ToString("f2"));
                dicdetail.Add("已付现金：",  "￥" + cart.cashpayamt.ToString("f2"));
                ZhuiZhi_Integral_Scale_UncleFruit.BaseUI.BaseUIHelper.UpdateDgvOrderDetail(dicdetail, "找零：", "￥" + cart.cashchangeamt.ToString("f2"));
            }
            catch (Exception ex)
            {
            }
        }

        //返回上层
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        //现金支付完成
        private void btnPayOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                if (keyData == Keys.Enter)
                {
                    btnPayOK_Click(null,null);
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }


        }
    }
}
