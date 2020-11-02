using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WinSaasPOS_Scale.BaseUI;

namespace WinSaasPOS_Scale
{
    public partial class FormPayCashToOnLine : Form
    {


        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

/// <summary>
/// 数据处理委托方法
/// </summary>
/// <param name="type">0：退出页面 1：现金收银完成  2：现金不足继续收银</param>
/// <param name="cashnum"></param>
        public delegate void DataRecHandleDelegate(int type);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;

        /// <summary>
        /// 现金收银界面传过来的 抹零后的cartModel
        /// </summary>
        private Cart CurrentCart;

        /// <summary>
        /// 现金收银界面传过来的商品集合
        /// </summary>
        List<scancodememberModel> Currentlstscancode = new List<scancodememberModel>();

        HttpUtil httputil = new HttpUtil();

       // string CurrentOrderID = "";

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public FormPayCashToOnLine()
        {
            InitializeComponent();
        }


        public void UpInfo(Cart cart)
        {
            try
            {
                CurrentCart = (Cart)cart.qianClone();

                //lblPrice.Text = "￥" + cart.producttotalamt.ToString("f2");
                lblPrice.Text = "￥" + cart.payamtbeforecash.ToString("f2"); 
                lblCash.Text = "￥" + cart.cashpayamt.ToString("f2");
                lblChange.Text = "￥" + cart.totalpayment.ToString("f2");


                Dictionary<string, string> dicdetail = new Dictionary<string, string>();
                dicdetail.Add("应付：", "￥" + cart.payamtbeforecash.ToString("f2"));
                dicdetail.Add("已付现金：", "￥" + cart.cashpayamt.ToString("f2"));

                BaseUIHelper.UpdateDgvOrderDetail(dicdetail, "还需支付：", "￥" + cart.totalpayment.ToString("f2"));
                
            }
            catch (Exception ex)
            {

            }
        }

        //返回上层
        private void btnCancle_Click(object sender, EventArgs e)
        {
            ////if (DataReceiveHandle != null)
            ////    this.DataReceiveHandle.BeginInvoke(0, null, null);

            //string errormsg = "";
            //bool result = httputil.CancleOrder(CurrentOrderID, "取消支付", ref errormsg);

            //if (result)
            //{
            //    LogManager.WriteLog("取消订单" + CurrentOrderID);

            //}
            //else
            //{
            //    ShowLog("订单取消失败" + errormsg, true);
            //}
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }


        //现金不足，需要继续支付
        private void btnPayNext_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmOnLine_SizeChanged(object sender, EventArgs e)
        {
            //asf.ControlAutoSize(this);
        }
    }
}
