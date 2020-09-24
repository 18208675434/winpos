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
    public partial class FormTuikuan : Form
    {
        public FormTuikuan()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnpw1_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "1";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text+ "+￥1.0(赠送金额)";
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
                Kouchu.Visible = true ;

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
        BalanceDepositRefundRequest request = new BalanceDepositRefundRequest();

        private void btnOk_Click(object sender, EventArgs e)
        {
            btn();
        }
        public void btn()
        {
            try
            {
                request.refundcapital = MainModel.RechargeAmount.Replace("0元","") ;
                if (btncash.BackColor == Color.Blue || MainModel.fangshi == "现金")
                {
                    request.refundtype = 2;
                }
                else if (btnOriginal.BackColor == Color.Blue)
                {
                    request.refundtype = 1;
                }
                request.memberid = Convert.ToInt64(MainModel.MemberId);
                request.operatorid = Convert.ToInt64(MainModel.Id);
                request.depositbillid = Convert.ToInt64(MainModel.Depostid);
                request.shopid = MainModel.ShopId;
                request.tenantid = MainModel.Tenantid;
                string err = "";

               long  m = until.Depositbillrefund(request, ref err);
                if (m !=  null)
                {
                    this.Close();
                    BackHelper.HideFormBackGround();
                    MainModel.ShowLog("退款成功", true);

                }
                else
                {
                    this.Close();
                    

                }


            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private void FormTuikuan_Load(object sender, EventArgs e)
        {
            if (MainModel.Balance != "" )
            {
                txttuikuan.Text = MainModel.RechargeAmount;
                usermoney.Text = MainModel.Balance;
                usermoney.Visible = true;
                if (MainModel.fangshi != "现金")
                {
                    label2.Visible = false;
                    btncash.Visible = true;
                    btnOriginal.Visible = true;
                }
                else if(MainModel.fangshi == "现金")
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
