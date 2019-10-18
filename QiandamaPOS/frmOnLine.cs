﻿using QiandamaPOS.Common;
using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QiandamaPOS
{
    public partial class frmOnLine : Form
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

        string CurrentOrderID = "";


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public frmOnLine()
        {
            InitializeComponent();
        }

        public frmOnLine(string orderid,Cart cart)
        {
            InitializeComponent();
            CurrentOrderID = orderid;
            CurrentCart = (Cart)cart.qianClone();
            IniForm(CurrentCart);
        }



        //现金支付界面不足 跳转过来
        public frmOnLine(Cart cart, List<scancodememberModel> lstscancode)
        {
            InitializeComponent();
            CurrentCart = cart;
            Currentlstscancode = lstscancode;
            IniForm(cart);
           
        }

        public frmOnLine(double price, double cash)
        {
            InitializeComponent();

            //ini(price, cash);
        }

        private void IniForm(Cart cart)
        {
            try
            {

                lblPrice.Text = "￥" + cart.producttotalamt;
                    lblCash.Text = "￥" + cart.cashpayamt.ToString();
                    lblChange.Text = "￥" + cart.totalpayment.ToString();                
                
            }
            catch (Exception ex)
            {

            }
        }

        //返回上层
        private void btnCancle_Click(object sender, EventArgs e)
        {
            //if (DataReceiveHandle != null)
            //    this.DataReceiveHandle.BeginInvoke(0, null, null);

            string errormsg = "";
            bool result = httputil.CancleOrder(CurrentOrderID, "取消支付", ref errormsg);

            if (result)
            {
                LogManager.WriteLog("取消订单" + CurrentOrderID);

            }
            else
            {
                ShowLog("订单取消失败" + errormsg, true);
            }

            this.Close();

        }
        //现金支付完成
        private void btnPayOK_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(1, null, null);
            this.Close();
        }

        //现金不足，需要继续支付
        private void btnPayNext_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(1, null, null);
            this.Close();
        }

        //信息提示
        private void ShowLog(string msg, bool iserror)
        {
            this.Invoke(new InvokeHandler(delegate()
            {

                frmMsg frmmsf = new frmMsg(msg, iserror, 1000);
                frmmsf.ShowDialog(); LogManager.WriteLog(msg);
            }));

        }

        private void frmOnLine_SizeChanged(object sender, EventArgs e)
        {
            //asf.ControlAutoSize(this);
        }
    }
}
