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
    public partial class FormPricing : Form
    {
        public AdjustPriceInfo  adjustpriceinfo = new AdjustPriceInfo();

        public Product CurrentProduct = null;

        public ChangeType CurrentChangeType = ChangeType.unitprice;

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
                numBoard.Size = new Size(this.Width - 8, rbtnOK.Top - numBoard.Top - 20);

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
                   // decimal temptotal = CurrentProduct.adjustpriceinfo == null ? CurrentProduct.price.total : CurrentProduct.price.origintotal;


                    decimal temptotal = CurrentProduct.price.total;
                    if (CurrentProduct.adjustpriceinfo != null)
                    {
                        if (CurrentProduct.goodstagid == 0)
                        {
                            temptotal = CurrentProduct.adjustpriceinfo.beforeamt * CurrentProduct.num;
                        }
                        else
                        {
                            temptotal = CurrentProduct.adjustpriceinfo.beforeamt * CurrentProduct.price.specnum;
                        }
                    }

                    if (doublenum < 0)
                    {
                        MainModel.ShowLog("价格不能小于0", false);
                        return;
                    }

                    if ((CurrentChangeType == ChangeType.unitprice && doublenum >= CurrentProduct.price.originsaleprice) || (CurrentChangeType == ChangeType.totalprice && doublenum >=temptotal))
                    {
                        MainModel.ShowLog("价格只能小于当前商品价格",false);
                        return;
                    }

                    //fmj 2023.2.20 start
                    //单价调价
                    if (CurrentChangeType == ChangeType.unitprice)
                    {
                        if (ChangePriceUtil.PriceImpose(doublenum, CurrentProduct.price.originsaleprice, Lessthen.LESS))
                        {
                            MainModel.ShowLog("低于最高可调单价￥" + ChangePriceUtil.ThenPriceRange(CurrentProduct.price.originsaleprice), false);
                            return;
                        }
                    }
                    //总价调价
                    else if (CurrentChangeType == ChangeType.totalprice)
                    {
                        if (ChangePriceUtil.PriceImpose(doublenum, temptotal, Lessthen.LESS))
                        {
                            MainModel.ShowLog("低于最高可调总价￥" + ChangePriceUtil.ThenPriceRange(temptotal), false);
                            return;
                        }
                    }
                    //end

                    adjustpriceinfo.amt = doublenum;
                    adjustpriceinfo.beforeamt = CurrentProduct.price.originsaleprice;

                    if (CurrentChangeType == ChangeType.unitprice)
                    {
                        adjustpriceinfo.type = 1;
                    }
                    else
                    {
                        adjustpriceinfo.type = 3;
                    }

                    AbnormalOrderUtil.SingleAdjustPrice(CurrentProduct, CurrentProduct.price.originsaleprice-doublenum);

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

        private void btnUnitPrice_Click(object sender, EventArgs e)
        {

            
            if (CurrentChangeType == ChangeType.unitprice)
            {
                return;
            }

            btnUnitPrice.BackColor = Color.FromArgb(20, 137, 205);
            btnUnitPrice.ForeColor = Color.White;
            btnTotalPrice.BackColor = Color.White;
            btnTotalPrice.ForeColor = Color.FromArgb(20, 137, 205);
            lblStrPrice.Text = "当前单价";
            lblPrice.Text = "￥" + CurrentProduct.price.originsaleprice.ToString("f2");
            CurrentChangeType = ChangeType.unitprice;
            
        }

        private void btnTotalPrice_Click(object sender, EventArgs e)
        {
            if (CurrentChangeType == ChangeType.totalprice)
            {
                return;
            }

            btnUnitPrice.BackColor = Color.White;
            btnUnitPrice.ForeColor = Color.FromArgb(20, 137, 205);
            btnTotalPrice.BackColor = Color.FromArgb(20, 137, 205);
            btnTotalPrice.ForeColor = Color.White;
            lblStrPrice.Text = "当前总价";

            decimal temptotal = CurrentProduct.price.total;
            if (CurrentProduct.adjustpriceinfo != null)
            {
                if (CurrentProduct.goodstagid == 0)
                {
                    temptotal = CurrentProduct.adjustpriceinfo.beforeamt * CurrentProduct.num;
                }
                else
                {
                    temptotal = CurrentProduct.adjustpriceinfo.beforeamt * CurrentProduct.price.specnum;
                }
            }
            //CurrentProduct.adjustpriceinfo == null ? CurrentProduct.price.total : CurrentProduct.price.origintotal;

            lblPrice.Text = "￥" + temptotal.ToString("f2");
            CurrentChangeType = ChangeType.totalprice;
        }

    }

   
}
