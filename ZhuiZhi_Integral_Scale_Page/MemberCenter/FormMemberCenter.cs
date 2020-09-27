﻿using System;
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

        private Member CurrentMember = null;

        private ListAllTemplate CurrentTemplate = null;
        private CustomTemplateModel CustomTemplate = null;

        private string PassWord = "";

        private List<ListAllTemplate> LstTemplates = new List<ListAllTemplate>();
        private List<CustomTemplateModel> CustomTemp = new List<CustomTemplateModel>();

        public MemberCenterHttpUtil membercenterutil = new MemberCenterHttpUtil();

        bool IsEnable = true;
        ListAllTemplate listall = new ListAllTemplate();
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
                if (MainModel.NewPhone != "")
                {
                    label4.Visible = true;
                    newphone.Visible = true;
                    newphone.Text = MainModel.NewPhone;
                }

                lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;

                lblMenu.Text = MainModel.CurrentUser.nickname + ",你好 ";
                picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
                lblMenu.Left = picMenu.Right;
                if (MainModel.NewPhone != "")
                {
                    CurrentMember.memberheaderresponsevo.mobile = MainModel.NewPhone;
                }
                string phone = CurrentMember.memberheaderresponsevo.mobile;

                if (phone.Length == 11)
                {
                    string tempphone = phone.Substring(0, 3) + " " + phone.Substring(3, 4) + " " + phone.Substring(7, 4);
                    phone = tempphone;
                }
                lblPhone.Text = phone;
                MainModel.GetPhone = phone;
                btnChangePhone.Left = lblPhone.Right;

                string gender = CurrentMember.memberinformationresponsevo.gender == 0 ? "男" : "女";
                string birthday = CurrentMember.memberinformationresponsevo.birthdaystr;
                MainModel.memberid = CurrentMember.memberid;
                lblMemberInfo.Text = "性别：" + gender + " | " + "生日：" + birthday;

                lblBalance.Text = "￥" + CurrentMember.barcoderecognitionresponse.balance;

                lblCredit.Text = CurrentMember.creditaccountrepvo.availablecredit.ToString();

                lblCreditAmount.Text = "=" + CurrentMember.creditaccountrepvo.creditworth.ToString("f2") + "元";

                lblCreditAmount.Left = lblCredit.Right;


                Application.DoEvents();

                IsEnable = false;
                LoadingHelper.ShowLoadingScreen();

                MemberCenterMediaHelper.ShowFormMainMedia();



                LoadBalanceAccount();
                LoadCoupon();

                LoadTemplate(true);

                LoadBalanceConfigDetail();
                LoadingHelper.CloseForm();
                IsEnable = true;

                MemberCenterMediaHelper.UpdatememberInfo(lblPhone.Text, lblMemberInfo.Text, lblBalance.Text, lblCredit.Text, lblCreditAmount.Text, lblCoupon.Text);
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载会员中心页面异常" + ex.Message, true);
                LoadingHelper.CloseForm();

                IsEnable = true;
            }
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
                if (custommoney.Text != "+")
                {

                    para.amount = Convert.ToInt32(ListAllTemplate.mount);
                    para.memberid = Convert.ToInt64(CurrentMember.memberinformationresponsevo.memberid);
                    para.paymode = "2";
                    para.phone = CurrentMember.memberheaderresponsevo.mobile;
                    para.shopid = MainModel.CurrentShopInfo.shopid;
                }
                else
                {
                    
                    para.amount = CurrentTemplate.amount;
                    para.memberid = Convert.ToInt64(CurrentMember.memberinformationresponsevo.memberid);
                    para.paymode = "2";
                    para.phone = CurrentMember.memberheaderresponsevo.mobile;
                    para.shopid = MainModel.CurrentShopInfo.shopid;
                    
                }
                


                string errormsg = "";
                long result = httputil.MemberTopUp(para, ref errormsg);

                if (!string.IsNullOrEmpty(errormsg))
                {
                    MainModel.ShowLog(errormsg, false);
                }
                else
                {

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
                if (custommoney.Text != "+")
                {
                   
                   bool cashresult =  MemberCenterHelper.ShowFormTopUpByCash(Convert.ToInt32(ListAllTemplate.mount));
                   if (cashresult ==false)
                   {
                       return;
                   }
                    MemberTopUpPara para = new MemberTopUpPara();
                    para.amount = Convert.ToInt32(ListAllTemplate.mount);
                    para.memberid = Convert.ToInt64(CurrentMember.memberinformationresponsevo.memberid);
                    para.paymode = "0";
                    para.phone = CurrentMember.memberheaderresponsevo.mobile;
                    para.shopid = MainModel.CurrentShopInfo.shopid;


                    string errormsg = "";
                    long result = httputil.MemberTopUp(para, ref errormsg);

                    if (!string.IsNullOrEmpty(errormsg))
                    {
                        MainModel.ShowLog(errormsg, false);
                    }
                    else
                    {
                        PrintUtil.PrintTopUp(result.ToString());
                        TopUpOK();
                    }
                }
               
                else if (MemberCenterHelper.ShowFormTopUpByCash(CurrentTemplate.amount))
                {

                    MemberTopUpPara para = new MemberTopUpPara();
                    para.amount = CurrentTemplate.amount;
                    para.memberid = Convert.ToInt64(CurrentMember.memberinformationresponsevo.memberid);
                    para.paymode = "0";
                    para.phone = CurrentMember.memberheaderresponsevo.mobile;
                    para.shopid = MainModel.CurrentShopInfo.shopid;


                    string errormsg = "";
                    long result = httputil.MemberTopUp(para, ref errormsg);

                    if (!string.IsNullOrEmpty(errormsg))
                    {
                        MainModel.ShowLog(errormsg, false);
                    }
                    else
                    {
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
            MemberCenterHelper.ShowFormTopUPQuery();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadTemplate(true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string errormsg = "";
            httputil.BalanceConfigDetail(ref errormsg);
        }


        private void LoadTemplate(bool needrefresh)
        {
            try
            {
                dgvTemplate.Rows.Clear();
                if (LstTemplates == null || LstTemplates.Count == 0 || needrefresh)
                {

                    string errormsg = "";
                    LstTemplates = httputil.ListAllTemplate(ref errormsg);

                    if (LstTemplates == null || !string.IsNullOrEmpty(errormsg))
                    {
                        MainModel.ShowLog(errormsg, false);
                        return;
                    }
                }

                List<Bitmap> lstbmp = new List<Bitmap>();

                foreach (ListAllTemplate template in LstTemplates)
                {
                    if (CurrentTemplate == null && template.enabled == true)
                    {
                        CurrentTemplate = template;
                    }
                    lstbmp.Add(GetItemImg(template));
                }
                ListAllTemplate.mount = CurrentTemplate.amount;
                ListAllTemplate temp = new ListAllTemplate();
                
                    
                    
              

                //lstbmp.Add((Bitmap)MainModel.GetControlImage(custom));
                CustomTemplateModel zidingyi = new CustomTemplateModel();

                ListAllTemplate.iszidingyi = true;

                listall.able = ListAllTemplate.enable;
                if (CustomTemplate == null && listall.able == true)
                {

                    CustomTemplate = zidingyi;
                    listall.CustomId = CurrentTemplate.id;
                }

                lstbmp.Add(GetCustomImg(listall));








                int emptycount = 3 - lstbmp.Count % 3;

                for (int i = 0; i < emptycount; i++)
                {
                    lstbmp.Add(Resources.ResourcePos.empty);
                }
                int rowcount = lstbmp.Count / 3;

                for (int i = 0; i < rowcount; i++)
                {
                    dgvTemplate.Rows.Add(lstbmp[i * 3 + 0], lstbmp[i * 3 + 1], lstbmp[i * 3 + 2]);
                }

                Application.DoEvents();
                MemberCenterMediaHelper.UpdateDgvTemplate(lstbmp);
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("获取所有充值面额异常" + ex.Message, true);
            }
        }

        /// <summary>
        /// 自定义图片截图
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public Bitmap GetCustomImg(ListAllTemplate temp)
        {
            try
            {

                if (CurrentBalanceAccount != null && (ListAllTemplate.mount + ListAllTemplate.CustomMoney) <= 5000)
                {

                    if (CustomTemplate != null && temp.CustomId == CurrentTemplate.id)
                    {
                        custommoney.ForeColor = Color.White;
                        customdiscount.ForeColor = Color.White;
                        custom.BackColor = Color.FromArgb(68, 147, 225);
                    }
                    else
                    {
                        custommoney.ForeColor = Color.Blue;
                        customdiscount.ForeColor = Color.Blue;
                        custom.BackColor = Color.White;
                    }

                }
                else
                {
                    custommoney.ForeColor = Color.White;
                    customdiscount.ForeColor = Color.White;
                    custom.BackColor = Color.FromArgb(200, 200, 200);
                }
                Bitmap b = (Bitmap)MainModel.GetControlImage(custom);
                b.Tag = temp;
                return b;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private Bitmap GetItemImg(ListAllTemplate template)
        {
            try
            {

                if (CurrentBalanceAccount != null && (template.amount + template.rewardamount) <= 5000)
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

                lblAmountStr.Text = "赠" + template.rewardamount.ToString("f2") + "元";

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
                CurrentLstCoupon = httputil.ListMemberCouponAvailable(CurrentMember.memberinformationresponsevo.memberid, ref ErrorMsg);

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

                if (selectimg.Tag == null)  //空白单元格（无商品）
                {
                    return;
                }
                
                ListAllTemplate temp = (ListAllTemplate)selectimg.Tag;
               
               
                ListAllTemplate.mount = temp.amount;
              
                if (temp.id != 0)
                {
                    customdiscount.Text = "自定义金额";
                    custommoney.Text = "+";
                    custommoney.Font = new System.Drawing.Font("微软雅黑", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    customdiscount.Font = new System.Drawing.Font("微软雅黑", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                    GetCustomImg(temp);
                }


                if (temp.id == 0)
                {

                    FormCustomMoney money = new FormCustomMoney();
                    asf.AutoScaleControlTest(money, 420, 197, 420 * MainModel.midScale, 197 * MainModel.midScale, true);
                    money.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - money.Width) / 2, (Screen.AllScreens[0].Bounds.Height - money.Height) / 2);
                    money.TopMost = true;
                    money.ShowDialog();
                    money.Dispose();
                    money.Close();
                    if (money.DialogResult == DialogResult.Cancel)
                    {
                        customdiscount.Text = "自定义金额";
                        custommoney.Text = "+";
                        custommoney.Font = new System.Drawing.Font("微软雅黑", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                        customdiscount.Font = new System.Drawing.Font("微软雅黑", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                        GetCustomImg(temp);
                    }
                    custommoney.Text = ListAllTemplate.CustomMoney.ToString();
                    customdiscount.Text = ListAllTemplate.ZengCustomMoney;
                    if (customdiscount.Text == "label3")
                    {
                        ListAllTemplate.enable = true;

                        customdiscount.Text = "赠送0元";
                        customdiscount.ForeColor = Color.Blue;
                        customdiscount.Font = new System.Drawing.Font("微软雅黑", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                        LoadTemplate(true);

                        custom.BackColor = Color.Blue;
                        custommoney.ForeColor = Color.White;
                        customdiscount.ForeColor = Color.White;
                    }
                    else if (custommoney.Text == "0")
                    {
                        custommoney.Text = "+";
                        custommoney.ForeColor = Color.Blue;
                        custommoney.Font = new System.Drawing.Font("微软雅黑", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                        custommoney.Text = MainModel.CoustomMoney;
                        customdiscount.Text = MainModel.ZMoney;


                    }
                    else
                    {
                        LoadTemplate(true);
                        ListAllTemplate.enable = true;
                        if (custommoney.Text != "+")
                        {
                            ListAllTemplate.mount = int.Parse(custommoney.Text);

                        }
                        custom.BackColor = Color.Blue;
                        custommoney.ForeColor = Color.White;
                        customdiscount.ForeColor = Color.White;

                    }

                }

                else
                {
                    if (CurrentBalanceAccount == null || (Convert.ToInt64(ListAllTemplate.CustomMoney) + Convert.ToInt64(ListAllTemplate.Money) + CurrentBalanceAccount.balance) > 5000)
                    {
                        return;
                    }

                    CurrentTemplate = (ListAllTemplate)selectimg.Tag;

                    LoadTemplate(false);
                }


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
                LoadTemplate(true);
                LoadBalanceAccount();

                MemberCenterMediaHelper.UpdatememberInfo(lblPhone.Text, lblMemberInfo.Text, lblBalance.Text, lblCredit.Text, lblCreditAmount.Text, lblCoupon.Text);


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


        private void LoadBalanceConfigDetail()
        {
            try
            {
                string errormsg = "";
                BalanceConfigDetail configdetail = httputil.BalanceConfigDetail(ref errormsg);

                if (configdetail != null && configdetail.cashrechargeenableforpos)
                {
                    pnlPayByCash.Visible = true;

                    pnlPayByOnLine.Width = pnlPayByCash.Width;

                    picPayByOnLine.Left = (pnlPayByOnLine.Width - picPayByOnLine.Width - lblPayByOnLine.Width) / 2;

                    lblPayByOnLine.Left = picPayByOnLine.Right;
                }
                else
                {
                    pnlPayByCash.Visible = false;
                    pnlPayByOnLine.Width = dgvTemplate.Width;

                    picPayByOnLine.Left = (pnlPayByOnLine.Width - picPayByOnLine.Width - lblPayByOnLine.Width) / 2;

                    lblPayByOnLine.Left = picPayByOnLine.Right;
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FormIsokCancle isok = new FormIsokCancle();
                asf.AutoScaleControlTest(isok, 460, 197, 460 * MainModel.midScale, 197 * MainModel.midScale, true);
                isok.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - isok.Width) / 2, (Screen.AllScreens[0].Bounds.Height - isok.Height) / 2);
                isok.TopMost = true;
                BackHelper.ShowFormBackGround();
                //BackHelper.HideFormBackGround();
                isok.ShowDialog();
                isok.Dispose();
                isok.Close();
                MemberCenterHelper.ShowFormForgetPassword();
                string err = "";
                string smsCodeResult = membercenterutil.GetSendvalidateSmsCode(MainModel.CurrentMember.memberid, ref err);
            }
            catch (Exception ex)
            { }
        }

        private void btnChangePhone_Click(object sender, EventArgs e)
        {
            MemberCenterHelper.ShowFormChangePhoneNumber(CurrentMember);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void pnlPayByOther_Click(object sender, EventArgs e)
        {
            
            try
            {
                FormOtherMethod pay = new FormOtherMethod();
                asf.AutoScaleControlTest(pay, 500, 197, 500 * MainModel.midScale, 197 * MainModel.midScale, true);
                pay.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - pay.Width) / 2, (Screen.AllScreens[0].Bounds.Height - pay.Height) / 2);
                pay.TopMost = true;
                BackHelper.ShowFormBackGround();
                //BackHelper.HideFormBackGround();

                pay.ShowDialog();
                pay.Dispose();
                pay.Close();
                if (MainModel.isokcancle == true)
                {
                    return;
                }
                if (pay.DialogResult == DialogResult.Cancel)
                {
                    return;
                }
                MemberTopUpPara para = new MemberTopUpPara();    
                para.amount = ListAllTemplate.mount;
                para.memberid = Convert.ToInt64(MainModel.memberid);
                para.customerpaycode = MainModel.code;
                para.autoreward = false;
                para.paymode = "1";
                para.phone = MainModel.GetPhone;
                para.shopid = MainModel.CurrentShopInfo.shopid;
               
                para.rewardamount =  MainModel.GetZsje;
                string errormsg = "";
                long result = httputil.MemberTopUp(para, ref errormsg);

                if (result == null)
                {
                    MainModel.ShowLog(errormsg, false);
                    BackHelper.HideFormBackGround();
                    
                   
                }
                else
                {
                    PrintUtil.PrintTopUp(result.ToString());
                    TopUpOK();
                    BackHelper.HideFormBackGround();
                }

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("自定义充值异常" + ex.Message, true);
                BackHelper.HideFormBackGround();
                throw;
            }
            
            
        }
    }
}
