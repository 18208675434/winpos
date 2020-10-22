using Maticsoft.BLL;
using Maticsoft.Model;
using Newtonsoft.Json;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MenuUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    /// <summary>
    /// 挂失
    /// </summary>
    public partial class FormEntityCardBind : Form
    {

        #region 成员变量
        MemberCenterHttpUtil memberCenterHttpUtil = new MemberCenterHttpUtil();
        HttpUtil httpUtil = new HttpUtil();
        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        OutEntityCardResponseDto entityCard;//实体卡
        #endregion

        #region  页面加载与退出
        public FormEntityCardBind(OutEntityCardResponseDto entityCard)
        {
            InitializeComponent();
            this.entityCard = entityCard;

        }
        private void FormLossEntityCard_Load(object sender, EventArgs e)
        {
            lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;
            lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
            picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
            lblMenu.Left = picMenu.Right;


        }
        private void FormLossEntityCard_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            lblEntityCardNo.Text = entityCard.outcardid;
            lblMemberId.Text = MainModel.CurrentMember.memberheaderresponsevo.mobile;
            lblBalance.Text = "￥" + entityCard.balance.ToString("f2");
            lblBalaceDesc.Location = new Point(lblBalance.Right + 20, lblBalance.Location.Y);
            MemberCenterMediaHelper.ShowFormBindEntityCardMedia(entityCard);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void formRechargeQuery_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemberCenterMediaHelper.CloseLossEntityCardMedai();
        }
        #endregion

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                if (entityCard != null)
                {
                    EntityCardMoveRequest entityCardMoveRequest = new EntityCardMoveRequest();
                    entityCardMoveRequest.oldentitycardnumber = entityCard.outcardid;
                    entityCardMoveRequest.phone = MainModel.CurrentMember.memberheaderresponsevo.mobile;
                    entityCardMoveRequest.amount = entityCard.balance;   
                    string err ="";
                    LoadingHelper.ShowLoadingScreen();
                    bool flag = new MemberCenterHttpUtil().EntityCardMove(entityCardMoveRequest, ref err);
                    if (!flag)
                    {
                        MainModel.ShowLog(err);
                        return;
                    }
                    MainModel.ShowLog("绑卡成功");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "挂失异常:" + ex.Message);
                MainModel.ShowLog("挂失异常:" + ex.Message, true);
            }
            finally
            {
                LoadingHelper.CloseForm();
                this.Enabled = true;
            }
        }
    }
}
