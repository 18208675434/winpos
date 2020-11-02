using QDAMAPOS.Common;
using QDAMAPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QDAMAPOS
{
    public partial class frmCashPayOffLineBack : Form
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
        public frmCashPayOffLineBack()
        {
            InitializeComponent();
        }

        public frmCashPayOffLineBack(Cart cart)
        {
            InitializeComponent();
            ParaCart = cart;
        }

        private void frmCashPayBack_Shown(object sender, EventArgs e)
        {

            try
            {
                if (MainModel.frmcashpayoffline != null)
                {

                    MainModel.frmcashpayoffline.DataReceiveHandle += FormCashPay_DataReceiveHandle;
                    MainModel.frmcashpayoffline.Show();
                    MainModel.frmcashpayoffline.UpInfo(ParaCart);
                }
                else
                {
                    MainModel.frmcashpayoffline = new frmCashPayOffLine(ParaCart);
                    ///frmcashpay.frmCashPay_SizeChanged(null, null);
                    asf.AutoScaleControlTest(MainModel.frmcashpayoffline, 380, 520, Screen.AllScreens[0].Bounds.Width * 36 / 100, Screen.AllScreens[0].Bounds.Height * 70 / 100, true);
                   // MainModel.frmcashpayoffline.Size = new System.Drawing.Size(Screen.AllScreens[0].Bounds.Width * 36 / 100, this.Height * 70 / 100);
                    MainModel.frmcashpayoffline.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - MainModel.frmcashpayoffline.Width - 40, Screen.AllScreens[0].Bounds.Height * 15 / 100);

                    MainModel.frmcashpayoffline.TopMost = true;
                    MainModel.frmcashpayoffline.DataReceiveHandle += FormCashPay_DataReceiveHandle;
                    MainModel.frmcashpayoffline.Show();
                }



            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载现金支付窗体异常" + ex.Message, true);
                MainModel.frmcashpayoffline.DataReceiveHandle -= FormCashPay_DataReceiveHandle;

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
                MainModel.frmcashpayoffline.DataReceiveHandle -= FormCashPay_DataReceiveHandle;
                this.Close();

            }
            catch (Exception ex)
            {
                MainModel.frmcashpayoffline.DataReceiveHandle -= FormCashPay_DataReceiveHandle;
                LogManager.WriteLog("ERROR", "处理确认窗体结果异常" + ex.Message);
                MainModel.frmcashpayoffline = null;
                this.Close();
               
            }

        }




    }
}
