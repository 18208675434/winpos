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
    public partial class FormPricing : Form
    {
        public AdjustPriceInfo  adjustpriceinfo = new AdjustPriceInfo();

        public Product CurrentProduct = null;

        #region 页面加载与退出
        public FormPricing(Product pro)
        {
            InitializeComponent();

            CurrentProduct = pro;

        }

        private void FormPricing_Shown(object sender, EventArgs e)
        {
            try
            {
              
                lblPrice.Text = "￥" + CurrentProduct.price.originsaleprice.ToString("f2");
                numBoard.Size = new Size(pnlNumber.Width - 8, rbtnOK.Top - numBoard.Top - 30);

                if (CurrentProduct.adjustpriceinfo != null && CurrentProduct.adjustpriceinfo.type==1)
                {
                    txtPrice.Text = CurrentProduct.adjustpriceinfo.amt.ToString("f2");
                }

                txtPrice.Focus();
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
                    decimal doublenum = Convert.ToDecimal(txtPrice.Text);

                    if (doublenum <= 0 || doublenum>=CurrentProduct.price.originsaleprice)
                    {
                        MainModel.ShowLog("价格只能小于当前商品价格",false);
                        return;
                    }

                   
                    adjustpriceinfo.amt = doublenum;
                    adjustpriceinfo.beforeamt = CurrentProduct.price.originsaleprice;
                    adjustpriceinfo.type = 1;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch
                {
                    return;
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
                    focusing.Text = focusing.Text.Substring(0,focusing.Text.Length-1);
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

        private void lblShuiyin_Click(object sender, EventArgs e)
        {
            txtPrice.Focus();
            
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtPrice.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

     

    }
}
