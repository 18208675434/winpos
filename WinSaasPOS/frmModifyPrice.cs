using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinSaasPOS.Common;
using WinSaasPOS.Model;

namespace WinSaasPOS
{
    public partial class frmModifyPrice : Form
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">0:支付完成   1：在线收银继续支付 3：取消  12004：会员登录失效   100031：店员登录失效</param>
        /// <param name="orderid"></param>
        public delegate void DataRecHandleDelegate(int type, decimal fixprice);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;
        

        private Cart thisCurrentCart = new Cart();

        public decimal fixpricetotal = 0;

        public frmModifyPrice()
        {
            InitializeComponent();
        }
        public frmModifyPrice(Cart cart)
        {
            InitializeComponent();
            thisCurrentCart = (Cart)cart.qianClone();
        }

        
        private void frmModifyPrice_Shown(object sender, EventArgs e)
        {
            //btnNext.Focus();
        
        }

        private void frmModifyPrice_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Dispose();
            }catch(Exception ex){

            }
        }

        private void lblShuiyin_Click(object sender, EventArgs e)
        {
            txtCash.Focus();
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtCash.Text.Length > 0)
                {
                    lblShuiyin.Visible = false;
                }
                else
                {
                    lblShuiyin.Visible = true;
                }

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
           
        }



        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            try
            {
                //小数点后允许2
                if (txtCash.Text.Length > 3 && txtCash.Text.Substring(txtCash.Text.Length - 3, 1) == ".")
                {
                    return;
                }

                decimal CheckDecimal = Convert.ToDecimal(txtCash.Text + btn.Name.Replace("btn", ""));

                if (CheckDecimal > 100000 || txtCash.Text == "00")
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
            if (txtCash.Text.Length > 0 && !txtCash.Text.Contains("."))
            {
                txtCash.Text += ".";
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {

            if (txtCash.Text.Length > 0)
            {
                txtCash.Text = txtCash.Text.Substring(0, txtCash.Text.Length - 1);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    double doublenum = Convert.ToDouble(txtCash.Text);

                    if (doublenum <= 0)
                    {
                        return;
                    }
                }
                catch
                {
                    return;
                }
                //if (txtCash.Text.Length == 0 || txtCash.Text=="0")
                //{
                //    return;
                //}
                fixpricetotal = Convert.ToDecimal(txtCash.Text);
                if (fixpricetotal > thisCurrentCart.totalpaymentbeforefix)
                {
                    MainModel.ShowLog("最终收取的金额必须小于订单应收", false);
                    return;
                }
                this.DialogResult = DialogResult.OK;

                if (DataReceiveHandle != null)
                    this.DataReceiveHandle.BeginInvoke(1, fixpricetotal, null, null);

                this.Hide();
                //this.Close();

            }
            catch (Exception ex)
            {
            }
        }

        private void lbtnCancle_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataReceiveHandle != null)
                    this.DataReceiveHandle.BeginInvoke(0, 0, null, null);

                this.Hide();
               // this.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public void UpInfo(Cart cart)
        {
            try
            {
                txtCash.Text = "";
                thisCurrentCart = (Cart)cart.qianClone();

                btnDel.Focus();
                lblPrice.Text = "￥" + thisCurrentCart.totalpaymentbeforefix.ToString();
               // fixpricetotal = 0;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("修改支付金额窗体异常" + ex.Message);
            }
        }


    }
}
