using Maticsoft.BLL;
using Maticsoft.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Resources;
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
using ZhuiZhi_Integral_Scale_UncleFruit.ScaleUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class frmScale : Form
    {
        /// <summary>
        /// 电子秤表操作
        /// </summary>
       // private DBSWITCH_KEY_BEANBLL scalebll = new DBSWITCH_KEY_BEANBLL();
        private DBSCALE_KEY_BEANBLL scalebll = new DBSCALE_KEY_BEANBLL();
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

        /// <summary>
        /// 电子秤操作类
        /// </summary>
        private ScaleUtil scaleutil = new ScaleUtil();

        Bitmap bmpSendSccleSuccess;
        Bitmap bmpSendScaleFailed;
        Bitmap bmpSendScale;

        /// <summary>
        /// this.enable=false; 页面不可用页面不可控；  通过该标志控制页面是否可用
        /// </summary>
        private bool IsEnable = true;

        public frmScale()
        {
            InitializeComponent();

            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void frmScale_Shown(object sender, EventArgs e)
        {
            lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;
            lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
            picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
            lblMenu.Left = picMenu.Right;
            

            bmpSendScale = (Bitmap)MainModel.GetControlImage(btnSendScale);

            bmpSendSccleSuccess = (Bitmap)MainModel.GetControlImage(btnSuccess);

            bmpSendScaleFailed = (Bitmap)MainModel.GetControlImage(btnFaile);



            //Thread threadLoadProdcut = new Thread(LoadScale);
            //threadLoadProdcut.IsBackground = true;
            //threadLoadProdcut.Start();

            CurrentPage = 1;
            LoadDgvScale(true);


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
                    List<DBSCALE_KEY_BEANMODEL> lstScale = scalebll.GetModelList(" IP ='" + lstScaleIP[i] + "' and CREATE_URL_IP ='"+MainModel.URL+"'");
                    if (lstScale != null && lstScale.Count > 0)
                    {

                        DBTRANSFER_SCALE_STATUS_BEANMODEL scalestatusmodel = scalestatusbll.GetModelByScaleIp(lstScaleIP[i]);
                        if (scalestatusmodel != null && scalestatusmodel.STATUS==0)
                        {
                            dgvScale.Rows.Add((i + 1).ToString().PadLeft(2, '0'), lstScale[0].TEMPNAME, lstScale[0].IP, lstScale[0].SCALESTYPE, MainModel.GetDateTimeByStamp(scalestatusmodel.SYS_ERROR_TIME.ToString()).ToString("yyyy-MM-dd HH:mm"),bmpSendScaleFailed, bmpSendScale);

                            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
                            dataGridViewCellStyle1.ForeColor = Color.OrangeRed;
                            dgvScale.Rows[dgvScale.Rows.Count - 1].DefaultCellStyle = dataGridViewCellStyle1;

                        }
                        else if (scalestatusmodel != null && scalestatusmodel.STATUS == 1)
                        {
                            dgvScale.Rows.Add((i + 1).ToString().PadLeft(2, '0'), lstScale[0].TEMPNAME, lstScale[0].IP, lstScale[0].SCALESTYPE, MainModel.GetDateTimeByStamp(scalestatusmodel.SYS_SUCCESS_TIME.ToString()).ToString("yyyy-MM-dd HH:mm"), bmpSendSccleSuccess, bmpSendScale);

                            //System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
                           // dataGridViewCellStyle1.ForeColor = Color.OrangeRed;
                           // dgvScale.Rows[dgvScale.Rows.Count - 1].DefaultCellStyle = dataGridViewCellStyle1;
                        }
                        else
                        {
                            dgvScale.Rows.Add((i + 1).ToString().PadLeft(2, '0'), lstScale[0].TEMPNAME, lstScale[0].IP, lstScale[0].SCALESTYPE, "-", ResourcePos.White, bmpSendScale);
                        }
                    }
                }
               
                }));
            }
            catch (Exception ex)
            {
                //ShowLog("加载电子秤信息异常" + ex.Message, true);
            }
        }

        private void btnExits_Click(object sender, EventArgs e)
        {
            //if (!IsEnable)
            //{
            //    return;
            //}

            try
            {
                try
                {
                    if (frmsending != null)
                    {
                        frmsending.Close();
                        frmsending.Dispose();
                    }
                }
                catch { }
                this.Close();
                this.Dispose();
                
            }
            catch (Exception ex)
            {

            }
          
        }

        private void btnTransferAll_Click(object sender, EventArgs e)
        {
            if (!IsEnable)
            {
                return;
            }
            if (dgvScale.Rows.Count <= 0)
            {
                return;
            }
              List<string> scaleips = new List<string>();
              foreach (DataGridViewRow dr in dgvScale.Rows)
              {
                  scaleips.Add(dr.Cells["scaleip"].Value.ToString());
              }


             // SendScaleByScaleIp(scaleips);
              ParameterizedThreadStart Pts = new ParameterizedThreadStart(SendScaleByScaleIp);
              Thread thread = new Thread(Pts);
              thread.IsBackground = true;
              thread.Start(scaleips);
        }

        private void dgvScale_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (!IsEnable)
                {
                    return;
                }
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
                ShowLog("传秤出现异常：" + ex.Message, true);
            }
        }
        //Scale_Toledo scaletoledo = new Scale_Toledo();
        private void SendScaleByScaleIp(object scaleips)
        {
            try
            {
                this.Activate();
                IsEnable = false;
                List<string> lstscaleip = (List<string>)scaleips;

                List<string> lstSendPLUStr = new List<string>(); //PLU主档
                List<string> lstSendKSAStr = new List<string>(); //快捷键

                foreach (string ScaleIP in lstscaleip)
                {

                    List<DBSCALE_KEY_BEANMODEL> lstscale = scalebll.GetModelList(" IP='" + ScaleIP + "' and CREATE_URL_IP ='"+MainModel.URL+"'");
                    if (lstscale == null || lstscale.Count <= 0)
                    {
                        ShowLog(ScaleIP +"秤无商品信息",false);
                        continue;
                        //return;
                    }
                    string scaletype = lstscale[0].SCALESTYPE;
                    //LoadingHelper.ShowLoadingScreen(lstscale[0].TEMPNAME+"|"+"传秤数据下发中");
                  

                    if (scaletype != "SM-110")
                    {
                        ShowLog("暂未匹配秤：" + lstscale[0].TEMPNAME,false);
                        continue;
                        //return; 
                    }

                    ShowSending(lstscale[0].TEMPNAME + "|" + "传秤数据下发中");

                    //ParameterizedThreadStart Pts = new ParameterizedThreadStart(ShowSending);
                    //Thread thread = new Thread(Pts);
                    //thread.IsBackground = true;
                    //thread.Start(lstscale[0].TEMPNAME + "|" + "传秤数据下发中");


                    string errormsg ="";
                    bool SendScaleResult = ScaleUtil.SendScaleByScaleIp(ScaleIP,ref errormsg);//scaletoledo.SendToledoData(ScaleIP,"3001",out errormsg);
                    ClsoeSending();
                   
                    if (SendScaleResult)
                    {
                       
                        LogManager.WriteLog(ScaleIP+"传秤完成");
                        if (lstscaleip.Count == 1 && !this.IsDisposed)  ///==1 代表是单个传秤 显示传秤状态   否则为一键传秤 只更新表信息不弹窗
                        {
                            LoadPicScreen(true);
                            
                           // LoadingHelper.CloseForm();
                            frmScaleResult frmscaleresult = new frmScaleResult(true, lstscale[0].TEMPNAME,"下发成功");
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
                        Application.DoEvents();
                        LogManager.WriteLog(ScaleIP + "传秤失败");
                        frmScaleResult frmscaleresult = new frmScaleResult(false, lstscale[0].TEMPNAME, errormsg);
                        if (lstscaleip.Count == 1 && !this.IsDisposed)  ///==1 代表是单个传秤 显示传秤状态   否则为一键传秤 只更新表信息不弹窗
                        {
                            LoadPicScreen(true);
                           // LoadingHelper.CloseForm();
                            frmscaleresult.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmscaleresult.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmscaleresult.Height) / 2);
                            frmscaleresult.TopMost = true;
                            frmscaleresult.ShowDialog();

                            LoadPicScreen(false);
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
                        if (lstscaleip.Count == 1 && !this.IsDisposed)  ///==1 代表是单个传秤 显示传秤状态   否则为一键传秤 只更新表信息不弹窗
                        {
                            if (frmscaleresult.DialogResult == DialogResult.Retry)
                            {
                                
                                SendScaleByScaleIp(scaleips);
                                continue;
                                //return;
                            }
                        }
                    }


                }

                CurrentPage = 1;
                LoadDgvScale(true);
                //LoadScale();
                LoadPicScreen(false);
                Application.DoEvents();
            }
            catch (Exception ex)
            {

                  
                   LoadPicScreen(false);
                ShowLog("传秤异常" + ex.Message, true);
            }
            finally
            {
                ClsoeSending();
                    LoadPicScreen(false);
                    IsEnable = true;
                    Application.DoEvents();

            }
        }

        //Win+D    页面FormBoardStyle  属性不能为none 否则返回windows页面只要有焦点事件就会打开程序

        [DllImport("User32.dll")]
        public static extern void keybd_event(Byte bVk, Byte bScan, Int32 dwFlags, Int32 dwExtraInfo);

        public void btnWindows_Click(object sender, EventArgs e)
        {
            try
            {

                if (!IsEnable)
                {
                    return;
                }

                MainModel.ShowTask();
                MainModel.ShowWindows();
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


        private void LoadPicScreen(bool isShown)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    if (isShown)
                    {
                        if (!picScreen.Visible)
                        {
                            picScreen.BackgroundImage = MainModel.GetWinformImage(this);
                            picScreen.Size = new System.Drawing.Size(this.Width, this.Height);
                            picScreen.Visible = true;
                        }
                    }
                    else
                    {
                        picScreen.Visible = false;
                    }

                    Application.DoEvents();
                }));
            }
            catch (Exception ex)
            {
                picScreen.Visible = false;
                LogManager.WriteLog("修改电子秤背景图异常：" + ex.Message);
            }
        }


        private void ShowLog(string msg,bool iserror)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    MainModel.ShowLog(msg, iserror);
                }
               
            }
            catch { }
        }

        private FormSending frmsending = null;
        private void ShowSending(object msg)
        {
            try
            {


                this.Invoke(new Action(() =>
                {
                    if (frmsending != null)
                    {
                        frmsending.Close();
                    }

                    frmsending = new FormSending(msg.ToString());
                    frmsending.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmsending.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmsending.Height) / 2);
                    frmsending.TopMost = true;
                    frmsending.Show();
                }));

            }
            catch (Exception ex)
            {
            }
        }
        private void ClsoeSending()
        {
            try
            {

                this.Invoke(new Action(() =>
                {
                    frmsending.Close();
                    frmsending.Dispose();
                }));

                //pnlSending.Visible = false;
                //frmsending.Close();
                //frmsending.Dispose();
            }
            catch(Exception ex)
            {
               // ShowLog("关闭发送中异常" + ex.Message, true);
            }
        }

        #region 分页
        private int CurrentPage = 1;

        private List<DBSCALE_KEY_BEANMODEL> CurrentLstScale = new List<DBSCALE_KEY_BEANMODEL>();
        private void rbtnPageUp_ButtonClick(object sender, EventArgs e)
        {
            if (!rbtnPageUp.WhetherEnable)
            {
                return;
            }
            CurrentPage--;
            LoadDgvScale(false);
        }

        private void rbtnPageDown_ButtonClick(object sender, EventArgs e)
        {
            if (!rbtnPageDown.WhetherEnable)
            {
                return;
            }
            CurrentPage++;
            LoadDgvScale(false);
        }


       /// <summary>
       /// 分页加载电子秤列表
       /// </summary>
       /// <param name="needRefresh">是否需要刷新</param>
        private void LoadDgvScale(bool needRefresh)
        {
            try
            {
                if (needRefresh || CurrentLstScale==null || CurrentLstScale.Count==0)
                {
                    CurrentLstScale = scalebll.GetModelList("_id in(select max(_id) from DBSCALE_KEY_BEAN group by ip)");
                }
                    dgvScale.Rows.Clear();
                
                    if (CurrentLstScale == null || CurrentLstScale.Count == 0)
                    {
                        return;
                    }

                    if (CurrentPage > 1)
                    {
                        rbtnPageUp.WhetherEnable = true;
                    }
                    else
                    {
                        rbtnPageUp.WhetherEnable = false;
                    }
                    int startindex = (CurrentPage - 1) * 10;

                    int lastindex = Math.Min(CurrentLstScale.Count - 1, startindex + 11);

                    List<DBSCALE_KEY_BEANMODEL> LstTempScale = CurrentLstScale.GetRange(startindex,lastindex-startindex+1);

                    int rownum = 0;
                    foreach (DBSCALE_KEY_BEANMODEL scalemodel in LstTempScale)
                    {
                        rownum++;
                        DBTRANSFER_SCALE_STATUS_BEANMODEL scalestatusmodel = scalestatusbll.GetModelByScaleIp(scalemodel.IP);
                        if (scalestatusmodel != null && scalestatusmodel.STATUS==0)
                        {
                            dgvScale.Rows.Add(rownum.ToString(), scalemodel.TEMPNAME, scalemodel.IP, scalemodel.SCALESTYPE, MainModel.GetDateTimeByStamp(scalestatusmodel.SYS_ERROR_TIME.ToString()).ToString("yyyy-MM-dd HH:mm"), bmpSendScaleFailed, bmpSendScale);

                            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
                            dataGridViewCellStyle1.ForeColor = Color.OrangeRed;
                            dgvScale.Rows[dgvScale.Rows.Count - 1].DefaultCellStyle = dataGridViewCellStyle1;

                        }
                        else if (scalestatusmodel != null && scalestatusmodel.STATUS == 1)
                        {
                            dgvScale.Rows.Add(rownum.ToString(), scalemodel.TEMPNAME, scalemodel.IP, scalemodel.SCALESTYPE, MainModel.GetDateTimeByStamp(scalestatusmodel.SYS_SUCCESS_TIME.ToString()).ToString("yyyy-MM-dd HH:mm"), bmpSendSccleSuccess, bmpSendScale);

                        }
                        else
                        {
                            dgvScale.Rows.Add(rownum.ToString(), scalemodel.TEMPNAME, scalemodel.IP, scalemodel.SCALESTYPE, "-", ResourcePos.White, bmpSendScale);
                        }
                    }


                if (dgvScale.Rows.Count > 0)
                {
                    //pnlEmptyReceipt.Visible = false;
                    MainModel.ShowLog("刷新完成", false);
                }
                else
                {
                    //pnlEmptyReceipt.Visible = true;
                    MainModel.ShowLog("暂无数据", false);
                }
                Application.DoEvents();

                //在线接口每页20个 防止本地分页和接口分页最小积数  例 6*10 = 20*3
                if (CurrentLstScale.Count > CurrentPage * 10)
                {
                    rbtnPageDown.WhetherEnable = true;
                }
                else
                {
                    rbtnPageDown.WhetherEnable = false;
                }



            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载订单列表异常" + ex.Message, true);
            }
        }

        #endregion
    }
}
