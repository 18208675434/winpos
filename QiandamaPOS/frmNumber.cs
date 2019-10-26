using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using QiandamaPOS.Common;
using System.Threading;

namespace QiandamaPOS
{
    public partial class frmNumber : Form
    {

        /// <summary>
        /// 当前页面唯一标识
        /// </summary>
        public string ThisGuid = Guid.NewGuid().ToString();
        /// <summary>
        /// 数据处理委托方法
        /// </summary>
        /// <param name="type">0：返回  1：确认数字</param>
        public delegate void DataRecHandleDelegate(int type,string number);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;

        bool isrun = true;

        bool isDouble = false;

        public double NumValue = 0;

        Thread threadItemExedate;
        /// <summary>
        /// 按比例缩放页面及控件
        /// </summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public frmNumber(string title,bool isdouble,bool isweight)
        {
           
            InitializeComponent();

            isDouble = isdouble;
            lblInfo.Text = title;
            if (isweight)
            {
                lblg.Visible = true;
                txtNum.MaxLength = 6;
            }
            else
            {
                lblg.Visible = false;

                txtNum.SetWatermark(title);

                btnCancle.Focus();
            }
            Application.DoEvents();
        }

        private void frmCash_Shown(object sender, EventArgs e)
        {
            if (isDouble)
            {
                 threadItemExedate = new Thread(UpdteForm);
                threadItemExedate.IsBackground = true;
                threadItemExedate.Start();
            }

            NumberUtil.IsUpdate = false;
            NumberUtil.FormGuid = "";

            btnDel.Focus();
            //this.Size = new System.Drawing.Size(Screen.AllScreens[0].Bounds.Width / 3, Screen.AllScreens[0].Bounds.Height - 200);
            //this.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - this.Width - 50, 100);
          
           // this.Size = new System.Drawing.Size(Screen.AllScreens[0].Bounds.Width / 3, Screen.AllScreens[0].Bounds.Height - 200);
            //this.Location = new System.Drawing.Point(400, 10);
            //txtNum.SetWatermark("请输入实收现金");
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            Control btn =(Control)sender;
            UpdatNumberUtil(btn.Name);
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(0, txtNum.Text, null, null);
            this.Close();
        }

        //下一步需要判断实收现金是否足够
        private void btnNext_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            UpdatNumberUtil(btn.Name);
            if (txtNum.Text.Length == 0)
            {
                return;
            }

            this.DialogResult = DialogResult.OK;
            NumValue = Convert.ToDouble(txtNum.Text);

            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(1,txtNum.Text, null, null);
            this.Close();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            UpdatNumberUtil(btn.Name);

            Button button = (Button)sender;
            txtNum.Text += button.Name.Replace("btn", "");
        }

        private void btnDot_Click(object sender, EventArgs e)
        {

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            UpdatNumberUtil(btn.Name);

            if (txtNum.Text.Length > 0)
            {
                txtNum.Text = txtNum.Text.Substring(0,txtNum.Text.Length-1);
            }
        }


        //控制仅允许录入数字
        private void TextNUMBER_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;

                e.Handled = true;
                char ch = e.KeyChar;

                if (ch >= '0' && ch <= '9' )
                    e.Handled = false;


                if (ch == '.')
                    e.Handled = false;

                if (ch == (char)Keys.Back)
                    e.Handled = false;
               
            }
            catch { }
        }

        /// <summary>
        /// 防止死循环
        /// </summary>
        bool thisEnable = true;
        private void UpdatNumberUtil(string btnname)
        {


            if (thisEnable)
            {
                NumberUtil.BtnName = btnname;
                NumberUtil.FormGuid = ThisGuid;

                NumberUtil.IsUpdate = true;
            }
            else
            {
                thisEnable = true;
            }
            
        }


        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();
        private void UpdteForm()
        {
            while (isrun)
            {
                try
                {
                    if (NumberUtil.IsUpdate && NumberUtil.FormGuid!="" && NumberUtil.FormGuid != ThisGuid)
                    {
                        //NumberUtil.IsUpdate = false;
                        
                        //Control control = Controls.Find(NumberUtil.BtnName, true)[0];
                        //Button tempbtn = (Button)control;
                        //tempbtn.PerformClick();

                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new InvokeHandler(delegate()
                              {
                                  thisEnable = false;
                                  NumberUtil.IsUpdate = false;
                                  Control control = Controls.Find(NumberUtil.BtnName, true)[0];
                                  Button tempbtn = (Button)control;
                                  tempbtn.PerformClick();

                              }));
                        }
                       // return;

                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("同步数字窗体数据异常"+ex.Message);
                }

                Delay.Start(50);
            }
        }

        private void frmNumber_FormClosing(object sender, FormClosingEventArgs e)
        {
            isrun = false;

            if (threadItemExedate != null && threadItemExedate.IsAlive)
            {
                threadItemExedate.Abort();

            }
        }

        public void frmNumber_SizeChanged(object sender, EventArgs e)
        {
            asf.ControlAutoSize(this);
        }

    }
}
