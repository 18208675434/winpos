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
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormMemberRecevice : Form
    {
        public FormMemberRecevice()
        {
            InitializeComponent();

        }
        
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            
        }
        
        public void GetNewPhone()
        {
            
            txtOldCardNumber.Text = MainModel.NewPhone;

        }
       
        private void btOldCardOK_Click(object sender, EventArgs e)
        {
            


            string ErrorMsgMember= "";
            MainModel.NewPhone =  txtOldCardNumber.Text;
           // txtOldCardNumber.Text = MainModel.UpdatePhone;
            HttpUtil httputil = new HttpUtil();
            MemberCenterHelper.member = httputil.GetMember(MainModel.NewPhone, ref ErrorMsgMember);

            if (MemberCenterHelper.member == null)
            {

                MainModel.ShowLog("不能为空", false);
            }
            else
            {

                FormChangePhonePayPwd pwd = new FormChangePhonePayPwd(MemberCenterHelper.member);

                asf.AutoScaleControlTest(pwd, 380, 197, 380 * MainModel.midScale, 197 * MainModel.midScale, true);
                pwd.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - pwd.Width) / 2, (Screen.AllScreens[0].Bounds.Height - pwd.Height) / 2);
                pwd.TopMost = true;
                pwd.ShowDialog();
                

                if (pwd.DialogResult == DialogResult.OK)
                {


                    this.DialogResult = System.Windows.Forms.DialogResult.OK;

                    MainModel.NewPhone = txtOldCardNumber.Text;

                    
                    BackHelper.HideFormBackGround();
                    this.Close();

                }
                else if (pwd.DialogResult == DialogResult.Cancel)
                {
                    GlobalUtil.CloseOSK();
                    BackHelper.HideFormBackGround();
                    this.Close();
                    return;
                }
                else
                {
                    this.Close();
                    return;
                }
             


            }
        }
        
        

        private void FormMemberRecevice_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            //BackHelper.ShowFormBackGround();
            BackHelper.HideFormBackGround();
            GlobalUtil.CloseOSK();
            this.Close();
        }
    }
}
