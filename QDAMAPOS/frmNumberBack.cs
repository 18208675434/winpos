using QDAMAPOS.Common;
using QDAMAPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QDAMAPOS
{
    public partial class frmNumberBack : Form
    {

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        public double NumValue = 0;

        public string strNumValue = "";

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private string ParaTitle = "";
        private NumberType ParaNumberType =NumberType.None;
        private ShowLocation ParaShowLocation = ShowLocation.Right;
        


        public frmNumberBack()
        {
            InitializeComponent();
        }


        public frmNumberBack(string title, NumberType numbertype, ShowLocation showlocatoin)
        {
            InitializeComponent();
            ParaTitle=title;
            ParaNumberType=numbertype;
            ParaShowLocation = showlocatoin;


        }


        private void frmNumberBack_Shown(object sender, EventArgs e)
        {
            try
            {
                if (MainModel.frmnumber != null)
                {

                    if (ParaShowLocation == ShowLocation.Right)
                    {
                        MainModel.frmnumber.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - MainModel.frmnumber.Width - 40, Screen.AllScreens[0].Bounds.Height * 15 / 100);
                    }
                    else
                    {
                        MainModel.frmnumber.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - MainModel.frmnumber.Width) / 2, (Screen.AllScreens[0].Bounds.Height - MainModel.frmnumber.Height) / 2);
                    }
                    MainModel.frmnumber.DataReceiveHandle += FormNumber_DataReceiveHandle;
                    MainModel.frmnumber.UpInfo(ParaTitle, ParaNumberType);
                    MainModel.frmnumber.Show();
                    
                }
                else
                {
                    MainModel.frmnumber = new frmNumber(ParaTitle, ParaNumberType);
                    asf.AutoScaleControlTest(MainModel.frmnumber, 380, 540, this.Width * 36 / 100, this.Height * 70 / 100, true);

                    if (ParaShowLocation == ShowLocation.Right)
                    {
                        MainModel.frmnumber.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - MainModel.frmnumber.Width - 40, Screen.AllScreens[0].Bounds.Height * 15 / 100);
                    }
                    else
                    {
                        MainModel.frmnumber.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - MainModel.frmnumber.Width) / 2, (Screen.AllScreens[0].Bounds.Height - MainModel.frmnumber.Height) / 2);
                    }

                    MainModel.frmnumber.TopMost = true;
                    MainModel.frmnumber.DataReceiveHandle += FormNumber_DataReceiveHandle;
                    //MainModel.frmnumber=frmnumber;
                    MainModel.frmnumber.Show();
                    Application.DoEvents();
                }
              
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载数字键盘窗体异常" + ex.Message+ex.StackTrace, true);
                MainModel.frmnumber.DataReceiveHandle -= FormNumber_DataReceiveHandle;

                this.Close();
            }
        }




        private void FormNumber_DataReceiveHandle(int type, string number)
        {
            try
            {
                if (type == 1)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        NumValue = Convert.ToDouble(number);
                        strNumValue = number;
                        this.DialogResult = DialogResult.OK;


                    }));
                }

            }
            catch (Exception ex)
            {
                this.Close();
                LogManager.WriteLog("ERROR", "处理数字窗体数据异常" + ex.Message);
            }
            finally
            {
                MainModel.frmnumber.DataReceiveHandle -= FormNumber_DataReceiveHandle;
                this.Close();
            }
        }

   

    }

    public enum ShowLocation
    {
        Center,
        Right
    }
}
