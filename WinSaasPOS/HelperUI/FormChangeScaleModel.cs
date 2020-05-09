using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinSaasPOS.Common;

namespace WinSaasPOS.HelperUI
{
    public partial class FormChangeScaleModel : Form
    {

        
        public FormChangeScaleModel()
        {
            InitializeComponent();
        }


        public void UpInfo(ChangeModel model)
        {
            try
            {
                if (model == ChangeModel.WeightAndScale)
                {
                    lblTitle.Text = "切换秤模式";
                    lblInfo.Text = "(当前模式：称重收银一体模式)";
                    lblMsg.Text = "您确认要将称重收银一体模式切换为常规秤模式吗？";
                    lblInfo.Left = lblTitle.Left + lblTitle.Width;
                }
                else
                {
                    lblTitle.Text = "切换称重收银一体模式";
                    lblInfo.Text = "(当前模式：秤模式)";
                    lblMsg.Text = "您确认要将秤模式切换为称重收银一体模式吗？";
                    lblInfo.Left = lblTitle.Left + lblTitle.Width;
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新切换秤模式窗体异常"+ex.Message);
            }
        }

        private void lbtnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lbtnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }

    public enum ChangeModel
    {
        None,
        WeightAndScale,
        OnlyScale
    }
}
