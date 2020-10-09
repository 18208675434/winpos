using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.MyControl;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MenuUI
{
    public partial class FormRefundByAmt : Form
    {

        private Order CurrentOrder = null;

        public RefundPara Refrefundpara = null;

        public Order Reforder = null;

        private HttpUtil httputil = new HttpUtil();

        private int CurrentPage = 1;

        public FormRefundByAmt(Order order)
        {
            InitializeComponent();

            CurrentOrder = (Order)MainModel.Clone(order);
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormRefundByAmt_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            loadDgvGood();

            numTxt1.BorderStyle = BorderStyle.None;
            numTxt2.BorderStyle = BorderStyle.None;
            numTxt3.BorderStyle = BorderStyle.None;
            numTxt4.BorderStyle = BorderStyle.None;

            Application.DoEvents();
            numTxt1.Focus();
        }


        private void loadDgvGood(){
            try
            {

                dgvProduct.Rows.Clear();

                if (CurrentOrder == null || CurrentOrder.products == null || CurrentOrder.products.Count == 0)
                {
                    return;
                   
                }

                rbtnPageUp.WhetherEnable = CurrentPage > 1;

                int startindex = (CurrentPage - 1) * 4;

                int lastindex = Math.Min(CurrentOrder.products.Count - 1, startindex + 3);

                List<QuereOrderProduct> lstLoading = CurrentOrder.products.GetRange(startindex, lastindex - startindex + 1);

                numTxt1.Visible = false;
                numTxt2.Visible = false;
                numTxt3.Visible = false;
                numTxt4.Visible = false;
                for (int i = 0; i < lstLoading.Count; i++)
                {
                    QuereOrderProduct pro = lstLoading[i];
                   dgvProduct.Rows.Add(GetDgvItem(pro));

                   string WaterText = string.IsNullOrEmpty(pro.specifiedamountrefunddesc) ? "请输入退款金额" : pro.specifiedamountrefunddesc;
                   string refundamt = pro.refundamt == 0 ? "" : pro.refundamt.ToString();

                   switch (i)
                   {
                       case 0: numTxt1.Visible = true; numTxt1.Text = refundamt; numTxt1.WaterText = WaterText; numTxt1.Tag = pro; numTxt1.Enabled = pro.supportspecifiedamountrefund == 1; break;
                       case 1: numTxt2.Visible = true; numTxt2.Text = refundamt; numTxt2.WaterText = WaterText; numTxt2.Tag = pro; numTxt2.Enabled = pro.supportspecifiedamountrefund == 1; break;
                       case 2: numTxt3.Visible = true; numTxt3.Text = refundamt; numTxt3.WaterText = WaterText; numTxt3.Tag = pro; numTxt3.Enabled = pro.supportspecifiedamountrefund == 1; break;
                       case 3: numTxt4.Visible = true; numTxt4.Text = refundamt; numTxt4.WaterText = WaterText; numTxt4.Tag = pro; numTxt4.Enabled = pro.supportspecifiedamountrefund == 1; break;
                       default: break;
                   }
                }
                rbtnPageDown.WhetherEnable = CurrentOrder.products.Count > CurrentPage * 4;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载商品列表异常"+ex.Message,true);
            }
        }

        private Bitmap GetDgvItem(QuereOrderProduct pro)
        {
            try
            {
                lblSkuName.Text = pro.title;
                lblSkucode.Text = pro.skucode;

                lblTotalPrice.Text = "￥" + pro.price.payamtafterpromo.ToString("f2");

               lblNum.Text= pro.goodstagid==0 ? pro.num+pro.price.unit :  pro.price.specnum +pro.price.unit;

               lblTotalPrice.Left = pnlItem.Width - lblTotalPrice.Width;
               lblNum.Left = pnlItem.Width - lblNum.Width;

               Bitmap b = (Bitmap)MainModel.GetControlImage(pnlItem);
               return b;


            }
            catch (Exception ex)
            {
                return Resources.ResourcePos.empty;
            }
        }

        private void dgvProduct_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProduct_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    QuereOrderProduct selectpro = CurrentOrder.products[e.RowIndex];

                    decimal input = Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                    if (input > selectpro.price.payamtafterpromo)
                    {
                        MainModel.ShowLog("退款金额不能大于总价", false);
                    }

                    selectpro.refundamt = input;
                }

            }
            catch { }
        }

        private void dgvProduct_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    QuereOrderProduct selectpro = CurrentOrder.products[e.RowIndex];

                    if (selectpro.supportspecifiedamountrefund == 0)
                    {
                        dgvProduct.ClearSelection();

                        btnOK.Select();
                    }
                }

            }
            catch { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string confirminfo = "确认商品及退款金额\r\n";
                List<OrderRefunditem> Lstorderrefunitem = new List<OrderRefunditem>();
                foreach (QuereOrderProduct pro in CurrentOrder.products)
                {
                    if (pro.refundamt > pro.price.payamtafterpromo)
                    {
                        MainModel.ShowLog(pro.title+"退款金额不能大于总价",false);
                        return;
                    }
                    if (pro.supportspecifiedamountrefund==1 && pro.refundamt>0)
                    {

                        confirminfo += pro.title + "￥" + pro.refundamt.ToString("f2")+"\r\n";

                        OrderRefunditem orderrefunditem = new OrderRefunditem();

                        orderrefunditem.refundqty = pro.num;
                        orderrefunditem.refundamt = pro.refundamt;
                        orderrefunditem.orderitemid = pro.orderitemid;
                        orderrefunditem.goodsid = pro.skucode;

                        Lstorderrefunitem.Add(orderrefunditem);
                    }
                }

                if (Lstorderrefunitem.Count <= 0)
                {
                    MainModel.ShowLog("请输入退款的商品金额", false);
                    return;
                }

                RefundPara refundpara = new RefundPara();
                refundpara.aftersaletype = 2;
                refundpara.specifiedamountrefund = 1;
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

                    this.Hide();
                    if (!ConfirmHelper.Confirm("商品退差价",confirminfo))
                    {
                        this.Show();
                        return;
                    }


                    string ErrorMsg1 = "";
                    string resultorderid = httputil.Refund(refundpara, ref ErrorMsg1);
                    if (ErrorMsg != "")
                    {
                        MainModel.ShowLog(ErrorMsg, true);
                        this.Show();
                        return;
                    }
                    else
                    {
                        AbnormalOrderUtil.RefundDiffList(resultorderid,refundpara);
                        PrintDetail printdetail = httputil.GetPrintDetail(resultorderid, ref ErrorMsg);

                        if (ErrorMsg != "" || printdetail == null)
                        {
                            LoadingHelper.CloseForm();
                            MainModel.ShowLog(ErrorMsg, true);
                        }
                        else
                        {
                            //SEDPrint(printdetail);
                            string PrintErrorMsg = "";
                            bool printresult = PrintUtil.PrintOrder(printdetail, true, ref PrintErrorMsg); //PrintUtil.PrintOrder(printdetail, false, ref PrintErrorMsg);

                            if (PrintErrorMsg != "" || !printresult)
                            {
                                MainModel.ShowLog(PrintErrorMsg, true);
                            }
                            else
                            {
                                // MainModel.ShowLog("打印完成", false);
                            }

                        }


                        MainModel.ShowLog("退款成功", false);
                        try { PrintUtil.OpenCashDrawerEx(); }
                        catch { }

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch { }
        }

        private void rbtnPageUp_ButtonClick(object sender, EventArgs e)
        {
            if ( !rbtnPageUp.WhetherEnable)
            {
                return;
            }
            CurrentPage--;
            loadDgvGood();
        }

        private void rbtnPageDown_ButtonClick(object sender, EventArgs e)
        {
            if ( !rbtnPageDown.WhetherEnable)
            {
                return;
            }
            CurrentPage++;
            loadDgvGood();
        }

        private void numTxt_Leave(object sender, EventArgs e)
        {
            try
            {

                NumberTextBox numtext = sender as NumberTextBox;
                QuereOrderProduct pro = (QuereOrderProduct)numtext.Tag;

                decimal inputnum = 0;
                if (!string.IsNullOrEmpty(numtext.Text) && !decimal.TryParse(numtext.Text, out inputnum))
                {
                    MainModel.ShowLog("请输入正确的金额",false);
                    return;
                }

                if (inputnum > pro.price.payamtafterpromo)
                {
                    MainModel.ShowLog("退款金额不能大于总价", false);
                }
                pro.refundamt = inputnum;
                //MessageBox.Show(numTxt1.Text);
            }
            catch { }
        }

        private void FormRefundByAmt_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalUtil.CloseOSK();
        }

    }
}
