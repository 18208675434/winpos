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

        private Image imgSelect;
        private Image imgNotSelect;

        public OtherPayType SelectPayType = null;

        public Cart thisCurrentCart = null;

        public OtherPayResult otherPayResult = null;
        public FormPayByOther(Cart cart)
        {
            try
            {
                InitializeComponent();
                otherPayResult = new OtherPayResult();
                SelectPayType = null;
                thisCurrentCart = cart;
            }
            catch (Exception ex)
            {
            }

        }

        private void FormPayByOther_Shown(object sender, EventArgs e)
        {
            imgSelect = MainModel.GetControlImage(cbtnSelect);
            imgNotSelect = MainModel.GetControlImage(cbtnNotSelect);
            txtCash.Text = (thisCurrentCart.totalpayment+thisCurrentCart.otherpayamt).ToString("f2");
            Application.DoEvents();
            LoadPayType();
        }


        private void LoadPayType()
        {
            try
            {

                if (SelectPayType == null && thisCurrentCart.otherpaytypeinfo.Count>0)
                {
                    SelectPayType=thisCurrentCart.otherpaytypeinfo[0];
                }
                dgvType.Rows.Clear();
                if (thisCurrentCart != null && thisCurrentCart.otherpaytypeinfo != null)
                {
                    List<Image> lstimg = new List<Image>();

                    dgvType.Rows.Clear();
                    foreach (OtherPayType otherpay in thisCurrentCart.otherpaytypeinfo)
                    {
                        if (SelectPayType == otherpay)
                        {
                            cbtnSelect.ShowText = otherpay.value;

                            lstimg.Add(MainModel.GetControlImage(cbtnSelect, otherpay));
                        }
                        else
                        {
                            cbtnNotSelect.ShowText = otherpay.value;
                            lstimg.Add(MainModel.GetControlImage(cbtnNotSelect, otherpay));
                        }
                    }

                    int emptycount = 3 - lstimg.Count % 3;

                    for (int i = 0; i < emptycount; i++)
                    {
                        lstimg.Add(Resources.ResourcePos.empty);
                    }

                    int rowcount = lstimg.Count / 3;
                    for (int i = 0; i < rowcount; i++)
                    {
                        dgvType.Rows.Add(lstimg[i * 3 + 0], lstimg[i * 3 + 1], lstimg[i * 3 + 2]);
                    }


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
                if (SelectPayType.key == "pay.other.dfflw") //东方福利网通过券码获取金额
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
                    otherPayResult.otherpaytype = SelectPayType.key;
                }
                else
                {
                    otherPayResult.otherpayamt = Convert.ToDecimal(txtCash.Text);
                    otherPayResult.otherpaytype = SelectPayType.key;
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

                SelectPayType =(OtherPayType) selectimg.Tag;

                if (SelectPayType.key == "pay.other.dfflw")
                {
                    lblShuiyin.Text = "请输入使用码";
                    btnBack.Width = btnOK.Width;
                    lblYuan.Visible = false;

                    txtCash.Text = "";
                }
                else
                {
                    lblShuiyin.Text = "请输入支付金额";
                    btnBack.Width = lblYuan.Left - btnBack.Left;
                    lblYuan.Visible = true;

                    txtCash.Text = (thisCurrentCart.totalpayment + thisCurrentCart.otherpayamt).ToString("f2");
                }

                LoadPayType();

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

                decimal CheckDecimal = Convert.ToDecimal(txtCash.Text + btn.Name.Replace("btn", ""));

                //if (CheckDecimal > 100000)
                //{
                //    return;
                //}
                txtCash.Text += btn.Name.Replace("btn", "");
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

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {

                lblShuiyin.Visible = string.IsNullOrEmpty(txtCash.Text);

                txtCash.Focus();
                this.txtCash.Select(this.txtCash.TextLength, 0);
            }
            catch { }
        }

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
