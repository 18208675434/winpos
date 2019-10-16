using QiandamaPOS.Common;
using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QiandamaPOS
{
    public partial class frmGood : Form
    {
        /// <summary>
        /// 数据接收处理委托方法  type 0:减少  1：增加 2：删除
        /// </summary> 
        /// <param name="data"></param>
        public delegate void DataRecHandleDelegate(int type, Product pro);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;

        Product CurrentPro;

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public frmGood(Product pro)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            CurrentPro = pro;

            Ini(pro);
        }
        private void Ini(Product pro)
        {
            try
            {
                lblProName.Text = pro.title;
                lblBarCode.Text = pro.barcode;
                lblItemPrice.Text = "￥" + pro.price.saleprice.ToString();

                //0 是标品  1是散称
                if (pro.goodstagid == 0)
                {
                    lblNum.Text = pro.num.ToString();
                    lblSalePrice.Text = "￥" + pro.price.total.ToString();

                    btnAdd.Visible = true;
                    btnMinus.Visible = true;
                    pnlNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                }
                else
                {
                    lblNum.Text = pro.price.specnum.ToString() + pro.price.unit;
                    lblSalePrice.Text = "￥" + pro.price.total.ToString();

                    btnAdd.Visible = false;
                    btnMinus.Visible = false;
                    pnlNum.BorderStyle = System.Windows.Forms.BorderStyle.None;
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化商品显示行异常：" + ex.Message);
            }
        }

        private void picbtnDel_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(2, CurrentPro, null, null);
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            //剩下一个时不允许减
            if (CurrentPro.num <= 1)
            {
                return;
            }
            CurrentPro.num = CurrentPro.num - 1;
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(0, CurrentPro, null, null);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CurrentPro.num = CurrentPro.num + 1;
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(1, CurrentPro, null, null);
        }

        public void frmGood_SizeChanged(object sender, EventArgs e)
        {
            asf.ControlAutoSize(this);
        }
    }
}
