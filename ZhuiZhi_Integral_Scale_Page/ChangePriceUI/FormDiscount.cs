using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.ChangePriceUI
{
    public partial class FormDiscount : Form
    {

        public AdjustPriceInfo adjustpriceinfo = new AdjustPriceInfo();

        public Product CurrentProduct = null;

        #region  页面加载与退出
        public FormDiscount(Product pro)
        {
            InitializeComponent();
            CurrentProduct = pro;
        }

        private void FormDiscount_Shown(object sender, EventArgs e)
        {
            try
            {

             
                lblPrice.Text = "￥" + CurrentProduct.price.originsaleprice.ToString("f2");

                numBoard.Size = new Size(pnlNumber.Width - 8, rbtnOK.Top - numBoard.Top - 30);

                if (CurrentProduct.adjustpriceinfo != null && CurrentProduct.adjustpriceinfo.type==2)
                {
                    txtPrice.Text = (CurrentProduct.adjustpriceinfo.amt * 10).ToString();
                }

                txtPrice.Focus();
            }
            catch (Exception ex)
            {

            }

        }

        #endregion


        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtnOK_ButtonClick(object sender, EventArgs e)
        {

                try
                {

                    if (string.IsNullOrEmpty(txtPrice.Text))
                    {
                        adjustpriceinfo = null;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                    }

                    if (!rbtnOK.WhetherEnable)
                    {
                        return;
                    }
                    decimal doublenum = Convert.ToDecimal(txtPrice.Text)/10;

                    if (doublenum <= 0 || doublenum>=1)
                    {
                        MainModel.ShowLog("请输入正确的折扣",false);
                        return;
                    }
                    adjustpriceinfo.amt = doublenum;
                    adjustpriceinfo.beforeamt = CurrentProduct.price.originsaleprice;
                    adjustpriceinfo.type = 2; 
                    this.DialogResult = DialogResult.OK;

                    this.Close();
                }
                catch
                {
                    return;
                }
        }



        string keyInput = "";
        private void MiniKeyboardHandler(object sender, MyControl.NumberBoard.KeyboardArgs e)
        {
            TextBox focusing = txtPrice;
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
        }

        private void lblShuiyin_Click(object sender, EventArgs e)
        {
            txtPrice.Focus();
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblShuiyin.Visible = string.IsNullOrEmpty(txtPrice.Text);
                if (txtPrice.Text == "0" || txtPrice.Text == "." || txtPrice.Text == "")
                {
                    txtPrice.Clear();
                    lblDiscountPrice.Text = "￥" + CurrentProduct.price.originsaleprice.ToString("f2");
                }

                if (Convert.ToDecimal(txtPrice.Text) >= 10)
                {
                    txtPrice.Text = txtPrice.Text.Insert(1, ".");
                }

                if (txtPrice.Text.Length > 6)
                {
                    txtPrice.Text = txtPrice.Text.Substring(0,txtPrice.Text.Length-1);
                }
                else
                {
                    decimal tempdiscount = Convert.ToDecimal(txtPrice.Text) / 10;

                    lblDiscountPrice.Text = "￥" + (CurrentProduct.price.originsaleprice * tempdiscount).ToString("f2");

                    //if (tempdiscount > 0 && tempdiscount < 1)
                    //{
                    //    rbtnOK.WhetherEnable = true;
                    //}
                    //else
                    //{
                    //    rbtnOK.WhetherEnable = false;
                    //}
                }
               
            }
            catch (Exception ex)
            {
                                    
            }

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
            catch(Exception ex) {
                MainModel.ShowLog("快捷折扣按钮异常"+ex.Message,false);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtPrice.Text = "";
            }
            catch { }
        }



    }
}
