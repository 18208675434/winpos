using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormCustomreCharge : Form
    {

        public ListAllTemplate CustomTemplate;
        decimal rewardamount = 0;
        public FormCustomreCharge()
        {
            InitializeComponent();
        }

        private void btnNum_Click(object sender, EventArgs e)
        {
            Button btnNum = sender as Button;
            if (btnNum != null && btnNum != btnDel)
            {
                if (lblOutPutMoney.Text.Contains(".") && btnNum.Text == ".")
                {
                    return;
                }
                lblOutPutMoney.Text += btnNum.Text;
                if (lblOutPutMoney.Text.Length==1&&btnNum.Text == "0")
                {
                    lblOutPutMoney.Text += ".";
                }                
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (lblOutPutMoney.Text.Length > 0)
            {
                lblOutPutMoney.Text = lblOutPutMoney.Text.Substring(0, lblOutPutMoney.Text.Length - 1);
            }
        }

        private void pictureCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblOutPutMoney.Text))
                {
                    return;
                }
                CustomTemplate = new ListAllTemplate();
                decimal cash = Convert.ToDecimal(lblOutPutMoney.Text);
                CustomTemplate.id = 0;
                CustomTemplate.amount = cash;
                CustomTemplate.rewardamount = rewardamount;
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("自定义充值异常" + ex.Message, true);
            }
        }

        private void FormCustomreCharge_Shown(object sender, EventArgs e)
        {
            lblOutPutMoney.Focus();
        }
        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        //计算赠送金额
        private void outputmoney_DataChanged(string data)
        {
            try
            {
                decimal amount = 0;
                decimal.TryParse(data, out amount);
                rewardamount = 0;

                for (int i = MainModel.LstRechargeTemplates.Count - 1; i >= 0; i--)
                {
                    var item = MainModel.LstRechargeTemplates[i];
                    if (item.amount <= 0)
                    {
                        continue;
                    }
                    rewardamount += Convert.ToInt32(Math.Floor(amount / item.amount)) * item.rewardamount;
                    amount = amount % item.amount;
                }

                this.Invoke(new InvokeHandler(delegate()
                {
                    if (rewardamount > 0)
                    {
                        lblRewardAmount.Visible = true;
                        lblRewardAmount.Text = "赠" + rewardamount.ToString("f2") + "元";
                    }
                    else
                    {
                        lblRewardAmount.Visible = false;
                        lblRewardAmount.Text = "";
                    }
                }));

            }
            catch (Exception ex)
            {
            }

        }
    }
}
