using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using QDAMAPOS.Common;
using QDAMAPOS.Model;
using Maticsoft.Model;
using Newtonsoft.Json;
using Maticsoft.BLL;

namespace QDAMAPOS
{
    public partial class frmCashPayOffLine : Form
    {

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">0:支付完成   1：在线收银继续支付 3：取消  12004：会员登录失效   100031：店员登录失效</param>
        /// <param name="orderid"></param>
        public delegate void DataRecHandleDelegate(int type, string orderid,Cart cart);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;

        /// <summary>
        /// 收银主界面传过来的 抹零后的cartModel
        /// </summary>
        private Cart thisCurrentCart = new Cart();


        /// <summary>
        /// 界面初始化录入默认值   修改的话自动清空
        /// </summary>
        bool isfirst = true;

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private decimal relaycash=0;

        private DBORDER_BEANBLL ordrebll = new DBORDER_BEANBLL();

        private string offlineorderid = "";

        public frmCashPayOffLine()
        {
            InitializeComponent();
        }

        public frmCashPayOffLine(Cart cart)
        {
            InitializeComponent();

            thisCurrentCart = (Cart)cart.qianClone();
            int tempcash = (int)(cart.totalpayment * 10);
            relaycash = (decimal)tempcash / 10;
            btnNext.Focus();
            
        }



        private void frmCashPay_Load(object sender, EventArgs e)
        {
            try
            {

                txtCash.Text = relaycash.ToString("f2");
                btnNext.Focus();

                lblPrice.Text = "￥" + relaycash.ToString("f2");

              
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("现金页面初始化异常" + ex.Message, true);
            }
        }

        private void frmCash_Shown(object sender, EventArgs e)
        {
        }


        public void UpInfo(Cart cart)
        {
            try
            {

                isfirst = true;


              
                int cashpaytype = 0;


                string cashpayorderid = "";

                thisCurrentCart = (Cart)cart.qianClone();
                int tempcash = (int)(cart.totalpayment * 10);
                relaycash = (decimal)tempcash / 10;
                txtCash.Text = relaycash.ToString("f1");
                btnNext.Focus();

                lblPrice.Text = "￥" + relaycash.ToString("f1");

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("刷新数字窗体异常" + ex.Message);
            }
        }

        private void CheckUserAndMember(int resultcode)
        {
            try
            {
                if (resultcode == MainModel.HttpMemberExpired || resultcode == MainModel.HttpUserExpired)
                {
                    if (DataReceiveHandle != null)
                        this.DataReceiveHandle.BeginInvoke(resultcode, "",thisCurrentCart, null, null);
                    this.Hide();    //this.close();
                }
            }
            catch (Exception ex)
            {
                //ShowLog("验证用户/会员异常", true);
            }

        }


        private void btnCancle_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataReceiveHandle != null)
                    this.DataReceiveHandle.BeginInvoke(3, "", thisCurrentCart, null, null);

