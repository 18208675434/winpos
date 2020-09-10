using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.PayUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard
{
    public partial class FormGiftCard : Form
    {

        #region  成员变量

        /// <summary>
        /// this.enable=false; 页面不可用页面不可控；  通过该标志控制页面是否可用
        /// </summary>
        private bool IsEnable = true;

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        /// <summary>
        /// 礼品卡接口访问类
        /// </summary>
        GiftCardHttp gifthttputil = new GiftCardHttp();

        /// <summary>
        /// 公共接口访问类
        /// </summary>
        HttpUtil httputil = new HttpUtil();

        /// <summary>
        /// 当前购物车信息
        /// </summary>
        private CartAloneUpdate CurrentCart = new CartAloneUpdate();

        private bool IsRun = true;

        /// <summary>
        /// 扫描到数据直接扔进来，单独开线程处理
        /// </summary>
        Queue<string> QueueScanCode = new Queue<string>();
        /// <summary>
        /// 购卡人会员信息
        /// </summary>
        private Member CurrentMember = null;
        /// <summary>
        /// 绑定会员信息
        /// </summary>
        private Member CurretnBindingMember = null;
        #endregion
        #region 页面加载与退出
        public FormGiftCard()
        {
            InitializeComponent();
        }

        private void FormGiftCard_Shown(object sender, EventArgs e)
        {
            pnlLoading.Visible = false;
            lblToast.Visible = false;
            Application.DoEvents();
            Thread threadScanCode = new Thread(ScanCodeThread);
            threadScanCode.IsBackground = false;
            threadScanCode.Start();

            GiftCardMediaHelper.ShowFormGiftCardMedia();
        }

        private void FormGiftCard_FormClosing(object sender, FormClosingEventArgs e)
        {
            GiftCardMediaHelper.CloseFormGiftCartMedia();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            IsRun = false;
            this.Close();
        }

        #endregion

        /// <summary>
        /// 购买记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGiftCardRecord_Click(object sender, EventArgs e)
        {
            GiftCardHelper.ShowFormGiftCardRecord();
        }

        /// <summary>
        /// 输入礼品卡卡号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInputCardNo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                ShowLoading(true, false);

                string number = NumberHelper.ShowFormNumber("礼品卡卡号",NumberType.GiftCardNo,true);
                if (!string.IsNullOrEmpty(number))
                {
                    QueueScanCode.Enqueue(number);
                }
                

                ShowLoading(false, true);
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);

                LogManager.WriteLog("会员登录异常" + ex.Message);
            }

        }


        #region  OTHER

        private void ShowLog(string msg, bool iserror)
        {
            ParameterizedThreadStart Pts = new ParameterizedThreadStart(showlogthread);
            Thread threadmqtt = new Thread(Pts);
            threadmqtt.IsBackground = true;
            threadmqtt.Start(msg);

            if (iserror)
            {
                LogManager.WriteLog(msg);
            }
        }

        private void showlogthread(object obj)
        {
            try
            {
                lblToast.Text = obj.ToString();

                lblToast.Left = (this.Width - lblToast.Width) / 2;
                if (this.IsHandleCreated)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        lblToast.Visible = true;
                    }));
                }
                else
                {
                    lblToast.Visible = true;
                }

                Thread.Sleep(1000);
                if (this.IsHandleCreated)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        lblToast.Visible = false;
                    }));
                }
                else
                {
                    lblToast.Visible = false;

                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="showloading">显示等待框</param>
        /// <param name="isenable">页面是否可操作</param>
        private void ShowLoading(bool showloading, bool isenable)
        {
            try
            {
                IsEnable = isenable;
                if (this.IsHandleCreated)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        pnlLoading.Visible = showloading;
                    }));
                }
                else
                {
                    pnlLoading.Visible = showloading;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示等待异常" + ex.Message);
            }

        }


        private void ClearForm()
        {
            try
            {
                CurrentCart = new CartAloneUpdate();
                CurrentMember = null;
                CurretnBindingMember = null;
                ClearMember();
                ClearBindingMember();
                dgvCart.Rows.Clear();

                lblProCount.Text = "(" + 0 + "件商品)";

                lblCartTotal.Text = "￥0.00";

                pnlPayByCash.Tag = 0;
                pnlPayByCash.BackColor = Color.Silver;

                pnlPayByOnLine.Tag = 0;
                pnlPayByOnLine.BackColor = Color.Silver;
            }
            catch { }
        }
        #endregion

        #region  会员信息
        private void btnLoadPhone_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                ShowLoading(true, false);
                string numbervalue = NumberHelper.ShowFormNumber("输入会员手机号", NumberType.MemberCode,true);

                if (!string.IsNullOrEmpty(numbervalue))
                {
                    string ErrorMsgMember = "";
                    Member member = httputil.GetMember(numbervalue, ref ErrorMsgMember);

                    if (ErrorMsgMember != "" || member == null) //会员不存在
                    {
                        ShowLog(ErrorMsgMember, false);

                        if (MainModel.CurrentMember != null)
                        {
                            ClearMember();
                        }
                    }
                    else
                    {
                        LoadMember(member);

                        CurrentCart.memberid = member.memberinformationresponsevo.memberid;
                    }
                }
                ShowLoading(false, true);
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);

                LogManager.WriteLog("会员登录异常" + ex.Message);
            }
        }

        private void btnLoadBindingPhone_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                ShowLoading(true, false);
                string numbervalue = NumberHelper.ShowFormNumber("输入会员手机号", NumberType.MemberCode,true);
                if (!string.IsNullOrEmpty(numbervalue))
                {
                    string ErrorMsgMember = "";
                    Member member = httputil.GetMember(numbervalue, ref ErrorMsgMember);

                    if (ErrorMsgMember != "" || member == null) //会员不存在
                    {
                        ShowLog(ErrorMsgMember, false);

                        if (MainModel.CurrentMember != null)
                        {
                            ClearBindingMember();
                        }
                    }
                    else
                    {
                        LoadBindingMember(member);

                        
                    }
                }
                ShowLoading(false, true);
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);

                LogManager.WriteLog("会员登录异常" + ex.Message);
            }
        }
        private void LoadMember(Member member)
        {
            try
            {
                CurrentMember = member;
                if (this.IsHandleCreated)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {

                        pnlMemberInfo.Visible = true;
                        pbtnExitMember.Visible = true;
                        btnLoadPhone.Visible = false;

                        lblMemberPhone.Text = member.memberheaderresponsevo.mobile;
                        lblMemberBalance.Text = "￥" + member.barcoderecognitionresponse.balance.ToString("f2");
                        lblMemberCredit.Text = CurrentMember.creditaccountrepvo.availablecredit.ToString();
                    }));
                }
            }
            catch (Exception ex)
            {
                ShowLog("加载会员异常" + ex.StackTrace, true);
            }
        }
        private void ClearMember()
        {
            try
            {
                CurrentMember = null;
                CurrentCart.memberid = "";
                this.Invoke(new InvokeHandler(delegate()
                {
                    pnlMemberInfo.Visible = false;
                    pbtnExitMember.Visible = false;
                    btnLoadPhone.Visible = true;
                }));
            }
            catch (Exception ex)
            {
                ShowLog("清空会员异常", true);
            }
        }


        private void LoadBindingMember(Member member)
        {
            try
            {
                CurretnBindingMember = member;
                if (this.IsHandleCreated)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        lblBindingPhone.Visible = true;
                        btnLoadBindingPhone.Visible = false;
                        pbtnExitBindingMember.Visible = true;
                        lblBindingPhone.Text = "会员手机号：" + member.memberheaderresponsevo.mobile;

                        if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count>0)
                        {
                            UploadDgvCart();
                        }

                        GiftCardMediaHelper.LoadMember(CurretnBindingMember);
                    }));
                }
            }
            catch (Exception ex)
            {
                ShowLog("加载会员异常" + ex.StackTrace, true);
            }
        }

        private void ClearBindingMember()
        {
            try
            {
                CurretnBindingMember = null;
                this.Invoke(new InvokeHandler(delegate()
                {
                    lblBindingPhone.Visible = false;
                    btnLoadBindingPhone.Visible = true;
                    pbtnExitBindingMember.Visible = false;

                    if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                    {
                        UploadDgvCart();
                    }

                    GiftCardMediaHelper.ClearMember();
                }));
            }
            catch (Exception ex)
            {
                ShowLog("清空会员异常", true);
            }
        }
        #endregion

        #region 购物车
        StringBuilder scancode = new StringBuilder();
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {              
                switch (keyData)
                {
                    //不同键盘数字键值不同
                    case Keys.D0: scancode.Append("0"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D1: scancode.Append("1"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D2: scancode.Append("2"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D3: scancode.Append("3"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D4: scancode.Append("4"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D5: scancode.Append("5"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D6: scancode.Append("6"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D7: scancode.Append("7"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D8: scancode.Append("8"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D9: scancode.Append("9"); return !base.ProcessDialogKey(keyData); break;

                    case Keys.NumPad0: scancode.Append("0"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad1: scancode.Append("1"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad2: scancode.Append("2"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad3: scancode.Append("3"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad4: scancode.Append("4"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad5: scancode.Append("5"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad6: scancode.Append("6"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad7: scancode.Append("7"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad8: scancode.Append("8"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad9: scancode.Append("9"); return !base.ProcessDialogKey(keyData); break;

                    //case Keys.Back: AddNum(0, true); return base.ProcessDialogKey(keyData); break;
                    //case Keys.Enter: QueueScanCode.Enqueue(scancode.ToString()); scancode = new StringBuilder(); return !base.ProcessDialogKey(keyData); break;
                }
                if (keyData == Keys.Enter)
                {
                    QueueScanCode.Enqueue(scancode.ToString().Trim());
                    scancode = new StringBuilder();
                    return false;
                }
                if (keyData == Keys.Space)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("监听键盘事件异常" + ex.Message);
                return false;
            }
        }

        private void ScanCodeThread(object obj)
        {
            while (IsRun)
            {
                if (QueueScanCode.Count > 0 && IsEnable)
                {
                    try
                    {
                        string logCode = "";
                        ShowLoading(true, false);

                        List<string> LstScanCode = new List<string>();

                        while (QueueScanCode.Count > 0)
                        {
                            string tempcode = QueueScanCode.Dequeue();
                            logCode += tempcode + " ";
                            if (!string.IsNullOrEmpty(tempcode))
                            {
                                LstScanCode.Add(tempcode);
                            }
                        }
                        List<GiftCardDetail> LstGiftCardDetail = new List<GiftCardDetail>();
                        foreach (string scancode in LstScanCode)
                        {
                            string errormsg = "";
                            int errorcode=-1;
                            GiftCardDetail giftcarddetail = gifthttputil.GetPrepaiidCardDetail(scancode, ref errormsg,ref errorcode);

                            if (!string.IsNullOrEmpty(errormsg) || giftcarddetail == null)
                            {
                                ShowLoading(false, true);
                                ShowLog(errormsg, false);
                            }
                            else
                            {
                                LstGiftCardDetail.Add(giftcarddetail);
                            }
                        }

                        if (LstGiftCardDetail != null && LstGiftCardDetail.Count > 0)
                        {
                            InsertToCart(LstGiftCardDetail);
                        }

                        RefreshCart();
                        Thread.Sleep(10);
                    }
                    catch (Exception ex)
                    {
                        ShowLoading(false, true);// LoadingHelper.CloseForm();//关闭
                        LogManager.WriteLog("ERROR", "扫描数据处理异常：" + ex.Message);
                    }
                    finally
                    {
                        ShowLoading(false, true);// LoadingHelper.CloseForm();
                    }
                }

                Thread.Sleep(100);
            }
        }


        private void InsertToCart(List<GiftCardDetail> LstGiftCardDetail)
        {
            if (LstGiftCardDetail == null || LstGiftCardDetail.Count == 0)
            {
                return;
            }

            if (CurrentCart == null)
            {
                CurrentCart = new CartAloneUpdate();
            }

            if (CurrentCart.requestproducts == null)
            {
                CurrentCart.requestproducts = new List<CardProduct>();
            }

            if (CurrentCart.products != null && CurrentCart.products.Count > 0)
            {
                CurrentCart.requestproducts.AddRange(CurrentCart.products);
            }

            foreach (GiftCardDetail gift in LstGiftCardDetail)
            {
                CardProduct pro = new CardProduct();
                pro.addtime = MainModel.getStampByDateTime(DateTime.Now);

                pro.bindcardsecret = 0; //输入扫描卡号  时未输入秘钥

                pro.cardnumber = gift.cardno.ToString();
                pro.qty = 1;
                pro.saleprice = gift.amount;

                CurrentCart.requestproducts.Add(pro);
            }

            CurrentCart.shopid = MainModel.CurrentShopInfo.shopid;
            CurrentCart.tenantid = MainModel.CurrentShopInfo.tenantid;
            CurrentCart.carttype = "gift.card";

        }

        private bool RefreshCart()
        {
            try
            {
                // isGoodRefresh = false;
                DateTime starttime = DateTime.Now;
                string ErrorMsgCart = "";
                int ResultCode = -1;

                if (CurrentCart.requestproducts != null && CurrentCart.requestproducts.Count > 0)
                {
                    CartAloneUpdate tempcart = gifthttputil.CartAloneUpdate(CurrentCart, ref ErrorMsgCart, ref ResultCode);

                    if (!string.IsNullOrEmpty(ErrorMsgCart) || tempcart == null)
                    {
                        ShowLog(ErrorMsgCart, false);
                       // return false;
                    }
                    else
                    {
                        CurrentCart = tempcart;
                    }
                    
                }
              

               

                UploadDgvCart();
                //lbl
                return true;
                           
            }
            catch (Exception ex)
            {

                ShowLog("刷新购物车异常：" + ex.Message, true);
                return false;
            }
        }

        private void UploadDgvCart()
        {
            Invoke((new Action(() =>
            {
                dgvCart.Rows.Clear();

                if (CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    foreach (CardProduct pro in CurrentCart.products)
                    {
                        dgvCart.Rows.Add(GetDgvRow(pro, 0));
                    }

                    lblProCount.Text = "(" + CurrentCart.products.Count + "件商品)";

                    lblCartTotal.Text = "￥" + CurrentCart.pspamt.ToString("f2");

                    pnlPayByCash.Tag = 1;
                    pnlPayByCash.BackColor = Color.FromArgb(255, 146, 27);

                    pnlPayByOnLine.Tag = 1;
                    pnlPayByOnLine.BackColor = Color.FromArgb(255, 70, 21);
                }
                else
                {
                    lblProCount.Text = "(" + 0 + "件商品)";

                    lblCartTotal.Text = "￥0.00";

                        pnlPayByCash.Tag = 0;
                        pnlPayByCash.BackColor = Color.Silver;
                      
                        pnlPayByOnLine.Tag = 0;
                        pnlPayByOnLine.BackColor = Color.Silver;
                }


                GiftCardMediaHelper.UpdateCartInfo(CurrentCart);
            })));
        }


        private void dgvCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (e.RowIndex < 0)
                    return;

                Bitmap bmp = (Bitmap)dgvCart.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                CardProduct pro = (CardProduct)bmp.Tag;


                Point po = GlobalUtil.GetCursorPos();
                //删除礼品卡
                if (po.X < (dgvCart.Left + picDelete.Right + 10) && po.X > (dgvCart.Left + picDelete.Left - 10))
                {
                    if (ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("确认删除？", pro.title +"\r\n"+ pro.cardnumber))
                    {
                        CurrentCart.products.ForEach(r => {
                            if (r.cardnumber == pro.cardnumber)
                            {
                                CurrentCart.products.Remove(r);
                            }
                        });
                    }

                    CurrentCart.requestproducts = CurrentCart.products;
                    RefreshCart();
                }
                    //绑定秘钥
                else if (CurretnBindingMember != null && pro.bindcardsecret == 0 && po.X < (dgvCart.Left + btnBindingPwd.Right + 10) && po.X > (dgvCart.Left + btnBindingPwd.Left - 10))
                {

                    if (GiftCardHelper.BindingMember(pro, CurrentMember == null ? "" : CurrentMember.memberid, CurretnBindingMember.memberheaderresponsevo.mobile))
                    {
                        CurrentCart.products.ForEach(r =>
                        {
                            if (r.cardnumber == pro.cardnumber)
                            {
                                r.bindcardsecret = 1;
                            }
                        });
                        CurrentCart.requestproducts = CurrentCart.products;
                        RefreshCart();
                    }

                    
                    
                }
                //else if (pro.goodstagid != 0 && po.X < (dgvCart.Left + lblProNum.Right + 10) && po.X > (dgvCart.Left + lblProNum.Left - 10))
                //{
                //    decimal newweight = BrokenHelper.ShowBrokenNumber(pro.skuname);

                //    if (newweight > 0)
                //    {
                //        for (int i = 0; i < CurrentCart.products.Count; i++)
                //        {
                //            if (CurrentCart.products[i].skucode == pro.skucode && CurrentCart.products[i].specnum == pro.specnum)
                //            {
                //                CurrentCart.products[i].specnum = newweight;
                //                CurrentCart.products[i].price.specnum = newweight;
                //                CurrentCart.products[i].num = 1;
                //                break;
                //            }
                //        }
                //    }

                //    UploadOffLineDgvCart();

                //}
                //else
                //{
                //    OperationProduct = pro;
                //    btnDeletePro.BackColor = Color.FromArgb(22, 135, 206);
                //    btnChangePrice.BackColor = Color.FromArgb(22, 135, 206);
                //    btnDiscount.BackColor = Color.FromArgb(22, 135, 206);
                //}

            }
            catch (Exception ex)
            {
                ShowLog("操作购物车商品异常" + ex.Message, true);
            }
            finally
            {
                btnScan.Select();
            }
        }


        private Bitmap GetDgvRow(CardProduct pro, int rownum)
        {
            try
            {
                Bitmap bmpPro;

                lblSkuName.Text = pro.title;
                lblSkuCode.Text = pro.cardnumber;

                lblSaleprice.Text = "￥" + pro.saleprice.ToString("f2");
                lblTotalPrice.Text = "￥" + pro.saleprice.ToString("f2");

              ;

                if (CurretnBindingMember == null)
                {
                    lblBindingInfo.Visible = false;
                    btnBindingPwd.Visible = false;
                }
                else if (pro.bindcardsecret == 0)
                {
                    lblBindingInfo.Visible = false;
                    btnBindingPwd.Visible = true;
                }
                else if (pro.bindcardsecret == 1)
                {
                    lblBindingInfo.Visible = true;
                    btnBindingPwd.Visible = false;
                }

                  bmpPro = new Bitmap(pnlCartItem.Width, pnlCartItem.Height);
                bmpPro.Tag = pro;
                pnlCartItem.DrawToBitmap(bmpPro, new Rectangle(0, 0, pnlCartItem.Width, pnlCartItem.Height));

                bmpPro.MakeTransparent(Color.White);

                return bmpPro;
            }
            catch (Exception ex)
            {
                ShowLog("解析商品信息异常" + ex.Message, true);
                return null;
            }
        }

        private void pbtnExitMember_Click(object sender, EventArgs e)
        {
            ClearMember();
        }

        private void pbtnExitBindingMember_Click(object sender, EventArgs e)
        {
            ClearBindingMember();
        }

        #endregion

        #region 支付
        private void pnlPayByOnLine_Click(object sender, EventArgs e)
        {

        }

        private void pnlPayByCash_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable || pnlPayByCash.Tag == null || pnlPayByCash.Tag.ToString() == "0")
                {
                    return;
                }

                IsEnable = false;
                decimal cash = 0;
                if (GiftCardHelper.ShowFormGiftCardByCash(CurrentCart, out cash))
                {
                    CreateGiftCardOrder(CurrentCart,cash);
                }

                IsEnable = true;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("现金支付异常"+ex.Message,true);
            }
        }


        private void CreateGiftCardOrder(CartAloneUpdate cart,decimal cash)
        {
            try
            {
                CreateCardOrderPara para = new CreateCardOrderPara();
                para.carttype = "gift.card";
                para.cashier = MainModel.CurrentUser.nickname;
                para.devicesn = MainModel.DeviceSN;
                para.fromwinpos = true;
                para.memberid = cart.memberid;
                para.pcashpayamt = cash;
                if (CurretnBindingMember != null)
                {
                    para.phone = CurretnBindingMember.memberheaderresponsevo.mobile;
                }
                //para.phone=currentcart.
                para.products = cart.products;
                para.shopid = MainModel.CurrentShopInfo.shopid;
                para.tenantid = MainModel.CurrentShopInfo.tenantid;


                string errormsg = "";

                CreateCardOrder result = gifthttputil.CreateCardOrder(para, ref errormsg);

                LogManager.WriteLog("DEBUG","礼品卡订单号"+result.orderid.ToString());
                if (!string.IsNullOrEmpty(errormsg) || result == null)
                {
                    MainModel.ShowLog(errormsg, false);
                    return;
                }

                string errorsuccess = "";
                GiftCardPaySuccess paysuccess = gifthttputil.CardPaySuccess(result.orderid.ToString(),ref errorsuccess);

                if (!string.IsNullOrEmpty(errormsg) || result == null)
                {
                    MainModel.ShowLog(errorsuccess, false);
                    return;
                }

               // PayHelper.ShowFormPaySuccess(result.orderid.ToString());

                ClearForm();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("创建礼品卡订单异常"+ex.Message);
            }

        }

        
        #endregion


    }
}
