using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QiandamaPOS
{
    public partial class FormMainMedia : Form
    {
        public FormMainMedia()
        {
            InitializeComponent();
            IniForm();
        }
        public void IniForm()
        {
    

        }

        public void UpdateForm(Cart Cart, Member member)
        {

           
        }

        public void ShowPayResult(object payinfo)
        {
            //try
            //{

            //    //frmCashierResultMedia frmresult = new frmCashierResultMedia(payinfo.ToString());
            //    //frmresult.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width, 0);

            //    //frmresult.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //    //frmresult.Show();

            //    frmCashierResultMedia frmresult = new frmCashierResultMedia(payinfo.ToString());
            //    frmresult.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width, 0);

            //    frmresult.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //    frmresult.Show();
            //    Application.DoEvents();
            //}
            //catch (Exception ex)
            //{
            //    LogManager.WriteLog("客屏展示收银成功异常" + ex.Message);
            //}
        }

        public void ShowNumber()
        {

            frmNumber frmnumber = new frmNumber("请输入会员号", true);

            frmnumber.frmNumber_SizeChanged(null, null);
            frmnumber.Size = new System.Drawing.Size(this.Width / 3, this.Height - 200);
            // frmnumber.Location = new System.Drawing.Point(this.Width - frmnumber.Width - 50, 100);

            frmnumber.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width + Screen.AllScreens[1].Bounds.Width - frmnumber.Width - 50, 100);

            //frmresult.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            frmnumber.Show();
            Application.DoEvents();


        }

        private void frmMainMedia_SizeChanged(object sender, EventArgs e)
        {
           // asf.ControlAutoSize(this);
        }
    }
}
