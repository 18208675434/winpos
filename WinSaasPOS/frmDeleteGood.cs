using WinSaasPOS.Common;
using WinSaasPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinSaasPOS
{
    public partial class frmDeleteGood : Form
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

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public frmDeleteGood(string msgstr,string proname,string barcode)
        {
            InitializeComponent();

            try
            {
                if (string.IsNullOrEmpty(proname + barcode))
                {
                    lbtnCancle.Location = new Point(lbtnCancle.Location.X,lbtnCancle.Location.Y-30);
                    lbtnOK.Location = new Point(lbtnOK.Location.X, lbtnOK.Location.Y - 30);
                    this.Size = new System.Drawing.Size(Convert.ToInt32(520 * MainModel.wScale), Convert.ToInt32((160-30) * MainModel.hScale));
                }
                else
                {
                    this.Size = new System.Drawing.Size(Convert.ToInt32(520 * MainModel.wScale), Convert.ToInt32(160 * MainModel.hScale));
                }


                if (msgstr.Contains("会员登录已过期") || msgstr.Contains("温馨提示"))
                {
                    lbtnCancle.Visible = false;
                }
                    
                AutoScaleControl();

                this.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - this.Width) / 2, (Screen.AllScreens[0].Bounds.Height - this.Height) / 2);
                lblMsgStr.Text = msgstr;
                lblMsg.Text = proname + "  " + barcode;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("确认窗体显示异常"+ex.Message);
            }
        }

        private void frmDeleteGood_SizeChanged(object sender, EventArgs e)
        {
           //asf.ControlAutoSize(this);
        }

        private void lbtnCancle_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(0, null, null);
            this.Close();
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
