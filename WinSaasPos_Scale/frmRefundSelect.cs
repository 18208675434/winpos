using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinSaasPOS_Scale
{
    public partial class frmRefundSelect : Form
    {
        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();


        private Order CurrentOrder = null;

        private HttpUtil httputil = new HttpUtil();


        public RefundPara Refrefundpara = null;

        public Order Reforder = null;

        public frmRefundSelect()
        {
            InitializeComponent();
        }

        public frmRefundSelect(Order order)
        {
            InitializeComponent();
            CurrentOrder = order;
            IniOrder();
            LoadDgv(order);
        }
        private void IniOrder(){
            try{
                for (int i = 0; i < CurrentOrder.products.Count; i++)
                {
                     CurrentOrder.products[i].maxnum=Math.Max(CurrentOrder.products[i].num,CurrentOrder.products[i].maxnum);
                     CurrentOrder.products[i].IsSelect = true;
                }

            }catch{}
        }

        private void frmRefundSelect_Shown(object sender, EventArgs e)
        {

        }

        private void picExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 全部退款不调用退款试算接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefundAll_Click(object sender, EventArgs e)
        {

            RefundPara refundpara = new RefundPara();
            refundpara.aftersaletype = 2;
            refundpara.orderid =Convert.ToInt64(CurrentOrder.orderid);
            refundpara.allorder = true;
                this.DialogResult = DialogResult.OK;
                Refrefundpara = refundpara;
                Reforder = CurrentOrder;
                this.Close();
            
        }

        private void btnRefund_Click(object sender, EventArgs e)
        {
            //不支持部分退 调用全部退款
            if (CurrentOrder.supportpartrefund == 0)
            {
                btnRefundAll_Click(null,null);
                return;
            }

            List<OrderRefunditem> Lstorderrefunitem=new List<OrderRefunditem>();
            foreach(QuereOrderProduct pro in CurrentOrder.products)
            {
                if(pro.IsSelect){
                    OrderRefunditem orderrefunditem = new OrderRefunditem();

                    orderrefunditem.refundqty=pro.num;
                   
                    orderrefunditem.orderitemid=pro.orderitemid;
                    orderrefunditem.goodsid=pro.skucode;
                   
                    Lstorderrefunitem.Add(orderrefunditem);
                }
            }

            if(Lstorderrefunitem.Count<=0){
                MainModel.ShowLog("请选择退款商品",false);
                return;
            }

            RefundPara refundpara = new RefundPara();
            refundpara.aftersaletype = 2;
            refundpara.orderid = Convert.ToInt64(CurrentOrder.orderid);

            refundpara.allorder = false;
            refundpara.orderrefunditems = Lstorderrefunitem;

            string ErrorMsg = "";

            Order queryorder = httputil.RefundValuate(refundpara, ref ErrorMsg);

            if (!string.IsNullOrEmpty(ErrorMsg) || queryorder == null)
            {
                MainModel.ShowLog(ErrorMsg, true);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                Refrefundpara = refundpara;
                Reforder = queryorder;
                this.Close();
            }

        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void LoadDgv(Order order)
        {
            try
            {

                int SelectCount = 0;
                int oldrowindex = dgvGood.FirstDisplayedScrollingRowIndex;                
                dgvGood.Rows.Clear();
                foreach(QuereOrderProduct pro in order.products)
                {
                string barcode =pro.title + "\r\n  " + pro.skucode;
                string price = "";
                string jian = "";

                string num = "";
                string add = "";
                string total = "";
                string orderitemid = pro.orderitemid.ToString();

                if (pro.price.saleprice == pro.price.originprice)
                {
                    price = pro.price.saleprice.ToString("f2");
                }
                else
                {
                    price = pro.price.saleprice.ToString("f2");
                }

                if (pro.goodstagid == 0)  //0是标品  1是称重
                {
                    add = "+";
                    jian = "-";
                    num = pro.num.ToString();
                }
                else
                {
                    add = "";
                    jian = "";
                    num = pro.price.specnum + pro.price.unit;
                }
                total = pro.price.total.ToString("f2");

                     Image img=null;
                if (order.supportpartrefund == 1)
                {
                    dgvGood.ReadOnly = false;
                    lblCash.Visible = false;
                    if (pro.IsSelect)
                    {
                        img= picCheck.Image;

                        //SelectCount++;
                        SelectCount += pro.num;
                    }
                    else
                    {
                        img = picUnCheck.Image;
                    }
                }
                else
                {
                    dgvGood.ReadOnly = true;
                    lblCash.Visible = true;
                    pro.IsSelect = true;
                    img = picCheck.Image;
                   // SelectCount++;
                    SelectCount += pro.num;
                    //dataGridView1.Enabled = false;
                }

                if (this.IsHandleCreated)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        dgvGood.Rows.Insert(0, new object[] { img, orderitemid, barcode, price, jian, num, add, total });
                       // dataGridView1.Rows.Insert(0, new object[] { img, orderitemid, barcode, price, jian, num, add, total });
                    }));
                }
                else
                {
                    dgvGood.Rows.Insert(0, new object[] { img, orderitemid, barcode, price, jian, num, add, total });
                    //dataGridView1.Rows.Insert(0, new object[] { img, orderitemid, barcode, price, jian, num, add, total });
                }
                
            }
                try { dgvGood.FirstDisplayedScrollingRowIndex = oldrowindex; }
                catch { }
                Application.DoEvents();
                lblSelectCount.Text = "已选（"+SelectCount+"）";
            }
            catch (Exception ex)
            {
                
            }
        }

        private void dgvGood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                string proinfo = dgvGood.Rows[e.RowIndex].Cells["barcode"].Value.ToString();
                string[] strs = proinfo.Replace("\r\n", "*").Split('*');
                string skucode = strs[strs.Length - 1].Trim();
                string proname = strs[strs.Length - 2].Trim();
                long orderitemid = Convert.ToInt64(dgvGood.Rows[e.RowIndex].Cells["orderitemid"].Value.ToString());
                //增加标品
                if (dgvGood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "+")
                {

                    for (int i = 0; i < CurrentOrder.products.Count; i++)
                    {
                        if (CurrentOrder.products[i].skucode == skucode)
                        {
                            if (CurrentOrder.products[i].num < CurrentOrder.products[i].maxnum)
                            {
                                CurrentOrder.products[i].num += 1;
                            }

                            break;
                        }
                    }

                    LoadDgv(CurrentOrder);

                    //UploadDgvGoods(CurrentCart);
                }
                //减少标品
                else if (dgvGood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "-")
                {
                    for (int i = 0; i < CurrentOrder.products.Count; i++)
                    {
                        if (CurrentOrder.products[i].skucode == skucode)
                        {

                            if (CurrentOrder.products[i].num == 1)
                            {

                                CurrentOrder.products[i].IsSelect = false;
                                this.Enabled = true;

                            }
                            else
                            {
                                CurrentOrder.products[i].num -= 1;
                            }
                            break;
                        }
                    }
                    LoadDgv(CurrentOrder);
                }
                else
                {
                    //QuereOrderProduct querypro = (QuereOrderProduct)CurrentOrder.products.Where(r => r.orderitemid == orderitemid).ToList()[0];
                    //querypro.IsSelect = !querypro.IsSelect;

                    for (int i = 0; i < CurrentOrder.products.Count; i++)
                    {
                        if (CurrentOrder.products[i].orderitemid == orderitemid)
                        {
                            CurrentOrder.products[i].IsSelect = !CurrentOrder.products[i].IsSelect;
                            break;
                        }
                    }

                    LoadDgv(CurrentOrder);
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("选择退款商品异常"+ex.Message,true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dgvGood.Rows.Insert(0, new object[] { "", orderitemid, barcode, price, "", num, "", total });
        }


    }
}
