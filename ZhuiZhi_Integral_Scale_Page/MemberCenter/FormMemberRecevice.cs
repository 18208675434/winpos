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
        Member member = new Member();
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //BackHelper.ShowFormBackGround();
            BackHelper.HideFormBackGround();
            GlobalUtil.CloseOSK();
            this.Close();
            
        }
        public void GetNewPhone()
        {
            txtOldCardNumber.Text = MainModel.NewPhone;
        }
        private void btOldCardOK_Click(object sender, EventArgs e)
        {
            


            string ErrorMsgMember= "";
            string phone =  txtOldCardNumber.Text;
            HttpUtil httputil = new HttpUtil();
            member = httputil.GetMember(phone, ref ErrorMsgMember);
            FormChangePhoneNumber con = new FormChangePhoneNumber(member);
            
            if (member == null)
            {

                MainModel.ShowLog("不能为空", false);
            }
            else
            {

                FormChangePhonePayPwd pwd = new FormChangePhonePayPwd(member);

                asf.AutoScaleControlTest(pwd, 380, 197, 380 * MainModel.midScale, 197 * MainModel.midScale, true);
                pwd.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - pwd.Width) / 2, (Screen.AllScreens[0].Bounds.Height - pwd.Height) / 2);
                pwd.TopMost = true;
                pwd.ShowDialog();

                if (pwd.DialogResult == DialogResult.OK)
                {

                    

                    string number = txtOldCardNumber.Text;

                    con.GetPhone(number);
                    this.Close();

                }
                else if (pwd.DialogResult == DialogResult.Cancel)
                {
                    GlobalUtil.CloseOSK();
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
    }
}
