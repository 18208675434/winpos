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

        bool IsEnable = true;
        public FormOtherMethod()
        {
            InitializeComponent();
        }
        HttpUtil httputil = new HttpUtil();
        private ClassPayment current = null;
        private List<ClassPayment> payment = new List<ClassPayment>();
        private Bitmap btnzffss(ClassPayment pay)
        {
            try
            {

                if (pay.defaultamt <= 5000)
                {

                    if (current != null && pay.id == current.id)
                    {

                        btnzffs.ForeColor = Color.White;
                        btnzffs.BackColor = Color.Blue;
                    }
                    else
                    {
                        btnzffs.BackColor = Color.White;
                        btnzffs.ForeColor = Color.Black;

                    }

                }
                else
                {
                    btnzffs.ForeColor = Color.White;
                    btnzffs.BackColor = Color.Blue;
                }

                btnzffs.Text = pay.name;


                Bitmap b = (Bitmap)MainModel.GetControlImage(btnzffs);
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
                 dataGridView1.Rows.Clear();
                 if (payment == null || payment.Count == 0 || need)
                {

                    string error = "";


                    payment = httputil.Custompaycon(ref error);

                    if (payment == null || !string.IsNullOrEmpty(error))
                    {
                        MainModel.ShowLog(error, false);
                        return;
                    }
                }

                List<Bitmap> lstbmp = new List<Bitmap>();

                foreach (ClassPayment template in payment)
                {
                    if (current == null && template.posenable == true)
                    {
                        current = template;
                    }
                    lstbmp.Add(btnzffss(template));
                }
                int emptycount = 3 - lstbmp.Count % 3;

                for (int i = 0; i < emptycount; i++)
                {
                    lstbmp.Add(Resources.ResourcePos.empty);
                }
                int rowcount = lstbmp.Count / 3;

                for (int i = 0; i < rowcount; i++)
                {
                    dataGridView1.Rows.Add(lstbmp[i * 3 + 0], lstbmp[i * 3 + 1], lstbmp[i * 3 + 2]);
                }
                MemberCenterMediaHelper.UpdateDgvTemplate(lstbmp);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        private void FormOtherMethod_Load(object sender, EventArgs e)
        {
            Load1(true);
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
                Image selectimg = (Image)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (selectimg.Tag == null)  //空白单元格（无商品）
                {
                    return;
                }
                ClassPayment temp = (ClassPayment)selectimg.Tag;
                MainModel.code = temp.code;
                ListAllTemplate.zhifu = temp.name;
                if (temp.id != "")
                {
                    this.Close();

                    FormReminder remm = new FormReminder();
                    asf.AutoScaleControlTest(remm, 520, 170, 520 * MainModel.midScale, 170 * MainModel.midScale, true);
                    remm.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - remm.Width) / 2, (Screen.AllScreens[0].Bounds.Height - remm.Height) / 2);
                    remm.TopMost = true;
                    BackHelper.ShowFormBackGround();
                    //BackHelper.HideFormBackGround();
                    this.Hide();
                    remm.ShowDialog();
                    if (MainModel.isokcancle == true)
                    {
                        return;
                    }
                    remm.Dispose();
                    remm.Close();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
