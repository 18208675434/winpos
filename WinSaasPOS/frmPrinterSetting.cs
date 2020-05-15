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
using System.Drawing.Printing;

namespace WinSaasPOS
{

    //    ///串口
    //#define COM1	1
    //#define COM2	2
    //#define COM3	3
    //#define COM4	4
    //#define COM5	5
    //#define COM6	6
    //#define COM7	7
    //#define COM8	8
    //#define COM9	9
    //#define COM10	10
    /////并口
    //#define LPT1	11
    //#define LPT2	12

    /////USB 口
    //#define USB		13

    /////(有线或无线WIFI)网络 打印机
    //#define NET		14

    public partial class frmPrinterSetting : Form
    {
        /// <summary>
        /// 打印机宽度
        /// </summary>
        private int PageSize = 58;
        /// <summary>
        /// 便宜量
        /// </summary>
        private int LocationX = 0;
        /// <summary>
        /// 打印机名称
        /// </summary>
        private string PrintName = "";


        /// <summary>
        /// 58 MM纸张下 一行能够打印的小字符数
        /// </summary>
        private int BodyCharCountOfLine = 30;

        /// <summary>
        /// 58 MM纸张下 一行能够打印的大字符数
        /// </summary>
        private int HeadCharCountOfLine = 28;

        /// <summary>
        /// 58 MM纸张下 小字符高度
        /// </summary>
        private double BodyCharHeightOfLine = 14;

        /// <summary>
        /// 58 MM纸张下 大字符高度
        /// </summary>
        private double HeadCharHeightOfLine = 16;


        Font fontHead;
      //  SizeF sizehead;

        Font fontBody;
        //SizeF sizeBody;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">
        /// <param name="orderid"></param>
        public delegate void DataRecHandleDelegate(int type);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;

        public frmPrinterSetting()
        {
            InitializeComponent();
        }

        private void frmPrinterSetting_Shown(object sender, EventArgs e)
        {
            UpdatePrint();
        }

        private void UpdatePrint()
        {
            try
            {
                this.Enabled = false;
                LoadingHelper.ShowLoadingScreen("加载中...");
                //第一次没有这些值
                try
                {
                    PageSize = Convert.ToInt32(INIManager.GetIni("Print", "PageSize", MainModel.IniPath));

                    LocationX = Convert.ToInt32(INIManager.GetIni("Print", "LocationX", MainModel.IniPath));

                    PrintName = INIManager.GetIni("Print", "PrintName", MainModel.IniPath);
                }
                catch
                {

                }


                bool ExitsPrinter = false;
                cbxPrint.Items.Clear();
                //获取安装的打印机列表，并选中默认打印机
                foreach (string print in PrinterSettings.InstalledPrinters)
                {
                    cbxPrint.Items.Add(print);
                    if (print == PrintName)
                    {
                        ExitsPrinter = true;
                    }
                }

                //默认打印机
                PrintDocument pd = new PrintDocument();
                string defaultStr = pd.PrinterSettings.PrinterName;

                if (ExitsPrinter)
                {
                    cbxPrint.SelectedItem = PrintName;
                }
                else
                {
                    INIManager.SetIni("Print", "PrintName", "", MainModel.IniPath);
                    cbxPrint.SelectedItem = defaultStr;
                }

                txtPaperSize.Text = PageSize.ToString();

                txtLocationX.Text = LocationX.ToString();
                this.Enabled = true;
                LoadingHelper.CloseForm();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载打印机参数异常" + ex.Message);
            }
            finally
            {
                this.Enabled = true;
                LoadingHelper.CloseForm();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string printname = cbxPrint.Text;
            string pagesize = txtPaperSize.Text;
            string locationx = txtLocationX.Text;

            if (string.IsNullOrEmpty(printname))
            {
                MainModel.ShowLog("请选择打印机", false);
                return;
            }

            if (string.IsNullOrEmpty(pagesize))
            {
                MainModel.ShowLog("请输入纸张尺寸", false);
                return;
            }
            else
            {
                try
                {
                    Convert.ToDecimal(pagesize);
                }
                catch
                {
                    MainModel.ShowLog("请输入正确的纸张尺寸", false);
                    return;
                }
            }

            if (string.IsNullOrEmpty(locationx))
            {
                MainModel.ShowLog("请填写偏移量", false);
                return;
            }
            else
            {
                try
                {
                    Convert.ToDecimal(locationx);
                }
                catch
                {
                    MainModel.ShowLog("请输入正确的偏移量数值", false);
                    return;
                }
            }

            INIManager.SetIni("Print", "PageSize", txtPaperSize.Text, MainModel.IniPath);
            INIManager.SetIni("Print", "LocationX", txtLocationX.Text, MainModel.IniPath);
            INIManager.SetIni("Print", "PrintName", cbxPrint.Text, MainModel.IniPath);

            MainModel.ShowLog("保存完成",false);

            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(0, null, null);
            this.Hide();
            //this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(0, null, null);
            this.Hide();
            //this.Close();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdatePrint();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {

            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(0,  null, null);
            this.Hide();
           // this.Close();

        }

        private void txt_Enter(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                GlobalUtil.OpenOSK();
                txt.Focus();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("焦点打开键盘异常" + ex.Message);
            }
        }

        private void txt_Leave(object sender, EventArgs e)
        {
            try
            {

                if (!txtPaperSize.Focused && !txtLocationX.Focused)
                {
                    GlobalUtil.CloseOSK();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("失去焦点关闭键盘异常" + ex.Message);
            }
        }

        private void frmPrinterSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            GlobalUtil.CloseOSK();
        }

    }

}

