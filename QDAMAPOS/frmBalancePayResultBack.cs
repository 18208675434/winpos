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
    public partial class frmBalancePayResultBack : Form
    {
                /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();


        private Cart ParaCart = new Cart();


                public string OverOrderId = "";

           public ExpiredType ExpiredCode = ExpiredType.None;

           public Cart CurrentCart = new Cart();

        public frmBalancePayResultBack()
        {
            InitializeComponent();
        }

        public frmBalancePayResultBack(Cart cart)
        {
            InitializeComponent();
            ParaCart = cart;
        }

        private void frmBalancePayResultBack_Load(object sender, EventArgs e)
        {
            try
            {
                frmBalancePayResult frmbalancepayresult = new frmBalancePayResult(ParaCart);

                frmbalancepayresult.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmbalancepayresult.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmbalancepayresult.Height) / 2);
                frmbalancepayresult.TopMost = true;
                frmbalancepayresult.DataReceiveHandle += FormOnLinePayResult_DataReceiveHandle;
                frmbalancepayresult.Show();

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载在线支付窗体异常" + ex.Message, true);
                this.Close();
            }
        }



        private void FormOnLinePayResult_DataReceiveHandle(DialogResult dialogresult, string orderid,ExpiredType  expiredtype,Cart cart)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    this.DialogResult = dialogresult;
                    OverOrderId = orderid;
                    ExpiredCode = expiredtype;
                    CurrentCart = cart;
                    this.Close();
                }));
            }
            catch (Exception ex)
            {
                this.Close();
                LogManager.WriteLog("ERROR", "处理余额收银结果异常" + ex.Message);
            }

        }

    }
}
