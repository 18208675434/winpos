using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormCreateMember : Form
    {

        private Color SelectColor = Color.FromArgb(34, 190, 139);
        private Color NotSelectColor = Color.FromArgb(230,230,230);

        public Member CurrentMember = null;

        private MemberCenterHttpUtil memberhttp = new MemberCenterHttpUtil();
        private HttpUtil httputil = new HttpUtil();

        private bool IsEnable =true;
        #region 页面加载与退出
        public FormCreateMember()
        {
            InitializeComponent();
        }

        private void FormCreateMember_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            CreateMemberMediaHelper.ShowFormCreateMedia();
        }

        private void FormCreateMember_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsEnable = true;
            LoadingHelper.CloseForm();

            CreateMemberMediaHelper.CloseFormCreateMedai();
            GlobalUtil.CloseOSK();

        }
        
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void btnCreateByScan_Click(object sender, EventArgs e)
        {
            if (btnCreateByScan.Tag == null || btnCreateByScan.Tag.ToString() == "0")
            {
                return;
            }

            CreateMemberMediaHelper.UpdateType(0);

            tlpType.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
            tlpType.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);

            btnCreateByScan.BackColor = SelectColor;
            btnCreateByScan.ForeColor = Color.White;
            btnCreateByWrite.BackColor = NotSelectColor;
            btnCreateByWrite.ForeColor = Color.Black;
            btnCreateByScan.Tag = 0;
            btnCreateByWrite.Tag = 1;
        }

        private void btnCreateByWrite_Click(object sender, EventArgs e)
        {
            if (btnCreateByWrite.Tag == null || btnCreateByWrite.Tag.ToString() == "0")
            {
                return;
            }

            CreateMemberMediaHelper.UpdateType(1);

            tlpType.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
            tlpType.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);


            btnCreateByScan.BackColor = NotSelectColor;
            btnCreateByScan.ForeColor = Color.Black;
            btnCreateByWrite.BackColor = SelectColor;
            btnCreateByWrite.ForeColor = Color.White;
            btnCreateByScan.Tag = 1;
            btnCreateByWrite.Tag = 0;
        }

        private void mcBirthday_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtBirthday.Text = mcBirthday.SelectionStart.ToString("yyyy-MM-dd");
            mcBirthday.Visible = false;
        }

        private void txtBirthday_Enter(object sender, EventArgs e)
        {
            mcBirthday.Top = this.Height - mcBirthday.Height;
            mcBirthday.Visible = true;
        }

        private void txtBirthday_Leave(object sender, EventArgs e)
        {
            mcBirthday.Visible = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try{
                if (!IsEnable)
                {
                    return;
                }
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MainModel.ShowLog("账号不能为空",false);
                return;
            }
            IsEnable = false;
            LoadingHelper.ShowLoadingScreen();

            CreateMemberPara para = new CreateMemberPara();
            para.mobile = txtPhone.Text;
            if (string.IsNullOrEmpty(txtBirthday.Text))
            {
                para.birthday = MainModel.getStampByDateTime(mcBirthday.SelectionStart);
            }

            if (picMan.BackgroundImage == picSelect.Image)
            {
                para.gender = "0";
            }
            else if (picWoman.BackgroundImage == picSelect.Image)
            {
                para.gender = "1";
            }

            string errormsg = "";

            if (memberhttp.CreateMember(para, ref errormsg))
            {
                CurrentMember = httputil.GetMember(txtPhone.Text,ref errormsg);
                IsEnable = true;
                LoadingHelper.CloseForm();
                this.Close();
            }
            else
            {
                MainModel.ShowLog(errormsg,false);

                IsEnable = true;
                LoadingHelper.CloseForm();
                return;
            }

            //memberhttp.
            }catch(Exception ex){
                IsEnable = true;
                LoadingHelper.CloseForm();
            }
        }

        private void pnlMan_Click(object sender, EventArgs e)
        {
            picMan.BackgroundImage = picSelect.Image;
            picWoman.BackgroundImage = picNotSelect.Image;
            txt_DataChanged("");
        }

        private void pnlWoman_Click(object sender, EventArgs e)
        {
            picMan.BackgroundImage = picNotSelect.Image;
            picWoman.BackgroundImage = picSelect.Image;

            txt_DataChanged("");
        }

        private void txt_DataChanged(string data)
        {
            string phone = txtPhone.Text;
            string name = txtName.Text;
            string birthday = txtBirthday.Text;
            string gender = "";

            if (picMan.BackgroundImage == picSelect.Image)
            {
                gender = "男";
            }
            else if (picWoman.BackgroundImage == picSelect.Image)
            {
                gender = "女";
            }

            CreateMemberMediaHelper.UpdateMemberInfo(phone,name,birthday,gender);
        }



     




    }
}
