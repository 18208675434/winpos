using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinSaasPosStart
{
    public partial class FormStart : Form
    {
        /// <summary>
        /// 0:初始化下载  1：下载更新  2：不需要更新，启动posapplication
        /// </summary>
        private int StartType = 0;

        private VersionInfo CurrentVersionInfo;


        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        private string testsn = "";

        public FormStart()
        {
            InitializeComponent();
            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void FormStart_Shown(object sender, EventArgs e)
        {
            //电脑可能会有多个mac地址，取第一次获取的mac地址为准  同时同步start.exe 获取的mac
            string currentmac = "";
            try
            {
                currentmac = INIManager.GetIni("System", "DeviceSN", MainModel.StartIniPath);
            }
            catch { }
            string devicesn = MainModel.GetMacAddress(currentmac);
            txtDeviceSN.Text = devicesn;
            MainModel.DeviceSN = devicesn;

            INIManager.SetIni("System", "DeviceSN",devicesn, MainModel.StartIniPath);
            //INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);
            //启动扫描处理线程
            Thread threadItemExedate = new Thread(LoadForm);
            threadItemExedate.IsBackground = true;
            threadItemExedate.Start();
        }

        HttpUtil httputil = new HttpUtil();

        private void LoadForm()
        {
            try {
                this.Enabled = false;
              if (!File.Exists(MainModel.IniPath))//判断文件是否存在 不存在配置文件 程序需要重新下载
              {

                  LoadingHelper.ShowLoadingScreen("版本信息检查中...");
                  ShowStatus("版本信息检查中...");
                  StartType = 0;

                  MainModel.Version = "--";
                  txtDeviceSN.Text = MainModel.DeviceSN;

                  string ErrorMsg = "";
                  VersionInfo versioninfo = httputil.GetWinPosVersion(ref ErrorMsg);

                  LoadingHelper.CloseForm();
                  if (ErrorMsg != "" || versioninfo == null)
                  {
                      MessageBox.Show("检查最新版本失败"+ErrorMsg);
                      StartPOS();
                  }
                  else
                  {
                      CurrentVersionInfo = versioninfo;

                      lblVersion.Text = versioninfo.apkversiondto.version;
                      txtVersion.Text = versioninfo.apkversiondto.description;

                      UpdateFile();
                  }

              }
              else
              {

                  LoadingHelper.ShowLoadingScreen("版本信息检查中...");
                  //string CurrentVersion = "0.0.0";
                  string CurrentVersion = INIManager.GetIni("System", "Version", MainModel.IniPath);

                  string ErrorMsg = "";
                  VersionInfo versioninfo = httputil.GetWinPosVersion(ref ErrorMsg);
                  LoadingHelper.CloseForm();
                  if (ErrorMsg != "" || versioninfo == null)
                  {
                      MessageBox.Show("检查最新版本失败"+ErrorMsg);
                      StartPOS();
                  }
                  else
                  {
                      CurrentVersionInfo = versioninfo;

                      lblCurrentVersion.Text = CurrentVersion;
                      lblVersion.Text = versioninfo.apkversiondto.version;
                      txtVersion.Text = versioninfo.apkversiondto.description;

                      Version vcurrent = new Version(CurrentVersion);
                      Version vonline = new Version(versioninfo.apkversiondto.version);
                      Version vmin = new Version(versioninfo.apkversiondto.versionmin);

                     //判断是否在更新范围内
                          if (CurrentVersionInfo.testsn == null || CurrentVersionInfo.testsn.Length == 0 || CurrentVersionInfo.testsn.Contains(MainModel.DeviceSN))
                          {
                              if (vcurrent < vmin)
                              {
                                  UpdateFile();
                              }
                              else if (vcurrent >= vmin && vcurrent < vonline)
                              {
                                  //用户可选择是否更新

                                  if (MessageBox.Show("有版本可以更新，是否选择更新？", "系统提示...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                  {
                                      UpdateFile();
                                  }
                                  else
                                  {
                                      StartPOS();
                                  }
                              }
                              else
                              {
                                  StartPOS();
                              }                           
                          }
                          else
                          {
                              StartPOS();
                          }
                  }
              }
               
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新异常"+ex.Message);
                MessageBox.Show("更新异常" + ex.Message);

            }
            finally
            {
                this.Enabled = true;
                LoadingHelper.CloseForm();
            }
        }


        /// <summary>
        /// 更新程序文件
        /// </summary>
        private void UpdateFile()
        {
            try
            {
                this.Enabled = false;
                LoadingHelper.ShowLoadingScreen("系统升级中|请稍候...");
                
          //      this.Invoke(new InvokeHandler(delegate()
          //{
              string pathurl = CurrentVersionInfo.apkversiondto.url;

              ShowStatus("文件下载中...");
              Application.DoEvents();
              if (!DownLoadFile(pathurl))
              {
                  return;
              }             

              ShowStatus("文件下载完成");

              //更新文件
              string[] files = Directory.GetFiles(MainModel.ServerPath);
              List<string> lstfilename = new List<string>();
              foreach (string file in files)
              {
                  lstfilename.Add(Path.GetFileName(file));
              }


              ShowStatus("文件更新中...");
              string[] filesTemp = Directory.GetFiles(MainModel.TempFilePath);

              foreach (string file in filesTemp)
              {
                  string fileName = Path.GetFileName(file);
                  if (!files.Contains(file))
                  {
                      //配置文件已存在不更新
                      if ((fileName == "Config.ini" && lstfilename.Contains("Conofig.ini")) || (fileName == "StartConfig.ini" && lstfilename.Contains("StartConfig.ini")) || (fileName == "OffLine.db" && lstfilename.Contains("OffLine.db"))) //已存在配置文件不更新
                      {

                      }
                      else
                      {
                          //防止文件正在使用
                          try
                          {
                              File.Copy(file, MainModel.ServerPath + fileName, true);
                          }
                          catch (Exception ex)
                          {

                          }
                      }

                  }

              }

              ShowStatus("更新完成");

              INIManager.SetIni("System", "Version", lblVersion.Text, MainModel.IniPath);
              INIManager.SetIni("System", "Version", lblVersion.Text, MainModel.StartIniPath);
              StartPOS();
         // }));
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新文件异常"+ex.Message);
                ShowStatus("更新异常"+ex.Message) ;
            }
            finally
            {
                this.Enabled = true;
                LoadingHelper.CloseForm();
                
            }
        }

        /// <summary>
        /// 下载文件 并解压
        /// </summary>
        /// <param name="url"></param>
        private bool DownLoadFile(string url)
        {
            try
            {

          //      this.Invoke(new InvokeHandler(delegate()
          //{
              // string url = "http://10.208.27.113:8080/assets/apk/qdama_pos/1.2.20/debug/app-debug.apk";
              string remoteUri = System.IO.Path.GetDirectoryName(url);

              string fileName = System.IO.Path.GetFileName(url);

              string myStringWebResource = null;

              WebClient myWebClient = new WebClient();

              myStringWebResource = url;

              if (!System.IO.Directory.Exists(MainModel.TempFilePath))
              {
                  System.IO.Directory.CreateDirectory(MainModel.TempFilePath);//不存在就创建文件夹
              }

              myWebClient.DownloadFile(myStringWebResource, MainModel.ServerPath + "TempZip.zip");

              ZipFileUtil.unZipFile(MainModel.ServerPath + "TempZip.zip", MainModel.TempFilePath);
              //解压完成后删除
             // File.Delete(MainModel.ServerPath + "TempZip.zip");
              
         // }));
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("下载文件异常：" + ex.Message);
                ShowStatus("下载文件异常：" + ex.Message);                
                return false;
            }
        }


        /// <summary>
        /// 启动POS程序
        /// </summary>
        private void StartPOS()
        {
            try
            {
                ShowStatus("收银程序启动中...");
                Delay.Start(1000);
                //启用标签打印程序
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"WinSaasPOS.exe"))
                {
                    System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"WinSaasPOS.exe");
                }
                else
                {
                    MessageBox.Show("收银系统启动失败！", "system prompt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Application.DoEvents();

                LogManager.WriteLog("收银系统启动，启动程序关闭");
                System.Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("启动收银程序异常" + ex.Message);
                //ShowStatus("启动收银程序异常" + ex.Message);
                LogManager.WriteLog("ERROR","启动收银程序异常"+ex.Message);
            }
        }

        private void picExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
       

        private void ShowStatus(string msg)
        {
            //this.Invoke(new InvokeHandler(delegate()
            //{
            //lblStatus.Text = msg;
           // }));
        }

        private void btnStartPos_Click(object sender, EventArgs e)
        {
            try
            {

                LoadForm();
                //string ErrorMsg = "";
                //VersionInfo versioninfo = httputil.GetWinPosVersion(ref ErrorMsg);
                //if (ErrorMsg != "" || versioninfo == null)
                //{
                //    MessageBox.Show(ErrorMsg);
                //}
                //else
                //{
                //    CurrentVersionInfo = versioninfo;
                //    lblVersion.Text = versioninfo.apkversiondto.version;
                //    txtVersion.Text = versioninfo.apkversiondto.description;

                //    if (CurrentVersionInfo != null && (CurrentVersionInfo.testsn == null || CurrentVersionInfo.testsn.Length == 0 || CurrentVersionInfo.testsn.Contains(MainModel.DeviceSN)))
                //    {
                //        UpdateFile();

                //        Application.DoEvents();
                //        StartPOS();
                //    }
                //    else
                //    {
                //        StartPOS();
                //    }

                //}
                //Application.DoEvents();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("启动收银程序异常"+ex.Message);
               // ShowStatus("启动收银程序异常" + ex.Message);
            }

        }


        private void btnCopyCode_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(txtDeviceSN.Text);
                ParameterizedThreadStart Pts = new ParameterizedThreadStart(ShowCopyMsg);
                Thread thread = new Thread(ShowCopyMsg);
                thread.IsBackground = true;
                thread.Start("复制完成");

                ShowStatus("设备号复制完成");
            }
            catch (Exception ex)
            {
                ParameterizedThreadStart Pts = new ParameterizedThreadStart(ShowCopyMsg);
                Thread thread = new Thread(Pts);
                thread.IsBackground = true;
                thread.Start("复制失败");

                ShowStatus("设备号复制失败"+ex.Message);
            }
        }

        private void ShowCopyMsg(object msg)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    lblCopyMsg.Text = msg.ToString();
                    lblCopyMsg.Visible = true;

                    Application.DoEvents();
                    Delay.Start(500);
                    lblCopyMsg.Visible = false;
                }));

            }
            catch (Exception ex)
            {

            }
        }



    }
}
