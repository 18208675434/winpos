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
using System.Drawing.Printing;
using ZhuiZhi_Integral_Scale_UncleFruit.MyEnum;
using System.Net;
using ZhuiZhi_Integral_Scale_UncleFruit.ScaleFactory;
using System.Xml;

namespace ZhuiZhi_Integral_Scale_UncleFruit
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
        /// 小票打印机名称
        /// </summary>
        private string PrintName = "";

        /// <summary>
        /// 标签打印机名称
        /// </summary>
        private string LabelPrintName = "";

        //页面加载完成后再监听属性值变化 否则加载页面赋值会触发事件
        private bool IsLoadSuccess = false;
        public frmPrinterSetting()
        {
            InitializeComponent();
        }

        private void frmPrinterSetting_Shown(object sender, EventArgs e)
        {
            try
            {
                lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;
                lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
                picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
                lblMenu.Left = picMenu.Right;

                IsLoadSuccess = false;
                LoadCashierSet();
                LoadTvIp();
                LoadScaleSet();
                UpdatePrint();
                LoadHalfOffLineIni();
                LoadShowWithJinIni();
                IsLoadSuccess = true;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载设备参数异常"+ex.Message,true);
            }
            finally
            {
                IsLoadSuccess = true;
            }       
        }

        private void frmPrinterSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            GlobalUtil.CloseOSK();
            this.Dispose();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
           try
           {
               this.Close();
               //this.Dispose();
           }
           catch (Exception ex)
           {
               LogManager.WriteLog("关闭设备管理页面异常",ex.Message);
           }
        }


        #region 收银设置
        /// <summary>
        /// 加载收银设置
        /// </summary>
        private void LoadCashierSet()
        {
            try
            {

                string WhetherAutoCart = INIManager.GetIni("CashierSet", "WhetherAutoCart", MainModel.IniPath);
                string WhetherPrint = INIManager.GetIni("CashierSet", "WhetherPrint", MainModel.IniPath);
                string WhetherAutoPrint = INIManager.GetIni("CashierSet", "WhetherAutoPrint", MainModel.IniPath);


               


                if (WhetherPrint == "1")
                {
                    MainModel.WhetherPrint = true;
                    picOpenPrint.BackgroundImage = picSelect.Image;
                    picClosePrint.BackgroundImage = picNotSelect.Image;

                }
                else
                {
                    MainModel.WhetherPrint = false ;
                    picOpenPrint.BackgroundImage = picNotSelect.Image;
                    picClosePrint.BackgroundImage = picSelect.Image;

                }

                if (WhetherAutoCart == "1")
                {
                    MainModel.WhetherAutoCart = true;
                    picOpenAutoCart.BackgroundImage = picSelect.Image;
                    picCloseAutoCart.BackgroundImage = picNotSelect.Image;
                }
                else
                {
                    MainModel.WhetherAutoCart = false;
                    picOpenAutoCart.BackgroundImage = picNotSelect.Image;
                    picCloseAutoCart.BackgroundImage = picSelect.Image;

                }


                if (WhetherAutoPrint == "1")
                {
                    MainModel.WhetherAutoPrint = true;
                    picOpenAutoPrint.BackgroundImage = picSelect.Image;
                    picCloseAutoPrint.BackgroundImage = picNotSelect.Image;
                }
                else
                {
                    MainModel.WhetherAutoPrint = false;
                    picOpenAutoPrint.BackgroundImage = picNotSelect.Image;
                    picCloseAutoPrint.BackgroundImage = picSelect.Image;
                }

                if (WhetherAutoCart != "1" && WhetherPrint == "1")
                {
                    pnlAutoPrint.Visible = true;
                }
                else
                {
                    pnlAutoPrint.Visible = false;
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载收银设置异常"+ex.Message);
            }
        }

        //开启自动加购
        private void pnlOpenAutoCart_Click(object sender, EventArgs e)
        {
           
                MainModel.WhetherAutoCart = true;
                picOpenAutoCart.BackgroundImage = picSelect.Image;
                picCloseAutoCart.BackgroundImage = picNotSelect.Image;

                INIManager.SetIni("CashierSet", "WhetherAutoCart","1", MainModel.IniPath);

                pnlAutoPrint.Visible = false;
        }
        //关闭自动加购
        private void pnlCloseAutoCart_Click(object sender, EventArgs e)
        {
            

                MainModel.WhetherAutoCart = false;
                picOpenAutoCart.BackgroundImage = picNotSelect.Image;
                picCloseAutoCart.BackgroundImage = picSelect.Image;

                INIManager.SetIni("CashierSet", "WhetherAutoCart", "0", MainModel.IniPath);

                if (MainModel.WhetherPrint)
                {
                    pnlAutoPrint.Visible = true;
                }
            
        }
        //开启打码
        private void pnlOpenPrint_Click(object sender, EventArgs e)
        {

                MainModel.WhetherPrint = true;
                picOpenPrint.BackgroundImage = picSelect.Image;
                picClosePrint.BackgroundImage = picNotSelect.Image;

                INIManager.SetIni("CashierSet", "WhetherPrint", "1", MainModel.IniPath);

                if (!MainModel.WhetherAutoCart)
                {
                    pnlAutoPrint.Visible = true;
                }
        }
        //关闭打码
        private void pnlClosePrint_Click(object sender, EventArgs e)
        {           
                MainModel.WhetherPrint = false;
                picOpenPrint.BackgroundImage = picNotSelect.Image;
                picClosePrint.BackgroundImage = picSelect.Image;

                INIManager.SetIni("CashierSet", "WhetherPrint", "0", MainModel.IniPath);


                pnlAutoPrint.Visible = false;
            
        }
        //开启自动打码
        private void pnlOpenAutoPrint_Click(object sender, EventArgs e)
        {           
                MainModel.WhetherAutoPrint = true;
                picOpenAutoPrint.BackgroundImage = picSelect.Image;
                picCloseAutoPrint.BackgroundImage = picNotSelect.Image;

                INIManager.SetIni("CashierSet", "WhetherAutoPrint", "1", MainModel.IniPath);


              
        }
        //关闭自动打码
        private void pnlCloseAutoPrint_Click(object sender, EventArgs e)
        {
           
                MainModel.WhetherAutoPrint = false;
                picOpenAutoPrint.BackgroundImage = picNotSelect.Image;
                picCloseAutoPrint.BackgroundImage = picSelect.Image;

                INIManager.SetIni("CashierSet", "WhetherAutoPrint", "0", MainModel.IniPath);
   
        }

        #endregion

        #region 电视屏地址
        private void LoadTvIp()
        {
            try
            {
                 IPAddress iip = Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault(a => a.AddressFamily.ToString().Equals("InterNetwork"));

                 string ipAddress = iip.ToString();

                 lblTvIp1.Text = ipAddress + ":8080" + "/static/html/tvscreen1";
                 lblTvIp2.Text = ipAddress + ":8080" + "/static/html/tvscreen2";
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("获取电视屏IP地址异常"+ex.Message,true);
            }
        }

        #endregion

        #region  打印机设置

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
                    LabelPrintName = INIManager.GetIni("Print", "LabelPrintName", MainModel.IniPath);
                }
                catch
                {

                }

                bool ExitsPrinter = false;
                bool ExitsLabelPrinter = false;
                cbxPrint.Items.Clear();
                cbxLabelPrint.Items.Clear();
                //获取安装的打印机列表，并选中默认打印机
                foreach (string print in PrinterSettings.InstalledPrinters)
                {
                    cbxPrint.Items.Add(print);
                    cbxLabelPrint.Items.Add(print);
                    if (print == PrintName)
                    {
                        ExitsPrinter = true;
                    }

                    if (print == LabelPrintName)
                    {
                        ExitsLabelPrinter = true;
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

                if (ExitsLabelPrinter)
                {
                    cbxLabelPrint.SelectedItem = LabelPrintName;
                }
                else
                {
                    INIManager.SetIni("Print", "LabelPrintName", "", MainModel.IniPath);
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdatePrint();
        }


        private void cbxPrint_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsLoadSuccess)
            {
                return;
            }
            try
            {
                INIManager.SetIni("Print", "PrintName", cbxPrint.Text, MainModel.IniPath);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("选择小票打印机异常" + ex.Message);
            }
        }

        private void cbxLabelPrint_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsLoadSuccess)
            {
                return;
            }
            try
            {
                INIManager.SetIni("Print", "LabelPrintName", cbxLabelPrint.Text, MainModel.IniPath);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("选择标签打印机异常" + ex.Message);
            }
        }


        private void txtPaperSize_Leave(object sender, EventArgs e)
        {
            if (!IsLoadSuccess)
            {
                return;
            }
            try
            {
                int pagesize = 58;

                if (int.TryParse(txtPaperSize.Text, out pagesize))
                {
                    INIManager.SetIni("Print", "PageSize", txtPaperSize.Text, MainModel.IniPath);
                }
                else
                {
                    MainModel.ShowLog("请输入正确的纸张尺寸", false);
                }

                txt_Leave(null,null);
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("保存纸张尺寸异常" + ex.Message, true);
            }
        }

        private void txtLocationX_Leave(object sender, EventArgs e)
        {
            if (!IsLoadSuccess)
            {
                return;
            }
            try
            {
                int LocationX = 0;

                if (int.TryParse(txtLocationX.Text, out LocationX))
                {
                    INIManager.SetIni("Print", "LocationX", txtLocationX.Text, MainModel.IniPath);
                }
                else
                {
                    MainModel.ShowLog("请输入正确的偏移量", false);
                }
                txt_Leave(null, null);
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("保存偏移量异常" + ex.Message, true);
            }
        }


        private void linklblPrint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {

                PrintDialog dlg = new PrintDialog();
                dlg.ShowDialog();
            }
            catch { }
        }
        #endregion

        #region 电子秤设置
        private void LoadScaleSet()
        {
            try
            {
                string ComNo = INIManager.GetIni("Scale", "ComNo", MainModel.IniPath);
                string Baud = INIManager.GetIni("Scale", "Baud", MainModel.IniPath);
                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);



                cbxScaleName.Text = ScaleName;
                cbxComNo.Text = ComNo;
                cbxBaud.Text = Baud;

                pnlYKPrint.Visible = ScaleName == ScaleType.爱宝.ToString() || ScaleName == ScaleType.易衡.ToString(); ;

                int printcomno = ZhuiZhi_Integral_Scale_UncleFruit.PrintFactory.YKPrintUtil.GetYKComNo();
                string printbaud = ZhuiZhi_Integral_Scale_UncleFruit.PrintFactory.YKPrintUtil.GetYKBaud().ToString();

                cbxPrintComNo.SelectedIndex = printcomno - 1;
                cbxPrintBaud.Text = printbaud;

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载电子秤设置异常"+ex.Message,true);
            }
        }

        private void cbxScaleName_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!IsLoadSuccess)
            {
                return;
            }
            string scalename = cbxScaleName.SelectedItem.ToString();
            INIManager.SetIni("Scale", "ScaleName", scalename, MainModel.IniPath);
            pnlYKPrint.Visible = scalename == ScaleType.爱宝.ToString() || scalename == ScaleType.易衡.ToString(); ;
            //中科英泰 S373电子秤端口COM6 波特率9600
            if (scalename == ScaleType.中科英泰.ToString())
            {

                string comno = "COM6";
                int baud = 9600;

                cbxComNo.SelectedItem = comno;
                cbxBaud.SelectedItem = baud.ToString();

                UpdateXml();

                ScaleFactory.ScaleGlobalHelper.IniScale(true);
                ScaleFactory.ScaleGlobalHelper.Open(comno, baud);
                //ScaleFactory.ScaleFactory.Close();

            }
                //爱宝 电子秤端口COM2  波特率9600
            else if (scalename == ScaleType.爱宝.ToString())
            {
                string comno = "COM2";
                int baud = 9600;

                cbxComNo.SelectedItem = comno;
                cbxBaud.SelectedItem = baud.ToString();

                cbxPrintComNo.SelectedIndex = 2;

                ScaleFactory.ScaleGlobalHelper.IniScale(true);
                ScaleFactory.ScaleGlobalHelper.Open(comno, baud);

            }
            else if(scalename==ScaleType.易捷通.ToString())
            {
                string comno = "COM1";
                int baud = 9600;

                cbxComNo.SelectedItem = comno;
                cbxBaud.SelectedItem = baud.ToString();

                ScaleFactory.ScaleGlobalHelper.IniScale(true);
                ScaleFactory.ScaleGlobalHelper.Open(comno, baud);
            }
            else if (scalename == ScaleType.托利多.ToString())
            {
                string comno = "COM1";
                int baud = 9600;

                cbxComNo.SelectedItem = comno;
                cbxBaud.SelectedItem = baud.ToString();

                ScaleFactory.ScaleGlobalHelper.IniScale(true);
                ScaleFactory.ScaleGlobalHelper.Open(comno, baud);
            }
             else if (scalename == ScaleType.易衡.ToString())
            {
                string comno = "COM4";
                int baud = 19200;

                cbxComNo.SelectedItem = comno;
                cbxBaud.SelectedItem = baud.ToString();

                ScaleFactory.ScaleGlobalHelper.IniScale(true);
                ScaleFactory.ScaleGlobalHelper.Open(comno, baud);
            }
        }

        private void cbxComNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoadSuccess)
                {
                    return;
                }
                INIManager.SetIni("Scale", "ComNo", cbxComNo.Text, MainModel.IniPath);

                string scalename = cbxScaleName.SelectedItem.ToString();
                if (scalename == ScaleType.中科英泰.ToString())
                {
                    UpdateXml();
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("修改端口号异常"+ex.Message,true);
            }
        }

        private void cbxBaud_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoadSuccess)
                {
                    return;
                }
                INIManager.SetIni("Scale", "Baud", cbxBaud.Text, MainModel.IniPath);

                string scalename = cbxScaleName.SelectedItem.ToString();
                if (scalename == ScaleType.中科英泰.ToString())
                {
                    UpdateXml();
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("修改波特率异常"+ex.Message ,true);
            }
        }

        /// <summary>
        /// 保存中科英泰配置   pos_ad_dll.dll自动识别WintecScalePOS.xml 参数， api传参无效
        /// </summary>
        private void UpdateXml()
        {
            try
            {
                string xmlpath = AppDomain.CurrentDomain.BaseDirectory + "WintecScalePOS.xml";

                XmlDocument doc = new XmlDocument();

                doc.Load(xmlpath);

                XmlNode scaleposxn = doc.SelectSingleNode("ScalePOS");

                XmlNode modelxn = scaleposxn.SelectSingleNode("Model");
                modelxn.InnerText = "S373";

                XmlNode paraxn = scaleposxn.SelectSingleNode("Parameters");
                XmlNode scalexn = paraxn.SelectSingleNode("Scale");
                XmlNode portxn = scalexn.SelectSingleNode("PortName");
                XmlNode baudxn = scalexn.SelectSingleNode("BaudRate");
                portxn.InnerText = cbxComNo.SelectedItem.ToString();
                baudxn.InnerText = cbxBaud.SelectedItem.ToString();

                doc.Save(xmlpath);

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("更新电子秤配置文件异常"+ex.StackTrace,true);
            }
        }

        private void cbxPrintComNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string printcom = (cbxPrintComNo.SelectedIndex + 1).ToString();
                INIManager.SetIni("Scale", "PrintComNo", printcom, MainModel.IniPath);
            }
            catch
            {

            }

        }

        private void cbxPrintBaud_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string baud = cbxPrintBaud.Text;
                INIManager.SetIni("Scale", "PrintBaud", baud, MainModel.IniPath);

            }
            catch { }
        }
        #endregion

   


        private void txt_Enter(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                GlobalUtil.ShowKeyBoard(this); //GlobalUtil.OpenOSK();
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

        #region 在线模式下 半离线设置

        private void LoadHalfOffLineIni()
        {

            string WhetherHalfOffLine = INIManager.GetIni("System", "WhetherHalfOffLine", MainModel.IniPath);


            if (WhetherHalfOffLine == "1")
            {
                MainModel.WhetherHalfOffLine = true;
                picOpenHalfOffLine.BackgroundImage = picSelect.Image;
                picCloseHalfOffLine.BackgroundImage = picNotSelect.Image;


            }
            else
            {
                MainModel.WhetherHalfOffLine = false;
                picOpenHalfOffLine.BackgroundImage = picNotSelect.Image;
                picCloseHalfOffLine.BackgroundImage = picSelect.Image;

            }
        }

        private void pnlOpenHalfOffLine_Click(object sender, EventArgs e)
        {
            MainModel.WhetherHalfOffLine = true;
            picOpenHalfOffLine.BackgroundImage = picSelect.Image;
            picCloseHalfOffLine.BackgroundImage = picNotSelect.Image;

            INIManager.SetIni("System", "WhetherHalfOffLine", "1", MainModel.IniPath);
        }

        private void pnlCloseHalfOffLine_Click(object sender, EventArgs e)
        {
            MainModel.WhetherHalfOffLine = false;
            picOpenHalfOffLine.BackgroundImage = picNotSelect.Image;
            picCloseHalfOffLine.BackgroundImage = picSelect.Image;

            INIManager.SetIni("System", "WhetherHalfOffLine", "0", MainModel.IniPath);

        }
        #endregion


        #region 面板商品重量显示是否为：斤

        private void LoadShowWithJinIni()
        {

            string WhetherShowWithJin = INIManager.GetIni("System", "WhetherShowWithJin", MainModel.IniPath);


            if (WhetherShowWithJin == "1")
            {
                MainModel.WhetherShowWithJin = true;
                picOpenShowWithJin.BackgroundImage = picSelect.Image;
                picCloseShowWithJin.BackgroundImage = picNotSelect.Image;


            }
            else
            {
                MainModel.WhetherShowWithJin = false;
                picOpenShowWithJin.BackgroundImage = picNotSelect.Image;
                picCloseShowWithJin.BackgroundImage = picSelect.Image;

            }
        }
        private void pnlOpenShowWithJin_Click(object sender, EventArgs e)
        {
            MainModel.WhetherShowWithJin = true;
            picOpenShowWithJin.BackgroundImage = picSelect.Image;
            picCloseShowWithJin.BackgroundImage = picNotSelect.Image;
            INIManager.SetIni("System", "WhetherShowWithJin", "1", MainModel.IniPath);
        }

        private void pnlCloseShowWithJin_Click(object sender, EventArgs e)
        {
            MainModel.WhetherShowWithJin = false;
            picOpenShowWithJin.BackgroundImage = picNotSelect.Image;
            picCloseShowWithJin.BackgroundImage = picSelect.Image;
            INIManager.SetIni("System", "WhetherShowWithJin", "0", MainModel.IniPath);
        }

        #endregion




    }

}

