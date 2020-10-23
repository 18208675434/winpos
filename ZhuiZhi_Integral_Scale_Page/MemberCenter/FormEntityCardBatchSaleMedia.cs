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
    public partial class FormEntityCardBatchSaleMedia : Form
    {
        public FormEntityCardBatchSaleMedia()
        {
            InitializeComponent();
        }

        public void RefreshDgv(string cardSum, string totalRechargeAmount, string totalGiftAmount, string totalRechargeAll, string totalPay, List<RechargeCardInfo> lstCard)
        {
            lbCardSum.Text = cardSum;
            lblTotalRechargeAmount.Text = totalRechargeAmount;
            lblTotalGiftAmount.Text = totalGiftAmount;
            lblTotalRechargeAll.Text = totalRechargeAll;
            lblTotalPay.Text = totalPay;
            LoadDgvCart(lstCard);
        }

        private void LoadDgvCart(List<RechargeCardInfo> lstCard)
        {
            try
            {
                dgvCard.Rows.Clear();
                if (lstCard == null || lstCard.Count == 0)
                {
                    return;
                }
                foreach (RechargeCardInfo rechargeCardInfo in lstCard)
                {
                    dgvCard.Rows.Add(GetDgvRow(rechargeCardInfo));
                }
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
                lblRechargeAmount.Text = rechargeCardInfo.rechargeamount.ToString();
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
    }
}
