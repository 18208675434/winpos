using System;
using System.Drawing;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormRechargeRefund : Form
    {
        #region 全局变量
        HttpUtil until = new HttpUtil();
        decimal balance;
        string operatorid = "";
        string depostBillid;
        string memberId;
        string shopId = "";
        string tenantId = "";
        string payModeForApi = "";
        decimal rechargeAmount;
        decimal rewardAmount;
        int refundtype = 2;//默认现金 1-原路返回退款 2-现金
        #endregion
        public FormRechargeRefund(string depostBillid, string memberId, string shopId, string tenantId, string payModeForApi, decimal rechargeAmount, decimal rewardAmount)
        {
            InitializeComponent();

            this.depostBillid = depostBillid;
            this.memberId = memberId;
            this.shopId = shopId;
            this.tenantId = tenantId;
            this.payModeForApi = payModeForApi;
            this.rechargeAmount = rechargeAmount;
            this.rewardAmount = rewardAmount;
        }



        private void FormRechargeRefund_Load(object sender, EventArgs e)
        {
            LoadBalanceAccount();
            lblBalance.Text = "账户余额:￥" + balance.ToString("f2");
            lblBalance.Visible = true;
            lblTotalRefund.Location = new Point(lblBalance.Location.X + lblBalance.Width + 15, lblTotalRefund.Location.Y);
            txtRefound.Text = rechargeAmount.ToString();
            DisplayRefundAmount();
            if (payModeForApi == "现金")
            {
                refundtype = 2;
                lblCashDesc.Visible = true;
                btncash.Visible = false;
                btnOriginal.Visible = false;
            }
            else
            {
                refundtype = 1;
                lblCashDesc.Visible = false;
                btncash.Visible = true;
                btnOriginal.Visible = true;
            }
        }

        #region 事件
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRefound.Text))
                {
                    MainModel.ShowLog("请输入退款金额", true);
                    return;
                }               
                DepositRefundRequest request = new DepositRefundRequest();
                request.refundcapital = txtRefound.Text;
                request.refundtype = refundtype;
                request.memberid = memberId;
                request.operatorid = MainModel.CurrentUser.loginaccount;
                request.depositbillid = depostBillid;
                request.shopid = shopId;
                request.tenantid = tenantId;
                string err = "";
                LoadingHelper.ShowLoadingScreen();
                long refundId = until.Depositbillrefund(request, ref err); //获取退款的id  
                LoadingHelper.CloseForm();
                if (refundId > 0)
                {
                    //退款打印小票
                    PrintUtil.PrintTopUp(refundId.ToString(),true);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    BackHelper.HideFormBackGround();
                    MainModel.ShowLog("退款成功", true);
                }
                else
                {
                    MainModel.ShowLog("退款失败：" + err, true);
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("退款异常：" + ex.Message, true);
            }
        }



        private void btncash_Click(object sender, EventArgs e)
        {
            refundtype = 2;
            btncash.ForeColor = Color.White;
            btncash.BackColor = Color.FromArgb(20, 137, 205);
            btnOriginal.ForeColor = Color.FromArgb(20, 137, 205);
            btnOriginal.BackColor = Color.White;
        }
        private void btnOriginal_Click(object sender, EventArgs e)
        {
            refundtype = 1;
            btnOriginal.ForeColor = Color.White;
            btnOriginal.BackColor = Color.FromArgb(20, 137, 205);
            btncash.ForeColor = Color.FromArgb(20, 137, 205);
            btncash.BackColor = Color.White;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtRefound.Text.Length > 0)
            {
                txtRefound.Text = txtRefound.Text.Substring(0, txtRefound.Text.Length - 1);

            }
            DisplayRefundAmount();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            BackHelper.HideFormBackGround();
            this.Close();

        }

        #region 数字键

        private void btnpw1_Click(object sender, EventArgs e)
        {
            if (txtRefound.Text.Length <= 4)
            {
                txtRefound.Text += "1";
                DisplayRefundAmount();
            }
        }

        private void btnpw2_Click(object sender, EventArgs e)
        {
            if (txtRefound.Text.Length <= 4)
            {
                txtRefound.Text += "2";
                DisplayRefundAmount();
            }
        }

        private void btnpw3_Click(object sender, EventArgs e)
        {
            if (txtRefound.Text.Length <= 4)
            {
                txtRefound.Text += "3";
                DisplayRefundAmount();
            }
        }

        private void btnpw4_Click(object sender, EventArgs e)
        {
            if (txtRefound.Text.Length <= 4)
            {
                txtRefound.Text += "4";
                DisplayRefundAmount();
            }
        }

        private void btnpw5_Click(object sender, EventArgs e)
        {
            if (txtRefound.Text.Length <= 4)
            {
                txtRefound.Text += "5";
                DisplayRefundAmount();
            }
        }

        private void btnpw6_Click(object sender, EventArgs e)
        {
            if (txtRefound.Text.Length <= 4)
            {
                txtRefound.Text += "6";
                DisplayRefundAmount();

            }
        }

        private void btnpw7_Click(object sender, EventArgs e)
        {
            if (txtRefound.Text.Length <= 4)
            {
                txtRefound.Text += "7";
                DisplayRefundAmount();
            }
        }

        private void btnpw8_Click(object sender, EventArgs e)
        {
            if (txtRefound.Text.Length <= 4)
            {
                txtRefound.Text += "8";
                DisplayRefundAmount();
            }
        }

        private void btnpw9_Click(object sender, EventArgs e)
        {
            if (txtRefound.Text.Length <= 4)
            {
                txtRefound.Text += "9";
                DisplayRefundAmount();
            }
        }

        private void btnpwd_Click(object sender, EventArgs e)
        {
            if (txtRefound.Text.Length <= 4)
            {
                txtRefound.Text += ".";
                DisplayRefundAmount();
            }
        }

        private void btnpw0_Click(object sender, EventArgs e)
        {
            if (txtRefound.Text.Length <= 4)
            {
                txtRefound.Text += "0";
                DisplayRefundAmount();
            }

        }


        #endregion



        #endregion


        #region 方法
        /// <summary> 加载会员余额
        /// </summary>
        private void LoadBalanceAccount()
        {
            try
            {
                string errormsg = "";
                LoadingHelper.ShowLoadingScreen();
                ZtBalanceAccount currentBalanceAccount = until.ZtBalanceAccount(MainModel.CurrentMember.memberinformationresponsevo.memberid, ref errormsg);
                LoadingHelper.CloseForm();
                if (!string.IsNullOrEmpty(errormsg) || currentBalanceAccount == null)
                {
                    MainModel.ShowLog(errormsg, false);
                }
                else
                {
                    //TODO
                    balance = currentBalanceAccount.balance;

                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("获取会员账户余额异常" + ex.Message, true);
            }
        }
        /// <summary> 展示退款金额
        /// </summary>
        private void DisplayRefundAmount()
        {
            if (txtRefound.Text.Length == 0)
            {
                lblTotalRefund.Visible = false;
            }
            else
            {
                lblTotalRefund.Text = "扣除:￥" + txtRefound.Text;
                //if (rewardAmount > 0)
                //{
                //    lblTotalRefund.Text = lblTotalRefund.Text + "+￥" + rewardAmount.ToString("f2") + "(赠送金额)";
                //}
                lblTotalRefund.ForeColor = Color.Red;
                lblTotalRefund.Visible = true;
            }

        }
        #endregion

    }
}
