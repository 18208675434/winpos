using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI
{
    public partial class FormBrokenBatch : Form
    {
        private Bitmap bmpSelect;
        private List<BrokenType> CurrentBrokenTypes = null;

        public int SelectTypeKey = -1;
        public string SelectTypeValue = "";
        public FormBrokenBatch()
        {
            InitializeComponent();
        }

        private void FormBrokenBatch_Shown(object sender, EventArgs e)
        {
            try
            {
                Application.DoEvents();

                CurrentBrokenTypes = BrokenHelper.GetBrokenType(false);
                bmpSelect = new Bitmap(picCheck.BackgroundImage, dgvCoupon.RowTemplate.Height * 30 / 100, dgvCoupon.RowTemplate.Height * 30 / 100);

                CurrentPage = 1;
                LoadDgvCoupon(false);
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载批量报损页面异常"+ex.Message,true);
            }
        }


        #region 分页
        private int CurrentPage = 1;

        private void rbtnPageUp_ButtonClick(object sender, EventArgs e)
        {
            if (!rbtnPageUp.WhetherEnable)
            {
                return;
            }
            CurrentPage--;
            LoadDgvCoupon(false);
        }

        private void rbtnPageDown_ButtonClick(object sender, EventArgs e)
        {
            if (!rbtnPageDown.WhetherEnable)
            {
                return;
            }
            CurrentPage++;
            LoadDgvCoupon(false);
        }


        /// <summary>
        /// 分页加载电子秤列表
        /// </summary>
        /// <param name="needRefresh">是否需要刷新</param>
        private void LoadDgvCoupon(bool needRefresh)
        {
            try
            {
                dgvCoupon.Rows.Clear();
                if (CurrentBrokenTypes == null || CurrentBrokenTypes.Count == 0)
                {
                    return;
                }
                if (CurrentPage > 1)
                {
                    rbtnPageUp.WhetherEnable = true;
                }
                else
                {
                    rbtnPageUp.WhetherEnable = false;
                }
                int startindex = (CurrentPage - 1) * 5;

                int lastindex = Math.Min(CurrentBrokenTypes.Count - 1, startindex + 4);

                List<BrokenType> LstTempcoupon = CurrentBrokenTypes.GetRange(startindex, lastindex - startindex + 1);

                foreach (BrokenType typee in LstTempcoupon)
                {
                    dgvCoupon.Rows.Add(typee.key,typee.value,bmpSelect);
                }
                dgvCoupon.ClearSelection();
                Application.DoEvents();

                if (CurrentBrokenTypes.Count > CurrentPage * 5)
                {
                    rbtnPageDown.WhetherEnable = true;
                }
                else
                {
                    rbtnPageDown.WhetherEnable = false;
                }

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载订单列表异常" + ex.Message, true);
            }
        }

        #endregion

        private void dgvCoupon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                SelectTypeKey = Convert.ToInt16(dgvCoupon.Rows[e.RowIndex].Cells[0].Value);

                this.Close();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("选择批量报损类型异常"+ex.Message,true);
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
