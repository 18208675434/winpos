using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.ChangePriceUI
{
    public partial class FormOrderPricing : Form
    {

        private ChangeType CurrentChangeType = ChangeType.totaldiscount;
        private static decimal CurrentTotalpaymentBeforeFix = 0;

        public decimal fixpricetotal = 0;

        #region 页面加载与退出
        public FormOrderPricing(decimal total)
        {
            InitializeComponent();

            CurrentTotalpaymentBeforeFix = total;
        }

        private void FormPricing_Shown(object sender, EventArgs e)
        {
            try
            {
                lblBeforeFixTotal.Text = "￥" + CurrentTotalpaymentBeforeFix;
                lblDiscountPrice.Text = "￥" + CurrentTotalpaymentBeforeFix;
                numBoard.Size = new Size(this.Width - 8, rbtnOK.Top - numBoard.Top - 20);

                txtPrice.Focus();


                if (MainModel.CurrentShopInfo.posalterorderdiscountflag == 1 && MainModel.CurrentShopInfo.posalterorderpriceflag == 1)
                {
                    //txtPrice.WaterText = "最高可折扣" + MainModel.CurrentShopInfo.posalterorderdiscountrange;
                    txtPrice.WaterText = "请输入折扣";
                    // btnUnitPrice.PerformClick();
                }
                else if (MainModel.CurrentShopInfo.posalterorderdiscountflag == 0 && MainModel.CurrentShopInfo.posalterorderpriceflag == 1)
                {
                    btnTotalPrice.Visible = false;
                    btnUnitPrice.Visible = false;
                  //  btnTotalPrice.PerformClick();

                    btnTotalPrice_Click(null,null);
                }
                else if (MainModel.CurrentShopInfo.posalterorderdiscountflag == 1 && MainModel.CurrentShopInfo.posalterorderpriceflag == 0)
                {
                    btnTotalPrice.Visible = false;
                    btnUnitPrice.Visible = false;
                  //  btnUnitPrice.PerformClick();

                    btnUnitPrice_Click(null,null);
                }

                Application.DoEvents();
            }
            catch { }
        }

        #endregion

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void rbtnOK_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                decimal doublenum = 0;
                try
                {
                    doublenum = Convert.ToDecimal(txtPrice.Text);

                    if (doublenum <= 0)
                    {
                        return;
                    }
                }
                catch
                {
                    return;
                }

                if (CurrentChangeType == ChangeType.totalprice)
                {
                    if (CurrentTotalpaymentBeforeFix- doublenum > MainModel.CurrentShopInfo.posalterorderpricerange)
                    {
                        MainModel.ShowLog("超过最高可减金额￥" + MainModel.CurrentShopInfo.posalterorderpricerange, false);
                        return;
                    }
                    if (doublenum > CurrentTotalpaymentBeforeFix)
                    {
                        MainModel.ShowLog("最终收取的金额必须小于订单应收", false);
                        return;
                    }

                    fixpricetotal = doublenum;

                    this.DialogResult = DialogResult.OK;
                    this.Close();

                }
                else if (CurrentChangeType == ChangeType.totaldiscount)
                {
                    if (doublenum < MainModel.CurrentShopInfo.posalterorderdiscountrange)
                    {
                        MainModel.ShowLog("超过最高折扣" + MainModel.CurrentShopInfo.posalterorderdiscountrange + "折", false);
                        return;
                    }

                    fixpricetotal = (decimal)CurrentTotalpaymentBeforeFix * doublenum / 10;


                   
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

            }
            catch (Exception ex)
            {
            }
        }



        string keyInput = "";
        private void MiniKeyboardHandler(object sender, MyControl.NumberBoard.KeyboardArgs e)
        {
            ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox focusing = txtPrice;


            keyInput = e.KeyCode;

            int startDel = 0;

            //退格
            if (keyInput == numBoard.KeyDelete)
            {
                if (focusing.Text.Length > 0)
                {
                    focusing.Text = focusing.Text.Substring(0, focusing.Text.Length - 1);
                }
            }
            else if (keyInput == numBoard.KeyDot)
            {
                if (!focusing.Text.Contains(".") && !string.IsNullOrEmpty(focusing.Text))
                {
                    focusing.Text += ".";
                }
            }

            //其他键直接输入
            else
            {
                focusing.Text += keyInput;

            }
            // txtPrice.Focus();
        }


        private void btnDiscount_Click(object sender, EventArgs e)
        {
            try
            {

                btn5.BackgroundImage = picNotSelect.Image;
                btn6.BackgroundImage = picNotSelect.Image;
                btn7.BackgroundImage = picNotSelect.Image;
                btn8.BackgroundImage = picNotSelect.Image;
                btn9.BackgroundImage = picNotSelect.Image;

                btn5.ForeColor = Color.FromArgb(80, 80, 80);
                btn6.ForeColor = Color.FromArgb(80, 80, 80);
                btn7.ForeColor = Color.FromArgb(80, 80, 80);
                btn8.ForeColor = Color.FromArgb(80, 80, 80);
                btn9.ForeColor = Color.FromArgb(80, 80, 80);

                Button btn = (Button)sender;
                btn.BackgroundImage = picSelect.Image;
                btn.ForeColor = Color.White;
                int discount = Convert.ToInt16(btn.Tag);

                txtPrice.Text = discount.ToString();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("快捷折扣按钮异常" + ex.Message, false);
            }
        }


        private void btnUnitPrice_Click(object sender, EventArgs e)
        {

            if (CurrentChangeType == ChangeType.unitprice)
            {
                return;
            }

            //txtPrice.WaterText = "最高可折扣" + MainModel.CurrentShopInfo.posalterorderdiscountrange;
            txtPrice.WaterText = "请输入折扣";

            txtPrice.Clear();
            btnUnitPrice.BackColor = Color.FromArgb(20, 137, 205);
            btnUnitPrice.ForeColor = Color.White;
            btnTotalPrice.BackColor = Color.White;
            btnTotalPrice.ForeColor = Color.FromArgb(20, 137, 205);

            lblBeforeFixTotal.Text = "￥" + CurrentTotalpaymentBeforeFix.ToString("f2");

            lblDiscountPrice.Visible = true;
            lblStrDiscount.Visible = true;
            lblType.Text = "折";
            pnlBtnDiscount.Visible = true;

            CurrentChangeType = ChangeType.totaldiscount;
        }

        private void btnTotalPrice_Click(object sender, EventArgs e)
        {
            if (CurrentChangeType == ChangeType.totalprice)
            {
                return;
            }
            //txtPrice.WaterText = "最高可减价" + MainModel.CurrentShopInfo.posalterorderpricerange;
            txtPrice.WaterText = "请输入修改价格";
            txtPrice.Clear();
            btnUnitPrice.BackColor = Color.White;
            btnUnitPrice.ForeColor = Color.FromArgb(20, 137, 205);
            btnTotalPrice.BackColor = Color.FromArgb(20, 137, 205);
            btnTotalPrice.ForeColor = Color.White;

            lblBeforeFixTotal.Text = "￥" + CurrentTotalpaymentBeforeFix.ToString("f2");

            lblDiscountPrice.Visible = false;
            lblStrDiscount.Visible = false;

            lblType.Text = "元";
            pnlBtnDiscount.Visible = false;

            CurrentChangeType = ChangeType.totalprice;
        }

        private void txtPrice_DataChanged(string data)
        {
            try
            {

                if (CurrentChangeType == ChangeType.totaldiscount)
                {
                    if (txtPrice.Text == "0" || txtPrice.Text == "." || txtPrice.Text == "")
                    {
                        txtPrice.Clear();
                        // lblDiscountPrice.Text = "￥" + CurrentTotalpaymentBeforeFix*tempdi
                    }

                    if (Convert.ToDecimal(txtPrice.Text) >= 10)
                    {
                        txtPrice.Text = txtPrice.Text.Insert(1, ".");
                    }

                    decimal tempdiscount = Convert.ToDecimal(txtPrice.Text) / 10;

                    lblDiscountPrice.Text = "￥" + (CurrentTotalpaymentBeforeFix * tempdiscount).ToString("f2");
                }


            }
            catch (Exception ex)
            {

            }
        }


    }


}
