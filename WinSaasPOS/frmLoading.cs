using WinSaasPOS.Common;
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

    public partial class frmLoading : Form
    {


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public frmLoading()
        {
            InitializeComponent();
            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public void UpInfo(string msg)
        {
            try
            {
                if (msg.Contains("|"))
                {
                    string[] strs = msg.Split('|');
                    lblMsg1.Text = strs[0];
                    lblMsg2.Text = strs[1];
                }
                else
                {
                    lblMsg1.Text = msg;
                    lblMsg2.Text = "";
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                try
                {
                    LoadingHelper.loadingForm = null;
                    this.Dispose();
                }
                catch { }

                LogManager.WriteLog("loading UpInfo 异常"+ex.Message);
            }
        }




    }

    class CONSTANTDEFINE
    {

        public delegate void SetUISomeInfo();

    }




}
