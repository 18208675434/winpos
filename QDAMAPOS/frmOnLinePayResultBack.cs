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
    public partial class frmOnLinePayResultBack : Form
    {
        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        /// <summary>
        /// 1、交易完成 2、交易失败
        /// </summary>
        /// <param name="type"></param>
        public delegate void DataRecHandleDelegate(int type, string orderid);

        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;

        private string ParaOrderID = "";
        private Cart ParaCart = new Cart();

        public frmOnLinePayResultBack()
        {
            InitializeComponent();
        }
            public frmOnLinePayResultBack(string orderid,Cart cart)
        {
            InitializeComponent();
            ParaOrderID = orderid;
            ParaCart = cart;
        }

        private void frmOnLinePayResultBack_Load(object sender, EventArgs e)
        {
            try
            {

                frmOnLinePayResult frmonlinepayresult = new frmOnLinePayResult(ParaOrderID,ParaCart);
                
                //frmonlinepayresult.Size = new System.Drawing.Size(Screen.AllScreens[0].Bounds.Width / 3, this.Height - 200);
                frmonlinepayresult.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmonlinepayresult.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmonlinepayresult.Height) / 2);
                frmonlinepayresult.TopMost = true;
                frmonlinepayresult.DataReceiveHandle += FormOnLinePayResult_DataReceiveHandle;
                frmonlinepayresult.Show();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载在线支付窗体异常" + ex.Message, true);
                this.Close();
            }
        }


        private void FormOnLinePayResult_DataReceiveHandle(int type, string orderid)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    if (DataReceiveHandle != null)
                        this.DataReceiveHandle.BeginInvoke(type, orderid, null, null);

                    this.Close();
                }));
            }
            catch(Exception ex)
            {
                this.Close();
                LogManager.WriteLog("ERROR", "处理在线收银结果异常" + ex.Message);
            }

        }

    }
}
