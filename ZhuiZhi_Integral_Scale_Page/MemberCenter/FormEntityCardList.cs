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
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormEntityCardList : Form
    {
        MemberCenterHttpUtil memberCenterHttpUtil = new MemberCenterHttpUtil();
        List<OutEntityCardResponseDto> outentitycards;
        public FormEntityCardList(List<OutEntityCardResponseDto> outentitycards)
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;          
            this.outentitycards = outentitycards;
        }

        private void FormEntityCardList_Shown(object sender, EventArgs e)
        {
            BindEntityCards();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FormBackGround tempfrmback = new FormBackGround();
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                if (dgvData.Columns[e.ColumnIndex].Name == "colButton")
                {
                    tempfrmback = new FormBackGround();
                    tempfrmback.Location = new System.Drawing.Point(0, 0);
                    tempfrmback.TopMost = true;
                    tempfrmback.Show();

                    OutEntityCardResponseDto entityCard = (OutEntityCardResponseDto)dgvData.Rows[e.RowIndex].Tag;
                    if (entityCard.status=="LOST")
                    {
                        return;
                    }
                    if (ConfirmHelper.Confirm("提示", "实体卡号" + entityCard.outcardid + "，是否确认挂失？\r\n挂失后，该卡将无法使用。", true, false))
                    {
                        string err = "";
                        LoadingHelper.ShowLoadingScreen();
                        if (!memberCenterHttpUtil.LossEntityCard(entityCard.outcardid, ref err))
                        {
                            MainModel.ShowLog(err);
                            return;
                        }
                        MainModel.CurrentMember = new HttpUtil().GetMember(MainModel.CurrentMember.memberheaderresponsevo.mobile, ref err);                       
                        MainModel.ShowLog("挂失成功");
                        //entityCard.status = "LOST";
                        //dgvData.Rows[e.RowIndex].Tag = entityCard;
                        //dgvData.Rows[e.RowIndex].Cells["colButton"].Value=Resources.ResourcePos.empty;
                        //dgvData.Rows[e.RowIndex].Cells["status"].Value = GetStatusDesc(entityCard.status);
                        //dgvData.Rows[e.RowIndex].Cells["status"].Style.ForeColor = Color.FromArgb(153, 153, 153);
                        //dgvData.Rows[e.RowIndex].Cells["status"].Style.SelectionForeColor = Color.FromArgb(153, 153, 153);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("挂失失败：" + ex.Message, true);
            }
            finally
            {
                LoadingHelper.CloseForm();
                if (tempfrmback != null)
                {
                    tempfrmback.Close();
                }
            }
        }

        private Bitmap bmpLoss;
        void BindEntityCards()
        {
            dgvData.Rows.Clear();
            foreach (var item in outentitycards)
            {
                if (bmpLoss == null)
                {
                    bmpLoss = (Bitmap)MainModel.GetControlImage(btnLoss);
                }
                string type = GetCardTypeDesc(item.type);
                string status = GetStatusDesc(item.status);

                if (item.status == "ACTIVE")
                {
                    dgvData.Rows.Add(item.outcardid, type, status, bmpLoss);
                    dgvData.Rows[dgvData.Rows.Count - 1].Cells["status"].Style.ForeColor = Color.FromArgb(20, 137, 205);
                    dgvData.Rows[dgvData.Rows.Count - 1].Cells["status"].Style.SelectionForeColor = Color.FromArgb(20, 137, 205);
                }
                else
                {                   
                    dgvData.Rows.Add(item.outcardid, type, status, Resources.ResourcePos.empty);
                    dgvData.Rows[dgvData.Rows.Count - 1].Cells["status"].Style.ForeColor = Color.FromArgb(153, 153, 153);
                    dgvData.Rows[dgvData.Rows.Count - 1].Cells["status"].Style.SelectionForeColor = Color.FromArgb(153, 153, 153);
                }                

                dgvData.Rows[dgvData.Rows.Count - 1].Tag = item;
            }
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

        string GetStatusDesc(string status)
        {
            if (status == "INIT")
            {
                return "未使用";
            }
            if (status == "LOST")
            {
                return "已挂失";
            }
            if (status == "ACTIVE")
            {
                return "已激活";
            }
            return status;
        }

        string GetCardTypeDesc(string type)
        {
            if (type == "OLD_CARD")
            {
                return "旧卡";
            }
            if (type == "NEW_CARD")
            {
                return "实体卡";
            }
            return "";
        }
    }
}