                //this.DialogResult = DialogResult.OK;
                this.Hide();    //this.close();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("关闭现金支付窗体异常"+ex.Message);
            }
        }

        //下一步需要判断实收现金是否足够 通过cart接口返回值判断
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    decimal cash = Convert.ToDecimal(txtCash.Text);

                    if (cash < relaycash)
                    {
                        return;
                    }
                    else if (cash == relaycash)
                    {
                        ReceiptUtil.EditOpenMoneyPacketCount(1);


                        if (!OrderUtil.CreaterOrder(thisCurrentCart, relaycash, ref offlineorderid))
                        {
                            return;
                        }
                       // CreaterOrder();

                        if (DataReceiveHandle != null)
                            this.DataReceiveHandle.BeginInvoke(1, offlineorderid, thisCurrentCart, null, null);
                        this.Hide();
                        //chhaugnjdingdan 
                    }
                    else
                    {
                        ReceiptUtil.EditOpenMoneyPacketCount(1);
                        MainModel.frmMainmediaCart = thisCurrentCart;
                        MainModel.frmmainmedia.UpdateForm();

                        thisCurrentCart.payamtbeforecash = relaycash;
                        this.thisCurrentCart.cashpayamt = cash;
                        thisCurrentCart.cashchangeamt = cash - relaycash;

                        frmCashChange frmcashchange = new frmCashChange(thisCurrentCart);

                        frmcashchange.frmCashChange_SizeChanged(null, null);
                        asf.AutoScaleControlTest(frmcashchange, 380, 540, this.Width, this.Height, true);
                        frmcashchange.Location = this.Location;
                        frmcashchange.DataReceiveHandle += FormCashChange_DataReceiveHandle;
                        frmcashchange.TopMost = true;
                        frmcashchange.ShowDialog();

                        if (frmcashchange.DialogResult == DialogResult.OK)
                        {
                            //创建订单
                            if (!OrderUtil.CreaterOrder(thisCurrentCart, relaycash, ref offlineorderid))
                            {
                                return;
                            }
                           // CreaterOrder();

                            if (DataReceiveHandle != null)
                                this.DataReceiveHandle.BeginInvoke(1, offlineorderid, thisCurrentCart, null, null);
                            this.Hide();
                        }
                        Application.DoEvents();
                    }             

                }
                catch
                {
                    return;
                }


            }
            catch (Exception ex)
            {
                ShowLog("现金支付异常：" + ex.Message, false);
            }

        }


        private void FormCashChange_DataReceiveHandle(int type)
        {

            if (type == 1)
            {
                if (!OrderUtil.CreaterOrder(thisCurrentCart, relaycash, ref offlineorderid))
                {
                    return;
                }
              //  CreaterOrder();
                    this.DialogResult = DialogResult.OK;
                    if (DataReceiveHandle != null)
                        this.DataReceiveHandle.BeginInvoke(1, offlineorderid, thisCurrentCart, null, null);
                    this.Hide();
            }
            else
            {              
                //找零页面返回  不做处理 
            }
        }

        //TODO  修改样式
        private void ShowLog(string msg, bool iserror)
        {

            MsgHelper.AutoShowForm(msg);
            //this.Invoke(new InvokeHandler(delegate()
            //{

            //    frmMsg frmmsf = new frmMsg(msg, iserror, 1000);
            //    frmmsf.ShowDialog(); LogManager.WriteLog(msg);
            //}));

        }

        #region  小键盘按键
        private void btn_Click(object sender, EventArgs e)
        {
            if (isfirst)
            {
                txtCash.Clear();
                isfirst = false;
            }
            Button btn = (Button)sender;
            try
            {
                //小数点后允许一位  现金抹零后不允许再输入零
                if (txtCash.Text.Length > 2 && txtCash.Text.Substring(txtCash.Text.Length - 2, 1) == ".")
                {
                    return;
                }
                //限制金额不超过100000

                //第一位是0 后面只能输入.
                if (txtCash.Text == "0")
                {
                    return;
                }

                decimal CheckDecimal = Convert.ToDecimal(txtCash.Text + btn.Name.Replace("btn", ""));

                if (CheckDecimal > 100000)
                {
                    return;
                }
                txtCash.Text += btn.Name.Replace("btn", "");
            }
            catch
            {

            }



        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (isfirst)
            {
                txtCash.Clear();
                isfirst = false;
            }


            if (txtCash.Text.Length > 0 && !txtCash.Text.Contains("."))
            {
                txtCash.Text += ".";
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (isfirst)
            {
                txtCash.Clear();
                isfirst = false;
            }


            if (txtCash.Text.Length > 0)
            {
                txtCash.Text = txtCash.Text.Substring(0, txtCash.Text.Length - 1);
            }
        }


        //控制仅允许录入数字
        private void TextNUMBER_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        #endregion


        public void frmCashPay_SizeChanged(object sender, EventArgs e)
        {
            //asf.ControlAutoSize(this);
        }

        private void frmCashPay_FormClosing(object sender, FormClosingEventArgs e)
        {
            //thisCurrentCart.cashpayamt = 0;
            //thisCurrentCart.cashpayoption = 0;
        }

        //屏蔽回车和空格键
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Space)
                return false;
            else
                return base.ProcessDialogKey(keyData);
        }

        private void frmCashPay_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)            
            //{               
            //    e.Handled = false;   
            //    //将Handled设置为true，指示已经处理过KeyPress事件                        
            //}

        }

        private void txtNum_TextChanged(object sender, EventArgs e)
        {
            if (txtCash.Text.Length > 0)
            {
                lblShuiyin.Visible = false;
            }
            else
            {
                lblShuiyin.Visible = true;
            }

            try
            {
                decimal doublenum = Convert.ToDecimal(txtCash.Text);

                if (doublenum >= relaycash)
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

        private void lblShuiyin_Click(object sender, EventArgs e)
        {
            txtCash.Focus();
        }

        private string GetOrderID()
        {
            try
            {
                //订单号：取当时时间戳+设备SN+门店ID+登录离线店员手机号+该订单总价+现金支付价+找零价+抹分价+实际订单对象hashcode+订单对象hashcode，生成 后的订单hashcode+4位随机数,生成后的订单号去掉"-"为本次生成的离线订单号

                string strorder = MainModel.getStampByDateTime(DateTime.Now) + MainModel.DeviceSN + MainModel.CurrentUserPhone + thisCurrentCart.GetHashCode();
                return strorder.GetHashCode().ToString().Replace("-", "") + Getrandom(4);
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("获取离线订单号异常"+ex.Message,true);
            }
            return "";
        }

        private string Getrandom(int num)
        {
            Random rd = new Random();
            string result = "";
            for (int i = 0; i < num; i++)
            {
                result += rd.Next(10).ToString();
            }
            return result;
        }

    }
}
