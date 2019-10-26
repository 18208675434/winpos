using QiandamaPOS.Common;
using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QiandamaPOS
{
    public partial class frmCashChange : Form
    {

/// <summary>
/// 数据处理委托方法
/// </summary>
/// <param name="type">0：退出页面 1：找零页面确认 </param>
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


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public frmCashChange()
        {
            InitializeComponent();
        }

        public frmCashChange(Cart cart)
        {
            InitializeComponent();
            CurrentCart = (Cart)cart.qianClone();
            IniForm(cart);
           
        }


        private void IniForm(Cart cart)
        {
            try
            {
                lblPrice.Text = "￥" + cart.payamtbeforecash.ToString();
                lblCash.Text = "￥" + cart.cashpayamt.ToString();
                lblChange.Text = "￥" + cart.cashchangeamt.ToString();                                           
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

            this.Close();
        }
        //现金支付完成
        private void btnPayOK_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(1, null, null);
            this.Close();
        }

        public void frmCashChange_SizeChanged(object sender, EventArgs e)
        {
           // asf.ControlAutoSize(this);
        }

    }
}
