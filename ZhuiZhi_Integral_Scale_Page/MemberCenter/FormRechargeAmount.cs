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
        public ListAllTemplate listAllTemplate;
        MemberCenterHttpUtil memberCenterHttpUtil = new MemberCenterHttpUtil();
        List<ListAllTemplate> lstRechargeTemplates;
        public FormRechargeAmount()
        {
            InitializeComponent();
        }

        private void FormRechargeAmount_Shown(object sender, EventArgs e)
        {
            string errormsg = "";

            LoadingHelper.ShowLoadingScreen();
            lstRechargeTemplates = new HttpUtil().ListAllTemplate(ref errormsg);
            MainModel.LstRechargeTemplates = lstRechargeTemplates;
            LoadingHelper.CloseForm();

            if (lstRechargeTemplates == null)
            {
                lstRechargeTemplates = new List<ListAllTemplate>();
            }
            lstRechargeTemplates.Add(new ListAllTemplate() { id = -1 });
        
            if (lstRechargeTemplates.Count > 7)
            {
                //计算分页按钮位置
                this.dgvData.Height = this.dgvData.Height - dgvData.Height / 7;
            }
            BindData();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            Bitmap bmp = (Bitmap)dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            listAllTemplate = (ListAllTemplate)bmp.Tag;
            if (listAllTemplate.id == -1)
            {
                this.Hide();
                //FormBackGround tempfrmback=null;
                try
                {
                    //tempfrmback = new FormBackGround();
                    //tempfrmback = new FormBackGround();
                    //tempfrmback.Location = new System.Drawing.Point(0, 0);
                    //tempfrmback.TopMost = true;
                    //tempfrmback.Show();

                    listAllTemplate = MemberCenterHelper.ShowFormCustomerChange();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    //if (tempfrmback != null)
                    //{
                    //    tempfrmback.Close();
                    //}
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #region 分页
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        void BindData()
        {
            dgvData.Rows.Clear();
            rbtnPageUp.WhetherEnable = CurrentPage > 1;
            int startindex = (CurrentPage - 1) * PageSize;
            int lastindex = Math.Min(lstRechargeTemplates.Count - 1, startindex + PageSize - 1);

            List<ListAllTemplate> pageRechargeTemplates = lstRechargeTemplates.GetRange(startindex, lastindex - startindex + 1);
            foreach (var item in pageRechargeTemplates)
            {
                if (item.id >= 0)
                {
                    pnlCustom.Visible = false;
                    pnlTemplate.Visible = true;
                }
                else
                {
                    pnlCustom.Visible = true;
                    pnlTemplate.Visible = false;
                }
                Bitmap bitmap = GetItem(item);
                bitmap.Tag = item;
                dgvData.Rows.Add(bitmap);
            }
            rbtnPageDown.WhetherEnable = lstRechargeTemplates.Count > CurrentPage * PageSize;
            Application.DoEvents();
            dgvData.ClearSelection();
            this.Activate();
        }

        Bitmap GetItem(ListAllTemplate item)
        {
            lblAmount.Text = item.amount.ToString();
            lblRewardAmount.Text = string.Format("赠送金额{0}元", item.rewardamount.ToString());


            //lblRewardAmount.Text = "";
            //if (item.rewardamount > 0)
            //{
            //    lblRewardAmount.Text = "赠" + item.rewardamount.ToString("f2") + "元";
            //}
            //lblRewardAmount.Text += item.couponname;
            Bitmap picItem = (Bitmap)MainModel.GetControlImage(pnlItem);
            return picItem;
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


    }
}
