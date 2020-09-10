using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard
{
    public partial class FormGiftCardByCash : Form
    {

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        /// <summary>
        /// 接口访问类
        /// </summary>
        private GiftCardHttp giftcardhttp = new GiftCardHttp();

      
        /// <summary>
        /// 界面初始化录入默认值   修改的话自动清空
        /// </summary>
        bool isfirst = true;

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();


        public CartAloneUpdate currentcart = null;

        public decimal RealCash = 0;


        public FormGiftCardByCash(CartAloneUpdate cart)
        {
            InitializeComponent();
            try
            {
                currentcart = cart;

                lblPrice.Text = "￥" + cart.ppaidamt.ToString("f2");
                txtCash.Text = cart.ppaidamt.ToString("f2");
                txtCash.Focus();
            }
            catch { }

        }




        private void btnCancle_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("关闭现金支付窗体异常" + ex.Message);
            }
        }

        //下一步需要判断实收现金是否足够 通过cart接口返回值判断
        private void btnNext_Click(object sender, EventArgs e)
        {

            try
            {
                
                    if (string.IsNullOrEmpty(txtCash.Text))
                    {
                        MainModel.ShowLog("请输入金额",false);
                        return;
                    }
                    decimal doublenum = Convert.ToDecimal(txtCash.Text);

                    if (doublenum < currentcart.ppaidamt)
                    {
                        MainModel.ShowLog("支付金额不能小于应收金额", false);
                        return;
                    }
                    else if (doublenum == currentcart.ppaidamt)
                    {

                        try
                        {
                            PrintUtil.OpenCashDrawerEx();
                        }
                        catch
                        {
                        }

                        RealCash = Convert.ToDecimal(txtCash.Text);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }else{
                        try
                        {
                                PrintUtil.OpenCashDrawerEx(); 
                        }
                        catch
                        {
                        }

                        FormPayCashToChange frmpaycashtochange = new FormPayCashToChange(currentcart.ppaidamt,doublenum);
                        asf.AutoScaleControlTest(frmpaycashtochange, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                        frmpaycashtochange.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaycashtochange.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaycashtochange.Height) / 2);

                        frmpaycashtochange.TopMost = true;
                        frmpaycashtochange.ShowDialog();

                        frmpaycashtochange.Dispose();
                        Application.DoEvents();
                        if (frmpaycashtochange.DialogResult == DialogResult.OK)
                        {
                            RealCash = Convert.ToDecimal(txtCash.Text);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("现金支付异常：" + ex.StackTrace, false);
            }
        }


    

        #region  小键盘按键
        private void btn_Click(object sender, EventArgs e)
        {
            if (isfirst)
            {
                txtCash.Clear();
                isfirst = false;
            }
            Button btn = (Button)sender;
            try
            {
                //小数点后允许一位  现金抹零后不允许再输入零
                if (txtCash.Text.Length > 2 && txtCash.Text.Substring(txtCash.Text.Length - 2, 1) == ".")
                {
                    return;
                }
                //限制金额不超过100000

                //第一位是0 后面只能输入.
                if (txtCash.Text == "0")
                {
                    return;
                }

                decimal CheckDecimal = Convert.ToDecimal(txtCash.Text + btn.Name.Replace("btn", ""));

                if (CheckDecimal > 100000)
                {
                    return;
                }
                txtCash.Text += btn.Name.Replace("btn", "");
            }
            catch
            {

            }



        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (isfirst)
            {
                txtCash.Clear();
                isfirst = false;
            }


            if (txtCash.Text.Length > 0 && !txtCash.Text.Contains("."))
            {
                txtCash.Text += ".";
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (isfirst)
                {
                    txtCash.Clear();
                    isfirst = false;
                }


                if (txtCash.Text.Length > 0)
                {
                    txtCash.Text = txtCash.Text.Substring(0, txtCash.Text.Length - 1);
                }
            }
            catch
            {

            }
            finally
            {
                txtCash.Focus();
                this.txtCash.Select(this.txtCash.TextLength, 0);
            }
        }


        //控制仅允许录入数字
        private void TextNUMBER_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        #endregion



        private void txtNum_TextChanged(object sender, EventArgs e)
        {
           
            if (txtCash.Text.Length > 0)
            {
                lblShuiyin.Visible = false;
            }
            else
            {
                lblShuiyin.Visible = true;
            }

            try
            {
                double doublenum = Convert.ToDouble(txtCash.Text);

                if (doublenum > 0)
                {
                    btnNext.BackColor = Color.OrangeRed;
                }
                else
                {
                    btnNext.BackColor = Color.Silver;
                }
            }
            catch
            {
                btnNext.BackColor = Color.Silver;
            }
            finally
            {
                txtCash.Focus();
                this.txtCash.Select(this.txtCash.TextLength, 0);
            }
        }

        private void lblShuiyin_Click(object sender, EventArgs e)
        {
            txtCash.Focus();
            this.txtCash.Select(this.txtCash.TextLength, 0);
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                if (keyData == Keys.Enter)
                {
                    btnNext_Click(null,null);
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }


        }
    }
}
