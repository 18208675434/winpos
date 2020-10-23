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
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine;
using ZhuiZhi_Integral_Scale_UncleFruit.PayUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormMemberCenter : Form
    {
        private HttpUtil httputil = new HttpUtil();
        PageBalanceDepositBill request = new PageBalanceDepositBill();
        public static decimal reward = 0;

        private Member CurrentMember = null;

        private ListAllTemplate CurrentTemplate = null;
        private CustomTemplateModel CustomTemplate = null;

        private string PassWord = "";

        private List<ListAllTemplate> LstTemplates = new List<ListAllTemplate>();
        private List<CustomTemplateModel> CustomTemp = new List<CustomTemplateModel>();

        public MemberCenterHttpUtil membercenterutil = new MemberCenterHttpUtil();

        bool IsEnable = true;
        ListAllTemplate listall = new ListAllTemplate();

        private Bitmap bmpCustom;
        public FormMemberCenter(Member member)
        {
            InitializeComponent();

            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
            CurrentMember = member;
        }

        private void FormMemberCenter_Shown(object sender, EventArgs e)
        {
            try
            {
                bmpCustom = (Bitmap)MainModel.GetControlImage(custom);

                lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;

                lblMenu.Text = MainModel.CurrentUser.nickname + ",你好 ";
                picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
                lblMenu.Left = picMenu.Right;
               

                Application.DoEvents();

                IsEnable = false;
                LoadingHelper.ShowLoadingScreen();


                UpdateMemberInfo();

                LoadTemplate(true);


                this.BeginInvoke(new Action(delegate()
                {
                    LoadBalanceConfigDetail();
                }));

                LoadingHelper.CloseForm();
                IsEnable = true;
                MemberCenterMediaHelper.ShowFormMainMedia();
                if (AllShowlstbmp != null && AllShowlstbmp.Count > 0)
                {
                    MemberCenterMediaHelper.UpdateDgvTemplate(AllShowlstbmp);
                }

                MemberCenterMediaHelper.UpdatememberInfo(lblPhone.Text, lblMemberInfo.Text, lblBalance.Text, lblCredit.Text, lblCreditAmount.Text, lblCoupon.Text,lblEntityCard.Text);
            }
            catch (Exception ex)
            {
                //MainModel.ShowLog("加载会员中心页面异常" + ex.Message, true);

                LogManager.WriteLog("加载会员中心页面异常" + ex.Message);
                LoadingHelper.CloseForm();

                IsEnable = true;
            }
        }

        /// <summary>
        /// 刷新会员中心会员信息
        /// </summary>
        public void UpdateMemberInfo()
        {
            string phone = CurrentMember.memberheaderresponsevo.mobile;

            if (phone.Length == 11)
            {
                string tempphone = phone.Substring(0, 3) + " " + phone.Substring(3, 4) + " " + phone.Substring(7, 4);
                phone = tempphone;
            }

            if (CurrentMember.mergememberrecordresponsevo != null)
            {
                lblNewPhoneDesc.Visible = true;
                lblNewPhone.Visible = true;
                lblNewPhone.Text = CurrentMember.mergememberrecordresponsevo.targetmobile;
            }

            lblPhone.Text = phone;
            btnChangePhone.Left = lblPhone.Right;

            string gender = CurrentMember.memberinformationresponsevo.gender == 0 ? "男" : "女";
            string birthday = CurrentMember.memberinformationresponsevo.birthdaystr;
            MainModel.memberid = CurrentMember.memberid;
            lblMemberInfo.Text = "性别：" + gender + " | " + "生日：" + birthday;

            lblBalance.Text = "￥" + CurrentMember.barcoderecognitionresponse.balance;

            lblCredit.Text = CurrentMember.creditaccountrepvo.availablecredit.ToString();

            lblCreditAmount.Text = "=" + CurrentMember.creditaccountrepvo.creditworth.ToString("f2") + "元";

            lblCreditAmount.Left = lblCredit.Right;

            this.BeginInvoke(new Action(delegate()
            {
                LoadCoupon();
            }));

            if (MainModel.CurrentMember.outentitycards != null)
            {
                lblEntityCard.Text = MainModel.CurrentMember.outentitycards.Count + "张";
                picEntityCard.Left = lblEntityCard.Right;
            }

            this.BeginInvoke(new Action(delegate()
            {
                LoadBalanceAccount();
            }));
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            ListAllTemplate.enable = false;
            this.Close();
        }

        private void pnlPayByOnLine_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (CurrentBalanceAccount != null && !CurrentBalanceAccount.haspaypassword && CurrentBalanceAccount.needinitpassword)
                {
                    MemberCenterHelper.ShowFormNoPayPwd();
                    return;
                }

                if (CurrentTemplate == null)
                {
                    MainModel.ShowLog("请选择充值金额", false);
                    return;
                }

                MemberTopUpPara para = new MemberTopUpPara();
                para.amount = CurrentTemplate.amount;
                para.memberid = Convert.ToInt64(CurrentMember.memberinformationresponsevo.memberid);
                para.paymode = "2";
                para.phone = CurrentMember.memberheaderresponsevo.mobile;
                para.shopid = MainModel.CurrentShopInfo.shopid;
                //0 代表自定义充值
                if (CurrentTemplate.id == 0)
                {
                    para.rewardamount = CurrentTemplate.rewardamount;
                }

                para.autoreward = !CurrentTemplate.customAndreward;


                string errormsg = "";
                long result = httputil.MemberTopUp(para, ref errormsg);

                if (!string.IsNullOrEmpty(errormsg))
                {
                    MainModel.ShowLog(errormsg, false);
                }
                else
                {
                    //DbJsonUtil.AddBalanceInfo("微信", para.amount);
                    if (MemberCenterHelper.ShowFormTopUpByOnline(result, CurrentMember.memberheaderresponsevo.mobile))
                    {
                        PrintUtil.PrintTopUp(result.ToString());
                        TopUpOK();
                    }

                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("在线充值异常" + ex.Message, true);
            }
        }

        private void pnlPayByCash_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (CurrentTemplate == null)
                {
                    MainModel.ShowLog("请选择充值金额", false);
                    return;
                }

                if (CurrentBalanceAccount != null && !CurrentBalanceAccount.haspaypassword && CurrentBalanceAccount.needinitpassword)
                {
                    MemberCenterHelper.ShowFormNoPayPwd();
                    return;
                }
                if (MemberCenterHelper.ShowFormTopUpByCash(CurrentTemplate.amount))
                {

                    MemberTopUpPara para = new MemberTopUpPara();
                    para.amount = CurrentTemplate.amount;
                    para.memberid = Convert.ToInt64(CurrentMember.memberinformationresponsevo.memberid);
                    para.paymode = "0";
                    para.phone = CurrentMember.memberheaderresponsevo.mobile;
                    para.shopid = MainModel.CurrentShopInfo.shopid;
                    //0 代表自定义充值
                    if (CurrentTemplate.id == 0)
                    {
                        para.rewardamount = CurrentTemplate.rewardamount;
                    }
                    para.autoreward = !CurrentTemplate.customAndreward;


                    string errormsg = "";
                    long result = httputil.MemberTopUp(para, ref errormsg);

                    if (!string.IsNullOrEmpty(errormsg))
                    {
                        MainModel.ShowLog(errormsg, false);
                    }
                    else
                    {
                        //DbJsonUtil.AddBalanceInfo("现金", para.amount);
                        PrintUtil.PrintTopUp(result.ToString());
                        TopUpOK();
                    }
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("现金充值异常" + ex.Message, true);
            }
        }

        private void lblTopUp_Click(object sender, EventArgs e)
        {
            //MemberCenterHelper.ShowFormSingleUserRechangeQuery();
            try
            {
                IsEnable = false;
                MemberCenterHelper.ShowFormSingleUserRechangeQuery(CurrentMember.memberheaderresponsevo.mobile);
                IsEnable = true;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("切换充值明细异常" + ex.Message, true);
            }
        }


        private int CurrentPage = 1;
        List<Bitmap> AllShowlstbmp = null;
        private void LoadTemplate(bool needrefresh)
        {
            try
            {
                dgvTemplate.Rows.Clear();

                if (MainModel.LstRechargeTemplates != null && MainModel.LstRechargeTemplates.Count > 0)
                {
                    LstTemplates = MainModel.LstRechargeTemplates;
                }
                if (LstTemplates == null || LstTemplates.Count == 0)
                {

                    string errormsg = "";
                    LstTemplates = httputil.ListAllTemplate(ref errormsg);
                    if (LstTemplates == null || !string.IsNullOrEmpty(errormsg))
                    {
                        MainModel.ShowLog(errormsg, false);
                        return;
                    }
                    MainModel.LstRechargeTemplates = LstTemplates;//刷新模板信息
                }

                AllShowlstbmp = new List<Bitmap>();

                foreach (ListAllTemplate template in LstTemplates)
                {
                    if (CurrentTemplate == null && template.enabled == true)
                    {
                        CurrentTemplate = template;
                    }
                    AllShowlstbmp.Add(GetItemImg(template));
                }

                if (MainModel.balanceconfigdetail != null && MainModel.balanceconfigdetail.customrechargeenable)
                {
                    if (CurrentTemplate.id == 0)
                    {
                        AllShowlstbmp.Add(GetItemImg(CurrentTemplate));
                    }
                    else
                    {
                        AllShowlstbmp.Add(bmpCustom);
                    }
                }

              

                //当前页展示的图片集合
               List<Bitmap> ThisPageShowlstbmp = new List<Bitmap>();

               if (AllShowlstbmp.Count > 12)
               {
                   rbtnPageUp.Visible = true;
                   rbtnPageDown.Visible = true;

                   rbtnPageUp.WhetherEnable = CurrentPage > 1;
                   rbtnPageDown.WhetherEnable = AllShowlstbmp.Count > CurrentPage * 11;

                   int startindex = (CurrentPage - 1) * 12;

                   int lastindex = Math.Min(AllShowlstbmp.Count - 1, startindex + 11);


                   ThisPageShowlstbmp = AllShowlstbmp.GetRange(startindex, lastindex - startindex + 1);

               }
               else
               {
                   rbtnPageUp.Visible = false ;
                   rbtnPageDown.Visible = false ;
                   ThisPageShowlstbmp = AllShowlstbmp;
                   CurrentPage = 1;
               }


               int emptycount = 3 - ThisPageShowlstbmp.Count % 3;

                for (int i = 0; i < emptycount; i++)
                {
                    ThisPageShowlstbmp.Add(Resources.ResourcePos.empty);
                }
                int rowcount = ThisPageShowlstbmp.Count / 3;

                for (int i = 0; i < rowcount; i++)
                {
                    dgvTemplate.Rows.Add(ThisPageShowlstbmp[i * 3 + 0], ThisPageShowlstbmp[i * 3 + 1], ThisPageShowlstbmp[i * 3 + 2]);
                }

                Application.DoEvents();
                MemberCenterMediaHelper.UpdateDgvTemplate(ThisPageShowlstbmp);
            }
            catch (Exception ex)
            {
                //MainModel.ShowLog("获取所有充值面额异常" + ex.Message, true);
            }
        }

        private void rbtnPageUp_ButtonClick(object sender, EventArgs e)
        {

            if (!IsEnable || !rbtnPageUp.WhetherEnable)
            {
                return;
            }
            CurrentPage--;
            LoadTemplate(false);
        }

        private void rbtnPageDown_ButtonClick(object sender, EventArgs e)
        {
            if (!IsEnable || !rbtnPageDown.WhetherEnable)
            {
                return;
            }
            CurrentPage++;
            LoadTemplate(false);
        }


        private Bitmap GetItemImg(ListAllTemplate template)
        {
            try
            {

                if (template.amount <= 5000)
                {

                    if (CurrentTemplate != null && template.id == CurrentTemplate.id)
                    {
                        lblAmount.ForeColor = Color.White;
                        lblAmountStr.ForeColor = Color.White;
                        pnlItem.BackColor = Color.FromArgb(0, 122, 204);
                    }
                    else
                    {
                        lblAmount.ForeColor = Color.Black;
                        lblAmountStr.ForeColor = Color.FromArgb(150, 150, 150);
                        pnlItem.BackColor = Color.White;
                    }

                }
                else
                {
                    lblAmount.ForeColor = Color.White;
                    lblAmountStr.ForeColor = Color.White;
                    pnlItem.BackColor = Color.FromArgb(200, 200, 200);
                }

                lblAmount.Text = template.amount.ToString("f2") + "元";

                lblAmountStr.Text = "";
                if (template.rewardamount > 0)
                {
                    lblAmountStr.Text = "赠" + template.rewardamount.ToString("f2") + "元";
                }

                lblAmountStr.Text += template.couponname;



                Bitmap b = (Bitmap)MainModel.GetControlImage(pnlItem);
                b.Tag = template;
                return b;

            }
            catch (Exception ex)
            {
                return Resources.ResourcePos.empty;
            }
        }

        private List<PromotionCoupon> CurrentLstCoupon = null;

        private void LoadCoupon()
        {
            try
            {
                string ErrorMsg = "";
                CurrentLstCoupon = httputil.MyCouponList(CurrentMember.memberinformationresponsevo.memberid, ref ErrorMsg);

                if (CurrentLstCoupon == null || !string.IsNullOrEmpty(ErrorMsg))
                {
                    MainModel.ShowLog(ErrorMsg, false);
                }
                else
                {
                    lblCoupon.Text = CurrentLstCoupon.Count + "张";
                    picCoupon.Left = lblCoupon.Right;
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载优惠券异常" + ex.Message, true);
            }
        }

        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();
        private void dgvTemplate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!IsEnable || e.RowIndex < 0)
                {
                    return;
                }

                Other.CrearMemory();
                Image selectimg = (Image)dgvTemplate.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;


                if (selectimg.Tag == null && selectimg != bmpCustom)  //空白单元格（无商品）
                {
                    return;
                }

                ListAllTemplate temp = (ListAllTemplate)selectimg.Tag;


                //自定义充值
                if (selectimg == bmpCustom || temp.id == 0)
                {
                    ListAllTemplate customtemplate = MemberCenterHelper.ShowFormCustomerChange();
                    this.Activate();
                    if (customtemplate != null)
                    {
                        CurrentTemplate = customtemplate;
                        LoadTemplate(false);
                    }

                    return;
                }



                if (temp.amount > 5000)
                {
                    return;
                }
                CurrentTemplate = (ListAllTemplate)selectimg.Tag;

                LoadTemplate(false);

                //ListAllTemplate.mount = temp.amount;
                //reward = temp.rewardamount;
                //if (ListAllTemplate.mount == 10)
                //{

                //    CurrentTemplate.id = 41;
                //}
                //else if (ListAllTemplate.mount == 100)
                //{
                //    CurrentTemplate.id = 42;
                //}
                //else if (ListAllTemplate.mount == 1000)
                //{
                //    CurrentTemplate.id = 43;
                //}
                //if (temp.id != 0)
                //{
                //    customdiscount.Text = "自定义金额";
                //    custommoney.Text = "+";
                //    custommoney.Font = new System.Drawing.Font("微软雅黑", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                //    customdiscount.Font = new System.Drawing.Font("微软雅黑", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                //    GetCustomImg(temp);
                //}

                //if (ListAllTemplate.mount == 10 || ListAllTemplate.mount == 100 || ListAllTemplate.mount == 1000)
                //{

                //    customdiscount.Text = "自定义金额";
                //    custommoney.Text = "+";
                //    custommoney.Font = new System.Drawing.Font("微软雅黑", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                //    customdiscount.Font = new System.Drawing.Font("微软雅黑", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                //    custom.BackColor = Color.White;
                //    custommoney.ForeColor = Color.Blue;
                //    customdiscount.ForeColor = Color.Blue;
                //    temp.able = true;
                //    GetCustomImg(temp);
                //    LoadTemplate(true);

                //    return;
                //}

                //if (temp.id == 0)
                //{

                //    FormCustomMoney money = new FormCustomMoney();
                //    asf.AutoScaleControlTest(money, 420, 197, 420 * MainModel.midScale, 197 * MainModel.midScale, true);
                //    money.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - money.Width) / 2, (Screen.AllScreens[0].Bounds.Height - money.Height) / 2);
                //    money.TopMost = true;
                //    BackHelper.ShowFormBackGround();
                //    //BackHelper.HideFormBackGround();
                //    money.ShowDialog();
                //    money.Dispose();
                //    money.Close();
                //    if (money.DialogResult == DialogResult.Cancel)
                //    {
                //        customdiscount.Text = "自定义金额";
                //        custommoney.Text = "+";
                //        custommoney.Font = new System.Drawing.Font("微软雅黑", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                //        customdiscount.Font = new System.Drawing.Font("微软雅黑", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                //        GetCustomImg(temp);
                //        return;
                //    }


                //    custommoney.Text = ListAllTemplate.CustomMoney.ToString() + "元";
                //    customdiscount.Text = ListAllTemplate.ZengCustomMoney;
                //    if (customdiscount.Text == "label3")
                //    {
                //        ListAllTemplate.enable = true;
                //        decimal de = Convert.ToDecimal(custommoney.Text.Replace("元", ""));
                //        ListAllTemplate.mount = de;
                //        customdiscount.Text = "赠0元";
                //        customdiscount.ForeColor = Color.Blue;
                //        customdiscount.Font = new System.Drawing.Font("微软雅黑", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                //        LoadTemplate(true);

                //        custom.BackColor = Color.Blue;
                //        custommoney.ForeColor = Color.White;
                //        customdiscount.ForeColor = Color.White;
                //    }
                //    else if (custommoney.Text == "0")
                //    {
                //        custommoney.Text = "+";
                //        custommoney.ForeColor = Color.Blue;
                //        custommoney.Font = new System.Drawing.Font("微软雅黑", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                //        custommoney.Text = MainModel.CoustomMoney;
                //        customdiscount.Text = MainModel.ZMoney;


                //    }
                //    else
                //    {

                //        listall.CustomId = 41;

                //        LoadTemplate(true);
                //        ListAllTemplate.enable = true;
                //        if (custommoney.Text != "+")
                //        {
                //            ListAllTemplate.mount = int.Parse(custommoney.Text.Replace("元", ""));
                //        }
                //        custom.BackColor = Color.Blue;
                //        custommoney.ForeColor = Color.White;
                //        customdiscount.ForeColor = Color.White;
                //        pnlItem.BackColor = Color.White;
                //        LoadTemplate(true);
                //    }
                //}

                //else
                //{
                //    if (CurrentBalanceAccount == null || (Convert.ToInt64(ListAllTemplate.CustomMoney) + Convert.ToInt64(ListAllTemplate.Money)) > 5000)
                //    {
                //        return;
                //    }

                //    CurrentTemplate = (ListAllTemplate)selectimg.Tag;

                //    LoadTemplate(false);
                //}


            }
            catch (Exception ex)
            {
                MainModel.ShowLog("选择充值项异常" + ex.Message, true);
            }
        }

        private ZtBalanceAccount CurrentBalanceAccount = null;
        public void LoadBalanceAccount()
        {
            try
            {
                string errormsg = "";
                CurrentBalanceAccount = httputil.ZtBalanceAccount(CurrentMember.memberinformationresponsevo.memberid, ref errormsg);

                if (!string.IsNullOrEmpty(errormsg) || CurrentBalanceAccount == null)
                {
                    MainModel.ShowLog(errormsg, false);
                }
                else
                {
                    //TODO

                    lblBalance.Text = "￥" + CurrentBalanceAccount.balance.ToString("f2");
                    MainModel.Balance = lblBalance.Text;

                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("获取会员账户余额异常" + ex.Message, true);
            }
        }


        private void TopUpOK()
        {
            try
            {
                IsEnable = false;
                listall.CustomId = 0;

                LoadingHelper.ShowLoadingScreen();
                MainModel.ShowLog("充值成功", false);


                CurrentTemplate = null;
                custommoney.Text = "+";
                custommoney.ForeColor = Color.Blue;
                custommoney.Font = new System.Drawing.Font("微软雅黑", 20, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                customdiscount.Text = "自定义金额";
                customdiscount.ForeColor = Color.Blue;
                customdiscount.Font = new System.Drawing.Font("微软雅黑", 16, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                LoadTemplate(false);
                LoadBalanceAccount();

                MemberCenterMediaHelper.UpdatememberInfo(lblPhone.Text, lblMemberInfo.Text, lblBalance.Text, lblCredit.Text, lblCreditAmount.Text, lblCoupon.Text,lblEntityCard.Text);


                IsEnable = true;
                LoadingHelper.CloseForm();

            }
            catch (Exception ex)
            {
                IsEnable = true;
                LoadingHelper.CloseForm();
                MainModel.ShowLog("刷新信息异常" + ex.Message, true);
            }
        }

        private void FormMemberCenter_FormClosing(object sender, FormClosingEventArgs e)
        {
            MemberCenterMediaHelper.CloseFormMainMedia();
            this.Dispose();
        }

        private void pnlCoupon_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentLstCoupon == null || CurrentLstCoupon.Count == 0)
                {
                    MainModel.ShowLog("暂无优惠券", false);
                    return;
                }

                MemberCenterHelper.ShowFormAllCoupon(CurrentLstCoupon);

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载优惠券异常" + ex.Message, true);
            }
        }

        private void pnlEntityCard_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainModel.CurrentMember.outentitycards == null || MainModel.CurrentMember.outentitycards.Count == 0)
                {
                    MainModel.ShowLog("暂无实体卡", false);
                    return;
                }

                if (MemberCenterHelper.ShowFormEntityCardList(MainModel.CurrentMember.outentitycards))
                {
                    UpdateMemberInfo();
                }

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载实体卡异常" + ex.Message, true);
            }
        }


        private void LoadBalanceConfigDetail()
        {
            try
            {
                string errormsg = "";
                BalanceConfigDetail configdetail = httputil.BalanceConfigDetail(ref errormsg);


                MainModel.balanceconfigdetail = configdetail;
                if (configdetail != null && configdetail.cashrechargeenableforpos)
                {
                    pnlPayByCash.Visible = true;

                    //pnlPayByOnLine.Width = pnlPayByCash.Width;

                    //picPayByOnLine.Left = (pnlPayByOnLine.Width - picPayByOnLine.Width - lblPayByOnLine.Width) / 2;

                    //lblPayByOnLine.Left = picPayByOnLine.Right;
                }
                else
                {
                    pnlPayByCash.Visible = false;
                    //pnlPayByOnLine.Width = dgvTemplate.Width;

                    //picPayByOnLine.Left = (pnlPayByOnLine.Width - picPayByOnLine.Width - lblPayByOnLine.Width) / 2;

                    //lblPayByOnLine.Left = picPayByOnLine.Right;
                }
            }
            catch (Exception ex) { }
        }

        private void btnChangePwd_Click(object sender, EventArgs e)
        {

            try
            {
                MemberCenterHelper.ShowFormSeavePassword(CurrentMember);
            }
            catch (Exception ex)
            { }
        }

        private void btnForgetPwd_Click(object sender, EventArgs e)
        {
            try
            {
                FormIsokCancle isok = new FormIsokCancle();
                asf.AutoScaleControlTest(isok, 460, 197, 460 * MainModel.midScale, 197 * MainModel.midScale, true);
                isok.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - isok.Width) / 2, (Screen.AllScreens[0].Bounds.Height - isok.Height) / 2);
                isok.TopMost = true;
                BackHelper.ShowFormBackGround();

                if (isok.ShowDialog() == DialogResult.OK)
                {
                    MemberCenterHelper.ShowFormForgetPassword();
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                BackHelper.HideFormBackGround();
            }
        }

        private void btnChangePhone_Click(object sender, EventArgs e)
        {

            if (MemberCenterHelper.ShowFormChangePhoneNumber(CurrentMember))
            {
                LoadingHelper.ShowLoadingScreen();
                try
                {                   
                    CurrentMember = MainModel.CurrentMember;
                    UpdateMemberInfo();
                    MemberCenterMediaHelper.UpdatememberInfo(lblPhone.Text, lblMemberInfo.Text, lblBalance.Text, lblCredit.Text, lblCreditAmount.Text, lblCoupon.Text,lblEntityCard.Text);
                }
                catch (Exception ex)
                {
                    MainModel.ShowLog("会员信息加载异常：" + ex.Message, true);
                }
                finally
                {
                    LoadingHelper.CloseForm();
                }
            }
        }

        private void pnlPayByOther_Click(object sender, EventArgs e)
        {

            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (CurrentTemplate == null)
                {
                    MainModel.ShowLog("请选择充值金额", false);
                    return;
                }

                string error = "";
                List<ClassPayment> payments = httputil.Custompaycon(ref error);
                if (payments == null || payments.Count == 0)
                {
                    MainModel.ShowLog("暂无其他支付配置，或退出重试", false);
                    return;
                }


                if (CurrentBalanceAccount != null && !CurrentBalanceAccount.haspaypassword && CurrentBalanceAccount.needinitpassword)
                {
                    MemberCenterHelper.ShowFormNoPayPwd();
                    return;
                }

                ClassPayment customerpayment = MemberCenterHelper.ShowFormOtherMethord(payments, CurrentTemplate.amount);

                if (customerpayment == null)
                {
                    return;
                }

                MemberTopUpPara para = new MemberTopUpPara();
                para.amount = CurrentTemplate.amount;
                para.memberid = Convert.ToInt64(CurrentMember.memberinformationresponsevo.memberid);
                para.paymode = "1";
                para.phone = CurrentMember.memberheaderresponsevo.mobile;
                para.shopid = MainModel.CurrentShopInfo.shopid;

                para.customerpaycode = customerpayment.code;
                //0 代表自定义充值
                if (CurrentTemplate.id == 0)
                {
                    para.rewardamount = CurrentTemplate.rewardamount;
                }
                para.autoreward = !CurrentTemplate.customAndreward;


                string errormsg = "";
                long result = httputil.MemberTopUp(para, ref errormsg);

                if (!string.IsNullOrEmpty(errormsg))
                {
                    MainModel.ShowLog(errormsg, false);
                }
                else
                {
                    //DbJsonUtil.AddBalanceInfo(customerpayment.name, para.amount);
                    PrintUtil.PrintTopUp(result.ToString());
                    TopUpOK();
                }

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("自定义充值异常" + ex.Message, true);
                BackHelper.HideFormBackGround();
                throw;
            }
        }

        //绑卡
        private void btnbang_Click(object sender, EventArgs e)
        {
            try
            {
                string entityCardNo = DialogHelper.ShowFormCode("绑定实体卡","请输入实体卡号");
                if (!string.IsNullOrEmpty(entityCardNo))
                {
                    string err = "";
                    LoadingHelper.ShowLoadingScreen();
                    OutEntityCardResponseDto entityCard = membercenterutil.GetCard(entityCardNo, ref err);
                    LoadingHelper.CloseForm();
                    if (!string.IsNullOrEmpty(err) || entityCard == null)
                    {
                        MainModel.ShowLog(err, true);
                        return;
                    }
                    else
                    {
                        bool flag = MemberCenterHelper.ShowFormBindEntityCard(entityCard);
                        MainModel.CurrentMember = CurrentMember = httputil.GetMember(CurrentMember.memberheaderresponsevo.mobile, ref err);
                        UpdateMemberInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "绑定实体卡异常:" + ex.Message);
            }
            finally
            {
                LoadingHelper.CloseForm();
            }
        }

    }
}
