using Maticsoft.BLL;
using Maticsoft.Model;
using QDAMAPOS.Common;
using QDAMAPOS.Model;
using QDAMAPOS.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QDAMAPOS
{
    public partial class frmScale : Form
    {
        /// <summary>
        /// 电子秤表操作
        /// </summary>
        private DBSWITCH_KEY_BEANBLL scalebll = new DBSWITCH_KEY_BEANBLL();

        /// <summary>
        /// 电子秤更新状态表操作类
        /// </summary>
        private DBTRANSFER_SCALE_STATUS_BEANBLL scalestatusbll = new DBTRANSFER_SCALE_STATUS_BEANBLL();

        /// <summary>
        /// 产品表操作类
        /// </summary>
        private DBPRODUCT_BEANBLL productbll = new DBPRODUCT_BEANBLL();

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        Bitmap bmpSendSccleSuccess;
        Bitmap bmpSendScaleFailed;
        Bitmap bmpSendScale;

        public frmScale()
        {
            InitializeComponent();
        }

        private void frmScale_Shown(object sender, EventArgs e)
        {
            lblShopName.Text = MainModel.CurrentShopInfo.shopname;
            btnOnLineType.Left = lblShopName.Left + lblShopName.Width + 10;
            if (MainModel.IsOffLine)
            {
                btnOnLineType.BackgroundImage = Resources.ResourcePos.OffLineType; btnOnLineType.Text = "   离线"; 
            }
            else
            {
                btnOnLineType.BackgroundImage = Resources.ResourcePos.OnLineType; btnOnLineType.Text = "   在线"; 
            }
            btnMenu.Text = MainModel.CurrentUser.nickname + "，你好  ";
            timerNow.Interval = 1000;
            timerNow.Enabled = true;



            //bmpSendScale = new Bitmap(picSendScale.Image, dgvScale.Columns["operation"].Width * 80 / 100, dgvScale.RowTemplate.Height*80/100);

            //bmpSendSccleSuccess = new Bitmap(picScaleSuccess.Image, dgvScale.Columns["ScaleStatus"].Width * 60 / 100, dgvScale.RowTemplate.Height * 50 / 100);

            //bmpSendScaleFailed = new Bitmap(picScaleFaild.Image, dgvScale.Columns["ScaleStatus"].Width * 60 / 100, dgvScale.RowTemplate.Height * 50 / 100);



            bmpSendScale = (Bitmap)MainModel.GetControlImage(btnSendScale);

            bmpSendSccleSuccess = (Bitmap)MainModel.GetControlImage(btnSuccess);

            bmpSendScaleFailed = (Bitmap)MainModel.GetControlImage(btnFaile);



            Thread threadLoadProdcut = new Thread(LoadScale);
            threadLoadProdcut.IsBackground = true;
            threadLoadProdcut.Start();
        }

        private void frmScale_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void LoadScale()
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {

                    List<string> lstScaleIP = scalebll.GetDiatinctByScaleIP(" CREATE_URL_IP ='"+MainModel.URL+"'");

                dgvScale.Rows.Clear();
                for (int i = 0; i < lstScaleIP.Count; i++)
                {
                    List<DBSWITCH_KEY_BEANMODEL> lstScale = scalebll.GetModelList(" SCALEIP ='" + lstScaleIP[i] + "' and CREATE_URL_IP ='"+MainModel.URL+"'");
                    if (lstScale != null && lstScale.Count > 0)
                    {

                        DBTRANSFER_SCALE_STATUS_BEANMODEL scalestatusmodel = scalestatusbll.GetModelByScaleIp(lstScaleIP[i]);
                        if (scalestatusmodel != null && scalestatusmodel.STATUS==0)
                        {
                            dgvScale.Rows.Add((i + 1).ToString().PadLeft(2, '0'), lstScale[0].KEYPLANNAME, lstScale[0].SCALEIP, lstScale[0].SCALETYPENAME, MainModel.GetDateTimeByStamp(scalestatusmodel.SYS_ERROR_TIME.ToString()).ToString("yyyy-MM-dd HH:mm"),bmpSendScaleFailed, bmpSendScale);

                            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
                            dataGridViewCellStyle1.ForeColor = Color.OrangeRed;
                            dgvScale.Rows[dgvScale.Rows.Count - 1].DefaultCellStyle = dataGridViewCellStyle1;

                        }
                        else if (scalestatusmodel != null && scalestatusmodel.STATUS == 1)
                        {
                            dgvScale.Rows.Add((i + 1).ToString().PadLeft(2, '0'), lstScale[0].KEYPLANNAME, lstScale[0].SCALEIP, lstScale[0].SCALETYPENAME, MainModel.GetDateTimeByStamp(scalestatusmodel.SYS_SUCCESS_TIME.ToString()).ToString("yyyy-MM-dd HH:mm"), bmpSendSccleSuccess, bmpSendScale);

                            //System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
                           // dataGridViewCellStyle1.ForeColor = Color.OrangeRed;
                           // dgvScale.Rows[dgvScale.Rows.Count - 1].DefaultCellStyle = dataGridViewCellStyle1;
                        }
                        else
                        {
                            dgvScale.Rows.Add((i + 1).ToString().PadLeft(2, '0'), lstScale[0].KEYPLANNAME, lstScale[0].SCALEIP, lstScale[0].SCALETYPENAME, "-", ResourcePos.White, bmpSendScale);
                        }
                    }

                }
               
                }));
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载电子秤信息异常" + ex.Message, true);
            }
        }

        private void btnExits_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void btnTransferAll_Click(object sender, EventArgs e)
        {

            if (dgvScale.Rows.Count <= 0)
            {
                return;
            }
              List<string> scaleips = new List<string>();
              foreach (DataGridViewRow dr in dgvScale.Rows)
              {
                  scaleips.Add(dr.Cells["scaleip"].Value.ToString());
              }         

            ParameterizedThreadStart Pts = new ParameterizedThreadStart(SendScaleByScaleIp);
            Thread thread = new Thread(Pts);
            thread.IsBackground = true;
            thread.Start(scaleips);
        }

        private void frmScale_EnabledChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.Enabled)
                {
                    picScreen.Visible = false;
                    //Application.DoEvents();
                }
                else
                {
                    picScreen.BackgroundImage = MainModel.GetWinformImage(this);
                    picScreen.Size = new System.Drawing.Size(this.Width, this.Height);
                    picScreen.Visible = true;
                    //Application.DoEvents();
                }

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                picScreen.Visible = false;
                LogManager.WriteLog("修改面板商品背景图异常：" + ex.Message);
            }
        }

        private void dgvScale_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;
                if (e.ColumnIndex != 6)
                {
                    return;
                }

                string scaleip = dgvScale.Rows[e.RowIndex].Cells["scaleip"].Value.ToString();
                //SendScaleByScaleIp(scaleip);
                List<string> scaleips = new List<string>();
                scaleips.Add(scaleip);                
                
                //SendScaleByScaleIp(scaleips);

                ParameterizedThreadStart Pts = new ParameterizedThreadStart(SendScaleByScaleIp);
                Thread thread = new Thread(Pts);
                thread.IsBackground = true;
                thread.Start(scaleips);
             
               // LoadScale();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("传秤出现异常：" + ex.Message, true);
            }
        }

        private void SendScaleByScaleIp(object scaleips)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
        {
            this.Enabled = false;
        }));
                List<string> lstscaleip = (List<string>)scaleips;

                List<string> lstSendPLUStr = new List<string>(); //PLU主档
                List<string> lstSendKSAStr = new List<string>(); //快捷键

                foreach (string ScaleIP in lstscaleip)
                {

                    List<DBSWITCH_KEY_BEANMODEL> lstscale = scalebll.GetModelList(" SCALEIP='" + ScaleIP + "' and CREATE_URL_IP ='"+MainModel.URL+"'");
                    if (lstscale == null || lstscale.Count <= 0)
                    {
                        MainModel.ShowLog(ScaleIP +"秤无商品信息",false);
                        return;
                    }
                    LoadingHelper.ShowLoadingScreen(lstscale[0].KEYPLANNAME+"|"+"传秤数据下发中");

                    string scalename = "sm110";
                    if (lstscale[0].SCALETYPENAME.ToUpper().Contains("SM-120"))
                    {
                        scalename = "sm120";
                    }
                    else if (lstscale[0].SCALETYPENAME.ToUpper().Contains("SM-110"))
                    {
                        scalename = "sm110";
                    }
                    else
                    {
                        MainModel.ShowLog("暂未匹配该秤" + lstscale[0].SCALETYPENAME,false);
                       
                        return ;
                    }
                    // List<DBPRODUCT_BEANMODEL> lstSendPro = new List<DBPRODUCT_BEANMODEL>();
                    foreach (DBSWITCH_KEY_BEANMODEL scale in lstscale)
                    {
                        DBPRODUCT_BEANMODEL pro = productbll.GetModelBySkuCode(scale.SKUCODE,MainModel.URL);
                        //lstSendPro.Add(pro);

                        StringBuilder sb = new StringBuilder();
                        sb.Append(pro.GOODS_ID + ",");
                        sb.Append(pro.GOODS_ID + ",");
                        sb.Append(pro.SKUNAME + ",");
                        sb.Append("" + ",");
                        sb.Append("" + ",");

                        sb.Append("" + ",");
                        sb.Append(pro.SALEPRICE + ",");

                        //csv文件 称重商品0  标品为1  与数据库字段定义相反        条码头标品固定26  称重固定25
                        if (pro.WEIGHTFLAG == 1)
                        {
                            sb.Append("0" + ",");
                            sb.Append("25" + ",");  //条码头标识？
                        }
                        else
                        {
                            sb.Append("1" + ",");
                            sb.Append("26" + ",");  //条码头标识？
                        }

                        sb.Append("25" + ",");  //条码格式编号
                        sb.Append(pro.BESTDAYS + ",");  //保质期
                        sb.Append("" + ","); //销售日期？
                        sb.Append("0" + ","); //包装日期 固定0当天
                        sb.Append(17 + ",");  //标签格式编号  ？
                        sb.Append("997" + ","); //主组号

                        if (string.IsNullOrEmpty(pro.LOCATION))
                        {
                            sb.Append("  " + ",");
                        }
                        else
                        {
                            sb.Append("产地信息:" + pro.LOCATION + ",");
                        }

                        if (string.IsNullOrEmpty(pro.SPINFO))
                        {
                            sb.Append("  " + ",");
                        }
                        else
                        {
                            sb.Append("贮存条件:" + pro.SPINFO + ",");
                        }

                        sb.Append("  " + ",");
                        sb.Append("门店:" + MainModel.CurrentShopInfo.shopname + ",");
                        sb.Append("称号:" + ScaleIP.Substring(ScaleIP.LastIndexOf(".") + 1) + ",");//秤号？


                        if (string.IsNullOrEmpty(pro.REMARK))
                        {
                            sb.Append("  " + ",");
                        }
                        else
                        {
                            sb.Append("备注:" + pro.REMARK + ",");
                        }


                        if (string.IsNullOrEmpty(pro.INGREDIENT))
                        {
                            sb.Append("  " + ",");
                        }
                        else
                        {
                            sb.Append("成分:" + pro.INGREDIENT + ",");
                        }


                        if (string.IsNullOrEmpty(pro.COMPANY))
                        {
                            sb.Append("  " + ",");
                        }
                        else
                        {
                            sb.Append("销售商:" + pro.COMPANY + ",");
                        }

                        sb.Append(" " + ",");
                        sb.Append(" " + ",");

                        sb.Append(pro.QRCODECONTENT + ",");
                        sb.Append("1" + ",");
                        sb.Append("0" + ",");
                        sb.Append("1" + ",");
                        sb.Append("EAN");   //ITF  18位   EAN13位

                        lstSendPLUStr.Add(sb.ToString());


                        if (!string.IsNullOrEmpty(scale.KEYNO))
                        {
                            if (scale.KEYNO.Length == 3)
                            {
                                lstSendKSAStr.Add(pro.GOODS_ID + "," + scale.KEYNO.Substring(1, 2) + "," + scale.KEYNO.Substring(0, 1));
                            }
                            else
                            {
                                lstSendKSAStr.Add(pro.GOODS_ID + "," + scale.KEYNO + "," + "0");
                            }
                        }
                    }

                    File.WriteAllLines(MainModel.ServerPath + "plu_import.csv", lstSendPLUStr.ToArray(), Encoding.Default);
                    File.WriteAllLines(MainModel.ServerPath + "kas_import.csv", lstSendKSAStr.ToArray(), Encoding.Default);


                    string plucmd = "digicon -P -s " + scaleip + ":" + scalename + " -m plu_template.json -i plu_import.csv";

                    bool cmdPluResult = RunCmd(plucmd);

                    //
                    string kascmd = "digicon -K -s " + scaleip + ":" + scalename + " -m kas_template.json -i kas_import.csv";
                    bool cmdKasResult = RunCmd(kascmd);
                    ////TEST
                    ////string strcmd = "digicon -P -s "+ScaleIP+":sm120"+" -m plu_template.json -i plu_import.csv";
                    //string plucmd = "digicon -P -s " + "192.168.1.9" + ":sm110" + " -m plu_template.json -i plu_import.csv";
                    //bool cmdPluResult = RunCmd(plucmd);

                    ////TEST
                    ////string strcmd = "digicon -P -s "+ScaleIP+":sm120"+" -m plu_template.json -i plu_import.csv";
                    //string kascmd = "digicon -K -s "+ "192.168.1.9" + ":sm110" +" -m kas_template.json -i kas_import.csv";
                    //bool cmdKasResult = RunCmd(kascmd);


                    if (cmdPluResult && cmdKasResult)
                    {
                       
                        LogManager.WriteLog(ScaleIP+"传秤完成");
                        if (lstscaleip.Count == 1)  ///==1 代表是单个传秤 显示传秤状态   否则为一键传秤 只更新表信息不弹窗
                        {
                            LoadingHelper.CloseForm();
                            frmScaleResult frmscaleresult = new frmScaleResult(true, lstscale[0].KEYPLANNAME,"下发成功");
                        frmscaleresult.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmscaleresult.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmscaleresult.Height) / 2);
                        frmscaleresult.TopMost = true;
                        frmscaleresult.ShowDialog();
                        }

                        DBTRANSFER_SCALE_STATUS_BEANMODEL scalestatusmodel = new DBTRANSFER_SCALE_STATUS_BEANMODEL();
                        scalestatusmodel.SYS_SUCCESS_TIME =Convert.ToInt64( MainModel.getStampByDateTime(DateTime.Now));
                        scalestatusmodel.STATUS = 1;
                        scalestatusmodel.SCALEIP = ScaleIP;
                        scalestatusmodel.CREATE_URL_IP = MainModel.URL;

                        if (scalestatusbll.ExistsByScaleIp(ScaleIP))
                        {
                            scalestatusbll.UpdateByScaleIp(scalestatusmodel);
                        }
                        else
                        {
                            scalestatusbll.Add(scalestatusmodel);
                        }
                        //success
                    }
                    else
                    {
                        
                        LogManager.WriteLog(ScaleIP + "传秤失败");
                        frmScaleResult frmscaleresult = new frmScaleResult(false, lstscale[0].KEYPLANNAME,"下发失败");
                        if (lstscaleip.Count == 1)  ///==1 代表是单个传秤 显示传秤状态   否则为一键传秤 只更新表信息不弹窗
                        {
                            LoadingHelper.CloseForm();
                            frmscaleresult.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmscaleresult.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmscaleresult.Height) / 2);
                            frmscaleresult.TopMost = true;
                            frmscaleresult.ShowDialog();
                        }

                        DBTRANSFER_SCALE_STATUS_BEANMODEL scalestatusmodel = new DBTRANSFER_SCALE_STATUS_BEANMODEL();
                        scalestatusmodel.SYS_ERROR_TIME = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
                        scalestatusmodel.STATUS = 0;
                        scalestatusmodel.SCALEIP = ScaleIP;
                        scalestatusmodel.CREATE_URL_IP = MainModel.URL;

                        if (scalestatusbll.ExistsByScaleIp(ScaleIP))
                        {
                            scalestatusbll.UpdateByScaleIp(scalestatusmodel);
                        }
                        else
                        {
                            scalestatusbll.Add(scalestatusmodel);
                        }
                        if (lstscaleip.Count == 1)  ///==1 代表是单个传秤 显示传秤状态   否则为一键传秤 只更新表信息不弹窗
                        {
                            if (frmscaleresult.DialogResult == DialogResult.Retry)
                            {
                                SendScaleByScaleIp(scaleips);
                            }
                        }
                        //false
                    }

                    LoadingHelper.CloseForm();

                }
                LoadScale();
                this.Enabled = true;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                this.Invoke(new InvokeHandler(delegate()
               {
                   LoadingHelper.CloseForm();
                   this.Enabled = true;
               }));
                MainModel.ShowLog("传秤异常" + ex.Message, true);
            }
            finally
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    LoadingHelper.CloseForm();
                    this.Enabled = true;
                    Application.DoEvents();
                }));
            }

        }

        /// <summary>
        /// 执行CMD语句
        /// </summary>
        /// <param name="cmd">要执行的CMD命令</param>
        public bool RunCmd(string cmd)
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();
                proc.StandardInput.WriteLine(cmd);
                proc.StandardInput.WriteLine("exit");
                string outStr = proc.StandardOutput.ReadToEnd();
                proc.Close();

                if (outStr.Contains("Successfully"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //return outStr;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("发送CMD命令异常" + ex.Message, true);
                return false;
            }
        }


        //检测IP连接
        bool CheckNet()
        {
            bool var = false;

            try
            {
                string ip = "www.baidu.com";
                Ping pingSender = new Ping();

                PingOptions pingOption = new PingOptions();
                pingOption.DontFragment = true;
                string data = "0";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 500;
                PingReply reply = pingSender.Send(ip, timeout, buffer);
                if (reply.Status == IPStatus.Success)
                    var = true;
                else
                    var = false;
            }
            catch (Exception ex)
            {

                return false;
                // ShowLog("无法检测网络连接是否正常-" + ex.Message, true);
            }

            return var;
        }

        private void timerNow_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        }


        //Win+D    页面FormBoardStyle  属性不能为none 否则返回windows页面只要有焦点事件就会打开程序

        [DllImport("User32.dll")]
        public static extern void keybd_event(Byte bVk, Byte bScan, Int32 dwFlags, Int32 dwExtraInfo);

        public void btnWindows_Click(object sender, EventArgs e)
        {
            try
            {

                keybd_event(0x5b, 0, 0, 0); //0x5b是left win的代码，这一句使key按下，下一句使key释放。 
                keybd_event(68, 0, 0, 0);
                keybd_event(0x5b, 0, 0x2, 0);
                keybd_event(68, 0, 0x2, 0);
                //this.WindowState = FormWindowState.Minimized;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("最小化窗体异常" + ex.Message);
            }
        }


    }
}
