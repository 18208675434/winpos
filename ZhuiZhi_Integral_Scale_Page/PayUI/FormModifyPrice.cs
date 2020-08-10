using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.PayUI
{
    public partial class FormModifyPrice : Form
    {
        //private Cart thisCurrentCart = new Cart();
        private static decimal CurrentTotalpaymentBeforeFix = 0;

        public decimal fixpricetotal = 0;


        public FormModifyPrice()
        {
            InitializeComponent();
        }



        private void lblShuiyin_Click(object sender, EventArgs e)
        {
            txtCash.Focus();
            this.txtCash.Select(this.txtCash.TextLength, 0);
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtCash.Text.Length > 0)
                {
                    lblShuiyin.Visible = false;
                }
                else
                {
                    lblShuiyin.Visible = true;
                }

                double doublenum = Convert.ToDouble(txtCash.Text);

                if (doublenum > 0)
                {
                    btnNext.BackColor = Color.OrangeRed;

                }
                else
                {
                    btnNext.BackColor = Color.Silver;

                }
            }
            catch
            {
                btnNext.BackColor = Color.Silver;
            }
            finally
            {
                txtCash.Focus();
                this.txtCash.Select(this.txtCash.TextLength, 0);
            }

        }



        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            try
            {
                //小数点后允许2
                if (txtCash.Text.Length > 3 && txtCash.Text.Substring(txtCash.Text.Length - 3, 1) == ".")
                {
                    return;
                }

                decimal CheckDecimal = Convert.ToDecimal(txtCash.Text + btn.Name.Replace("btn", ""));

                if (CheckDecimal > 100000 || txtCash.Text == "00")
                {
                    return;
                }
                txtCash.Text += btn.Name.Replace("btn", "");
            }
            catch
            {

            }



        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (txtCash.Text.Length > 0 && !txtCash.Text.Contains("."))
            {
                txtCash.Text += ".";
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {

            if (txtCash.Text.Length > 0)
            {
                txtCash.Text = txtCash.Text.Substring(0, txtCash.Text.Length - 1);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    double doublenum = Convert.ToDouble(txtCash.Text);

                    if (doublenum <= 0)
                    {
                        return;
                    }
                }
                catch
                {
                    return;
                }

                fixpricetotal = Convert.ToDecimal(txtCash.Text);
                if (fixpricetotal > CurrentTotalpaymentBeforeFix)
                {
                    MainModel.ShowLog("最终收取的金额必须小于订单应收", false);
                    return;
                }
                this.DialogResult = DialogResult.OK;

                this.Close();

            }
            catch (Exception ex)
            {
            }
        }

        private void lbtnCancle_Click(object sender, EventArgs e)
        {
            try
            {

                fixpricetotal = 0;
                 this.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public void UpInfo(decimal totoalpaymentbeforefix)
        {
            try
            {
                txtCash.Text = "";
                CurrentTotalpaymentBeforeFix = totoalpaymentbeforefix;
               // btnDel.Focus();
                lblPrice.Text = "￥" + totoalpaymentbeforefix.ToString("f2");
                // fixpricetotal = 0;
                txtCash.Focus();
                this.txtCash.Select(this.txtCash.TextLength, 0);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("修改支付金额窗体异常" + ex.Message);
            }
        }

        private void FormModifyPrice_Resize(object sender, EventArgs e)
        {
            SetWindowRegion();
        }



        /// <summary>
        /// 设置窗体的Region   画半径为10的圆角
        /// </summary>
        public void SetWindowRegion()
        {
            try
            {
                GraphicsPath FormPath;
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                FormPath = GetRoundedRectPath(rect, 10);
                this.Region = new Region(FormPath);
            }
            catch (Exception ex)
            {

            }

        }
        /// <summary>
        /// 绘制圆角路径
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            try
            {
                int diameter = radius;
                Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
                GraphicsPath path = new GraphicsPath();

                // 左上角
                path.AddArc(arcRect, 180, 90);

                // 右上角
                arcRect.X = rect.Right - diameter;
                path.AddArc(arcRect, 270, 90);

                // 右下角
                arcRect.Y = rect.Bottom - diameter;
                path.AddArc(arcRect, 0, 90);

                // 左下角
                arcRect.X = rect.Left;
                path.AddArc(arcRect, 90, 90);
                path.CloseFigure();//闭合曲线
                return path;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                if (keyData == Keys.Enter)
                {
                    btnNext_Click(null, null);
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }


        }

    }
}
