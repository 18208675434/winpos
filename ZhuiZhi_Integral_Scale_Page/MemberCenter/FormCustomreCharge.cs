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

        private void btnpw1_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblOutPutMoney.Text.Length == 3)
                {
                    lblOutPutMoney.Text += "1";
                    int zeng = int.Parse(lblOutPutMoney.Text);
                    int name = zeng / 10;
                    lblRewardAmount.Text = "赠" + name + ".00元";
                    ////lblRewardAmount.Visible = true;

                }
                else if (lblOutPutMoney.Text.Length == 2)
                {
                    lblOutPutMoney.Text += "1";
                    int zeng = int.Parse(lblOutPutMoney.Text);
                    int name = zeng / 10;
                    lblRewardAmount.Text = "赠" + name + ".00元";
                    //lblRewardAmount.Visible = true;

                }
                else if (lblOutPutMoney.Text.Length == 1)
                {
                    lblOutPutMoney.Text += "1";
                    int zeng = int.Parse(lblOutPutMoney.Text);
                    int name = zeng / 10;
                    lblRewardAmount.Text = "赠" + name + ".00元";
                    //lblRewardAmount.Visible = true;

                }
                else if (lblOutPutMoney.Text == "")
                {
                    lblOutPutMoney.Text += "1";
                }
                else if (lblOutPutMoney.Text.Length > 3)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("格式输入错误" + ex.Message, true);
                throw;
            }

        }

        private void btnpw2_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblOutPutMoney.Text.Length == 3)
                {
                    lblOutPutMoney.Text += "2";

                    int zeng = int.Parse(lblOutPutMoney.Text);
                    int name = zeng / 10;
                    lblRewardAmount.Text = "赠" + name + ".00元";
                    //lblRewardAmount.Visible = true;

                }
                else if (lblOutPutMoney.Text.Length == 2)
                {
                    lblOutPutMoney.Text += "2";

                    int zeng = int.Parse(lblOutPutMoney.Text);
                    int name = zeng / 10;
                    lblRewardAmount.Text = "赠" + name + ".00元";
                    //lblRewardAmount.Visible = true;


                }
                else if (lblOutPutMoney.Text.Length == 1)
                {
                    lblOutPutMoney.Text += "2";

                    int zeng = int.Parse(lblOutPutMoney.Text);
                    int name = zeng / 10;
                    lblRewardAmount.Text = "赠" + name + ".00元";
                    //lblRewardAmount.Visible = true;

                }
                else if (lblOutPutMoney.Text == "")
                {
                    lblOutPutMoney.Text += "2";
                }
                else if (lblOutPutMoney.Text.Length > 3)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("输入格式错误" + ex.Message, true);
                throw;
            }

        }

        private void btnpw3_Click(object sender, EventArgs e)
        {
            if (lblOutPutMoney.Text.Length == 3)
            {
                lblOutPutMoney.Text += "3";
                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;

            }
            else if (lblOutPutMoney.Text.Length == 2)
            {
                lblOutPutMoney.Text += "3";
                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;


            }
            else if (lblOutPutMoney.Text.Length == 1)
            {
                lblOutPutMoney.Text += "3";
                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;


            }
            else if (lblOutPutMoney.Text == "")
            {
                lblOutPutMoney.Text += "3";
            }
            else if (lblOutPutMoney.Text.Length > 3)
            {
                return;
            }
        }

        private void btnpw4_Click(object sender, EventArgs e)
        {
            if (lblOutPutMoney.Text.Length == 3)
            {
                lblOutPutMoney.Text += "4";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;

            }
            else if (lblOutPutMoney.Text.Length == 2)
            {
                lblOutPutMoney.Text += "4";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;


            }
            else if (lblOutPutMoney.Text.Length == 1)
            {
                lblOutPutMoney.Text += "4";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;


            }
            else if (lblOutPutMoney.Text == "")
            {
                lblOutPutMoney.Text += "4";
            }
            else if (lblOutPutMoney.Text.Length > 3)
            {
                return;
            }
        }

        private void btnpw5_Click(object sender, EventArgs e)
        {
            if (lblOutPutMoney.Text.Length == 3)
            {
                lblOutPutMoney.Text += "5";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;

            }
            else if (lblOutPutMoney.Text.Length == 2)
            {
                lblOutPutMoney.Text += "5";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;

            }
            else if (lblOutPutMoney.Text.Length == 1)
            {
                lblOutPutMoney.Text += "5";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;

            }
            else if (lblOutPutMoney.Text == "")
            {
                lblOutPutMoney.Text += "5";
            }
            else if (lblOutPutMoney.Text.Length > 3)
            {
                return;
            }
        }

        private void btnpw6_Click(object sender, EventArgs e)
        {
            if (lblOutPutMoney.Text.Length == 3)
            {
                lblOutPutMoney.Text += "6";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;
            }
            else if (lblOutPutMoney.Text.Length == 2)
            {
                lblOutPutMoney.Text += "6";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;

            }
            else if (lblOutPutMoney.Text.Length == 1)
            {
                lblOutPutMoney.Text += "6";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;

            }
            else if (lblOutPutMoney.Text == "")
            {
                lblOutPutMoney.Text += "6";
            }
            else if (lblOutPutMoney.Text.Length > 3)
            {
                return;
            }
        }

        private void btnpw7_Click(object sender, EventArgs e)
        {
            if (lblOutPutMoney.Text.Length == 3)
            {
                lblOutPutMoney.Text += "7";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;
            }
            else if (lblOutPutMoney.Text.Length == 2)
            {
                lblOutPutMoney.Text += "7";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;

            }
            else if (lblOutPutMoney.Text.Length == 1)
            {
                lblOutPutMoney.Text += "7";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;

            }
            else if (lblOutPutMoney.Text == "")
            {
                lblOutPutMoney.Text += "7";
            }
            else if (lblOutPutMoney.Text.Length > 3)
            {
                return;
            }
        }

        private void btnpw8_Click(object sender, EventArgs e)
        {
            if (lblOutPutMoney.Text.Length == 3)
            {
                lblOutPutMoney.Text += "8";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;
            }
            else if (lblOutPutMoney.Text.Length == 2)
            {
                lblOutPutMoney.Text += "8";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;
            }
            else if (lblOutPutMoney.Text.Length == 1)
            {
                lblOutPutMoney.Text += "8";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;
            }
            else if (lblOutPutMoney.Text == "")
            {
                lblOutPutMoney.Text += "8";
            }
            else if (lblOutPutMoney.Text.Length > 3)
            {
                return;
            }
        }

        private void btnpw9_Click(object sender, EventArgs e)
        {
            if (lblOutPutMoney.Text.Length == 3)
            {
                lblOutPutMoney.Text += "9";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;
            }
            else if (lblOutPutMoney.Text.Length == 2)
            {
                lblOutPutMoney.Text += "9";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;

            }
            else if (lblOutPutMoney.Text.Length == 1)
            {
                lblOutPutMoney.Text += "9";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;

            }
            else if (lblOutPutMoney.Text == "")
            {
                lblOutPutMoney.Text += "9";
            }
            else if (lblOutPutMoney.Text.Length > 3)
            {
                return;
            }
        }

        private void btnpwd_Click(object sender, EventArgs e)
        {
            if (lblOutPutMoney.Text.Length == 3)
            {
                lblOutPutMoney.Text += ".";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;
            }
            else if (lblOutPutMoney.Text.Length == 2)
            {
                lblOutPutMoney.Text += ".";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;

            }
            else if (lblOutPutMoney.Text.Length == 1)
            {
                lblOutPutMoney.Text += ".";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;

            }
            else if (lblOutPutMoney.Text == "")
            {
                lblOutPutMoney.Text += ".";
            }
            else if (lblOutPutMoney.Text.Length > 3)
            {
                return;
            }


        }

        private void btnpw0_Click(object sender, EventArgs e)
        {
            if (lblOutPutMoney.Text.Length == 3)
            {
                lblOutPutMoney.Text += "0";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;
            }
            else if (lblOutPutMoney.Text.Length == 2)
            {
                lblOutPutMoney.Text += "0";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;

            }
            else if (lblOutPutMoney.Text.Length == 1)
            {
                lblOutPutMoney.Text += "0";

                int zeng = int.Parse(lblOutPutMoney.Text);
                int name = zeng / 10;
                lblRewardAmount.Text = "赠" + name + ".00元";
                //lblRewardAmount.Visible = true;

            }
            else if (lblOutPutMoney.Text == "")
            {
                lblOutPutMoney.Text += "0";
            }
            else if (lblOutPutMoney.Text.Length > 3)
            {
                return;
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {


                if (lblOutPutMoney.Text.Length > 0)
                {
                    lblOutPutMoney.Text = lblOutPutMoney.Text.Substring(0, lblOutPutMoney.Text.Length - 1);
                }
                else if (lblOutPutMoney.Text == "")
                {
                    lblRewardAmount.Text = "";
                    lblRewardAmount.Visible = false;
                }
                if (lblOutPutMoney.Text.Length == 3)
                {
                    int zeng = int.Parse(lblOutPutMoney.Text);
                    int name = zeng / 10;
                    lblRewardAmount.Text = "赠" + name + ".00元";
                    //lblRewardAmount.Visible = true;
                }
                else if (lblOutPutMoney.Text.Length == 2)
                {
                    int zeng = int.Parse(lblOutPutMoney.Text);
                    int name = zeng / 10;
                    lblRewardAmount.Text = "赠" + name + ".00元";
                    //lblRewardAmount.Visible = true;

                }
                else if (lblOutPutMoney.Text.Length == 1)
                {
                    int zeng = int.Parse(lblOutPutMoney.Text);
                    int name = zeng / 10;
                    lblRewardAmount.Text = "赠" + name + ".00元";
                    //lblRewardAmount.Visible = true;

                }
            }
            catch { }
        }

        private void pictureCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private HttpUtil util = new HttpUtil();
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
                decimal.TryParse(lblOutPutMoney.Text, out amount);
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
