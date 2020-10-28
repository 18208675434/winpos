using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
   
    public partial class FormCreateMemberMedia : Form
    {

        private HttpUtil httputil = new HttpUtil();
        public FormCreateMemberMedia()
        {
            InitializeComponent();

            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
        }




        public void LoadScanInfo()
        {
            string ErrorMsg = "";
            string imgurl = httputil.GetMemberCard(ref ErrorMsg);
            if (!string.IsNullOrEmpty(imgurl) && string.IsNullOrEmpty(ErrorMsg))
            {
                //this.Invoke(new InvokeHandler(delegate()
                //{

                Image _image = Image.FromStream(System.Net.WebRequest.Create(imgurl).GetResponse().GetResponseStream());
                picMember.BackgroundImage = _image;
            }
        }

        public void UpdateMemberInfo(string phone,string name,string birthday,string gender)
        {
            lblPhone.Text = phone;
            lblName.Text = name;
            lblBirthday.Text = birthday;
            lblGender.Text = gender;
        }

        public void UpdateType(int select)
        {
            if (select == 0)
            {
                tlpType.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
                tlpType.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
            }
            else
            {
                tlpType.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
                tlpType.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);
            }
        }

        private void FormCreateMemberMedia_Shown(object sender, EventArgs e)
        {
            lblShopName.Text = ZhuiZhi_Integral_Scale_UncleFruit.Model.MainModel.Titledata + "   " + ZhuiZhi_Integral_Scale_UncleFruit.Model.MainModel.CurrentShopInfo.shopname;
            bgwLoadMemberCard.RunWorkerAsync();
        }

        private void bgwLoadMemberCard_DoWork(object sender, DoWorkEventArgs e)
        {
            string ErrorMsg = "";
            string imgurl = httputil.GetMemberCard(ref ErrorMsg);
            if (!string.IsNullOrEmpty(imgurl) && string.IsNullOrEmpty(ErrorMsg))
            {
                //this.Invoke(new InvokeHandler(delegate()
                //{

                Image _image = Image.FromStream(System.Net.WebRequest.Create(imgurl).GetResponse().GetResponseStream());
                picMember.BackgroundImage = _image;
            }
        }
    }
}
