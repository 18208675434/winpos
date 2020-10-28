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
    public partial class FormOtherMethod : Form
    {

        public ClassPayment SelectPayMent = null;

        public decimal currentamount = 0;

        public List<ClassPayment> CurrentPayments = new List<ClassPayment>();

        bool IsEnable = true;
        public FormOtherMethod(List<ClassPayment> payments,decimal amount)
        {
            InitializeComponent();
            CurrentPayments = payments;

            currentamount = amount;
        }
        HttpUtil httputil = new HttpUtil();
        private ClassPayment current = null;
        private Bitmap btnzffss(ClassPayment pay)
        {
            try
            {

                if (pay.defaultamt <= 5000)
                {

                    if (current != null && pay.id == current.id)
                    {

                        btnItem.ForeColor = Color.White;
                        btnItem.BackColor = Color.DeepSkyBlue;
                    }
                    else
                    {
                        btnItem.BackColor = Color.White;
                        btnItem.ForeColor = Color.Black;

                    }

                }
                else
                {
                    btnItem.ForeColor = Color.White;
                    btnItem.BackColor = Color.DeepSkyBlue;
                }

                btnItem.Text = pay.name;


                Bitmap b = (Bitmap)MainModel.GetControlImage(btnItem);
                b.Tag = pay;
                return b;

            }
            catch (Exception ex)
            {
                return Resources.ResourcePos.empty;
            }
        }
        public void Load1(bool need)
        {
            try
            {
                 dgvType.Rows.Clear();


                List<Bitmap> lstbmp = new List<Bitmap>();

                foreach (ClassPayment template in CurrentPayments)
                {
                    if (current == null && template.posenable == true)
                    {
                        current = template;
                    }
                    lstbmp.Add(btnzffss(template));
                }
                int emptycount = 4 - lstbmp.Count % 4;

                for (int i = 0; i < emptycount; i++)
                {
                    lstbmp.Add(Resources.ResourcePos.empty);
                }
                int rowcount = lstbmp.Count / 4;

                for (int i = 0; i < rowcount; i++)
                {
                    dgvType.Rows.Add(lstbmp[i * 4 + 0], lstbmp[i * 4 + 1], lstbmp[i * 4 + 2], lstbmp[i * 4 + 3]);
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }
        private void FormOtherMethod_Load(object sender, EventArgs e)
        {
            LoadDgvType();
            //btnItem.Height = dgvType.RowTemplate.Height - 5;
            //Load1(true);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            BackHelper.HideFormBackGround();
            this.Close();

        }
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                 if (!IsEnable || e.RowIndex < 0)
                {
                    return;
                }
                 Load1(true);
                Other.CrearMemory();
                Image selectimg = (Image)dgvType.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (selectimg.Tag == null)  //空白单元格（无商品）
                {
                    return;
                }
                ClassPayment temp = (ClassPayment)selectimg.Tag;
                MainModel.code = temp.code;

                FormReminder remm = new FormReminder(temp.name,currentamount);
                asf.AutoScaleControlTest(remm, 500, 180, 500 * MainModel.midScale, 180 * MainModel.midScale, true);
                remm.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - remm.Width) / 2, (Screen.AllScreens[0].Bounds.Height - remm.Height) / 2);
                remm.TopMost = true;
                BackHelper.ShowFormBackGround();
                //BackHelper.HideFormBackGround();
                this.Hide();
                remm.ShowDialog();
                remm.Dispose();
                Application.DoEvents();

                if (remm.DialogResult == DialogResult.OK)
                {
                    
                    SelectPayMent = temp;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    SelectPayMent = null;
                    this.DialogResult = DialogResult.Cancel;
                }
                this.Close();
                //ListAllTemplate.zhifu = temp.name;
                //if (temp.id != "")
                //{
                //    this.Close();

                //    FormReminder remm = new FormReminder();
                //    asf.AutoScaleControlTest(remm, 520, 170, 520 * MainModel.midScale, 170 * MainModel.midScale, true);
                //    remm.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - remm.Width) / 2, (Screen.AllScreens[0].Bounds.Height - remm.Height) / 2);
                //    remm.TopMost = true;
                //    BackHelper.ShowFormBackGround();
                //    //BackHelper.HideFormBackGround();
                //    this.Hide();
                //    remm.ShowDialog();
                //    if (MainModel.isokcancle == true)
                //    {
                //        return;
                //    }
                //    remm.Dispose();
                //    remm.Close();
                //}
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void FormOtherMethod_Shown(object sender, EventArgs e)
        {

        }

        #region

        private int CurrentPage = 1;
        private void rbtnPageUp_ButtonClick(object sender, EventArgs e)
        {

            if (!IsEnable || !rbtnPageUp.WhetherEnable)
            {
                return;
            }
            CurrentPage--;
            LoadDgvType();
        }

        private void rbtnPageDown_ButtonClick(object sender, EventArgs e)
        {
            if (!IsEnable || !rbtnPageDown.WhetherEnable)
            {
                return;
            }
            CurrentPage++;
            LoadDgvType();
        }


        public void LoadDgvType()
        {
            try
            {
                dgvType.Rows.Clear();


                    rbtnPageUp.WhetherEnable = CurrentPage>1;
               

                int startindex = (CurrentPage - 1) * 8;

                int lastindex = Math.Min(CurrentPayments.Count - 1, startindex + 7);


                List<ClassPayment> lstloadpay = CurrentPayments.GetRange(startindex, lastindex - startindex + 1);




                List<Bitmap> lstbmp = new List<Bitmap>();

                foreach (ClassPayment template in lstloadpay)
                {
                    if (current == null && template.posenable == true)
                    {
                        current = template;
                    }
                    lstbmp.Add(btnzffss(template));
                }
                int emptycount = 8 - lstloadpay.Count;

                for (int i = 0; i < emptycount; i++)
                {
                    lstbmp.Add(Resources.ResourcePos.empty);
                }

                for (int i = 0; i < 2; i++)
                {
                    dgvType.Rows.Add(lstbmp[i * 4 + 0], lstbmp[i * 4 + 1], lstbmp[i * 4 + 2], lstbmp[i * 4 + 3]);
                }

                    rbtnPageDown.WhetherEnable = CurrentPayments.Count>CurrentPage*8;

                    lblPage.Text = CurrentPage + "/" + Math.Ceiling((decimal)CurrentPayments.Count/8);

            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

    }
}
