using System;
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
        HttpUtil httpUtil = new HttpUtil();
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

        private void FormEntityCardBatchSale_Load(object sender, EventArgs e)
        {
            try
            {
                lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;
                lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
                picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
                lblMenu.Left = picMenu.Right;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载批量售卡页面异常" + ex.Message, true);
            }
        }


        private void FormEntityCardBatchSale_Shown(object sender, EventArgs e)
        {
            MemberCenterMediaHelper.ShowFormEntityCardBatchSaleMedia();
            LoadingHelper.ShowLoadingScreen();
            lstCard = DbJsonUtil.GetRecord<List<RechargeCardInfo>>(DbJsonUtil.EntityCardBatchSale);
            if (lstCard==null)
            {
                lstCard = new List<RechargeCardInfo>();
            }
            RefreshDgv();
            LoadingHelper.CloseForm();         
          
        }


        private void FormEntityCardBatchSale_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemberCenterMediaHelper.CloseFormEntityCardBatchSaleMedia();
        }

        private void btnRechargeQuery_Click(object sender, EventArgs e)
        {
            MemberCenterHelper.ShowFormRechangeQuery();
        }


        private void btnGetCard_Click(object sender, EventArgs e)
        {
            if (!IsEnable)
            {
                return;
            }
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
            if (!IsEnable)
            {
                return;
            }
            if (dgvCard.Rows.Count == 0)
            {
                MainModel.ShowLog("请添加实体卡");
                return;
            }
            if (dgvCard.Rows.Count >= 20)
            {
                MainModel.ShowLog("单词批量你最多添加20个");
                return;
            }
            ListAllTemplate customtemplate = MemberCenterHelper.ShowFormRechargeAmount();
            if (customtemplate != null)
            {
                foreach (var item in lstCard)
                {
                    item.rechargeamount = customtemplate.amount;
                    item.rewardamount = customtemplate.rewardamount;
                    item.autoreward = !customtemplate.customAndreward;// customtemplate.id > 0
                }
                RefreshDgv();
            }
            this.Activate();
        }

        public void RefreshDgv()
        {
            LoadingHelper.ShowLoadingScreen();
            DbJsonUtil.AddAndUpdateRecord<List<RechargeCardInfo>>(DbJsonUtil.EntityCardBatchSale, lstCard);
            LoadingHelper.CloseForm();
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
                if (lstCard == null)
                {
                    return;
                }
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

                MemberCenterMediaHelper.UpdateFormEntityCardBatchSaleMedia(lbCardSum.Text, lblTotalRechargeAmount.Text, lblTotalGiftAmount.Text, lblTotalRechargeAll.Text, lblTotalPay.Text, lstLoadingCards);
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
                if (rechargeCardInfo.rechargeamount > 0)
                {
                    lblRechargeAmountWarterTxt.Visible = false;
                    txtRechargeAmount.Text = rechargeCardInfo.rechargeamount.ToString();
                }
                else
                {
                    txtRechargeAmount.Text = "";
                    lblRechargeAmountWarterTxt.Visible = true;
                }

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
            if (sender == btnCancle)
            {
                this.Close();
            }
            else if (ConfirmHelper.Confirm("确认取消交易？"))
            {
                LoadingHelper.ShowLoadingScreen();
                DbJsonUtil.DelRecord(DbJsonUtil.EntityCardBatchSale);
                LoadingHelper.CloseForm();
                this.Close();
            }
        }

        private void dgvCard_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!IsEnable)
            {
                return;
            }
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
                        rechargeCardInfo.autoreward = !customtemplate.customAndreward;// customtemplate.id > 0
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
        bool IsEnable = true;//支付过程中不可用
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
            request.operatorid = MainModel.CurrentUser.loginaccount;
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
                if (!IsEnable)
                {
                    return;
                }
                IsEnable = false;
                LoadingHelper.ShowLoadingScreen();
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
                if (paymode == PayMode.other)//其它支付
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
                    customerpaycode = customerpayment.code;
                }
                decimal realCash = 0;
                if (paymode == PayMode.cash)//现金支付
                {
                    if (!MemberCenterHelper.ShowFormTopUpByCash(totalPay,ref realCash))
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
                bool payResult = true;


                if (paymode == PayMode.online)//在线支付需单独处理
                {
                    tradePara trade = new tradePara();
                    trade.orderid = batchoperatorid;
                    trade.totalfee = totalPay;
                    trade.ordertype = 2;
                    trade.tradebatchorderrequestdtos = new List<TradeBatchOrderRequestDto>();
                    foreach (var item in result.depositdetails)
                    {
                        trade.tradebatchorderrequestdtos.Add(new TradeBatchOrderRequestDto()
                        {
                            buyid = item.memberid,
                            orderid = item.depositbillid,
                            ordertype = 2,
                            totalfee = item.capitalamount
                        });
                    }
                    payResult = MemberCenterHelper.ShowFormTopUpByOnline(Convert.ToInt64(batchoperatorid), "", trade);
                }
                if (payResult)
                {
                    DbJsonUtil.DelRecord(DbJsonUtil.EntityCardBatchSale);
                    List<string> orderids = new List<string>();
                    foreach (var item in result.depositdetails)
                    {
                        orderids.Add(item.depositbillid);
                    }
                    if (!MemberCenterHelper.ShowRechargeSuccess(batchoperatorid, realCash, orderids))
                    {
                        this.Close();
                        return;
                    }
                    CurrentPage = 1;
                    lstCard.Clear();
                    RefreshDgv();
                    
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("在线充值异常" + ex.Message, true);
            }
            finally
            {
                LoadingHelper.CloseForm();
                IsEnable = true;
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
