using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class FormToolMainScale : Form
    {

        public delegate void DataRecHandleDelegate(ToolType tooltype);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;


        public FormToolMainScale()
        {
            InitializeComponent();
        }

        private void FormToolMainScale_Shown(object sender, EventArgs e)
        {
            try
            {
                pnlLine2.Height = 1;
                pnlLine4.Height = 1;

                lblDeviceSN.Text = "设备号:" + MainModel.DeviceSN;
                lblVersion.Text = "版本号:" + MainModel.Version;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载任务栏菜单异常"+ex.Message,true);
            }
        }

        private void pnlExit_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(ToolType.Exit, null, null);

            this.Hide();
        }

        private void pnlScaleModel_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(ToolType.ScaleModel, null, null);

            this.Hide();
        }

        private void FormToolMainScale_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
