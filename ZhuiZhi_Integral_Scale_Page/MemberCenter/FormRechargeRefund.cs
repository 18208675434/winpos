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
        public string balance;
        public string operatorid = "";
        public string depostBillid;
        public string memberId;
        public string shopId = "";
        public string tenantId = "";
        public string payModeForApi = "";
        public decimal rechargeAmount;
        public decimal rewardAmount;
        #endregion
        public FormRechargeRefund(string depostBillid,string memberId,string shopId,string tenantId, string payModeForApi, decimal rechargeAmount, decimal rewardAmount)
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


        private void btnpw1_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "1";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw2_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "2";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw3_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "3";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw4_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "4";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw5_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "5";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw6_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "6";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw7_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "7";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw8_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "8";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw9_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "9";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpwd_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += ".";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw0_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "0";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length > 0)
            {
                txttuikuan.Text = txttuikuan.Text.Substring(0, txttuikuan.Text.Length - 1);
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
            }
            else
            {
                Kouchu.Text = "";
                Kouchu.Visible = false;
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            BackHelper.HideFormBackGround();
            this.Close();

        }
        HttpUtil until = new HttpUtil();
        DepositRefundRequest request = new DepositRefundRequest();

        private void btnOk_Click(object sender, EventArgs e)
        {
            btn();
        }
        public void btn()
        {
            try
            {
                request.refundcapital = rechargeAmount.ToString().Replace("0元", "");
                if (btncash.BackColor == Color.Blue || payModeForApi == "现金")
                {
                    request.refundtype = 2;
                }
                else if (btnOriginal.BackColor == Color.Blue)
                {
                    request.refundtype = 1;
                }
                request.memberid = memberId;
                request.operatorid =MainModel.CurrentUser.loginaccount;
                request.depositbillid = depostBillid;
                request.shopid = shopId;
                request.tenantid = tenantId;
                string err = "";

                long refundId = until.Depositbillrefund(request, ref err); //获取退款的id               
                if (refundId > 0)
                {
                    this.Tag = refundId;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    BackHelper.HideFormBackGround();
                    MainModel.ShowLog("退款成功", true);
                }
                else
                {
                    MainModel.ShowLog("退款异常：" + err, true);
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("退款异常：" + ex.Message, true);
            }
        }
       
        private void FormTuikuan_Load(object sender, EventArgs e)
        {
            if (balance != "")
            {
                txttuikuan.Text = rechargeAmount.ToString();
                usermoney.Text = balance;
                usermoney.Visible = true;
                if (payModeForApi != "现金")
                {
                    label2.Visible = false;
                    btncash.Visible = true;
                    btnOriginal.Visible = true;
                }
                else if (payModeForApi == "现金")
                {
                    label2.Visible = true;
                    btncash.Visible = false;
                    btnOriginal.Visible = false;
                }
            }


        }

        private void btncash_Click(object sender, EventArgs e)
        {
            xianjin();
        }

        public void xianjin()
        {
            btncash.ForeColor = Color.White;
            btncash.BackColor = Color.Blue;
            btnOriginal.BackColor = Color.White;
        }
        private void btnOriginal_Click(object sender, EventArgs e)
        {
            yuan();
        }
        public void yuan()
        {
            btnOriginal.ForeColor = Color.White;
            btnOriginal.BackColor = Color.Blue;
            btncash.BackColor = Color.White;
        }
    }
}
