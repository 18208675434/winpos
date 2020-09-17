using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.PayUI
{
    public partial class FormPayByOther : Form
    {

        public OrderPayTypeVo SelectPayType = null;

        public Cart thisCurrentCart = null;

        public OtherPayResult otherPayResult = null;

        private Bitmap bmpCash;
        private Bitmap bmpDelete;

        //是否需要清空文本框，默认带入金额时 修改直接覆盖
        private bool NeedClearTxt = true;

        public List<OtherPayInfoEntity> LstOtherPayInfos = new List<OtherPayInfoEntity>();

        /// <summary>
        /// 其他支付方式 上一页
        /// </summary>
        private Image imgPageUpForType;
        /// <summary>
        /// 其他支付方式  下一页
        /// </summary>
        private Image imgPageDownForType;

        public FormPayByOther(Cart cart)
        {
            try
            {
                InitializeComponent();
                otherPayResult = new OtherPayResult();
                SelectPayType = null;
                thisCurrentCart = cart;
                Application.DoEvents();
            }
            catch (Exception ex)
            {

            }
        }

        private void FormPayByOther_Shown(object sender, EventArgs e)
        {
            try
            {
                txtCash.Text = (thisCurrentCart.totalpayment + thisCurrentCart.otherpayamt).ToString("f2");
                lblTotalPayMent.Text = "￥" + thisCurrentCart.totalpayment.ToString("f2");

                bmpCash = new Bitmap(picCash.Image, dgvSelectType.RowTemplate.Height * 40 / 100, dgvSelectType.RowTemplate.Height * 40 / 100);
                bmpDelete = new Bitmap(picDelete.Image, dgvSelectType.RowTemplate.Height * 40 / 100, dgvSelectType.RowTemplate.Height * 40 / 100);

                imgPageUpForType = MainModel.GetControlImage(btnPageUp);
                imgPageDownForType = MainModel.GetControlImage(btnPageDown);
                Application.DoEvents();
                UpdateDgvSelectType();
                LoadPayType();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载其他支付页面控件异常"+ex.Message,true);
            }
        }


        private void LoadPayType()
        {
            try
            {
                if (SelectPayType != null)
                {
                    if ( SelectPayType.needcouponcode)
                    {

                        txtCash.WaterText = "请输入券码";
                        txtCash.decimaldigits = 0;
                        txtCash.Text = "";

                        btn30.Text = "00";
                        btn50.Text = "000";
                        btn100.Text = "0000";
                        btn200.Text = "00000";

                                            }
                    else
                    {

                        btn30.Text = "30";
                        btn50.Text = "50";
                        btn100.Text = "100";
                        btn200.Text = "200";


                        txtCash.WaterText = "请输入支付金额";
                        txtCash.decimaldigits = 2;
                        if (SelectPayType.defaultamt == 0)
                        {
                            txtCash.Text = thisCurrentCart.totalpayment.ToString("f2");
                        }
                        else
                        {
                            txtCash.Text = SelectPayType.defaultamt.ToString("f2");
                        }

                        NeedClearTxt = true;
                    }
                }
                


                dgvType.Rows.Clear();
                if (thisCurrentCart == null || thisCurrentCart.paymenttypes == null || thisCurrentCart.paymenttypes.otherpaytypeinfo == null || thisCurrentCart.paymenttypes.otherpaytypeinfo.Count == 0)
                {
                    return;
                }

                if (SelectPayType == null)
                {
                    //SelectPayType=thisCurrentCart.paymenttypes.otherpaytypeinfo[0];
                }
               
                    List<Image> lstimg = new List<Image>();
                    foreach (OrderPayTypeVo otherpay in thisCurrentCart.paymenttypes.otherpaytypeinfo)
                    {
                        if (SelectPayType!=null && SelectPayType.code == otherpay.code)
                        {
                            btnSelect.Text = otherpay.name;

                            lstimg.Add(MainModel.GetControlImage(btnSelect, otherpay));
                        }
                        else
                        {

                            if (!otherpay.discountsoverlay && thisCurrentCart.totalpromoamt > 0)
                            {
                                btnNotSelect.BackColor = Color.Silver;
                                btnNotSelect.ForeColor = Color.White;
                            }
                            else
                            {
                                btnNotSelect.BackColor = Color.White;
                                btnNotSelect.ForeColor = Color.Black;
                            }
                            btnNotSelect.Text = otherpay.name;
                            lstimg.Add(MainModel.GetControlImage(btnNotSelect, otherpay));
                        }
                    }

                    int emptycount = 4 - lstimg.Count % 4;

                    for (int i = 0; i < emptycount; i++)
                    {
                        lstimg.Add(Resources.ResourcePos.empty);
                    }

                    int rowcount = lstimg.Count / 4;
                    for (int i = 0; i < rowcount; i++)
                    {
                        dgvType.Rows.Add(lstimg[i * 4 + 0], lstimg[i * 4 + 1], lstimg[i * 4 + 2], lstimg[i * 4 + 3]);
                    }


               
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载其他支付方式异常"+ex.Message);
            }
        }

        //退出清空 其他支付选择
        private void lbtnCancle_Click(object sender, EventArgs e)
        {

            LstOtherPayInfos = null;
            this.DialogResult = DialogResult.Cancel;
            otherPayResult = null;
            this.Close();
        }

        HttpUtil httputil = new HttpUtil();
        private void btnOK_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCash.Text))
                {
                    MainModel.ShowLog("请输入金额", false);
                    return;
                }
                if (SelectPayType == null)
                {
                    MainModel.ShowLog("请选择支付方式",false);
                    return;
                }
                otherPayResult = new OtherPayResult();
                if (SelectPayType.needcouponcode) //东方福利网通过券码获取金额
                {
                    string ErrorMsg="";
                    this.Enabled = false;
                    LoadingHelper.ShowLoadingScreen();
                    ValidateOuterCoupon validataoutercoupon = httputil.GetValidateOuterCoupon(txtCash.Text, ref ErrorMsg);
                    LoadingHelper.CloseForm();
                    this.Enabled = true;
                    if (!string.IsNullOrEmpty(ErrorMsg) || validataoutercoupon==null)
                    {
                        MainModel.ShowLog(ErrorMsg, false);
                        return;
                    }


                    otherPayResult.otherpayamt = validataoutercoupon.amount;
                    otherPayResult.otherpaycouponcode = validataoutercoupon.couponcode;
                    otherPayResult.otherpaytype = SelectPayType.code;
                }
                else
                {
                    otherPayResult.otherpayamt = Convert.ToDecimal(txtCash.Text);
                    otherPayResult.otherpaytype = SelectPayType.code;
                }
                
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {

            }
        }


        private void dgvType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Image selectimg = (Image)dgvType.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (selectimg == null || selectimg == Resources.ResourcePos.empty || selectimg.Tag == null)
                {
                    return;
                }

                SelectPayType =(OrderPayTypeVo) selectimg.Tag;

                if (!SelectPayType.discountsoverlay && thisCurrentCart.totalpromoamt > 0)
                {
                    MainModel.ShowLog("订单有优惠,无法使用该支付方式",false);
                    return;
                }             

                LoadPayType();
                UpdateDgvSelectType();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("选择支付方式异常"+ex.Message,true);
            }

        }

        #region  小键盘按键
        private void btn_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            try
            {
                if (NeedClearTxt)
                {
                    txtCash.Clear();
                    NeedClearTxt = false;
                }
                
                //小数点后允许一位  现金抹零后不允许再输入零
                if (txtCash.Text.Length > 3 && txtCash.Text.Substring(txtCash.Text.Length - 3, 1) == ".")
                {
                    return;
                }
                //限制金额不超过100000

                //第一位是0 后面只能输入.
                if (txtCash.Text == "0")
                {
                    return;
                }

                //decimal CheckDecimal = Convert.ToDecimal(txtCash.Text + btn.Text);


                txtCash.Text += btn.Text;
            }
            catch
            {

            }



        }

        private void btnDot_Click(object sender, EventArgs e)
        {


            if (txtCash.Text.Length > 0 && !txtCash.Text.Contains("."))
            {
                txtCash.Text += ".";
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {

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


        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                if (keyData == Keys.Enter)
                {
                    btnOK_ButtonClick(null,null);
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                decimal tempcash = 0;
                if (string.IsNullOrEmpty(txtCash.Text))
                {
                    MainModel.ShowLog("请输入金额", false);
                    return;
                }

                if (SelectPayType == null)
                {
                    MainModel.ShowLog("请选择支付方式",false);
                    return;
                }

                if (thisCurrentCart.totalpayment == 0)
                {
                    MainModel.ShowLog("剩余应付金额为0,请先删除其他支付后再操作", false);
                    return;
                }
               

                if (thisCurrentCart.otherpayinfos == null)
                {
                    thisCurrentCart.otherpayinfos = new List<OtherPayInfoEntity>();
                }

                OtherPayInfoEntity otherpay = new OtherPayInfoEntity();

                if (SelectPayType.needcouponcode) //东方福利网通过券码获取金额
                {

                    //判断是否有特殊字符（数字和字母外）
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtCash.Text, "^[0-9a-zA-Z]+$"))
                    {
                        //System.Diagnostics.Debug.WriteLine("是符合要求字符");
                        MainModel.ShowLog("不能输入特殊字符", false);
                        return;
                    }

                    string ErrorMsg = "";
                    this.Enabled = false;
                    LoadingHelper.ShowLoadingScreen();
                    PaymentCouponDetail paymentcoupondetail = httputil.GetPaymentCouponDetail(txtCash.Text, ref ErrorMsg);
                    LoadingHelper.CloseForm();
                    this.Enabled = true;
                    if (!string.IsNullOrEmpty(ErrorMsg) || paymentcoupondetail == null)
                    {
                        MainModel.ShowLog(ErrorMsg, false);
                        return;
                    }

                    if (!paymentcoupondetail.availablecustomerpaycode.Contains(SelectPayType.code))
                    {
                        MainModel.ShowLog("券码不适用于当前支付方式",false);
                        return;
                    }

                    string couponpassword=null;
                    if (paymentcoupondetail.needpassword && !PayHelper.ShowFormCouponPwd(txtCash.Text,out couponpassword))
                    {
                        this.Activate();
                        return;
                    }
                    this.Activate();


                    tempcash = paymentcoupondetail.amount;
                    otherpay.payamt = paymentcoupondetail.amount;
                    otherpay.paycouponcode = paymentcoupondetail.couponcode;
                    otherpay.payname = SelectPayType.name;
                    otherpay.paypromoamt = paymentcoupondetail.amount;
                    otherpay.paytype = SelectPayType.code;

                    otherpay.paypassword = couponpassword;
                }
                else
                {
                    tempcash = Convert.ToDecimal(txtCash.Text);
                    if (tempcash > thisCurrentCart.totalpayment && SelectPayType.defaultamt==0)
                    {
                        MainModel.ShowLog("支付金额之和不可大于剩余应付金额", false);
                        return;
                    }

                    otherpay.payamt = tempcash;
                    otherpay.payname = SelectPayType.name;
                    otherpay.paypromoamt = tempcash;
                    otherpay.paytype = SelectPayType.code;
                }

                //如果以存在该类型优惠 则修改金额
                List<OtherPayInfoEntity> lsttempotherpay = thisCurrentCart.otherpayinfos.Where(r => r.paytype == SelectPayType.code).ToList();
                if (lsttempotherpay != null && lsttempotherpay.Count > 0)
                {
                    lsttempotherpay[0].payamt = tempcash;
                    lsttempotherpay[0].paypromoamt = tempcash;
                }
                else
                {
                    thisCurrentCart.otherpayinfos.Add(otherpay);
                }

                txtCash.Clear();
                RefreshCart();

            }
            catch (Exception ex)
            {

            }
        }

        private void btnConfirmPay_Click(object sender, EventArgs e)
        {
            try
            {

                int count = thisCurrentCart.otherpayinfos.Count;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {

            }
        }


        private bool RefreshCart()
        {
            try
            {
                this.Enabled = false;
                LoadingHelper.ShowLoadingScreen("加载中...");

                string ErrorMsgCart = "";
                int ResultCode = -1;
                Cart cart = httputil.RefreshCart(thisCurrentCart, ref ErrorMsgCart, ref ResultCode);


                if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                {
                    this.Enabled = true;
                    LoadingHelper.CloseForm();

                    MainModel.ShowLog(ErrorMsgCart, true);
                    return false;
                }
                else
                {
                    thisCurrentCart = cart;

                    UpdateDgvSelectType();
                    return true;
                }

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("刷新其他支付方式购物车异常" + ex.Message, true);
                return false;
            }
            finally
            {
                this.Enabled = true;
                LoadingHelper.CloseForm();
            }
        }

        private void UpdateDgvSelectType()
        {
            try{

                dgvSelectType.Rows.Clear();

                LstOtherPayInfos = thisCurrentCart.otherpayinfos;

                lblTotalPayMent.Text = "￥" + thisCurrentCart.totalpayment.ToString("f2");

                if (thisCurrentCart==null || thisCurrentCart.otherpayinfos==null || thisCurrentCart.otherpayinfos.Count==0)
                {
                    return;
                }

                foreach (OtherPayInfoEntity otherpayinfo in thisCurrentCart.otherpayinfos)
                {
                    Image imgeamt;

                    if (SelectPayType != null && otherpayinfo.paytype == SelectPayType.code)
                    {
                        btnAmtSelect.Text = otherpayinfo.payamt.ToString();
                        imgeamt = MainModel.GetControlImage(btnAmtSelect);
                    }
                    else
                    {
                        btnAmtNotSelect.Text = otherpayinfo.payamt.ToString();
                        imgeamt = MainModel.GetControlImage(btnAmtNotSelect);
                    }

                    dgvSelectType.Rows.Add(otherpayinfo.paytype, bmpCash, otherpayinfo.payname, imgeamt, bmpDelete);

                }
                dgvSelectType.ClearSelection();

                //int selectrowindex =-1;

                //for(int i=0;i<thisCurrentCart.otherpayinfos.Count;i++){
                //    dgvSelectType.Rows.Add(thisCurrentCart.otherpayinfos[i].paytype, bmpCash, thisCurrentCart.otherpayinfos[i].payname, thisCurrentCart.otherpayinfos[i].payamt.ToString(), bmpDelete);

                //    if (SelectPayType != null && thisCurrentCart.otherpayinfos[i].paytype == SelectPayType.code)
                //    {
                //        selectrowindex = i;
                //    }
                //}
                //dgvSelectType.ClearSelection();
                //if (selectrowindex > -1)
                //{
                //    dgvSelectType.Rows[selectrowindex].DefaultCellStyle.BackColor = Color.FromArgb(205, 232, 248);
                //}
                
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("刷新其他支付方式列表异常" + ex.Message, true);
            }
        }

        private void dgvSelectType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                string operaitoncode = dgvSelectType.Rows[e.RowIndex].Cells[0].Value.ToString();

                if (e.ColumnIndex == 4)
                {
                    thisCurrentCart.otherpayinfos.RemoveAll(r => r.paytype == operaitoncode);
                    RefreshCart();
                }
                else
                {
                    SelectPayType = thisCurrentCart.paymenttypes.otherpaytypeinfo.FirstOrDefault(r => r.code == operaitoncode);
                    NeedClearTxt = true;
                    LoadPayType();
                    UpdateDgvSelectType();
                }

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("操作选择支付方式异常"+ex.Message,true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dgvSelectType.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(205, 232, 248);
            dgvType.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(205, 232, 248);
        }

    }

    public class OtherPayResult
    {
        /// <summary>
        /// 多方支付 金额
        /// </summary>
        public decimal otherpayamt { set; get; }
        /// <summary>
        /// 多方支付方式  key
        /// </summary>
        public string otherpaytype { set; get; }

        /// <summary>
        /// 多方支付 兑换券码（仅东方福利网）
        /// </summary>
        public string otherpaycouponcode { set; get; }
    }
}
