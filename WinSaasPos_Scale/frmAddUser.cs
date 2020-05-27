using Maticsoft.BLL;
using Maticsoft.Model;
using WinSaasPOS_Scale.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinSaasPOS_Scale
{
    public partial class frmAddUser : Form
    {

        private SYSTEM_USER_BEANBLL userbll = new SYSTEM_USER_BEANBLL();

        public SYSTEM_USER_BEANMODEL CurrentUser = new SYSTEM_USER_BEANMODEL();
        public string username ="";
        public string userphone = "";

        public frmAddUser()
        {
            InitializeComponent();
        }

        private void pnlName_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Control con = (Control)sender;

                // Draw(e.ClipRectangle, e.Graphics, 100, false, Color.FromArgb(113, 113, 113), Color.FromArgb(0, 0, 0));
                //base.OnPaint(e);
                Graphics g = e.Graphics;
                // g.DrawString("", new Font("微软雅黑", 9, FontStyle.Regular), new SolidBrush(Color.White), new PointF(10, 10));

                LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush(e.ClipRectangle, Color.Gainsboro, Color.Gainsboro, LinearGradientMode.Vertical);
                //填充         

                ////四边圆角
                GraphicsPath gp = new GraphicsPath();
                gp.AddArc(e.ClipRectangle.X, e.ClipRectangle.Y, con.Height, con.Height, 180, 90);
                gp.AddArc(e.ClipRectangle.Width - 2 - con.Height, e.ClipRectangle.Y, con.Height, con.Height, 270, 90);
                gp.AddArc(e.ClipRectangle.Width - 2 - con.Height, e.ClipRectangle.Height - 1 - con.Height, con.Height, con.Height, 0, 90);
                gp.AddArc(e.ClipRectangle.Y, e.ClipRectangle.Height - 1 - con.Height, con.Height, con.Height, 90, 90);
                gp.CloseAllFigures();

                g.FillPath(myLinearGradientBrush, gp);
            }
            catch { }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string phone = txtPhone.Text;
            if (phone.Length != 11 || phone.Substring(0, 1) != "1")
            {
                MainModel.ShowLog("请输入正确的手机号",false );
                return;
            }

            string name = txtName.Text;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MainModel.ShowLog("员工姓名不能为空", false);
                return;
            }

            CurrentUser = new SYSTEM_USER_BEANMODEL();
            CurrentUser.LOGINACCOUNT = phone;
            CurrentUser.NICKNAME = name;
            CurrentUser.CREATE_URL_IP = MainModel.URL;

            userbll.Add(CurrentUser);
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lblName_Click(object sender, EventArgs e)
        {
            txtName.Focus();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                lblName.Visible = false;
            }
            else
            {
                lblName.Visible = true;
            }
        }

        private void lblPhone_Click(object sender, EventArgs e)
        {
            txtPhone.Focus();
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPhone.Text))
            {
                lblPhone.Visible = false;
            }
            else
            {
                lblPhone.Visible = true;
            }
        }


        //控制仅允许录入数字
        private void TextNUMBER_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                e.Handled = true;
                char ch = e.KeyChar;

                if (ch >= '0' && ch <= '9')
                    e.Handled = false;

                if (ch == (char)Keys.Back)
                    e.Handled = false;

            }
            catch { }
        }
    }
}
