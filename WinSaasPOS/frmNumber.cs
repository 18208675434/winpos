using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WinSaasPOS.Common;
using System.Threading;
using WinSaasPOS.Model;

namespace WinSaasPOS
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

        public string strNumValue = "";

        /// <summary>
        /// 按比例缩放页面及控件
        /// </summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private NumberType CurrentNumberType;


        public frmNumber()
        {

            InitializeComponent();
          
          
        }
        public frmNumber(string title,NumberType numbertype)
        {
           
            InitializeComponent();
            //Application.EnableVisualStyles();
            CurrentNumberType = numbertype;

            lblInfo.Text = title;

            Application.DoEvents();
        }


        private void frmNumber_Load(object sender, EventArgs e)
        {
            try
            {

                switch (CurrentNumberType)
                {
                    case NumberType.BarCode: lblShuiyin.Text = "输入商品条码"; lblg.Visible = false; btnBack.Width = btnNext.Width; txtNum.Width = btnBack.Width - (txtNum.Left - btnBack.Left) - 4; break;
                    case NumberType.MemberCode: lblShuiyin.Text = "输入会员手机号"; lblg.Visible = false; btnBack.Width = btnNext.Width; txtNum.Width = btnBack.Width - (txtNum.Left - btnBack.Left) - 4; break;
                    case NumberType.ProWeight: lblShuiyin.Text = "请输入实际重量"; lblg.Visible = true; btnBack.Width = lblg.Left - btnBack.Left - 2; txtNum.Width = btnBack.Width - (txtNum.Left - btnBack.Left) - 4; break;
                }
               

                txtNum.Font = new System.Drawing.Font("微软雅黑", txtNum.Font.Size * Math.Min(MainModel.hScale, MainModel.wScale));
              
                NumberUtil.IsUpdate = false;
                NumberUtil.FormGuid = "";

                btnNext.Focus();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载number页面异常" + ex.Message);
            }
        }


        public void UpInfo(string title, NumberType numbertype)
        {
            try
            {
                NumValue = 0;

                strNumValue = "";
                txtNum.Text = "";
                lblMsg.Text = "";
                lblInfo.Text = title;
                CurrentNumberType = numbertype;
                switch (CurrentNumberType)
                {
                    case NumberType.BarCode: lblShuiyin.Text = "输入商品条码"; lblg.Visible = false; btnBack.Width = btnNext.Width; txtNum.Width = btnBack.Width - (txtNum.Left - btnBack.Left) - 4; break;
                    case NumberType.MemberCode: lblShuiyin.Text = "输入会员手机号"; lblg.Visible = false; btnBack.Width = btnNext.Width; txtNum.Width = btnBack.Width - (txtNum.Left - btnBack.Left) - 4; break;
                    case NumberType.ProWeight: lblShuiyin.Text = "请输入实际重量"; lblg.Visible = true; btnBack.Width = lblg.Left - btnBack.Left - 2; txtNum.Width = btnBack.Width - (txtNum.Left - btnBack.Left) - 4; break;
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("刷新数字窗体异常"+ex.Message);
            }
        }


        private void btnCancle_Click(object sender, EventArgs e)
        {
            Control btn = (Control)sender;
            UpdatNumberUtil(btn.Name);
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(0, txtNum.Text, null, null);
            //this.Close();
            this.Hide();
        }

        //下一步需要判断实收现金是否足够
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                UpdatNumberUtil(btn.Name);
                if (txtNum.Text.Length == 0)
                {
                    return;
                }

                if (CurrentNumberType==NumberType.MemberCode)
                {
                    if (txtNum.Text.Length != 11)
                    {
                        // ShowLog("请输入正确的手机号！",false);
                        lblMsg.Text = "手机号格式不正确！";
                        return;
                    }

                    if (txtNum.Text.Length > 0 && txtNum.Text.Substring(0, 1) != "1")
                    {
                        //ShowLog("请输入正确的手机号！", false);
                        lblMsg.Text = "手机号格式不正确！";
                        return;
                    }
                }

                if (CurrentNumberType == NumberType.ProWeight)
                {

                    if (Convert.ToDouble(txtNum.Text) == 0)
                    {
                        return;
                    }
                }

                if (lblInfo.Text == "请输入商品条码")
                {
                    //if (txtNum.Text.Length > 5 && txtNum.Text.Length != 8 && txtNum.Text.Length != 13)
                    //{
                    //    // ShowLog("请输入正确的手机号！",false);
                    //    lblMsg.Text = "只允许输入1-5、8、13 位！";
                    //    return;
                    //}

                    //if (txtNum.Text.Length == 8)
                    //{
                    //    txtNum.Text = txtNum.Text.Substring(2, 5);
                    //}
                }


                this.DialogResult = DialogResult.OK;
                NumValue = Convert.ToDouble(txtNum.Text);

                strNumValue = txtNum.Text;
                if (DataReceiveHandle != null)
                    this.DataReceiveHandle.BeginInvoke(1, txtNum.Text, null, null);
               // this.Close();

                this.Hide();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("数字窗体关闭异常"+ex.StackTrace);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            UpdatNumberUtil(btn.Name);

            Button button = (Button)sender;

            //条码控制20位
            if (CurrentNumberType == NumberType.BarCode && txtNum.Text.Length >= 20)
            {
                return;
            }

            //会员号控制11位
            if (CurrentNumberType == NumberType.MemberCode && txtNum.Text.Length >= 11)
            {
                return;
            }

            //商品重量控制最大6位
            if (CurrentNumberType == NumberType.ProWeight && txtNum.Text.Length >= 6)
            {
                return;
            }

            //控制重量信息第一位不为0
            if (CurrentNumberType == NumberType.ProWeight && txtNum.Text.Length >= 5 && button.Name=="btn00")
            {
                return;
            }
            if (CurrentNumberType == NumberType.ProWeight && txtNum.Text == "" && (button.Name == "btn00" || button.Name == "btn0"))
            {
                return;
            }
          
         
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
            if (txtNum.Text.Length == 0)
            {
                lblMsg.Text = "";
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


        private void lblShuiyin_Click(object sender, EventArgs e)
        {
            txtNum.Focus();
        }

        private void txtNum_TextChanged(object sender, EventArgs e)
        {
            if (txtNum.Text.Length > 0)
            {
                lblShuiyin.Visible = false;
            }
            else
            {
                lblShuiyin.Visible = true;
            }

            try
            {
                double doublenum = Convert.ToDouble(txtNum.Text);

                if (doublenum > 0)
                {
                    btnNext.BackColor = Color.OrangeRed;
                }
                else
                {
                    btnNext.BackColor = Color.Silver;
                }
            }
            catch
            {
                btnNext.BackColor = Color.Silver;
            }
        }


    }

    public enum NumberType
    {
        None,
        BarCode,
        MemberCode,
        ProWeight
    }
}
