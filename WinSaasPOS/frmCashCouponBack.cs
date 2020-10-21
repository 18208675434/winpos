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
    public partial class frmCashCouponBack : Form
    {

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private Cart ParaCart = new Cart();

        public string SuccessOrderID = "";
        public List<string> ParaLstCash = new List<string>();
        public frmCashCouponBack()
        {
            InitializeComponent();
        }
        public frmCashCouponBack(Cart cart,List<string> lstcash)
        {
            InitializeComponent();
            ParaCart = cart;
            ParaLstCash = lstcash;
        }

        private void frmCashCouponBack_Load(object sender, EventArgs e)
        {
            try
            {

                if (MainModel.frmcashcoupon != null)
                {
                    MainModel.frmcashcoupon.DataReceiveHandle += FormCashCoupon_DataReceiveHandle;
                    MainModel.frmcashcoupon.UpInfo(ParaCart);
                    
                    MainModel.frmcashcoupon.Show();
                    MainModel.frmcashcoupon.LoadCashCoupon(ParaLstCash);
                    
                }
                else
                {
                    MainModel.frmcashcoupon = new frmCashCoupon(ParaCart,ParaLstCash);
                    asf.AutoScaleControlTest(MainModel.frmcashcoupon, 380, 480, Screen.AllScreens[0].Bounds.Width * 36 / 100, Screen.AllScreens[0].Bounds.Height * 70 / 100, true);
                    MainModel.frmcashcoupon.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - MainModel.frmcashpay.Width - 40, Screen.AllScreens[0].Bounds.Height * 15 / 100);
                    MainModel.frmcashcoupon.TopMost = true;

                    MainModel.frmcashcoupon.DataReceiveHandle += FormCashCoupon_DataReceiveHandle;

                    MainModel.frmcashcoupon.Show();
                }
               
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载现金券背景窗体异常" + ex.Message, true);
                this.Close();
            }
        }


        private void FormCashCoupon_DataReceiveHandle(int type, string orderid)
        {
            
                try
                {

                    if (type == 0)
                    {
                        this.DialogResult = DialogResult.Cancel;
                    }
                    else
                    {
                        SuccessOrderID = orderid;
                        this.DialogResult = DialogResult.OK;
                    }
                    
                    this.Close();

                }
                catch (Exception ex)
                {
                    this.Close();
                    LogManager.WriteLog("ERROR", "处理现金券窗体结果异常" + ex.Message);
                }
                finally
                {
                    MainModel.frmcashcoupon.DataReceiveHandle -= FormCashCoupon_DataReceiveHandle;
                }

        }

    }
}
