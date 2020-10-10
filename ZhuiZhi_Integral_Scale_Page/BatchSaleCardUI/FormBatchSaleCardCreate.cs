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
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.BatchSaleCardUI
{
    public partial class FormBatchSaleCardCreate : Form
    {
        #region 成员变量
        /// <summary>
        /// 当前购物车商品
        /// </summary>
        private List<RechargeCardInfo> CurrentCards = new List<RechargeCardInfo>();

        /// <summary>
        /// 接口访问类
        /// </summary>
        HttpUtil httputil = new HttpUtil();
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

        public FormBatchSaleCardCreate()
        {
            InitializeComponent();
        }

        private void btnGetCard_Click(object sender, EventArgs e)
        {
            try
            {
                ShowLoading(true, false);
                string numbervalue = BatchSaleCardHelper.ShowFormVoucher();
                if (!string.IsNullOrEmpty(numbervalue))
                {
                    string ErrorMsgMember = "";
                    Card card = httputil.GetCardNew(numbervalue, ref ErrorMsgMember);

                    if (ErrorMsgMember != "" || card == null) //会员不存在
                    {
                        ShowLog(ErrorMsgMember, false);
                    }
                    else
                    {
                        int index = dgvCard.Rows.Count + 1;
                        CurrentCards.Add(new RechargeCardInfo() 
                        { CardNo = index * 1000000 + "", RechargeAmount = (index % 2) * 100, GiftAmount = (index % 2) * 10, 
                            CardStatus = (index % 2), MemberNo = "B" + index * 1000000 });
                        RefreshDgv();
                    }
                }
                ShowLoading(false, true);
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);

                LogManager.WriteLog("获取实体卡异常" + ex.Message);
            }
        }

        private void ShowLog(string msg, bool iserror)
        {
            //ParameterizedThreadStart Pts = new ParameterizedThreadStart(showlogthread);
            //Thread threadmqtt = new Thread(Pts);
            //threadmqtt.IsBackground = true;
            //threadmqtt.Start(msg);

            //if (iserror)
            //{
            //    LogManager.WriteLog(msg);
            //}
        }

        public void RefreshDgv()
        {
            decimal totalRechargeAmount = 0;
            decimal totalGiftAmount = 0;
            decimal totalRechargeAll = 0;
            foreach (var item in CurrentCards)
            {
                totalRechargeAmount += item.RechargeAmount;
                totalGiftAmount += item.GiftAmount;
            }
            totalRechargeAll = totalRechargeAmount + totalGiftAmount;
            lbCardSum.Text = string.Format("({0}个实体卡卡号)", CurrentCards.Count);
            lblTotalRechargeAmount.Text = "￥" + totalRechargeAmount.ToString("f2");
            lblTotalGiftAmount.Text = "￥" + totalGiftAmount.ToString("f2");
            lblTotalRechargeAll.Text = "￥" + totalRechargeAll.ToString("f2");
            lblTotalPay.Text = "￥" + totalRechargeAmount.ToString("f2");
            LoadDgvCart();
        }

        private void LoadDgvCart()
        {
            try
            {
                dgvCard.Rows.Clear();
                if (CurrentCards == null || CurrentCards.Count == 0)
                {
                    return;
                }

                CurrentCards.Reverse();
                rbtnPageUp.WhetherEnable = CurrentPage > 1;
                int startindex = (CurrentPage - 1) * PageSize;
                int lastindex = Math.Min(CurrentCards.Count - 1, startindex + PageSize - 1);

                List<RechargeCardInfo> lstLoadingCards = CurrentCards.GetRange(startindex, lastindex - startindex + 1);
                foreach (RechargeCardInfo rechargeCardInfo in lstLoadingCards)
                {
                    dgvCard.Rows.Add(GetDgvRow(rechargeCardInfo));
                }

                rbtnPageDown.WhetherEnable = CurrentCards.Count > CurrentPage * PageSize;
                CurrentCards.Reverse();
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
                lblCardNo.Text = rechargeCardInfo.CardNo;
                txtRechargeAmount.Text = rechargeCardInfo.RechargeAmount.ToString();
                lblGiftAmount.Text = rechargeCardInfo.GiftAmount.ToString();
                if (rechargeCardInfo.CardStatus == 0)
                {
                    lblCardStatus.Text = "未开卡";
                    lblCardStatus.Location = new Point(lblCardStatus.Location.X, lblCardNo.Location.Y);
                    lblMemberNo.Visible = false;
                }
                else
                {
                    lblCardStatus.Text = "已开卡";
                    lblMemberNo.Text = "会员卡号：" + rechargeCardInfo.MemberNo;
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
                    rechargeCardInfo.RechargeAmount = 200;
                    rechargeCardInfo.GiftAmount = 20;
                    RefreshDgv();
                }

                //删除
                if (po.X < (dgvCard.Left + picDelete.Right + 10) && po.X > (dgvCard.Left + picDelete.Left - 10))
                {
                    if (ConfirmHelper.Confirm("确认删除？", "实体卡卡号：" + rechargeCardInfo.CardNo))
                    {
                        CurrentCards.Remove(rechargeCardInfo);
                        RefreshDgv();
                    }
                }
            }
            catch (Exception ex)
            {

                MainModel.ShowLog("操作购物车商品异常" + ex.Message, true);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="showloading">显示等待框</param>
        /// <param name="isenable">页面是否可操作</param>
        private void ShowLoading(bool showloading, bool isenable)
        {
            //try
            //{
            //    IsEnable = isenable;
            //    if (this.IsHandleCreated)
            //    {
            //        this.Invoke(new InvokeHandler(delegate()
            //        {
            //            pnlLoading.Visible = showloading;

            //        }));
            //    }
            //    else
            //    {
            //        pnlLoading.Visible = showloading;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogManager.WriteLog("显示等待异常" + ex.Message);
            //}

        }
    }

    public class RechargeCardInfo
    {
        string cardNo;
        string memberNo;

        int rechargeAmount;
        int giftAmount;
        int cardStatus;


        public String CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }

        public String MemberNo
        {
            get { return memberNo; }
            set { memberNo = value; }
        }

        public int RechargeAmount
        {
            get { return rechargeAmount; }
            set { rechargeAmount = value; }
        }
        public int GiftAmount
        {
            get { return giftAmount; }
            set { giftAmount = value; }
        }
        public int CardStatus
        {
            get { return cardStatus; }
            set { cardStatus = value; }
        }
    }
}
