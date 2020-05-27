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
    public partial class frmPrinterSettingBack : Form
    {

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public frmPrinterSettingBack()
        {
            InitializeComponent();
        }

        private void frmPrinterSettingBack_Load(object sender, EventArgs e)
        {
            try
            {

                if (MainModel.frmprintersetting != null)
                {
                    MainModel.frmprintersetting.DataReceiveHandle += FormPrinterSetting_DataReceiveHandle;

                    MainModel.frmprintersetting.Show();
                }
                else
                {
                    MainModel.frmprintersetting = new frmPrinterSetting();
                    MainModel.frmprintersetting.TopMost = true;
                    asf.AutoScaleControlTest(MainModel.frmprintersetting, 536, 243, 536 * MainModel.wScale, 243 * MainModel.hScale, true);
                    MainModel.frmprintersetting.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - MainModel.frmprintersetting.Width) / 2, (Screen.AllScreens[0].Bounds.Height - MainModel.frmprintersetting.Height) / 2);                

                    MainModel.frmprintersetting.DataReceiveHandle += FormPrinterSetting_DataReceiveHandle;

                    MainModel.frmprintersetting.Show();
                }

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载打印机设置背景窗体异常" + ex.Message, true);
                this.Close();
            }
        }


        private void FormPrinterSetting_DataReceiveHandle(int type)
        {

            try
            {
                this.Close();

            }
            catch (Exception ex)
            {
                this.Close();
                LogManager.WriteLog("ERROR", "处理现金券窗体结果异常" + ex.Message);
            }
            finally
            {
                MainModel.frmprintersetting.DataReceiveHandle -= FormPrinterSetting_DataReceiveHandle;
            }

        }


    }
}
