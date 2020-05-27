using WinSaasPOS.Common;
using WinSaasPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinSaasPOS
{
    public partial class frmCashPayBack : Form
    {


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private Cart ParaCart = new Cart();

        /// <summary>
        /// 页面返回类型结果
        /// </summary>
        public int cashpaytype = 0;

        /// <summary>
        /// 页面返回订单
        /// </summary>
        public string cashpayorderid = "";

        public Cart CurrentCart = new Cart();
        public frmCashPayBack()
        {
            InitializeComponent();
        }

        public frmCashPayBack(Cart cart)
        {
            InitializeComponent();
            ParaCart = cart;
        }

        private void frmCashPayBack_Shown(object sender, EventArgs e)
        {

            try
            {
                if (MainModel.frmcashpay != null)
                {

                    MainModel.frmcashpay.DataReceiveHandle += FormCashPay_DataReceiveHandle;
                    MainModel.frmcashpay.Show();
                    MainModel.frmcashpay.UpInfo(ParaCart);
                }
                else
                {
                    MainModel.frmcashpay = new frmCashPay(ParaCart);
                    ///frmcashpay.frmCashPay_SizeChanged(null, null);
                    asf.AutoScaleControlTest(MainModel.frmcashpay, 380, 520, Screen.AllScreens[0].Bounds.Width * 36 / 100, Screen.AllScreens[0].Bounds.Height * 70 / 100, true);
                   // MainModel.frmcashpay.Size = new System.Drawing.Size(Screen.AllScreens[0].Bounds.Width * 36 / 100, this.Height * 70 / 100);
                    MainModel.frmcashpay.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - MainModel.frmcashpay.Width - 40, Screen.AllScreens[0].Bounds.Height * 15 / 100);

                    MainModel.frmcashpay.TopMost = true;
                    MainModel.frmcashpay.DataReceiveHandle += FormCashPay_DataReceiveHandle;
                    MainModel.frmcashpay.Show();
                }



            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载现金支付窗体异常" + ex.Message, true);
                MainModel.frmcashpay.DataReceiveHandle -= FormCashPay_DataReceiveHandle;

                this.Close();
            }
        }




        private void FormCashPay_DataReceiveHandle(int type, string orderid,Cart cart)
        {
            try
            {
                cashpaytype = type;
                cashpayorderid = orderid;
                CurrentCart = cart;
                MainModel.frmcashpay.DataReceiveHandle -= FormCashPay_DataReceiveHandle;
                this.Close();

            }
            catch (Exception ex)
            {
                MainModel.frmcashpay.DataReceiveHandle -= FormCashPay_DataReceiveHandle;
                LogManager.WriteLog("ERROR", "处理确认窗体结果异常" + ex.Message);
                MainModel.frmcashpay = null;
                this.Close();
               
            }

        }




    }
}
