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

namespace ZhuiZhi_Integral_Scale_UncleFruit.HelperUI
{
    public partial class FormCouponPwd : Form
    {

        private string CouponCode = "";
        private HttpUtil httputil = new HttpUtil();
        public FormCouponPwd( string couponcode)
        {
            InitializeComponent();

            CouponCode = couponcode;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //下一步需要判断实收现金是否足够
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (txtNum.Text.Length == 0 || btnOK.Tag==null || btnOK.Tag.ToString()=="0")
                {
                    return;
                }

                //TODO  验证密码

                string errormsg ="";
                bool result =httputil.ValidateOuterCoupon(CouponCode,txtNum.Text,ref errormsg);

                if (!string.IsNullOrEmpty(errormsg) || result == null)
                {
                    MainModel.ShowLog(errormsg,false);
                    return;
                }

                if (result == false)
                {
                    MainModel.ShowLog("密码不正确",false);
                    return;
                }

                this.DialogResult = DialogResult.OK;

                this.Close();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("数字窗体关闭异常" + ex.StackTrace);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;

                Button button = (Button)sender;

                txtNum.Text += button.Name.Replace("btn", "");
            }
            catch { }
            finally
            {
               // txtNum.Focus();
            }
        }


        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;

                if (txtNum.Text.Length > 0)
                {
                    txtNum.Text = txtNum.Text.Substring(0, txtNum.Text.Length - 1);
                }
            }
            catch { }


        }



        private void txtNum_DataChanged(string data)
        {
            try
            {
                //double doublenum = Convert.ToDouble(txtNum.Text);
                decimal doublenum = 0;
                bool changesuccess = decimal.TryParse(txtNum.Text, out doublenum);
                if (changesuccess && doublenum > 0)
                {
                    btnOK.BackColor = Color.FromArgb(20, 137, 205);
                    btnOK.Tag = 1;

                }
                else
                {
                    btnOK.BackColor = Color.LightGray;
                    btnOK.Tag = 0;

                }
            }
            catch (Exception ex)
            {
                btnOK.BackColor = Color.LightGray;
                btnOK.Tag = 0;

                LogManager.WriteLog("数字窗体控件数据变化检测异常" + ex.Message);
            }
           
        }

    }
}
