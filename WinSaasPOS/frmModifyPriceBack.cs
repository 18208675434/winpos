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
    public partial class frmModifyPriceBack : Form
    {

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private Cart ParaCart;

        public decimal fixpricetotal = 0;

        public frmModifyPriceBack()
        {
            InitializeComponent();
        }

        public frmModifyPriceBack(Cart cart)
        {
            InitializeComponent();
            ParaCart = cart;
        }

        private void frmModifyPriceBack_Shown(object sender, EventArgs e)
        {
            try
            {
                if (MainModel.frmmodifyprice != null)
                {
                    MainModel.frmmodifyprice.DataReceiveHandle += FormModifyPrice_DataReceiveHandle;
                    MainModel.frmmodifyprice.Show();
                    MainModel.frmmodifyprice.UpInfo(ParaCart);
                }
                else
                {
                    MainModel.frmmodifyprice = new frmModifyPrice(ParaCart);
                    ///frmcashpay.frmCashPay_SizeChanged(null, null);
                    asf.AutoScaleControlTest(MainModel.frmcashpay, 380, 520, Screen.AllScreens[0].Bounds.Width * 36 / 100, Screen.AllScreens[0].Bounds.Height * 70 / 100, true);
                    MainModel.frmmodifyprice.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - MainModel.frmmodifyprice.Width - 40, Screen.AllScreens[0].Bounds.Height * 15 / 100);

                    MainModel.frmmodifyprice.TopMost = true;
                    MainModel.frmmodifyprice.DataReceiveHandle += FormModifyPrice_DataReceiveHandle;
                    MainModel.frmmodifyprice.Show();
                }

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载修改订单金额窗体异常" + ex.Message, true);
                MainModel.frmmodifyprice.DataReceiveHandle -= FormModifyPrice_DataReceiveHandle;

                this.Close();
            }
        }



        private void FormModifyPrice_DataReceiveHandle(int type, decimal fixprice)
        {
            try
            {

                if (type == 1)
                {
                    fixpricetotal = fixprice;
                    this.DialogResult = DialogResult.OK;
                    MainModel.frmmodifyprice.DataReceiveHandle -= FormModifyPrice_DataReceiveHandle;
                    this.Close();
                }
                else
                {
                    fixpricetotal = 0;
                    this.DialogResult = DialogResult.Cancel;
                    MainModel.frmmodifyprice.DataReceiveHandle -= FormModifyPrice_DataReceiveHandle;
                    this.Close();
                }
              

            }
            catch (Exception ex)
            {
                MainModel.frmmodifyprice.DataReceiveHandle -= FormModifyPrice_DataReceiveHandle;
                LogManager.WriteLog("ERROR", "处理修改订单金额结果异常" + ex.Message);
                this.Close();

            }

        }

    }
}
