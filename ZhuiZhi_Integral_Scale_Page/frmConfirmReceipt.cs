using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class frmConfirmReceipt : Form
    {

        /// <summary>
        /// 数据处理委托方法
        /// </summary>
        /// <param name="type">0：返回  1：确认</param>
        public delegate void DataRecHandleDelegate(int result);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;

        private Receiptdetail CurrentReceipt;

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public frmConfirmReceipt(Receiptdetail receipt)
        {
            InitializeComponent();

            CurrentReceipt = receipt;
            try
            {

               this.Size = new System.Drawing.Size(Convert.ToInt32(600 * MainModel.wScale), Convert.ToInt32(200 * MainModel.hScale));


                AutoScaleControl();

                this.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - this.Width) / 2, (Screen.AllScreens[0].Bounds.Height - this.Height) / 2);

                //PrintUtil.OpenCashDrawerEx();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("确认窗体显示异常" + ex.Message);
            }
        }

        private void frmDeleteGood_SizeChanged(object sender, EventArgs e)
        {
           //asf.ControlAutoSize(this);
        }

        private void lbtnCancle_Click(object sender, EventArgs e)
        {
            try {
                this.Enabled = false;
                string ErrorMsgReceipt = "";
                bool receiptresult = PrintUtil.ReceiptPrint(CurrentReceipt, ref ErrorMsgReceipt);
            }
            catch (Exception ex) {
                LogManager.WriteLog("重新交班出现异常："+ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void lbtnOK_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(1, null, null);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void AutoScaleControl()
        {

            try
            {
                foreach (Control c in this.Controls)
                {
                    c.Left = (int)Math.Ceiling(c.Left * MainModel.wScale);
                    c.Top = (int)Math.Ceiling(c.Top * MainModel.hScale);

                    c.Width = (int)Math.Ceiling(c.Width * MainModel.wScale);
                    c.Height = (int)Math.Ceiling(c.Height * MainModel.hScale);

                    float wSize = c.Font.Size * MainModel.wScale;
                    float hSize = c.Font.Size * MainModel.hScale;
                  
                    c.Font = new Font(c.Font.Name, Math.Min(hSize, wSize), c.Font.Style, c.Font.Unit);

                }
            }
            catch
            {

            }
          
        }
    }
}
