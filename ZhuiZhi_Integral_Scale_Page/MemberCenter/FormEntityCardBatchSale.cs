﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.BatchSaleCardUI.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormEntityCardBatchSale : Form
    {
        #region 成员变量
        /// <summary>
        /// 当前购物车商品
        /// </summary>
        private List<RechargeCardInfo> lstCard = new List<RechargeCardInfo>();

        /// <summary>
        /// 接口访问类
        /// </summary>
        HttpUtil httpUtil =new HttpUtil();
        MemberCenterHttpUtil memberCenterHttpUtil = new MemberCenterHttpUtil();
        #endregion

        #region 分页事件
        private int CurrentPage = 1;
        private int PageSize = 7;
        private void rbtnPageUp_ButtonClick(object sender, EventArgs e)
        {
            if (!rbtnPageUp.WhetherEnable)
            {
                return;
            }
            CurrentPage--;
            LoadDgvCart();
        }

        private void rbtnPageDown_ButtonClick(object sender, EventArgs e)
        {
            if (!rbtnPageDown.WhetherEnable)
            {
                return;
            }
            CurrentPage++;
            LoadDgvCart();
        }

        #endregion

        public FormEntityCardBatchSale()
        {
            InitializeComponent();
        }

        private void btnGetCard_Click(object sender, EventArgs e)
        {
            try
            {
                string cardid = DialogHelper.ShowFormCode("输入实体卡号", "请输入实体卡卡号");
                if (!string.IsNullOrEmpty(cardid))
                {
                    if (lstCard.Exists(e1 => e1.cardid == cardid))
                    {
                        MainModel.ShowLog("实体卡号：" + cardid + " 已存在");
                        return;
                    }
                    string err = "";
                    LoadingHelper.ShowLoadingScreen();
                    OutEntityCardResponseDto entityCard = memberCenterHttpUtil.GetCard(cardid, ref err);
                    LoadingHelper.CloseForm();
                    if (err != "" || entityCard == null) //会员不存在
                    {
                        MainModel.ShowLog(err);
                        return;
                    }
                    if (entityCard.balance > 0)
                    {
                        MainModel.ShowLog("批量售卡仅支持未开卡且无预储值金额的实体卡，请检查后重试");
                        return;
                    }
                    lstCard.Insert(0, new RechargeCardInfo()
                    {
                        cardid = cardid,
                        status = entityCard.status,
                        memberid = entityCard.mobile
                    });

                    //int index = dgvCard.Rows.Count + 1;
                    //CurrentCards.Add(new RechargeCardInfo() 
                    //{ CardNo = index * 1000000 + "", RechargeAmount = (index % 2) * 100, RewardAmount = (index % 2) * 10, 
                    //    Status = (index % 2)+"", MemberNo = "B" + index * 1000000 });
                    RefreshDgv();
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("获取实体卡异常" + ex.Message, true);
            }
        }

        private void btnBatchSetRechargeAmount_Click(object sender, EventArgs e)
        {
            if (dgvCard.Rows.Count == 0)
            {
                MainModel.ShowLog("请添加实体卡");
                return;
            }
            ListAllTemplate customtemplate = MemberCenterHelper.ShowFormRechargeAmount();
            if (customtemplate != null)
            {
                foreach (var item in lstCard)
                {
                    item.rechargeamount = customtemplate.amount;
                    item.rewardamount = customtemplate.rewardamount;
                    item.autoreward = customtemplate.id > 0;
                }
                RefreshDgv();
            }
            this.Activate();
        }

        public void RefreshDgv()
        {
            decimal totalRechargeAmount = 0;
            decimal totalGiftAmount = 0;
            decimal totalRechargeAll = 0;
            foreach (var item in lstCard)
            {
                totalRechargeAmount += item.rechargeamount;
                totalGiftAmount += item.rewardamount;
            }
            totalRechargeAll = totalRechargeAmount + totalGiftAmount;
            lbCardSum.Text = string.Format("({0}个实体卡卡号)", lstCard.Count);
            lblTotalRechargeAmount.Text = "￥" + totalRechargeAmount.ToString("f2");
            lblTotalGiftAmount.Text = "￥" + totalGiftAmount.ToString("f2");
            lblTotalRechargeAll.Text = "￥" + totalRechargeAll.ToString("f2");
            lblTotalPay.Text = "￥" + totalRechargeAmount.ToString("f2");
            lblTotalPay.Tag = totalRechargeAmount;
            LoadDgvCart();
        }

        private void LoadDgvCart()
        {
            try
            {
                dgvCard.Rows.Clear();
                if (lstCard == null || lstCard.Count == 0)
                {
                    return;
                }

                //CurrentCards.Reverse();
                rbtnPageUp.WhetherEnable = CurrentPage > 1;
                int startindex = (CurrentPage - 1) * PageSize;
                int lastindex = Math.Min(lstCard.Count - 1, startindex + PageSize - 1);

                List<RechargeCardInfo> lstLoadingCards = lstCard.GetRange(startindex, lastindex - startindex + 1);
                foreach (RechargeCardInfo rechargeCardInfo in lstLoadingCards)
                {
                    dgvCard.Rows.Add(GetDgvRow(rechargeCardInfo));
                }

                rbtnPageDown.WhetherEnable = lstCard.Count > CurrentPage * PageSize;

                Application.DoEvents();
                dgvCard.ClearSelection();
                this.Activate();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载批量充值实体卡列表异常" + ex.Message);
            }
        }

        int locationY = 0;//保留cardstatus初始位置
        private Bitmap GetDgvRow(RechargeCardInfo rechargeCardInfo)
        {
            try
            {
                if (locationY == 0)
                {
                    locationY = lblCardStatus.Location.Y;
                }
                lblCardNo.Text = rechargeCardInfo.cardid;
                txtRechargeAmount.Text = rechargeCardInfo.rechargeamount.ToString();
                lblGiftAmount.Text = rechargeCardInfo.rewardamount.ToString();
                if (string.IsNullOrEmpty(rechargeCardInfo.memberid))
                {
                    lblCardStatus.Text = "未开卡";
                    lblCardStatus.Location = new Point(lblCardStatus.Location.X, lblCardNo.Location.Y);
                    lblMemberNo.Visible = false;
                }
                else
                {
                    lblCardStatus.Text = "已开卡";
                    lblMemberNo.Text = "会员卡号：" + rechargeCardInfo.memberid;
                    lblCardStatus.Location = new Point(lblCardStatus.Location.X, locationY);
                    lblMemberNo.Visible = true;
                }

                Bitmap bmpPro = new Bitmap(pnlDgvItemContent.Width, pnlDgvItemContent.Height);
                bmpPro.Tag = rechargeCardInfo;
                pnlDgvItemContent.DrawToBitmap(bmpPro, new Rectangle(0, 0, pnlDgvItemContent.Width, pnlDgvItemContent.Height));
                bmpPro.MakeTransparent(Color.White);

                return bmpPro;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("解析批量充值信息异常" + ex.Message, true);
                return null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCard_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                Bitmap bmp = (Bitmap)dgvCard.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                RechargeCardInfo rechargeCardInfo = (RechargeCardInfo)bmp.Tag;
                Point po = GlobalUtil.GetCursorPos();

                //修改充值金额
                if (po.X < (dgvCard.Left + txtRechargeAmount.Right + 10) && po.X > (dgvCard.Left + txtRechargeAmount.Left - 10))
                {
                    ListAllTemplate customtemplate = MemberCenterHelper.ShowFormRechargeAmount();
                    if (customtemplate != null)
                    {
                        rechargeCardInfo.rechargeamount = customtemplate.amount;
                        rechargeCardInfo.rewardamount = customtemplate.rewardamount;
                        rechargeCardInfo.autoreward = customtemplate.id > 0;
                        RefreshDgv();
                    }
                    this.Activate();
                }

                //删除
                if (po.X < (dgvCard.Left + picDelete.Right + 10) && po.X > (dgvCard.Left + picDelete.Left - 10))
                {
                    if (ConfirmHelper.Confirm("确认删除？", "实体卡卡号：" + rechargeCardInfo.cardid))
                    {
                        lstCard.Remove(rechargeCardInfo);
                        RefreshDgv();
                    }
                }
            }
            catch (Exception ex)
            {

                MainModel.ShowLog("操作购物车商品异常" + ex.Message, true);
            }
        }

        #region 支付
        bool IsEnable;

        private void pnlPayByCash_Click(object sender, EventArgs e)
        {
            Pay(PayMode.cash);
        }

        private void pnlPayByOnLine_Click(object sender, EventArgs e)
        {
            Pay(PayMode.online);
        }


        private void pnlPayByOther_Click(object sender, EventArgs e)
        {


            Pay(PayMode.other);
        }

        EntityCardBatchDepositRequest BuildRequest(string payMode, string customerpaycode)
        {
            EntityCardBatchDepositRequest request = new EntityCardBatchDepositRequest();
            request.paymode = payMode;
            request.customerpaycode = customerpaycode;
            request.shopid = MainModel.CurrentShopInfo.shopid;
            //request.operatorid = MainModel.CurrentUser.loginaccount;
            request.requestdetails = new List<EntityCardBatchDepositRequestDetail>();
            foreach (var item in lstCard)
            {
                request.requestdetails.Add(new EntityCardBatchDepositRequestDetail()
                {
                    cardno = item.cardid,
                    capitalamount = item.rechargeamount,
                    rewardamount = item.rewardamount,
                    autoreward = item.autoreward
                });
            }
            return request;
        }

        void Pay(PayMode paymode)
        {
            try
            {
                if (dgvCard.Rows.Count == 0)
                {
                    MainModel.ShowLog("请添加实体卡");
                    return;
                }
                decimal totalPay;
                if (!decimal.TryParse(lblTotalPay.Tag.ToString(), out totalPay))
                {
                    return;
                }
                if (totalPay == 0)
                {
                    MainModel.ShowLog("请设置充值金额");
                    return;
                }

                string customerpaycode = "";
                if (paymode == PayMode.other)
                {
                    string error = "";
                    List<ClassPayment> payments = httpUtil.Custompaycon(ref error);
                    if (payments == null || payments.Count == 0)
                    {
                        MainModel.ShowLog("暂无其他支付配置，或退出重试", false);
                        return;
                    }
                    ClassPayment customerpayment = MemberCenterHelper.ShowFormOtherMethord(payments, totalPay);
                    if (customerpayment == null || string.IsNullOrEmpty(customerpayment.code))
                    {
                        return;
                    }
                }

                EntityCardBatchDepositRequest request = BuildRequest((int)paymode + "", customerpaycode);
                request.paymode = (int)paymode + "";
                string err = "";
                var result = memberCenterHttpUtil.BacthEntityCard(request, ref err);
                if (result == null || !string.IsNullOrEmpty(err))
                {
                    MainModel.ShowLog(err);
                    return;
                }
                string batchoperatorid = result.batchoperatorid;
                if (string.IsNullOrEmpty(result.batchoperatorid))
                {
                    MainModel.ShowLog("支付单号不见了，请退出重试");
                    return;
                }
                if (paymode == PayMode.online)//在线支付需单独处理
                {//AuthCodeTradeRequestVo 
                    if (MemberCenterHelper.ShowFormTopUpByOnline(Convert.ToInt64(batchoperatorid), ""))
                    {
                        //PrintUtil.PrintTopUp(result.ToString());
                        MainModel.ShowLog("支付成功");
                        lstCard.Clear();
                        Refresh();
                    }
                }

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("在线充值异常" + ex.Message, true);
            }
        }


        #endregion
    }

    enum PayMode
    {
        cash = 0,//现金
        online = 2,// 微信或支付号
        other = 1,//其它支付
    }

    public class RechargeCardInfo
    {
        public string cardid { get; set; }
        public string memberid { get; set; }

        public decimal rechargeamount { get; set; }
        public decimal rewardamount { get; set; }
        public bool autoreward { get; set; }
        public string status { get; set; }

    }
}
