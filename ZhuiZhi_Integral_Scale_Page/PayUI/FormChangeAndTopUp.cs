using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.PayUI
{
    public partial class FormChangeAndTopUp : Form
    {
        private Cart CurrentCart;
        private decimal CurrentTotalChange;
        private decimal CurrentBalance;
        private HttpUtil httputil = new HttpUtil();
        public string SuccessOrderid = "";
        public FormChangeAndTopUp( Cart cart)
        {
            InitializeComponent();
            CurrentCart=cart;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void FormChangeAndTopUp_Load(object sender, EventArgs e)
        {
            CurrentTotalChange = CurrentCart.cashchangeamt;

            lblTotalChange.Text ="￥"+ CurrentTotalChange.ToString("f2");
        }


        private void btnAll_Click(object sender, EventArgs e)
        {
            try
            {
                numTxtBalance.Text = CurrentTotalChange.ToString();
            }
            catch
            {

            }
        }

        private void btnDecimalPoint_Click(object sender, EventArgs e)
        {
            numTxtBalance.Text =( CurrentTotalChange - Math.Floor(CurrentTotalChange)).ToString();
        }

        private void btnSaveBalance_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(numTxtBalance.Text))
                {
                    MainModel.ShowLog("存入余额不能为空",false);
                    return;
                }

                decimal balance = Convert.ToDecimal(numTxtBalance.Text);

                if (balance == 0)
                {
                    MainModel.ShowLog("存入余额必须大于0", false);
                    return;
                }

                if (balance > CurrentTotalChange)
                {
                    MainModel.ShowLog("存入余额不能大于总找零金额",false);
                    return;
                }


                if (CreateOrder(balance))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch
            {

            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {

        }

        private void numTxtBalance_DataChanged(string data)
        {
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    decimal tempnum = Convert.ToDecimal(data);

                    if (tempnum > CurrentTotalChange)
                    {
                        MainModel.ShowLog("转存金额不能大于总找零金额", false);
                        return;
                    }
                    CurrentBalance = tempnum;

                    lblCurrentChange.Text = (CurrentTotalChange - tempnum).ToString("f2");
                    lblYuan.Left = lblCurrentChange.Right;
                }
                else
                {
                    CurrentBalance = 0;
                }
            }
            catch { }

        }



        private bool CreateOrder(decimal balance)
        {
            try
            {
                string ErrorMsg = "";
                int ResultCode = 0;

                CurrentCart.balancedepositamt = balance;
                CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg, ref ResultCode);
                if (orderresult ==null || orderresult.continuepay==1)
                {
                    MainModel.ShowLog("订单支付失败" + ErrorMsg, true);
                    return false;
                }               
                else
                {
                    SuccessOrderid = orderresult.orderid;
                    if (balance > 0)
                    {
                        if (MemberTopUp(balance))
                        {
                            return true;
                        }
                        else
                        {
                            string errormsg = "";
                            bool result = httputil.CancleOrder(orderresult.orderid, "取消支付", ref errormsg);

                            if (result)
                            {
                                LogManager.WriteLog("取消订单" + orderresult.orderid);
                            }
                            else
                            {
                                LogManager.WriteLog("订单取消失败" + errormsg);
                                // return;
                            }
                        }
                    }

                   

                    return true;
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("创建订单异常"+ex.Message,false);
                return false;
            }
        }


        private bool MemberTopUp(decimal balance)
        {

            try
            {

                MemberTopUpPara para = new MemberTopUpPara();
                para.amount = balance;
                para.memberid = Convert.ToInt64(MainModel.CurrentMember.memberinformationresponsevo.memberid);
                para.paymode = "0";
                para.phone = MainModel.CurrentMember.memberheaderresponsevo.mobile;
                para.shopid = MainModel.CurrentShopInfo.shopid;


                string errormsg = "";
                long result = httputil.MemberTopUp(para, ref errormsg);

                if (!string.IsNullOrEmpty(errormsg))
                {
                    MainModel.ShowLog(errormsg, false);
                    return false;
                }
                else
                {
                    return true;
                    //PrintUtil.PrintTopUp(result.ToString());
                    //TopUpOK();
                }
            }
            catch
            {
                return false;
            }
        }
    
    }
}
