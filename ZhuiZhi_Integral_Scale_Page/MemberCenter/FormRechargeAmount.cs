using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormRechargeAmount : Form
    {
        MemberCenterHttpUtil memberCenterHttpUtil = new MemberCenterHttpUtil();
        public FormRechargeAmount()
        {
            InitializeComponent();
        }

        private void FormRechargeAmount_Shown(object sender, EventArgs e)
        {
            BindData();
        }

        private int CurrentPage = 1;

        private int PageSize = 7;
        private void rbtnPageUp_ButtonClick(object sender, EventArgs e)
        {

            if (!rbtnPageUp.WhetherEnable)
            {
                return;
            }
            CurrentPage--;
            BindData();
        }

        private void rbtnPageDown_ButtonClick(object sender, EventArgs e)
        {
            if (!rbtnPageDown.WhetherEnable)
            {
                return;
            }
            CurrentPage++;
            BindData();
        }

        void BindData()
        {
            string errormsg = "";
            if (MainModel.LstRechargeTemplates == null)
            {
                LoadingHelper.ShowLoadingScreen();
                MainModel.LstRechargeTemplates = new HttpUtil().ListAllTemplate(ref errormsg);
                LoadingHelper.CloseForm();
            }

            if (MainModel.LstRechargeTemplates != null)
            {
                foreach (var item in MainModel.LstRechargeTemplates)
                {
                    if (MainModel.LstRechargeTemplates.Count + 1 > 7)
                    {
                        //计算分页按钮位置
                    }
                    dgvData.Rows.Add(GetItem(item));
                }
            }
            dgvData.ClearSelection();
        }

        Bitmap GetItem(ListAllTemplate item)
        {
            lblAmount.Text = item.amount.ToString();
            lblRewardAmount.Text = string.Format("赠送金额{0}元", item.rewardamount.ToString());
            Bitmap picItem = (Bitmap)MainModel.GetControlImage(pnlItem);
            return picItem;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 窗体处理
        private void FormConfirm_Resize(object sender, EventArgs e)
        {
            SetWindowRegion();
        }
        /// <summary>
        /// 设置窗体的Region   画半径为10的圆角
        /// </summary>
        public void SetWindowRegion()
        {
            try
            {
                GraphicsPath FormPath;
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                FormPath = GetRoundedRectPath(rect, 10);
                this.Region = new Region(FormPath);
            }
            catch (Exception ex)
            {

            }

        }
        /// <summary>
        /// 绘制圆角路径
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            try
            {
                int diameter = radius;
                Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
                GraphicsPath path = new GraphicsPath();

                // 左上角
                path.AddArc(arcRect, 180, 90);

                // 右上角
                arcRect.X = rect.Right - diameter;
                path.AddArc(arcRect, 270, 90);

                // 右下角
                arcRect.Y = rect.Bottom - diameter;
                path.AddArc(arcRect, 0, 90);

                // 左下角
                arcRect.X = rect.Left;
                path.AddArc(arcRect, 90, 90);
                path.CloseFigure();//闭合曲线
                return path;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        private void pnlItem_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
