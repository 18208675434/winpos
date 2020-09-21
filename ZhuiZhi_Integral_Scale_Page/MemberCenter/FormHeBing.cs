using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormHeBing : Form
    {
        public FormHeBing()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormHeBing_Load(object sender, EventArgs e)
        {


            label2.Text = "账户" + MainModel.NewPhone + "已注册为会员，是否合并账户？\n合并后本账户的积分和余额将迁移到新手机号的账户中。\n订单数据将不会迁移";
            label2.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        MemberCenterHttpUtil memberchttputil = new MemberCenterHttpUtil();
        HttpUtil httputil = new HttpUtil();
        private void button3_Click(object sender, EventArgs e)
        {
            string errrormsg = "";
            MainModel.Sourcetoken = MainModel.CurrentMember.memberheaderresponsevo.token;
            
            bool resule = memberchttputil.MergeMemberPhonenumber(ref errrormsg);
            
            if (resule)
            {
                
                this.DialogResult = DialogResult.OK;
                this.Close();
                
                 
            }
            else
            {
                MainModel.ShowLog(errrormsg, false);

                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            //FormChangePhoneConfirm con = new FormChangePhoneConfirm();

            //con.Hebing();
            //con.Dispose();


            //this.DialogResult = DialogResult.OK;
            //this.Close();
            //BackHelper.HideFormBackGround();

        }
    }
}
